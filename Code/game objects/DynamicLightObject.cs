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
    public class DynamicLightObject : LightObject
    {
        public bool ConstUpdate = false;
        public bool LimitedLifetime = true;
        public int MaxLifeTime = 0;
        public float LifeTime = 0;
        public RenderTargetCube ShadowCube;
        public bool NeedTOUpdateMatrix = false;
        //public RenderTarget2D ShadowPlane;

        public void UpdateMatrix()
        {

            NeedTOUpdateMatrix = true;
        }

        public void UpdateMatrix2()
        {

            WorldMatrix = Matrix.CreateScale(Distancee * 0.018f) *
       Matrix.CreateTranslation(new Vector3(Position.X, -48, Position.Z));
            NeedTOUpdateMatrix = false;
        }

        public  void Die(Game1 game)
        {
            
           // ShadowCube.Dispose();
            NeedShadows = false;
        }

        public void Load(Game1 game)
        {
            if(false)
            ShadowCube = new RenderTargetCube(game.GraphicsDevice,
                                                    128,
                                                    false,
                                                    SurfaceFormat.Bgr565,
                                                    DepthFormat.Depth24);

            IsDynamic = true;

          //  ShadowPlane = new RenderTarget2D(game.GraphicsDevice, 512, 512);
            Cube = ShadowCube;
           


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
            }
            game.GraphicsDevice.SetRenderTarget(null);
        }

        public void Update(Game1 game,GameTime gametime)
        {
            NeedShadows = false;
            Bounderies.Center = Position;
            if (LimitedLifetime)
            {

                Brightness = (float)(MaxLifeTime-LifeTime)/MaxLifeTime;
                if (Brightness < 0)
                    Relevent = false;
                LifeTime += 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
                if (LifeTime > MaxLifeTime)
                {
                    Relevent = false;
                    NeedShadows = true;
                }
            }
        }

        public void DrawShadowMap(Game1 game)
        {
            if (IsSpot)
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
                               // if(game.B_Holder.DrawShadow[block.Type])
                                if (block.Boundaries.Intersects(Bounderies))
                                    if (block.Boundaries.Intersects(ShadowFrustram[i]))
                                        if (block.Visible[0] || block.Visible[1] || block.Visible[2] || block.Visible[3])
                                        block.DrawShadow(game, Distancee, Position, ShadowView[i], ShadowProjection, ShadowCube);

                        foreach (BasicOrb orb in game.Orbs)
                            if (orb.relevent && orb.Alive)
                                if (orb.Boundaries.Intersects(Bounderies))
                                    if (orb.Boundaries.Intersects(ShadowFrustram[i]))
                                        if (orb.Visible[0] || orb.Visible[1] || orb.Visible[2] || orb.Visible[3])
                                            if(orb.Alpha>0)
                                            game.DrawShadow(game.PlayerModel, orb.Position, orb.Rotation, Vector3.One, Distancee, Position, ShadowView[i], ShadowProjection, ShadowCube,orb.Alpha);

                        if (game.gamemode == Game1.GameMode.KeepAway)
                            if (game.flag.bounders.Intersects(Bounderies))
                                if (game.flag.bounders.Intersects(ShadowFrustram[i]))
                                    if (game.flag.Visible[0] || game.flag.Visible[1] || game.flag.Visible[2] || game.flag.Visible[3])
                                        game.DrawShadow(game.Loader.FlagModel2, game.flag.Position, game.flag.Rotation, Vector3.One, Distancee, Position, ShadowView[i], ShadowProjection,ShadowCube, 1);
                    }
                }
            else
            {

                 
                        //game.GraphicsDevice.SetRenderTarget(ShadowPlane);

                        Create(game);
                    game.GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.White);
                    if (Relevent)
                    {
                        //if(false)
                        foreach (Block block in game.Blocks)
                            if (block.alive && block.Relevent)
                                if (block.Boundaries.Intersects(Bounderies))
                                    if (block.Boundaries.Intersects(ShadowFrustram[0]))
                                        block.DrawShadow(game, Distancee, Position, ShadowView[0], ShadowProjection, ShadowCube);
                        //if (false)
                        foreach (BasicOrb orb in game.Orbs)
                            if (orb.relevent && orb.Alive)
                                if (orb.Boundaries.Intersects(Bounderies))
                                    if (orb.Boundaries.Intersects(ShadowFrustram[0]))
                                        if (orb.Visible[0] || orb.Visible[1] || orb.Visible[2] || orb.Visible[3])
                                            game.DrawShadow(game.PlayerModel, orb.Position, orb.Rotation, Vector3.One, Distancee, Position, ShadowView[0], ShadowProjection, ShadowCube,orb.Alpha);

                    }

            }
        }

        public void ClearShadowMap(Game1 game)
        {
            if (IsSpot)
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
                }
            game.GraphicsDevice.SetRenderTarget(null);
        }

        public void Create(Game1 game)
        {
            //LifeTime = 0;
            NeedShadows = false;
            Bounderies.Center = Position;
            Bounderies.Radius = Distancee;
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
                ShadowProjection = Matrix.CreatePerspectiveFieldOfView(45 * 3.14159265f / 180, 1, 0.1f, 2000) * Matrix.CreateScale(-1, 1, 1);
                ShadowView[0] = Matrix.CreateLookAt(Position, TargetPosition, Vector3.Up);
                ShadowFrustram[0] = new BoundingFrustum(ShadowView[0] * ShadowProjection);
            }

            NeedTOUpdateMatrix = true;
        }
    }
}
