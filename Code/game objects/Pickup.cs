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
    public class Pickup
    {
        public Vector3 Position = Vector3.Zero;
        public Vector2 Size = new Vector2(100, 100);
        public int Type = 0;
        public float Rotation = 0;
        public bool Relevent = false;
        public bool[] Visible = new bool[4];
        public Model model;
        public Effect effect;
        public float Age = 0;
        Vector3 Color = new Vector3(0.25f, 1, 0.35f);
        public BoundingSphere bounders;
        public float HealthAmount = 0;
        public float LifeTime = 0;
        public float MaxHealthDropLifeTime = 400;

        public void Update(Game1 game,GameTime gametime)
        {
            Rotation += MathHelper.ToRadians(0.25f) * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
            bounders.Center = Position;
            bounders.Radius = 50;

            if (Type == 0||Type==2)
            {
                foreach (BasicOrb orb in game.Orbs)
                    if (orb.Alive)
                        if (orb.relevent)
                            if (!orb.IsPhasing)
                                if (Position.X + Size.X / 2 + orb.Size.X / 2 > orb.Position.X && Position.X - Size.X / 2 - orb.Size.X / 2 < orb.Position.X)
                                    if (Position.Z + Size.Y / 2 + orb.Size.Y / 2 > orb.Position.Z && Position.Z - Size.Y / 2 - orb.Size.Y / 2 < orb.Position.Z)
                                        //if (Type == 0)
                                        {
                                            if (Type == 0)
                                            {
                                                if (orb.life < orb.MaxLife)
                                                {
                                                    game.PlaySound(game.soundHolder.soundEffects["player_pickup_life"], orb.Position);

                                                    Relevent = false;
                                                    orb.life = Math.Min(orb.MaxLife, orb.life + orb.MaxLife*0.4f);
                                                }
                                            }
                                            if (Type == 2)
                                            {
                                                if (orb.Energy < orb.MaxEnergy)
                                                {
                                                    Relevent = false;
                                                    //orb.Energy = Math.Min(orb.MaxEnergy, orb.Energy + orb.MaxEnergy/4);
                                                }
                                            }
                                        }
            }

            if (Type == 1)
            {
                LifeTime += 1;
                if (LifeTime > MaxHealthDropLifeTime)
                    Relevent = false;
                HealthAmount -= Age;
                Age += 0.005f;
               // if (Age > 0.12f)
               //     Age = 0.12f;
                foreach (BasicOrb orb in game.Orbs)
                    if (orb.Alive)
                        if (orb.relevent)
                            if (!orb.IsPhasing)
                                if (Vector3.Distance(Position, orb.Position) < 200)
                                {
                                    orb.life += Age*2;
                                    if (orb.life > orb.MaxLife)
                                        orb.life = orb.MaxLife;
                                    HealthAmount -= Age;
                                }
                if (HealthAmount < 0)
                    Relevent = false;
            }
        }

        public void Load(Game1 game)
        {

        }

        public void Create(Game1 game)
        {
            Visible = new bool[4];
            if (Type == 0||Type==1)
            {
                Color = new Vector3(1, 0.35f, 0.25f);
                effect = game.Loader.Ambient;
                model = game.Loader.HealthPackModel;
                if (Type == 1)
                {
                    Age = 0;
                    HealthAmount = 300;
                    LifeTime = 0;
                }
            }
            
            if (Type == 2)
            {
                effect = game.Loader.Ambient;
                model = game.Loader.EnergyPackModel;
                Color = new Vector3(0.25f, 1, 0.35f);
            }
            
        }

        public void Draw(Game1 game, LocalPlayer player)
        {
            
            if (Relevent)
            {

                //Color = new Vector3(0f, 1, 0f);

                  DrawModel(game,model, player, Position, new Vector3(0,Rotation,0), 1, Vector3.One, Color);
                
            }

        }

        public void DrawModel(Game1 game, Model mod, LocalPlayer player, Vector3 pos, Vector3 Rot, float Alpha, Vector3 Scale, Vector3 Color)
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
                    effect.Parameters["Color"].SetValue(new Vector4(Color.X, Color.Y, Color.Z, 1));

                    //effect.Parameters["ViewPosition"].SetValue(new Vector4(player.CameraPos.X, player.CameraPos.Y, player.CameraPos.Z,0));
                }

                mesh.Draw();
            }
        }




    }
}
