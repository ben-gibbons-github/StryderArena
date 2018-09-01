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
//using NormalMappingEffectPipeline;
using Microsoft.Xna.Framework.Storage;
//using Microsoft.Xna.Framework.Design;
//using Microsoft.Xna.Framework.Design;
//using Microsoft.Xna.Framework.Design;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Xml;
using Microsoft.Xna.Framework.Net;

namespace Orb
{
    public class LocalPlayer: game_objects.BasicController
    {
         #region Fields
        public Vector2 MoveStick = Vector2.Zero;
        public Vector2 XPos;
        public Vector2 LookStick = Vector2.Zero;
        public float LowVibrationAmount = 0;
        public float HighVibrationAmount = 0;
        public float VibrationMinus = 0;
        public Vector2 PermMult = Vector2.Zero;
        public float PrevLife = 0;
        public float ErrorTimer = 0;
        float WhiteMax = 0.33f;
        public float PrevEnergy = 0;
        public float XAlpha = 0;
        public List<LightObject> VisibleLights = new List<LightObject>();
        public float EnergyAlpha = 0;
        public float HealthAlpha = 0;
        public float MenuRot = 0;
        public bool HasSaidTied = true;
        public bool HasSaidLeading = false;
        public string AlphaText=" ";
        public float AlphaTextAlpha = 0;
        public bool HasSaidDashReady = false;
        public int ID = 0;
        public Vector3 CameraPosPreviousForVelocity = Vector3.Zero;
        
        public float RingAlpha=0;
        public bool HasLived = false;
        public Vector2 DPad = Vector2.Zero;
        public SoundManager soundManager;
        public Vector2 MoveStickPrev = Vector2.Zero;
       // public int TeamScreenWait = 60;
        public bool HasRingFlashed = false;
        public int RingSpin = 0;
        public float LookSensitivity = 1;
        public int TeamSelectX = 0;
        public float BuyWindowSize = 0;
        public float BuyWindowIconSize = 0;
        public float MoveTime = 0;
        public bool AButtonPrev = false;
        
        public int TeamSelectY = 0;

        public CompletePlayer MyPlayer;

        public float DamageAlpha=0;

        public float TeamSelectAlpha = 0;
        public bool TeamSelectIsOpen = false;

        public bool PauseWindowIsOpen = false;
        public float PauseWindowAlpha = 0;
        public int PauseWindowY = 0;
        public float[] PauseWindowIndividualAlpha = new float[4];

        public int BuyWindowX = 0;
        int BestKills = 0;
        int BestDeaths = 10000000;
        BasicOrb BestOrb;
        public float ScoreWindowAlpha;
        public bool TabIsPressed = false;
        public float[,] BuyWindowIndivualSize = new float[4,2];
        public bool BuyWindowIsSwitching = false;
        public bool BuyWindowIsOpen = false;
        public int BuyWindowStage = 0;
        public bool RightTrigger = false;
       
        public bool LeftTrigger = false;
        public Vector2 PortSize;
      //  BoundingFrustum Boundaries;
        public Vector2 OldMouse = new Vector2(0,0);
        public bool TryingToSave = false;
        public float AbilityAlpha = 0;
        public PlayerIndex Currentplayer = PlayerIndex.One;
        public bool Bbutton = false;
        public bool FoundSaveDevice = false;
        public bool RightBumper = false;
        public bool RightBumperPrevious = false;
        public GamePadState PadState = new GamePadState();
        public GamePadState OldPadState = new GamePadState();
        public bool LeftBumper = false;
        public bool LeftBumperPrevious = false;
        public bool AButton = false;
        public SaveLevel LevelSave;
        int MoveDelayTimer = 0;
        public bool PropertiesWindowIsOpen = false;
        public int PropertiesWindowSelect = 0;
        public int CubeFace = 0;
        public int MaxPropertiesWindowSize = 200;
        public int DoubleClickTime = 0;
        public Vector2 PropertiesWindowPos = new Vector2(60, 60);
        bool RightTriggerPrev;
        Rectangle Rect;

        public BoundingFrustum cameraFrustum = new BoundingFrustum(Matrix.Identity);
        
        Block Blockfound;
        
        SpecialObject SpecialFound;
        StaticLightObject LightFound;
        Floor Floorfound;

        public Vector3 CameraPos;
        public Vector3 CameraPosHalf;
        Vector3 CameraDesiredPos;
            KeyboardState KeyCheck;
            KeyboardState OldKeyCheck;
            MouseState MouseCheck;
            public Vector2 MouseChange = Vector2.Zero;

            public Matrix playerView, playerProjection;
            public bool SelectorWindowIsOpen = false;
            public Viewport PlayerPort;
            public Vector2 UpgradeViewPos = new Vector2(0,1);
            public float UpgradeViewNumb = 0;
            public string Mode = "Spectate";

            public enum PlayMode
            {
                  Spectate,
                Forge,
                Play
            }
            public PlayMode playmode = PlayMode.Play;

            public bool IsXboxController = true;
            public int PlayerTarget = 0;
            public bool Relevent = false;
            public Vector3 DummyPos = Vector3.Zero;
            Vector3 DummyRot = Vector3.Zero;
            Vector3 PlaceRot = Vector3.Zero;
            public Vector3 lookAt = Vector3.Zero;
            Vector3 BlockAdd = Vector3.Zero;
            Vector3 BlockPos = Vector3.Zero;
            public int Updateddeaths = 0;
            public int UpdatedKills = 0;
            Vector3 BlockScale = new Vector3(100, 100, 100);
            IAsyncResult result;
            public int TypeSelect = 4;
            public int[] NumberSelect = { 0, 0, 0,0 };
            public int[] Value={0,0,0,0,0};
            public int[] MaxValue = { 0, 0, 0, 0, 0 };
            public int[] MinValue = { 0, 0, 0, 0, 0 };
            public int value_numb = 0;
            public string[] Value_name = { "", "", "", "", "" };
            public string[] Value_Type = { "", "", "", "", "" };
            int TypeFound = 0;
            bool SwitchDraw = false;
            public int PropertiesWindowSize = 0;

            public int PlayerNumber = 0;
            public bool StartButtonPressed = false;
            public bool StartButtonPressedPrev = false;

        const int MaxMessages=10;
        public int maxMessages=MaxMessages;

        public int MessageNumb = 0;
        public float[] MessageAlpha = new float[MaxMessages];
        public int[] MessageType = new int[MaxMessages];
        public int[] MessageTarget = new int[MaxMessages];




        #endregion

         #region HUD Rectangles

            Rectangle HUDRECT;


         #endregion

            public void Load(Game1 game,int i,CompletePlayer MyNewPlayer)
            {
                
                soundManager = new SoundManager();

                ID = i;
                LevelSave = game.LevelSave;

               // if (i == 1)
                   // IsXboxController = false;
                //if (game.onlineHandler.networkSession == null)
                {
                    if (i == 1)
                        Currentplayer = PlayerIndex.One;
                    if (i == 2)
                        Currentplayer = PlayerIndex.Two;

                    if (i == 3)
                        Currentplayer = PlayerIndex.Three;
                    if (i == 4)
                        Currentplayer = PlayerIndex.Four;
                }
             //   else if(MyNewPlayer.MyGamer!=null)
                 //   Currentplayer = MyNewPlayer.MyGamer.PlayerIndex;

                Currentplayer = MyNewPlayer.MyIndex;

                if (i < game.LocalPlayerNumb + 1)
                    Relevent = true;
                else
                    Relevent = false;

                int XX = 0;
                int YY = 0;
                int Width2 = 0;
                int Height2 = 0;

                if(game.LocalPlayerNumb==1)
                {
                    Width2 = game.GraphicsDevice.Viewport.Width;
                    Height2 = game.GraphicsDevice.Viewport.Height;
                }
                if (game.LocalPlayerNumb == 2)
                {
                    Width2 =(int)((game.GraphicsDevice.Viewport.Width)*0.7f);
                    Height2 = game.GraphicsDevice.Viewport.Height/2;
                    if (i == 2)
                        YY = game.GraphicsDevice.Viewport.Height / 2;
                    XX = (int)(game.GraphicsDevice.Viewport.Width * (1-0.7f)/2);
                }
                if (game.LocalPlayerNumb == 3)
                {
                    if (i == 1)
                    {
                        Width2 = game.GraphicsDevice.Viewport.Width / 2;
                        XX = game.GraphicsDevice.Viewport.Width / 4;
                    }
                    else
                        Width2 = game.GraphicsDevice.Viewport.Width / 2;

                    Height2 = game.GraphicsDevice.Viewport.Height / 2;
                    if (i == 2||i==3)
                        YY = game.GraphicsDevice.Viewport.Height / 2;
                    if(i==3)
                        XX = game.GraphicsDevice.Viewport.Width / 2;
                }
                if (game.LocalPlayerNumb == 4)
                {
                    Width2 = game.GraphicsDevice.Viewport.Width / 2;
                    Height2 = game.GraphicsDevice.Viewport.Height / 2;

                    if (i == 2 ||i==4)
                        XX= game.GraphicsDevice.Viewport.Width / 2;
                    if (i == 3||i==4)
                        YY = game.GraphicsDevice.Viewport.Height / 2;
                }


                PlayerPort = new Viewport
                {

                    MinDepth = 0,
                    MaxDepth = 1,
                    X = XX,
                    Y = YY,
                    Height=Height2,
                    Width=Width2,
                };

                if (game.LocalPlayerNumb == 1)
                    SoundEffect.MasterVolume = 1*game.SoundEffectsVolume;
                if (game.LocalPlayerNumb == 2)
                    SoundEffect.MasterVolume = 0.75f * game.SoundEffectsVolume;
                if (game.LocalPlayerNumb == 3)
                    SoundEffect.MasterVolume = 0.6f * game.SoundEffectsVolume;
                if (game.LocalPlayerNumb == 4)
                    SoundEffect.MasterVolume = 0.45f * game.SoundEffectsVolume;

                PortSize = new Vector2(Width2, Height2);
                playerView = Matrix.CreateLookAt(new Vector3(100f, 1000f, 100f),new Vector3(0f, 0f, 0f),Vector3.Up);
                playerProjection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 1, 10f, 500000f);
     
                    PlayerTarget = i - 1;

                //Mode = "Play";
                playmode = PlayMode.Play;

                if (game.IsEditorMode)
                {
                    playmode = PlayMode.Forge;
                    IsXboxController = false;
                }

                PlayerNumber = i;
                Name = MyNewPlayer.MyName;
                LookSensitivity = 1;
                MyPlayer = MyNewPlayer;

            }

            public void ControlTeamWindow(Game1 game)
            {

                if (TeamSelectIsOpen)
                {
                    TeamSelectAlpha += 0.1f;

                    if (Vector2.Distance(MoveStickPrev, Vector2.Zero) < 0.1)
                    {

                       // game.menus.GoTo("error", false);

                        if (MoveStick.X > 0.1f)
                        {
                            game.soundHolder.soundEffects["menu_move"].Play(game.SoundEffectsVolume, 0, 0);
                            TeamSelectY -= 1;
                        }
                        if (MoveStick.X < -0.1f)
                        {
                            game.soundHolder.soundEffects["menu_move"].Play(game.SoundEffectsVolume, 0, 0);
                            TeamSelectY += 1;
                        }
                        if (MoveStick.Y > 0.1f)
                        {
                            game.soundHolder.soundEffects["menu_move"].Play(game.SoundEffectsVolume, 0, 0);
                            TeamSelectX = 1;
                        }
                        if (MoveStick.Y < -0.1f)
                        {
                            game.soundHolder.soundEffects["menu_move"].Play(game.SoundEffectsVolume, 0, 0);
                            TeamSelectX = 0;
                        }
                    }

                    if (game.gamemode != Game1.GameMode.TeamDeathMatch)
                        TeamSelectY = (int)MathHelper.Clamp(TeamSelectY, 0, 3);
                    else
                        TeamSelectY = (int)MathHelper.Clamp(TeamSelectY, 0, 1);

                    if (AButton && !AButtonPrev&&PauseWindowAlpha==0)
                    {

                        game.soundHolder.soundEffects["menu_select"].Play(game.SoundEffectsVolume, 0, 0);
                         BasicOrb myorb = game.Orbs[PlayerTarget];

                         if (game.gamemode == Game1.GameMode.TeamDeathMatch)
                         {


                             // game.NumberOnTeam[game.Orbs[PlayerTarget].Team] -= 1;
                             myorb.Team = TeamSelectY;
                             game.NumberOnTeam[myorb.Team] += 1;
                             myorb.BusySelectingTeams = false;


                             foreach (BasicOrb orb in game.Orbs)
                                 if (orb.relevent)
                                 {
                                     if (orb.IsAI)
                                     {
                                         int team = orb.Team;
                                         game.NumberOnTeam[orb.Team] -= 1;
                                         orb.AsignToNewTeam(game);
                                         if (team != orb.Team)
                                             orb.Die(game, true, true);
                                     }
                                     orb.GetColor(game);
                                 }
                             TeamSelectIsOpen = false;

                             myorb.DrawColor = new Vector3((float)Math.Max(colorVec.X, WhiteMax), (float)Math.Max(colorVec.Y, WhiteMax), (float)Math.Max(colorVec.Z, WhiteMax)) * 1.25f;

                         }
                         else
                         {
                             TeamSelectIsOpen = false;
                             myorb.BusySelectingTeams = false;
                             color=game.C_holder.colors[TeamSelectY+TeamSelectX*4];
                             colorVec = game.C_holder.colorVecs[TeamSelectY + TeamSelectX * 4];
                             myorb.DrawColor = new Vector3((float)Math.Max(colorVec.X, WhiteMax), (float)Math.Max(colorVec.Y, WhiteMax), (float)Math.Max(colorVec.Z, WhiteMax)) * 1.25f;

                         }
                    }
                }
                else
                    TeamSelectAlpha -= 0.1f;

                TeamSelectAlpha = MathHelper.Clamp(TeamSelectAlpha, 0, 1);
            }

            public void ControlBuyWindow(Game1 game)
            {
                XAlpha -= 0.05f;

                MoveTime+=1;
                if (BuyWindowSize==1&&!BuyWindowIsSwitching&&BuyWindowIconSize==1)
                {

                    ErrorTimer++;

                    if (MoveStick.X != 0)
                    {
                        MoveTime = 0;
                        if (MoveStick.X < -0.1f&&Vector2.Distance(MoveStickPrev,Vector2.Zero)<0.1)
                        {
                            game.soundHolder.soundEffects["menu_move"].Play(game.SoundEffectsVolume, 0, 0);

                            BuyWindowX += 1;
                            if (BuyWindowX > 3)
                                BuyWindowX = 0;
                        }
                        if (MoveStick.X > 0.1f && Vector2.Distance(MoveStickPrev, Vector2.Zero) < 0.1)
                        {
                            game.soundHolder.soundEffects["menu_move"].Play(game.SoundEffectsVolume, 0, 0);
                            BuyWindowX -= 1;
                            if (BuyWindowX < 0)
                                BuyWindowX = 3;
                        }
                    }

                    if (MoveStick.Y != 0)
                    {
                        MoveTime = 0;
                        if (MoveStick.Y > 0.1f && Vector2.Distance(MoveStickPrev, Vector2.Zero) < 0.1)
                        {
                            game.soundHolder.soundEffects["menu_move"].Play(game.SoundEffectsVolume, 0, 0);
                            BuyWindowStage += 1;
                            if (BuyWindowStage > 1)
                                BuyWindowStage = 1;
                        }
                        if (MoveStick.Y < -0.1f && Vector2.Distance(MoveStickPrev, Vector2.Zero) < 0.1)
                        {
                            game.soundHolder.soundEffects["menu_move"].Play(game.SoundEffectsVolume, 0, 0);
                            BuyWindowStage -= 1;
                            if (BuyWindowStage < 0)
                                BuyWindowStage = 0;
                        }
                    }

                    if (AButton&&!AButtonPrev)
                    {

                        if (Money >= 1)//||UnLocked[BuyWindowX,BuyWindowStage])
                        {
                            if (BuyWindowX == 0 || UnLocked[BuyWindowX - 1, BuyWindowStage])
                            {


                                if (!UnLocked[BuyWindowX, BuyWindowStage])
                                {
                                    game.soundHolder.soundEffects["menu_select"].Play(game.SoundEffectsVolume, 0, 0);
                                    Money -= 1;
                                    BuyWindowIsSwitching = true;
                                    MoveTime = -4;
                                    if (BuyWindowStage == 0)
                                    {
                                        if (!UnLocked[BuyWindowX, BuyWindowStage])
                                            GunUpgrades += 1;
                                        game.Orbs[PlayerTarget].GunCurrent = game.U_holder.UpgradeSet[BuyWindowX, BuyWindowStage];
                                    }
                                    if (BuyWindowStage == 1)
                                    {
                                        if (!UnLocked[BuyWindowX, BuyWindowStage])
                                            AbilityUpgrades += 1;
                                        game.Orbs[PlayerTarget].Abilty[0] = game.U_holder.UpgradeSet[BuyWindowX, BuyWindowStage];
                                    }
                                    UnLocked[BuyWindowX, BuyWindowStage] = true;
                                }
                                else
                                {
                                    if (ErrorTimer > 15)
                                    {
                                        ErrorTimer = 0;
                                        game.soundHolder.soundEffects["player_fail"].Play(game.SoundEffectsVolume, 0, 0);
                                        XAlpha = 1;
                                        XPos = new Vector2(BuyWindowX, BuyWindowStage);
                                    }
                                }
                                //Money -= game.U_holder.UpgradeCost[BuyWindowX, BuyWindowStage];


                                // BuyWindowIsSwitching = true;
                            }
                            else
                            {
                                if (ErrorTimer > 15)
                                {
                                    ErrorTimer = 0;
                                    game.soundHolder.soundEffects["player_fail"].Play(game.SoundEffectsVolume, 0, 0);
                                    XAlpha = 1;
                                    XPos = new Vector2(BuyWindowX, BuyWindowStage);
                                }
                            }
                        }
                    }
                   // if (Bbutton)
                      //  BuyWindowIsSwitching = true;
                }

            }

