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
using Microsoft.Xna.Framework.Net;

namespace Orb
{
    public class BasicOrb
    {
        #region Fields

        public bool IsAssasin = false;
        public float ShootTime = 0;
        public float LegRot = 37;
        public float WalkSoundTimer = 0;
        public bool IsTakenByLocalPlayer = false;
        public float LegVel;
        float WhiteMax = 0.33f;
        public bool IsOnline = false;
        public int OldGunCurrent = -10;
        public NetworkGamer MyGamer;
        public bool HasPlayedReverseDash = false;
        public bool HasFoundLights = false;
        public int ShouldDownGradeNext = 0;
        IAudioEmitter CloakEmitter;
        public bool CloakEmitterin = false;
        public float[] LegBend = new float[2];
        public float[] TargetLegBend = new float[2];
        public Vector3 AIPrevPoint = Vector3.Zero;
        public float PrevPointTimer = 0;
        public float PhaseParticleTimer = 0;
        public float FlashAlpha = 0;
        public float WalkMax = 45;
        public float TimeFromLastMessage = 0;

        public Vector3 DrawColor=Vector3.One;

        public bool IsTakenByOnlinePlayer = false;
        public float WalkSpeed = 1;

        public float Alpha = 1;
        public BoundingSphere Boundaries;
        public Vector3 Position = Vector3.Zero;
        public bool Taken = false;
        public float AutoAimCounter = 0;
        public float AbilityCounter = 0;
        public float AbilityMaxCounter = 0;
        public float PhasePushTime = 16;
        public int MaxSpawnInv = 200;
        public float AllMoney = 0;
        public int Inv = 0;
        float OverAllPushVelMult = 0.9f;
        public bool BusySelectingTeams = false;
        public float AppearancePause = 0;
        public bool WallWalking = false;
        public int MaxPhaseinv = 10;
        public int[] ShouldBuy = new int[2];
        public float MaxNeedles = 10;
        public float CloakTime = 0;
        public float MaxPhasePushTime = 18;
        public BasicOrb AutoAimOrb;
        public float PhasePushNegative = 0;
        public float DodgeMult = 1;
        public float NeedleTime = 0;
        public float MaxNeedleTime = 450;
        public float RailTargetTime = 0;
        public bool IsAutoShooting = false;
        public BasicOrb RailTargetOrb;
        public float TargetTime = 0;
        public float ShieldDivide = 1.4f;
        public float AbilityStop = 0;
        public float AIPhaseTime = 0;
        public float SpeedBoostTime = 0;
        public float AiDodgePause = 0;
        public float MovePause = 0;
        public float SpeedBoostMult = 1.6f;
        public float HealthPackHoldTime = 0;
        public Vector3 PhaseColor = new Vector3(0.25f, 0.65f, 1f) * 4;
        public  Vector3 Rotation = Vector3.Zero;
        public bool HasDied = false;
        public float EMPTime = 0;
        public float PushVelMult = 0;
        DynamicLightObject PhaseLight;
        public bool ShouldRespawnYet = true;
        public BasicOrb LastShooter;
        public Vector2 Size = new Vector2(90, 90);
        public float PhasePause = 0;
        public float PhasePauseRepeat = 0;
        public float[] AbilityCoolDown = new float[2];
        public int LastDamager = 0;
        public bool relevent = false;
        public float WeaponPushTime = 0;
        public float life = 100f;
        public float MaxLife = 200 * LifeMult;
        public float HitBounce = 0;
        public float MaxHitBounce = 10;
        public float MaxEnergy = 100;
        public float StartingMaxEnergy = 100;
        public float Energy = 0;
        
        public int HealthPacks = 0;
        public float MaxRechargeTime = 60;
        public float MoveSpeed = 14;//*1.15f;
        public const float LifeMult = 1;
        public float MaxDamageResistance = 15*LifeMult*0;
   
        public LocalPlayer HumanController;
        public float LastShootTime = 0;
        public float MaxDamageResistance2 = 35 * LifeMult * 0;
        public LightObject[] NewDrawList = new LightObject[8];
        float AIDistance = 0;
        public BasicOrb BestAiTargetOrb;
        public float DamageResistance = 0;
        public float RechargeTime = 0;
        public float DamageResistance2 = 0;
        public float AIReaction = 0;
        public float PushTimeMult = 3;
        public int NewDrawNumb = 0;
        public Vector3 Velocity = Vector3.Zero;
        public Vector3 PushVelocity = Vector3.Zero;
        BasicOrb BestTarget;
        public Vector3 PhaseVelocity = Vector3.Zero;
        public Vector3 ProjectedPosition;
        public bool Alive = true;
        public float RespawnTime = 0;
        public bool AutoAiming = false;
        BasicOrb BestAITargetOrb;
        public BasicOrb TargetOrb;
        float TempDire;
        public float TeamImunity = 0;
        public float MaxTeamImuntiy = 45;
        public bool TeamImunityActive = false;
        public game_objects.BasicController MyController;
        DynamicLightObject FlashLight;
        public bool Respawning = false;
        public float PhaseRecharge = 0;
        Vector3 ToPosition = Vector3.Zero;
        public int MaxPhaseRecharge = 90;
        public int MaxRespawnTime = 150;
        public bool IsAI = false;
        const int GunNumb=6;
        public int GunCurrent = 1;
        public bool Falling = false;
        public bool BeenFalling = false;
        public bool inGame = true;
        public int Team = 1;
        float BestSpawnValue = 0;
        public float PhaseSpeed = 29;
        SpecialObject BestSpawnPoint;
        float BestPlayerValue = 0;
        public float pushDamage = 0;
        public float pushTime = 0;
        public float Damage = 0;
        public float pushDirection = 0;
        public float AimHelpAmount = 0;
        public float BaseAimHelpAmount = 20;
        public bool IsPhasing;
        public float PhasingDirection;
        public int MaxPhasingTime = 22;
        public float PhaseTimer = 0;
        public float TargetDire = 0;
        public bool HasFlashLight = false;
        public bool IsControlled = false;
        public int[] Abilty = new int[2];
        public float[] AbiltyCharge = new float[2];
        public bool ControllerIsHuman = false;
        public bool[] Visible = new bool[4];
        public bool IsLocal = false;
        public int ID = 0;
        public float[] DamageFrom = new float[16];
        public int PrimaryWeaponQue = 0;
        public int SecondaryWeaponQue = 0;
        public int AbilityQue = 0;

        public PlayerDrawObject PlayerTorso = new PlayerDrawObject();
        public PlayerDrawObject PlayerLeg = new PlayerDrawObject();
        public PlayerDrawObject PlayerLeg2 = new PlayerDrawObject();
        public PlayerDrawObject PlayerLowerLeg = new PlayerDrawObject();
        public PlayerDrawObject PlayerLowerLeg2 = new PlayerDrawObject();
        public PlayerDrawObject PlayerGun = new PlayerDrawObject();
        public PlayerDrawObject PlayerOutline = new PlayerDrawObject();
        public PlayerDrawObject PlayerFull = new PlayerDrawObject();

        #region AI

       // bool HoldingForGrenade = false;
        float AIGrenadeCooldown = 0;
        float AISuggestedDistance = 0;
       public float AIPhasePause = 0;
       // bool AISHouldHealthPack = false;
        Vector2 AimPoint = Vector2.Zero;
        public bool LoadedOnce = false;
        public float AiShouldntphase = 0;

        #endregion

        public bool[] HasWeapon = new bool[GunNumb];
        public float[,] ROF = new float[GunNumb,2];
        public float[,] ClipSize = new float[GunNumb, 2];
        public float[,] BurstSize = new float[GunNumb, 2];
        public float[,] ReloadTime = new float[GunNumb, 2];
        public float[,] BurstTime = new float[GunNumb, 2];
        public float[,] Ammo = new float[GunNumb, 2];
        public float[] Charge = new float[2];

        public bool FirstSpawn = true;

        #endregion

        public void  Load(Game1 game, int i, bool AsignTeam)
        {
            PlayerOutline.LoadEffectParameters(game, game.Loader.GlowLines,PlayerDrawObject.EffectType.Ambient);
            PlayerTorso.LoadEffectParameters(game, game.Loader.Torso, PlayerDrawObject.EffectType.Light);
            PlayerLeg.LoadEffectParameters(game, game.Loader.Leg, PlayerDrawObject.EffectType.Light);
            PlayerLeg2.LoadEffectParameters(game, game.Loader.Leg, PlayerDrawObject.EffectType.Light);
            PlayerLowerLeg.LoadEffectParameters(game, game.Loader.LowerLeg, PlayerDrawObject.EffectType.Light);
            PlayerLowerLeg2.LoadEffectParameters(game, game.Loader.LowerLeg2, PlayerDrawObject.EffectType.Light);

            PlayerFull.LoadEffectParameters(game, game.Loader.FullPlayerModel, PlayerDrawObject.EffectType.Light);

            FirstSpawn = true;
            Position = game.AverageBlockPosition;
            LegRot = WalkMax + 7;
            ID = i - 1;
            bool Found = false;

            if(game.onlineHandler.networkSession==null)
            {
                if (i - 1 >= game.LocalPlayerNumb)
                {
                    foreach (game_objects.AIController AI in game.Ais)
                        if (!Found)
                            if (!AI.Taken)
                            {
                                AI.Taken = true;
                                AI.MyOrb = this;
                                Found = true;
                                MyController = AI;
                                AI.Name = "Comp " + (i).ToString();
                                IsAI = true;
                            }
                }
                else
                {
                    MyController = game.Localplayers[i - 1];
                    IsAI = false;
                    game.Localplayers[i - 1].TeamSelectIsOpen = true;
                    game.Localplayers[i - 1].Taken = true;
                }
            }
            if (AsignTeam)
            {
                bool HasLocal = false;
                foreach (LocalPlayer local in game.Localplayers)
                    if (local.PlayerTarget == ID && local.Relevent)
                    {
                        HasLocal = true;
                        MyController = local;
                        local.TeamSelectIsOpen = true;
                        local.Taken = true;
                    }

                if (!HasLocal)
                {
                    Found = false;
                    foreach (game_objects.AIController AI in game.Ais)
                        if (!Found)
                            if (!AI.Taken)
                            {
                                AI.Taken = true;
                                AI.MyOrb = this;
                                Found = true;
                                MyController = AI;
                                AI.Name = "Comp " + (i).ToString();
                                IsAI = true;
                            }
                }
            }

            LoadedOnce = true;
            ShouldBuy[0] = game.random.Next(0, 3);
            ShouldBuy[1] = 3 - ShouldBuy[0];
            ShouldBuy[0]++;
            ShouldBuy[1]++;

            MaxEnergy = StartingMaxEnergy;
            Boundaries.Center = Position;
            Boundaries.Radius = 50;
            Team = i;

            Abilty[0] = 3;
            Abilty[1] = 3;
            GunCurrent = 0;
            Rotation.Y = i * 30;
            if (i < game.OrbsPlaying + 1)
                relevent = true;
            else
                relevent = false;

            if (relevent)
            {
                if (IsAI)
                {
                    if (game.gamemode == Game1.GameMode.TeamDeathMatch)
                        AsignToNewTeam(game);
                    GetColor(game);
                }
                else
                {
                    BusySelectingTeams = true;
                }
            }


            for (int d = 0; d < 2; d++)
            {
                for (int b = 0; b <= 5; b++)
                {
                    HasWeapon[b] = false;
                    ROF[b, d] = 0;
                    ClipSize[b, d] = 0;
                    BurstSize[b, d] = 0;
                    ReloadTime[b, d] = 0;
                    BurstTime[b, d] = 0;
                    Ammo[b, d] = 0;
                }
            }

            if (Game1.GameMode.DownGrade == game.gamemode)
            {
                for (int t = 0; t < 4; t++)
                    for (int b = 0; b < 2; b++)
                    {
                        MyController.UnLocked[t, b] = true;
                        if (ControllerIsHuman)
                            HumanController.UnLocked[t, b] = true;

                        Abilty[0] = 9;
                        GunCurrent = 4;
                    }
            }
            if (!game.ActiveOrbs.Contains(this))
                game.ActiveOrbs.Add(this);
            DrawColor = new Vector3((float)(Math.Max(MyController.colorVec.X, WhiteMax)),(float)(Math.Max(MyController.colorVec.Y, WhiteMax)),(float)(Math.Max(MyController.colorVec.Z, WhiteMax))) * 1.25f;
            
            return;
        }

        public void ReleaseController(Game1 game)
        {
            Die(game, false, true);
            

            if (game.gamemode == Game1.GameMode.TeamDeathMatch)
                game.NumberOnTeam[Team] -= 1;


            if (MyController != null)
            {
                if (ControllerIsHuman)
                {

                    HumanController.Reset(game);
                    HumanController.Taken = false;
                    HumanController.Relevent = false;
                    HumanController.Load(game, HumanController.ID, HumanController.MyPlayer);
                }

                MyController.Reset(game);
                MyController.Taken = false;
            }

            IsAI = false;
            IsOnline = false;
            ControllerIsHuman = false;
            IsLocal = false;
            HumanController = null;
            MyController = null;
            Reset(game);
        }

        public void ChangeToAI(Game1 game)
        {

            ReleaseController(game);

            bool Found = false;

            foreach (game_objects.AIController AI in game.Ais)
                if (!Found)
                    if (!AI.Taken)
                    {
                        AI.Taken = true;
                        AI.MyOrb = this;
                        Found = true;
                        MyController = AI;
                        AI.Name = "Comp " + (ID).ToString();
                        IsAI = true;
                    }

            Load(game, ID + 1,false);
        }

        public void ChangeToLocal(Game1 game,LocalNetworkGamer gamer)
        {
            ReleaseController(game);

            bool Found = false;

            foreach (LocalPlayer local in game.Localplayers)
                if (!Found)
                    //if(local.Currentplayer==gamer.SignedInGamer.PlayerIndex)
                        
                {
                    Found = true;
                    MyController = local;
                    IsAI = false;
                    IsOnline = false;
                    if (gamer != null)
                    {
                        local.Name = gamer.Gamertag;
                       // local.Currentplayer = gamer.SignedInGamer.PlayerIndex;
                    }
                    else
                        local.Name = "Unknown";
                    local.TeamSelectIsOpen = true;
                    local.Taken = true;
                    HumanController = local;
                    local.Relevent = true;
                    ControllerIsHuman = true;
                    local.PlayerTarget = ID;
                    IsLocal = true;
                    
                    //local.Currentplayer = PlayerIndex.One;
  
                    local.Relevent = true;
                    local.playmode = LocalPlayer.PlayMode.Play;
                }

            Load(game, ID + 1,false);
        }

        public void ChangeToOnline(Game1 game,NetworkGamer gamer)
        {
            ReleaseController(game);

            foreach (game_objects.BasicController cont in game.OnlineController)
                if (!cont.Taken)
                {
                    cont.Taken = true;
                    cont.MyOrb = this;
                    MyController = cont;
                    cont.Name = gamer.Gamertag;
                    IsOnline = true;
                    break;
                }

            Load(game, ID + 1,false);
        }

        public void AsignToNewTeam(Game1 game)
        {
            int toTeam = 0;
           if (game.random.NextDouble() > 0.55f)
                toTeam = 1;

            if (game.NumberOnTeam[toTeam] < game.NumberOnTeam[1 - toTeam])
                Team = toTeam;
           else
                Team = 1 - toTeam;
            
           // Team = toTeam;
            game.NumberOnTeam[Team] += 1;
           // if (ID == 0 || ID == 1)
           //     Team = 0;
           // else
             //   Team = 1;
        }

        public void GiveMoney(Game1 game, float Mult,BasicOrb GiveOrb)
        {
            if (game.gamemode != Game1.GameMode.Assasin && game.gamemode != Game1.GameMode.WarLord&&GiveOrb.MyController!=null)
            {
                int NumbPlayers = 0;
                int NumbBetterThanMe = 0;
                int HighestKill = 0;
                int NumbWorseThanMe = 0;
                //if (false)
                if (game.gamemode != Game1.GameMode.TeamDeathMatch)
                {
                    foreach (BasicOrb orb in game.Orbs)
                        if(orb.MyController!=null)
                        if (orb.MyController.Kills > 0)
                        {
                            NumbPlayers += 1;
                            if (orb.MyController.Kills > GiveOrb.MyController.Kills)
                                NumbBetterThanMe += 1;
                            else
                                NumbWorseThanMe += 1;
                            if (orb.MyController.Kills > HighestKill)
                                HighestKill = orb.MyController.Kills;
                        }
                }
                else
                {
                    HighestKill = Math.Max(game.TeamScore[0], game.TeamScore[1]);
                }

                float MoneyMult = 1;
                //if (GiveOrb.MyController.Kills != HighestKill)
                //     MoneyMult += 0.4f * NumbBetterThanMe;
                // MoneyMult -= 0.4f * NumbWorseThanMe;

                MoneyMult += GiveOrb.MyController.Deaths * 0.25f;
                MoneyMult -= GiveOrb.MyController.Kills * 0.25f;

                int ShouldMoney = 0;

                for (int i = 0; i < 3; i++)
                    if (HighestKill >= game.KillsToWin / 4 * (i + 1))
                    {
                        ShouldMoney += 1;
                        MoneyMult += 1f;
                    }

                //GiveOrb.MyController.Money += Math.Max(50, 50 * MoneyMult);
                //GiveOrb.MyController.Money = (float)Math.Floor((double)(GiveOrb.MyController.Money));

                if (ShouldMoney > GiveOrb.MyController.AllMoney)
                {
                    GiveOrb.MyController.Money += 1;
                    GiveOrb.MyController.AllMoney += 1;
                }
            }
        }

