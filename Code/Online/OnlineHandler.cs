#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
//using Microsoft.Xna.Framework.Design;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;
using Microsoft.Xna.Framework.Net;
#endregion

namespace Orb
{
    public class OnlineHandler
    {
        public PacketWriter packetWriter = new PacketWriter();
        public PacketReader packetReader = new PacketReader();

        public NetworkSessionType SessionType = NetworkSessionType.PlayerMatch;

        public AvailableNetworkSessionCollection availableSessions;

        public IAsyncResult FindResult;
        public IAsyncResult CreateResult;
        public IAsyncResult JoinResult;

        public NetworkSession networkSession;

        public string OnlineString;
        public string OnlineEventString;
        public float OnlineStringAlpha = 0;

        public bool FoundSessions = false;
        public bool IsSearching = false;
        public bool IsCreating = false;
        public bool IsJoining = false;

        public bool HasUpdateLocal;

        public int FutureMap = -10;

        public int FutureGameMode = -10;

        public bool IsTryingToGoToGame = false;

        public int Timer = 0;

        Game1 game;

        public void Load(Game1 gme)
        {
            game = gme;
        }

        public void BeginGameSearch()
        {
            if (networkSession == null)
            {
                IsSearching = true;

                SetString("Searching for Games...");

                try
                {
                    FindResult = NetworkSession.BeginFind(SessionType, 4, null, null, null);
                }
                catch (Exception e)
                {
                    // if (networkSession == null)
                    OnlineString = e.Message;
                }
            }
        }

        public void EndGameSearch()
        {
            IsSearching = false;
            IsCreating = false;
            IsJoining = false;
        }

        public void SetString(string text)
        {
            OnlineString = text;
            OnlineStringAlpha = 1;
        }

        public void CreateGame()
        {
            IsCreating = true;

            SetString("Creating Game  ...");

            CreateResult = NetworkSession.BeginCreate(SessionType, 4, game.maxOrbs, null, null);
        }

        public void SortSessions()
        {
            AvailableNetworkSession BestSession = null;
            double BestQuality = 1000000000;


            foreach (AvailableNetworkSession session in availableSessions)
            {
                double Val = session.QualityOfService.AverageRoundtripTime.TotalMilliseconds;
                if (Val < BestQuality)
                {
                    BestQuality = Val;
                    BestSession = session;
                }
            }
            if (BestSession != null)
                JoinSession(BestSession);
            else
                CreateGame();

            IsSearching = false;

        }

        public void JoinSession(AvailableNetworkSession session)
        {
            IsJoining = false;

            networkSession = NetworkSession.Join(session);

            HookSessionEvents();

            BeginOnlineGameAsGuest();
        }

        public void ReturnOrbsToAI()
        {
            foreach (BasicOrb orb in game.Orbs)
            {
                orb.IsTakenByOnlinePlayer = false;
                orb.IsTakenByLocalPlayer = false;
            }

            foreach (NetworkGamer gamer in networkSession.LocalGamers)
                if (gamer.Tag != null)
                {
                    BasicOrb orb = gamer.Tag as BasicOrb;
                    orb.IsTakenByLocalPlayer = true;
                }

            foreach (NetworkGamer gamer in networkSession.AllGamers)
                if (gamer.Tag != null)
                {
                    BasicOrb orb = gamer.Tag as BasicOrb;

                    orb.IsTakenByOnlinePlayer = true;

                    if (!orb.IsTakenByLocalPlayer)
                        if (!orb.IsOnline)
                            orb.ChangeToOnline(game, gamer);
                }

            foreach (BasicOrb orb in game.Orbs)
                if (!orb.IsTakenByOnlinePlayer && !orb.IsTakenByLocalPlayer && !orb.IsAI)
                    orb.ChangeToAI(game);
        }

