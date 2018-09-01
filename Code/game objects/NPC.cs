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

namespace Orb
{
    public class NPC
    {
        public Vector3 Position = Vector3.Zero;
        public bool Relevent = false;
        public float Size = 50;
        public bool hasMadeSound = false;
        public Model model;
        public LightObject[] NewDrawList = new LightObject[16];
        public float SpeedPoints = 28;
        public int NewDrawNumb = 0;
        Vector3 PositionPrevious;
        public Vector3 Velocity = Vector3.Zero;
        float GrenadeGravity=0.05f;
        public bool HasPlayedTurretSound = false;
        BasicOrb BestTarget;
        float Ysize = 50;
        public BlendState blendstate;
        public Vector3 Rotation;
        public int Type = 0;
        Vector2 Size2 = new Vector2(0, 0);
        public float Growth = 0;
        public float TimeAlive = 0;
        public float TurretRotation;
        public BasicOrb TurretTargetOrb;
        public float MaxTimeAlive = 0;
        public float Reps=0;
        public bool Ready;
        public float Life;
        public float GrenadeGravityAdd=0;
        public float LastHit = 0;
        public bool Alive = true;
        public BasicOrb creator;
        public float TurretROF = 0;
        public float TurretMaxROF = 3;
        public float TurretShots = 0;
        public float TurretReload = 0;
        public float TurretMaxShots = 30;
        public float TurretMaxReload = 20;
        public bool[] Visible = new bool[4];
        public bool Hittable = false;
        public BoundingSphere bounders= new BoundingSphere();
        public float Damage = 0;

        public float pushDamage=0;
        public float pushDirection = 0;
        public float PushVelMult = 0;
        public float AddDamage = 0;
        public float PopWave = 0;
        public bool GiveEnergy = false;
        public float MinLife = 0;
        public float DuplicateFacing = 0;
        

        public void Create(Game1 game)
        {
            HasPlayedTurretSound = false;
            hasMadeSound = false;
            TurretShots = 0;
            TurretROF = 0;
            TurretReload = -TurretMaxReload;
            GiveEnergy = false;
            Damage = 0;
            AddDamage = 0;
            
            TimeAlive = 0;
           
            if (Type == 0)
            {
                Alive = true;
                Growth = 0;
                model = game.Loader.DomeModel;
                MinLife = 0;
                blendstate = BlendState.Additive;
                MaxTimeAlive = 600;
                TimeAlive = 0;
                Hittable = false;
                Rotation = Vector3.Zero;
                PopWave = 0;
                bounders.Radius = 300;
                Size = 300;
            }
            if (Type == 1)
            {
                Alive = true;
                model = game.Loader.PlayerRing;
                blendstate = BlendState.Opaque;
                Hittable = true;
                bounders.Radius = 50;
                Size = 50;
            }
            if (Type == 2)
            {
                Alive = true;
                model = game.Loader.TurretModel;
                blendstate = BlendState.Opaque;
                Hittable = true;
                bounders.Radius = 50;
            }
        }

