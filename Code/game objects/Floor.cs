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
    public class Floor:LightDrawObject
    {
        public Vector3 Position = Vector3.Zero;
        public Vector3 Rotation = Vector3.Zero;
        public bool SwitchDraw = false;
        public bool Relevent = false;
        public int Type = 0;
        public Vector2 Size = Vector2.Zero;
        public BoundingBox Boundaries;

        public LightObject[] PerPixelDrawList = new LightObject[5];
        public int PerPixelDrawNumb = 0;
        public LightObject[] NewDrawList = new LightObject[16];
        public int NewDrawNumb = 0;
        
        public bool NeedToUpdateLights = false;
        public bool[] Visible = new bool[4];

        LightDrawObject2 BlackObect = new LightDrawObject2();

        public void Draw(Game1 game, LocalPlayer player)
        {
            if (Relevent)
                {
                    //if (NeedToUpdateLights)
                    {
                        game.FindLights(Position, Vector2.Distance(Size,Vector2.Zero)/3, false, true,Boundaries);
                        NewDrawList = game.ReturnLightList;
                        NewDrawNumb = game.ReturnLightNumb;
                        NeedToUpdateLights = false;
                    }
                    //if (player.playmode == LocalPlayer.PlayMode.Play)
                    {
                        if (!SwitchDraw)
                            game.DrawFloor(this, game.F_Holder.FloorModel[Type],BlackObect,game.Loader.BlackFloor, player, Position, Rotation, 1, new Vector3(Size.X / game.F_Holder.FloorSize[Type].X, 1, Size.Y / game.F_Holder.FloorSize[Type].Z), NewDrawNumb, NewDrawList, Size / 400, DepthStencilState.Default);
                        else
                            game.DrawFloor(this, game.F_Holder.FloorModel[Type], BlackObect, game.Loader.BlackFloor, player, Position, Rotation, 1, new Vector3(Size.Y / game.F_Holder.FloorSize[Type].X, 1, Size.X / game.F_Holder.FloorSize[Type].Z), NewDrawNumb, NewDrawList, Size / 400, DepthStencilState.Default);
                    }
                }

        }

        public void Create(Game1 game)
        {
            Boundaries.Min = new Vector3(Position.X - Size.X / 2, -60, Position.Z - Size.Y / 2);
            Boundaries.Max = new Vector3(Position.X + Size.X / 2, -40, Position.Z + Size.Y / 2);
            Boundaries.Min = new Vector3(-1000000, -10000001, -10000000);
            Boundaries.Max = new Vector3(1000000, 10000001, 10000000);
            
            base.LoadEffectParameters(game.F_Holder.FloorModel[Type], Size);

            BlackObect.LoadEffectParameters(game.Loader.BlackFloor, Size);

            float Mult=5;

            WorldMatrix = Matrix.CreateScale(new Vector3(game.MaxBounds.X - game.MinBounds.X, 100 / Mult, game.MaxBounds.Z - game.MinBounds.Z) * Mult / 100) *
               Matrix.CreateTranslation(new Vector3(game.AverageBlockPosition.X, -51, game.AverageBlockPosition.Z));
        }

        public void DrawShadow(Game1 game, float ShadowDepth, Vector3 LightPos, Matrix View, Matrix Projection,RenderTargetCube ShadowCube)
        {

            if (Relevent)
                {
                    if (!SwitchDraw)
                        game.DrawShadow(game.F_Holder.FloorModel[Type], Position, Rotation, new Vector3(Size.X / game.F_Holder.FloorSize[Type].X, 1, Size.Y / game.F_Holder.FloorSize[Type].Z), ShadowDepth, LightPos, View, Projection, ShadowCube,1);
                    else
                        game.DrawShadow(game.F_Holder.FloorModel[Type], Position, Rotation, new Vector3(Size.Y / game.F_Holder.FloorSize[Type].X, 1, Size.X / game.F_Holder.FloorSize[Type].Z), ShadowDepth, LightPos, View, Projection,ShadowCube,1);

                }

        }

        
    }
}