        public void CheckNetworkSearch()
        {
            if (IsCreating || IsJoining || IsSearching)
            {
                bool TemparyProfiles = false;

                foreach (CompletePlayer player in game.ThisGamesPlayers)
                    if (player.InUse && !player.IsProfile)
                        TemparyProfiles = true;

                if (!TemparyProfiles)
                //if(true)
                {
                    if (IsSearching && FindResult.IsCompleted)
                    {

                        FindResult.AsyncWaitHandle.WaitOne();
                        try
                        {
                            availableSessions = NetworkSession.EndFind(FindResult);

                            if (availableSessions.Count == 0)
                            {
                                SetString("No Games Found, Creating Game...");

                                CreateGame();
                            }
                            else
                            {
                                SetString(availableSessions.Count.ToString() + " Games Found\nFinding Best Game");

                                SortSessions();
                            }

                            IsSearching = false;
                        }
                        catch (Exception e)
                        {
                            // if(networkSession==null)
                            if (e.Message != null)
                            {
                                OnlineString = " ";
                                game.ErrorMessage = e.Message.Replace(". ", ".\n");
                                game.menus.GoTo("Error", false);
                                IsCreating = false;
                                IsJoining = false;
                                IsSearching = false;
                            }
                        }



                        IsSearching = false;
                    }

                    if (IsJoining && JoinResult.IsCompleted)
                    {
                        JoinResult.AsyncWaitHandle.WaitOne();

                        try
                        {
                            networkSession = NetworkSession.EndJoin(JoinResult);

                            HookSessionEvents();

                            BeginOnlineGameAsGuest();
                        }
                        catch (Exception e)
                        {
                            // if (networkSession == null)
                            if (e.Message != null)
                            {
                                OnlineString = " ";
                                game.ErrorMessage = e.Message.Replace(". ", ".\n");
                                game.menus.GoTo("Error", false);
                                IsCreating = false;
                                IsJoining = false;
                                IsSearching = false;
                            }
                        }
                        IsJoining = false;


                    }

                    if (IsCreating && CreateResult.IsCompleted)
                    {
                        CreateResult.AsyncWaitHandle.WaitOne();
                        try
                        {
                            networkSession = NetworkSession.EndCreate(CreateResult);

                            HookSessionEvents();

                            BeginOnlineGameAsHost();
                        }
                        catch (Exception e)
                        {
                            if (e.Message != null)
                            {
                                OnlineString = " ";
                                game.ErrorMessage = e.Message.Replace(". ", ".\n");
                                game.menus.GoTo("Error", false);
                                IsCreating = false;
                                IsJoining = false;
                                IsSearching = false;
                            }
                        }

                        IsCreating = false;


                    }
                }
                else
                {
                    OnlineString = " ";
                    game.ErrorMessage = "No Temporary Profiles May Be Taken Online \n Sign into XBOX LIVE Guest Profiles Instead";
                    game.menus.GoTo("Error", false);
                    IsCreating = false;
                    IsJoining = false;
                    IsSearching = false;
                }
            }
        }


        public void Update()
        {
            if (networkSession != null)
            {
                if (game.menus.MenuCurrent.MyType != "InGame")
                    game.menus.GoTo("InGame", true);
                UpdateNetworkSession();
            }
            else
                CheckNetworkSearch();

            OnlineStringAlpha -= 0.005f;
        }


        public void UpdateNetworkSession()
        {
            foreach (NetworkGamer gamer in networkSession.LocalGamers)
            {
                if (gamer.Tag == null)
                    GamerGetTag(gamer);
            }

            HasUpdateLocal = false;

            ReturnOrbsToAI();

            if (networkSession != null)
            {
                if (!networkSession.IsHost)
                {
                    foreach (LocalNetworkGamer gamer in networkSession.LocalGamers)
                    {
                        if (gamer.Tag == null)
                            game.onlineHandler.GamerGetTag(gamer);

                        if (gamer.Tag != null)
                            UpdateLocalGamer(gamer);
                    }
                }

                if (networkSession != null)
                {
                    networkSession.Update();

                    if (networkSession != null)
                    {

                        foreach (LocalNetworkGamer gamer in networkSession.LocalGamers)
                        {
                            if (gamer.Tag == null)
                                game.onlineHandler.GamerGetTag(gamer);

                            if (gamer.IsHost)
                            {
                                ServerReadInputFromClients(gamer);
                            }
                            else if (!networkSession.IsHost)
                            {
                                HasUpdateLocal = true;

                                ClientReadGameStateFromServer(gamer);
                            }
                        }

                        if (networkSession.IsHost)
                        {
                            UpdateServer();
                        }
                    }
                }
            }

        }

