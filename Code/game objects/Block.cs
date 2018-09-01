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
    public class Block: LightDrawObject
    {
        public Vector2 Size = new Vector2(100, 100);
        public Vector3 Position = Vector3.Zero;
        public Vector3 Rotation = Vector3.Zero;
        public BoundingBox Boundaries;
        public bool Destructable = true;
        public bool HasCheckedLights = false;
        public float life = 100;
        public float[] Alpha = { 1, 1, 1, 1 };
        public bool Relevent = false;
        public bool alive = true;
        public bool NeedToFindLightList = false;
        public bool respawning = false;
        public int RespawnTime = 0;
        public bool HasGottenLights = false;
        public int SetTimer = 10;

        public LightObject[] PerPixelDrawList = new LightObject[5];
        public int PerPixelDrawNumb = 0;
        public LightObject[] NewDrawList = new LightObject[16];
        public int NewDrawNumb = 0;

        public int MaxRespawnTime = 120;
        public float MaxLife = 100;
        public int Type = 0;
        public bool SwitchDraw;
        public bool PhaseBlock=false;

        public bool NeedToUpdateLights = false;
        public bool[] Visible = new bool[4];
        public float MistCount = 0;
        public float OverAllCounter = 0;

        public void Update(Game1 game)
        {
            MistCount+=1;
            if(MistCount>100)
            {
              
         MistCount = 0;
            OverAllCounter += 4;
            if (OverAllCounter > 10)
               OverAllCounter -= 10;
            }
                if (!alive)
            {
                if(!respawning)
                {
                    respawning = true;
                RespawnTime=1;
                }
                else
                {
                    RespawnTime+=1;
                if(RespawnTime>MaxRespawnTime)
                {
                    bool PlaceFree = true;
                    foreach (BasicOrb orb in game.Orbs)
                        if (orb.Alive)
                            if (orb.relevent)
                                    if (Position.X + Size.X / 2 + orb.Size.X / 2 > orb.Position.X && Position.X - Size.X / 2 - orb.Size.X / 2 < orb.Position.X)
                                        if (Position.Z + Size.Y / 2 + orb.Size.Y / 2 > orb.Position.Z && Position.Z - Size.Y / 2 - orb.Size.Y / 2 < orb.Position.Z)
                                            PlaceFree = false;
                    if (PlaceFree)
                    {
                        alive = true;
                        respawning = false;
                        life = MaxLife;
                        RespawnTime = 0;
                    }

                }
                }
                }
        }

        public void Create(Game1 game)
        {

            SetTimer = 100;
            Visible = new bool[4];
            HasCheckedLights = false;
            NeedToFindLightList = true;
            HasGottenLights = false;
            life = MaxLife;
            alive = true;
            respawning = false;
            RespawnTime = 0;

            Boundaries.Min = new Vector3(Position.X - Size.X / 2, Position.Y - 50, Position.Z - Size.Y / 2);
            Boundaries.Max = new Vector3(Position.X + Size.X / 2, Position.Y + game.B_Holder.BlockSize[Type].Y - 50, Position.Z + Size.Y / 2);

            Size = new Vector2(Boundaries.Max.X - Boundaries.Min.X, Boundaries.Max.Z - Boundaries.Min.Z);

            

            Vector3 scale;
            if (!SwitchDraw)
                scale = new Vector3(Size.X / game.B_Holder.BlockSize[Type].X, 1, Size.Y / game.B_Holder.BlockSize[Type].Z);
            else
                scale = new Vector3(Size.Y / game.B_Holder.BlockSize[Type].X, 1, Size.X / game.B_Holder.BlockSize[Type].Z);

            RotationMatrix = Matrix.CreateFromYawPitchRoll(-Rotation.Y, Rotation.X, Rotation.Z);

            WorldMatrix = Matrix.CreateScale(scale) * Matrix.CreateFromYawPitchRoll(-Rotation.Y, Rotation.X, Rotation.Z) *
                         Matrix.CreateTranslation(Position);


            base.LoadEffectParameters(game.B_Holder.BlockModel[Type],Size);
        
        }


        public void Draw(Game1 game, LocalPlayer player)
        {
            if (Relevent)
                if (alive)
                {
                    if (NeedToFindLightList && !HasCheckedLights)
                    {
                        HasCheckedLights = true;
                        NeedToFindLightList = false;
                        game.FindLightsLimited(Position, Vector2.Distance(Size, Vector2.Zero) / 4, false, true, Boundaries);
                        NewDrawList = (LightObject[])game.ReturnLightList.Clone();
                        NewDrawNumb = game.ReturnLightNumb;
                    }

                    if (!game.B_Holder.EffectHasSetCamera[Type, player.ID - 1])
                    {
                        ViewPositionParameter.SetValue(new Vector4(player.CameraPos, 0));
                        ViewParameter.SetValue(player.playerView);
                        ProjectionParameter.SetValue(player.playerProjection);
                        game.B_Holder.EffectHasSetCamera[Type, player.ID - 1] = true;
                    }


                    {
                        float alpha = 1;
                        if (game.B_Holder.BlockSize[Type].Y > 600)
                        {
                            alpha = Alpha[player.PlayerNumber - 1];
                            if (Vector3.Distance(Position, player.CameraPos) < 1200)
                            {
                                alpha -= 0.05f;
                                if (alpha < 0.1f)
                                    alpha = 0.1f;
                            }
                            else
                            {

                                alpha += 0.05f;
                                if (alpha > 1)
                                    alpha = 1;
                            }
                            if (game.IsInMenu)
                                alpha = 1;

                            Alpha[player.PlayerNumber - 1] = alpha;
                        }




                        if (player.playmode==LocalPlayer.PlayMode.Play&&game.SectionLoaded == 0 && Type == 3)
                        {
                            if (!SwitchDraw)
                                game.Draw(game.B_Holder.BlockModel[Type], player, Position, Rotation, alpha, new Vector3(Size.X / game.B_Holder.BlockSize[Type].X, 1, Size.Y / game.B_Holder.BlockSize[Type].Z), NewDrawNumb, NewDrawList, new Vector2(1, (Size.X + Size.Y) / 600), game.B_Holder.BlockStencil[Type], Vector3.One);
                            else
                                game.Draw(game.B_Holder.BlockModel[Type], player, Position, Rotation, alpha, new Vector3(Size.Y / game.B_Holder.BlockSize[Type].X, 1, Size.X / game.B_Holder.BlockSize[Type].Z), NewDrawNumb, NewDrawList, new Vector2(1, (Size.X + Size.Y) / 600), game.B_Holder.BlockStencil[Type], Vector3.One);
                        }
                        else
                        {
                            // if (!SwitchDraw)
                            game.Draw(this, game.B_Holder.BlockModel[Type], player, alpha, NewDrawNumb, NewDrawList, game.B_Holder.BlockStencil[Type], Vector3.One);
                            // else
                            //    game.Draw(this, game.B_Holder.BlockModel[Type], player, Position, Rotation, alpha, new Vector3(Size.Y / game.B_Holder.BlockSize[Type].X, 1, Size.X / game.B_Holder.BlockSize[Type].Z), NewDrawNumb, NewDrawList, new Vector2(1, (Size.X + Size.Y) / 600), game.B_Holder.BlockStencil[Type], Vector3.One);
                        }


                    }

                }
        }
        public void DrawShadow(Game1 game, float ShadowDepth,Vector3 LightPos,Matrix View, Matrix Projection,RenderTargetCube ShadowCube)
        {

            if (Relevent)
                if (alive)
                {
                    
                    if (!SwitchDraw)
                        game.DrawShadow(game.B_Holder.BlockModel[Type], Position, Rotation, new Vector3(Size.X / game.B_Holder.BlockSize[Type].X, 1, Size.Y / game.B_Holder.BlockSize[Type].Z), ShadowDepth, LightPos, View, Projection, ShadowCube,1);
                    else
                        game.DrawShadow(game.B_Holder.BlockModel[Type], Position, Rotation, new Vector3(Size.Y / game.B_Holder.BlockSize[Type].X, 1, Size.X / game.B_Holder.BlockSize[Type].Z), ShadowDepth, LightPos, View, Projection, ShadowCube,1);
                    
                }

        }
    

            
}
}