            public void ControlPauseWindow(Game1 game)
            {
                if (StartButtonPressed && !StartButtonPressedPrev)
                {
                    game.soundHolder.soundEffects["menu_select"].Play(game.SoundEffectsVolume, 0, 0);
                    PauseWindowY = 0;
                    PauseWindowIsOpen = !PauseWindowIsOpen;

                    if (TeamSelectIsOpen)
                        PauseWindowIsOpen = false;
                    if (BuyWindowIsOpen)
                        PauseWindowIsOpen = false;

                }

                //PauseWindowIsOpen = true;

                if (PauseWindowIsOpen)
                    PauseWindowAlpha += 0.1f;
                else
                    PauseWindowAlpha -= 0.1f;

                PauseWindowAlpha = MathHelper.Clamp(PauseWindowAlpha, 0, 1);

                if (PauseWindowAlpha > 0)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (PauseWindowY == i)
                            PauseWindowIndividualAlpha[i] += 0.1f;
                        else
                            PauseWindowIndividualAlpha[i] -= 0.1f;

                        PauseWindowIndividualAlpha[i] = MathHelper.Clamp(PauseWindowIndividualAlpha[i], 0, 1);
                    }

                }

                if (PauseWindowIsOpen)
                {
                    //PauseWindowY += 1;
                    if (Vector2.Distance(MoveStickPrev, Vector2.Zero) < 0.1)
                    {
                        if (MoveStick.Y > 0.1)
                        {
                            game.soundHolder.soundEffects["menu_move"].Play(game.SoundEffectsVolume, 0, 0);
                            PauseWindowY += 1;
                        }
                        if (MoveStick.Y < -0.1)
                        {
                            game.soundHolder.soundEffects["menu_move"].Play(game.SoundEffectsVolume, 0, 0);
                            PauseWindowY -= 1;
                        }
                        if (PauseWindowY == 2)
                        {
                            if (MoveStick.X > 0.1)
                                LookSensitivity -= 0.25f;
                            if (MoveStick.X < -0.1)
                                LookSensitivity += 0.25f;

                            LookSensitivity = MathHelper.Clamp(LookSensitivity, 0.5f, 1.5f);
                            MyPlayer.Sensitivity = LookSensitivity;
                        }

                    }
                    PauseWindowY = (int)MathHelper.Clamp(PauseWindowY, 0, 3);

                    if (AButton && !AButtonPrev)
                    {
                        game.soundHolder.soundEffects["menu_select"].Play(game.SoundEffectsVolume, 0, 0);

                        if (PauseWindowY == 0||PauseWindowY==2)
                            PauseWindowIsOpen = false;

                        if (PauseWindowY == 1&&!TeamSelectIsOpen)
                        {
                            if (game.gamemode == Game1.GameMode.TeamDeathMatch)
                            {
                                game.NumberOnTeam[game.Orbs[PlayerTarget].Team] -= 1;
                                game.Orbs[PlayerTarget].Team = 1 - game.Orbs[PlayerTarget].Team;
                                game.NumberOnTeam[game.Orbs[PlayerTarget].Team] += 1;
                                game.Orbs[PlayerTarget].Die(game, true, true);


                                foreach (BasicOrb orb in game.Orbs)
                                    if (orb.relevent)
                                    {
                                        if (orb.IsAI)
                                        {
                                            int team = orb.Team;
                                            game.NumberOnTeam[orb.Team] -= 1;
                                            orb.AsignToNewTeam(game);
                                            if (team != orb.Team)
                                                orb.Die(game, true, true);
                                        }
                                        orb.GetColor(game);
                                    }
                                PauseWindowIsOpen = false;
                            }
                            else
                            {
                                PauseWindowIsOpen = false;
                                TeamSelectIsOpen = true;
                            }
                        }
                        if (PauseWindowY == 3)
                            game.menus.GoTo("Score",true);
                            //game.menus.IsFadingOut = true;
                    }

                }

                }

            public void SetVibration(float Minus, float Low, float High)
            {

                VibrationMinus = Minus;
                LowVibrationAmount = Math.Max(Low, LowVibrationAmount);
                HighVibrationAmount = Math.Max(High, HighVibrationAmount);
            }

            public void SetVibration(float Minus, float Low, float High,Vector3 Position,float Distance)
            {
                if (false)
                {
                    float Mult = (Distance - Vector3.Distance(Position, soundManager.Listener.Position)) / Distance;

                    VibrationMinus = Minus * Mult + VibrationMinus * (1 - Mult);
                    LowVibrationAmount = Math.Max(Low * Mult + LowVibrationAmount * (1 - Mult), LowVibrationAmount);
                    HighVibrationAmount = Math.Max(High * Mult + HighVibrationAmount * (1 - Mult), HighVibrationAmount);
                }
            }


            public void Vibrate()
            {
                if (LowVibrationAmount > 0 || HighVibrationAmount > 0)
                {
                    GamePad.SetVibration(Currentplayer, MathHelper.Clamp(LowVibrationAmount, 0, 1), MathHelper.Clamp(HighVibrationAmount, 0, 1));
                }
                    LowVibrationAmount -= VibrationMinus;
                    HighVibrationAmount -= VibrationMinus;
                
            }

            public void Update(Game1 game,GameTime gametime)
            {
                
                //Kills++;

                //Vibrate();
                BasicOrb orb = game.Orbs[PlayerTarget];

                AlphaTextAlpha -= 0.0035f;
                HealthAlpha -= 0.005f;
                EnergyAlpha -= 0.005f;

                if (orb.life != PrevLife)
                {
                    HealthAlpha = 1;
                    PrevLife = orb.life;
                }
                if (orb.Energy != PrevEnergy)
                {
                    if (orb.Energy > PrevEnergy)
                        game.soundHolder.soundEffects["energy_sound"].Play(game.SoundEffectsVolume, 0, 0);
                    EnergyAlpha = 1;
                    PrevEnergy = orb.Energy;
                }

                soundManager.Update();

                

                if (orb.pushTime > 0)
                {
                    float val = (0.2f + orb.pushTime / 50 * 0.8f) * (1.5f - orb.life / orb.MaxLife);
                    if (DamageAlpha < val)
                        DamageAlpha = val;
                    else
                        DamageAlpha -= 0.02f;
                    DamageAlpha = MathHelper.Clamp(DamageAlpha, val, 2);
                    //DamageAlpha = 1;

                }
                else
                {
                    DamageAlpha -= 0.033f;
                    DamageAlpha = MathHelper.Clamp(DamageAlpha, 0, 2);
                }
                    ControlPauseWindow(game);
               if(!PauseWindowIsOpen)
                ControlTeamWindow(game);
                cameraFrustum.Matrix = playerView * playerProjection;

                if (playmode==PlayMode.Play)
                {
                    
                    if (game.gamemode == Game1.GameMode.Assasin||game.gamemode==Game1.GameMode.WarLord||game.gamemode==Game1.GameMode.DownGrade)
                    {
                        //if(BuyWindowIsOpen)
                        game.Orbs[PlayerTarget].ShouldRespawnYet = true;
                        BuyWindowIsOpen = false;
                        BuyWindowIsSwitching = true;
                        
                    }
                     

                    if (TabIsPressed&&!PauseWindowIsOpen||game.GameOver||!game.Orbs[PlayerTarget].Alive&&Deaths==0)
                    {
                        ScoreWindowAlpha += 0.1f;
                        if (ScoreWindowAlpha > 1)
                            ScoreWindowAlpha = 1;
                    }
                    else
                    {
                        ScoreWindowAlpha -= 0.1f;
                        if (ScoreWindowAlpha < 0)
                            ScoreWindowAlpha = 0;
                    }

                    if (BuyWindowIsOpen&&!game.GameOver)
                    {
                        if (PauseWindowAlpha==0)
                        ControlBuyWindow(game);
                        for (int b = 0; b < 2; b++)
                        for (int i = 0; i < 4; i++)
                        {
                            
                            if(i==BuyWindowX&&b==BuyWindowStage)
                            {
                            BuyWindowIndivualSize[i,b] += 0.05f;
                            if (BuyWindowIndivualSize[i, b] > 1)
                                BuyWindowIndivualSize[i, b] = 1;
                            }
                            else
                            {
                                BuyWindowIndivualSize[i, b] -= 0.05f;
                                if (BuyWindowIndivualSize[i, b] < 0.65f)
                                    BuyWindowIndivualSize[i, b] = 0.65f;
                            }
                        }

                            BuyWindowSize += 0.05f;
                        if (BuyWindowSize > 1)
                            BuyWindowSize = 1;
                        if (BuyWindowIsSwitching)
                        {
                            BuyWindowIconSize -= 0.05f;
                            if (BuyWindowIconSize < 0)
                            {
                                BuyWindowIconSize = 0;
                                BuyWindowIsSwitching = false;
                                if (BuyWindowStage > -1)
                                {
                                    BuyWindowIsOpen = false;
                                    game.Orbs[PlayerTarget].ShouldRespawnYet = true;
                                }
                                else
                                    BuyWindowStage += 1;

                            }
                        }
                        else
                        {
                            BuyWindowIconSize += 0.05f;
                            if (BuyWindowIconSize > 1)
                                BuyWindowIconSize = 1;
                        }

                    }
                    else
                    {
                        BuyWindowSize -= 0.05f;
                        if (BuyWindowSize < 0)
                        {
                            BuyWindowSize = 0;
                            BuyWindowIsOpen = false;
                        }
                    }
                    

                    game.Orbs[PlayerTarget].IsControlled = true;
                    game.Orbs[PlayerTarget].HumanController = this;
                    game.Orbs[PlayerTarget].ControllerIsHuman = true;

                    if (!IsXboxController)
                        ControlFromKeyboard(game);
                    else
                        ControlFromXbox(game);
                    if (!PauseWindowIsOpen&&!TeamSelectIsOpen&&!BuyWindowIsOpen&&!game.IsInMenu)
                    {
                        game.Orbs[PlayerTarget].MoveFromController(game, MoveStick, LookStick * LookSensitivity, MouseChange, AButton, AButtonPrev, gametime);
                        game.Orbs[PlayerTarget].HandleOtherInput(game, RightTrigger, LeftTrigger, Bbutton, RightBumper, LeftBumper, gametime);
                    }
                    Vector3 DesiredPositionOffset = new Vector3(500, 0, 0);
                    Matrix transform = Matrix.Identity;
                    transform.Forward = game.Orbs[PlayerTarget].Rotation;
                    transform.Up = Vector3.Up;
                    transform.Right = Vector3.Cross(Vector3.Up, game.Orbs[PlayerTarget].Rotation);
                    if (!game.IsInMenu)
                    {
                        float Mult = 750;
                        if (game.LocalPlayerNumb == 2)
                            Mult -= 100;
                            //Mult = 650;

                        float HeightMult = 1.3f;

                        CameraDesiredPos = game.Orbs[PlayerTarget].Position + new Vector3(0, 25, 0) + new Vector3(
        (float)Math.Cos(game.Orbs[PlayerTarget].Rotation.Y), HeightMult, (float)Math.Sin(game.Orbs[PlayerTarget].Rotation.Y)) * Mult;

                     
                        CameraPosHalf = game.Orbs[PlayerTarget].Position + new Vector3(0, 25, 0) + new Vector3(
        (float)Math.Cos(-game.Orbs[PlayerTarget].Rotation.Y * 0), HeightMult, (float)Math.Sin(-game.Orbs[PlayerTarget].Rotation.Y) * 0) * Mult;
                       


                        lookAt = game.Orbs[PlayerTarget].Position - new Vector3(
        (float)Math.Cos(game.Orbs[PlayerTarget].Rotation.Y), 1f, (float)Math.Sin(game.Orbs[PlayerTarget].Rotation.Y)) * 150f;

                    }
                    else
                    {
                        MenuRot+=MathHelper.ToRadians(0.1f);
                        CameraDesiredPos = game.AverageBlockPosition+ new Vector3(
        (float)Math.Cos(MenuRot), 0.5f, (float)Math.Sin(MenuRot)) * 1000;


                        lookAt = game.AverageBlockPosition;

                    }

                    lookAt.Y = 50;


                    //CameraPos += (CameraDesiredPos - CameraPos) * 1f;
                    CameraPos = CameraDesiredPos;
                
                }

                if (playmode==PlayMode.Forge)
                {

                    if (!IsXboxController)
                        ControlFromKeyboard(game);
                    else
                        ControlFromXbox(game);
                    if (!PropertiesWindowIsOpen)
                        MoveCamera(game, RightTrigger);
                    else
                        ControlPropertiesWindow(game);
                        

                    Vector3 DesiredPositionOffset = new Vector3(0, 1600, 1000);
                    Matrix transform = Matrix.Identity;
                    transform.Forward = DummyRot;
                    transform.Up = Vector3.Up;
                    transform.Right = Vector3.Cross(Vector3.Up,DummyRot);
                    lookAt = new Vector3( DummyPos.X,50,DummyPos.Z);
                    CameraDesiredPos = DummyPos+DesiredPositionOffset;

                    CameraPos += (CameraDesiredPos - CameraPos) * 1f;

                    if (TryingToSave)

                        SaveMap2(game);

                    if (TypeSelect == 2)
                    {
                        if (BlockScale.X > game.S_Holder.SpecialMaxSize[NumberSelect[TypeSelect - 1]].X)
                            BlockScale.X = game.S_Holder.SpecialMaxSize[NumberSelect[TypeSelect - 1]].X;
                        if (BlockScale.Z > game.S_Holder.SpecialMaxSize[NumberSelect[TypeSelect - 1]].Z)
                            BlockScale.Z = game.S_Holder.SpecialMaxSize[NumberSelect[TypeSelect - 1]].Z;
                    }
                }



                
                // Calculate desired camera properties in world space



                //CameraDesiredPos = game.Orbs[PlayerTarget].Position// +new Vector3(0, 500, 150)
                //    + Vector3.TransformNormal(DesiredPositionOffset, transform);

                

                soundManager.Listener.Position = (CameraPos+lookAt+lookAt)/3;
                soundManager.Listener.Forward = Vector3.Normalize(lookAt-CameraPos);
                soundManager.Listener.Up = Vector3.Up;
                soundManager.Listener.Velocity = CameraPos-CameraPosPreviousForVelocity;

                CameraPosPreviousForVelocity = CameraPos;

                playerView = Matrix.CreateLookAt(CameraPos, lookAt, Vector3.Up);
                playerProjection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, PlayerPort.AspectRatio, 10f, 50000f);

                }

            public void ResetKeys(Game1 game)
            {
                RightBumperPrevious = RightBumper;
                LeftBumperPrevious = LeftBumper;

                MoveStick = Vector2.Zero;
                LookStick = Vector2.Zero;
                DPad = Vector2.Zero;
                RightTriggerPrev = RightTrigger;
                RightTrigger = false;
                LeftTrigger = false;
                AButton = false;
                RightBumper = false;
                LeftBumper = false;
                Bbutton = false;
                
            }

            public void ControlFromXbox(Game1 game)
            {
                //if(false)
                {

                    ResetKeys(game);
                    OldPadState = PadState;

                    StartButtonPressedPrev = OldPadState.IsButtonDown(Buttons.Start);
                    MoveStickPrev = -OldPadState.ThumbSticks.Left;
                    AButtonPrev = OldPadState.IsButtonDown(Buttons.A);

                    int i = PlayerNumber;

                    if (game.onlineHandler.networkSession == null)
                    {
                        if (i == 1)
                            Currentplayer = PlayerIndex.One;
                        if (i == 2)
                            Currentplayer = PlayerIndex.Two;

                        if (i == 3)
                            Currentplayer = PlayerIndex.Three;
                        if (i == 4)
                            Currentplayer = PlayerIndex.Four;
                    }

                   // PadState = GamePad.GetState(Currentplayer);
                    PadState = GamePad.GetState(PlayerIndex.One);

                    TabIsPressed = false;
                    /*

                    if (KeyCheck.IsKeyDown(Keys.W))
                       // if(PadState.IsButtonUp(Buttons.))
                        MoveStick.Y = -1;
                    if (KeyCheck.IsKeyDown(Keys.S))
                        MoveStick.Y = 1;
                    if (KeyCheck.IsKeyDown(Keys.A))
                        MoveStick.X = 1;
                    if (KeyCheck.IsKeyDown(Keys.D))
                        MoveStick.X = -1;
                     * */
                    MoveStick = -PadState.ThumbSticks.Left;
                    LookStick = PadState.ThumbSticks.Right * 2;
                    MoveStickTrack = MoveStick;
                    LeftTrigger = PadState.IsButtonDown(Buttons.LeftTrigger);
                    StartButtonPressed = PadState.IsButtonDown(Buttons.Start);
                    Bbutton = PadState.IsButtonDown(Buttons.LeftTrigger);
                    AButton = PadState.IsButtonDown(Buttons.A);
                    //if (PadState.IsButtonDown(Buttons.RightShoulder))
                    //    AButton

                    RightTrigger = PadState.IsButtonDown(Buttons.RightTrigger);

                    RightBumper = PadState.IsButtonDown(Buttons.X);
                    //LeftBumper = PadState.IsButtonDown(Buttons.LeftShoulder);
                    if (PadState.IsButtonDown(Buttons.RightShoulder))
                        AButton = true;
                    if (PadState.IsButtonDown(Buttons.LeftShoulder))
                        RightBumper = true;

                    TabIsPressed = PadState.IsButtonDown(Buttons.Back);

                    if (KeyCheck.IsKeyDown(Keys.Left))
                        if (!OldKeyCheck.IsKeyDown(Keys.Left))
                            DPad.X = -1;
                    if (KeyCheck.IsKeyDown(Keys.Right))
                        if (!OldKeyCheck.IsKeyDown(Keys.Right))
                        {
                            DPad.X = 1;
                            CubeFace += 1;
                        }
                    if (KeyCheck.IsKeyDown(Keys.Down))
                        if (!OldKeyCheck.IsKeyDown(Keys.Down))
                        {
                            CubeFace -= 1;
                            DPad.Y = -1;
                        }
                    if (KeyCheck.IsKeyDown(Keys.Up))
                        if (!OldKeyCheck.IsKeyDown(Keys.Up))
                            DPad.Y = 1;

                    if (KeyCheck.IsKeyDown(Keys.Space))
                        if (!OldKeyCheck.IsKeyDown(Keys.Space))
                        {
                            if (playmode == PlayMode.Forge)
                                playmode = PlayMode.Play;
                            else
                                playmode = PlayMode.Forge;
                        }

                }
            }

            public void ControlFromKeyboard(Game1 game)
            {

                MoveStickPrev = MoveStick;

                ResetKeys(game);
                OldKeyCheck = KeyCheck;
                KeyCheck = Keyboard.GetState();
                MouseCheck = Mouse.GetState();

                MouseChange = new Vector2(MouseCheck.X - OldMouse.X, MouseCheck.Y - OldMouse.Y) / 2;
                
                OldMouse = new Vector2(MouseCheck.X, MouseCheck.Y);
                if (!PropertiesWindowIsOpen)
                    if (Math.Abs(MouseCheck.X + MouseCheck.Y - game.GraphicsDevice.Viewport.Width / 2 - game.GraphicsDevice.Viewport.Height / 2) > 50)
                    {
                        Mouse.SetPosition(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2);
                        OldMouse = new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2);
                    }

                StartButtonPressedPrev = StartButtonPressed;

                TabIsPressed = KeyCheck.IsKeyDown(Keys.Tab);
                StartButtonPressed = KeyCheck.IsKeyDown(Keys.Escape);

                if (KeyCheck.IsKeyDown(Keys.W))
                    MoveStick.Y = -1;
                if (KeyCheck.IsKeyDown(Keys.S))
                    MoveStick.Y = 1;
                if (KeyCheck.IsKeyDown(Keys.A))
                    MoveStick.X = 1;
                if (KeyCheck.IsKeyDown(Keys.D))
                    MoveStick.X = -1;
                if (KeyCheck.IsKeyDown(Keys.F))
                    Bbutton = true;
                if (KeyCheck.IsKeyDown(Keys.Left))
                    if(!OldKeyCheck.IsKeyDown(Keys.Left))
                    DPad.X = -1;
                if (KeyCheck.IsKeyDown(Keys.Right))
                    if (!OldKeyCheck.IsKeyDown(Keys.Right))
                    {
                        DPad.X = 1;
                        CubeFace += 1;
                    }
                if (KeyCheck.IsKeyDown(Keys.Down))
                    if (!OldKeyCheck.IsKeyDown(Keys.Down))
                    {
                        CubeFace -= 1;
                        DPad.Y = -1;
                    }
                        if (KeyCheck.IsKeyDown(Keys.Up))
                    if (!OldKeyCheck.IsKeyDown(Keys.Up))
                    DPad.Y = 1;
                if (KeyCheck.IsKeyDown(Keys.Space))
                    AButton = true;
                if (KeyCheck.IsKeyDown(Keys.E))
                   // if (!OldKeyCheck.IsKeyDown(Keys.E))
                        RightBumper = true;
                if (KeyCheck.IsKeyDown(Keys.Q))
                   // if (!OldKeyCheck.IsKeyDown(Keys.Q))
                        LeftBumper = true;
                if (KeyCheck.IsKeyDown(Keys.P))
                    if (!OldKeyCheck.IsKeyDown(Keys.P))
                    {
                        if (playmode == PlayMode.Forge)
                            playmode = PlayMode.Play;
                        else
                            playmode = PlayMode.Forge;
                    }
                if (MouseCheck.LeftButton==ButtonState.Pressed)
                    RightTrigger = true;
                if (MouseCheck.RightButton == ButtonState.Pressed)
                    LeftTrigger = true;

                if (KeyCheck.IsKeyDown(Keys.F))
                    TryingToSave = true;

                if (KeyCheck.IsKeyDown(Keys.Y))
                    game.TryingToLoad = true;

                //if (KeyCheck.IsKeyDown(Keys.I))
                //    game.Loader.OcclusionRefinement2.Parameters["simple"].SetValue(true);
                //if (KeyCheck.IsKeyDown(Keys.O))
                 //   game.Loader.OcclusionRefinement2.Parameters["simple"].SetValue(false);
            }

            public void MoveCamera(Game1 game,bool RightTrigger)
            {
                //DummyPos.X -= MoveStick.X * 10;
                //DummyPos.Z += MoveStick.Y * 10;
                PropertiesWindowSize -= 12;

                if (DoubleClickTime > 0)
                    DoubleClickTime += 1;
                if (LookStick != Vector2.Zero)
                    DoubleClickTime = 0;

                int MaxVal = 600;

                BlockAdd.X += MouseChange.X;
                BlockAdd.Z += MouseChange.Y;
                if (BlockAdd.X >   MaxVal)
                    DummyPos.X += 10;
                if (BlockAdd.X <  - MaxVal)
                    DummyPos.X -= 10;
                if (BlockAdd.Z >  + MaxVal)
                    DummyPos.Z += 10;
                if (BlockAdd.Z < - MaxVal)
                    DummyPos.Z -= 10;


                if (DPad.Y == 1)
                    TypeSelect += 1;
                if (DPad.Y == -1)
                    TypeSelect -= 1;
                if (TypeSelect < 1)
                    TypeSelect = 1;
                if (TypeSelect > 4)
                    TypeSelect = 4;
                if (DPad.Y != 0)
                {
                    
                    Switch(game);
                }



                    if (TypeSelect == 1||TypeSelect == 2)
                {


                    if (RightBumper&&!RightBumperPrevious)
                    {
                        NumberSelect[TypeSelect - 1] += 1;

                    }
                    if (LeftBumper && !LeftBumperPrevious)
                    {
                        NumberSelect[TypeSelect - 1] -= 1;

                    }
                    NumberSelect[TypeSelect - 1] = (int)MathHelper.Clamp(NumberSelect[TypeSelect - 1], 0, game.B_Holder.MaxBlockTypesP-1);

                    int TempVal = 0;
                    if (TypeSelect == 1)
                        TempVal = game.B_Holder.MaxBlockTypesP;
                    if (TypeSelect == 2)
                        TempVal = game.S_Holder.MaxSpecialsp;


                    if (NumberSelect[TypeSelect - 1] > TempVal)
                        NumberSelect[TypeSelect - 1] = 0;
                    if (NumberSelect[TypeSelect - 1] < 0)
                        NumberSelect[TypeSelect - 1] = TempVal;

                    if (RightBumper || LeftBumper)
                        Switch(game);
                }

                BlockPos = new Vector3((float)Math.Round((BlockAdd.X + DummyPos.X) / 100) * 100, 0, (float)Math.Round((BlockAdd.Z + DummyPos.Z) / 100) * 100);

                
                MoveDelayTimer += 1;
                if (MoveDelayTimer > 7)
                {
                    if (MoveStick != Vector2.Zero)
                    {
                        MoveDelayTimer = 0;
                        if (MoveStick.X == 1 || MoveStick.X == -1)
                            BlockScale.X -= MoveStick.X * 100;
                        if (MoveStick.Y == 1 || MoveStick.Y == -1)
                            BlockScale.Z -= MoveStick.Y * 100;
                    }
                }

                if (DPad.X != 0)
                {
                    if (SwitchDraw)
                        SwitchDraw = false;
                    else
                        SwitchDraw = true;

                    PlaceRot.Y += DPad.X * MathHelper.ToRadians(90);
                    float temp;
                    temp = BlockScale.X;
                    BlockScale.X = BlockScale.Z;
                    BlockScale.Z = temp;
                }

                if (BlockScale.X < 100)
                    BlockScale.X = 100;
                if (BlockScale.Z < 100)
                    BlockScale.Z = 100;

                if ((float)Math.Floor(BlockScale.X / 200) - BlockScale.X / 200 == 0)
                    BlockPos.X += 50;
                if ((float)Math.Floor(BlockScale.Z / 200) - BlockScale.Z / 200 == 0)
                    BlockPos.Z += 50;

                if (RightTrigger||LeftTrigger)
                {
                   
                    bool PlaceFree = true;

                    if (TypeSelect == 1 || TypeSelect == 2 ||TypeSelect==4)
                    {
                        foreach (Block block in game.Blocks)
                            if (PlaceFree)
                                if (block.Relevent)
                                    if (block.alive)
                                        if (BlockPos.X + BlockScale.X * 0.99f / 2 + block.Size.X * 0.99f / 2 > block.Position.X && BlockPos.X - BlockScale.X * 0.99f / 2 - block.Size.X * 0.99f / 2 < block.Position.X)
                                            if (BlockPos.Z + BlockScale.Z * 0.99f / 2 + block.Size.Y * 0.99f / 2 > block.Position.Z && BlockPos.Z - BlockScale.Z * 0.99f / 2 - block.Size.Y * 0.99f / 2 < block.Position.Z)
                                                if (block.Position == BlockPos||LeftTrigger)
                                            {
                                                PlaceFree = false;
                                                Blockfound = block;
                                                TypeFound = 0;
                                            }
                        if (TypeSelect != 1)
                        {
                            foreach (SpecialObject block in game.Specials)
                                if (PlaceFree)
                                    if (block.Relevent)
                                        if (BlockPos.X + BlockScale.X * 0.99f / 2 + block.Size.X * 0.99f / 2 > block.Position.X && BlockPos.X - BlockScale.X * 0.99f / 2 - block.Size.X * 0.99f / 2 < block.Position.X)
                                            if (BlockPos.Z + BlockScale.Z * 0.99f / 2 + block.Size.Y * 0.99f / 2 > block.Position.Z && BlockPos.Z - BlockScale.Z * 0.99f / 2 - block.Size.Y * 0.99f / 2 < block.Position.Z)
                                            {
                                                PlaceFree = false;
                                                SpecialFound = block;
                                                TypeFound = 1;
                                            }
                            foreach (StaticLightObject block in game.StaticLights)
                                if (PlaceFree)
                                    if (block.Relevent)
                                        if (BlockPos.X + BlockScale.X * 0.99f / 2 + block.Size.X * 0.99f / 2 > block.Position.X && BlockPos.X - BlockScale.X * 0.99f / 2 - block.Size.X * 0.99f / 2 < block.Position.X)
                                            if (BlockPos.Z + BlockScale.Z * 0.99f / 2 + block.Size.Y * 0.99f / 2 > block.Position.Z && BlockPos.Z - BlockScale.Z * 0.99f / 2 - block.Size.Y * 0.99f / 2 < block.Position.Z)
                                            {
                                                PlaceFree = false;
                                                LightFound = block;
                                                TypeFound = 3;
                                            }
                        }
                    }
                    else
                    {
                        foreach (Floor block in game.Floors)
                            if (PlaceFree)
                                if (block.Relevent)
                                        if (BlockPos.X + BlockScale.X * 0.99f / 2 + block.Size.X * 0.99f / 2 > block.Position.X && BlockPos.X - BlockScale.X * 0.99f / 2 - block.Size.X * 0.99f / 2 < block.Position.X)
                                            if (BlockPos.Z + BlockScale.Z * 0.99f / 2 + block.Size.Y * 0.99f / 2 > block.Position.Z && BlockPos.Z - BlockScale.Z * 0.99f / 2 - block.Size.Y * 0.99f / 2 < block.Position.Z)
                                            {
                                                PlaceFree = false;
                                                Floorfound = block;
                                                TypeFound = 2;
                                            }

                    }


                    if (PlaceFree)
                    {
                        if (RightTrigger)
                        {
                            bool FoundBlock = false;

                            if (TypeSelect == 1)
                                foreach (Block block in game.Blocks)
                                    if (!FoundBlock)
                                        if (!block.Relevent)
                                        {
                                            block.Rotation = PlaceRot;
                                            block.SwitchDraw = SwitchDraw;
                                            block.Relevent = true;
                                            block.Position = BlockPos;
                                            FoundBlock = true;
                                            block.Size.X = BlockScale.X;
                                            block.Size.Y = BlockScale.Z;
                                            block.Type = NumberSelect[TypeSelect - 1];
                                            block.MaxRespawnTime = Value[3];

                                            if (Value[0] == 1)
                                                block.Destructable = true;
                                            else
                                                block.Destructable = false;
                                            block.MaxLife = Value[1];
                                            if (Value[2] == 1)
                                                block.PhaseBlock = true;
                                            else
                                                block.PhaseBlock = false;
                                            block.Create(game);

                                        }

                            if (TypeSelect == 2)
                                foreach (SpecialObject block in game.Specials)
                                    if (!FoundBlock)
                                        if (!block.Relevent)
                                        {
                                            block.Rotation = PlaceRot;
                                            block.SwitchDraw = SwitchDraw;
                                            block.Relevent = true;
                                            block.Size.X = BlockScale.X;
                                            block.Size.Y = BlockScale.Z;
                                            block.Position = BlockPos;
                                            FoundBlock = true;
                                            block.Type = NumberSelect[1];
                                            

                                            for (int i = 0; i < game.S_Holder.SpecialControlsNumber[NumberSelect[1]]; i++)
                                            {
                                                block.Value[i] = Value[i];

                                            }
                                            block.Create(game);
                                        }
                            if (TypeSelect == 3)
                                foreach (Floor block in game.Floors)
                                    if (!FoundBlock)
                                        if (!block.Relevent)
                                        {
                                            block.Rotation = PlaceRot;
                                            block.SwitchDraw = SwitchDraw;
                                            block.Relevent = true;
                                            block.Size.X = BlockScale.X;
                                            block.Size.Y = BlockScale.Z;
                                            block.Position = new Vector3(BlockPos.X, -50, BlockPos.Z);
                                            FoundBlock = true;
                                            block.Type = NumberSelect[2];
                                            block.Create(game);


                                        }
                            if (TypeSelect == 4)
                                foreach (StaticLightObject block in game.StaticLights)
                                    if (!FoundBlock)
                                        if (!block.Relevent)
                                        {
                                            block.Relevent = true;
                                            block.Size.X = BlockScale.X;
                                            block.Size.Y = BlockScale.Z;
                                            block.Position = new Vector3(BlockPos.X, 0, BlockPos.Z);
                                            FoundBlock = true;
                                            block.Type = NumberSelect[3];
                                            block.Color.X = (float)(Value[0]) / 100;
                                            block.Color.Y = (float)(Value[1]) / 100;
                                            block.Color.Z = (float)(Value[2]) / 100;
                                            block.Distancee = Value[3];
                                            block.Position.Y = (float)Value[4];
                                            block.Create(game);
                                            block.RecalculateLights(game);

                                        }

                        }
                    }
                    else
                    {
                        if (LeftTrigger)
                        {
                            if (TypeFound == 0)
                            {
                                Blockfound.Relevent = false;
                                foreach (StaticLightObject light in game.StaticLights)
                                    //if (light.Relevent)
                                        light.DrawShadowMap(game);
                            }
                            if (TypeFound == 1)
                            {
                                SpecialFound.Destroy(game);
                                SpecialFound.Relevent = false;

                            } if (TypeFound == 2)
                                Floorfound.Relevent = false;
                            if (TypeFound == 3)
                            {
                                LightFound.Relevent = false;
                                LightFound.RecalculateLights(game);
                            }
                        }
                        if(RightTrigger)
                        {
                            if (DoubleClickTime > 3 && DoubleClickTime < 21 && !RightTriggerPrev)
                            {
                                

                                PropertiesWindowSize = 0;
                                PropertiesWindowIsOpen = true;
                                DoubleClickTime = 0;
                                SwitchTO(game);
                            }
                            else
                                DoubleClickTime = 1;
                        }
                    }
           
                }
                
            }

            public void ControlPropertiesWindow(Game1 game)
            {
                PropertiesWindowClose(game);
                    
                PropertiesWindowSize += 12;
                if (PropertiesWindowSize > MaxPropertiesWindowSize)
                    PropertiesWindowSize = MaxPropertiesWindowSize;


                if (RightTrigger)
                {
                    for (int i = 0; i < value_numb; i++)
                    {

                        if (MouseCheck.X > (int)PropertiesWindowPos.X + (int)(PropertiesWindowSize * 0.8) &&
                            MouseCheck.X < (int)PropertiesWindowPos.X + (int)(PropertiesWindowSize * 0.8) + PropertiesWindowSize &&
                            MouseCheck.Y > (int)PropertiesWindowPos.Y + PropertiesWindowSize / 4 + PropertiesWindowSize / 4 * i - 24 &&
                            MouseCheck.Y < (int)PropertiesWindowPos.Y + PropertiesWindowSize / 4 + PropertiesWindowSize / 4 * i + 24)
                            Value[i] = (int)Math.Round((((float)(MouseCheck.X - (int)(PropertiesWindowPos.X + (PropertiesWindowSize * 0.8))) * (MaxValue[i] - MinValue[i])) / PropertiesWindowSize))+MinValue[i];
                            
                            //Value[i] = ((MouseCheck.X - ((int)PropertiesWindowPos.X + (int)(PropertiesWindowSize * 0.8f))) / PropertiesWindowSize * (MaxValue[i] - MinValue[i])) + MinValue[i];
                        if(!RightTriggerPrev)
                        {
                            Rect.X=(int)PropertiesWindowPos.X;
                            Rect.Y=(int)PropertiesWindowPos.Y;

                            Rect.Width=(int)MaxPropertiesWindowSize*2;
                            Rect.Height=(int)MaxPropertiesWindowSize*2;
                                    
                            if(!Rect.Contains(MouseCheck.X,MouseCheck.Y))
                            {
                                PropertiesWindowIsOpen=false;
                                RightTrigger = false;
                                Mouse.SetPosition(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2);
                                PropertiesWindowClose(game);

                                
                            }
                            }

                    }
                }

            }

            public void PropertiesWindowClose(Game1 game)
            {
                if (TypeFound == 0)
                {
                    if (Value[0] == 1)
                        Blockfound.Destructable = true;
                    else
                        Blockfound.Destructable = false;
                    Blockfound.MaxLife = Value[1];
                    if (Value[2] == 1)
                        Blockfound.PhaseBlock = true;
                    else
                        Blockfound.PhaseBlock = false;
                    Blockfound.MaxRespawnTime = Value[3];
                    Blockfound.Create(game);
                }
                if (TypeFound == 1)
                {
                    for (int i = 0; i < game.S_Holder.SpecialControlsNumber[SpecialFound.Type]; i++)
                    {
                        SpecialFound.Value[i] = Value[i];
                        Value_name[i] = game.S_Holder.ControlName[SpecialFound.Type, i];
                        MinValue[i] = game.S_Holder.ControlMin[SpecialFound.Type, i];
                        MaxValue[i] = game.S_Holder.ControlMax[SpecialFound.Type, i];
                        
                    }
                    SpecialFound.Create(game);
                }
                if (TypeFound == 3)
                {
                    LightFound.Color.X = (float)(Value[0])/100;
                    LightFound.Color.Y = (float)(Value[1]) / 100;
                    LightFound.Color.Z = (float)(Value[2]) / 100;
                    LightFound.Distancee = Value[3];
                    LightFound.Position.Y = (float)Value[4];
                    LightFound.RecalculateLights(game);
                    LightFound.Create(game);
                }
            }

            public void Draw(Game1 game,Model mod, LocalPlayer player)
            {
                


                foreach (ModelMesh mesh in mod.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        //effect.World = boneTransforms[mesh.ParentBone.Index];

                        effect.View = player.playerView;
                        effect.Projection = player.playerProjection;
                        effect.EnableDefaultLighting();
                        effect.PreferPerPixelLighting = false;
                        if(TypeSelect==1)
                        effect.World = Matrix.CreateScale(BlockScale * new Vector3(0.0105f, 0.0105f * game.B_Holder.BlockSize[NumberSelect[TypeSelect - 1]].Y/100, 0.0105f)) *
                    Matrix.CreateTranslation(BlockPos + new Vector3(0, game.B_Holder.BlockSize[NumberSelect[TypeSelect - 1]].Y/2-50, 0));

                        if (TypeSelect == 2)
                            effect.World = Matrix.CreateScale(BlockScale * new Vector3(0.0105f, 0.0105f * game.S_Holder.SpecialSize[NumberSelect[TypeSelect - 1]].Y / 100, 0.0105f)) *
                        Matrix.CreateTranslation(BlockPos + new Vector3(0, game.S_Holder.SpecialSize[NumberSelect[TypeSelect - 1]].Y / 2 - 50, 0));

                        if(TypeSelect==3)
                            effect.World = Matrix.CreateScale(new Vector3( BlockScale.X,1,BlockScale.Z) * new Vector3(0.0105f, 1, 0.0105f)) *
Matrix.CreateTranslation(BlockPos + new Vector3(0, - 70, 0));

                        if (TypeSelect == 4)
                            effect.World = Matrix.CreateScale(new Vector3(1, 1, 1) ) *
Matrix.CreateTranslation(BlockPos);

                    }

                    mesh.Draw();
                }


                
                DrawPropertiesWindow(game);


            }

            public void DrawPropertiesWindow(Game1 game)
            {
               


                if (PropertiesWindowIsOpen)
                {

                    


                    Rect.X = (int)PropertiesWindowPos.X;
                    Rect.Y = (int)PropertiesWindowPos.Y;
                    Rect.Width = PropertiesWindowSize * 2;
                    Rect.Height = PropertiesWindowSize * 2;
                    game.spriteBatch.Draw(game.Loader.GuiBoxSprite, Rect, Color.White);
                    //int i = 1;
                    //value_numb = 1;
                    for (int i = 0; i < value_numb; i++)
                    {
                        Rect.X = (int)PropertiesWindowPos.X+(int)(PropertiesWindowSize*0.8);
                        Rect.Y = (int)PropertiesWindowPos.Y + PropertiesWindowSize / 4 + PropertiesWindowSize/4 * i-3;
                        Rect.Width = PropertiesWindowSize;
                        Rect.Height = 6 * PropertiesWindowSize / MaxPropertiesWindowSize;
                        game.spriteBatch.Draw(game.Loader.GuiBoxSprite2, Rect, Color.White);

                        Rect.X = (int)PropertiesWindowPos.X + (int)(PropertiesWindowSize * 0.8)+PropertiesWindowSize*(Value[i]-MinValue[i])/(MaxValue[i]-MinValue[i]);
                        Rect.Y = (int)PropertiesWindowPos.Y + PropertiesWindowSize / 4 + PropertiesWindowSize / 4 * i-20;
                        Rect.Width = 8 * PropertiesWindowSize / MaxPropertiesWindowSize;
                        Rect.Height = 40 * PropertiesWindowSize / MaxPropertiesWindowSize;
                        game.spriteBatch.Draw(game.Loader.GuiBoxSprite2, Rect, Color.White);

                        if (Value_Type[i] == "Bool")
                        {
                            if(Value[i]==1)
                            game.spriteBatch.DrawString(game.Loader.font, Value_name[i] + " yes" , new Vector2(PropertiesWindowPos.X + 24, PropertiesWindowPos.Y + PropertiesWindowSize / 4 + PropertiesWindowSize / 4 * i - 20), Color.White);
                            else
                            game.spriteBatch.DrawString(game.Loader.font, Value_name[i] + " no", new Vector2(PropertiesWindowPos.X + 24, PropertiesWindowPos.Y + PropertiesWindowSize / 4 + PropertiesWindowSize / 4 * i - 20), Color.White);
                           
                        }
                        else
                            game.spriteBatch.DrawString(game.Loader.font, Value_name[i] + " " + Value[i].ToString(), new Vector2(PropertiesWindowPos.X + 24, PropertiesWindowPos.Y + PropertiesWindowSize / 4 + PropertiesWindowSize / 4 * i - 20), Color.White);
                       
                    }

                    Rect.X = (int)MouseCheck.X-8;
                    Rect.Y = (int)MouseCheck.Y-8;
                    Rect.Width = 16;
                    Rect.Height = 16;
                    game.spriteBatch.Draw(game.Loader.Mouse, Rect, Color.White);
                }


    
            }

            public void DrawTestValue(Game1 game)
            {
              //  game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

                
                //game.spriteBatch.DrawString(game.Loader.font, ((int)game.Orbs[PlayerTarget].life + (int)game.Orbs[PlayerTarget].DamageResistance + (int)game.Orbs[PlayerTarget].DamageResistance2).ToString(), new Vector2(40, 40), Color.White);
                //game.spriteBatch.DrawString(game.Loader.font, Money.ToString(), new Vector2(40, 40), Color.White);
               // game.spriteBatch.DrawString(game.Loader.font, game.Content.ToString(), new Vector2(40, 40), Color.White);
                        


              //  game.spriteBatch.End();
            }

            public void MapToData()
            {
                using (BinaryWriter binarywriter = new BinaryWriter(File.Create("Output.lvl")))
                {
                    binarywriter.Write((Int32)LevelSave.BlockNumber);

                    for (int i = 0; i < LevelSave.BlockNumber; i++)
                    {
                        binarywriter.Write(LevelSave.BlockRelevent[i]);
                        binarywriter.Write(LevelSave.BlockRespawnTime[i]);
                        binarywriter.Write(LevelSave.BlockType[i]);

                        Vector3 Vec = LevelSave.BlockPos[i];
                        binarywriter.Write(Vec.X);
                        binarywriter.Write(Vec.Y);
                        binarywriter.Write(Vec.Z);

                        Vec = LevelSave.BlockRotation[i];
                        binarywriter.Write(Vec.X);
                        binarywriter.Write(Vec.Y);
                        binarywriter.Write(Vec.Z);


                        Vector2 Vec2 = LevelSave.BlockSize[i];
                        binarywriter.Write(Vec2.X);
                        binarywriter.Write(Vec2.Y);

                        binarywriter.Write(LevelSave.BlockDestructible[i]);
                        binarywriter.Write(LevelSave.BlockSwitchDraw[i]);
                        binarywriter.Write(LevelSave.BlockPhaseBlock[i]);
                        binarywriter.Write(LevelSave.MaxLife[i]);

                    }

                    binarywriter.Write((Int32)LevelSave.SpecialNumber);

                    for (int i = 0; i < LevelSave.SpecialNumber; i++)
                    {


                        Vector3 Vec = LevelSave.SpecialPos[i];
                        binarywriter.Write(Vec.X);
                        binarywriter.Write(Vec.Y);
                        binarywriter.Write(Vec.Z);

                        binarywriter.Write(LevelSave.SpecialRelevent[i]);
                        binarywriter.Write(LevelSave.SpecialType[i]);
                        binarywriter.Write(LevelSave.SpecialValue[i]);
                        binarywriter.Write(LevelSave.SpecialSwitchDraw[i]);

                        Vec = LevelSave.SpecialRotation[i];
                        binarywriter.Write(Vec.X);
                        binarywriter.Write(Vec.Y);
                        binarywriter.Write(Vec.Z);

                        Vector2 Vec2 = LevelSave.SpecialSize[i];
                        binarywriter.Write(Vec2.X);
                        binarywriter.Write(Vec2.Y);
                    }

                    binarywriter.Write((Int32)LevelSave.FloorNumber);

                    for (int i = 0; i < LevelSave.FloorNumber; i++)
                    {

                        Vector3 Vec = LevelSave.FloorPos[i];
                        binarywriter.Write(Vec.X);
                        binarywriter.Write(Vec.Y);
                        binarywriter.Write(Vec.Z);

                        binarywriter.Write(LevelSave.FloorRelevent[i]);
                        binarywriter.Write(LevelSave.FloorType[i]);
                        binarywriter.Write(LevelSave.FloorSwitchDraw[i]);

                        Vec = LevelSave.FloorRotation[i];
                        binarywriter.Write(Vec.X);
                        binarywriter.Write(Vec.Y);
                        binarywriter.Write(Vec.Z);

                        Vector2 Vec2 = LevelSave.FloorSize[i];
                        binarywriter.Write(Vec2.X);
                        binarywriter.Write(Vec2.Y);


                    }



                    binarywriter.Write((Int32)LevelSave.LightNumber);

                    for (int i = 0; i < LevelSave.LightNumber; i++)
                    {

                        binarywriter.Write(LevelSave.LightRelevent[i]);
                        binarywriter.Write(LevelSave.LightType[i]);


                        Vector3 Vec = LevelSave.LightPos[i];
                        binarywriter.Write(Vec.X);
                        binarywriter.Write(Vec.Y);
                        binarywriter.Write(Vec.Z);



                        Vec = LevelSave.LightColor[i];
                        binarywriter.Write(Vec.X);
                        binarywriter.Write(Vec.Y);
                        binarywriter.Write(Vec.Z);

                        binarywriter.Write(LevelSave.LightDistance[i]);

                    }

                }

            }


            public void SaveMap(Game1 game)
            {
              //  if (!FoundSaveDevice)
                {
                    AlignMap(game);
                    MapToData();
                   // FoundSaveDevice = true;
                   // result = game.Device.BeginOpenContainer("StorageDemo", null, null);

                    
                }
                if(false)
                if (result.IsCompleted)
                {
                    result.AsyncWaitHandle.WaitOne();
                    StorageContainer container = game.Device.EndOpenContainer(result);

                    Stream stream = container.CreateFile("TempFileName.txt");


                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(SaveLevel));

                    serializer.Serialize(stream, LevelSave);

                    stream.Close();

                    container.Dispose();

                    TryingToSave = false;
                    FoundSaveDevice = false;
                }
            }

            public void SaveMap2(Game1 game)
            {

                    SaveMap(game);
            }

            public void AlignMap(Game1 game)
            {
                LevelSave.BlockNumber=-1;
                foreach (Block block in game.Blocks)
                {
                    //if (block.Relevent)
                    {
                        LevelSave.BlockNumber += 1;
                        LevelSave.BlockDestructible[LevelSave.BlockNumber] = block.Destructable;
                        LevelSave.BlockPos[LevelSave.BlockNumber] = block.Position;
                        LevelSave.BlockSize[LevelSave.BlockNumber] = block.Size;
                        LevelSave.BlockRotation[LevelSave.BlockNumber] = block.Rotation;
                        LevelSave.BlockSwitchDraw[LevelSave.BlockNumber] = block.SwitchDraw;
                        LevelSave.MaxLife[LevelSave.BlockNumber] = (int)block.MaxLife;
                        LevelSave.BlockRelevent[LevelSave.BlockNumber] = block.Relevent;
                        LevelSave.BlockType[LevelSave.BlockNumber] = block.Type;
                        LevelSave.BlockPhaseBlock[LevelSave.BlockNumber] = block.PhaseBlock;
                        LevelSave.BlockRespawnTime[LevelSave.BlockNumber] = block.MaxRespawnTime;
                    }

                }
                

                LevelSave.SpecialNumber = -1;
                foreach (SpecialObject special in game.Specials)
                {
                    //if (special.Relevent)
                    {
                        LevelSave.SpecialNumber += 1;
                        LevelSave.SpecialPos[LevelSave.SpecialNumber] = special.Position;
                        LevelSave.SpecialType[LevelSave.SpecialNumber] = special.Type;
                        LevelSave.SpecialRelevent[LevelSave.SpecialNumber] = special.Relevent;
                        LevelSave.SpecialRotation[LevelSave.SpecialNumber] = special.Rotation;
                        LevelSave.SpecialSwitchDraw[LevelSave.SpecialNumber] = special.SwitchDraw;
                        LevelSave.SpecialSize[LevelSave.SpecialNumber] = special.Size;

                        for (int i = 0; i < game.S_Holder.SpecialControlsNumber[special.Type]; i++)
                        {
                            LevelSave.SpecialValue[LevelSave.SpecialNumber * 5 + i] = special.Value[i];

                        }
                    }
                   
                }

                LevelSave.FloorNumber = -1;
                foreach (Floor floor in game.Floors)
                {
                    //if (floor.Relevent)
                    {
                        LevelSave.FloorNumber += 1;
                        LevelSave.FloorPos[LevelSave.FloorNumber] = floor.Position;
                        LevelSave.FloorType[LevelSave.FloorNumber] = floor.Type;
                        LevelSave.FloorSize[LevelSave.FloorNumber] = floor.Size;
                        LevelSave.FloorSwitchDraw[LevelSave.FloorNumber] = floor.SwitchDraw;
                        LevelSave.FloorRotation[LevelSave.FloorNumber] = floor.Rotation;
                        LevelSave.FloorRelevent[LevelSave.FloorNumber] = floor.Relevent;
                    }

                }
                LevelSave.LightNumber = -1;
                foreach (StaticLightObject light in game.StaticLights)
                {

                    if (!light.LightIsBitch)
                    {
                        LevelSave.LightNumber += 1;
                        LevelSave.LightPos[LevelSave.LightNumber] = light.Position;
                        LevelSave.LightType[LevelSave.LightNumber] = light.Type;

                        LevelSave.LightRelevent[LevelSave.LightNumber] = light.Relevent;


                        LevelSave.LightDistance[LevelSave.LightNumber] = light.Distancee;
                        LevelSave.LightColor[LevelSave.LightNumber] = light.Color;
                    }
                    else
                    {
                        LevelSave.LightNumber += 1;
                        LevelSave.LightRelevent[LevelSave.LightNumber] = false;
                    }
                }
                LevelSave.BlockNumber = LevelSave.maxBlocks-1;
                LevelSave.SpecialNumber = LevelSave.maxSpecials-1;
                LevelSave.FloorNumber = LevelSave.maxFloors-1;
                LevelSave.LightNumber = LevelSave.maxLights-1;
            }

            public void Switch(Game1 game)
            {
                SwitchDraw = false;
                DummyRot = Vector3.Zero;
                PlaceRot = Vector3.Zero;

                if (TypeSelect == 1)
                {
                    BlockScale.X = game.B_Holder.BlockSize[NumberSelect[TypeSelect - 1]].X;
                    BlockScale.Y = 100;
                    BlockScale.Z = game.B_Holder.BlockSize[NumberSelect[TypeSelect - 1]].Z;



                    Value[0]=game.B_Holder.BlockDestructable[NumberSelect[TypeSelect - 1]];
                    Value[1]=game.B_Holder.BlockLife[NumberSelect[TypeSelect - 1]];
                    Value[2]=game.B_Holder.BlockPhaseBlock[NumberSelect[TypeSelect - 1]];
                    Value[3] = game.B_Holder.BlockRespawnTime[NumberSelect[TypeSelect - 1]];
                    MaxValue[0] = 1;
                    MinValue[0] = 0;
                    MaxValue[1] = 1000;
                    MinValue[1] = 10;
                    MaxValue[2] = 1;
                    MinValue[2] = 0;
                    MaxValue[3] = 1000;
                    MinValue[3] = 100;

                    Value_Type[0] = "Bool";
                    Value_Type[1] = "Int";
                    Value_Type[2] = "Bool";
                    Value_Type[3] = "Int";

                    Value_name[0] = "Destructable";
                    Value_name[1] = "Life";
                    Value_name[2] = "Phase Block";
                    Value_name[3] = "Respawn Time";

                    value_numb = 4;

                }

                if (TypeSelect == 2)
                {
                        BlockScale.X = game.S_Holder.SpecialSize[NumberSelect[TypeSelect - 1]].X;
                        BlockScale.Y = game.S_Holder.SpecialSize[NumberSelect[TypeSelect - 1]].Y;
                        BlockScale.Z = game.S_Holder.SpecialSize[NumberSelect[TypeSelect - 1]].Z;

                        for (int i = 0; i < game.S_Holder.SpecialControlsNumber[NumberSelect[TypeSelect - 1]]; i++)
                        {
                            Value[i] = game.S_Holder.ControlDefault[NumberSelect[TypeSelect - 1],i];
                            Value_name[i] = game.S_Holder.ControlName[NumberSelect[TypeSelect - 1], i];
                            MinValue[i] = game.S_Holder.ControlMin[NumberSelect[TypeSelect - 1], i];
                            MaxValue[i] = game.S_Holder.ControlMax[NumberSelect[TypeSelect - 1], i];
                            Value_Type[i] = game.S_Holder.ControlType[NumberSelect[TypeSelect - 1], i];
                        }
                        value_numb = (int)game.S_Holder.SpecialControlsNumber[NumberSelect[TypeSelect - 1]];

                }
                if (TypeSelect == 3)
                {
                    BlockScale.X = game.F_Holder.FloorSize[NumberSelect[TypeSelect - 1]].X;
                    BlockScale.Y = 100;
                    BlockScale.Z = game.F_Holder.FloorSize[NumberSelect[TypeSelect - 1]].Z;
                    value_numb = 0;
                }

                if (TypeSelect == 4)
                {
                    BlockScale.X = 100;
                    BlockScale.Y = 100;
                    BlockScale.Z = 100;



                    Value[0] = 100;
                    Value[1] = 100;
                    Value[2] = 100;
                    Value[3] = 1000;
                    Value[4] = 300;

                    MaxValue[0] = 800;
                    MinValue[0] = 0;
                    MaxValue[1] = 800;
                    MinValue[1] = 0;
                    MaxValue[2] = 800;
                    MinValue[2] = 0;
                    MaxValue[3] = 3000;
                    MinValue[3] = 0;
                    MaxValue[4] = 1000;
                    MinValue[4] = 0;

                    Value_Type[0] = "Int";
                    Value_Type[1] = "Int";
                    Value_Type[2] = "Int";
                    Value_Type[3] = "Int";
                    Value_Type[4] = "Int";

                    Value_name[0] = "Red";
                    Value_name[1] = "Green";
                    Value_name[2] = "Blue";
                    Value_name[3] = "Distance";
                    Value_name[4] = "Height";
                    value_numb = 5;

                }
            }

            public void SwitchTO(Game1 game)
            {
                
                if (TypeFound == 0)
                {
                    PlaceRot = Blockfound.Rotation;
                    SwitchDraw = Blockfound.SwitchDraw;
                    TypeSelect = 1;
                    NumberSelect[TypeSelect - 1] = Blockfound.Type;

                    BlockScale.X = Blockfound.Size.X;
                        BlockScale.Y = 100;
                        BlockScale.Z = Blockfound.Size.Y;


                        if (Blockfound.Destructable)
                            Value[0] = 1;
                        else
                            Value[0] = 0;
                        Value[1] = (int)Blockfound.MaxLife;
                        if (Blockfound.PhaseBlock)
                            Value[2] = 1;
                        else
                            Value[2] = 0;
                        Value[3] = Blockfound.MaxRespawnTime;

                  

                        MaxValue[0] = 1;
                        MinValue[0] = 0;
                        MaxValue[1] = 1000;
                        MinValue[1] = 10;
                        MaxValue[2] = 1;
                        MinValue[2] = 0;
                        MaxValue[3] = 3000;
                        MinValue[3] = 100;

                        Value_Type[0] = "Bool";
                        Value_Type[1] = "Int";
                        Value_Type[3] = "Bool";
                        Value_Type[3] = "int";


                        Value_name[0] = "Destructable";
                        Value_name[1] = "Life";
                        Value_name[2] = "Phase Block";
                        Value_name[3] = "Respawn Time";

                        value_numb = 4;

                    

                }

                if (TypeFound == 1)
                {
                    TypeSelect = 2;
                    NumberSelect[TypeSelect - 1] = SpecialFound.Type;
                    PlaceRot = SpecialFound.Rotation;
                    SwitchDraw = SpecialFound.SwitchDraw;
                    BlockScale.X = SpecialFound.Size.X;
                    BlockScale.Y = game.S_Holder.SpecialSize[SpecialFound.Type].Y;
                    BlockScale.Z = SpecialFound.Size.Y;

                    for (int i = 0; i < game.S_Holder.SpecialControlsNumber[SpecialFound.Type]; i++)
                    {
                        Value[i] = SpecialFound.Value[i];
                        Value_name[i] = game.S_Holder.ControlName[SpecialFound.Type, i];
                        MinValue[i] = game.S_Holder.ControlMin[SpecialFound.Type, i];
                        MaxValue[i] = game.S_Holder.ControlMax[SpecialFound.Type,i];
                        Value_Type[i] = game.S_Holder.ControlType[SpecialFound.Type, i];
                    }
                    value_numb = (int)game.S_Holder.SpecialControlsNumber[NumberSelect[TypeSelect - 1]];
                }
                if (TypeFound == 2)
                {
                    TypeSelect = 2;
                    NumberSelect[TypeSelect - 1] = Floorfound.Type;
                    PropertiesWindowIsOpen = false;
                }

                if (TypeFound ==3)
                {
                    BlockScale.X = 100;
                    BlockScale.Y = 100;
                    BlockScale.Z = 100;



                    Value[0] = (int)(LightFound.Color.X * 100);
                    Value[1] = (int)(LightFound.Color.Y * 100);
                    Value[2] = (int)(LightFound.Color.Z * 100);
                    Value[3] = (int)LightFound.Distancee;
                    Value[4] = (int)LightFound.Position.Y;

                    MaxValue[0] = 800;
                    MinValue[0] = 0;
                    MaxValue[1] = 800;
                    MinValue[1] = 0;
                    MaxValue[2] = 800;
                    MinValue[2] = 0;
                    MaxValue[3] = 3000;
                    MinValue[3] = 0;
                    MaxValue[4] = 1000;
                    MinValue[4] = 0;

                    Value_Type[0] = "Int";
                    Value_Type[1] = "Int";
                    Value_Type[2] = "Int";
                    Value_Type[3] = "Int";
                    Value_Type[4] = "Int";

                    Value_name[0] = "Red";
                    Value_name[1] = "Green";
                    Value_name[2] = "Blue";
                    Value_name[3] = "Distance";
                    Value_name[4] = "Height";
                    value_numb = 5;

                }
            }

            public void DrawPlayerRing(Game1 game, Vector2 Mult)
            {
                HUDRECT.Height = (int)(48 * Mult.Y);
                HUDRECT.Width = (int)(48 * Mult.Y);
                HUDRECT.Y = 0;
                HUDRECT.X = (int)(PlayerPort.Width / 2 - 160 * Mult.Y);
                float rot = 0;


                game.spriteBatch.DrawString(game.Loader.font, Name, new Vector2(HUDRECT.X + 80 * Mult.Y, HUDRECT.Y), Color.White, 0, Vector2.Zero, Mult.Y, SpriteEffects.None, 0);
          

                if(Currentplayer == PlayerIndex.Two)
                {
                    rot = 90;
                    HUDRECT.X += (int)(48 * Mult.Y);
                }
                if (Currentplayer == PlayerIndex.Four)
                {
                    rot = 180;
                    HUDRECT.X += (int)(48 * Mult.Y);
                    HUDRECT.Y += (int)(48 * Mult.Y);
                }
                if (Currentplayer == PlayerIndex.Three)
                {
                    rot = 270;
                    HUDRECT.Y += (int)(48 * Mult.Y);
                }
                //game.spriteBatch.Draw(game.Loader.PlayerBar, HUDRECT, color);
                game.spriteBatch.Draw(game.Loader.ControllerRing, HUDRECT, null, Color.White, MathHelper.ToRadians(rot), new Vector2(0.5f), SpriteEffects.None, 0);
            }

            public void DrawOverGlow(Game1 game, Vector2 Mult)
            {
               // game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);

                Color  Col = new Color(colorVec * HealthAlpha);
                HUDRECT.Height = (int)(600 * Mult.Y);
                HUDRECT.Width = (int)(600 * Mult.X);
                HUDRECT.Y = PlayerPort.Height - (int)(350 * Mult.Y);
                HUDRECT.X = (int)((50 - 300) * Mult.X);
                game.spriteBatch.Draw(game.Loader.Glow, HUDRECT, Col);

                Col = new Color(0.25f, 0.5f, 1) * EnergyAlpha;
                HUDRECT.Height = (int)(600 * Mult.Y);
                HUDRECT.Width = (int)(600 * Mult.X);
                HUDRECT.Y = PlayerPort.Height - (int)(450 * Mult.Y);
                HUDRECT.X = (int)((70 - 300) * Mult.X);
                game.spriteBatch.Draw(game.Loader.Glow, HUDRECT, Col);

                float Size = 600;
                Col = new Color(0.5f, 0.5f, 0.5f) * RingAlpha;
                HUDRECT.Height = (int)(Size * Mult.Y);
                HUDRECT.Width = (int)(Size * Mult.Y);
                HUDRECT.Y = PlayerPort.Height/2 - (int)(Size/2 * Mult.Y + 200 * Mult.Y);
                HUDRECT.X = PlayerPort.Width / 2 - (int)(Size / 2 * Mult.Y);
                game.spriteBatch.Draw(game.Loader.Glow, HUDRECT, Col);

         

                //game.spriteBatch.End();
            }

            public void DrawPointer(Game1 game, BasicOrb orb, Vector2 Mult)
            {
                Vector3 TargetPos = Vector3.Zero;

                if (game.gamemode == Game1.GameMode.KeepAway)
                    TargetPos = Vector3.Normalize(game.flag.Position-orb.Position);

                if (game.gamemode == Game1.GameMode.WarLord)
                    foreach (BasicOrb ob in game.Orbs)
                    if(ob.Alive&&ob.relevent&&ob.IsAssasin)
                    {
                        TargetPos = Vector3.Normalize(orb.Position - ob.Position);
                    }

                TargetPos *= 350 * Mult.Y;

                Vector3.TransformNormal(TargetPos, Matrix.CreateFromYawPitchRoll(0, orb.Rotation.Y, 0));

                float Rot= -(float)Math.Atan2(TargetPos.X,TargetPos.Z)+MathHelper.ToRadians(90);

                float Size = 100;
                HUDRECT.Height = (int)(Size * Mult.Y);
                HUDRECT.Width = (int)(Size * Mult.X);
                HUDRECT.Y = (int)(PlayerPort.Height/2+TargetPos.Z-Size*Mult.Y/2);
                HUDRECT.X = (int)(PlayerPort.Width / 2 + TargetPos.X - Size * Mult.Y / 2);
                game.spriteBatch.Draw(game.Loader.ScreenPointer, HUDRECT, null, Color.White, Rot - MathHelper.ToRadians(90), new Vector2(Size * Mult.Y / 2), SpriteEffects.None, 0);
            }

            public void DrawInGameHUD(Game1 game)
            {
              //  game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

                Color Col;
                BasicOrb orb = game.Orbs[PlayerTarget];
                Vector2 Mult = PortSize / new Vector2(1200, 1000);
                PermMult = Mult;

                //if(game.gamemode==Game1.GameMode.KeepAway||game.gamemode==Game1.GameMode.WarLord&&!orb.IsAssasin)
               // DrawPointer(game, orb, Mult);

                HUDRECT.Height = (int)(48 * Mult.Y);
                HUDRECT.Width = (int)(500 * Mult.X);
                HUDRECT.Y = 0;
                HUDRECT.X = (int)(PlayerPort.Width/2-250*Mult.X);
                game.spriteBatch.Draw(game.Loader.PlayerBar, HUDRECT, color);

                DrawPlayerRing(game, Mult);

                // Mult = new Vector2(0.5f, 2);
                if(!PauseWindowIsOpen&&!TeamSelectIsOpen&&ScoreWindowAlpha<0.5f&&orb.Alive)
                {
                    HasLived = true;





                    //if (false)
                    {
                        Col = color;
                        HUDRECT.Height = (int)(200 * Mult.Y);
                        HUDRECT.Width = (int)(200 * Mult.Y);
                        HUDRECT.Y = PlayerPort.Height - (int)(250 * Mult.Y);
                        HUDRECT.X = PlayerPort.Width/2 - (int)(100 * Mult.Y);
                        game.spriteBatch.Draw(game.Holder.GunIcon[orb.GunCurrent], HUDRECT,new Color(colorVec*0.5f));

                       // HUDRECT.Y = PlayerPort.Height - (int)(500 * Mult.Y);
                       // game.spriteBatch.Draw(game.A_holder.AbilityTexture[game.A_holder.Translate(orb.Abilty[0])], HUDRECT, Col);
                    }

                    Col = new Color(0, 0, 0);
                    HUDRECT.Height = (int)(80 * Mult.Y);
                    HUDRECT.Width = (int)(250 * Mult.X);
                    HUDRECT.Y = PlayerPort.Height - (int)(100 * Mult.Y);
                    HUDRECT.X = (int)(50 * Mult.X);
                    game.spriteBatch.Draw(game.Loader.Temp, HUDRECT, Col);

                    HUDRECT.Height -= 4;
                    HUDRECT.Y += 2;
                    HUDRECT.Width -= 4;
                    HUDRECT.X += 2;

                    Col = color;
                    HUDRECT.Width = (int)((int)(250 * Mult.X) * orb.life / orb.MaxLife);
                    game.spriteBatch.Draw(game.Loader.Temp, HUDRECT, Col);




                    if (false)
                    {
                        Col = new Color(0, 0, 0);
                        HUDRECT.Height = (int)(80 * Mult.Y);
                        HUDRECT.Width = (int)(250 * Mult.X);
                        HUDRECT.Y = PlayerPort.Height - (int)(100 * Mult.Y);
                        HUDRECT.X = (int)(350 * Mult.X);
                        game.spriteBatch.Draw(game.Loader.Temp, HUDRECT, Col);

                        Col = new Color(0.5f, 0.5f, 1);
                        HUDRECT.Width = (int)((int)(250 * Mult.X) * (orb.DamageResistance / orb.MaxDamageResistance + orb.DamageResistance2 / orb.MaxDamageResistance2) / 2);
                        game.spriteBatch.Draw(game.Loader.Temp, HUDRECT, Col);
                    }

                    Col = new Color(0, 0, 0);
                    HUDRECT.Height = (int)(40 * Mult.Y);
                    HUDRECT.Width = (int)(200 * Mult.X);
                    HUDRECT.Y = PlayerPort.Height - (int)(160 * Mult.Y);
                    HUDRECT.X = (int)(70 * Mult.X);
                    game.spriteBatch.Draw(game.Loader.Temp, HUDRECT, Col);

                    HUDRECT.Height -= 4;
                    HUDRECT.Y += 2;
                    HUDRECT.Width -= 4;
                    HUDRECT.X += 2;

                    Col = new Color(0.25f, 0.5f, 1);
                    HUDRECT.Width = (int)((int)(200 * Mult.X) * orb.Energy / orb.MaxEnergy);
                    game.spriteBatch.Draw(game.Loader.Temp, HUDRECT, Col);




                    float AvgMult = (Mult.X + Mult.Y) / 2;



                    Col = new Color(1, 1 - Math.Max(0, orb.HitBounce / orb.MaxHitBounce), 1 - Math.Max(0, orb.HitBounce / orb.MaxHitBounce));
                    float Size = 65 + 65 * Math.Max(0, orb.HitBounce / orb.MaxHitBounce);
                    HUDRECT.Height = (int)(Size * AvgMult);
                    HUDRECT.Width = (int)(Size * AvgMult);
                    HUDRECT.Y = (int)((PortSize.Y) / 2 - (Size / 2 * AvgMult + 200 * Mult.Y));
                    HUDRECT.X = (int)(PortSize.X / 2 - Size / 2 * AvgMult);
                    game.spriteBatch.Draw(game.Loader.CrossHair, HUDRECT, Col);



                    if (orb.Abilty[0] > -1)
                    {
                        int Key = game.A_holder.Translate(orb.Abilty[0]);

                        if (orb.Energy >= game.A_holder.AbilityCost[Key])
                        {
                            AbilityAlpha += 0.1f;
                            if (AbilityAlpha > 1)
                                AbilityAlpha = 1;
                        }
                        else
                        {
                            AbilityAlpha -= 0.1f;
                            if (AbilityAlpha < 0.1)
                                AbilityAlpha = 0.1f;
                        }

                        Col = new Color(1, 1, 1, 1f) * AbilityAlpha;
                        Size = 60;
                        HUDRECT.Height = (int)(Size * Mult.Y);
                        HUDRECT.Width = (int)(Size * Mult.Y);
                        HUDRECT.Y = (int)((PortSize.Y) / 2 - (Size / 2 + 100) * Mult.Y);
                        HUDRECT.X = (int)(PortSize.X / 2 - (Size / 2) * Mult.Y);
                        game.spriteBatch.Draw(game.A_holder.AbilityIcon[Key], HUDRECT, Col);

                        if (false)
                        {
                            Col = new Color(1, 1, 1, 0.5f);
                            Size = 60;
                            HUDRECT.Height = (int)(Size * Mult.Y);
                            HUDRECT.Width = (int)(Size * Mult.Y);
                            HUDRECT.Y = (int)((PortSize.Y) / 2 - (Size / 2 + 200) * Mult.Y);
                            HUDRECT.X = (int)(PortSize.X / 2 - (Size / 2 - 100) * Mult.Y);
                            game.spriteBatch.Draw(game.Holder.GunIcon[orb.GunCurrent], HUDRECT, Col);
                        }

                    }


                    float ChargeVal = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        if (orb.Charge[i] > 0)
                            ChargeVal = orb.Charge[i] / game.Holder.ChargeTime[orb.GunCurrent, i];
                        if (orb.AbiltyCharge[i] > 0)
                            ChargeVal = orb.AbiltyCharge[i];
                        if (orb.AbilityCounter > 0)
                            ChargeVal = orb.AbilityCounter / orb.AbilityMaxCounter;
                    }
                    if (ChargeVal > 0)
                    {
                        Col = new Color(0, 0, 0);
                        HUDRECT.Height = (int)(18 * Mult.Y);
                        HUDRECT.Width = (int)(100 * Mult.X);
                        HUDRECT.Y = (int)((PortSize.Y) / 2 - 140 * Mult.Y);
                        HUDRECT.X = (int)(PortSize.X / 2 - 50 * Mult.X);
                        game.spriteBatch.Draw(game.Loader.Temp, HUDRECT, Col);

                        HUDRECT.Height -= 2;
                        HUDRECT.Y += 1;
                        HUDRECT.Width -= 2;
                        HUDRECT.X += 1;

                        Col = new Color(Vector3.One);
                        HUDRECT.Width = (int)((int)(100 * Mult.X) * ChargeVal);
                        game.spriteBatch.Draw(game.Loader.Temp, HUDRECT, Col);
                    }

                }
                else if (!orb.Alive&&HasLived)
                {

                    Col = Color.White;
                    HUDRECT.Height = (int)(500 * Mult.Y);
                    HUDRECT.Width = (int)(500 * Mult.Y);
                    HUDRECT.Y = (int)((PortSize.Y) / 2 - 250 * Mult.Y);
                    HUDRECT.X = (int)(PortSize.X / 2 - 250 * Mult.Y);
                    game.spriteBatch.Draw(game.Loader.DeadTexture, HUDRECT, Color.Red);
                }

                //game.spriteBatch.DrawString(game.Loader.font, orb.HealthPacks.ToString(), new Vector2((int)(40 * Mult.X), PlayerPort.Height - (int)(140 * Mult.Y)), Color.White);
                // game.spriteBatch.DrawString(game.Loader.font, Kills.ToString(), new Vector2((int)(40 * Mult.X), (int)(40 * Mult.Y)), Color.White);
                // game.spriteBatch.DrawString(game.Loader.font, Deaths.ToString(), new Vector2((int)(40 * Mult.X), (int)(80 * Mult.Y)), Color.White);
                // game.spriteBatch.DrawString(game.Loader.font, Money.ToString(), new Vector2((int)(PortSize.X- 120 * Mult.X), (int)( 120 * Mult.Y)), Color.White);

                //game.spriteBatch.DrawString(game.Loader.font, orb.LastDamager.ToString(), new Vector2((int)(40 * Mult.X), (int)(40 * Mult.Y)), Color.White);

                //  for (int i = 0; i < 16;i++ )
                //     game.spriteBatch.DrawString(game.Loader.font, ((int)orb.DamageFrom[i]).ToString(), new Vector2((int)(40 * Mult.X), (int)(120+40*i * Mult.Y)), Color.White);




                DrawMessages(game, Mult);

                DrawHealthBars(game, Mult);


                //

                if (DamageAlpha > 0)
                    game.DrawFullscreenQuad(game.Loader.DamageTexture,this, BlendState.AlphaBlend, null, new Color(Vector4.One * DamageAlpha));
                if (orb.Alpha<1)
                    game.DrawFullscreenQuad(game.Loader.CloakTexture, this, BlendState.AlphaBlend, null, new Color(Vector4.One * (1-orb.Alpha)));

                DrawBuyScreen(game, Mult);
                if (!TeamSelectIsOpen)
                    DrawScoreScreen(game, Mult);
                DrawPauseScreen(game, Mult);
                if (!TeamSelectIsOpen)
                    DrawScoreHUD(game, Mult);
                DrawTeamScreen(game, Mult);


                if (!PauseWindowIsOpen && !TeamSelectIsOpen && ScoreWindowAlpha < 0.5f && orb.Alive)
                {
                    game.spriteBatch.End();
                    game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, game.Loader.RingEffect);
                    DrawRings(game, Mult, orb);
                }


            }

            public void DrawScoreScreen(Game1 game, Vector2 Mult)
            {

                if (ScoreWindowAlpha > 0)
                    game.DrawFullscreenQuad(game.Loader.Temp, this, BlendState.AlphaBlend, null, new Color(0, 0, 0, ScoreWindowAlpha * 0.4f));

               // game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                if (ScoreWindowAlpha > 0)
                {
                    /*
                    Color Col;

                    Col = new Color(0.3f, 0.3f, 0.3f);
                    HUDRECT.Height = (int)(800 * Mult.Y * BuyWindowSize);
                    HUDRECT.Width = (int)(600 * Mult.X * BuyWindowSize);
                    HUDRECT.Y = (int)(PlayerPort.Height / 2 - 400 * Mult.Y * BuyWindowSize);
                    HUDRECT.X = (int)(PlayerPort.Width / 2 - 300 * Mult.X * BuyWindowSize);
                    game.spriteBatch.Draw(game.Loader.Temp, HUDRECT, Col);
                    */

                    
                    
                    int listsize = 0;
                    if (game.gamemode != Game1.GameMode.TeamDeathMatch)
                    {



                        foreach (BasicOrb orb in game.Orbs)
                            if (orb.relevent)
                            {
                                listsize += 1;
                                orb.Taken = false;
                            }
                        //listsize = 10;
                        
                    }
                    else
                        listsize = 2;

                    if (false)
                    {
                        HUDRECT.Height = (int)(listsize * 64 * Mult.Y);
                        HUDRECT.Width = (int)(350);
                        HUDRECT.Y = (int)(PlayerPort.Y - listsize * 64 * Mult.Y);
                        HUDRECT.X = (int)(PlayerPort.Width / 2 - 175);
                        game.spriteBatch.Draw(game.Loader.MenuWindow, HUDRECT, new Color(Vector4.One * ScoreWindowAlpha));
                    }

                    BasicOrb[] List = new BasicOrb[16];

                    int[] TeamList = new int[2];

                    if (game.gamemode != Game1.GameMode.TeamDeathMatch)
                    {
                        for (int i = 0; i < listsize; i++)
                        {
                            BestKills = -1;
                            BestDeaths = 100000;



                            foreach (BasicOrb orb in game.Orbs)
                                if (orb.relevent)
                                    if (!orb.Taken)
                                    {
                                        int oldKills = orb.MyController.Kills;
                                        if (game.gamemode == Game1.GameMode.KeepAway)
                                            orb.MyController.Kills = orb.MyController.FlagScore;

                                        if (orb.MyController.Kills > BestKills || orb.MyController.Kills == BestKills && orb.MyController.Deaths <= BestDeaths)
                                        {
                                            BestOrb = orb;
                                            BestKills = BestOrb.MyController.Kills;
                                            BestDeaths = BestOrb.MyController.Deaths;
                                        }
                                        if (game.gamemode == Game1.GameMode.KeepAway)
                                        orb.MyController.Kills = oldKills;
                                    }
                            if (!BestOrb.Taken)
                            {
                                BestOrb.Taken = true;
                                List[i] = BestOrb;
                            }
                        }
                    }
                    else
                    {
                        if (game.TeamScore[0] > game.TeamScore[1] || game.TeamScore[0] == game.TeamScore[1] && game.TeamDeaths[0] < game.TeamDeaths[1])
                        {
                            TeamList[0] = 0;
                            TeamList[1] = 1;
                        }
                        else
                        {
                            TeamList[0] = 0;
                            TeamList[1] = 1;
                        }


                    }

                    string Text="";
                    string Text2 = "";
                    bool IsMe = false;

                    if (!game.GameOver)
                        Text = "Score to Win: " + game.KillsToWin.ToString();
                    else
                    {
                        if (game.gamemode != Game1.GameMode.TeamDeathMatch)
                            Text = game.Winner.MyController.Name.ToString();
                        else
                        {
                            if (game.WinningTeam == 0)
                                Text = "Red Team ";
                            else
                                Text = "Blue Team " ;
                        }
                        Text+=" Wins!";
                    }
                    Vector2 Pos = new Vector2((int)(PlayerPort.Width / 2 - (0 * (1 - ScoreWindowAlpha)) * Mult.X) - game.Loader.font.MeasureString(Text).X / 2*Mult.X, (int)(PlayerPort.Height / 4 -64*Mult.Y));



                    game.spriteBatch.DrawString(game.Loader.font, Text, Pos, new Color(1, 1, 1, ScoreWindowAlpha), 0, Vector2.Zero, Mult.X, SpriteEffects.None, 0);

                    game.spriteBatch.DrawString(game.Loader.font, game.gamemode.ToString(), Pos-new Vector2(0,game.Loader.font.MeasureString(Text).Y*Mult.Y*1.5f), new Color(1, 1, 1, ScoreWindowAlpha), 0, Vector2.Zero, Mult.X, SpriteEffects.None, 0);


                    float YY = 0;
                    for (int i = 0; i < listsize; i++)
                    {
                        
                        Color Col;
                        Vector4 vec = Vector4.Zero;
                        bool Go = false;

                        //foreach (BasicOrb orb in List)
                        if (game.gamemode == Game1.GameMode.TeamDeathMatch)
                        {
                            Go = true;
                            if (TeamList[i] == 0)
                            {
                                Text = "Red Team ";
                                vec = new Vector4(1, 0, 0, 1);
                            }
                            else
                            {
                                Text = "Blue Team ";
                                vec = new Vector4(0, 0, 1, 1);
                            }
                            Text2 = game.TeamScore[TeamList[i]].ToString();
                        }
                        else if (List[i] != null)
                        {
                            BasicOrb orb = List[i];
                            Go = true;

                            int oldKills = orb.MyController.Kills;
                            if(game.gamemode==Game1.GameMode.KeepAway)
                            orb.MyController.Kills = orb.MyController.FlagScore;

                            MyOrb = game.Orbs[PlayerTarget];

                            if (orb.Team == MyOrb.Team)
                            {
                                //if (orb == MyOrb)
                                //    vec = new Vector3(0.2f, 0.4f, 0.2f) * 2;
                                //else
                                //    vec = new Vector3(0.2f, 0.2f, 0.4f) * 2;
                            }
                            //else
                            // vec = new Vector3(0.4f, 0.2f, 0.2f)*2;

                            Text = orb.MyController.Name;// +" : " + orb.MyController.Kills.ToString();
                            Text2 = orb.MyController.Kills.ToString();

                            vec = orb.MyController.colorVec;// *new Vector4(0.75f, 0.75f, 0.75f, 1);

                            if (orb == game.Orbs[PlayerTarget])
                                IsMe = true;
                            else
                                IsMe = false;

                            if (game.gamemode == Game1.GameMode.KeepAway)
                            orb.MyController.Kills = oldKills;
                        }
                        if (Go)
                        {
                            SpriteFont font = game.Loader.font;
                            //if (game.LocalPlayerNumb > 1)
                            //    font = game.Loader.SmallFont;


                            Pos = new Vector2((int)(PlayerPort.Width / 2 - (0 * (1 - ScoreWindowAlpha)) * Mult.X)  - 150 * Mult.X, (int)(PlayerPort.Height / 4 + YY));


                            Col = new Color(vec);
                            HUDRECT.Height = (int)((font.MeasureString(Text).Y + 10) * Mult.X);
                            HUDRECT.Width = (int)(  250 * Mult.X);
                            HUDRECT.Y = (int)(Pos.Y - 5 * Mult.Y);
                            HUDRECT.X = (int)(Pos.X -2.5f * Mult.X);
                            game.spriteBatch.Draw(game.Loader.MenuWindow2, HUDRECT, Col);



                            Pos = new Vector2((int)(PlayerPort.Width / 2) -( font.MeasureString(Text).X / 2 + 75) * Mult.X, (int)(PlayerPort.Height / 4 + YY));

                            float StringMult = 1;
                            if (game.Loader.font.MeasureString(Text).X * Mult.X > 150 * Mult.X)
                                StringMult = 150 / game.Loader.font.MeasureString(Text).X;

                           // game.spriteBatch.DrawString(font, Text, Pos + new Vector2(2,2), new Color(0, 0, 0, ScoreWindowAlpha));
                            game.spriteBatch.DrawString(font, Text, Pos, new Color(1, 1, 1, ScoreWindowAlpha), 0, Vector2.Zero, Mult.X*StringMult, SpriteEffects.None, 0);
                            Pos = new Vector2((int)(PlayerPort.Width / 2 - (0 * (1 - ScoreWindowAlpha)) * Mult.X) - font.MeasureString(Text2).X / 2 + 75 * Mult.X, (int)(PlayerPort.Height / 4 + YY));
                            //game.spriteBatch.DrawString(font, Text2, Pos + new Vector2(2, 2), new Color(0, 0, 0, ScoreWindowAlpha));
                            game.spriteBatch.DrawString(font, Text2, Pos, new Color(1, 1, 1, ScoreWindowAlpha), 0, Vector2.Zero, Mult.X, SpriteEffects.None, 0);

                            YY += (font.MeasureString(Text).Y + 10) * Mult.X;


                            if (IsMe)
                            {
                                HUDRECT.Height = (int)((font.MeasureString(Text).Y + 10) * Mult.X);
                                HUDRECT.Width = (int)(64*Mult.Y);
                                HUDRECT.Y = (int)(Pos.Y - 5 * Mult.Y);
                                HUDRECT.X = (int)(PlayerPort.Width / 2 - 200*Mult.X);
                                game.spriteBatch.Draw(game.Loader.ArrowTexture, HUDRECT, Col);

                                HUDRECT.Height = (int)((font.MeasureString(Text).Y + 10) * Mult.X);
                                HUDRECT.Width = (int)(64 * Mult.Y);
                                HUDRECT.Y = (int)(Pos.Y - 5 * Mult.Y);
                                HUDRECT.X = (int)(PlayerPort.Width / 2 + 100 * Mult.X);
                                game.spriteBatch.Draw(game.Loader.ArrowTexture, HUDRECT,null, Col,0,Vector2.Zero,SpriteEffects.FlipHorizontally,0);
                            }

                        }
  


                    }
                }
               // game.spriteBatch.End();
            }

            public void DrawScoreHUD(Game1 game, Vector2 Mult)
            {
                
                string Text;
                Vector2 Pos;
                Color Col;
                BasicOrb OrbMine=game.Orbs[PlayerTarget];
               // game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                
                    

                    //BasicOrb myOrb = game.Orbs[PlayerTarget];


                int NumbBetterThanMe = 0; ;
                        BestKills = 0;
                        BestDeaths = 0;

                        bool IsTied = false;
                        if (game.gamemode == Game1.GameMode.TeamDeathMatch)
                        {
                            if (game.TeamScore[OrbMine.Team] < game.TeamScore[1 - OrbMine.Team])
                                NumbBetterThanMe += 1;
                            else
                                if (game.TeamScore[OrbMine.Team] == game.TeamScore[1 - OrbMine.Team])
                                    IsTied = true;
                        }
                        else
                        foreach (BasicOrb orb in game.Orbs)
                            if (orb.relevent)
                                if (orb.MyController != this)
                                {
                                    int oldKills = orb.MyController.Kills;
                                    if (game.gamemode == Game1.GameMode.KeepAway)
                                    orb.MyController.Kills = orb.MyController.FlagScore;

                                    if (orb.MyController.Kills > Kills)// || orb.MyController.Kills == Kills && orb.MyController.Deaths < Deaths)
                                    {
                                        NumbBetterThanMe += 1;
                                        if (orb.MyController.Kills > BestKills || orb.MyController.Kills == BestKills && orb.MyController.Deaths >= BestDeaths)
                                        {
                                            BestOrb = orb;
                                            BestKills = BestOrb.MyController.Kills;
                                            BestDeaths = BestOrb.MyController.Deaths;
                                        }
                                    }
                                    else if (orb.MyController.Kills == Kills )
                                        IsTied = true;
                                    if (game.gamemode == Game1.GameMode.KeepAway)
                                    orb.MyController.Kills = oldKills;
                                }
                        if (game.gamemode != Game1.GameMode.TeamDeathMatch)
                        {
                            if (game.gamemode == Game1.GameMode.KeepAway)
                                Text = FlagScore.ToString();
                            else
                                Text = Kills.ToString();
                        }
                        else
                            Text = game.TeamScore[game.Orbs[PlayerTarget].Team].ToString();
                        Pos = new Vector2(PlayerPort.Width-100*Mult.X,PlayerPort.Height-150*Mult.X);


                        //Pos = new Vector2(400);
                        Col = new Color(colorVec * 0.75f);
                        HUDRECT.Height = (int)((game.Loader.font.MeasureString(Text).Y + 20) * Mult.X);
                        HUDRECT.Width = (int)((game.Loader.font.MeasureString(Text).X + 10) * Mult.X);
                        HUDRECT.Y = (int)(Pos.Y - 10 * Mult.Y);
                        HUDRECT.X = (int)(Pos.X - 5 * Mult.X);
                        game.spriteBatch.Draw(game.Loader.Temp, HUDRECT, Col);

                        game.spriteBatch.DrawString(game.Loader.font, Text, Pos, Color.White,0,Vector2.Zero,Mult.X,SpriteEffects.None,0);

                        if (NumbBetterThanMe > 0)
                        {
                            if (game.gamemode != Game1.GameMode.TeamDeathMatch)
                            {
                                if (game.gamemode != Game1.GameMode.KeepAway)
                                    Text = BestOrb.MyController.Kills.ToString();
                                else
                                    Text = BestOrb.MyController.FlagScore.ToString();
                                Col = new Color(BestOrb.MyController.colorVec * 0.75f);
                            }
                            else
                            {
                                Text = game.TeamScore[1 - game.Orbs[PlayerTarget].Team].ToString();
                                if (1 - game.Orbs[PlayerTarget].Team == 0)
                                    Col = new Color(new Vector3(1, 0, 0)*0.75f);
                                else
                                    Col = new Color(new Vector3(0, 0, 1) * 0.75f);
                            }
                            Pos = new Vector2(PlayerPort.Width - 100*Mult.X, PlayerPort.Height - 200*Mult.X);


                            
                            HUDRECT.Height = (int)((game.Loader.font.MeasureString(Text).Y + 20) * Mult.X);
                            HUDRECT.Width = (int)((game.Loader.font.MeasureString(Text).X + 10) * Mult.X);
                            HUDRECT.Y = (int)(Pos.Y - 10 * Mult.Y);
                            HUDRECT.X = (int)(Pos.X - 5 * Mult.X);
                            game.spriteBatch.Draw(game.Loader.Temp, HUDRECT, Col);

                            game.spriteBatch.DrawString(game.Loader.font, Text, Pos, Color.White, 0, Vector2.Zero, Mult.X, SpriteEffects.None, 0);

                        }


                        Pos = new Vector2(PlayerPort.Width - 175*Mult.X, PlayerPort.Height - 150*Mult.Y);


                        Text = "";

                        if (game.gamemode == Game1.GameMode.TeamDeathMatch)
                            Text += "Team is ";

                        if (IsTied)
                            Text += "tied for ";

                if(NumbBetterThanMe==0)
                        Text += "1st";
                if (NumbBetterThanMe == 1)
                    Text += "2nd";
                if (NumbBetterThanMe == 2)
                    Text += "3rd";
                if (NumbBetterThanMe >2)
                    Text += (NumbBetterThanMe+1).ToString()+ "th";

                game.spriteBatch.DrawString(game.Loader.font, Text, Pos - new Vector2(game.Loader.font.MeasureString(Text).X * Mult.X, 0) + new Vector2(2, 2), Color.Black, 0, Vector2.Zero, Mult.X, SpriteEffects.None, 0);
                game.spriteBatch.DrawString(game.Loader.font, Text, Pos-new Vector2(game.Loader.font.MeasureString(Text).X*Mult.X,0), Color.White,0,Vector2.Zero,Mult.X,SpriteEffects.None,0);
                


             //   game.spriteBatch.End();
            }

            public void DrawRings(Game1 game, Vector2 Mult,BasicOrb orb)
            {
                float AvgMult = (Mult.X + Mult.Y) / 2;

                RingSpin = (int)(180 - orb.PhaseRecharge / orb.MaxPhaseRecharge * 360);
                game.GraphicsDevice.BlendState = BlendState.AlphaBlend;
                if (orb.PhaseRecharge > orb.MaxPhaseRecharge - 10)
                {
                    if (!HasSaidDashReady)
                    {
                        game.soundHolder.soundEffects["player_dash_ready"].Play(game.SoundEffectsVolume*0.25f,0,0);
                        HasSaidDashReady = true;
                    }
                    if (!HasRingFlashed)
                        RingAlpha += 0.1f;
                    else
                    {
                        if (RingAlpha > 0.25f)
                        {
                            RingAlpha -= 0.1f;
                            if (RingAlpha < 0.25f)
                                RingAlpha = 0.25f;
                        }
                        else
                        {
                            RingAlpha += 0.1f;
                        }

                    }

                    if (RingAlpha > 1)
                        HasRingFlashed = true;

                    
                }
                else
                {
                    HasSaidDashReady = false;

                    RingAlpha -= 0.1f;
                    if (RingAlpha < 0&&orb.PhaseRecharge<orb.MaxPhaseRecharge)
                        HasRingFlashed = false;
                }
                RingAlpha = MathHelper.Clamp(RingAlpha, 0, 1);

                float RingMult = 1;

                DrawRing(game, Mult, new Vector2(100, 100) * RingMult * AvgMult, new Vector2(PlayerPort.Width / 2, PlayerPort.Height / 2 - 200 * Mult.Y) - new Vector2(100, 100) * RingMult * AvgMult * 0.5f, MathHelper.ToRadians(RingSpin), 0.33f);
                if (RingAlpha > 0)
                    DrawRing(game, Mult, new Vector2(100, 100) * RingMult * AvgMult, new Vector2(PlayerPort.Width / 2, PlayerPort.Height / 2 - 200 * Mult.Y) - new Vector2(100, 100) * RingMult * AvgMult * 0.5f, MathHelper.ToRadians(-180), RingAlpha * 3f);
                    
            }

            public void DrawRing(Game1 game, Vector2 Mult,Vector2 Size,Vector2 Pos,float Angle,float Alpha)
            {
                
                
               
               HUDRECT.Height = (int)(Size.Y);
               HUDRECT.Width = (int)(Size.X);
               HUDRECT.Y = (int)(Pos.Y);
               HUDRECT.X = (int)(Pos.X);


                  // game.spriteBatch.Begin(0, BlendState.AlphaBlend, null, null, null, game.Loader.RingEffect);
                   game.Loader.RingEffect.Parameters["Angle"].SetValue(Angle);
                   game.Loader.RingEffect.Parameters["Alpha"].SetValue(Alpha);


               game.spriteBatch.Draw(game.Loader.RingTexture, HUDRECT,new Color(1,1,1,Alpha));

             //  game.spriteBatch.End();

            }


            public void DrawBuyScreen(Game1 game,Vector2 Mult)
            {
                string Text="";


               
                if (BuyWindowSize > 0)
                {
                    game.DrawFullscreenQuad(game.Loader.Temp,this,BlendState.AlphaBlend,null,new Color(0,0,0,0.4f));

                    //game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                    Text = "Select an Upgrade";
                    Vector2 Pos = new Vector2(PlayerPort.Width / 2 - game.Loader.font.MeasureString(Text).X / 2 * Mult.Y, PlayerPort.Height * 0.15f);

                    game.spriteBatch.DrawString(game.Loader.font, Text, Pos + new Vector2(2), Color.Black, 0, Vector2.Zero, Mult.Y, SpriteEffects.None, 0);
                    game.spriteBatch.DrawString(game.Loader.font, Text, Pos, Color.White, 0, Vector2.Zero, Mult.Y , SpriteEffects.None, 0);



                    Color Col;

                    Col = Color.Black;
                    HUDRECT.Height = (int)(450 * Mult.Y * BuyWindowSize);
                    HUDRECT.Width = (int)(800 * Mult.X * BuyWindowSize);
                    HUDRECT.Y = (int)(PlayerPort.Height/2 - 300  * Mult.Y * BuyWindowSize);
                    HUDRECT.X = (int)(PlayerPort.Width/2 - 400 * Mult.X * BuyWindowSize);
                    game.spriteBatch.Draw(game.Loader.MenuWindow, HUDRECT, Col);
                    game.spriteBatch.Draw(game.Loader.MenuWindow, HUDRECT, Col);
                    game.spriteBatch.Draw(game.Loader.MenuWindow, HUDRECT, Col);
                    Col = Color.White;
                    game.spriteBatch.Draw(game.Loader.MenuWindow, HUDRECT, Col);

                    int YY = -(int)(400*Mult.Y);
                    for (int b = 0; b < 2; b++)
                    {
                        YY += (int)(200 * Mult.Y);

                        for (int i = 0; i < 4; i++)
                        {
                            if (!UnLocked[i,b])
                                Col = new Color(0.6f, 0.6f, 0.6f) * 0.75f;
                            else
                            {
                                BasicOrb Orb = game.Orbs[PlayerTarget];
                                Col = new Color(0.6f, 0.6f, 0.6f)*0.75f;
                                if (b == 0 && Orb.GunCurrent == game.U_holder.UpgradeSet[i, b] || b == 1 && Orb.Abilty[0] == game.U_holder.UpgradeSet[i, b])
                                    Col = Color.White;
                            }
                            HUDRECT.Height = (int)(200 * Mult.Y * BuyWindowSize * BuyWindowIndivualSize[i, b]);
                            HUDRECT.Width = (int)(150 * Mult.X * BuyWindowSize * BuyWindowIconSize * BuyWindowIndivualSize[i, b]);
                            HUDRECT.Y = (int)(PlayerPort.Height / 2 - 100 * BuyWindowIndivualSize[i, b] * Mult.Y * BuyWindowSize) + YY;
                            HUDRECT.X = (int)(PlayerPort.Width / 2 + (600 * i / 3 - 300 - 75 * BuyWindowIndivualSize[i, b]) * Mult.X * BuyWindowSize * BuyWindowIconSize);

                            Texture2D tex = game.Loader.Temp;

                            if (b == 0)
                                tex = game.Holder.GunIcon[game.U_holder.UpgradeSet[i, b]];
                            else
                            {
                                tex = game.A_holder.AbilityTexture[game.A_holder.Translate(game.U_holder.UpgradeSet[i, b])];
                            }
                            game.spriteBatch.Draw(tex, HUDRECT, Col);

                            Text = game.U_holder.UpgradeName[i, b];

                            float FontDiv = 1.5f;

                            game.spriteBatch.DrawString(game.Loader.font, Text, new Vector2((int)(HUDRECT.X + 75 * BuyWindowIndivualSize[i, b] * Mult.X * BuyWindowSize * BuyWindowIconSize) - game.Loader.font.MeasureString(Text).X / (2*FontDiv) * Mult.X, (int)(PlayerPort.Height / 2) + YY+50*Mult.Y + game.Loader.font.MeasureString(Text).Y * Mult.Y/FontDiv)+new Vector2(1), Color.Black, 0, Vector2.Zero, Mult/ FontDiv, SpriteEffects.None, 0);
                            game.spriteBatch.DrawString(game.Loader.font, Text, new Vector2((int)(HUDRECT.X + 75 * BuyWindowIndivualSize[i, b] * Mult.X * BuyWindowSize * BuyWindowIconSize) - game.Loader.font.MeasureString(Text).X / (2 * FontDiv) * Mult.X, (int)(PlayerPort.Height / 2) + YY + 50 * Mult.Y + game.Loader.font.MeasureString(Text).Y * Mult.Y / FontDiv), Color.White, 0, Vector2.Zero, Mult / FontDiv, SpriteEffects.None, 0);
                            
                            Text = game.U_holder.UpgradeCost[i, b].ToString();

                            if (XAlpha > 0)
                                if (XPos.X == i && XPos.Y == b)
                                    game.spriteBatch.Draw(game.Loader.XTexture, HUDRECT, new Color(Vector4.One*XAlpha));

                           // game.spriteBatch.DrawString(game.Loader.font, Text, new Vector2((int)(HUDRECT.X + 75 * BuyWindowIndivualSize[i, b] * Mult.X * BuyWindowSize * BuyWindowIconSize) - game.Loader.font.MeasureString(Text).X / 2, (int)(PlayerPort.Height / 2 - 45 * Mult.Y) + YY), Color.White);

                        }
                    }
                    if (UnLocked[Math.Max(0, BuyWindowX - 1), BuyWindowStage])
                        Text = game.U_holder.UpgradeDescription[BuyWindowX, BuyWindowStage].Replace("/n", "\n");
                    else
                        Text = "Purcahse Previous Upgrades Before Buying this One";
                    Pos= new Vector2(PlayerPort.Width/2-game.Loader.font.MeasureString(Text).X/2*Mult.Y,PlayerPort.Height*0.7f);

                    game.spriteBatch.DrawString(game.Loader.font, Text,Pos+new Vector2(2), Color.Black, 0, Vector2.Zero, Mult.Y, SpriteEffects.None, 0);
                    game.spriteBatch.DrawString(game.Loader.font, Text,Pos, Color.White, 0, Vector2.Zero, Mult.Y, SpriteEffects.None, 0);

                  //  game.spriteBatch.End();
                }
                
            }

            public void DrawTeamScreen(Game1 game, Vector2 Mult)
            {
                game.DrawFullscreenQuad(game.Loader.Temp,this, BlendState.AlphaBlend, null, new Color(0, 0, 0, TeamSelectAlpha*0.5f));

               // game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);



                
                if (TeamSelectAlpha > 0)
                {


                    Color Col;

                    HUDRECT.Height = (int)(400 * Mult.Y);
                    HUDRECT.Width = (int)(800 * Mult.X);
                    HUDRECT.Y = (int)(PlayerPort.Height / 2 - 200 * Mult.Y);
                    HUDRECT.X = (int)(PlayerPort.Width / 2 - 400 * Mult.X);
                    game.spriteBatch.Draw(game.Loader.MenuWindow, HUDRECT, new Color(0, 0, 0, TeamSelectAlpha));

                    HUDRECT.Height = (int)(400 * Mult.Y);
                    HUDRECT.Width = (int)(800 * Mult.X);
                    HUDRECT.Y = (int)(PlayerPort.Height / 2 - 200 * Mult.Y);
                    HUDRECT.X = (int)(PlayerPort.Width / 2 - 400 * Mult.X);
                    game.spriteBatch.Draw(game.Loader.MenuWindow, HUDRECT, new Color(1, 1, 1, TeamSelectAlpha));

                    if (game.gamemode == Game1.GameMode.TeamDeathMatch)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            string Text = "Select Your Team";
                            game.spriteBatch.DrawString(game.Loader.font, Text, new Vector2(PlayerPort.Width / 2 - game.Loader.font.MeasureString(Text).X / 2 * Mult.X, PlayerPort.Height / 2 - 250 * Mult.Y), Color.White, 0, Vector2.Zero, Mult.X, SpriteEffects.None, 0);


                            if (TeamSelectY == i)
                            {
                                if (i == 0)
                                    Col = new Color(1, 0, 0, TeamSelectAlpha);
                                else
                                    Col = new Color(0, 0, 1, TeamSelectAlpha);
                            }
                            else Col = new Color(0.2f, 0.2f, 0.2f, TeamSelectAlpha);

                            HUDRECT.Height = (int)(300 * Mult.Y);
                            HUDRECT.Width = (int)(300 * Mult.Y);
                            HUDRECT.Y = (int)(PlayerPort.Height / 2 - 150 * Mult.Y);
                            HUDRECT.X = (int)(PlayerPort.Width / 2 + ((500 * i / 1 -450) * Mult.Y));
                            game.spriteBatch.Draw(game.Loader.TeamTexture, HUDRECT, Col);

                            Text = "";

                            if (i == 0)
                                Text = "Red Team";
                            else
                                Text = "Blue Team";

                            game.spriteBatch.DrawString(game.Loader.font, Text, new Vector2((int)(HUDRECT.X+150*Mult.Y) - game.Loader.font.MeasureString(Text).X / 2, (int)(PlayerPort.Height / 2)), Color.White);
                            //Text = game.U_holder.UpgradeCost[i, b].ToString();
                            // game.spriteBatch.DrawString(game.Loader.font, Text, new Vector2((int)(HUDRECT.X + 75 * BuyWindowIndivualSize[i, b] * Mult.X * BuyWindowSize * BuyWindowIconSize) - game.Loader.font.MeasureString(Text).X / 2, (int)(PlayerPort.Height / 2 - 45 * Mult.Y) + YY), Color.White);

                        }
                    }
                    else
                    {
                        string Text="Select Your Color";
                        game.spriteBatch.DrawString(game.Loader.font, Text, new Vector2(PlayerPort.Width / 2 - game.Loader.font.MeasureString(Text).X/2 * Mult.X, PlayerPort.Height / 2 - 250 * Mult.Y), Color.White, 0, Vector2.Zero, Mult.X, SpriteEffects.None, 0);

                        int YY = -(int)((150+75/2) * Mult.Y);
                        for (int b = 0; b < 2; b++)
                        {
                            YY += (int)(150 * Mult.Y);

                            for (int i = 0; i < 4; i++)
                            {
                                Vector4 ColVec = game.C_holder.colorVecs[i + b * 4]*TeamSelectAlpha;
                                if (i != TeamSelectY || b != TeamSelectX)
                                    ColVec *= 0.4f;
                                

                                Col = new Color(ColVec);
                                HUDRECT.Height = (int)(125 * Mult.Y);
                                HUDRECT.Width = (int)(125 * Mult.X);
                                HUDRECT.Y = (int)(PlayerPort.Height / 2 - 100 *Mult.Y) + YY;
                                HUDRECT.X = (int)(PlayerPort.Width / 2 + (500 * i / 3 - 250 - 75 ) * Mult.X);
                                game.spriteBatch.Draw(game.Loader.ColorBox, HUDRECT, Col);

                               // string Text = game.C_holder.

                                //game.spriteBatch.DrawString(game.Loader.font, Text, new Vector2((int)(HUDRECT.X + 75 * BuyWindowIndivualSize[i, b] * Mult.X * BuyWindowSize * BuyWindowIconSize) - game.Loader.font.MeasureString(Text).X / 2, (int)(PlayerPort.Height / 2) + YY), Color.White);
                                //Text = game.U_holder.UpgradeCost[i, b].ToString();
                                // game.spriteBatch.DrawString(game.Loader.font, Text, new Vector2((int)(HUDRECT.X + 75 * BuyWindowIndivualSize[i, b] * Mult.X * BuyWindowSize * BuyWindowIconSize) - game.Loader.font.MeasureString(Text).X / 2, (int)(PlayerPort.Height / 2 - 45 * Mult.Y) + YY), Color.White);

                            }
                        }
                    }

                }
                //game.spriteBatch.End();
            }

            public void DrawPauseScreen(Game1 game, Vector2 Mult)
            {
                if (PauseWindowAlpha > 0)
                    game.DrawFullscreenQuad(game.Loader.Temp, this, BlendState.AlphaBlend, null, new Color(0, 0, 0, PauseWindowAlpha * 0.6f));

              //  game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                if (PauseWindowAlpha > 0)
                {

                    
                    HUDRECT.Height = (int)(425*Mult.Y);
                    HUDRECT.Width = (int)(250*Mult.X);
                    HUDRECT.Y = (int)(PlayerPort.Height/2-150*Mult.Y);
                    HUDRECT.X = (int)(PlayerPort.Width/2-125*Mult.X);
                    game.spriteBatch.Draw(game.Loader.MenuWindow, HUDRECT, Color.White);


                    int YY = -(int)(200 * Mult.Y);

                        for (int i = 0; i < 4; i++)
                        {
                            YY += (int)(400 / 4 * Mult.Y);

                            string Text = "";
                            if (i == 0)
                                Text = "resume game";
                            if (i == 1)
                            {
                                if (game.gamemode == Game1.GameMode.TeamDeathMatch)
                                    Text = "change teams";
                                else
                                    Text = "change color";

                            }
                            if (i == 2)
                                Text = "sensitivy: "+LookSensitivity.ToString();
                            if (i == 3)
                                Text = "end game";

                            Vector2 Pos = new Vector2((int)(PlayerPort.Width / 2) - game.Loader.font.MeasureString(Text).X / 2*Mult.X, (int)(PlayerPort.Height / 2) + YY);
                            Color Col;

                            Vector4 vec = new Vector4(0.4f, 0.4f, 1, 1) * PauseWindowIndividualAlpha[i] * PauseWindowAlpha;

                            Col = new Color(vec);
                            HUDRECT.Height = (int)((game.Loader.font.MeasureString(Text).Y + 20) * Mult.X);
                            HUDRECT.Width = (int)((game.Loader.font.MeasureString(Text).X+10)*Mult.X);
                            HUDRECT.Y = (int)(Pos.Y-10*Mult.Y);
                            HUDRECT.X = (int)(Pos.X - 5 * Mult.X);
                            game.spriteBatch.Draw(game.Loader.Temp, HUDRECT, Col);


                            Color col;
                            col = new Color(1, 1, 1, 0.75f + 0.25f * PauseWindowIndividualAlpha[i] * PauseWindowAlpha);


                                   // col = new Color(0.25f, 0.25f, 0.25f, 1) * PauseWindowIndividualAlpha[i] * PauseWindowAlpha;

                            game.spriteBatch.DrawString(game.Loader.font, Text, Pos, col,0,Vector2.Zero,Mult.X,SpriteEffects.None,0);

                        }
                    


                }
               // game.spriteBatch.End();
            }

            public void SetAlphaText(string Text)
            {
                if (AlphaTextAlpha < 0 || Text != "+25 Energy")
                {
                    AlphaTextAlpha = 1;
                    AlphaText = Text;
                }
            }

            public void DrawMessages(Game1 game,Vector2 Mult)
            {
                string Text = "";

                Text = AlphaText;

                float TextMult = 1.5f;
                Vector2 Pos = new Vector2((int)(PortSize.X / 2) - game.Loader.font.MeasureString(Text).X * Mult.X/2 * TextMult, (int)(PlayerPort.Height * 0.7f)) + new Vector2(2, 2);

           

                game.spriteBatch.DrawString(game.Loader.font, Text, Pos + new Vector2(2), new Color(new Vector4(0, 0, 0, 1) * AlphaTextAlpha), 0, Vector2.Zero, Mult.X * TextMult, SpriteEffects.None, 0);

                game.spriteBatch.DrawString(game.Loader.font, Text, Pos, new Color(Vector4.One * AlphaTextAlpha), 0, Vector2.Zero, Mult.X * TextMult, SpriteEffects.None, 0);


                float AVGMult = (Mult.X + Mult.Y) / 2;
                float YY = 180 * Mult.Y;
                for (int i = maxMessages-1; i >-1; i--)
                if(MessageAlpha[i]*(1-ScoreWindowAlpha)>0)
                {
                    YY -= 30 * Mult.Y;
                    

                    if (MessageType[i] == 0)
                        Text = "You Killed Yourself";
                    if (MessageType[i] == 1)
                        Text = "You Were Killed By Comp";
                    if (MessageType[i] == 2)
                        Text = "You Killed Comp";
                    if (MessageType[i] == 3)
                        Text = "You Were Killed By " + MessageTarget[i].ToString();
                    if (MessageType[i] == 4)
                        Text = "You Killed " + MessageTarget[i].ToString();
                    if (MessageType[i] == 5)
                        Text = "Assist";
                    if (MessageType[i] == 6)
                        Text = "You Teamkilled " + game.Orbs[MessageTarget[i] - 1].MyController.Name;
                    if (MessageType[i] == 7)
                        Text = "You Were Teamkilled by " + game.Orbs[MessageTarget[i] - 1].MyController.Name;

                    if (MessageType[i] == 1 || MessageType[i] ==3)
                        Text = "You Were Killed By " + game.Orbs[MessageTarget[i] - 1].MyController.Name;
                    if (MessageType[i] == 2|| MessageType[i] == 4)
                        Text = "You Killed " + game.Orbs[MessageTarget[i] - 1].MyController.Name;

                    if (MessageType[i] == 8)
                        Text = "Gun Changed!";
                    if (MessageType[i] == 9)
                        Text = "Ability Changed!";

                    MessageAlpha[i] -= 0.01f;

                    Color col=Color.White;
                    //if(MessageType[i]!=5)
                    if(false)
                    col = new Color(game.Orbs[MessageTarget[i] - 1].MyController.colorVec * MessageAlpha[i] * (1 - ScoreWindowAlpha));
                    else
                        col = new Color(Vector4.One * MessageAlpha[i] * (1 - ScoreWindowAlpha));

                     Pos=new Vector2((int)(PortSize.X/2)-game.Loader.font.MeasureString(Text).X/2*Mult.X, (int)(YY))+new Vector2(2,2);
                    game.spriteBatch.DrawString(game.Loader.font, Text, Pos,new Color(new Vector4(0,0,0,1)*(1-ScoreWindowAlpha)*MessageAlpha[i]),0,Vector2.Zero,Mult.X,SpriteEffects.None,0);

                            Pos=new Vector2((int)(PortSize.X / 2) - game.Loader.font.MeasureString(Text).X / 2*Mult.X,(int)(YY));
                    game.spriteBatch.DrawString(game.Loader.font, Text, Pos,col, 0, Vector2.Zero, Mult.X, SpriteEffects.None, 0);

                
                }
            }

            public void DrawHealthBars(Game1 game, Vector2 Mult)
            {
                BasicOrb MyOrb3 = game.Orbs[PlayerTarget];
                //if(false)

                bool CloakDetected = false;


                foreach (BasicOrb orb in game.Orbs)
                    if (orb.relevent && orb != game.Orbs[PlayerTarget])
                    {
                        if (orb.CloakTime > 0)
                            if(Vector3.Distance(orb.Position,MyOrb3.Position)<1200)
                            CloakDetected = true;

                        if (orb.Alpha > 0)
                        {
                            Color Col;
                            Vector3 PlayerScreenPos = PlayerPort.Project(Vector3.Zero, playerProjection, playerView, Matrix.CreateTranslation(orb.Position));
                            PlayerScreenPos -= new Vector3(PlayerPort.X, PlayerPort.Y, 0);

                            if (orb.Alive)
                            {

                                Col = new Color(0, 0, 0, orb.Alpha);
                                HUDRECT.Height = (int)(20 * Mult.Y);
                                HUDRECT.Width = (int)(60 * Mult.X);
                                HUDRECT.Y = (int)(PlayerScreenPos.Y - 80 * Mult.Y);
                                HUDRECT.X = (int)(PlayerScreenPos.X - 30 * Mult.X);
                                game.spriteBatch.Draw(game.Loader.Temp, HUDRECT, Col);


                                HUDRECT.Height -= 2;
                                HUDRECT.Y += 1;
                                HUDRECT.Width -= 2;
                                HUDRECT.X += 1;


                                // if (orb.Team != MyOrb3.Team)
                                // Col = Color.Red;
                                //  else
                                //    Col = new Color(0, 0.5f, 1);
                                if (game.gamemode != Game1.GameMode.TeamDeathMatch)
                                    Col = new Color(orb.MyController.colorVec * orb.Alpha);
                                else
                                {
                                    if (orb.Team == 0)
                                        Col = new Color(1, 0, 0, orb.Alpha);
                                    else
                                        Col = new Color(0, 0, 1, orb.Alpha);
                                }
                                // Col = new Color(1, 0.5f, 0.5f);
                                HUDRECT.Width = (int)((int)(60 * Mult.X) * orb.life / orb.MaxLife);
                                game.spriteBatch.Draw(game.Loader.Temp, HUDRECT, Col);


                                if (MyOrb3.AutoAiming)
                                    if (MyOrb3.AutoAimOrb == orb)
                                    {

                                        float Val = 100;
                                        HUDRECT.Height = (int)(Val * Mult.Y);
                                        HUDRECT.Width = (int)(Val * Mult.Y);
                                        HUDRECT.Y = (int)(PlayerScreenPos.Y - Val / 2 * Mult.Y);
                                        HUDRECT.X = (int)(PlayerScreenPos.X - Val / 2 * Mult.Y);
                                        game.spriteBatch.Draw(game.Loader.AimerTexture, HUDRECT, Col);

                                        string Text = orb.MyController.Name;

                                        game.spriteBatch.DrawString(game.Loader.font, Text, new Vector2(PlayerScreenPos.X - game.Loader.font.MeasureString(Text).X / 2 * Mult.X, PlayerScreenPos.Y - 150 * Mult.Y), Col, 0, Vector2.Zero, Mult.X, SpriteEffects.None, 0);

                                    }
                            }
                            else if (!orb.FirstSpawn)
                            {
                                if (game.gamemode != Game1.GameMode.TeamDeathMatch)
                                    Col = new Color(orb.MyController.colorVec * 0.5f);
                                else
                                {
                                    if (orb.Team == 0)
                                        Col = new Color(0.5f, 0, 0, 0.35f);
                                    else
                                        Col = new Color(0, 0, 0.5f, 0.35f);
                                }
                                HUDRECT.Height = (int)(100 * Mult.Y);
                                HUDRECT.Width = (int)(100 * Mult.Y);
                                HUDRECT.Y = (int)(PlayerScreenPos.Y - 50 * Mult.Y);
                                HUDRECT.X = (int)(PlayerScreenPos.X - 50 * Mult.Y);
                                game.spriteBatch.Draw(game.Loader.DeadTexture, HUDRECT, Col);

                            }
                        }
                    }

                if (CloakDetected&&MyOrb3.CloakTime<1&&MyOrb3.Alive)
                {
                    float Val = 600;
                    HUDRECT.Height = (int)(Val * Mult.Y);
                    HUDRECT.Width = (int)(Val * Mult.Y);
                    HUDRECT.Y = (int)(PlayerPort.Height/2 + Val/2 * Mult.Y);
                    HUDRECT.X = (int)(PlayerPort.Width/2 - Val / 2 * Mult.Y);
                    game.spriteBatch.Draw(game.Loader.CloakWarning, HUDRECT, Color.White);


                }

                foreach (NPC npc in game.Npcs)
                    if (npc.Relevent&&npc.Type==2)
                        //if (orb.Alpha > 0)
                        {
                            Color Col;
                            Vector3 PlayerScreenPos = PlayerPort.Project(Vector3.Zero, playerProjection, playerView, Matrix.CreateTranslation(npc.Position));
                                
                           // if (orb.Alive)
                            {
                                //PlayerScreenPos = Vector3.Transform(orb.Position, playerProjection * playerView);
                                PlayerScreenPos -= new Vector3(PlayerPort.X, PlayerPort.Y, 0);
                                Col = new Color(0, 0, 0,1);
                                HUDRECT.Height = (int)(15 * Mult.Y);
                                HUDRECT.Width = (int)(40 * Mult.X);
                                HUDRECT.Y = (int)(PlayerScreenPos.Y - 60 * Mult.Y);
                                HUDRECT.X = (int)(PlayerScreenPos.X - 20 * Mult.X);
                                game.spriteBatch.Draw(game.Loader.Temp, HUDRECT, Col);


                                HUDRECT.Height -= 2;
                                HUDRECT.Y += 1;
                                HUDRECT.Width -= 2;
                                HUDRECT.X += 1;

                                BasicOrb orb =npc.creator;
                                // if (orb.Team != MyOrb3.Team)
                                // Col = Color.Red;
                                //  else
                                //    Col = new Color(0, 0.5f, 1);
                                if (game.gamemode != Game1.GameMode.TeamDeathMatch)
                                    Col = new Color(npc.creator.MyController.colorVec * orb.Alpha);
                                else
                                {
                                    if (orb.Team == 0)
                                        Col = new Color(1, 0, 0,1);
                                    else
                                        Col = new Color(0, 0, 1, 1);
                                }
                                // Col = new Color(1, 0.5f, 0.5f);

                                if (npc.TurretShots > 0)
                                    HUDRECT.Width = (int)((int)(40 * Mult.X) * (npc.TurretShots) / npc.TurretMaxShots);
                                else
                                    HUDRECT.Width = (int)(40 * Mult.X);

                                game.spriteBatch.Draw(game.Loader.Temp, HUDRECT, Col);




                            }

                        }
            }
            public void AddMessage(Game1 game,int messagetype,int messagetarget)
            {
                bool SetMessage = false;
                 for (int i = 0; i < MaxMessages; i++)
                     if(!SetMessage)
                     if (MessageAlpha[i] < 0.01)
                     {
                         SetMessage = true;
                         MessageType[i] = messagetype;
                         MessageTarget[i] = messagetarget;
                         MessageAlpha[i] = 2.5f;
  
                     }
            }

            public void DrawInGameObjects(Game1 game, int Temp)
            {

                game.GraphicsDevice.DepthStencilState = DepthStencilState.None;
                if (game.gamemode != Game1.GameMode.KeepAway)
                {
                    foreach (BasicOrb orb in game.Orbs)
                        if (orb.relevent && orb.Alive)
                            if (orb.Alpha > 0)
                            {
                                if (orb.Team != game.Orbs[PlayerTarget].Team)
                                {
                                    float TempDire = -(float)Math.Atan2(orb.Position.X - game.Orbs[PlayerTarget].Position.X, orb.Position.Z - game.Orbs[PlayerTarget].Position.Z) + MathHelper.ToRadians(270);

                                    float TorsoMove = 0;
                                    if (orb.ShootTime > 20)
                                        TorsoMove = -MathHelper.ToRadians(orb.LegRot / 8);

                                    Vector3 vec3 = new Vector3(orb.MyController.colorVec.X, orb.MyController.colorVec.Y, orb.MyController.colorVec.Z);
                                    BasicOrb MyOrb = game.Orbs[PlayerTarget];
                                    //  if (orb.Visible[Temp - 1])
                                    //      if (orb.Alpha > 0)
                                    //         DrawObject(game, game.Loader.PlayerRing, orb.Position + new Vector3(0, 0, 0), vec3 * orb.Alpha, orb.Rotation + new Vector3(0,TorsoMove,0), 1);
                                    float Distance = 0;
                                    float Angle = 0;
                                    if (game.gamemode != Game1.GameMode.Assasin && !game.Orbs[PlayerTarget].IsAssasin)
                                    {
                                        Angle = MathHelper.ToDegrees(Math.Min(Math.Abs(MyOrb.Rotation.Y - TempDire), Math.Abs((MyOrb.Rotation.Y - TempDire) + MathHelper.ToRadians(360))));
                                        Vector3.Distance(MyOrb.Position, orb.Position);
                                        Distance = Vector3.Distance(MyOrb.Position, orb.Position);

                                    }
                                    else
                                    {
                                        Distance = 600;
                                        Angle = 150;
                                    }

                                    if (Distance < 2400)
                                        if (Angle > 90)
                                        {

                                            DrawObject2(game, game.Loader.PlayerArrow, //model

                                                game.Orbs[PlayerTarget].Position - Vector3.Normalize(game.Orbs[PlayerTarget].Position - orb.Position) * Math.Min(200, Distance), //position
                                                orb.MyController.colorVec * MathHelper.Clamp((2400 - Distance) / 800, 0, 1) * MathHelper.Clamp((Angle - 90) / 30, 0, 1) * MathHelper.Clamp((2400 - Distance) / 800, 0, 1) * MathHelper.Clamp((Angle - 90) / 30, 0, 1),//MathHelper.Clamp((2000 - Vector3.Distance(game.Orbs[PlayerTarget].Position, orb.Position)) / 400, 0, 1) * MathHelper.Clamp((Vector3.Distance(game.Orbs[PlayerTarget].Position, orb.Position) - 400) / 400, 0, 1)), //color
                                                new Vector3(0, TempDire, 0), Vector3.One); //rotation
                                            if (false)
                                                DrawObject2(game, game.Loader.TorusModel, //model

                                                    game.Orbs[PlayerTarget].Position - Vector3.Normalize(game.Orbs[PlayerTarget].Position - orb.Position) * Math.Min(150 + Distance / 2400 * 100, Distance), //position
                                                    new Vector4(1, 0.5f, 0.25f, MathHelper.Clamp((2400 - Distance) / 800, 0, 1) * MathHelper.Clamp((Angle - 90) / 30, 0, 1)) * MathHelper.Clamp((2400 - Distance) / 800, 0, 1) * MathHelper.Clamp((Angle - 90) / 30, 0, 1),//MathHelper.Clamp((2000 - Vector3.Distance(game.Orbs[PlayerTarget].Position, orb.Position)) / 400, 0, 1) * MathHelper.Clamp((Vector3.Distance(game.Orbs[PlayerTarget].Position, orb.Position) - 400) / 400, 0, 1)), //color
                                                    new Vector3(0, TempDire, 0), Vector3.One);
                                        }
                                }
                                else if (false)
                                    if (orb.Visible[Temp - 1])
                                        if (orb.Alpha > 0)
                                        {



                                            BasicOrb MyOrb = game.Orbs[PlayerTarget];
                                            float TempDire = -(float)Math.Atan2(orb.Position.X - game.Orbs[PlayerTarget].Position.X, orb.Position.Z - game.Orbs[PlayerTarget].Position.Z) + MathHelper.ToRadians(270);


                                            float TorsoMove = 0;
                                            if (orb.ShootTime > 20)
                                                TorsoMove = -MathHelper.ToRadians(orb.LegRot / 8);


                                            float Distance = Vector3.Distance(MyOrb.Position, orb.Position);
                                            float Angle = MathHelper.ToDegrees(Math.Min(Math.Abs(MyOrb.Rotation.Y - TempDire), Math.Abs((MyOrb.Rotation.Y - TempDire) + MathHelper.ToRadians(360))));
                                            if (Distance < 2400)
                                            // if (Angle > 90)
                                            {

                                                DrawObject2(game, game.Loader.PlayerArrow, //model

                                                    game.Orbs[PlayerTarget].Position - Vector3.Normalize(game.Orbs[PlayerTarget].Position - orb.Position) * Math.Min(200, Distance), //position
                                                    orb.MyController.colorVec,//MathHelper.Clamp((2000 - Vector3.Distance(game.Orbs[PlayerTarget].Position, orb.Position)) / 400, 0, 1) * MathHelper.Clamp((Vector3.Distance(game.Orbs[PlayerTarget].Position, orb.Position) - 400) / 400, 0, 1)), //color
                                                    new Vector3(0, TempDire, 0), Vector3.One); //rotation
                                                if (false)
                                                    DrawObject2(game, game.Loader.TorusModel, //model

                                                        game.Orbs[PlayerTarget].Position - Vector3.Normalize(game.Orbs[PlayerTarget].Position - orb.Position) * Math.Min(150 + Distance / 2400 * 100, Distance), //position
                                                        new Vector4(1, 0.5f, 0.25f, MathHelper.Clamp((2400 - Distance) / 800, 0, 1) * MathHelper.Clamp((Angle - 90) / 30, 0, 1)) * MathHelper.Clamp((2400 - Distance) / 800, 0, 1) * MathHelper.Clamp((Angle - 90) / 30, 0, 1),//MathHelper.Clamp((2000 - Vector3.Distance(game.Orbs[PlayerTarget].Position, orb.Position)) / 400, 0, 1) * MathHelper.Clamp((Vector3.Distance(game.Orbs[PlayerTarget].Position, orb.Position) - 400) / 400, 0, 1)), //color
                                                        new Vector3(0, TempDire, 0), Vector3.One);
                                            }

                                            // if(orb==game.Orbs[PlayerTarget])
                                            //     DrawObject(game, game.Loader.PlayerRing, orb.Position + new Vector3(0, 0, 0), new Vector3(orb.MyController.colorVec.X, orb.MyController.colorVec.Y, orb.MyController.colorVec.Z) * orb.Alpha, orb.Rotation + new Vector3(0, TorsoMove, 0), 1);

                                            // else
                                            //    DrawObject(game, game.Loader.PlayerRing, orb.Position + new Vector3(0, 0, 0), new Vector3(orb.MyController.colorVec.X, orb.MyController.colorVec.Y, orb.MyController.colorVec.Z) * orb.Alpha, orb.Rotation + new Vector3(0, TorsoMove, 0), 1);
                                        }
                                //else
                                //   DrawObject(game, game.Loader.PlayerRing, orb.ProjectedPosition + new Vector3(0, 0, 0), new Vector3(0.55f, 0f, 0f), Vector3.Zero);

                            }


                    foreach (NPC orb in game.Npcs)
                        if (orb.Relevent && orb.Alive && orb.Type == 2)
                        {
                            if (orb.creator.Team != game.Orbs[PlayerTarget].Team)
                            {
                                float TempDire = -(float)Math.Atan2(orb.Position.X - game.Orbs[PlayerTarget].Position.X, orb.Position.Z - game.Orbs[PlayerTarget].Position.Z) + MathHelper.ToRadians(270);

                                BasicOrb MyOrb = game.Orbs[PlayerTarget];
                                if (orb.Visible[Temp - 1])
                                    DrawObject(game, game.Loader.TurretModel2, orb.Position + new Vector3(0, 0, 0), new Vector3(1, 0f, 0f), orb.Rotation, 0.5f);

                            }
                            else
                                if (orb.Visible[Temp - 1])
                                    DrawObject(game, game.Loader.TurretModel2, orb.Position + new Vector3(0, 0, 0), new Vector3(0f, 0f, 1), orb.Rotation, 0.5f);

                            //else
                            //   DrawObject(game, game.Loader.PlayerRing, orb.ProjectedPosition + new Vector3(0, 0, 0), new Vector3(0.55f, 0f, 0f), Vector3.Zero);

                        }

                }
                else if(!game.flag.IsCarried||game.flag.carrier!=game.Orbs[PlayerTarget])
                {
                    

                    float TempDire = -(float)Math.Atan2(game.flag.Position.X - game.Orbs[PlayerTarget].Position.X, game.flag.Position.Z - game.Orbs[PlayerTarget].Position.Z) + MathHelper.ToRadians(270);

                    Vector4 Col = Vector4.One;
                    if (game.flag.IsCarried)
                        Col = game.flag.carrier.MyController.colorVec;

                    DrawObject2(game, game.Loader.PlayerArrow, game.Orbs[PlayerTarget].Position - Vector3.Normalize(game.Orbs[PlayerTarget].Position - game.flag.Position) * 200,Col, //position 1   ,//MathHelper.Clamp((2000 - Vector3.Distance(game.Orbs[PlayerTarget].Position, orb.Position)) / 400, 0, 1) * MathHelper.Clamp((Vector3.Distance(game.Orbs[PlayerTarget].Position, orb.Position) - 400) / 400, 0, 1)), //color
                                               new Vector3(0, TempDire, 0), Vector3.One);
                }

                game.GraphicsDevice.BlendState=BlendState.Additive;
               // if(false)
                foreach(BasicOrb orb in game.Orbs)
                if (orb.RailTargetTime > 0)
                {
                    BasicOrb MyOrb2 = orb;
                    if(orb.AutoAimOrb.Visible[PlayerNumber-1])
                    DrawObject2(game, game.Loader.AimerModel, MyOrb2.AutoAimOrb.Position, new Vector4(1f, 0, 0, 1)*MyOrb2.RailTargetTime, Vector3.Zero,Vector3.One*(2-MyOrb2.RailTargetTime));
                }

                game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            }

            public void DrawObject(Game1 game,Model mod,Vector3 pos, Vector3 col,Vector3 Rot,float Scale)
            {
                game.GraphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
                game.GraphicsDevice.BlendState = BlendState.AlphaBlend;
                foreach (ModelMesh mesh in mod.Meshes)
                {
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        

                        Matrix world = Matrix.CreateScale(Scale)* Matrix.CreateFromYawPitchRoll(-Rot.Y, Rot.X, Rot.Z)* Matrix.CreateTranslation(pos) ;
                        Matrix PosOnly = Matrix.CreateScale(Scale) * Matrix.CreateFromYawPitchRoll(Rot.Y, Rot.X, Rot.Z) * Matrix.CreateTranslation(pos);
                        part.Effect = game.Loader.Fresnel;
                        Effect effect = game.Loader.Fresnel;
                        effect.Parameters["World"].SetValue(world);
                        effect.Parameters["PosOnly"].SetValue(PosOnly);
                        effect.Parameters["View"].SetValue(playerView);
                        effect.Parameters["Projection"].SetValue(playerProjection);
                        effect.Parameters["Color"].SetValue(new Vector4(col,0));
                        effect.Parameters["CameraPosition"].SetValue(new Vector4(CameraPosHalf, 0));
                    }
                    mesh.Draw();
                }
            }

            public void DrawObject2(Game1 game, Model mod, Vector3 pos, Vector4 col, Vector3 Rot,Vector3 Scale)
            {
                //game.GraphicsDevice.BlendState = BlendState.AlphaBlend;
                foreach (ModelMesh mesh in mod.Meshes)
                {
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        Matrix world = Matrix.CreateScale(Scale)*Matrix.CreateFromYawPitchRoll(-Rot.Y, Rot.X, Rot.Z) * Matrix.CreateTranslation(pos);
                        part.Effect = game.Loader.Ambient;
                        Effect effect = game.Loader.Ambient;
                        effect.Parameters["World"].SetValue(world);
                        effect.Parameters["View"].SetValue(playerView);
                        effect.Parameters["Projection"].SetValue(playerProjection);
                        effect.Parameters["Color"].SetValue(col);
                        //effect.Parameters["ViewPosition"].SetValue(new Vector4(CameraPos, 0));
                    }
                    mesh.Draw();
                }
                
            }

            public void Reset(Game1 game)
            {
                VisibleLights = new List<LightObject>();
                HasSaidTied = true;
                HasSaidLeading = false;
                Taken = false;
                TeamSelectIsOpen = false;
                TeamSelectAlpha = 0;
                TeamSelectY = 0;
                TeamSelectX = 0;
                Updateddeaths = 0;
                UpdatedKills = 0;
        TryingToSave = false;
        Currentplayer = PlayerIndex.One;
        HasLived = false;
        DamageAlpha = 0;
        FoundSaveDevice = false;
       LevelSave=null;
       SelectorWindowIsOpen = false;
         Mode = "Spectate";
           IsXboxController = true;
           PlayerTarget = 0;
          Relevent = false;
        SwitchDraw = false;

        BuyWindowIsOpen = false;
        BuyWindowSize = 0;
        BuyWindowStage = 0;
        BuyWindowX = 0;

        PauseWindowY = 0;
        PauseWindowIsOpen = false;
        PauseWindowAlpha = 0;
         MessageAlpha = new float[MaxMessages];
        MessageType = new int[MaxMessages];
         MessageTarget = new int[MaxMessages];
            }
    }
}