        public void SetTexture(Game1 game,Model mod,Texture2D texture)
        {
            foreach (ModelMesh mesh in mod.Meshes)
                foreach (ModelMeshPart part in mesh.MeshParts)
                    part.Effect.Parameters["Texture"].SetValue(texture);
        }

        public void SetMatricies(Game1 game, LocalPlayer player)
        {
            Matrix PositionMatrix = Matrix.CreateTranslation(Position);

            float TorsoMove = 0;
            if (ShootTime > 20)
                TorsoMove = -MathHelper.ToRadians(LegRot / 8);

            PlayerTorso.WorldMatrix = Matrix.CreateFromYawPitchRoll(-Rotation.Y - TorsoMove, 0, 0) *
                       PositionMatrix;

            PlayerOutline.WorldMatrix = PlayerTorso.WorldMatrix;

            PlayerFull.WorldMatrix = PlayerTorso.WorldMatrix;

            if (game.LocalPlayerNumb < 3)
            {

                PlayerLeg.WorldMatrix = Matrix.CreateFromYawPitchRoll(-Rotation.Y, 0, -MathHelper.ToRadians(LegRot)) *
                   PositionMatrix;

                PlayerLeg.WorldMatrix = Matrix.CreateScale(new Vector3(1, 1, -1)) * Matrix.CreateFromYawPitchRoll(-Rotation.Y, 0, -MathHelper.ToRadians(-LegRot)) *
                            PositionMatrix;

                PlayerLowerLeg.WorldMatrix =

        Matrix.CreateFromYawPitchRoll(0, 0, MathHelper.ToRadians(TargetLegBend[0]))

        *
        Matrix.CreateTranslation(new Vector3(0, -30, 0))
        *
        Matrix.CreateFromYawPitchRoll(0, 0, -MathHelper.ToRadians(LegRot))
        *
         Matrix.CreateFromYawPitchRoll(-Rotation.Y, 0, 0)

        *
        PositionMatrix;

                PlayerLowerLeg2.WorldMatrix =

        Matrix.CreateFromYawPitchRoll(0, 0, MathHelper.ToRadians(TargetLegBend[1]))

        *
        Matrix.CreateTranslation(new Vector3(0, -30, 0))
        *
        Matrix.CreateFromYawPitchRoll(0, 0, MathHelper.ToRadians(LegRot))
        *
         Matrix.CreateFromYawPitchRoll(-Rotation.Y, 0, 0)

        *
        PositionMatrix;

                float Offset = MathHelper.ToRadians(-90);

                Vector3 GunDrawPos = Position
                    + new Vector3(
           0 * (float)Math.Cos(Rotation.Y + Offset) +
            1 * (float)Math.Sin(-Rotation.Y + Offset), 0,
             0 * (float)Math.Sin(Rotation.Y + Offset) +
            1 * (float)Math.Cos(-Rotation.Y + Offset)) * 40;


                PlayerGun.WorldMatrix = Matrix.CreateFromYawPitchRoll(-Rotation.Y - TorsoMove, 0, 0) *
                           Matrix.CreateTranslation(GunDrawPos);
            }
        }

        public void Draw(Game1 game,LocalPlayer player)
        {
            if (game.LocalPlayerNumb<3)
            {
                float TorsoMove = 0;
                if (ShootTime > 20)
                    TorsoMove = -MathHelper.ToRadians(LegRot / 8);

                game.DrawPlayer(PlayerTorso, game.Loader.Torso, player, Alpha, NewDrawNumb, NewDrawList, DepthStencilState.Default, DrawColor);

                game.DrawPlayer(PlayerLeg, game.Loader.Leg, player, Alpha, NewDrawNumb, NewDrawList, DepthStencilState.Default, DrawColor);

                game.DrawPlayer(PlayerLeg, game.Loader.Leg, player, Alpha, NewDrawNumb, NewDrawList, DepthStencilState.Default, DrawColor);

                game.DrawPlayer(PlayerLowerLeg, game.Loader.LowerLeg, player, Alpha, NewDrawNumb, NewDrawList, DepthStencilState.Default, DrawColor);

                game.DrawPlayer(PlayerLowerLeg2, game.Loader.LowerLeg2, player, Alpha, NewDrawNumb, NewDrawList, DepthStencilState.Default, DrawColor);

                game.GraphicsDevice.BlendState = BlendState.Additive;

                game.DrawAmbient(PlayerOutline, game.Loader.GlowLines, player, Vector4.Multiply(new Vector4(DrawColor, 1), Alpha * 1.8f));

                // game.Npcs[0].DrawModel(game, game.Loader.GlowLines, player, Position, new Vector3(0, Rotation.Y + TorsoMove, 0), Alpha, Vector3.One, game.Loader.AmbientTexture, DrawColor * Alpha * 2, game.Loader.Temp);

                game.GraphicsDevice.BlendState = BlendState.AlphaBlend;

                Model mod = game.Holder.GunModel[GunCurrent];
                if (mod != null)
                    game.DrawPlayer(PlayerGun, mod, player, Alpha, NewDrawNumb, NewDrawList, DepthStencilState.Default, Vector3.One);
            }
            else
            {
                float TorsoMove = 0;
                if (ShootTime > 20)
                    TorsoMove = -MathHelper.ToRadians(LegRot / 8);

                game.DrawPlayer(PlayerFull, game.Loader.FullPlayerModel, player, Alpha, NewDrawNumb, NewDrawList, DepthStencilState.Default, DrawColor);


                game.GraphicsDevice.BlendState = BlendState.Additive;

                game.DrawAmbient(PlayerOutline, game.Loader.GlowLines, player, Vector4.Multiply(new Vector4(DrawColor, 1), Alpha * 1.8f));

                game.GraphicsDevice.BlendState = BlendState.AlphaBlend;

            }

        }

        public void Die(Game1 game,bool AwardKill,bool NoSuicide)
        {
            if (!game.IsInMenu)
            {
                bool FoundLight = false;
                if (Visible[0] || Visible[1] || Visible[2] || Visible[3])
                    foreach (DynamicLightObject light in game.DynamicLights)
                        if (!FoundLight)
                            if (!light.Relevent)
                            {
                                light.Relevent = true;
                                light.Position = Position;

                                light.Color = new Vector3(MyController.colorVec.X, MyController.colorVec.Y, MyController.colorVec.Z) * 2.5f;
                                light.Distancee = 1100;
                                light.LifeTime = 0;
                                light.MaxLifeTime = 40;
                                light.LimitedLifetime = true;
                                light.ConstUpdate = false;
                                FoundLight = true;
                                light.NeedShadows = false;
                                light.IsSpot = true;
                                light.RecalculateLights(game);
                                light.Create(game);
                            }

                   // game.PlaySound(game.soundHolder.soundEffects["player_die"], Position);
            }
              
            HasDied = true;
            Alive = false;
            for (int i = 0; i < 4; i++)
                if (Visible[i])
                    for (int b = 0; b < 10; b++)
                        game.ExplosionParticles[i].AddParticle(Position, Vector3.Zero);


            foreach (Bullet bull in game.Bullets)
                if (bull.Relevent && bull.Type == 14 && bull.Creator == this)
                    bull.TimeAlive = (int)(bull.MaxTimeAlive + 1);

            if(AwardKill&&!game.GameOver)
            {
              
            GiveMoney(game, 1, this);
            {
                if (ControllerIsHuman)
                {
                    if (MyController.Money > 0)
                    {
                        ShouldRespawnYet = false;
                        HumanController.BuyWindowIsOpen = true;
                        // HumanController.BuyWindowStage = 0;
                        // HumanController.BuyWindowX = ;
                        HumanController.MoveTime = -15;
                    }
                    else
                        ShouldRespawnYet = true;
                }



               
    

                int GiveKill = 0;
                DamageFrom[ID] /= 3;
                //DamageFrom[ID] = Math.Max(0.1f, DamageFrom[ID]);
                foreach (BasicOrb orb in game.Orbs)
                    if (orb.relevent && orb.Team == Team)
                        DamageFrom[orb.ID] /= 3;

                if (LastDamager != ID && DamageFrom[LastDamager] > 80)
                {
                    GiveKill = LastDamager;
                    int GetAssist = 0;
                    float MostDamage = 0;
                    for (int i = 0; i < 16; i++)

                        if (DamageFrom[i] > MostDamage)
                        {
                            MostDamage = DamageFrom[i];
                            GetAssist = i;
                        }
                    if (GiveKill != GetAssist && GetAssist != ID)
                    {
                        if (game.Orbs[GetAssist].Team != Team)
                        {
                            game.Orbs[GetAssist].Energy = Math.Min(game.Orbs[GetAssist].MaxEnergy, game.Orbs[GetAssist].Energy + game.Orbs[GetAssist].MaxEnergy / 4);

                            BasicOrb Assist = game.Orbs[GetAssist];

                            if (Assist.ControllerIsHuman)
                                Assist.HumanController.SetAlphaText("+25 Energy");

                            GiveMoney(game, 1, Assist);

                            if (Assist.ControllerIsHuman)
                            {
                                Assist.HumanController.AddMessage(game, 5, ID + 1);
                            }
                        }
                    }
                    
                }
                else
                {
                  

                    float MostDamage = 0;
                    for (int i = 0; i < 16; i++)

                        if (DamageFrom[i] > MostDamage)
                        {
                            MostDamage = DamageFrom[i];
                            GiveKill = i;
                        }
                    

                if (GiveKill!=LastDamager&&LastDamager!=ID)
                {
                    if (Team != game.Orbs[LastDamager].Team)
                    {
                        game.Orbs[LastDamager].Energy = Math.Min(game.Orbs[LastDamager].MaxEnergy, game.Orbs[LastDamager].Energy + game.Orbs[LastDamager].MaxEnergy / 4);
                     
                        

                        BasicOrb Assist = game.Orbs[LastDamager];

                        if (Assist.ControllerIsHuman)
                            Assist.HumanController.SetAlphaText("+25 Energy");

                        if (Assist.ControllerIsHuman)
                        {
                            Assist.HumanController.AddMessage(game, 5, ID + 1);
                        }
                    }
                }
                }

               BasicOrb Killer = game.Orbs[GiveKill];
               if (IsAssasin && game.gamemode == Game1.GameMode.Assasin)
               {
                   Killer.life = Killer.MaxLife;
                   IsAssasin = false;
                   Killer.IsAssasin = true;
                   Abilty[0] = 3;
                   GunCurrent = 4;
                   Killer.GunCurrent = 0;
                   Killer.Abilty[0] = 8;
                   Killer.Energy = Math.Max(Killer.Energy, Killer.MaxEnergy/2);
                   if (ControllerIsHuman)
                       HumanController.SetAlphaText("You are an assasin!");

                   if (Killer.ControllerIsHuman)
                       Killer.HumanController.SetAlphaText("You are now the loner!");
               }




               if (Killer.Team != Team)
               {
                   Killer.Energy += 25;

                   if (Killer.Energy > Killer.MaxEnergy)
                       Killer.Energy = Killer.MaxEnergy;
                   if (Killer.ControllerIsHuman)
                       Killer.HumanController.SetAlphaText("+25 Energy");
               }
                GiveMoney(game, 1, Killer);
                // Killer.life = Math.Min(Killer.MaxLife, Killer.life + Killer.MaxLife * 0.25f);

                if (Killer.IsControlled)
                {

                    if (Killer.ControllerIsHuman)
                    {
                        if (Killer != this)
                        {
                            if (Killer.Team != Team)
                            {
                                if (!ControllerIsHuman)
                                    Killer.HumanController.AddMessage(game, 2, ID + 1);
                                else
                                    Killer.HumanController.AddMessage(game, 4, ID + 1);
                            }
                            else
                                Killer.HumanController.AddMessage(game, 6, ID + 1);
                            //Killer.HumanController.Kills += 1;
                        }
                        else
                        {
                            Killer.HumanController.AddMessage(game, 0, 0);
                            //Killer.HumanController.Kills -= 1;
                        }
                    }

                }
                if (Killer != this)
                {
                    if (Killer.Team != Team)
                        Killer.MyController.Kills += 1;
                    else if(!NoSuicide)
                        Killer.MyController.Kills -= 1;
                }
                else
                {
                    Killer.MyController.Suicides += 1;
                    if(!NoSuicide)
                    Killer.MyController.Kills -= 1;
                }
                if(!NoSuicide)
                MyController.Deaths += 1;

                if (IsControlled)
                {

                    if (ControllerIsHuman)
                    {

                       // HumanController.Deaths += 1;
                        if (Killer != this)
                        {
                            if (Killer.Team != Team)
                            {
                                if (!ControllerIsHuman)
                                    HumanController.AddMessage(game, 1, GiveKill + 1);
                                else
                                    HumanController.AddMessage(game, 3, GiveKill + 1);
                            }
                            else
                                HumanController.AddMessage(game, 7, GiveKill + 1);
                        }
                    }
                }

                if (game.gamemode == Game1.GameMode.WarLord)
                {
                    int OldGun = Killer.GunCurrent;
                    int OldAbility = Killer.Abilty[0];

                    //if (GunCurrent <= Killer.GunCurrent && Abilty[0] <= Killer.Abilty[0] || game.random.NextDouble() > 0.5)
                    {
                        Killer.MyController.Money += 1;
                        Killer.CPUBuy(game);

                        if (false)
                        {
                            if (Killer.GunCurrent < OldGun)
                                Killer.GunCurrent = OldGun;
                            if (Killer.Abilty[0] < OldAbility)
                                Killer.Abilty[0] = OldAbility;
                        }
                    }

                    {
                        if (GunCurrent > Killer.GunCurrent)
                            Killer.GunCurrent = GunCurrent;

                        if (Abilty[0] > Killer.Abilty[0])
                            Killer.Abilty[0] = Abilty[0];
                    }

                    if (Killer.ControllerIsHuman)
                    {
                        if (OldAbility != Killer.Abilty[0])
                        {
                            if (OldGun != Killer.GunCurrent)
                                Killer.HumanController.SetAlphaText("Gun and Ability Switched!");
                            else
                            Killer.HumanController.SetAlphaText("Ability Switched!");
                            
                        }
                        else if (OldGun != Killer.GunCurrent)
                            Killer.HumanController.SetAlphaText("Gun Switched!");
                    }

                    if (MyController != null)
                    {
                        MyController.AllMoney = 0;
                        MyController.Money = 0;
                        Abilty[0] = 3;
                        GunCurrent = 0;

                        MyController.UnLocked = new bool[4, 2];
                        MyController.UnLocked[0, 0] = true;
                        MyController.UnLocked[0, 1] = true;
                    }
                }
                if (game.gamemode == Game1.GameMode.DownGrade)
                {
                    MyController.AllMoney++;
                    MyController.Money++;
                    CPUBuy(game);

                   

                    //Killer.AllMoney--;
                    int LargestUpgrade = 0;
                    int choose = game.random.Next(2);

                    for (int a = 0; a < 2; a++)
                    {
                       
                        if (LargestUpgrade == 0)
                        {
                            choose = 1 - choose;
                            for (int n = 0; n < 4; n++)
                                if (Killer.MyController.UnLocked[n, choose])
                                    LargestUpgrade = n;
                        }
                    }
                    Killer.MyController.UnLocked[LargestUpgrade, choose]=false;
                    Killer.MyController.UnLocked[0, 0] = true;
                    Killer.MyController.UnLocked[0, 1] = true;

                    Killer.CPUBuy(game);

                    Killer.MyController.Money = 0;
                    MyController.Money = 0;

                }


            }
        }
            foreach (BasicOrb orb in game.Orbs)
                orb.DamageFrom[ID] = 0;
        }

        public void UpdateWalk(Game1 game)
        {
            LegRot = MathHelper.Clamp(LegRot, -WalkMax - 8, WalkMax + 8);

            LegRot += LegVel / 4 * WalkSpeed;
            if (LegRot > WalkMax)
                LegVel -= 1;
            if (LegRot < -WalkMax)
                LegVel += 1;
            if (LegVel > 0)
            {
                LegBend[1] = -LegRot;
                LegBend[0] = (3 * LegVel + 7) * (0.75f + WalkSpeed / 4);
            }
            else
            {
                LegBend[0] = LegRot;
                LegBend[1] = ((3 * 14) - ((3 * LegVel + 7) * (0.75f + WalkSpeed / 4))) / 2;
            }

            for (int i = 0; i < 2; i++)
            {
                TargetLegBend[i] += (LegBend[i] - TargetLegBend[i]) / 3;
                //TargetLegBend[i] = Math.Max(LegBend[i], -30);
            }
        }