        public void Update(Game1 game,GameTime gametime)
        {
            bounders.Center = Position;
            bounders.Radius = 50;
            #region Type 0 Dome Shield
            if (Type == 0)
            {
                MinLife += 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
                if (PopWave > 0)
                {
                    PopWave += 0.1f * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
                if(PopWave<3)
                    foreach(NPC npc in game.Npcs)
                        if(npc.Relevent&&npc.Alive&&npc.Type==0)
                            if(npc!=this)
                            if (Vector3.Distance(Position,npc.Position)<Size*PopWave)
                                if (npc.PopWave > 3 || npc.TimeAlive < TimeAlive)
                                {
                                    PopWave = 0;
                                    npc.TimeAlive = npc.MaxTimeAlive + 1;
                                    float Time = Math.Min(45, npc.TimeAlive / 10);
                                   // npc.creator.PhasePause = npc.TimeAlive;
                                  //  npc.creator.pushTime = npc.TimeAlive;
                                    npc.creator.AIPhasePause = 60;
                                }
                }
                LastHit -= 0.1f * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
                //Rotation.Y += 0.01f;
                if (MinLife > 60)
                {
                    if (Damage > 0)
                    {
                        AddDamage += Damage;
                        Damage = 0;
                    }
                }
                else
                    Damage = 0;
                if (AddDamage > 40)
                    TimeAlive = MaxTimeAlive + 1;

                Growth += 0.05f * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
                if (Growth > 1&&TimeAlive<MaxTimeAlive)
                    Growth = 1;
                if (TimeAlive >= MaxTimeAlive)
                    if (!hasMadeSound)
                    {
                        hasMadeSound = true;
                        game.PlaySound(game.soundHolder.soundEffects["shield_colapse"],creator.Position);
                        foreach (LocalPlayer player in game.Localplayers)
                            if (player.Relevent)
                                player.SetVibration(-0.05f, 1, 0.5f, Position, 800);
                    }

                TimeAlive += 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
                if (game.gamemode == Game1.GameMode.Assasin && creator.IsAssasin)
                    TimeAlive += 3;
                if (TimeAlive > MaxTimeAlive)
                {
                    
                    if (Growth > 2)
                        Relevent = false;
                }
                //if(false)
                if (Growth <1.1f)
                {
                    //if(!creator.IsPhasing)
                    //Position = new Vector3(creator.Position.X, -50, creator.Position.Z);

                    bool Alive=false;
                    foreach(BasicOrb orb in game.Orbs)
                        if(orb.Alive&&orb.relevent)
                            
                            {
                                
                                float TempDist = Vector3.Distance(Position, orb.Position);
                                if (TempDist < Size)
                                {
                                    if (orb == creator||orb.Team==creator.Team)
                                    Alive = true;
                                    else 
                                    {
                                        orb.Damage = 1;
                                        orb.LastDamager = creator.ID;
                                        orb.pushDamage = 10;
                                        orb.pushDirection = -(float)Math.Atan2(orb.Position.X - Position.X, orb.Position.Z - Position.Z);
                                        orb.PushVelMult = 0.5f;
                                    }
                                }
                            }
                    if (!Alive)
                    {
                        TimeAlive = MaxTimeAlive + 1;
                       // if(!GiveEnergy)
                       // creator.Energy += 25;
                        GiveEnergy = true;
                    }
                    if(false)
                    foreach (NPC orb in game.Npcs)
                        if(orb!=this)
                        if (orb.Alive && orb.Relevent)
                            if(orb.Type==0)
                            {
                                float TempDist = Vector3.Distance(Position, orb.Position);
                                if (TempDist < Size*3)
                                {
                                    orb.TimeAlive = orb.MaxTimeAlive + 1;
                                    TimeAlive = orb.MaxTimeAlive + 1;
                                }
                            }

                }

            }
            #endregion

            #region Type 1 (Duplicate)
            if (Type == 1)
            {
                TimeAlive += 1;
                if (TimeAlive > MaxTimeAlive)
                    Relevent = false;
            }
            #endregion

            #region Type 2 Turret
            if (Type == 2)
            {
                MoveAsGrenade(game);
                Aim(game);
                if (TurretTargetOrb!=null)
                TurretRotation = -(float)Math.Atan2(TurretTargetOrb.Position.X - Position.X, TurretTargetOrb.Position.Z - Position.Z) - MathHelper.ToRadians(90);

                AddDamage += 0.4f * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
                    if (Damage > 0)
                    {
                        AddDamage += Damage;
                        Damage = 0;
                    }
                    if (AddDamage > 150)
                        Relevent = false;
                    TurretROF += 0.5f * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
                    TurretReload += 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
                if(TurretReload>TurretMaxReload)
                {
                    TurretROF=0;
                    TurretShots=TurretMaxShots;
                    TurretReload=0;
                    
                }
                if (TurretShots > 0&&TurretROF>TurretMaxROF)//&&TurretTargetOrb!=null)
                {
                    TurretROF = 0;
                    TurretShots -= 1;
                    if (TurretShots < 1)
                        Relevent = false;
                    TurretReload = 0;

                    bool found = false;

                    foreach (Bullet shot in game.Bullets)
                        if (!found)
                            if (!shot.Relevent)
                            {
                               game.PlaySound(game.soundHolder.soundEffects["laser_shot"+game.random.Next(2).ToString()], Position);
                               if (!HasPlayedTurretSound)
                               {
                                   game.PlaySound(game.soundHolder.soundEffects["turret_activate"], Position);
                                   HasPlayedTurretSound = true;
                               }



                                float angleOffset = -MathHelper.ToRadians(85+game.random.Next(11));
                                shot.Relevent = true;
                                shot.Position = Position;
                                shot.PulseArea = 0;
                                shot.CreatorID = creator.ID;
                                found = true;
                                shot.Velocity = Vector3.Zero;
                                shot.Creator = creator;
                                shot.TimeAlive = 0;
                                shot.MaxTimeAlive = 30;
                                shot.ExplosionDamage = 0;
                                shot.ExplosionPush = 0;
                                shot.ExplosionPushVelMult = 0;
                                shot.ExplosionSize = 0;
                                shot.bounces = 0;
                                float ShotRotation = -TurretRotation+angleOffset;
                                shot.Velocity = new Vector3(
                                     10 * (float)Math.Sin(ShotRotation), 0,
                                     10 * (float)Math.Cos(ShotRotation)
                                     );
                                shot.SpeedPoints = 80;
                                shot.LightColor = new Vector3(1, 0.35f, 0.75f);
                                shot.LightDistance = 300;
                                shot.Type = 17;
                                shot.Damage = 40;
                                shot.Push = 1;
                                shot.PushVelMult = 1;
                               

                                shot.Spawn(game);
                            }
                }

            }
            #endregion

        }

