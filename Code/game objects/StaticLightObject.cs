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
    public class StaticLightObject:LightObject
    {
        public RenderTargetCube ShadowCube2;
        public RenderTargetCube ShadowCube;
        //public RenderTarget2D ShadowPlane;
        public bool[] CubeIsBlank = new bool[6];
        public bool[] NeedToUpdate = new bool[6];
        public bool LightIsBitch = false;
        public float ReDrawTimer = 0;
        public SpecialObject MyCreatedSpecialObject;


        public void Load(Game1 game)
        {
            ShadowCube = new RenderTargetCube(game.GraphicsDevice,
                                                    256,
                                                    false,
                                                    SurfaceFormat.Bgr565,
                                                    DepthFormat.Depth24);

            ShadowCube2 = new RenderTargetCube(game.GraphicsDevice,
                                        256,
                                        false,
                                        SurfaceFormat.Bgr565,
                                        DepthFormat.Depth24);
            //ShadowPlane = new RenderTarget2D(game.GraphicsDevice, 512, 512);

            Cube = ShadowCube2;
            //Plane = ShadowPlane;
        }

        public  void Die(Game1 game)
        {
            ShadowCube.Dispose();
            ShadowCube2.Dispose();
            NeedShadows = true;
        }

        public void DrawShadowMap(Game1 game)
        {
            for (int i = 0; i < 6; i++)
            {
                if (i == 3)
                    game.GraphicsDevice.SetRenderTarget(ShadowCube, CubeMapFace.NegativeX);
                if (i == 4)
                    game.GraphicsDevice.SetRenderTarget(ShadowCube, CubeMapFace.NegativeY);
                if (i == 5)
                    game.GraphicsDevice.SetRenderTarget(ShadowCube, CubeMapFace.NegativeZ);
                if (i == 0)
                    game.GraphicsDevice.SetRenderTarget(ShadowCube, CubeMapFace.PositiveX);
                if (i == 1)
                    game.GraphicsDevice.SetRenderTarget(ShadowCube, CubeMapFace.PositiveY);
                if (i == 2)
                    game.GraphicsDevice.SetRenderTarget(ShadowCube, CubeMapFace.PositiveZ);

                game.GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.White);
                if (Relevent)
                {

                    foreach (Block block in game.Blocks)
                        if (block.alive && block.Relevent)
                            if (block.Boundaries.Intersects(Bounderies))
                                if (block.Boundaries.Intersects(ShadowFrustram[i]))
                                    //if (block.Visible[0] || block.Visible[1] || block.Visible[2] || block.Visible[3])
                                    block.DrawShadow(game, Distancee, Position, ShadowView[i], ShadowProjection, ShadowCube);
                }
            }
            UpdateShadowMap(game, true);
        }

        public void UpdateShadowMap(Game1 game,bool All)
        {
                if (!All)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        NeedToUpdate[i] = false;
                        if (CubeIsBlank[i])
                        {
                            foreach (BasicOrb orb in game.Orbs)
                                if (orb.relevent && orb.Alive)
                                    if (orb.Visible[0] || orb.Visible[1] || orb.Visible[2] || orb.Visible[3])
                                        if (orb.Boundaries.Intersects(Bounderies))
                                            if (orb.Boundaries.Intersects(ShadowFrustram[i]))
                                                //if(Vector3.Distance(orb.Position,Position)<Distancee*0.9)
                                                if (orb.Alpha > 0)
                                                NeedToUpdate[i] = true;

                            if (game.gamemode == Game1.GameMode.KeepAway)
                                if (game.flag.Visible[0] || game.flag.Visible[1] || game.flag.Visible[2] || game.flag.Visible[3])
                                    if (game.flag.bounders.Intersects(ShadowFrustram[i]))
                                        NeedToUpdate[i] = true;
                        }
                        else
                            NeedToUpdate[i] = true;
                    }
                }
                else
                    for (int i = 0; i < 6; i++)
                        NeedToUpdate[i] = true;

                for (int i = 0; i < 6; i++)
                    if (NeedToUpdate[i])
                    {
                        game.ShadowCalls += 1;
                        if (i == 3)
                            game.GraphicsDevice.SetRenderTarget(ShadowCube2, CubeMapFace.NegativeX);
                        if (i == 4)
                            game.GraphicsDevice.SetRenderTarget(ShadowCube2, CubeMapFace.NegativeY);
                        if (i == 5)
                            game.GraphicsDevice.SetRenderTarget(ShadowCube2, CubeMapFace.NegativeZ);
                        if (i == 0)
                            game.GraphicsDevice.SetRenderTarget(ShadowCube2, CubeMapFace.PositiveX);
                        if (i == 1)
                            game.GraphicsDevice.SetRenderTarget(ShadowCube2, CubeMapFace.PositiveY);
                        if (i == 2)
                            game.GraphicsDevice.SetRenderTarget(ShadowCube2, CubeMapFace.PositiveZ);

                            DrawShadowCube(game, i);
                            CubeIsBlank[i] = true;

                            if (!All)
                            {
                                foreach (BasicOrb orb in game.Orbs)
                                    if (orb.relevent && orb.Alive)
                                        if (orb.Visible[0] || orb.Visible[1] || orb.Visible[2] || orb.Visible[3])
                                            if (orb.Alpha > 0)
                                                if (orb.Boundaries.Intersects(Bounderies))
                                                    if (orb.Boundaries.Intersects(ShadowFrustram[i]))
                                                    {
                                                        CubeIsBlank[i] = false;
                                                        game.DrawShadow(game.PlayerModel, orb.Position, orb.Rotation, Vector3.One, Distancee, Position, ShadowView[i], ShadowProjection, ShadowCube, orb.Alpha);
                                                    }


                                if (game.gamemode == Game1.GameMode.KeepAway)
                                    if (game.flag.bounders.Intersects(Bounderies))
                                        if (game.flag.bounders.Intersects(ShadowFrustram[i]))
                                            if (game.flag.Visible[0] || game.flag.Visible[1] || game.flag.Visible[2] || game.flag.Visible[3])
                                            {
                                                CubeIsBlank[i] = false;
                                                game.DrawShadow(game.Loader.FlagModel2, game.flag.Position, game.flag.Rotation, Vector3.One, Distancee, Position, ShadowView[i], ShadowProjection, ShadowCube, 1);
                                            }
                            }
                    }
                game.GraphicsDevice.SetRenderTarget(null);
            
        }


        public void DrawShadowCube(Game1 game, int i)
        {
            foreach (ModelMesh mesh in game.Loader.Cube.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    Matrix world = Matrix.CreateTranslation(Position);
                    part.Effect = game.Loader.SkyboxEffect;
                    Effect effect = game.Loader.SkyboxEffect;
                    effect.Parameters["World"].SetValue(world);
                    effect.Parameters["View"].SetValue(ShadowView[i]);
                    effect.Parameters["Projection"].SetValue(ShadowProjection);
                    effect.Parameters["surfaceTexture"].SetValue(ShadowCube);
                    effect.Parameters["EyePosition"].SetValue(Position);
                }
                mesh.Draw();
                game.DrawCalls += 1;
            }
        }

        public void Create(Game1 game)
        {


            Bounderies2.Center = Position;
            Bounderies2.Radius = Distancee;//*0.75f;

            Bounderies.Center = Position;
            Bounderies.Radius = Distancee ;
            if (IsSpot)
            {
                ShadowProjection = Matrix.CreatePerspectiveFieldOfView(90 * 3.14159265f / 180, 1, 0.1f, 2000) * Matrix.CreateScale(-1, 1, 1);

                ShadowView[0] = Matrix.CreateLookAt(Position, Position + new Vector3(1000, 0, 0), Vector3.Up);


                ShadowView[1] = Matrix.CreateLookAt(Position, Position + new Vector3(0, 1000, 0), Vector3.Forward);


                ShadowView[2] = Matrix.CreateLookAt(Position, Position + new Vector3(0, 0, 1000), Vector3.Up);


                ShadowView[3] = Matrix.CreateLookAt(Position, Position + new Vector3(-1000, 0, 0), Vector3.Up);


                ShadowView[4] = Matrix.CreateLookAt(Position, Position + new Vector3(0, -1000, 0), Vector3.Backward);


                ShadowView[5] = Matrix.CreateLookAt(Position, Position + new Vector3(0, 0, -1000), Vector3.Up);


                for (int i = 0; i < 6; i++)
                    ShadowFrustram[i] = new BoundingFrustum(ShadowView[i] * ShadowProjection);

            }
            else
            {
                ShadowProjection = Matrix.CreatePerspectiveFieldOfView(45 * 3.14159265f / 180, 1, 0.01f, 4000) * Matrix.CreateScale(-1, 1, 1);
                ShadowView[0] = Matrix.CreateLookAt(Position, TargetPosition, Vector3.Up);
                ShadowFrustram[0] = new BoundingFrustum(ShadowView[0] * ShadowProjection);
            }
            //for (int i = 0; i < 6; i++)
                DrawShadowMap(game);
                UpdateShadowMap(game, true);



              

                float mult = 0.9f;
                Vector3 MinSize = new Vector3(Position.X - Distancee * mult, -49, Position.Z - Distancee * mult);
                Vector3 MaxSize = new Vector3(Position.X + Distancee * mult, -49, Position.Z + Distancee * mult);

              // MinSize = Vector3.Max(MinSize, game.MinBounds);
               //MaxSize = Vector3.Min(MaxSize, game.MaxBounds);

               // MinSize = Shrink(MinSize, player);
                //MaxSize = Shrink(MaxSize, player);

                // world = Matrix.CreateScale(Scale * LightList[b].Distancee / 50) * Matrix.CreateFromYawPitchRoll(-Rot.Y, Rot.X, Rot.Z) *
                //Matrix.CreateTranslation(new Vector3(LightList[b].Position.X, -48, LightList[b].Position.Z));

                Vector3 siz = new Vector3(MaxSize.X - MinSize.X, 100, MaxSize.Z - MinSize.Z) / 100;
                Vector3 newpos = (MinSize + MaxSize) / new Vector3(2, 2, 2);
                WorldMatrix= Matrix.CreateScale(siz)  *
           Matrix.CreateTranslation(new Vector3(newpos.X, -48, newpos.Z));



        }
  
    }
}