        public void SetMap(int inmap)
        {
            if (inmap == -1)
            {
                game.GameOver = true;
                game.Winner = game.Orbs[0];
            }

            if (inmap == 0)
                game.map = Game1.Map.Botanical_Cave_Large;
            if (inmap == 1)
                game.map = Game1.Map.Botanical_Cave1V1;
            if (inmap == 2)
                game.map = Game1.Map.Desert_Base_Large;
            if (inmap == 3)
                game.map = Game1.Map.Desert_Base_Small;
            if (inmap == 4)
                game.map = Game1.Map.Ice_Mountain_Small;
            if (inmap == 5)
                game.map = Game1.Map.Ice_Mountain_Large;
            if (inmap == 6)
                game.map = Game1.Map.Train_Station_Huge;
            if (inmap == 7)
                game.map = Game1.Map.Train_Station_Medium;

        }

        public void SetGameMode(int inGameMode)
        {
            if (inGameMode == -1)
            {
                game.GameOver = true;
                game.Winner = game.Orbs[0];
            }

            if (inGameMode == 0)
                game.gamemode = Game1.GameMode.Assasin;
            if (inGameMode == 1)
                game.gamemode = Game1.GameMode.DeathMatch;
            if (inGameMode == 2)
                game.gamemode = Game1.GameMode.DownGrade;
            if (inGameMode == 3)
                game.gamemode = Game1.GameMode.KeepAway;
            if (inGameMode == 4)
                game.gamemode = Game1.GameMode.TeamDeathMatch;
            if (inGameMode == 5)
                game.gamemode = Game1.GameMode.WarLord;

        }

        public void ServerReadInputFromClients(LocalNetworkGamer gamer)
        {
            while (gamer.IsDataAvailable)
            {

                NetworkGamer sender;

                // Read a single packet from the network.
                gamer.ReceiveData(packetReader, out sender);

                if (!sender.IsLocal)
                {
                    // Look up the tank associated with whoever sent this packet.
                    BasicOrb orb = sender.Tag as BasicOrb;

                    Vector2 OrbPos = packetReader.ReadVector2();
                    orb.Position = new Vector3(OrbPos.X, orb.Position.Y, OrbPos.Y);

                    orb.Rotation = new Vector3(0, packetReader.ReadSingle(), 0);

                    orb.PrimaryWeaponQue = packetReader.ReadInt32();
                    orb.SecondaryWeaponQue = packetReader.ReadInt32();
                    orb.AbilityQue = packetReader.ReadInt32();
                    orb.GunCurrent = packetReader.ReadInt32();
                    orb.Abilty[0] = packetReader.ReadInt32();
                    orb.Team = packetReader.ReadInt32();
                    orb.Alpha = packetReader.ReadSingle();

                    orb.IsPhasing = packetReader.ReadBoolean();
                    orb.PhaseTimer = packetReader.ReadInt32();
                    orb.PhaseVelocity = packetReader.ReadVector3();
                    orb.MyController.MoveStickTrack = packetReader.ReadVector2();

                }
            }
        }


        public void UpdateLocalGamer(LocalNetworkGamer gamer)
        {
            PlayerIndex index = gamer.SignedInGamer.PlayerIndex;

            BasicOrb orb = gamer.Tag as BasicOrb;

            packetWriter.Write(new Vector2(orb.Position.X, orb.Position.Z));
            packetWriter.Write(orb.Rotation.Y);
            packetWriter.Write(orb.PrimaryWeaponQue);
            packetWriter.Write(orb.SecondaryWeaponQue);
            packetWriter.Write(orb.AbilityQue);
            packetWriter.Write(orb.GunCurrent);
            packetWriter.Write(orb.Abilty[0]);
            packetWriter.Write(orb.Team);
            packetWriter.Write(orb.Alpha);
            packetWriter.Write(orb.IsPhasing);
            packetWriter.Write(orb.PhaseTimer);
            packetWriter.Write(orb.PhaseVelocity);
            packetWriter.Write(orb.MyController.MoveStickTrack);

            gamer.SendData(packetWriter, SendDataOptions.InOrder, networkSession.Host);

            return;
        }



