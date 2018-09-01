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
    public class Bullet
    {
        public bool DeliveredDamage = false;
        public float mintime = 0;
        public Vector3 Position=Vector3.Zero;
        public float Ready = 0;
        public int MinDrawTime = 0;
        public Vector3 PositionPrevious = Vector3.Zero;
        public Vector3 Velocity = Vector3.Zero;
        public bool BlackHoldGrowing = false;
        public bool seeking = false;
        public Model BulletModel;
        public DynamicLightObject PersonalLight;
        public Matrix ScaleMatrix;
        public float MaxSeekRadious = 300;
        public float BlackHoleExpand=1.5f;
        public float ParticleTimer = 0;
        public float StartSeekRadious = 250;
        public Vector4 MyColor = Vector4.One;
        public const int CollisionQuality = 40;
        public BasicOrb TargetOrb;
        public Model MyModel;
        public float SeekRadious;
        public bool hasLight = false;
        public float BlackHoleSize = 0;
        EffectDrawObject MyEffect;
        public float StartBlackHoleSize = 50;
        public float BlackHoleRange = 10;
        public float BlackHoldShrink = 0.1f;
        public float BlackHoleGrow = 1;
        public bool NeedLight = true;
        public float LightDistance = 0;
        public Vector3 LightColor = Vector3.Zero;
        public Vector2 Size;
        public bool IsStuck = false;
        public BasicOrb StuckTo;
        public Vector3 Rotation = Vector3.Zero;
        public Vector3 RotMod =  new Vector3(0, MathHelper.ToRadians( -90), 0);
        public BasicOrb Creator;
        public float SpeedPoints = 50;
        public int TimeAlive = 0;
        public float MaxTimeAlive = 0;
        public bool Relevent = false;
        public int bounces = 0;
        public int Maxbounces = 1;
        public float Push = 0;
        public float PushVelMult = 0;
        public float Charge = 0;
        public Vector3 StuckDist = Vector3.Zero;
        public int BounceTimer = 0;

        
        public int Type = 0;
        public int Reps = 0;
        public BoundingSphere bounders=new BoundingSphere();
        public int Life = 260;
        public int Ysize = 50;
        public float RailMinTime = 0;
        public float GrenadeGravity = 0.1f;
        public float GrenadeGravityAdd = 0;
        public float ExplosionSize = 0;
        public float ExplosionDamage = 0;
        public float ExplosionPush = 0;
        public float ExplosionPushVelMult = 0;

        public bool ApplyRotation = false;
        public Vector3 Scale=Vector3.One;
        public float Damage = 0;
        public int LightCreatedTime = 0;
        public bool ShouldSparkLight = true;
        public bool[] Visible = new bool[4];
        public int CreatorID = 0;
        public float PulseSize = 0;
        public float PulseArea = 600;
        public float TimeCharging = 0;
        public float MaxTimeCharging = 20;
        public float MaxTimeSwinging = 0;
        public float SwingAmount = 0;
        public bool IsSwinging = false;
        public float TargetSize = 200;
        public float MaxTargetSize = 4;
        public float TargetTime;
        public float MaxProjectiles = 6;
        public float Projectiles = 0;
        public Vector3 AirStrikeTarget = Vector3.Zero;
        public bool IsExplosiveDeath = false;

        public bool HasGeneratedMatrix = false;
        public Matrix WorldMatrix;
        

        public delegate void UpdateAction(Game1 game, GameTime gametime);
        UpdateAction updateAction;

        public delegate void DrawAction(Game1 game, LocalPlayer player);
        public DrawAction drawAction;


        public void PlayDeathSound(Game1 game)
        {
            if (Type == 1 || Type == 15)
                game.PlaySound(game.soundHolder.soundEffects["machine_grenade_explosion"], Position);
            else if (Type == 5)
                game.PlaySound(game.soundHolder.soundEffects["plasma_explosion"], Position);
            else if (Type == 9)
                game.PlaySound(game.soundHolder.soundEffects["laser_grenade_explode"], Position);
            if (Type == 1 || Type == 15 || Type == 5 || Type == 9)
                foreach (LocalPlayer player in game.Localplayers)
                    if (player.Relevent)
                        player.SetVibration(-0.05f, 1, 0.5f, Position, 800);

        }

        public void PlayWallSound(Game1 game)
        {
            //if (false)
            
                if (Type == 0 || Type == 14 || Type == 16)
                    game.PlaySound(game.soundHolder.soundEffects["bullet_impact_wall" + game.random.Next(3).ToString()], Position);
                else if (Type == 4)
                    game.PlaySound(game.soundHolder.soundEffects["plasma_hit_wall" + game.random.Next(2).ToString()], Position);
                   
        }

        public void PlayPlayerSound(Game1 game)
        {
           // if (false)
            
                if (Type == 0 || Type == 14 || Type == 16||Type==17)
                    game.PlaySound(game.soundHolder.soundEffects["bullet_impact_player" + game.random.Next(2).ToString()], Position);
                else if (Type == 4||Type==13)
                    game.PlaySound(game.soundHolder.soundEffects["plasma_hit_player" + game.random.Next(2).ToString()], Position);
                       
        }


        public void Update(Game1 game,GameTime gametime)
        {


            MinDrawTime += 1;
            if (Ready > 0)
                Ready += 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
            bounders.Center = Position;
            bounders.Radius = 20;

            LightCreatedTime += 1;
            //if(!IsStuck)
            //Rotation.Y= -(float)Math.Atan2(Position.X-PositionPrevious.X,Position.Z-PositionPrevious.Z)+MathHelper.ToRadians(90);

            updateAction(game, gametime);

            if (false)
            {
                if (Type == 0 || Type == 13 || Type == 15 || Type == 16 || Type == 17 || Type == 8 || Type == 9 || Type == 4)
                    MoveAsBullet(game, gametime);
                if (Type == 1 || Type == 5 || Type == 12 || Type == 14 || Type == 18 || Type == 10)
                    MoveAsGrenade(game, gametime);

                if (Type == 2)
                    MoveAsPulse(game);
                if (Type == 3)
                    MoveAsSword(game);
                if (Type == 4)
                    MoveAsBullet(game, gametime);
                if (Type == 6)
                    MoveAsTarget(game);
                if (Type == 7)
                    MoveAsAirStrike(game);
                if (Type == 11)
                {
                    MoveAsBlackHole(game);
                }
            }

                BounceTimer += 1;
            TimeAlive += 1;
            if (TimeAlive > MaxTimeAlive)
            {
                if(IsExplosiveDeath)
                ShouldSparkLight = false;
                Die(game,false);
                ShouldSparkLight = true;
            }
            if(LightDistance>0)
               // if(game.LocalPlayerNumb==1)
                    if(NeedLight)
            GetLight(game);
        }

        public void GetLight(Game1 game)
        {
            if(Relevent)
            if (Visible[0] || Visible[1] || Visible[2] || Visible[3])
            {
                if (!hasLight)
                {

                    foreach (DynamicLightObject light in game.DynamicLights)
                        if (!hasLight)
                            if (!light.Relevent)
                            {
                                light.Relevent = true;
                                light.Position = Position;
                                light.Color = LightColor;
                                light.Distancee = LightDistance;
                                light.LifeTime = 0;
                                light.ConstUpdate = true;
                                light.LimitedLifetime = false;
                                hasLight = true;
                                PersonalLight = light;
                                light.IsSpot = true;
                                light.NeedShadows = false;
                                light.Brightness = 1;
                                light.RecalculateLights(game);
                                light.Create(game);
                            }
                }
                else
                {
                    PersonalLight.Position = Position;
                    PersonalLight.UpdateMatrix();
                }
            }
            else
            {
                if (hasLight)
                {
                    PersonalLight.Relevent = false;
                    hasLight = false;
                }
            }
        }

        public void MoveAsBlackHole(Game1 game)
        {
            
            {
                if (!BlackHoldGrowing)
                {
                    BlackHoleSize -= BlackHoldShrink;
                    if (BlackHoleSize < 0)
                        Die(game, false);
                }
                //else
                if(false)
                    foreach (Bullet bull in game.Bullets)
                        if (bull.Relevent)
                            if (bull.Type != this.Type)
                            {
                                float Dist = Vector3.Distance(Position, bull.Position);
                                if (Dist < BlackHoleSize * BlackHoleRange)
                                {
                                    if (true)
                                        bull.Die(game, false);
                                    else
                                    {
                                        float Mult = (BlackHoleSize * BlackHoleRange - Dist) / (BlackHoleSize * BlackHoleRange);
                                        Vector3 Aimer = (Vector3.Normalize(Position - bull.Position));
                                        float velPrev = Vector3.Distance(Vector3.Zero, bull.Velocity);
                                        bull.Velocity += Aimer * Mult;
                                        bull.Velocity = Vector3.Normalize(bull.Velocity) * velPrev;
                                    }
                                }

                            }
            foreach (BasicOrb orb in game.Orbs)
            if(orb.relevent&&orb.Alive&&!orb.IsPhasing)
            {
                float Dist = Vector3.Distance(Position, orb.Position);
                if (Dist < BlackHoleSize * BlackHoleRange)
                {
                   // if(false)
                    //if (Dist < 50)
                    {
                        orb.Damage += 10;
                        orb.LastDamager = CreatorID;
                        orb.DamageFrom[CreatorID] += 10;
                    }
                    //else
                    {
                        Charge += 1;
                        //if(Charge>14)
                        {
                            Charge = 0;
                            float Mult = (BlackHoleSize * BlackHoleRange - Dist) / (BlackHoleSize * BlackHoleRange);
                            Vector3 Aimer = (Vector3.Normalize(Position - orb.Position)) * 1;
                            //orb.PushVelocity += Aimer * Mult;
                            //   if (Vector3.Distance(Vector3.Zero, orb.PushVelocity) > 4)
                            //      orb.PushVelocity = Vector3.Normalize(orb.PushVelocity) * 4;
                            orb.pushDamage = 1;
                            orb.pushTime = 10;
                            orb.Damage = 10;
                            orb.DamageFrom[CreatorID] += 10;
                            //orb.pushDirection = -(float)Math.Atan2(orb.Position.X - Position.X, orb.Position.Z - Position.Z) + MathHelper.ToRadians(180);

                            orb.LastShootTime = 0;
                            orb.LastShooter = Creator;
                            orb.PushVelMult = PushVelMult;
                            //if (orb.TeamImunityActive)
                            //    Damage /= 1.25f;
                            if (Creator.HitBounce < 0)
                                Creator.HitBounce = Creator.MaxHitBounce;
                        }
                    }
                }
            }
            }
            if (BlackHoldGrowing)
            {
                BlackHoleSize += BlackHoleExpand;
                if (BlackHoleSize > StartBlackHoleSize)
                {
                    BlackHoleSize = StartBlackHoleSize;
                    BlackHoldGrowing = false;
                }
            }

        }

        public void MoveAsTarget(Game1 game)
        {
            if (seeking)
            {
                if (Creator.Alive && Creator.relevent)
                    if (TargetOrb.Alive && TargetOrb.relevent)
                        seeking = false;
                    //Position = TargetOrb.Position;

                    else
                        seeking = false;
            }
            TargetSize -= 1;
            if (TargetSize < 0)
                TargetSize = 0;
            TargetTime += 1;

            if (TargetTime > MaxTargetSize * 2)
            {
                TargetTime -= 15;

                bool found = false;

                foreach (Bullet shot in game.Bullets)
                    if (!found)
                        if (!shot.Relevent)
                        {
                            Projectiles += 1;
                            if (Projectiles > MaxProjectiles)
                                Relevent = false;
                            shot.Relevent = true;
                            shot.Size = Vector2.Zero;
                            shot.Position = Position+new Vector3(0,2000,700);
                            shot.CreatorID =Creator.ID;
                            found = true;
                            shot.Damage = 0;

                            shot.SpeedPoints = 50;
                            shot.Velocity = Vector3.Zero;
                            shot.ExplosionDamage = 450;
                            shot.AirStrikeTarget = Position;
                            shot.ExplosionSize = 800;
                            shot.ExplosionPush = 1;
                            shot.Rotation.Y = 0;
                            shot.Creator = Creator;
                            shot.TimeAlive = 0;
                            shot.Push = 0;
                            shot.PushVelMult = 0;
                            shot.LightColor = new Vector3(4, 2, 1) * 4;
                            shot.LightDistance = 1000;
                            shot.ExplosionPushVelMult = 0.1f;


                            shot.MaxTimeAlive = 80;
                            shot.bounces = 0;
                            shot.Type = 7;

                            shot.Velocity.Y = 0;
                            shot.Spawn(game);
                        }

            }

        }

        public void MoveAsAirStrike(Game1 game)
        {
            Position += Vector3.Normalize(AirStrikeTarget- Position)*SpeedPoints;
            if (Position.Y < -25)
            {
                Die(game, false);
            }
        }

        public void MoveAsBullet(Game1 game,GameTime gametime)
        {
            if (Type == 9)
            {
                SpeedPoints -= 0.5f * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
            }

            ParticleTimer += 1.5f * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
            if (Type == 4 && ParticleTimer > 2 || Type == 5 && ParticleTimer > 3 || Type == 9 && ParticleTimer >3 || Type == 13 && ParticleTimer > 3)
            {
                for (int p = 0; p < 4; p++)
                    if (Visible[p])
                    {
                        if (Type != 4)
                            game.PlasmaParticles[p].AddParticle(Position, Vector3.Zero);
                        else
                            game.GreenParticle[p].AddParticle(Position, Vector3.Zero);

                    }
                ParticleTimer = 0;
            }
            if(Type==15)
                for (int p = 0; p < 4; p++)
                    if (Visible[p])
                    {
                        game.FireParticles[p].AddParticle(Position, Vector3.Zero);

                    }

            PositionPrevious = Position;
            float TempSpeedPoints = SpeedPoints * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
            Reps = 0;

            while (TempSpeedPoints > 0&&Reps<10)
            {
                RailMinTime += 1;
                //if (Type == 8)
                explode(game);
                if(Type==16&&TimeAlive>MaxTimeAlive/2)
                SeekEnemies(game);
                if (IsStuck)
                    Reps = 400;
                else

                {
                Reps += 1;
                Rotation.Y = -(float)Math.Atan2(Velocity.X, Velocity.Z) + MathHelper.ToRadians(90);
                Vector3 ToPosition = Position + Velocity / 10 * Math.Min(TempSpeedPoints, CollisionQuality);

                TempSpeedPoints -= CollisionQuality;
                bool PlaceFree = true;
               if(Type!=13)
                foreach (Block block in game.ActiveBlocks)
                    if (PlaceFree)
                        if (block.Relevent)
                            if(block.alive)
                               // if(Type!=8||game.B_Holder.BlockStopRail[block.Type])
                                if (game.B_Holder.BlockSolid[block.Type])
                            if (ToPosition.X + Size.X / 2 + block.Size.X / 2 > block.Position.X && ToPosition.X - Size.X / 2 - block.Size.X / 2 < block.Position.X)
                                if (ToPosition.Z + Size.Y / 2 + block.Size.Y / 2 > block.Position.Z && ToPosition.Z - Size.Y / 2 - block.Size.Y / 2 < block.Position.Z)
                                {
                                    if (Type != 9)
                                        PlaceFree = false;
                                    else
                                        SeekRadious = MaxSeekRadious;

                                    block.life -= 10;
                                    if (block.life < 0)
                                        if(block.Destructable)
                                        block.alive = false;
                                    //TempSpeedPoints += SpeedPoints*2;
                                    if (Type != 9)
                                    {
                                        PlayWallSound(game);

                                        if (BounceTimer > 1)
                                        {
                                            Die(game, true);
                                            bounces += 1;
                                        }
                                        BounceTimer = 0;

                                        if (bounces > Maxbounces)
                                        {
                                            Die(game, false);

                                        }
                                    }
                                }
                bool HitNpc=false;

                foreach(NPC npc in game.Npcs)
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
                            if(HitNpc)
                            {
                                if (Type == 13)
                                    npc.TimeAlive = npc.MaxTimeAlive + 1;
                                PlayWallSound(game);

                                if (npc.Hittable||Type==9)
                                    Die(game, false);
                                else
                                {
                                    if (BounceTimer > 1)
                                    {
                                        Die(game, true);
                                        bounces += 1;
                                    }
                                    BounceTimer = 0;
                                    if (bounces > Maxbounces)
                                    {
                                        Die(game, false);

                                    }

                                    if(Relevent)
                                    {
                                        Die(game, false);
                                    }
                                }
                            }
                        }
                    }

                if (!PlaceFree&&!HitNpc)
                {
                    Vector3 TempPosition = Position;
                    Vector3 PrevTempPosition = TempPosition;

                    for (int b = 0; b < CollisionQuality; b++)
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


                                            float TempAngle = -(float)Math.Atan2(Velocity.X, Velocity.Z) + MathHelper.ToRadians(90);

                                          

                                            if (Math.Min(Math.Min(Math.Abs(TempAngle - Rotation.Y), Math.Abs(TempAngle - Rotation.Y - MathHelper.ToRadians(360))), Math.Abs(TempAngle - Rotation.Y + MathHelper.ToRadians(360))) > MathHelper.ToRadians(150))
                                            {
                                                Die(game, false);

                                            }


                                        }
                    }
                }
                else
                {
                    Position = ToPosition;
                    //if(Relevent)
                    if(!HitNpc)
                        if(Type!=15)
                    foreach (BasicOrb orb in game.Orbs)
                        if (orb.Alive)
                            if (orb.relevent)
                                if (orb != Creator )
                                    if(!orb.IsPhasing)
                                        if (orb.Team != Creator.Team || game.FrendlyFire)
                                    if (Position.X + Size.X / 2 + orb.Size.X / 2 > orb.Position.X && Position.X - Size.X / 2 - orb.Size.X / 2 < orb.Position.X)
                                        if (Position.Z + Size.Y / 2 + orb.Size.Y / 2 > orb.Position.Z && Position.Z - Size.Y / 2 - orb.Size.Y / 2 < orb.Position.Z)
                                        {

                                            bool Protected = false;

                                            foreach (NPC npc in game.Npcs)
                                                if (npc.Alive && npc.Relevent)
                                                    if (npc.Growth < 1.1f)
                                                    {
                                                        if (Vector3.Distance(npc.Position, orb.Position) < npc.Size)
                                                            Protected = true;

                                                    }

                                            if (orb.Inv > 0)
                                                Protected = true;



                                            if (!Protected)
                                            {
                                                PlayPlayerSound(game);
                                                orb.LastShootTime = 0;
                                                orb.LastShooter = Creator;
                                                orb.PushVelMult = PushVelMult;
                                                //if (orb.TeamImunityActive)
                                                //    Damage /= 1.25f;
                                                orb.Damage = Damage;
                                                orb.pushDamage = Push;
                                                orb.pushDirection = Rotation.Y - MathHelper.ToRadians(90);
                                                Creator.HitBounce = Creator.MaxHitBounce;
                                                if (Type == 13)
                                                {
                                                    
                                                    game.PlaySound(game.soundHolder.soundEffects["player_gain_life"], Creator.Position);
                                                    Creator.life = Math.Min(Creator.MaxLife, Creator.life + Damage / 2);
                                                }
                                                orb.LastDamager = CreatorID;
                                            }
                                            else
                                            {
                                                PlayWallSound(game);
                                            }
                                                Die(game, false);
                                        }
                }
            }


                if (Type == 9||Type==15)
                    foreach (BasicOrb orb in game.Orbs)
                        if (orb.Alive)
                            if (orb.relevent)
                                if (!orb.IsPhasing)
                                    if (orb != Creator)
                                        
                                    {
                                        //if (!Protected)
                                        {

                                            if (Position.X + Size.X / 2 + orb.Size.X / 2 > orb.Position.X && Position.X - Size.X / 2 - orb.Size.X / 2 < orb.Position.X)
                                                if (Position.Z + Size.Y / 2 + orb.Size.Y / 2 > orb.Position.Z && Position.Z - Size.Y / 2 - orb.Size.Y / 2 < orb.Position.Z)
                                                {

                                                    if (TimeAlive < MaxTimeAlive)
                                                        TimeAlive = (int)MaxTimeAlive;
                                                }
                                            float NewSeekRadious = SeekRadious;
                                            if (orb.Alpha != 1)
                                                NewSeekRadious /= 2.5f;

                                            if (Vector3.Distance(orb.Position, Position) <NewSeekRadious)
                                                if (TimeAlive < MaxTimeAlive - 6)
                                                    TimeAlive = (int)MaxTimeAlive - 6;
                                        }
                                    }
        }
        }

        public void MoveAsGrenade(Game1 game,GameTime gametime)
        {
            Rotation.X +=( 0.0125f * (Math.Abs(Velocity.X) + Math.Abs(Velocity.Y) + Math.Abs(Velocity.Z))) * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;

            GrenadeGravityAdd += 0.0035f * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
            if(Type==18)
                GrenadeGravityAdd += 0.0035f * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
            PositionPrevious = Position;
            float TempSpeedPoints = SpeedPoints * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
            Reps = 0;

            #region move

            while (TempSpeedPoints > 0 && Reps < 20&&Relevent)
            {

                float CollisionY = 0; ;
                Reps += 1;
                Rotation.Y = -(float)Math.Atan2(Velocity.X, Velocity.Z) + MathHelper.ToRadians(90);
                Vector3 ToPosition = Position + Velocity / 10 * Math.Min(TempSpeedPoints, CollisionQuality);

                TempSpeedPoints -= CollisionQuality;
                bool PlaceFree = true;
                foreach (Block block in game.ActiveBlocks)
                    if (PlaceFree)
                        if (block.Relevent)
                            if (block.alive)
                                if (game.B_Holder.BlockSolid[block.Type])
                                if (ToPosition.X + Size.X / 2 + block.Size.X / 2 > block.Position.X && ToPosition.X - Size.X / 2 - block.Size.X / 2 < block.Position.X)
                                    if (ToPosition.Z + Size.Y / 2 + block.Size.Y / 2 > block.Position.Z && ToPosition.Z - Size.Y / 2 - block.Size.Y / 2 < block.Position.Z)
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
                                if (npc.Hittable)
                                    Die(game, false);
                                else
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

                if (!PlaceFree)
                {
                    Position.Y = ToPosition.Y;
                    Velocity.Y -= (GrenadeGravity+GrenadeGravityAdd)*2.5f;
                    if (Position.Y < -50)
                    {
                        Ready += 1;
                        Position.Y = -50;
                    }
                    if (Velocity.Y < 0)
                    {
                        if (Position.Y > CollisionY - Math.Abs(Velocity.Y) - 1)
                        {
                            Velocity.Y = 0;
                            Velocity /= 1.025f;
                            GrenadeGravityAdd += 0.005f;
                            Position += new Vector3(Velocity.X / 10 * CollisionQuality, Velocity.Y / 10 * CollisionQuality, Velocity.Z / 10 * CollisionQuality);
                        }
                        else
                        {
                            Velocity /= 1.5f;
                            Vector3 TempPosition = Position;
                            Vector3 PrevTempPosition;
                            for (int b = 0; b < CollisionQuality; b++)
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
                        Velocity.Y -= (GrenadeGravity + GrenadeGravityAdd)/10*CollisionQuality;
                    Position = ToPosition;
                    if (Position.Y < -25)
                    {
                        Ready += 1;
                        Position.Y = -25;
                        Velocity /= 1.15f;// / 10 *CollisionQuality;
                        if (Type == 18)
                            Die(game, false);

                    }


                    //if(game.LocalPlayerNumb==1)
                    ParticleTimer += 1;
                    if(ParticleTimer>2)
                    {
                        if (Position.Y > -20)
                            for (int p = 0; p < 4; p++)
                                if (Visible[p])
                                {
                                    game.SmokeParticles[p].AddParticle(Position, Vector3.Zero);

                                }
                        ParticleTimer = 0;
                    }

                }

            }
            #endregion


            explode(game);

        }

        public void explode(Game1 game)
        {
            #region explode
            // if(false)
            mintime += 1;
            if (Type == 1 || Type == 14 && Ready > 45 || Type == 15)
                foreach (BasicOrb orb in game.Orbs)
                    if (orb.Alive)
                        if (orb.relevent)
                            if (!orb.IsPhasing)
                                if (orb.Alpha == 1)
                                    if (orb != Creator)
                                        if(orb.Team!=Creator.Team)
                                    {
                                        bool Protected = false;

                                        foreach (NPC npc in game.Npcs)
                                            if (npc.Alive && npc.Relevent)
                                                if (npc.Growth == 1)
                                                {
                                                    if (Vector3.Distance(npc.Position, orb.Position) < npc.Size)
                                                        Protected = true;

                                                }
                                        //if (!Protected)
                                        {

                                            if (Position.X + Size.X / 2 + orb.Size.X / 2 > orb.Position.X && Position.X - Size.X / 2 - orb.Size.X / 2 < orb.Position.X)
                                                if (Position.Z + Size.Y / 2 + orb.Size.Y / 2 > orb.Position.Z && Position.Z - Size.Y / 2 - orb.Size.Y / 2 < orb.Position.Z)
                                                {

                                                    if (TimeAlive < MaxTimeAlive - 12)
                                                        TimeAlive = (int)MaxTimeAlive - 12;
                                                }
                                            if (Vector3.Distance(orb.Position, Position) < ExplosionSize)
                                                if (TimeAlive < MaxTimeAlive - 23)
                                                    TimeAlive = (int)MaxTimeAlive - 23;
                                        }
                                    }
            if (Type == 5 || Type == 10 || Type == 12 && mintime > 20 || Type == 14 && Ready > 45)
                SeekEnemies(game);

            #endregion
        }

        public void SeekEnemies(Game1 game)
        {
            bool Protected = false;

            foreach (NPC npc in game.Npcs)
                if (npc.Alive && npc.Relevent)
                    if (npc.Growth == 1)
                    {
                        if (Vector3.Distance(npc.Position, Position) < npc.Size)
                            Protected = true;

                    }

      
            if(!Protected)
            {
            if (!IsStuck)
            {
                //if (Position.Y > -50)
                foreach (BasicOrb orb in game.Orbs)
                    if (orb.Alive)
                        if (orb.relevent)
                            if (!orb.IsPhasing)
                                if (orb.Team != Creator.Team)
                                    if(orb.Alpha==1)
                                    if (orb != Creator)
                                    {
                                        float Distance = Vector3.Distance(orb.Position, Position);
                                        if (Distance < 350||Type==14&&Distance<800||Type==16&&Distance<1000)
                                        {
                                            Vector3 Aimer = (Vector3.Normalize(orb.Position - Position)) * Math.Max(10, Vector3.Distance(Vector3.Zero, Velocity));
                                            Velocity.X = Aimer.X;
                                            Velocity.Z = Aimer.Z;
                                            Velocity = Aimer;
                                        }
                                        if (Distance < 50 )
                                        {
                                            IsStuck=true;
                                            StuckTo = orb;
                                            StuckDist=Position-orb.Position;
                                            if (TimeAlive < MaxTimeAlive - 20)
                                                TimeAlive = (int)MaxTimeAlive - 20;
                                        }
                                    }
            }
            else
            {
                if (!StuckTo.IsPhasing)
                {
                    Position = StuckTo.Position + StuckDist;
                }
                else
                    IsStuck = false;
            }
        }
        }

        public void SeekEnemiesAsRail(Game1 game)
        {
            bool Protected = false;

            foreach (NPC npc in game.Npcs)
                if (npc.Alive && npc.Relevent)
                    if (npc.Growth == 1)
                    {
                        if (Vector3.Distance(npc.Position, Position) < npc.Size)
                            Protected = true;

                    }

            if (!Protected)
            {
                if (!IsStuck)
                {
                    //if (Position.Y > -50)
                    foreach (BasicOrb orb in game.Orbs)
                        if (orb.Alive)
                            if (orb.relevent)
                                if (!orb.IsPhasing)
                                    if (orb.Alpha == 1)
                                    if (orb.Team != Creator.Team)
                                        if (orb != Creator)
                                        {
                                            float Distance = Vector3.Distance(orb.Position, Position);
                                            if (Distance < 60)
                                            {
                                                Vector3 Aimer = (Vector3.Normalize(orb.Position - Position)) * Math.Max(10, Vector3.Distance(Vector3.Zero, Velocity));
                                                Velocity.X = Aimer.X;
                                                Velocity.Z = Aimer.Z;
                                                Velocity = Aimer;
                                            }
                                            if (Distance < 50)
                                            {
                                                if (!DeliveredDamage)
                                                {
                                                    float Mult = Math.Min(1, (RailMinTime) / 60);
                                                    DeliveredDamage = true;
                                                    orb.LastShootTime = 0;
                                                    orb.LastShooter = Creator;
                                                    orb.PushVelMult = PushVelMult*Mult;
                                                    orb.Damage = Damage*Mult;
                                                    orb.pushDamage = Push*Mult;
                                                    orb.pushDirection = Rotation.Y - MathHelper.ToRadians(90);
                                                    Creator.HitBounce = Creator.MaxHitBounce;


                                                    orb.LastDamager = CreatorID;
                                                }

                                                if ( hasLight)
                                                {
                                                    hasLight = false;
                                                    PersonalLight.Relevent = false;
                                                    NeedLight = false;
                                                }

                                                IsStuck = true;
                                                StuckTo = orb;
                                                StuckDist = Position - orb.Position;
                                                if (TimeAlive < MaxTimeAlive - 20)
                                                    TimeAlive = (int)MaxTimeAlive - 20;
                                            }
                                        }
                }
                else
                {
                    if(false)
                    if (StuckTo.Alive && StuckTo.relevent&&!StuckTo.IsPhasing)
                    {
                        Position = StuckTo.Position + StuckDist;
                        TimeAlive = 0;
                    }
                    else
                        TimeAlive = (int)MaxTimeAlive + 1;
                }
            }
        }

        public void MoveAsPulse(Game1 game)
        {

            PulseSize += 0.04f;
            if (PulseSize > 0.75f)
                Relevent = false;
            foreach(BasicOrb orb in game.Orbs)
                if(orb.relevent&&orb.Alive)
                    
                    if (orb != Creator)
                        if(!orb.IsPhasing)
                    {
                        float distance= Vector3.Distance(orb.Position,Position);
                        if (distance < PulseArea * PulseSize)
                        {
                           // float damage = 1;
                            //orb.Damage = damage;
                            //orb.DamageFrom[CreatorID] += damage;
                            //orb.pushDamage = PulseArea * PulseSize - distance;
                            orb.EMPTime = (PulseArea * PulseSize - distance)/3;
                        }
                        
                    }

        }

        public void MoveAsSword(Game1 game)
        {
            TimeCharging += 1;

            

            foreach (BasicOrb orb in game.Orbs)
                if (orb.Alive)
                    if (orb.relevent)
                        if (orb != Creator)
                            if (!orb.IsPhasing)
                                if (Position.X + Size.X / 2 + orb.Size.X / 2 > orb.Position.X && Position.X - Size.X / 2 - orb.Size.X / 2 < orb.Position.X)
                                    if (Position.Z + Size.Y / 2 + orb.Size.Y / 2 > orb.Position.Z && Position.Z - Size.Y / 2 - orb.Size.Y / 2 < orb.Position.Z)
                                    {
                                        if(TimeCharging<MaxTimeCharging-4)
                                        TimeCharging = MaxTimeCharging -3;

                                        orb.LastShootTime = 0;
                                        orb.LastShooter = Creator;
                                        orb.PushVelMult = PushVelMult;
                                        orb.Damage = Damage;
                                        orb.pushDamage = Push;
                                        orb.pushDirection = Rotation.Y - MathHelper.ToRadians(90);
                                        Creator.HitBounce = Creator.MaxHitBounce;


                                        orb.LastDamager = CreatorID;

                                    }


            if (TimeCharging > MaxTimeCharging)
                IsSwinging = true;

            if (IsSwinging)
            {
                if (SwingAmount > MaxTimeSwinging)
                    Relevent = false;
                SwingAmount += 1;
            }
            else
            {
                PositionPrevious = Position;
                float TempSpeedPoints = SpeedPoints;
                Reps = 0;

                while (TempSpeedPoints > 0 && Reps < 20)
                {
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
                                        if(block.PhaseBlock||TimeCharging>MaxTimeCharging-2)
                                        if (ToPosition.X + Size.X / 2 + block.Size.X / 2 > block.Position.X && ToPosition.X - Size.X / 2 - block.Size.X / 2 < block.Position.X)
                                            if (ToPosition.Z + Size.Y / 2 + block.Size.Y / 2 > block.Position.Z && ToPosition.Z - Size.Y / 2 - block.Size.Y / 2 < block.Position.Z)
                                            {
                                                PlaceFree = false;
                                                block.life -= 10;
                                                if (block.life < 0)
                                                    if (block.Destructable)
                                                        block.alive = false;

                                            }
                    bool HitNpc = false;

                    foreach (NPC npc in game.Npcs)
                        if (npc.Relevent && npc.Alive)
                            if (npc.Hittable)
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
                                        if(TimeCharging<MaxTimeCharging-3)
                                        TimeCharging = MaxTimeCharging -3;
                                    }
                                }
                            }

                    if (!PlaceFree)
                    {
                        Vector3 TempPosition = Position;
                        Vector3 PrevTempPosition = TempPosition;

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

                            foreach (Block block in game.ActiveBlocks)
                                if (block.Relevent)
                                    if (block.alive)
                                        if (game.B_Holder.BlockSolid[block.Type])
                                            if (TempPosition.X + Size.X / 2 + block.Size.X / 2 > block.Position.X && TempPosition.X - Size.X / 2 - block.Size.X / 2 < block.Position.X)
                                                if (TempPosition.Z + Size.Y / 2 + block.Size.Y / 2 > block.Position.Z && TempPosition.Z - Size.Y / 2 - block.Size.Y / 2 < block.Position.Z)
                                                {

                                                    if (block.PhaseBlock)
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
                                                    else
                                                        if (TimeCharging > MaxTimeCharging - 5)
                                                            TimeCharging = MaxTimeCharging - 5;



                                                }
                        }
                    }
                    else
                    {
                        Position = ToPosition;

                    }
                }
            }
            Creator.pushTime = 15;
            Creator.Position = Position;
        }

        public void DrawGrenade(Game1 game, LocalPlayer player)
        {
            game.GraphicsDevice.BlendState = BlendState.Opaque;
            DrawModel(MyEffect, MyModel, player);
            game.GraphicsDevice.BlendState = BlendState.Additive;
        }

        public void DrawGeneric(Game1 game, LocalPlayer player)
        {
            if (MinDrawTime > 1)
            DrawModel(MyEffect, MyModel, player);
        }

        public void SetDraw(Game1 game)
        {

            Vector3 Color=Vector3.One;
            if (Type == 0 || Type == 8)
            {
                MyModel = game.ShotModel;
                MyEffect = game.AmbientObject;
                Scale = new Vector3(0.5f, 0.5f, 2);
                Color = Vector3.One;
                drawAction = DrawGeneric;

                ApplyRotation = true;
            }
            if (Type == 1 || Type == 10 || Type == 14 && Ready < 45 || Type == 18)
            {
                MyModel = game.Loader.GrenadeModel;
                MyEffect = game.Fresnel3Object;
                Scale = Vector3.Multiply(Vector3.One, 0.75f);
                Color = new Vector3(4,2,1);
                drawAction = DrawGrenade;
                ApplyRotation = true;
            }

            if (Type == 5)
            {
                MyModel = game.Loader.GrenadeModel;
                MyEffect = game.Fresnel3Object;
                Scale = Vector3.Multiply(Vector3.One, 0.75f);
                Color = new Vector3(1, 4, 2);
                drawAction = DrawGrenade;
                ApplyRotation = true;
            }

            if (Type == 4)
            {
                MyModel = game.Loader.DomeModel2;
                MyEffect = game.Fresnel3Object;
                Scale = Vector3.Multiply(Vector3.One, 0.5f);
                Color = new Vector3(0.5f, 1.25f, 0.75f);
                drawAction = DrawGeneric;
            }

            if (Type == 9)
            {
                MyModel = game.Loader.DomeModel2;
                MyEffect = game.Fresnel3Object;

                float siz = 0.5f;
                Scale = Vector3.Multiply(Vector3.One, siz);

                Color = new Vector3(0.75f, 0.75f, 1.25f);

                drawAction = DrawGeneric;
            }

            if (Type == 17)
            {
                MyModel = game.ShotModel;
                MyEffect = game.AmbientObject;

                Scale = new Vector3(0.5f, 0.5f, 2)*0.75f;

                Color = new Vector3(1f, 0.5f, 1.25f);

                drawAction = DrawGeneric;
                ApplyRotation = true;
            }

            if (Type == 12)
            {
                MyModel = game.Loader.DomeModel2;
                MyEffect = game.Fresnel3Object;

                float siz = 0.35f;
                Scale = Vector3.Multiply(Vector3.One, siz);

                Color = new Vector3(1f, 0.65f, 0.25f);

                drawAction = DrawGeneric;
            }


            if (Type == 13)
            {
                MyModel = game.Loader.DomeModel2;
                MyEffect = game.Fresnel3Object;

                float siz = 0.3f;
                Scale = Vector3.Multiply(Vector3.One, siz);

                Color = new Vector3(0.35f, 0.65f, 1);

                drawAction = DrawGeneric;
            }

            if (Type == 15)
            {
                MyModel = game.Loader.DomeModel2;
                MyEffect = game.Fresnel3Object;

                float siz = 0.4f;
                Scale = Vector3.Multiply(Vector3.One, siz);

                Color = new Vector3(1, 0.65f, 0.2f);

                drawAction = DrawGeneric;
            }


            if (Type == 16)
            {
                MyModel = game.Loader.DomeModel2;
                MyEffect = game.Fresnel3Object;

                float siz = 0.25f;
                Scale = Vector3.Multiply(Vector3.One, siz);

                Color = new Vector3(1, 0.65f, 0.2f);

                drawAction = DrawGeneric;
            }

            MyColor = new Vector4(Color, 1);
            

         }


        public void Draw(Game1 game, LocalPlayer player)
        {

            if (MinDrawTime > 1)
                if (Type == 0 || Type == 8 || Type == 17)
                    if ((float)game.random.NextDouble() > 0.4f)
                    {
                        DrawModel(game, game.ShotModel, player, Position, Rotation + RotMod, 1, new Vector3(0.5f, 0.5f, 2), game.Loader.Ambient, Vector3.One);
                    }
            if (Type == 1 || Type == 10 || Type == 14 && Ready < 45 || Type == 18)
            {
                game.GraphicsDevice.BlendState = BlendState.Opaque;
                DrawModel(game, game.Loader.GrenadeModel, player, Position, Rotation + RotMod, 1, Vector3.One * 0.75f, game.Loader.Fresnel3, new Vector3(4, 2, 1));
                game.GraphicsDevice.BlendState = BlendState.Additive;
            }
            if (Type == 5)
            {
                game.GraphicsDevice.BlendState = BlendState.Opaque;
                DrawModel(game, game.Loader.GrenadeModel, player, Position, Rotation + RotMod, 1, Vector3.One * 0.75f, game.Loader.Fresnel3, new Vector3(1, 4, 2));
                game.GraphicsDevice.BlendState = BlendState.Additive;
            }
            if (Type == 2)
            {
                float size = PulseSize * PulseArea / 50;
                DrawModel(game, game.Loader.Disk, player, Position, Rotation + RotMod, 1, new Vector3(size, size, size), game.Loader.Wave, new Vector3((1 - PulseSize), (1 - PulseSize) * 2, (1 - PulseSize) * 4));
            }
            if (Type == 4)
            {
                float siz = 0.5f;
                game.Npcs[0].DrawModel(game, game.Loader.DomeModel, player, Position, Vector3.Zero, 1, new Vector3(siz, siz, siz), game.Loader.Fresnel3, new Vector3(0.5f, 1.25f, 0.75f));
            }
            if (Type == 9)
            {
                float siz = 0.5f;
                game.Npcs[0].DrawModel(game, game.Loader.DomeModel, player, Position, Vector3.Zero, 1, new Vector3(siz, siz, siz), game.Loader.Fresnel3, new Vector3(0.75f, 0.75f, 1.25f));
            }
            if (Type == 11)
            {
                float siz = BlackHoleSize * BlackHoleRange / 100;
                game.Npcs[0].DrawModel(game, game.Loader.DomeModel, player, Position, Vector3.Zero, 1, new Vector3(siz, siz, siz), game.Loader.Fresnel2, new Vector3(1f, 0.25f, 1f));
            }
            if (Type == 12)
            {
                float siz = 0.35f;
                game.Npcs[0].DrawModel(game, game.Loader.DomeModel, player, Position, Vector3.Zero, 1, new Vector3(siz, siz, siz), game.Loader.Fresnel3, new Vector3(1f, 0.65f, 0.25f));
            }
            if (Type == 13)
            {
                float siz = 0.3f;
                game.Npcs[0].DrawModel(game, game.Loader.DomeModel, player, Position, Vector3.Zero, 1, new Vector3(siz, siz, siz), game.Loader.Fresnel3, new Vector3(0.35f, 0.65f, 1));
            }
            if (Type == 15)
            {
                float siz = 0.5f;
                game.Npcs[0].DrawModel(game, game.Loader.DomeModel, player, Position, Vector3.Zero, 1, new Vector3(siz, siz, siz), game.Loader.Fresnel3, new Vector3(1, 0.65f, 0.2f));
            }
            if (Type == 16)
            {
                float siz = 0.25f;
                game.Npcs[0].DrawModel(game, game.Loader.DomeModel, player, Position, Vector3.Zero, 1, new Vector3(siz, siz, siz), game.Loader.Fresnel3, new Vector3(1, 0.65f, 0.2f));
            }
            if (Type == 17000)
            {
                float siz = 0.25f;
                game.Npcs[0].DrawModel(game, game.Loader.DomeModel, player, Position, Vector3.Zero, 1, new Vector3(siz, siz, siz), game.Loader.Fresnel3, new Vector3(1.25f, 0.5f, 1f));
            }

        }



        public void SetMatrix()
        {


            if (ApplyRotation)
            {
                Vector3 Rot = Rotation + RotMod;


                WorldMatrix =ScaleMatrix * Matrix.CreateFromYawPitchRoll(-Rot.Y, Rot.X, Rot.Z) *
     Matrix.CreateTranslation(Position);
            }
            else
                WorldMatrix = ScaleMatrix *  Matrix.CreateTranslation(Position);
        }


        public void DrawModel(EffectDrawObject DrawObject,Model mod,LocalPlayer player)
        {
            DrawObject.ViewParameter.SetValue(player.playerView);
            DrawObject.ProjectionParameter.SetValue(player.playerProjection);
            DrawObject.WorldParameter.SetValue(WorldMatrix);
            DrawObject.ColorParameter.SetValue(MyColor);

            if (DrawObject.ViewPositionParameter != null)
                DrawObject.ViewPositionParameter.SetValue(new Vector4(player.CameraPos, 0));

            foreach (ModelMesh mesh in mod.Meshes)
                mesh.Draw();

        }


        public void DrawModel(Game1 game, Model mod, LocalPlayer player, Vector3 pos, Vector3 Rot, float Alpha, Vector3 Scale,Effect effect,Vector3 color)
        {
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
                }
                mesh.Draw();
            }
        }

        public void Spawn(Game1 game)
        {
            Rotation = Vector3.Zero;
            MinDrawTime = 0;
            BlackHoleSize = 0;
            BlackHoldGrowing = true;
            RailMinTime = 0;
            ShouldSparkLight = true;
            Ready = 0;

            DeliveredDamage = false;
            SeekRadious = StartSeekRadious;
            Life = 260;
            PulseSize = 0;
            SwingAmount = 0;
            mintime = 0;
            IsSwinging = false;
            IsStuck = false;
            TimeCharging = 0;
            TargetSize = MaxTargetSize;
            TargetTime = 0;
            Projectiles = 0;
            NeedLight = true;
            IsExplosiveDeath = false;
            ApplyRotation = false;

            if (Type != 1 && Type != 5 && Type != 9 && Type != 12 && Type != 14 && Type != 15 && Type != 18)
                IsExplosiveDeath = true;

            if (Type == 0 || Type == 13 || Type == 15 || Type == 16 || Type == 17 || Type == 8 || Type == 9 || Type == 4)
                updateAction = MoveAsBullet;
            if (Type == 1 || Type == 5 || Type == 12 || Type == 14 || Type == 18 || Type == 10)
                updateAction = MoveAsGrenade;

            SetDraw(game);

            ScaleMatrix = Matrix.CreateScale(Scale);


        }

        public void Die(Game1 game,bool ToRelevent)
        {
            if (Type == 18)
            {
                bool Found = false;


                foreach (NPC npc in game.Npcs)
                    if (!npc.Relevent)
                        if (!Found)
                        {
                            Found = true;
                            npc.Relevent = true;
                            npc.Type = 2;
                            npc.Create(game);
                            npc.Position = Position;
                           // npc.Position.Y = 0;
                            npc.creator = Creator;
                                npc.PopWave = 1;

                        }
            }


            GrenadeGravityAdd = 0;
            int reps = 1;
            if (Type == 10)
                reps = 15;
            reps = 1;
            for (int i = 0; i < reps;i++ )
                if (!ToRelevent && Type == 10)
                {
                    bool found = false;

                    foreach (Bullet shot in game.Bullets)
                        if (!found)
                            if (!shot.Relevent)
                            {

                                if (true)
                                {
                                    shot.Relevent = true;
                                    shot.Position = Position;
                                    shot.PulseArea = 0;
                                    shot.CreatorID = CreatorID;
                                    shot.seeking = true;

                                    found = true;

                                    shot.Velocity = Vector3.Zero;

                                    shot.Creator = Creator;

                                    shot.TimeAlive = 0;
                                    shot.MaxTimeAlive = 10000;
                                    shot.bounces = 0;
                                    shot.LightColor = new Vector3(0.5f, 0.5f, 1);
                                    shot.LightDistance = 500;
                                    shot.Type = 11;
                                }
                                else
                                {
                                    shot.Relevent = true;
                                    shot.Position = Position;
                                    shot.PulseArea = 0;
                                    shot.CreatorID = CreatorID;
                                    found = true;
                                    shot.Velocity = Vector3.Zero;
                                    shot.Creator = Creator;
                                    shot.TimeAlive = 0;
                                    shot.MaxTimeAlive = 240;
                                    shot.ExplosionDamage = 120;
                                    shot.ExplosionPush = 1;
                                    shot.ExplosionPushVelMult = 1;
                                    shot.ExplosionSize = 500;
                                    shot.bounces = 0;
                                    float ShotRotation = (float)game.random.NextDouble() * MathHelper.ToRadians(360);
                                    shot.Velocity = new Vector3(
                                         10 * (float)Math.Sin(ShotRotation), 8 + (float)game.random.NextDouble() * 10,
                                         10 * (float)Math.Cos(ShotRotation)
                                         );
                                    shot.SpeedPoints = 10 * (float)game.random.NextDouble() + 9;
                                    shot.LightColor = new Vector3(0.5f, 0.5f, 1);
                                    shot.LightDistance = 0;
                                    shot.Type = 12;

                                }

                                shot.Spawn(game);
                            }

                }

            if (!ToRelevent&&hasLight)
            {
                hasLight = false;
                PersonalLight.Relevent = false;
            }
            if (Relevent)
            {
                #region ExplosionDamage
                foreach (BasicOrb orb in game.Orbs)
                    if (orb.relevent)
                        if (orb.Alive)
                            if(orb.Inv<orb.MaxPhaseinv+1)
                            if(orb.Team!=Creator.Team||game.FrendlyFire||orb==Creator)
                        {
                            bool Protected = false;

                            foreach(NPC npc in game.Npcs)
                                if (npc.Alive && npc.Relevent)
                                if(npc.Growth<1.1f)
                                {
                                    if (Vector3.Distance(npc.Position, orb.Position) < npc.Size)
                                        Protected = true;

                                }


                            if (Type == 16 && orb == Creator)
                                Protected = true;

                            if (!Protected)
                            {
                                float TempDist = Vector3.Distance(orb.Position, Position);
                                if (Type == 15 && orb == Creator)
                                    TempDist *= 1.5f;

                                if (TempDist < ExplosionSize)
                                {

                                    orb.Damage = (ExplosionSize - TempDist) / ExplosionSize * ExplosionDamage;
                                    if (Type == 15 && orb == Creator)
                                        orb.Damage *= 0.5f;
                                    orb.pushDamage = ExplosionPush;
                                    orb.pushDirection = -(float)Math.Atan2(orb.Position.X - Position.X, orb.Position.Z - Position.Z);
                                    orb.PushVelMult = ExplosionPushVelMult;
                                    Creator.HitBounce = Creator.MaxHitBounce;
                                    //orb.DamageFrom[CreatorID] += (ExplosionSize - TempDist) / ExplosionSize * ExplosionDamage;
                                    orb.LastDamager = CreatorID;

                                    if (orb.life < 0)
                                        orb.Alive = false;

                                }
                            }
                        }

                foreach (Block block in game.ActiveBlocks)
                    if (block.Relevent)
                        if (block.alive)
                            if (block.Destructable)

                                if (game.B_Holder.BlockSolid[block.Type])
                            {
                                float TempDist = Vector3.Distance(block.Position, Position);
                                if (TempDist < ExplosionSize)
                                {

                                    block.life -= (ExplosionSize - TempDist) / ExplosionSize * ExplosionDamage * 2;



                                    if (block.life < 0)

                                        block.alive = false;

                                }

                            }
                foreach (Pickup pick in game.Pickups)
                if(pick.Relevent)
                    if(pick.Type==1)
                {
                    float TempDist = Vector3.Distance(pick.Position, Position);
                    if (TempDist < ExplosionSize*0.75f)
                        pick.Relevent = false;
                }

                foreach(NPC npc in game.Npcs)
                    if (npc.Relevent && npc.Alive)
                    {
                        float newExplosionSize = ExplosionSize + npc.Size;
                        float TempDist = Vector3.Distance(npc.Position, Position);
                        if (TempDist < newExplosionSize)
                        {

                            npc.Damage = (newExplosionSize - TempDist) / newExplosionSize * ExplosionDamage;
                            npc.pushDamage = ExplosionPush;
                            npc.pushDirection = -(float)Math.Atan2(npc.Position.X - Position.X, npc.Position.Z - Position.Z);
                            npc.PushVelMult = ExplosionPushVelMult;
                            if(npc.Hittable)
                            Creator.HitBounce = Creator.MaxHitBounce;

                        }

                    }
                #endregion

                #region Particles
                if (ShouldSparkLight&&LightCreatedTime>2)
                {
                    LightCreatedTime = 0;
                    if (Type == 1||Type==5||Type==9||Type==12||Type==14||Type==15)
                    {
                        PlayDeathSound(game);

                        

                        for (int p = 0; p < 4; p++)
                            if (Visible[p])
                        for (int i = 0; i < 5; i++)
                        {
                            
                            
                            {
                                game.SparkParticles[p].AddParticle(Position, Vector3.Zero);
                                if (Type != 5)
                                    game.ExplosionParticles[p].AddParticle(Position, Vector3.Zero);
                                else
                                    game.PlasmaExplosionParticles[p].AddParticle(Position, Vector3.Zero);
                                //game.LargeSmokeParticles[p].AddParticle(Position, Vector3.Zero);
                            }

                        }
                        bool FoundLight = false;
                        if(Visible[0]||Visible[1]||Visible[2]||Visible[3])
                        foreach (DynamicLightObject light in game.DynamicLights)
                            if (!FoundLight)
                                if (!light.Relevent)
                                {
                                    light.Relevent = true;
                                    light.Position = Position + new Vector3(0, 50, 0);
                                    
                                    light.Color = new Vector3(4, 2, 1) * 2f;
                                    if (Type == 5)
                                        light.Color = new Vector3(1, 4, 1) * 2f;
                                    if (Type == 9||Type==17)
                                        light.Color = new Vector3(1, 1, 4) * 2f;
                                        light.Distancee = 600;
                                    light.LifeTime = 0;
                                    light.MaxLifeTime = 30;
                                    light.LimitedLifetime = true;
                                    light.ConstUpdate = false;
                                    FoundLight = true;
                                    light.NeedShadows = false;
                                    light.IsSpot = true;
                                    light.RecalculateLights(game);
                                    light.Create(game);
                                }
                    }



                    if (Type == 0 || Type == 1 || Type == 5 || Type == 8 || Type == 9 || Type == 12 || Type == 14 || Type == 15||Type==16)
                    {
                        

                        for (int p = 0; p < 4; p++)
                            if (Visible[p])
                        for (int i = 0; i < 5; i++)
                        {
                            
                                
                                
                                    game.SparkParticles[p].AddParticle(Position, Vector3.Zero);


                                


                        }

                        bool FoundLight = false;
                        if (Visible[0] || Visible[1] || Visible[2] || Visible[3])
                        foreach (DynamicLightObject light in game.DynamicLights)
                            if (!FoundLight)
                                if (!light.Relevent)
                                {
                                    light.Relevent = true;
                                    light.Position = Position - Velocity * 3;
                                    light.Color = new Vector3(1, 0.75f, 0.5f) * 2.5f;
                                    light.Distancee = 200;
                                    light.MaxLifeTime = 2;
                                    light.LifeTime = 0;
                                    light.ConstUpdate = false;
                                    light.LimitedLifetime = true;
                                    FoundLight = true;
                                    //light.RecalculateLights(game);
                                    light.Create(game);
                                    light.IsSpot = true;
                                    light.NeedShadows = false;
                                    light.ClearShadowMap(game);
                                }
                    }
                }
                #endregion

                Relevent = ToRelevent;
                //Type = 0;
            }
            
            }

    }
}
