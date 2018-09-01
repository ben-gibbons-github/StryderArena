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
    public abstract class LightObject
    {

        public Vector3 Position = Vector3.Zero;
        public float Distancee = 0;
        public Vector3 Color = Vector3.Zero;
        public bool Relevent = false;
        public Vector2 Size = new Vector2(100, 100);
        public Vector3 TargetPosition = Vector3.Zero;
        public int Type = 0;
        public BoundingSphere Bounderies;
        public BoundingSphere Bounderies2;
        public float Brightness = 1;
        public bool IsDynamic = false;
        public Matrix LightMatrix;
        public Vector3 Direction = Vector3.Zero;
        public RenderTargetCube Cube;
      //  public RenderTarget2D Plane;
        public Matrix[] ShadowView = new Matrix[6];
        public BoundingFrustum[] ShadowFrustram= new BoundingFrustum[6];

        public Matrix ShadowProjection;
        public bool[] Visible = new bool[4];
        public bool IsSpot = true;
        public bool NeedShadows = true;
        public Matrix WorldMatrix;




        public void DrawModel(Game1 game, Model mod, LocalPlayer player, Vector3 pos, Vector3 Rot, float Alpha, Vector3 Scale)
        {
            if(false)
            foreach (ModelMesh mesh in mod.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    Matrix world = Matrix.CreateScale(Scale) * Matrix.CreateFromYawPitchRoll(-Rot.Y, Rot.X, Rot.Z) *
                Matrix.CreateTranslation(pos);
                    effect.Parameters["World"].SetValue(world);
                    effect.Parameters["View"].SetValue(player.playerView);
                    effect.Parameters["Projection"].SetValue(player.playerProjection);
                    effect.Parameters["Color"].SetValue(new Vector4(Color.X, Color.Y, Color.Z, 1));
                    effect.Parameters["ViewPosition"].SetValue(new Vector4(player.CameraPos.X, player.CameraPos.Y, player.CameraPos.Z,0));
                }
                mesh.Draw();
            }
            if(false)
           foreach (ModelMesh mesh in game.Loader.Cube.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    Matrix world = Matrix.CreateScale(0.1f) * Matrix.CreateFromYawPitchRoll(-Rot.Y, Rot.X, Rot.Z) *
                Matrix.CreateTranslation(pos);
                    part.Effect = game.Loader.SkyboxEffect;
                    Effect effect = game.Loader.SkyboxEffect;
                    effect.Parameters["World"].SetValue(world);
                    effect.Parameters["View"].SetValue(player.playerView);
                    effect.Parameters["Projection"].SetValue(player.playerProjection);
                    effect.Parameters["surfaceTexture"].SetValue(Cube);
                    effect.Parameters["EyePosition"].SetValue(Position);
                }
                mesh.Draw();
            }
        }

        public void RecalculateLights(Game1 game)
        {
            
            foreach (Block block in game.Blocks)
                if(block.Relevent)
                  //  if (!block.Relevent)//
                    
            {
                block.NeedToUpdateLights = true;
            }

                foreach (Floor block in game.Floors)
                    if (block.Relevent)
                    {

                        block.NeedToUpdateLights = true;
                    }

        }



    }
}