        public void MoveAsGrenade(Game1 game)
        {

            PositionPrevious = Position;
            float TempSpeedPoints = SpeedPoints;
            Reps = 0;

            #region move

            while (TempSpeedPoints > 0 && Reps < 20 && Relevent)
            {

                float CollisionY = 0; ;
                Reps += 1;
                Rotation.Y = -(float)Math.Atan2(Velocity.X, Velocity.Z) + MathHelper.ToRadians(90);
                Vector3 ToPosition = Position + Velocity / 10 * Math.Min(TempSpeedPoints, 10);

                TempSpeedPoints -= 10;
                bool PlaceFree = true;
                foreach (Block block in game.ActiveBlocks)
                    if (PlaceFree)
                        if (block.Relevent)
                            if (block.alive)
                                if (game.B_Holder.BlockSolid[block.Type])
                                    if (ToPosition.X + Size2.X / 2 + block.Size.X / 2 > block.Position.X && ToPosition.X - Size2.X / 2 - block.Size.X / 2 < block.Position.X)
                                        if (ToPosition.Z + Size2.Y / 2 + block.Size.Y / 2 > block.Position.Z && ToPosition.Z - Size2.Y / 2 - block.Size.Y / 2 < block.Position.Z)
                                        {
                                            GrenadeGravityAdd += 0.005f;
                                            if (ToPosition.Y + Ysize / 2 + game.B_Holder.BlockSize[block.Type].Y / 2 > block.Position.Y && ToPosition.Y - Ysize / 2 - game.B_Holder.BlockSize[block.Type].Y / 2 < block.Position.Y)
                                            {

                                                while (Life > 0 && block.life > -1)
                                                {
                                                    Life -= 10;
                                                    block.life -= 10;
                                                }
                                                if (block.life < 0)
                                                    if (block.Destructable)
                                                        block.alive = false;
                                                if (block.alive)
                                                {
                                                    PlaceFree = false;
                                                    CollisionY = block.Position.Y + game.B_Holder.BlockSize[block.Type].Y / 2;
                                                }

                                            }
                                        }


                bool HitNpc = false;
                foreach (NPC npc in game.Npcs)
                    if (npc.Relevent && npc.Alive)
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
                                    if (Relevent)
                                    {
                                        Velocity = -Velocity * 0.4f;
                                        Velocity.Y = -Velocity.Y;
                                    }
                                }
                            }
                        }
                    }


                if (!PlaceFree&&!HitNpc)
                {
                    Position.Y = ToPosition.Y;
                    Velocity.Y -= (GrenadeGravity + GrenadeGravityAdd) * 2.5f;
                    if (Position.Y < -50)
                    {
                        //Ready += 1;
                        Position.Y = -50;
                    }
                    if (Velocity.Y < 0)
                    {
                        if (Position.Y > CollisionY - Math.Abs(Velocity.Y) - 1)
                        {
                            Velocity.Y = 0;
                            Velocity /= 1.025f;
                            GrenadeGravityAdd += 0.005f;
                            Position += Velocity;
                        }
                        else
                        {
                            Velocity /= 1.5f;
                            Vector3 TempPosition = Position;
                            Vector3 PrevTempPosition;
                            for (int b = 0; b < 10; b++)
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

                                foreach (Block block in game.Blocks)
                                    if (block.Relevent)
                                        if (block.alive)
                                            if (game.B_Holder.BlockSolid[block.Type])
                                                if (TempPosition.X + Size2.X / 2 + block.Size.X / 2 > block.Position.X && TempPosition.X - Size2.X / 2 - block.Size.X / 2 < block.Position.X)
                                                    if (TempPosition.Z + Size2.Y / 2 + block.Size.Y / 2 > block.Position.Z && TempPosition.Z - Size2.Y / 2 - block.Size.Y / 2 < block.Position.Z)
                                                    {


                                                        Position = PrevTempPosition;
                                                        Vector3 VelocityMult = Vector3.Zero;
                                                        if (Math.Abs(TempPosition.X - block.Position.X) / block.Size.X < Math.Abs(TempPosition.Z - block.Position.Z) / block.Size.Y)
                                                        {
                                                            if (TempPosition.Z > block.Position.Z)
                                                                Velocity.Z = Math.Abs(Velocity.Z);
                                                            else
                                                                Velocity.Z = -Math.Abs(Velocity.Z);
                                                        }
                                                        else
                                                        {
                                                            if (TempPosition.X > block.Position.X)
                                                                Velocity.X = Math.Abs(Velocity.X);
                                                            else
                                                                Velocity.X = -Math.Abs(Velocity.X);
                                                        }

                                                    }
                                Position.X = Position.X + Velocity.X;
                                Position.Z = Position.Z + Velocity.Z;
                            }
                        }
                    }

                }
                else
                {
                    if (Velocity != Vector3.Zero)
                        Velocity.Y -= GrenadeGravity;
                    Position = ToPosition;
                    if (Position.Y < -25)
                    {

                        Position.Y = -25;
                        Velocity /= 1.0125f;


                    }


                    if (game.LocalPlayerNumb == 1)
                        if (Position.Y > -20)
                            for (int p = 0; p < 4; p++)
                                if (Visible[p])
                                {
                                    game.SmokeParticles[p].AddParticle(Position, Vector3.Zero);

                                }


                }

            }
            #endregion


            //explode(game);

        }

        void Aim(Game1 game)
        {
            #region AimHelp

            //TurretTargetOrb = null;
            bool AutoAiming=false;

            float AimDist = 1400;
            {
                float TempDire;
                bool FoundTarget = false;
                Vector3 ToPosition;
               

                foreach (BasicOrb orb in game.Orbs)
                    if (orb.relevent)
                        if (orb.Alive)
                            if (orb.Alpha > 0)
                                if (orb != creator)
                                {
                                    float TempDist = Vector3.Distance(Position, orb.Position);
                                    bool FoundWall = false;

                                    if (TempDist < AimDist)
                                    {
                                        TempDire = -(float)Math.Atan2(orb.Position.X - Position.X, orb.Position.Z - Position.Z) + MathHelper.ToRadians(270);

                                        //float AimDire = MathHelper.ToDegrees(Math.Min(Math.Abs(Rotation.Y - TempDire), Math.Abs((Rotation.Y - TempDire) + MathHelper.ToRadians(360))));
                                        
                                       // if (AimDire < AimHelpAmount * game.Holder.AimHelpAmount[GunCurrent])
                                        {
                                           // if (AimDire < Math.Min(AimHelpAmount, BaseAimHelpAmount))
                                           //     IsAutoShooting = true;
                                            //game.TestValue = MathHelper.ToDegrees( Math.Abs(TempDire -MathHelper.ToRadians(90)));
                                            //if (IsControlled)
                                            // game.TestValue = MathHelper.ToDegrees(Math.Min(Math.Abs(Rotation.Y - TempDire), Math.Abs((Rotation.Y - TempDire) +MathHelper.ToRadians(360))));

                                            Vector3 TempPos = Position;

                                            float reps = 200;
                                            while (Vector3.Distance(TempPos, orb.Position) > 100 && !FoundWall && reps > 0)
                                            {
                                                reps -= 1;



                                                TempPos += (new Vector3(50 * (float)Math.Sin(-TempDire + MathHelper.ToRadians(270)), 0, 50 * (float)Math.Cos(-TempDire + MathHelper.ToRadians(270))));
                                                ToPosition = TempPos;
                                          
                                    
                                                        foreach (Block block in game.Blocks)
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
                                            //if (!FoundWall)
                                            {
                                                BestTarget=orb;
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
                    AutoAiming = true;
                     TurretTargetOrb = BestTarget;

                    //TargetDire = TempDire;//
                }
            }

            #endregion
        }


        public void Draw(Game1 game, LocalPlayer player)
        {

            if (Type == 0)
                    DrawModel(game, model, player, Position, Rotation, 1, Vector3.One*Growth*3, game.Loader.Fresnel2, new Vector3(0.2f,0.4f,0.8f)*(3-Growth*1.5f));
            if (Type == 2)
            {
                game.FindLights(Position, 100, false, true,bounders);
                NewDrawList = game.ReturnLightList;
                NewDrawNumb = game.ReturnLightNumb;

                game.Draw(model, player, Position, Rotation, 1, new Vector3(0.5f, 0.5f, 0.5f), NewDrawNumb, NewDrawList, Vector2.One, DepthStencilState.Default, Vector3.One);
                }
        }

        public void DrawModel(Game1 game, Model mod, LocalPlayer player, Vector3 pos, Vector3 Rot, float Alpha, Vector3 Scale, Effect effect, Vector3 color)
        {
            game.GraphicsDevice.BlendState = BlendState.Additive;
            game.GraphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
            foreach (ModelMesh mesh in mod.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    Matrix world = Matrix.CreateScale(Scale) * Matrix.CreateFromYawPitchRoll(-Rot.Y, Rot.X, Rot.Z) *
                Matrix.CreateTranslation(pos);
                    part.Effect = effect;
                    effect.Parameters["World"].SetValue(world);
                    effect.Parameters["View"].SetValue(player.playerView);
                    effect.Parameters["Projection"].SetValue(player.playerProjection);
                    effect.Parameters["Color"].SetValue(new Vector4(color, 1));
                    if(effect!=game.Loader.AmbientTexture)
                    effect.Parameters["ViewPosition"].SetValue(new Vector4(player.CameraPos, 0));
                    effect.Parameters["Texture"].SetValue(game.Loader.HexTexture);
                }
                mesh.Draw();
            }
        }

        public void DrawModel(Game1 game, Model mod, LocalPlayer player, Vector3 pos, Vector3 Rot, float Alpha, Vector3 Scale, Effect effect, Vector3 color,Texture2D Texture)
        {
            game.GraphicsDevice.BlendState = BlendState.Additive;
            game.GraphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
            foreach (ModelMesh mesh in mod.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    Matrix world = Matrix.CreateScale(Scale) * Matrix.CreateFromYawPitchRoll(-Rot.Y, Rot.X, Rot.Z) *
                Matrix.CreateTranslation(pos);
                    part.Effect = effect;
                    effect.Parameters["World"].SetValue(world);
                    effect.Parameters["View"].SetValue(player.playerView);
                    effect.Parameters["Projection"].SetValue(player.playerProjection);
                    effect.Parameters["Color"].SetValue(new Vector4(color, 1));
                    if (effect != game.Loader.AmbientTexture)
                        effect.Parameters["ViewPosition"].SetValue(new Vector4(player.CameraPos, 0));
                    effect.Parameters["Texture"].SetValue(Texture);
                }
                mesh.Draw();
            }
        }
    }
}