        public void Update(Game1 game, GameTime gametime)
        {
            MyGamer = null;
            if (game.onlineHandler.networkSession != null)
            {
                foreach(NetworkGamer gamer in game.onlineHandler.networkSession.AllGamers)
                    if (gamer.Tag != null)
                    {
                        if (gamer.Tag == this)
                            MyGamer = gamer;
                        
                    }

            }

            if (OldGunCurrent != GunCurrent)
            {
                PlayerGun.LoadEffectParameters(game, game.Holder.GunModel[GunCurrent], PlayerDrawObject.EffectType.Light);
                OldGunCurrent = GunCurrent;
            }

            
            if (!inGame)
                Alive = false;


            if (Alive)
            {
                if (Energy < 0)
                    Energy = 0;

                Boundaries.Center = Position;
                Boundaries.Radius = 50;

                #region Scores
                if (!BusySelectingTeams)
                    if (game.gamemode == Game1.GameMode.TeamDeathMatch)
                    {
                        game.TeamScore[Team] += MyController.Kills - MyController.UpdatedKills;
                        MyController.UpdatedKills = MyController.Kills;
                        game.TeamDeaths[Team] += MyController.Deaths - MyController.Updateddeaths;
                        MyController.Updateddeaths = MyController.Deaths;
                    }
                #endregion

                Inv -= 1;
                if (false)
                {
                    #region FindRails
                    NeedleTime += 1;


                    float count = 0;
                    foreach (Bullet bullet in game.Bullets)
                        if (bullet.Relevent && bullet.Type == 8)
                        {
                            float distance = Vector3.Distance(bullet.Position, Position);
                            if (distance < 100)
                                count += 1;


                        }
                    if (count > MaxNeedles || NeedleTime > MaxNeedleTime)
                        foreach (Bullet bullet in game.Bullets)
                            if (bullet.Relevent && bullet.Type == 8)
                            {
                                float distance = Vector3.Distance(bullet.Position, Position);
                                if (distance < 100)
                                {
                                    if (count > MaxNeedles)
                                    {
                                        bullet.ExplosionDamage = 600;
                                        bullet.ExplosionPush = 1;
                                        bullet.ExplosionPushVelMult = 1;
                                        bullet.ExplosionSize = 600;
                                        bullet.Type = 1;
                                        bullet.TimeAlive = (int)bullet.MaxTimeAlive + 1;
                                    }
                                    else if (bullet.Creator != this)
                                        bullet.Die(game, false);
                                }
                            }

                    #endregion
                }

                #region Phasing

                AimHelpAmount -= 3f * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
                if (AimHelpAmount < BaseAimHelpAmount)
                    AimHelpAmount = BaseAimHelpAmount;

                if (IsPhasing)
                {
                    PhaseParticleTimer += 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
                    if (PhaseParticleTimer > 3)
                    {

                        for (int i = 0; i < 4; i++)
                            if (Visible[i])
                                game.PhaseParticles[i].AddParticle(Position, Vector3.Zero);
                        PhaseParticleTimer = 0;
                    }

                    if (PhaseLight != null)
                    {
                        PhaseLight.Position = Position;
                        PhaseLight.UpdateMatrix();
                    }
                    AimHelpAmount = 180;

                    PhaseTimer -= 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
                    if (PhaseTimer < 0)
                    {
                        bool PlaceFree = true;
                        foreach (Block block in game.ActiveBlocks)
                            if (block.Relevent)
                                if (block.alive)
                                    if (game.B_Holder.BlockSolid[block.Type])
                                        if (PlaceFree)
                                        {
                                            if (Position.X + Size.X / 2 + block.Size.X / 2 > block.Position.X && Position.X - Size.X / 2 - block.Size.X / 2 < block.Position.X)
                                                if (Position.Z + Size.Y / 2 + block.Size.Y / 2 > block.Position.Z && Position.Z - Size.Y / 2 - block.Size.Y / 2 < block.Position.Z)
                                                    PlaceFree = false;
                                        }
                        if (PlaceFree)
                        {
                            game.PlaySound(game.soundHolder.soundEffects["player_dash_reverse"], Position);

                            IsPhasing = false;
                            if (PhasePushTime - PhasePushNegative > 5)
                                pushTime = PhasePushTime - PhasePushNegative;
                            PhasePushNegative = 0;
                            PhaseVelocity = Vector3.Zero;
                            if (PhaseLight != null)
                            {
                                PhaseLight.MaxLifeTime = 5;
                                PhaseLight.LimitedLifetime = true;
                                PhaseLight.LifeTime = 0;
                                PhaseLight.Distancee = 800;
                            }
                            if (pushTime < 5)
                                Inv = MaxPhaseinv;
                        }
                    }

                    ToPosition = Position + PhaseVelocity * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;

                    if (IsPhasing)
                    {
                        bool PlaceFree = true;
                        foreach (Block block in game.ActiveBlocks)
                            if (PlaceFree)
                                if (block.Relevent)
                                    if (block.alive)
                                        if (game.B_Holder.BlockSolid[block.Type])
                                            if (block.PhaseBlock)
                                                if (ToPosition.X + Size.X / 2 + block.Size.X / 2 > block.Position.X && ToPosition.X - Size.X / 2 - block.Size.X / 2 < block.Position.X)
                                                    if (ToPosition.Z + Size.Y / 2 + block.Size.Y / 2 > block.Position.Z && ToPosition.Z - Size.Y / 2 - block.Size.Y / 2 < block.Position.Z)
                                                    {
                                                        PlaceFree = false;

                                                    }

                        bool HitNpc = false;
                        foreach (NPC npc in game.Npcs)
                            if (npc.Relevent && npc.Alive)
                                if (!npc.Hittable)
                                {
                                    float Dist = Vector3.Distance(Position, npc.Position);
                                    if (Dist < npc.Size * 2)
                                    {

                                        float NewDist = Vector3.Distance(ToPosition, npc.Position);
                                        if (Dist > NewDist)
                                        {
                                            if (Dist > npc.Size)
                                                if (NewDist < npc.Size)
                                                    HitNpc = true;
                                            if (Dist < npc.Size)
                                                if (NewDist > npc.Size)
                                                    HitNpc = true;
                                        }
                                        if (HitNpc)
                                        {

                                            {
                                                PhaseVelocity = -PhaseVelocity;
                                            }
                                        }
                                    }
                                }


                        if (!PlaceFree)
                        {
                            Vector3 TempPosition = Position;
                            Vector3 PrevTempPosition = TempPosition;

                            for (int b = 0; b < 200; b++)
                            {
                                PrevTempPosition = TempPosition;
                                if (TempPosition.X < ToPosition.X)
                                    TempPosition.X += 1;
                                else
                                    TempPosition.X -= 1;

                                if (TempPosition.Z < ToPosition.Z)
                                    TempPosition.Z += 1;
                                else
                                    TempPosition.Z -= 1;

                                foreach (Block block in game.ActiveBlocks)
                                    if (block.Relevent)
                                        if (block.alive)
                                            if (game.B_Holder.BlockSolid[block.Type])
                                                if (block.PhaseBlock)
                                                    if (TempPosition.X + Size.X / 2 + block.Size.X / 2 > block.Position.X && TempPosition.X - Size.X / 2 - block.Size.X / 2 < block.Position.X)
                                                        if (TempPosition.Z + Size.Y / 2 + block.Size.Y / 2 > block.Position.Z && TempPosition.Z - Size.Y / 2 - block.Size.Y / 2 < block.Position.Z)
                                                        {
                                                            Position = PrevTempPosition;
                                                            Vector3 VelocityMult = Vector3.Zero;
                                                            if (Math.Abs(TempPosition.X - block.Position.X) / block.Size.X < Math.Abs(TempPosition.Z - block.Position.Z) / block.Size.Y)
                                                            {
                                                                if (TempPosition.Z > block.Position.Z)
                                                                    PhaseVelocity.Z = Math.Abs(PhaseVelocity.Z);
                                                                else
                                                                    PhaseVelocity.Z = -Math.Abs(PhaseVelocity.Z);
                                                            }
                                                            else
                                                            {
                                                                if (TempPosition.X > block.Position.X)
                                                                    PhaseVelocity.X = Math.Abs(PhaseVelocity.X);
                                                                else
                                                                    PhaseVelocity.X = -Math.Abs(PhaseVelocity.X);
                                                            }


                                                        }
                            }
                        }
                        else
                            Position = ToPosition;
                    }




                }

                #endregion

                #region Damage



                for (int i = 0; i < 16; i++)
                    if (DamageFrom[i] > 0)
                    {
                        // DamageFrom[i] -= 0.075f;

                        // DamageFrom[i] = 0;
                    }
                SpeedBoostTime -= 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
                EMPTime -= 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
                if (EMPTime > 0)
                {
                    Charge[0] = 0;
                    Charge[1] = 0;
                    if (pushTime < 2)
                        pushTime = 2;
                }


                HitBounce -= 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
                RechargeTime += 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
                LastShootTime += 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
                PhasePause -= 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
                if (PhasePause < 1)
                    PhasePauseRepeat += 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;

                if (TeamImunityActive)
                {
                    TeamImunity -= 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
                    if (TeamImunity < 1)
                        TeamImunityActive = false;
                }
                else
                    TeamImunity += 0.5f * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;

                if (RechargeTime > MaxRechargeTime)
                {

                    DamageResistance += 0.2f * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
                    if (DamageResistance > MaxDamageResistance - 1)
                        DamageResistance2 += 0.075f;

                    if (DamageResistance > MaxDamageResistance)
                        DamageResistance = MaxDamageResistance;
                    if (DamageResistance2 > MaxDamageResistance2)
                        DamageResistance2 = MaxDamageResistance2;
                }
                if (IsPhasing)
                {
                    Damage = 0;
                    pushDamage = 0;
                    pushTime = 0;
                    PushVelocity = Vector3.Zero;
                }
                if (pushDamage > 0)
                {
                    //CloakTime = 0;
                    Alpha = 1;
                    NeedleTime = 0;
                    for (int i = 0; i < 2; i++)
                        if (game.Holder.StopCharge[GunCurrent, i])
                        {
                            Charge[i] = 0;

                        }

                    for (int i = 0; i < 2; i++)
                        if (Charge[i] > 0)
                        {
                            //Charge[i]=100000;
                            //if(i==0)
                            //Shoot(game,true,false);
                            //else
                            //Shoot(game,false,true);
                            //Charge[i] = 0;

                        }
                    if (PhasePauseRepeat > 20)
                    {
                        PhasePause = 10;
                        PhasePauseRepeat = 0;
                    }
                    pushTime = (pushDamage / 3 * Damage * (1.75f + (MaxLife - (life - 1)) / (MaxLife * 0.66f))) * PushTimeMult;

                    if(ControllerIsHuman)
                    HumanController.SetVibration(0.05f, 0, pushTime/30);

                    if (SpeedBoostTime > 0)
                    {
                        PhaseRecharge += 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
                        pushTime = 0;
                    }
                    PushVelocity = new Vector3(
       0 * (float)Math.Cos(pushDirection) +
        1 * (float)Math.Sin(-pushDirection), 0,
         0 * (float)Math.Sin(pushDirection) +
        1 * (float)Math.Cos(-pushDirection)
        ) * Math.Min(pushDamage * Damage * (1 + (MaxLife - (life - 1)) / 75), 50) * PushVelMult * OverAllPushVelMult;
                    PushVelMult = 0;
                    pushDamage = 0;
                }

                while (Damage > 0)
                {


                    RechargeTime = 0;
                    if (DamageResistance > 0 || DamageResistance2 > 0)
                    {
                        if (DamageResistance > 0)
                            DamageResistance -= 1;
                        else
                            DamageResistance2 -= 1;
                    }
                    else
                    {

                        life -= 1;

                        if (life < 0 && !HasDied)
                            Die(game, true, false);
                    }
                    Damage -= 1;
                    DamageFrom[LastDamager] += 1;
                }

                pushTime -= 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
                if (game.gamemode == Game1.GameMode.Assasin && IsAssasin)
                    pushTime -= 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
                float MaxDist = 1000000;

                foreach (BasicOrb orb in game.Orbs)
                    if (orb.relevent && orb.Alive && orb.Team != Team && orb != this)
                    {
                        float dist = Vector3.Distance(Position, orb.Position);
                        if (MaxDist > dist)
                            MaxDist = dist;

                    }
                if (MaxDist > 400)
                    pushTime -= Math.Max(0, (MaxDist - 400) / 400) * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
                if (Alpha < 0.25f)
                    pushTime -= 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;

                if (pushTime < 1)
                    WeaponPushTime -= 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;

                ToPosition = Position + PushVelocity * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;

                if (PushVelocity != Vector3.Zero)
                {
                    bool PlaceFree = true;
                    foreach (Block block in game.ActiveBlocks)
                        if (PlaceFree)
                            if (block.Relevent)
                                if (block.alive)
                                    if (game.B_Holder.BlockSolid[block.Type])
                                        if (ToPosition.X + Size.X / 2 + block.Size.X / 2 > block.Position.X && ToPosition.X - Size.X / 2 - block.Size.X / 2 < block.Position.X)
                                            if (ToPosition.Z + Size.Y / 2 + block.Size.Y / 2 > block.Position.Z && ToPosition.Z - Size.Y / 2 - block.Size.Y / 2 < block.Position.Z)
                                            {
                                                PlaceFree = false;

                                            }

                    if (!PlaceFree)
                    {
                        PushVelocity /= 2;
                        Vector3 TempPosition = Position;
                        Vector3 PrevTempPosition = TempPosition;

                        for (int b = 0; b < 200; b++)
                        {
                            PrevTempPosition = TempPosition;
                            if (TempPosition.X < ToPosition.X)
                                TempPosition.X += 1;
                            else
                                TempPosition.X -= 1;

                            if (TempPosition.Z < ToPosition.Z)
                                TempPosition.Z += 1;
                            else
                                TempPosition.Z -= 1;

                            foreach (Block block in game.ActiveBlocks)
                                if (block.Relevent)
                                    if (block.alive)
                                        if (game.B_Holder.BlockSolid[block.Type])
                                            if (TempPosition.X + Size.X / 2 + block.Size.X / 2 > block.Position.X && TempPosition.X - Size.X / 2 - block.Size.X / 2 < block.Position.X)
                                                if (TempPosition.Z + Size.Y / 2 + block.Size.Y / 2 > block.Position.Z && TempPosition.Z - Size.Y / 2 - block.Size.Y / 2 < block.Position.Z)
                                                {
                                                    Position = PrevTempPosition;
                                                    Vector3 VelocityMult = Vector3.Zero;
                                                    if (Math.Abs(TempPosition.X - block.Position.X) / block.Size.X < Math.Abs(TempPosition.Z - block.Position.Z) / block.Size.Y)
                                                    {
                                                        if (TempPosition.Z > block.Position.Z)
                                                            PushVelocity.Z = Math.Abs(PushVelocity.Z);
                                                        else
                                                            PushVelocity.Z = -Math.Abs(PushVelocity.Z);
                                                    }
                                                    else
                                                    {
                                                        if (TempPosition.X > block.Position.X)
                                                            PushVelocity.X = Math.Abs(PushVelocity.X);
                                                        else
                                                            PushVelocity.X = -Math.Abs(PushVelocity.X);
                                                    }


                                                }
                        }
                    }
                    else
                    {

                        Position = ToPosition;
                    }
                }

                if (pushTime < 0)
                    PushVelocity = PushVelocity * 0.75f * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
                PushVelocity = PushVelocity * 0.95f * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;

                if (Math.Abs(PushVelocity.X) + Math.Abs(PushVelocity.Y) + Math.Abs(PushVelocity.Z) < 2)
                    PushVelocity = Vector3.Zero;


                #endregion

                #region Falling
                if (!IsPhasing)
                    Falling = false;





                if (!BeenFalling)
                    foreach (Floor block in game.Floors)
                        if (Falling)
                            if (block.Relevent)
                                if (Falling)
                                {
                                    if (Position.X + Size.X / 2 + block.Size.X / 2 > block.Position.X && Position.X - Size.X / 2 - block.Size.X / 2 < block.Position.X)
                                        if (Position.Z + Size.Y / 2 + block.Size.Y / 2 > block.Position.Z && Position.Z - Size.Y / 2 - block.Size.Y / 2 < block.Position.Z)
                                            Falling = false;
                                }

                if (Falling)
                {
                    BeenFalling = true;
                    Position.Y -= 20;
                }
                if (Position.Y < -2000)
                {
                    Position.Y = 0;
                    Alive = false;
                    BeenFalling = false;
                }

                #endregion

                #region Reload

                ShootTime += 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
                FlashAlpha -= 0.25f * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;


                WeaponHolder Holder = game.Holder;

                for (int i = 0; i < 2; i++)
                {

                    ROF[GunCurrent, i] += 1 *(float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f/1.25f;
                    BurstTime[GunCurrent, i] += 1 *(float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
                    if (BurstTime[GunCurrent, i] > Holder.BurstTime[GunCurrent, i])
                    {
                        BurstSize[GunCurrent, i] = Holder.BurstSize[GunCurrent, i];

                    }

                    if (ClipSize[GunCurrent, i] == 0)
                        if (ReloadTime[GunCurrent, i] == 0)
                            ReloadTime[GunCurrent, i] = 1;

                    if (ReloadTime[GunCurrent, i] > 0)
                    {
                        ReloadTime[GunCurrent, i] += 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
                        if (ReloadTime[GunCurrent, i] > Holder.ReloadTime[GunCurrent, i])
                        {
                            BurstSize[GunCurrent, i] = Holder.BurstSize[GunCurrent, i];
                            ReloadTime[GunCurrent, i] = 0;
                            while (ClipSize[GunCurrent, i] < Holder.ClipSize[GunCurrent, i])
                            {
                                ClipSize[GunCurrent, i] += 1;
                            }
                        }
                    }
                }
                #endregion

                #region AimHelp
                AutoAimCounter += (1 + game.random.Next(5)) * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;

                TargetTime -= 0.1f * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
                if (TargetTime < 0)
                    TargetTime = 0;

                if (AutoAiming)
                {
                    TargetTime += 0.2f * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
                    if (TargetTime > 1)
                        TargetTime = 1;

                    if (GunCurrent == 2)
                    {
                        RailTargetOrb = AutoAimOrb;
                        RailTargetTime = Charge[0] / Holder.ChargeTime[GunCurrent, 0] * 1.75f;

                    }
                }
                if (AutoAimCounter > 20)
                {
                    AutoAimCounter = 0;

                    float AimDist = 1400;
                    if (RailTargetTime < 0.1f)
                    {
                        AutoAiming = false;

                        bool FoundTarget = false;

                        foreach (BasicOrb orb in game.Orbs)
                            if (orb.relevent && orb.Alive && orb.Alpha > 0 && orb != this && orb.Team != Team)
                            {
                                float TempDist = Vector3.Distance(Position, orb.Position);
                                bool FoundWall = false;

                                if (TempDist < AimDist)
                                {
                                    TempDire = -(float)Math.Atan2(orb.Position.X - Position.X, orb.Position.Z - Position.Z) + MathHelper.ToRadians(270);

                                    float AimDire = MathHelper.ToDegrees(Math.Min(Math.Abs(Rotation.Y - TempDire), Math.Abs((Rotation.Y - TempDire) + MathHelper.ToRadians(360))));
                                    IsAutoShooting = false;
                                    if (AimDire < AimHelpAmount * game.Holder.AimHelpAmount[GunCurrent])
                                    {
                                        if (AimDire < Math.Min(AimHelpAmount, BaseAimHelpAmount))
                                            IsAutoShooting = true;
                                        
                                        Vector3 TempPos = Position;

                                        float reps = 50;
                                        while (Vector3.Distance(TempPos, orb.Position) > 100 && !FoundWall && reps > 0)
                                        {
                                            reps -= 1;

                                            TempPos += (new Vector3(100 * (float)Math.Sin(-TempDire + MathHelper.ToRadians(270)), 0, 50 * (float)Math.Cos(-TempDire + MathHelper.ToRadians(270))));
                                            ToPosition = TempPos;
                                            if (GunCurrent != 2)
                                                if (Abilty[0] != 6 || AbilityCounter == 0)
                                                    foreach (Block block in game.ActiveBlocks)
                                                        if (block.Relevent)
                                                            if (block.alive)
                                                                if (game.B_Holder.BlockSolid[block.Type])
                                                                    if (!block.Destructable)
                                                                        //  if(game.B_Holder.BlockStopRail[block.Type])
                                                                        if (ToPosition.X + block.Size.X / 2 > block.Position.X && ToPosition.X - block.Size.X / 2 < block.Position.X)
                                                                            if (ToPosition.Z + block.Size.Y / 2 > block.Position.Z && ToPosition.Z - block.Size.Y / 2 < block.Position.Z)
                                                                            {
                                                                                FoundWall = true;
                                                                                reps = 0;
                                                                                //block.Size.X += 0.1f;//
                                                                                //TempDire = -(float)Math.Atan2(TempPos.X - Position.X, TempPos.Z - Position.Z) - MathHelper.ToRadians(90);//
                                                                            }
                                        }
                                        if (!FoundWall)
                                        {
                                            BestTarget = orb;
                                            AimDist = TempDist;
                                            FoundTarget = true;
                                        }
                                    }
                                }
                            }

                        AutoAiming = false;
                        //FoundTarget = true;//
                        if (FoundTarget)
                        {
                            TargetDire = -(float)Math.Atan2(BestTarget.Position.X - Position.X, BestTarget.Position.Z - Position.Z) - MathHelper.ToRadians(90);
                            AutoAiming = true;
                            TargetOrb = BestTarget;
                            AutoAimOrb = TargetOrb;
                            //TargetDire = TempDire;//
                        }
                    }
                    else
                    {
                        if (RailTargetOrb.Alive && RailTargetOrb.relevent)
                            TargetDire = -(float)Math.Atan2(RailTargetOrb.Position.X - Position.X, RailTargetOrb.Position.Z - Position.Z) - MathHelper.ToRadians(90);
                        else
                        {
                            RailTargetTime = 0;
                            AutoAiming = false;
                            Charge[0] = 0;
                        }
                    }
                }
                #endregion

                #region flashlight

                if (false)
                //if(Team==1)
                {
                    if (!HasFlashLight)
                    {
                        bool FoundLight = false;
                        foreach (DynamicLightObject light in game.DynamicLights)
                            if (!FoundLight)
                                if (!light.Relevent)
                                {
                                    light.Relevent = true;
                                    light.Position = Position + new Vector3(0, 100, 0);
                                    light.Color = new Vector3(1, 0.75f, 0.5f) * 2.5f;
                                    light.Distancee = 1400;
                                    light.LifeTime = 0;
                                    light.ConstUpdate = true;
                                    light.LimitedLifetime = false;
                                    FoundLight = true;
                                    light.RecalculateLights(game);
                                    light.Create(game);
                                    FlashLight = light;
                                    HasFlashLight = true;
                                    light.IsSpot = false;
                                }
                    }
                    else
                    {
                        float Offset = MathHelper.ToRadians(-90);
                        FlashLight.Position = Position + new Vector3(
   0 * (float)Math.Cos(Rotation.Y + Offset) +
    1 * (float)Math.Sin(-Rotation.Y + Offset), 0,
     0 * (float)Math.Sin(Rotation.Y + Offset) +
    1 * (float)Math.Cos(-Rotation.Y + Offset)) * 50;
                        FlashLight.TargetPosition = Position + new Vector3(
       0 * (float)Math.Cos(Rotation.Y + Offset) +
        1 * (float)Math.Sin(-Rotation.Y + Offset), 0,
         0 * (float)Math.Sin(Rotation.Y + Offset) +
        1 * (float)Math.Cos(-Rotation.Y + Offset)) * 100;
                    }
                }
                #endregion

                #region cloak

               // Abilty[0] = 8;

                if (CloakEmitterin)
                {
                    if (CloakEmitter != null)
                        if (!CloakEmitter.Taken)
                            CloakEmitterin = false;

                    CloakEmitter.Position = Position;
                }


                if (game.gamemode == Game1.GameMode.Assasin)
                {
                    pushTime -= 2 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;

                    if (!IsAssasin)
                    {

                        Team = 0;
                        Abilty[0] = 8;
                        GunCurrent = 0;

                        if (CloakTime < 100)
                            CloakTime = 100;
                        if (Energy < MaxEnergy / 2)
                            Energy = MaxEnergy / 2;
                        //Alpha = 1;
                    }
                    if (IsAssasin)
                    {
                        Team = 1;
                        life = Math.Min(MaxLife, life + 0.1f);
                        GunCurrent = 4;
                        Abilty[0] = 3;
                        CloakTime = 0;
                    }
                }
                else
                {


                    CloakTime -= 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
                }

                AppearancePause += 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
                if (Alpha < 0.25f)
                    AppearancePause = 0;

                if (CloakTime > 0)
                {
                    if (Abilty[0] != 8)
                        CloakTime = 0;

                    //if (IsPhasing)
                      //  Alpha = 1;

                    if (game.gamemode != Game1.GameMode.Assasin)
                        Alpha -= 0.01f*(float)gametime.ElapsedGameTime.TotalMilliseconds/16.66f;
                    else
                        Alpha -= 0.01f*(float)gametime.ElapsedGameTime.TotalMilliseconds/16.66f;

                    if (Alpha < 0)
                    {
                        Alpha = 0;

                    }
                }
                else
                {
                    Alpha += 0.02f*(float)gametime.ElapsedGameTime.TotalMilliseconds/16.66f;;
                    if (Alpha > 1)
                        Alpha = 1;
                }



                if (CloakTime > 0 && game.gamemode != Game1.GameMode.Assasin)
                {
                    if (Energy > 0)
                        Energy -= 0.066f*(float)gametime.ElapsedGameTime.TotalMilliseconds/16.66f;
                    else
                        CloakTime = 0;
                }




                #endregion

                #region DeathZones
                float MoveAMT = 20 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
                if (!IsPhasing)
                {
                    if (Position.X > game.MaxBounds.X)
                        Damage = 100000;
                    if (Position.Z > game.MaxBounds.Z)
                        Damage = 100000;

                    if (Position.X < game.MinBounds.X)
                        Damage = 100000;
                    if (Position.Z < game.MinBounds.Z)
                        Damage = 100000;

                    bool Protected = false;

                    foreach (NPC npc in game.Npcs)
                        if (npc.Alive && npc.Relevent && npc.creator.Team == Team)
                            if (npc.Growth < 1.1f)
                            {
                                if (Vector3.Distance(npc.Position, Position) < npc.Size)
                                    Protected = true;

                            }

                    foreach (Block block in game.ActiveBlocks)

                        if (block.Relevent && block.alive)
                            if (!Protected || block.PhaseBlock)
                                //if (block.Type == 3)
                                if (game.B_Holder.BlockSolid[block.Type])
                                {
                                    if (Position.Z + Size.Y / 2 + block.Size.Y / 2 > block.Position.Z && Position.Z - Size.Y / 2 - block.Size.Y / 2 < block.Position.Z)
                                        if (Position.X + Size.X / 2 + block.Size.X / 2 > block.Position.X && Position.X - Size.X / 2 - block.Size.X / 2 < block.Position.X)
                                        {
                                            PushVelocity = Vector3.Zero;

                                            if (Position.X > block.Position.X)
                                                Position.X += MoveAMT;
                                            else
                                                Position.X -= MoveAMT;
                                            if (Position.Z + Size.Y / 2 + block.Size.Y / 2 > block.Position.Z && Position.Z - Size.Y / 2 - block.Size.Y / 2 < block.Position.Z)
                                                if (Position.X + Size.X / 2 + block.Size.X / 2 > block.Position.X && Position.X - Size.X / 2 - block.Size.X / 2 < block.Position.X)
                                                {
                                                    if (Position.Z > block.Position.Z)
                                                        Position.Z += MoveAMT;
                                                    else
                                                        Position.Z -= MoveAMT;
                                                }
                                        }

                                }
                }
                foreach (SpecialObject block in game.Specials)

                    if (block.Relevent)
                        if (block.Type == 3)
                            //if (game.B_Holder.BlockSolid[block.Type])
                            if (Position.X + Size.X / 2 + block.Size.X / 2 > block.Position.X && Position.X - Size.X / 2 - block.Size.X / 2 < block.Position.X)
                                if (Position.Z + Size.Y / 2 + block.Size.Y / 2 > block.Position.Z && Position.Z - Size.Y / 2 - block.Size.Y / 2 < block.Position.Z)
                                {
                                    Damage = 1000;
                                    //DamageFrom[LastDamager] += 1000;

                                }
                #endregion

                // if (Team > 1)
                AI(game,gametime);
            }
            else
            {
                life = 0;
                Energy = 0;
            }

            #region respawning

            if (!Alive&&inGame)
            {
                if (!game.GameOver)
                    if(!IsAI)
                        if (MyController.Money > 0)
                            if(!HumanController.BuyWindowIsOpen&&HumanController.BuyWindowSize<0.1f)
                        {
                            ShouldRespawnYet = false;
                            HumanController.BuyWindowIsOpen = true;
                            HumanController.MoveTime = -15;

                        }
                if (Respawning)
                {
                    RespawnTime += 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
                    if (RespawnTime > MaxRespawnTime&&game.RespawnWave>game.MaxRespawnWave)
                    {
                        Alive = true;
                        Respawning = false;
                        life = MaxLife;
                        Respawn(game);
                        game.RespawnWave = 0;
                    }
                }
                else
                {
                    if (ShouldRespawnYet&&!BusySelectingTeams)
                    {
                        Respawning = true;
                        RespawnTime = 0;
                    }
                }
            }
            #endregion

        }

        public static void dummyRead(PacketReader reader)
        {
            reader.ReadSingle();
            reader.ReadBoolean();
            reader.ReadInt32();
            reader.ReadSingle();
            reader.ReadVector3();
            reader.ReadInt32();
            reader.ReadInt32();
            reader.ReadInt32();
            dummyReadLocal(reader);
        }

        public static void dummyReadLocal(PacketReader reader)
        {
            reader.ReadBoolean();
            reader.ReadVector2();
            reader.ReadInt32();
            reader.ReadVector3();
            reader.ReadVector3();
            reader.ReadSingle();
            reader.ReadInt32();
            reader.ReadInt32();
            reader.ReadVector3();
            reader.ReadVector4();
            reader.ReadInt32();
            reader.ReadInt32();
            reader.ReadInt32();
            reader.ReadInt32();
        }

        public void Read(PacketReader reader)
        {
            life = reader.ReadSingle();
            Alive = reader.ReadBoolean();
            Inv = reader.ReadInt32();
            pushTime = reader.ReadSingle();
            PushVelocity = reader.ReadVector3();
            MyController.Deaths = reader.ReadInt32();
            MyController.Kills = reader.ReadInt32();
            MyController.FlagScore = reader.ReadInt32();

            if (!IsLocal)
            {
                IsPhasing = reader.ReadBoolean();
                MyController.MoveStickTrack = reader.ReadVector2();
                Team = reader.ReadInt32();
                Position = reader.ReadVector3();
                Rotation = reader.ReadVector3();
                Alpha = reader.ReadSingle();
                GunCurrent = reader.ReadInt32();
                PhaseTimer = reader.ReadInt32();
                PhaseVelocity = reader.ReadVector3();
                MyController.colorVec = reader.ReadVector4();
                Abilty[0] = reader.ReadInt32();
                PrimaryWeaponQue = reader.ReadInt32();
                SecondaryWeaponQue = reader.ReadInt32();
                AbilityQue = reader.ReadInt32();
            }
            else
                dummyReadLocal(reader);
        }

        public void Write(PacketWriter writer)
        {
            if (MyGamer != null)
                writer.Write((byte)MyGamer.Id);
            else
                writer.Write((byte)0);

            writer.Write((float)Math.Max(0, life));
            writer.Write((bool)Alive);
            writer.Write(Math.Max(0, Inv));
            writer.Write(Math.Max(0, pushTime));
            writer.Write(PushVelocity);
            writer.Write(MyController.Deaths);
            writer.Write(Math.Max(0, MyController.Kills));
            writer.Write(MyController.FlagScore);

            writer.Write(IsPhasing);
            writer.Write(MyController.MoveStickTrack);
            writer.Write(Team);
            writer.Write(Position);
            writer.Write(Rotation);
            writer.Write(Math.Max(0, Alpha));
            writer.Write(GunCurrent);
            writer.Write(Math.Max(0, PhaseTimer));
            writer.Write(PhaseVelocity);
            writer.Write(MyController.colorVec);
            writer.Write(Abilty[0]);
            writer.Write(PrimaryWeaponQue);
            writer.Write(SecondaryWeaponQue);
            writer.Write(AbilityQue);
        }

        public bool AICheckShouldPhase
        {
            get
            {
                bool ShouldPhase = true;
                if (Alpha != 1)
                {
                    ShouldPhase = false;
                    if (pushTime > 10)
                        ShouldPhase = true;
                    if (PrevPointTimer > 10 && Vector3.Distance(Position, AIPrevPoint) < 50)
                        ShouldPhase = true;
                    if (AIDistance < 1000 || AIDistance > 2000)
                        ShouldPhase = true;
                }
                else if (AIPhasePause > 0)
                    ShouldPhase = false;

                return ShouldPhase;
            }
        }

        public void AI(Game1 game, GameTime gametime)
        {

            PrevPointTimer += 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
            if (PrevPointTimer > 15)
            {
                PrevPointTimer = 0;
                AIPrevPoint = Position;
            }

           // if(false)
            //if (ID+1 > game.LocalPlayerNumb)
            if(IsAI)
                if(game.onlineHandler.networkSession==null||game.onlineHandler.networkSession.IsHost)
            {
                AiShouldntphase -= 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
                AIGrenadeCooldown += 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;


                if (Alpha != 1)
                    AiShouldntphase = 60;

                float MaxDist = 10000;


                foreach (BasicOrb orb in game.Orbs)
                    if (orb.relevent && orb.Alive && orb != this&&orb.Team!=Team)
                        if (orb.Alpha > 0.5f ||game.gamemode==Game1.GameMode.KeepAway&&game.flag.IsCarried&&game.flag.carrier==orb|| orb.ROF[orb.GunCurrent, 0] < game.Holder.ROF[orb.GunCurrent, 0] || orb.ROF[orb.GunCurrent, 1] < game.Holder.ROF[orb.GunCurrent, 1])
                    {
                        float TempDist = Vector3.Distance(Position, orb.Position);

                        if (game.gamemode == Game1.GameMode.KeepAway)
                            if(game.flag.IsCarried)
                            if (game.flag.carrier == orb)
                                TempDist /= 5;

                        if (TempDist < MaxDist)
                        {
                            MaxDist = TempDist;
                            BestAITargetOrb = orb;
                        }


                    }
                AIPhaseTime += 1;
                if(BestAITargetOrb!=null)
                AIDistance = Vector3.Distance(BestAITargetOrb.Position, Position);
                //if(AIDistance>1000)
                //AISHouldHealthPack = true;
                //if (life > MaxLife - 30)
                //    AISHouldHealthPack = false;
                bool UsePower = false;
                if (BestAITargetOrb != null)
                if (AIDistance<800)
                    if(BestAITargetOrb.Alive)
                        if(AutoAiming)
                    UsePower = true;
                if (Abilty[0] == 7)
                    UsePower = true;
                if (Abilty[0] == 6)
                if (AIDistance < 800)
                    if (BestAITargetOrb != null)
                    if (BestAITargetOrb.Alive)
                    //    if (AutoAiming)
                            UsePower = true;
                if (Abilty[0] == 8)
                    UsePower = true;
                bool UseNade = false;
                bool UseShoot = false;

                //if (GunCurrent == 0||GunCurrent==1||GunCurrent==2||GunCurrent==3)
                {
                    if (AutoAiming)
                       // if(GunCurrent==1||AIDistance<1200)
                        UseShoot = true;
                    if (BestAITargetOrb != null)
                    if (!AutoAiming|| BestAITargetOrb.IsPhasing||BestAITargetOrb.pushTime>15)
                        UseNade = true;

                    if (AIDistance < 1000)
                        foreach (NPC npc in game.Npcs)
                            if (npc.Alive && npc.Relevent && npc.Type == 0)
                                if (npc.Growth < 1.1f) 
                                {
                                    if (BestAITargetOrb != null)
                                    if (Vector3.Distance(npc.Position, BestAITargetOrb.Position) < npc.Size)
                                        UseNade = true;

                                }
                }
                if (GunCurrent == 10)
                {
                    if (AutoAiming)
                        UseShoot = true;
                    if (BestAITargetOrb != null)
                    if (!AutoAiming && AIDistance < 800 || BestAITargetOrb.IsPhasing && AIDistance < 900)
                        UseNade = true;

                    if (AIDistance < 1000)
                        foreach (NPC npc in game.Npcs)
                            if (npc.Alive && npc.Relevent && npc.Type == 0)
                                if (npc.Growth < 1.1f)
                                {
                                    if (BestAITargetOrb != null)
                                    if (Vector3.Distance(npc.Position, BestAITargetOrb.Position) < npc.Size)
                                        UseNade = true;

                                }
                }
                if (AIPhasePause > 0)
                    UsePower = false;
                //if(false)
                if (Alpha != 1 && AIDistance > 500||Alpha!=1&&!AutoAiming)
                {
                    UseNade = false;
                    UseShoot = false;
                }
                if (Alpha != 1 && Alpha > 0 && AutoAiming)
                {
                    UseShoot = true;
                    UseNade = true;
                }

                if (AIPhasePause < 0)
                {
                    if (Math.Abs(AIDistance) > 800)
                        HandleOtherInput(game,UseShoot, UseNade,false,UsePower,false,gametime);
                    else
                        HandleOtherInput(game, UseShoot, UseNade, false, UsePower, false, gametime);
                }
                if (IsPhasing)
                {
                    if (game.ailevel == Game1.AILevel.VeryEasy)
                        AIPhasePause = 30;
                    if (game.ailevel == Game1.AILevel.Easy)
                        AIPhasePause = 10;
                }

                if (BestAITargetOrb != null)
                {
                    if (BestAITargetOrb.IsPhasing)
                    {
                        if (game.ailevel == Game1.AILevel.VeryEasy)
                            AIPhasePause = 120;
                        if (game.ailevel == Game1.AILevel.Easy)
                            AIPhasePause = 80;
                        if (game.ailevel == Game1.AILevel.Medium)
                            AIPhasePause = 50;
                        if (game.ailevel == Game1.AILevel.Hard)
                            AIPhasePause = 25;
                        if (game.ailevel == Game1.AILevel.Insane)
                            AIPhasePause = 0;
                        AiDodgePause = 10;
                    }
                    if (BestAiTargetOrb != null && BestAiTargetOrb != BestAITargetOrb)
                    {
                        if (game.ailevel == Game1.AILevel.VeryEasy)
                            AIPhasePause = 80;
                        if (game.ailevel == Game1.AILevel.Easy)
                            AIPhasePause = 40;
                        if (game.ailevel == Game1.AILevel.Medium)
                            AIPhasePause = 20;
                        if (game.ailevel == Game1.AILevel.Hard)
                            AIPhasePause = 10;
                        if (game.ailevel == Game1.AILevel.Insane)
                            AIPhasePause = 0;
                       
                    }
                    BestAiTargetOrb = BestAITargetOrb;
                }
                if (!AutoAiming&&AIDistance>500)
                {
                    if (game.ailevel == Game1.AILevel.VeryEasy)
                        if (Math.Floor(game.random.NextDouble() * 80) == 0)
                        AIPhasePause = 50;
                    if (game.ailevel == Game1.AILevel.Easy)
                        if(Math.Floor(game.random.NextDouble()*80)==0)
                        AIPhasePause = 40;
                    if (game.ailevel == Game1.AILevel.Medium)
                        if (Math.Floor(game.random.NextDouble() * 120) == 0)
                        AIPhasePause = 20;
                    if (game.ailevel == Game1.AILevel.Hard)
                        AIPhasePause = 0;
                    if (game.ailevel == Game1.AILevel.Insane)
                        AIPhasePause = 0;
                }
                if (BestAITargetOrb != null)
                if (BestAITargetOrb.AppearancePause <15)
                    if(game.gamemode!=Game1.GameMode.Assasin)
                   // if (AIPhasePause < 11)
                    {
                        AiDodgePause = 14;
                        if (game.ailevel == Game1.AILevel.VeryEasy)
                            AIPhasePause = 80;
                        if (game.ailevel == Game1.AILevel.Easy)
                            AIPhasePause = 40;
                        if (game.ailevel == Game1.AILevel.Medium)
                            AIPhasePause = 30;
                        if (game.ailevel == Game1.AILevel.Hard)
                            AIPhasePause = 20;
                        if (game.ailevel == Game1.AILevel.Insane)
                            AIPhasePause = 14;
                        //pushTime = 14;
                    }
                if (Alpha != 1)
                    AIPhasePause = 0;

                AIPhasePause -= 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
                AiDodgePause -= 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
                if (AIPhasePause < 0)
                {
                   // AimPoint += Vector2.Normalize( (new Vector2(BestAITargetOrb.Position.X, BestAITargetOrb.Position.Z)-AimPoint))*8;
                    if (BestAITargetOrb != null)
                    AimPoint = new Vector2( BestAITargetOrb.Position.X,BestAITargetOrb.Position.Z);
                    Rotation.Y = -(float)Math.Atan2(AimPoint.X - Position.X, AimPoint.Y - Position.Z) + MathHelper.ToRadians(270);
                }

               // if (Math.Abs( Rotation.Y - TargetRotationY) < MathHelper.ToRadians(2))


                bool MovedAlready = false;
                if (game.ailevel == Game1.AILevel.VeryEasy)
                    DodgeMult = 50;
                if (game.ailevel == Game1.AILevel.Easy)
                    DodgeMult = 20;
                if (game.ailevel == Game1.AILevel.Medium)
                    DodgeMult = 10;
                if (game.ailevel == Game1.AILevel.Hard)
                    DodgeMult = 3;
                if (game.ailevel == Game1.AILevel.Insane)
                    DodgeMult = -1;

                if (Math.Floor(game.random.NextDouble() * 30) == 0)
                    AIReaction = 0;

                bool ShouldWorry=false;

                    if(game.gamemode==Game1.GameMode.KeepAway)
                        if (!game.flag.IsCarried)
                    if(AiDodgePause<0)
                        if (!MovedAlready)
                        {
                            float TempDist = Vector3.Distance(Position, game.flag.Position);


                            MaxDist = TempDist;

                            float TempRotationY = Rotation.Y - (-(float)Math.Atan2(game.flag.Position.X - Position.X, game.flag.Position.Z - Position.Z) + MathHelper.ToRadians(270));
                            Vector2 DodgeMove = new Vector2(
0 * (float)Math.Cos(TempRotationY) +
1 * (float)Math.Sin(-TempRotationY),
 0 * (float)Math.Sin(TempRotationY) +
1 * (float)Math.Cos(-TempRotationY));
                            bool ShouldPhase = AICheckShouldPhase;

                            MoveFromController(game, -DodgeMove, Vector2.Zero, Vector2.Zero, ShouldPhase, false, gametime);
                            //HandleOtherInput(game, true, false);
                            MovedAlready = true;
                        }


                if(AiDodgePause<0)
                if(!MovedAlready)
                foreach (Bullet bull in game.Bullets)
                    if (bull.Relevent)
                        if (bull.Type == 1 && bull.Position.Y < 0 || bull.Type == 4 && bull.Creator != this || bull.Type == 5 && bull.Position.Y < 0||bull.Type==6||bull.Type==10&&bull.Position.Y < 0||bull.Type==11||bull.Type==12)
                        {
                            float TempDist = Vector3.Distance(Position, bull.Position);

                            if (TempDist < 800 && bull.Type == 1 || bull.Type == 4 && TempDist < 0 + game.random.NextDouble() * 300 || bull.Type == 6 && TempDist < 1200 || TempDist < 800 && bull.Type == 10 || TempDist < bull.BlackHoleRange * bull.BlackHoleSize * 2.2f && bull.Type == 11 || bull.Type == 12 && TempDist < 550 || bull.Type == 13 && TempDist < 200 || bull.Type == 14 && TempDist < 650 || bull.Type == 15 && TempDist < 300)

                            if (TempDist < MaxDist)
                            
                            {
                                ShouldWorry = true;
                                AIReaction += 1;
                               // if (TempDist > 200)
                                if(AIReaction>DodgeMult)
                                {
                                    MaxDist = TempDist;

                                    float TempRotationY = Rotation.Y-(-(float)Math.Atan2(bull.Position.X - Position.X, bull.Position.Z - Position.Z) + MathHelper.ToRadians(270));
                                    Vector2 DodgeMove = new Vector2(
       0 * (float)Math.Cos(TempRotationY) +
        1 * (float)Math.Sin(-TempRotationY),
         0 * (float)Math.Sin(TempRotationY) +
        1 * (float)Math.Cos(-TempRotationY));
                                    bool ShouldPhase = AICheckShouldPhase;

                                    MoveFromController(game,DodgeMove, Vector2.Zero, Vector2.Zero,ShouldPhase,false,gametime);
                                    //HandleOtherInput(game, true, false);
                                    MovedAlready = true;
                                }
                            }

                           
                        }
                if (!ShouldWorry)
                    AIReaction = 0;

                if (!MovedAlready)
                    foreach (NPC bull in game.Npcs)
                        if (bull.Relevent && bull.Alive)
                            if (bull.Type == 0||bull.Type==2)
                            {



                                float TempDist = Vector3.Distance(Position, bull.Position);

                                if (TempDist < 2600)

                                //if (TempDist < MaxDist)
                                {

                                    // if (TempDist > 200)
                                    {
                                        MaxDist = TempDist;

                                        float TempRotationY = Rotation.Y - (-(float)Math.Atan2(bull.Position.X - Position.X, bull.Position.Z - Position.Z) + MathHelper.ToRadians(270))+MathHelper.ToRadians(30);
                                        Vector2 DodgeMove = new Vector2(
           0 * (float)Math.Cos(TempRotationY) +
            1 * (float)Math.Sin(-TempRotationY),
             0 * (float)Math.Sin(TempRotationY) +
            1 * (float)Math.Cos(-TempRotationY));
                                        if (bull.creator == this||bull.creator.Team==Team)
                                        {
                                            if (AutoAiming || AIDistance < 1400)
                                            {
                                                if(TempDist>100)
                                                    MoveFromController(game, -DodgeMove, Vector2.Zero, Vector2.Zero, false, false, gametime);
                                                MovedAlready = true;
                                            }
                                        }
                                        else if(bull.creator.Team!=Team)
                                        {
                                            bool ShouldPhase = AICheckShouldPhase;
                                            MoveFromController(game, DodgeMove, Vector2.Zero, Vector2.Zero, ShouldPhase, false, gametime);
                                            MovedAlready = true;
                                        }
                                        //HandleOtherInput(game, true, false);

                                    }
                                }


                            }

                //if(false)
                if(!MovedAlready)
                foreach (Pickup pick in game.Pickups)
                    if (pick.Relevent)
                        if (life<MaxLife&&pick.Type==0)
                        {
                            float TempDist = Vector3.Distance(Position, pick.Position);

                            if (TempDist < 40000)

                                if (TempDist < MaxDist)
                                {

                                    // if (TempDist > 200)
                                    {
                                        MaxDist = TempDist;

                                        float TempRotationY = Rotation.Y - (-(float)Math.Atan2(pick.Position.X - Position.X, pick.Position.Z - Position.Z) + MathHelper.ToRadians(270));
                                        Vector2 DodgeMove = -new Vector2(
           0 * (float)Math.Cos(TempRotationY) +
            1 * (float)Math.Sin(-TempRotationY),
             0 * (float)Math.Sin(TempRotationY) +
            1 * (float)Math.Cos(-TempRotationY));
                                        bool ShouldPhase = AICheckShouldPhase;
                                        MoveFromController(game, DodgeMove, Vector2.Zero, Vector2.Zero, ShouldPhase, false, gametime);
                                        //HandleOtherInput(game, true, false);
                                        MovedAlready = true;
                                    }
                                }


                        }
                


                //if(false)
                if(!MovedAlready)
                {
                    if (BestAITargetOrb != null)
                if (BestAITargetOrb.pushTime > 0)
                {
                    //HoldingForGrenade = true;
                    AISuggestedDistance = 100;
                    //if (TeamImunityActive)
                       // AISuggestedDistance = 1000000;
                   // if (AIGrenadeCooldown < 60)
                   //     AISuggestedDistance = 800;
                    //if (life < MaxLife - 30&&HealthPacks>0)
                   //     AISuggestedDistance = 100000;
                    if (GunCurrent == 1)
                        AISuggestedDistance = 700;
                    if (BestAITargetOrb != null)
                    if (life  < BestAITargetOrb.life)
                        AISuggestedDistance = 700;
                    if (GunCurrent == 2)
                        AISuggestedDistance = 1200;
                    if (Alpha != 1||AiShouldntphase>0)
                        AISuggestedDistance = 100;
                    bool ShouldPhase =AICheckShouldPhase;
                    if (BestAITargetOrb != null)
                        if (BestAiTargetOrb.Inv > 0)
                            AISuggestedDistance = 1000000;
                    MoveFromController(game, new Vector2(0, MathHelper.Clamp((AISuggestedDistance - AIDistance) / 10, -1, 1)), Vector2.Zero, Vector2.Zero, false, false, gametime);
                }
                else
                {
                    AISuggestedDistance = 100;
                    if (DamageResistance+1 < MaxDamageResistance)
                    {
                       
                        AISuggestedDistance = 700;
                    }
                    if (Energy >= 50)
                        AISuggestedDistance = 100;
                    if (GunCurrent == 1)
                        AISuggestedDistance = 700;
                        
                   // if (AIGrenadeCooldown < 60)
                     //   AISuggestedDistance = 800;

                   // if (life < MaxLife - 30 && HealthPacks > 0)
                    //    AISuggestedDistance = 100000;
                    if (BestAITargetOrb != null)
                    if (life * 1 < BestAITargetOrb.life)
                        AISuggestedDistance = 700;
                    if (GunCurrent == 2)
                        AISuggestedDistance = 1200;

                    bool ShouldPhase = AICheckShouldPhase;


                    if (Alpha != 1 || AiShouldntphase > 0)
                        AISuggestedDistance = 100;
                    if (BestAITargetOrb != null)
                        if (BestAiTargetOrb.Inv > 0)
                            AISuggestedDistance = 1000000;
                    MoveFromController(game, new Vector2(0.5f, MathHelper.Clamp((AISuggestedDistance - AIDistance) / 10, -1, 1)), Vector2.Zero, Vector2.Zero, ShouldPhase, false, gametime);
                }
                    

                }



 
            }
           
        }

        public void MoveFromController(Game1 game,Vector2 MoveStick, Vector2 LookStick,Vector2 MouseCheck,bool AButton,bool AbuttonPrev,GameTime gametime)
        {
           
           

            #region Moving
            MovePause -= 1;
            if (Position.Y == 0 && Alive && pushTime < 0 && !IsPhasing&&MovePause<0)
            {
                //if (Charge[0] == 0 && Charge[1] == 0)
                {
                    float mult = 1;   
                    /*
                    if(MoveStick.Y>0)
                        MoveStick.Y *= 0.85f;
                    MoveStick.X *= 0.85f;
                    */
                    if (MoveStick.Y > 0)
                        mult *= (1 - MoveStick.Y)*0.1f + 0.9f;
                    mult *= (1 - Math.Abs(MoveStick.X)) * 0.05f + 0.95f;
                   // mult = 1;
                    

                    float MovingDirection = -(float)Math.Atan2(MoveStick.X, -MoveStick.Y) + Rotation.Y + MathHelper.ToRadians(90);

                    float movespeed = MoveSpeed*(float)gametime.ElapsedGameTime.TotalMilliseconds/16.66f;

                    if (SpeedBoostTime > 0)
                        movespeed *= SpeedBoostMult;

                    //if(Alpha==0)
                    movespeed *= 1+(1 - Alpha)*0.625f;

                    if (game.gamemode == Game1.GameMode.KeepAway)
                    {
                        if (game.flag.carrier != this)
                            movespeed *= 1.35f;
                        else
                            movespeed *= 0.95f;
                    }

                    //if (CloakTime > 0)
                      //  movespeed *= 0.5f;
                    for (int i = 0; i < 2; i++)
                    {
                        //if(Charge[i]>0)
                       // movespeed *= game.Holder.ChargeSpeed[GunCurrent, i];
                    }

                    Vector3 ToPosition = Position + new Vector3(
       0 * (float)Math.Cos(MovingDirection) +
        1 * (float)Math.Sin(-MovingDirection), 0,
         0 * (float)Math.Sin(MovingDirection) +
        1 * (float)Math.Cos(-MovingDirection)) * movespeed * Math.Min(0.75f, Vector2.Distance(MoveStick, Vector2.Zero))*(1/0.75f)*(0.8f+0.2f*life/MaxLife)*mult;

                    //WalkSpeed = Vector2.Distance(MoveStick, Vector2.Zero) * 10;
                    if (Vector2.Distance(MoveStick, Vector2.Zero) > 0.05f)
                    {
                        WalkSpeed = Vector2.Distance(MoveStick, Vector2.Zero) * 12.5f * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
                        UpdateWalk(game);
                        WalkSoundTimer += 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
                        if (WalkSoundTimer > 20)
                        {
                            WalkSoundTimer = 0;
                            game.PlaySound(game.soundHolder.soundEffects["player_walk"+game.random.Next(3)], Position);
                        }
                    }
                        /*

                    float mult = Vector3.Dot(Vector3.Normalize( new Vector3((float)Math.Sin(-Rotation.Y), 0, (float)Math.Cos(-Rotation.Y))),Vector3.Normalize(ToPosition-Position));
                    
                    ToPosition = Position + new Vector3(
        0 * (float)Math.Cos(MovingDirection) +
         1 * (float)Math.Sin(-MovingDirection), 0,
          0 * (float)Math.Sin(MovingDirection) +
         1 * (float)Math.Cos(-MovingDirection)) * movespeed * Math.Min(0.75f, Vector2.Distance(MoveStick, Vector2.Zero)) * (1 / 0.75f) * (0.8f + 0.2f * life / MaxLife);
                    */
                    bool Protected = false;

                    foreach (NPC npc in game.Npcs)
                        if (npc.Alive && npc.Relevent)
                            if (npc.Growth < 1.1f)
                            {
                                if (Vector3.Distance(npc.Position, ToPosition) < npc.Size)
                                    Protected = true;

                            }
                    if (WallWalking)
                        Protected = true;

                    WallWalking = false;

                    bool PlaceFree = true;
                    foreach (Block block in game.ActiveBlocks)
                        if (block.Relevent)
                            if (block.alive)
                                if (game.B_Holder.BlockSolid[block.Type])
                                    if (ToPosition.X + Size.X / 2 + block.Size.X / 2 > block.Position.X && ToPosition.X - Size.X / 2 - block.Size.X / 2 < block.Position.X)
                                        if (ToPosition.Z + Size.Y / 2 + block.Size.Y / 2 > block.Position.Z && ToPosition.Z - Size.Y / 2 - block.Size.Y / 2 < block.Position.Z)
                                {
                                    if (PlaceFree)
                                    
                                        if (!Protected || block.PhaseBlock)
                                        {

                                                    PlaceFree = false;
                                        }
                                        if(Protected&&!block.PhaseBlock)
                                            WallWalking = true;
                                    }
                    if (PlaceFree)
                    {
                        
                        Position = ToPosition;
                    }
                    else
                    {
                        Vector3 TempPosition = Position;
                        for (int b = 0; b < 10; b++)
                        {
                            Position = TempPosition;
                            if (TempPosition.X < ToPosition.X)
                                TempPosition.X += 1;
                            else
                                TempPosition.X -= 1;

                            if (Math.Abs(TempPosition.X - ToPosition.X) < 1.5f)
                                TempPosition.X = ToPosition.X;

                            foreach (Block block in game.ActiveBlocks)
                                if (block.Relevent)
                                    if (block.alive)
                                        if (game.B_Holder.BlockSolid[block.Type])
                                        {
                                            if (TempPosition.X + Size.X / 2 + block.Size.X / 2 > block.Position.X && TempPosition.X - Size.X / 2 - block.Size.X / 2 < block.Position.X)
                                                if (TempPosition.Z + Size.Y / 2 + block.Size.Y / 2 > block.Position.Z && TempPosition.Z - Size.Y / 2 - block.Size.Y / 2 < block.Position.Z)
                                                    TempPosition = Position;
                                        }

                        }
                        for (int b = 0; b < 10; b++)
                        {
                            Position = TempPosition;
                            if (TempPosition.Z < ToPosition.Z)
                                TempPosition.Z += 1;
                            else
                                TempPosition.Z -= 1;

                            if (Math.Abs(TempPosition.Z - ToPosition.Z) < 1.5f)
                                TempPosition.Z = ToPosition.Z;

                            foreach (Block block in game.ActiveBlocks)
                                if (block.Relevent)
                                    if (block.alive)
                                        if (game.B_Holder.BlockSolid[block.Type])
                                        {
                                            if (TempPosition.X + Size.X / 2 + block.Size.X / 2 > block.Position.X && TempPosition.X - Size.X / 2 - block.Size.X / 2 < block.Position.X)
                                                if (TempPosition.Z + Size.Y / 2 + block.Size.Y / 2 > block.Position.Z && TempPosition.Z - Size.Y / 2 - block.Size.Y / 2 < block.Position.Z)
                                                    TempPosition = Position;
                                        }

                        }


                    }

                }
            }
            if (MouseCheck == Vector2.Zero)
            {
                Rotation.Y += LookStick.X / 25 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;



                #region AimAdjust
                if (AutoAiming||RailTargetTime>0)
                {


                    ProjectedPosition = Position + new Vector3((float)Math.Sin(-Rotation.Y - MathHelper.ToRadians(90)), 0, (float)Math.Cos(-Rotation.Y - MathHelper.ToRadians(90))) * 200;
                    float MultAmt = 30000;
                    if (IsPhasing)
                        MultAmt = 22500;
                    if (RailTargetTime > 0)
                        MultAmt = 5000;

                    MultAmt /= ((float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f);

                    ProjectedPosition += (TargetOrb.Position - ProjectedPosition) * Math.Max(0, (1000 - Vector3.Distance(TargetOrb.Position, ProjectedPosition)) / MultAmt);// *Math.Abs(LookStick.X / 2500);

                    if (Vector3.Distance(TargetOrb.Position, ProjectedPosition) < 40)
                        ProjectedPosition = TargetOrb.Position;
                    Rotation.Y = -(float)Math.Atan2(ProjectedPosition.X - Position.X, ProjectedPosition.Z - Position.Z) - MathHelper.ToRadians(90);

                }
                #endregion

            }
            else
            {

                if (AutoAiming)
                {

                    Rotation.Y += MouseCheck.X / 200 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;


                    //Rotation.Y += (Rotation.Y - TargetDire) * Math.Abs(MouseCheck.X / 200);
                    //Rotation.Y += (TargetDire-Rotation.Y)/10;

                }
                else
                    Rotation.Y += MouseCheck.X / 200 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
            }
                if (Rotation.Y > MathHelper.ToRadians(360))
                    Rotation.Y -= MathHelper.ToRadians(360);
                if (Rotation.Y < 0)
                    Rotation.Y += MathHelper.ToRadians(360);
            
            #endregion

            #region Phasing
            if (Position.Y == 0 && Alive&&!IsPhasing&&EMPTime<1&&PhasePause<1)
            {

                PhaseRecharge += 1 * (0.75f + 0.25f * life / MaxLife) * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
                if (!AbuttonPrev&&AButton && PhaseRecharge > MaxPhaseRecharge)
                {
                   // CloakTime = 0;
                    LegRot = WalkMax + 8;

                    Alpha = 1;
                    PhasePushTime=Math.Min(MaxPhasePushTime,pushTime);
                    Charge[0] = 0;
                    Charge[1] = 0;
                    PhaseRecharge = 0;
                    IsPhasing = true;
                    PhasingDirection = Rotation.Y;

                    Vector3 TempVec3=Vector3.Normalize(new Vector3(MoveStick.X,0,MoveStick.Y));

                    PhasingDirection = -(float)Math.Atan2(MoveStick.X, -MoveStick.Y)+Rotation.Y+MathHelper.ToRadians(90);
                    PhaseTimer = MaxPhasingTime;
                    float mult = 1;
                    if (MoveStick.Y > 0)
                        mult *= (1 - MoveStick.Y) * 0.1f + 0.9f;
                    mult *= (1 - Math.Abs(MoveStick.X)) * 0.05f + 0.95f;

                    PhaseVelocity= new Vector3(
0 * (float)Math.Cos(PhasingDirection) +
1 * (float)Math.Sin(-PhasingDirection), 0,
0 * (float)Math.Sin(PhasingDirection) +
1 * (float)Math.Cos(-PhasingDirection)) * PhaseSpeed*mult*1.03f;


                    bool FoundLight = false;
                    foreach (DynamicLightObject light in game.DynamicLights)
                        if (!FoundLight)
                            if (!light.Relevent)
                            {
                                HasPlayedReverseDash = false;
                                game.PlaySound(game.soundHolder.soundEffects["player_dash"], Position);

                                light.Relevent = true;
                                light.Position = Position;
                                light.Color = PhaseColor;
                                light.Distancee = 300;
                                light.LifeTime = 0;
                                light.ConstUpdate = true;
                                light.LimitedLifetime = false;
                                FoundLight = true;
                                PhaseLight = light;
                                light.IsSpot = true;
                                light.NeedShadows = false;
                                light.Brightness = 1;
                                light.RecalculateLights(game);
                                light.Create(game);
                            }

                }

            }
            #endregion

            if(false)
            if (IsPhasing && !AButton&&PhaseTimer>-1)
            {
                PhasePushNegative = PhaseTimer / MaxPhasingTime * PhasePushTime;
                PhaseTimer = -1;
            }
        }

        public void HandleOtherInput(Game1 game, bool RightTrigger,bool LeftTrigger,bool Bbutton,bool RightBumper, bool LeftBumper,GameTime gametime)
        {
            AbilityStop -= 1;
           

            #region shooting
            if (WeaponPushTime<1)
            if (RightTrigger||LeftTrigger||Charge[1]>0||Charge[0]>0)
                if (pushTime < 0 && !IsPhasing || Charge[1] > 0 || Charge[0] > 0)
                    Shoot(game, RightTrigger, LeftTrigger, gametime);
            #endregion

            #region healthpacks
            if(false)
            if (Bbutton)
            {
                if (HealthPacks > 0)
                {
                    HealthPackHoldTime += 1;
                    pushTime = 10;
                    WeaponPushTime = 90;
                    if (HealthPackHoldTime > 150)
                    {

                        if (false)
                        {
                            bool FoundPickup = false;
                            foreach (Pickup pick in game.Pickups)
                                if (!FoundPickup)
                                    if (!pick.Relevent)
                                    {
                                        FoundPickup = true;
                                        pick.Relevent = true;
                                        pick.Type = 1;
                                        pick.Position = Position + new Vector3(50, 0, 0);
                                        pick.Create(game);
                                        HealthPacks -= 1;
                                        HealthPackHoldTime = -40;
                                    }
                        }
                        else
                        {
                            HealthPacks -= 1;
                            life = MaxLife;
                            pushTime = 30;
                        }
                    }
                }
            }
            else
                HealthPackHoldTime = 0;
            #endregion

            #region Abilities

            if(Alive)
                if(AbilityStop<1||Abilty[0]==8)
            for (int i = 0; i < 2; i++)
            {
                AbilityCoolDown[i] -= 1;
                //if (AbilityStop > 0)
                   // AbiltyCharge[i] = 0;

                bool IsPressed=false;

                if (i == 0)
                    IsPressed = RightBumper;
                else
                    IsPressed = LeftBumper;

                #region Ability 1
                if (Abilty[i] == 1)
                {
                    if (Energy >= 25&&EMPTime<1)
                    {
                        if (IsPressed && pushTime < 1&&AbiltyCharge[i]==0||AbiltyCharge[i]>0&&AbiltyCharge[i]<1-2/30||IsPhasing&&AbiltyCharge[i]>0)
                        {

                            AbiltyCharge[i] += (float)1 / 30;
                            if (AbiltyCharge[i] > 1)
                                AbiltyCharge[i] = 1;
                        }
                        else
                        {
                            if (AbiltyCharge[i] > 0.5)
                            {
                                Energy -= 25;
                                bool found = false;

                                foreach (Bullet shot in game.Bullets)
                                    if (!found)
                                        if (!shot.Relevent)
                                        {
                                            shot.Relevent = true;
                                            shot.Position = Position;
                                            shot.PulseArea = 750 * AbiltyCharge[i];
                                            AbiltyCharge[i] = 0;
                                            shot.CreatorID = ID;
                                            found = true;

                                            shot.Velocity = Vector3.Zero;

                                            shot.Creator = this;

                                            shot.TimeAlive = 0;
                                            shot.MaxTimeAlive = 10000;
                                            shot.bounces = 0;
                                            shot.Type = 2;


                                            shot.Spawn(game);
                                        }
                            }

                        }
                    }
                    else
                        AbiltyCharge[i] = 0;
                }
                #endregion

                #region Ability 2 Speed
                if (Abilty[i] == 2)
                {
                    if(IsPressed)
                    if (Energy >= 50 &&SpeedBoostTime<1)//&& pushTime < 1)
                    {
                        Energy -= 50;
                        SpeedBoostTime = 200;
                        pushTime = 0;
                        EMPTime = 0;

                    }

                }
                #endregion

                #region Ability 3 (Shield)

                if (Abilty[i] == 3&&Energy>=50&&AbilityCoolDown[i]<1)
                {
                    if (IsPressed || AbiltyCharge[i] > 0 || IsPressed && IsPhasing)
                    {
                        if (!IsPhasing)
                            if (Math.Abs(PushVelocity.X) + Math.Abs(PushVelocity.Z) < 10)
                        {
                            Energy -= 50;
                            PushVelocity = Vector3.Zero;
                            pushTime = 0;
                            AbilityCoolDown[i] = 60;
                            MovePause = 20;

                            game.PlaySound(game.soundHolder.soundEffects["shield_sound"], Position);
                            bool Found = false;

                            if (ControllerIsHuman)
                                HumanController.SetVibration(0.01f,1, 0.3f);

                            foreach (NPC npc in game.Npcs)
                                if (!npc.Relevent)
                                    if (!Found)
                                    {
                                        Found = true;
                                        npc.Relevent = true;
                                        npc.Type = 0;
                                        npc.Create(game);
                                        npc.Position = Position;
                                        npc.creator = this;
                                       // if (AbiltyCharge[i] > 0)
                                        {
                                            AbiltyCharge[i] = 0;
                                            npc.PopWave = 1;
                                        }
                                    }
                        }
                        else
                            AbiltyCharge[i] = 1;

                    }

                }

                #endregion

                #region Ability 4 AirStrike
                if (Abilty[i] == 4)
                {
                    if (Energy >= 50)
                    {
                        if (IsPressed && pushTime < 1 && AbilityCoolDown[i] < 30&&AutoAiming)
                        {


                            Energy -= 50;
                            bool found = false;

                            foreach (Bullet shot in game.Bullets)
                                if (!found)
                                    if (!shot.Relevent)
                                    {
                                        shot.Relevent = true;
                                        shot.Position = AutoAimOrb.Position;
                                        shot.TargetOrb = AutoAimOrb;
                                        shot.PulseArea = 0;
                                        AbiltyCharge[i] = 0;
                                        shot.CreatorID = ID;
                                        shot.seeking = true;

                                        found = true;

                                        shot.Velocity = Vector3.Zero;

                                        shot.Creator = this;

                                        shot.TimeAlive = 0;
                                        shot.MaxTimeAlive = 10000;
                                        shot.bounces = 0;
                                        shot.LightColor = new Vector3(1, 0, 0);
                                        shot.LightDistance = 500;
                                        shot.Type = 6;


                                        shot.Spawn(game);
                                    }



                        }
                    }
                    else
                        AbiltyCharge[i] = 0;
                }
                #endregion

                #region Ability 5 Dark Grenade
                if (Abilty[i] == 5)
                {
                    if (Energy >= 75)
                    {
                        if (IsPressed && AbilityCoolDown[i] < 0)
                        {

                            AbilityCoolDown[i] = 75;
                            Energy -= 50;
                            bool found = false;

                            foreach (Bullet shot in game.Bullets)
                                if (!found)
                                    if (!shot.Relevent)
                                    {
                                        float ShotRotation;
                                        if (!AutoAiming || !IsAutoShooting)
                                            ShotRotation = -Rotation.Y;
                                        else
                                            ShotRotation = -TargetDire;
                                        shot.Relevent = true;
                                        shot.Size = Vector2.Zero;
                                        shot.Position = Position;
                                        shot.CreatorID = ID;
                                        found = true;
                                        shot.Damage = 0;
                                        float angleOffset = -MathHelper.ToRadians(90);

                                        shot.Velocity = new Vector3(
                            10 * (float)Math.Sin(ShotRotation + angleOffset), 0,
                            10 * (float)Math.Cos(ShotRotation + angleOffset)
                            );
                                        shot.ExplosionDamage = 0;
                                        shot.ExplosionSize = 600;
                                        shot.ExplosionPush = 0;
                                        shot.Rotation.Y = ShotRotation;
                                        shot.Creator = this;
                                        shot.TimeAlive = 0;
                                        shot.Push = 0;
                                        shot.PushVelMult = 0;
                                        shot.LightColor = new Vector3(0.5f,0.5f,1)*2;
                                        shot.LightDistance = 250;
                                        shot.ExplosionPushVelMult = 0;
                                        shot.MaxTimeAlive = 75;
                                        shot.bounces = 0;
                                        shot.Type = 10;
                                        shot.Velocity.Y = 6;
                                        shot.Spawn(game);
                                        shot.SpeedPoints = 24;
                                    }



                        }
                    }
                    else
                        AbiltyCharge[i] = 0;
                }
                #endregion

                #region Ability 6 Vampire Dart
                if (Abilty[i] == 6)
                {
                    if (Energy >= 25)
                    {

                        if (IsPressed && AbilityCoolDown[i] < 0||AbilityCounter>0)
                        {
                            if (!IsPhasing)
                                if (Math.Abs(PushVelocity.X) + Math.Abs(PushVelocity.Z) < 10)
                                {
                                    AbilityCounter += 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f; ;
                                    AbilityMaxCounter = 20;
                                    if (AbilityCounter > AbilityMaxCounter)
                                    {
                                        AbilityCounter = 0;
                                        ROF[GunCurrent, 0] = 0;
                                        ROF[GunCurrent, 1] = 0;

                                        AbilityCoolDown[i] = 20;
                                        Energy -= 25;
                                        game.PlaySound(game.soundHolder.soundEffects["dart_sounds"], Position);

                                        if (ControllerIsHuman)
                                            HumanController.SetVibration(0.1f, 0, 0.3f);

                                        for (int t = 0; t < 7; t++)
                                        {
                                            bool found = false;

                                            foreach (Bullet shot in game.Bullets)
                                                if (!found)
                                                    if (!shot.Relevent)
                                                    {
                                                        float ShotRotation;
                                                        if (!AutoAiming || !IsAutoShooting)
                                                            ShotRotation = -Rotation.Y;
                                                        else
                                                            ShotRotation = -TargetDire;
                                                        shot.Relevent = true;
                                                        shot.Size = Vector2.Zero;
                                                        shot.Position = Position;
                                                        shot.CreatorID = ID;
                                                        found = true;
                                                        shot.Damage = 75;
                                                        float angleOffset = -MathHelper.ToRadians(90 - 20 + t * (40 / 7));

                                                        shot.Velocity = new Vector3(
                                            10 * (float)Math.Sin(ShotRotation + angleOffset), 0,
                                            10 * (float)Math.Cos(ShotRotation + angleOffset)
                                            );
                                                        shot.ExplosionDamage = 0;
                                                        shot.ExplosionSize = 0;
                                                        shot.ExplosionPush = 0;
                                                        shot.Rotation.Y = ShotRotation;
                                                        shot.Creator = this;
                                                        shot.TimeAlive = 0;
                                                        shot.Push = 1;
                                                        shot.PushVelMult = 1;
                                                        shot.LightColor = new Vector3(0.35f, 0.65f, 1);
                                                        shot.LightDistance = 250;
                                                        shot.ExplosionPushVelMult = 0;
                                                        shot.MaxTimeAlive = 120;
                                                        shot.bounces = 0;
                                                        shot.Type = 13;
                                                        shot.Velocity.Y = 0;
                                                        shot.SpeedPoints = 35;
                                                        shot.Spawn(game);

                                                    }
                                        }
                                    }
                                }
                                else
                                    AbilityCounter = 0;


                        }
                        else
                            AbilityCounter = 0;
                    }
                    else
                        AbilityCounter = 0;
                }
                #endregion

                #region Ability 7 LandMine
                if (Abilty[i] == 7)
                {
                    if (Energy >= 25)
                    {
                        if (IsPressed && AbilityCoolDown[i] < 0 && pushTime < 0 || AbilityCounter > 0)
                        {
                           // AbilityCounter += 1;
                          //  AbilityMaxCounter = 20;
                            //if (AbilityCounter > AbilityMaxCounter)
                            {
                             //   AbilityCounter = 0;
                                ROF[GunCurrent, 0] = 0;
                                ROF[GunCurrent, 1] = 0;

                                AbilityCoolDown[i] = 40;
                                Energy -= 25;
                                //for (int t = 0; t < 6; t++)
                                {
                                    bool found = false;

                                    foreach (Bullet shot in game.Bullets)
                                        if (!found)
                                            if (!shot.Relevent)
                                            {
                                                float ShotRotation;
                                                if (!AutoAiming || !IsAutoShooting)
                                                    ShotRotation = -Rotation.Y;
                                                else
                                                    ShotRotation = -TargetDire;
                                                shot.Relevent = true;
                                                shot.Size = Vector2.Zero;
                                                shot.Position = Position;
                                                shot.CreatorID = ID;
                                                found = true;
                                                shot.Damage = 0;
                                                float angleOffset = -MathHelper.ToRadians(90);

                                                shot.Velocity = new Vector3(
                                    10 * (float)Math.Sin(ShotRotation + angleOffset), 0,
                                    10 * (float)Math.Cos(ShotRotation + angleOffset)
                                    );
                                                shot.ExplosionDamage = 500;
                                                shot.ExplosionSize = 450;
                                                shot.ExplosionPush = 1;
                                                shot.Rotation.Y = ShotRotation;
                                                shot.Creator = this;
                                                shot.TimeAlive = 0;
                                                shot.Push = 0;
                                                shot.PushVelMult = 0;
                                                shot.LightColor = new Vector3(1, 0.3f, 0.15f)*1.25f;
                                                shot.LightDistance = 300;
                                                shot.ExplosionPushVelMult = 0;
                                                shot.MaxTimeAlive = 60000;
                                                shot.bounces = 0;
                                                shot.Type = 14;
                                                shot.Velocity.Y = 6;
                                                shot.SpeedPoints = 12;
                                                
                                                shot.Spawn(game);

                                            }
                                }
                            }


                        }
                        else
                            AbilityCounter = 0;
                    }
                    else
                        AbilityCounter = 0;
                }
                #endregion

                #region Ability 8 cloak
                if (Abilty[i] == 8)
                {
                    if (IsPressed)
                        if(Energy>0)
                        if (CloakTime < 1)
                        {
                            
                           CloakEmitter=  game.PlaySound(game.soundHolder.soundEffects["cloak_sound"], Position,false);
                            //Energy -= 50;
                            CloakTime = 1000000;
                            pushTime = 0;
                            EMPTime = 0;

                        }

                }
                #endregion

                #region Ability 9 (Turret)

                if (Abilty[i] == 9 && Energy >= 25 && AbilityCoolDown[i] < 1)
                {
                    if (IsPressed || AbiltyCharge[i] > 0 || IsPressed && IsPhasing)
                    {
                        if (!IsPhasing)
                            if (Math.Abs(PushVelocity.X) + Math.Abs(PushVelocity.Z) < 15)
                            {
                                Energy -= 25;
                                PushVelocity = Vector3.Zero;
                                pushTime = 0;
                                AbilityCoolDown[i] = 60;
                                //MovePause = 20;

                                if (ControllerIsHuman)
                                    HumanController.SetVibration(0.1f, 0, 0.3f);

                                bool Found = false;


                                foreach (NPC npc in game.Npcs)
                                    if (!npc.Relevent)
                                        if (!Found)
                                        {
                                            float ShotRotation;
                                            if (!AutoAiming || !IsAutoShooting)
                                                ShotRotation = -Rotation.Y;
                                            else
                                                ShotRotation = -TargetDire;
                                            
                                            float angleOffset = -MathHelper.ToRadians(90);

                                            npc.Velocity = new Vector3(
                                10 * (float)Math.Sin(ShotRotation + angleOffset), 0,
                                10 * (float)Math.Cos(ShotRotation + angleOffset)
                                );

                                            game.PlaySound(game.soundHolder.soundEffects["machine_grenade_launch"], Position);

                                            npc.SpeedPoints = 34;
                                            Found = true;
                                            npc.Relevent = true;
                                            npc.Type = 2;
                                            npc.Create(game);
                                            npc.Position = Position;
                                            npc.creator = this;
                                            // if (AbiltyCharge[i] > 0)
                                            {
                                                AbiltyCharge[i] = 0;
                                                npc.PopWave = 1;
                                            }
                                        }
                            }
                            else
                                AbiltyCharge[i] = 1;

                    }

                }

                #endregion
            }

            #endregion
        }

        public void Shoot(Game1 game, bool RightTrigger, bool LeftTrigger,GameTime gametime)
        {

            bool Protected = false;

            foreach (NPC npc in game.Npcs)
                if (npc.Alive && npc.Relevent&&npc.Type==0)
                    if (npc.Growth < 1.1f)
                    {
                        if (Vector3.Distance(npc.Position, Position) < npc.Size)
                            Protected = true;

                    }

            WeaponHolder Holder = game.Holder;
            for (int i = 0; i < 2;i++ )
                if(GunCurrent!=2||AutoAiming||i!=0||RailTargetTime>0)
                if(i==0&&RightTrigger||i==1&&LeftTrigger||Charge[i]>0)//||Charge[i]>0)
                
                //   if(Charge[1-i]<1)
                    if(Charge[1-i]<1)
                if (ReloadTime[GunCurrent, i] == 0)
                    if (ROF[GunCurrent, i] > Holder.ROF[GunCurrent, i])

                        if (BurstSize[GunCurrent, i] > 0)
                        {
                            if (ClipSize[GunCurrent, i] > 0)
                            {
                                Charge[i] += 1.25f * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
                                game.TestValue = Charge[i];

                                if (Charge[i] > Holder.ChargeTime[GunCurrent, i])
                                {
                                    float Offset = MathHelper.ToRadians(-90);

                                    bool PlaceFree = true;


                                        ToPosition = Position + new Vector3(
    0 * (float)Math.Cos(Rotation.Y + Offset) +
    1 * (float)Math.Sin(-Rotation.Y + Offset), 0,
    0 * (float)Math.Sin(Rotation.Y + Offset) +
    1 * (float)Math.Cos(-Rotation.Y + Offset)) * (10);


                                        foreach (Block block in game.ActiveBlocks)
                                            if (PlaceFree)
                                                if (block.Relevent)
                                                    if (block.alive)
                                                        if (game.B_Holder.BlockSolid[block.Type])
                                                            if (ToPosition.X + Size.X / 2 + block.Size.X / 2 > block.Position.X && ToPosition.X - Size.X / 2 - block.Size.X / 2 < block.Position.X)
                                                                if (ToPosition.Z + Size.Y / 2 + block.Size.Y / 2 > block.Position.Z && ToPosition.Z - Size.Y / 2 - block.Size.Y / 2 < block.Position.Z)
                                                                    PlaceFree = false;
                                    


                               
                                        ROF[GunCurrent, 1 - i] = Holder.ROF[GunCurrent, i] - Holder.ShootPause[GunCurrent, i];
                                        BurstSize[GunCurrent, i] -= 1;
                                        ClipSize[GunCurrent, i] -= 1;
                                        ROF[GunCurrent, i] = 0;
                                        BurstTime[GunCurrent, i] = 0;


                                        if (PlaceFree)
                                        {
                                            if (ControllerIsHuman)
                                                HumanController.SetVibration(0.1f, 0, 0.5f);

                                            for (int g = 0; g < Holder.ShotBulletNumb[GunCurrent, i]; g++)
                                            {
                                                float Accuracy = Holder.Accuracy[GunCurrent, i];
                                                if (Protected)
                                                    Accuracy /= ShieldDivide;
                                                float angleOffset = -MathHelper.ToRadians(90) + MathHelper.ToRadians(Accuracy / 2 - (float)game.random.NextDouble() * Accuracy);

                                                if (Holder.ShotBulletNumb[GunCurrent, i] > 1)
                                                    angleOffset = -MathHelper.ToRadians(90) + MathHelper.ToRadians((Accuracy * g / (Holder.ShotBulletNumb[GunCurrent, i] - 1)) - (Accuracy / 2));

                                                bool found = false;
                                                foreach (Bullet shot in game.Bullets)
                                                    if (!found)
                                                        if (!shot.Relevent)
                                                        {

                                                            string ind = game.Holder.GunSoundEffect[GunCurrent, i];
                                                            if (game.Holder.GunSoundEffectRange[GunCurrent, i] > 0)
                                                                ind += game.random.Next(game.Holder.GunSoundEffectRange[GunCurrent, i]);

                                                            game.PlaySound(game.soundHolder.soundEffects[ind], Position);

                                                            FlashAlpha = 1.25f;
                                                            ShootTime = 0;
                                                            //CloakTime = 0;
                                                            Alpha = 1;
                                                            if (Holder.BulletType[GunCurrent, i] == 1)
                                                                AIGrenadeCooldown = 0;
                                                            //AutoAiming = false;
                                                            float ShotRotation;
                                                            if (!AutoAiming || !IsAutoShooting)
                                                                ShotRotation = -Rotation.Y;
                                                            else
                                                                ShotRotation = -TargetDire;


                                                            Vector3 GunDrawPos = Position + new Vector3(
                   0 * (float)Math.Cos(Rotation.Y + Offset) +
                    1 * (float)Math.Sin(-Rotation.Y + Offset), 0,
                     0 * (float)Math.Sin(Rotation.Y + Offset) +
                    1 * (float)Math.Cos(-Rotation.Y + Offset)) * (40 + Holder.GunOffset[GunCurrent]);

                                                            shot.Relevent = true;
                                                            shot.Size = Holder.ShotSize[GunCurrent, i];// +new Vector2(50);
                                                            shot.Position = GunDrawPos;
                                                            shot.CreatorID = ID;
                                                            found = true;
                                                            shot.Damage = Holder.BulletDamage[GunCurrent, i];
                                                            if (Protected)
                                                                shot.Damage *= (1 + ShieldDivide) / 2;


                                                            if (Holder.ChargeTime[GunCurrent, i] == 0)
                                                            {
                                                                shot.SpeedPoints = Holder.BulletSpeed[GunCurrent, i];
                                                                pushTime = Holder.Pause[GunCurrent, i];
                                                            }
                                                            else
                                                            {
                                                                pushTime = Holder.Pause[GunCurrent, i] * Charge[i] / Holder.ChargeTime[GunCurrent, i];
                                                                shot.SpeedPoints = Holder.BulletSpeed[GunCurrent, i] * Charge[i] / Holder.ChargeTime[GunCurrent, i];
                                                            }
                                                            if (Protected)
                                                                shot.SpeedPoints *= ShieldDivide;
                                                            shot.Velocity = new Vector3(
                                                10 * (float)Math.Sin(ShotRotation + angleOffset), 0,
                                                10 * (float)Math.Cos(ShotRotation + angleOffset)
                                                );

                                                            shot.ExplosionDamage = Holder.BulletExplosionDamage[GunCurrent, i];
                                                            shot.ExplosionSize = Holder.BulletExplosionSize[GunCurrent, i];
                                                            shot.ExplosionPush = Holder.BulletExplosionPush[GunCurrent, i];
                                                            shot.Rotation.Y = ShotRotation;
                                                            shot.Creator = this;
                                                            shot.TimeAlive = 0;
                                                            shot.Push = Holder.BulletPush[GunCurrent, i];
                                                            shot.PushVelMult = Holder.BulletPushVelMult[GunCurrent, i];
                                                            shot.LightColor = Holder.ShotLight[GunCurrent, i];
                                                            shot.LightDistance = Holder.ShotLightDist[GunCurrent, i];
                                                            if (Protected)
                                                                shot.PushVelMult /= ShieldDivide;
                                                            shot.ExplosionPushVelMult = Holder.BulletExplosionPushVelMult[GunCurrent, i];

                                                            //shot.Update(game);
                                                            //shot.Update(game);

                                                            shot.MaxTimeAlive = Holder.BulletLifeTIme[GunCurrent, i];
                                                            shot.bounces = 0;
                                                            shot.Type = Holder.BulletType[GunCurrent, i];


                                                            if (game.gamemode == Game1.GameMode.Assasin)
                                                            {
                                                                if (IsAssasin)
                                                                {
                                                                    Damage *= 3f;
                                                                    shot.ExplosionDamage *= 1.5f;
                                                                }
                                                                else
                                                                    Damage *= 0.25f;
                                                            }

                                                            if (GunCurrent == 8 && i == 0)
                                                            {
                                                                RailTargetTime = 0;
                                                                AutoAiming = false;
                                                            }

                                                            if (shot.Type == 1 || shot.Type == 5)
                                                            {
                                                                //HoldingForGrenade = false;
                                                                if (Holder.ChargeTime[GunCurrent, i] == 0)
                                                                    shot.Velocity.Y = 6;
                                                                else
                                                                    shot.Velocity.Y = 6 * Charge[i] / Holder.ChargeTime[GunCurrent, i];
                                                                //if (AutoAiming)
                                                                //    shot.Velocity.Y = Vector3.Distance(Position, TargetOrb.Position)/150;
                                                            }
                                                            shot.Spawn(game);
                                                        }
                                            }
                                            bool FoundLight = false;
                                            if (Visible[0] || Visible[1] || Visible[2] || Visible[3])
                                                foreach (DynamicLightObject light in game.DynamicLights)
                                                    if (!FoundLight)
                                                        if (!light.Relevent)
                                                        {
                                                            light.Relevent = true;
                                                            light.Position = Position + new Vector3(0, 100, 0);

                                                            // float Offset = MathHelper.ToRadians(-90);
                                                            light.Position = Position + new Vector3(
                                       0 * (float)Math.Cos(Rotation.Y + Offset) +
                                        1 * (float)Math.Sin(-Rotation.Y + Offset), 0,
                                         0 * (float)Math.Sin(Rotation.Y + Offset) +
                                        1 * (float)Math.Cos(-Rotation.Y + Offset)) * 120;

                                                            light.Color = Holder.GunLight[GunCurrent, i];
                                                            light.Distancee = 400;
                                                            light.LifeTime = 0;
                                                            light.MaxLifeTime = 2;
                                                            light.LimitedLifetime = true;
                                                            light.ConstUpdate = false;
                                                            FoundLight = true;
                                                            //light.RecalculateLights(game);
                                                            light.Create(game);
                                                            light.IsSpot = true;
                                                        }
                                        }
                                        Charge[i] = 0;
                                    
                                }
                            //else
                            //ReloadTime[GunCurrent] = 1;
                        }
        }
        }

        public void CPUBuy(Game1 game)
        {
            GunCurrent=-1;
            Abilty[0]=-1;

            float chooser=(float)game.random.NextDouble();

            int set=0;
            if(chooser>0.5f)
                set=1;
            Buy(game,set);
            set = 1 - set;
            Buy(game,set);


        }

        void  Buy(Game1 game,int set)
        {
            bool GotIt = false;
            for (int i = 0; i < 4; i++)
            {
                if(!GotIt)
                    if(3-i==0||MyController.UnLocked[3-i-1,set])
                if (MyController.Money >=1||MyController.UnLocked[3-i,set])
                    if(set==0&&GunCurrent==-1||set==1&&Abilty[0]==-1)
                {
                    GotIt = true;
                        if(!MyController.UnLocked[3-i,set])
                    MyController.Money -= 1;
                        
                        if (set == 0)
                        {
                            if (!MyController.UnLocked[3 - i, set])
                            MyController.GunUpgrades += 1;
                            GunCurrent = game.U_holder.UpgradeSet[3 - i, set];
                        }
                        else
                        {
                            if (!MyController.UnLocked[3 - i, set])
                            MyController.AbilityUpgrades += 1;
                            Abilty[0] = game.U_holder.UpgradeSet[3 - i, set];
                        }
                        MyController.UnLocked[3 - i, set] = true;
                }
            }
        }

        public void Respawn(Game1 game)
        {
            if (ControllerIsHuman)
                HumanController.SetVibration(0.5f, 0.75f, 0.75f);

            if (FirstSpawn&&ControllerIsHuman)
            {
                if (game.gamemode == Game1.GameMode.DeathMatch || game.gamemode == Game1.GameMode.DownGrade || game.gamemode == Game1.GameMode.WarLord)
                    HumanController.SetAlphaText(game.gamemode.ToString()+ ": Kill everything that moves!");
                if (game.gamemode == Game1.GameMode.TeamDeathMatch)
                    HumanController.SetAlphaText("Team Deathmatch: Kill the other team!");
                if (game.gamemode == Game1.GameMode.Assasin)
                {
                    if (IsAssasin)
                        HumanController.SetAlphaText("You are the loner\nwatch out for assasins!");
                    else
                        HumanController.SetAlphaText("You are an assasin\nkill the loner.");
                }
                if (game.gamemode == Game1.GameMode.KeepAway)
                    HumanController.SetAlphaText("Keepaway: go for the flag!");
            }

            FirstSpawn = false;
            Inv = MaxSpawnInv;

                    AbilityCounter = 0;
            ShouldRespawnYet = true;
            RailTargetTime = 0;
            AutoAiming = false;
            CloakTime = 0;
            AbilityStop = 60;
            AbilityCoolDown[0] = 60;
            AbilityCoolDown[1] = 60;
            WallWalking = false;
            for (int i = 0; i < 2; i++)
            {
                AbiltyCharge[i] = 0;
                AbilityCoolDown[i] = 0;
                for (int g = 0; g < GunNumb; g++)
                {
                    HasWeapon[g] = false;
                    ROF[g, i] = 0;
                    ClipSize[g, i] = 0;
                    BurstSize[g, i] = 0;
                    ReloadTime[g, i] = 0;
                    BurstTime[g, i] = 0;
                    Ammo[g, i] = 0;
                    Charge[i] = 0;
                }
            }

            //Abilty[1] = -1;
            if (game.LocalPlayerNumb <= ID)
                CPUBuy(game);
           // else if(blah2>0.6f)
           // Abilty[0] = 8;
           // Abilty[0] = 8;
           // GunCurrent = 2;
            //if (ID == 0)
               // GunCurrent = 2;

                HasDied = false;
            pushTime = 0;
            PushVelocity = Vector3.Zero;
            pushDamage = 0;
            Damage = 0;
            DamageResistance = MaxDamageResistance;
            DamageResistance2 = MaxDamageResistance2;
            BestSpawnValue = 0;
            for (int i = 0; i < 16; i++)
                DamageFrom[i] = 0;
                Energy = MaxEnergy / 2;
            if (game.GameIsTeams)
            BestSpawnValue = 100000000;

            bool SpawnPointFound = false;

            foreach (SpecialObject special in game.Specials)
            if(special.Relevent)
                if(special.Type==0)
                    

            {
             bool OK=true;       
                    foreach(BasicOrb orb in game.Orbs)
                        if(orb.relevent)
                                if (orb.Alive)
                                    if (orb != this)
                                    {
                                        if (special.Position.X + special.Size.X / 2 + orb.Size.X / 2 > orb.Position.X && special.Position.X - special.Size.X / 2 - orb.Size.X / 2 < orb.Position.X)
                                            if (special.Position.Z + special.Size.Y / 2 + orb.Size.Y / 2 > orb.Position.Z && special.Position.Z - special.Size.Y / 2 - orb.Size.Y / 2 < orb.Position.Z)
                                                OK = false;
                                    }
                    if(OK)
                    {
                    if (special.Value[1] == 1 && !game.GameIsTeams)
                    {
                        BestPlayerValue = 1000000;

                        foreach(BasicOrb orb in game.Orbs)
                            if(orb.relevent)
                                if (orb.Alive)
                                    //if (orb !=this)
                                {
                                    float tempDist;
                                    tempDist = Vector3.Distance(special.Position, orb.Position);//+special.RespawnCharge;
                                    if (DamageResistance < MaxDamageResistance || DamageResistance2 < MaxDamageResistance2)
                                        tempDist *= 4;
                                    else
                                        tempDist /= 2;
                                    if (tempDist < BestPlayerValue)
                                        BestPlayerValue = tempDist;
                                }
                        if (BestPlayerValue > BestSpawnValue)
                        {
                            BestSpawnValue = BestPlayerValue;
                            BestSpawnPoint = special;
                            SpawnPointFound = true;
                        }

                    }



                    if (special.Value[1] == 0 && game.GameIsTeams && Team == special.Value[0])
                    {
                        BestPlayerValue = 10000000;

                        foreach (BasicOrb orb in game.Orbs)
                            if (orb.relevent)
                                if (orb.Alive)
                                    if (orb.Team == Team)
                                        if (orb != this)
                                        {
                                            float tempDist;
                                            tempDist = Vector3.Distance(special.Position, orb.Position) / special.RespawnCharge;
                                            if (tempDist > BestPlayerValue)
                                                BestPlayerValue = tempDist;
                                        }
                        if (BestPlayerValue < BestSpawnValue)
                        {
                            BestSpawnValue = BestPlayerValue;
                            BestSpawnPoint = special;
                            SpawnPointFound = true;
                        }
                    }

                    }

            }
            if (SpawnPointFound)
            {
                Position = BestSpawnPoint.Position;
                BestSpawnPoint.RespawnCharge = 1;
            }
            if(false)
            for (int t = 0; t < 2; t++)
                for (int m = 0; m < 4; m++)
                {
                    if (t == 0 && GunCurrent == game.U_holder.UpgradeSet[m, t])
                        MyController.GunUpgrades = m;
                    if (t == 1 && Abilty[0] == game.U_holder.UpgradeSet[m, t])
                        MyController.GunUpgrades = m;
                }

        }

        public void Reset(Game1 game)
        {
            IsAssasin = false;
            IsOnline = false;
            IsLocal = false;
            relevent = false;
            Alpha = 1;
            Position = Vector3.Zero;
         Taken = false;
       AbilityCounter = 0;
        AbilityMaxCounter = 0;
        PhasePushTime = 16;
        AllMoney = 0;
        Inv = 0;
        OverAllPushVelMult = 0.9f;
        WallWalking = false;
        MaxPhaseinv = 10;

        IsAI = false;
        MaxNeedles = 10;
        CloakTime = 0;
        MaxPhasePushTime = 18;
        AutoAimOrb=null;
        PhasePushNegative = 0;
        NeedleTime = 0;
        MaxNeedleTime = 450;
        RailTargetTime = 0;
        IsAutoShooting = false;
        RailTargetOrb=null;
        TargetTime = 0;
        ShieldDivide = 1.4f;
        AbilityStop = 0;
        AIPhaseTime = 0;
        SpeedBoostTime = 0;
        AiDodgePause = 0;
        MovePause = 0;
        SpeedBoostMult = 1.6f;
        HealthPackHoldTime = 0;
        PhaseColor = new Vector3(0.25f, 0.65f, 1f) * 4;
        Rotation = Vector3.Zero;
        HasDied = false;
        EMPTime = 0;
        PushVelMult = 0;
        PhaseLight=null;
       ShouldRespawnYet = true;
        LastShooter=null;
        Size = new Vector2(90, 90);
        PhasePause = 0;
        PhasePauseRepeat = 0;
       AbilityCoolDown = new float[2];
        LastDamager = 0;
        relevent = false;
        WeaponPushTime = 0;
        life = 100f;
        MaxLife = 200 * LifeMult;
        HitBounce = 0;
        MaxHitBounce = 10;
        MaxEnergy = 100;
          StartingMaxEnergy = 100;
          Energy = 0;
        
          HealthPacks = 0;
          MaxRechargeTime = 60;
          MoveSpeed = 14;//*1.15f;
          MaxDamageResistance = 15*LifeMult*0;
   
        HumanController=null;
          LastShootTime = 0;
          MaxDamageResistance2 = 35 * LifeMult * 0;
         NewDrawList = new LightObject[8];
        AIDistance = 0;
          DamageResistance = 0;
          RechargeTime = 0;
          DamageResistance2 = 0;
          PushTimeMult = 3;
          NewDrawNumb = 0;
          Velocity = Vector3.Zero;
          PushVelocity = Vector3.Zero;
         BestTarget=null;
          PhaseVelocity = Vector3.Zero;
          ProjectedPosition=Vector3.Zero;
          Alive = true;
          RespawnTime = 0;
          AutoAiming = false;
         BestAITargetOrb=null;
          TargetOrb=null;
         TempDire=0;
          TeamImunity=0;
          MaxTeamImuntiy = 45;
          TeamImunityActive = false;
         MyController=null;
         FlashLight=null;
          Respawning = false;
          PhaseRecharge = 0;
         ToPosition = Vector3.Zero;
          MaxPhaseRecharge = 90;
          MaxRespawnTime = 150;
          GunCurrent = 1;
          Falling = false;
          BeenFalling = false;
          inGame = true;
          Team = 1;
         BestSpawnValue = 0;
          PhaseSpeed = 29;
        SpecialObject BestSpawnPoint=null;
         BestPlayerValue = 0;
          pushDamage = 0;
          pushTime = 0;
          Damage = 0;
          Team = 0;
          pushDirection = 0;
          AimHelpAmount = 0;
          BaseAimHelpAmount = 20;
          IsPhasing = false; ;
          PhasingDirection = 0; ;
          MaxPhasingTime = 22;
          PhaseTimer = 0;
          TargetDire = 0;
          HasFlashLight = false;
          IsControlled = false;
         Abilty = new int[2];
          AbiltyCharge = new float[2];
        ControllerIsHuman = false;
      Visible = new bool[4];
         ID = 0;
       DamageFrom = new float[16];



         //HoldingForGrenade = false;
         AIGrenadeCooldown = 0;
         AISuggestedDistance = 0;
         AIPhasePause = 0;
        // AISHouldHealthPack = false;
         AimPoint = Vector2.Zero;
          AiShouldntphase = 0;
        }
        public void GetColor(Game1 game)
        {
            if (game.gamemode != Game1.GameMode.TeamDeathMatch)
            {
                int reps = 0;
                int NewColor = 0;
                bool foundColor = false;
                while (!foundColor && reps < game.random.Next(0,100))
                {
                    reps += 1;
                    NewColor = (int)MathHelper.Clamp((float)Math.Ceiling(game.random.NextDouble() * 10), 0, game.C_holder.maxColors - 1);
                    foundColor = true;

                    foreach (BasicOrb orb in game.Orbs)
                        if (orb.relevent && orb != this)
                        {
                            if (orb.MyController.ColorTaken == NewColor)
                                foundColor = false;
                        }

                }
                MyController.ColorTaken = NewColor;
                MyController.colorVec = game.C_holder.colorVecs[NewColor];
                MyController.color = game.C_holder.colors[NewColor];
            }
            else
            {
                Vector4 vec=Vector4.Zero;
                if (Team == 0)
                    vec = new Vector4(1, 0, 0, 1);
                if (Team == 1)
                    vec = new Vector4(0, 0, 1, 1);

               // MyController.ColorTaken = NewColor;
                MyController.colorVec = vec;
                MyController.color = new Color(vec);
            }

            DrawColor = new Vector3((float)(Math.Max(MyController.colorVec.X, WhiteMax)), (float)(Math.Max(MyController.colorVec.Y, WhiteMax)), (float)(Math.Max(MyController.colorVec.Z, WhiteMax))) * 1.25f;

        }
    }
}