        public void ClientReadGameStateFromServer(LocalNetworkGamer gamer)
        {
            while (gamer.IsDataAvailable)
            {
                NetworkGamer sender;

                gamer.ReceiveData(packetReader, out sender);

                while (packetReader.Position < packetReader.Length)
                {
                    game.GameOver = packetReader.ReadBoolean();
                    int WinnerId = packetReader.ReadInt32();

                    if (game.GameOver)
                    {
                        foreach (BasicOrb orb in game.Orbs)
                            if (orb.relevent && orb.ID == WinnerId)
                                game.Winner = orb;
                    }


                    int GameMode = packetReader.ReadInt32();

                    SetGameMode(GameMode);

                    FutureGameMode = GameMode;


                    int Map = packetReader.ReadInt32();

                    SetMap(Map);

                    FutureMap = Map;


                    foreach (BasicOrb orb in game.Orbs)
                        orb.relevent = packetReader.ReadBoolean();

                    foreach (BasicOrb orb in game.Orbs)
                        if (orb.relevent)
                        {
                            byte gamerId = packetReader.ReadByte();

                            networkSession.FindGamerById(gamerId).Tag = orb;

                            orb.Read(packetReader);
                        }

                    if (game.gamemode == Game1.GameMode.KeepAway)
                    {
                        game.flag.IsCarried = packetReader.ReadBoolean();
                        game.flag.Position = packetReader.ReadVector3();

                        int CarrierID = packetReader.ReadInt32();

                        if (game.flag.IsCarried)
                        {
                            foreach (BasicOrb orb in game.Orbs)
                                if (orb.relevent && orb.ID == CarrierID)
                                    game.flag.carrier = orb;
                        }
                    }
                }
            }
        }


        public void UpdateServer()
        {
            Int32 GameMode = -1;
            Int32 Map = -1;

            packetWriter.Write(game.GameOver);
            packetWriter.Write((Int32)(game.Winner != null ? game.Winner.ID : 0));

            if (game.menus.IsInGame)
            {
                packetWriter.Write((Int32)game.gamemode);
                packetWriter.Write((Int32)game.map);
            }
            else
            {
                packetWriter.Write((Int32)(-1));
                packetWriter.Write((Int32)(-1));
            }

            packetWriter.Write(GameMode);
            packetWriter.Write(Map);

            foreach (BasicOrb orb in game.Orbs)
                packetWriter.Write((bool)orb.relevent);

            foreach (BasicOrb orb in game.Orbs)
            {
                if (orb.relevent)
                {
                    orb.Write(packetWriter);
                }
            }

            if (game.gamemode == Game1.GameMode.KeepAway)
            {
                packetWriter.Write(game.flag.IsCarried);
                packetWriter.Write(game.flag.Position);
                packetWriter.Write(game.flag.carrier.ID);
            }

            LocalNetworkGamer server = (LocalNetworkGamer)networkSession.Host;
            server.SendData(packetWriter, SendDataOptions.InOrder);
        }

        public void BeginOnlineGameAsGuest()
        {
            Begin();
            game.menus.GoTo("InGame", true);
        }

        public void BeginOnlineGameAsHost()
        {
            Begin();
            game.menus.GoTo("InGame", true);
            networkSession.AllowHostMigration = true;
            networkSession.AllowJoinInProgress = true;
            networkSession.StartGame();
        }

        public void Begin()
        {
            IsTryingToGoToGame = true;
            FutureGameMode = 1;
            FutureMap = 1;

            return;
        }

        public void GamerGetTag(NetworkGamer NewGamer)
        {
            foreach (BasicOrb orb in game.Orbs)
                orb.IsTakenByOnlinePlayer = false;

            foreach (NetworkGamer gamer in networkSession.AllGamers)
                if (gamer != NewGamer && gamer.Tag != null)
                {
                    BasicOrb orb = gamer.Tag as BasicOrb;
                    orb.IsTakenByOnlinePlayer = true;
                }

            foreach (BasicOrb orb in game.Orbs)
                if (!orb.IsTakenByOnlinePlayer)
                {
                    NewGamer.Tag = orb;
                    return;
                }

            return;
        }

        void GamerJoinedEventHandler(object sender, GamerJoinedEventArgs e)
        {
            int gamerIndex = networkSession.AllGamers.IndexOf(e.Gamer);
            GamerGetTag(e.Gamer);

            ReturnOrbsToAI();
            game.OrbsPlaying = Math.Max(game.OrbsPlaying, networkSession.AllGamers.Count);
        }

        void SessionEndedEventHandler(object sender, NetworkSessionEndedEventArgs e)
        {
            OnlineEventString = e.EndReason.ToString();
            networkSession.Dispose();
            networkSession = null;
            game.menus.GoTo("Score", true);
        }

        void HookSessionEvents()
        {
            networkSession.GamerJoined += GamerJoinedEventHandler;
            networkSession.SessionEnded += SessionEndedEventHandler;
        }
    }
}
