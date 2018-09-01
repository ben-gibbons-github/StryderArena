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
using Microsoft.Xna.Framework.Storage;
//using Microsoft.Xna.Framework.Design;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;

namespace Orb
{
    public class Flag
    {
        public Vector3 Position = Vector3.Zero;
        public BasicOrb carrier;
        public bool hasLight = true;
        public bool HasFoundLights = false;
        public DynamicLightObject PersonalLight;
        public bool IsCarried;
        Vector2 Size = new Vector2(200);
        public float ScoreTimer = 0;
        public int NewDrawNumb = 0;
        public Vector3 Rotation = Vector3.Zero;
        public LightObject[] NewDrawList = new LightObject[8];
        public bool[] Visible = new bool[4];
        public BoundingSphere bounders = new BoundingSphere(Vector3.Zero, 250);
        Vector3 LightColor = new Vector3(1, 2, 4)*0.75f;
        float LightDistance = 800;


        public void GetLight(Game1 game)
        {
            //if (Relevent)
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

        public void Update(Game1 game,GameTime gametime)
        {
            ScoreTimer += 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;

            if (hasLight)
                PersonalLight.Position = Position + new Vector3(0, 200, 0);
            else
                GetLight(game);

            bounders.Center = Position;

            if (IsCarried && carrier != null)
            {
                if (carrier.Alive && carrier.relevent)
                {
                    float Offset = MathHelper.ToRadians(-90);
                    Vector3 GunDrawPos = carrier.Position + new Vector3(
               0 * (float)Math.Cos(carrier.Rotation.Y + Offset) +
                1 * (float)Math.Sin(-carrier.Rotation.Y + Offset), 0,
                 0 * (float)Math.Sin(carrier.Rotation.Y + Offset) +
                1 * (float)Math.Cos(-carrier.Rotation.Y + Offset)) * 40
                + new Vector3(
               0 * (float)Math.Cos(carrier.Rotation.Y - Offset) +
                1 * (float)Math.Sin(-carrier.Rotation.Y - Offset), 0,
                 0 * (float)Math.Sin(carrier.Rotation.Y - Offset) +
                1 * (float)Math.Cos(-carrier.Rotation.Y - Offset)) * -10
                ;

                    Rotation = carrier.Rotation;
                    Position = GunDrawPos;
                    Position.Y = 0;
                   
                    if (ScoreTimer > 150)
                    {
                        ScoreTimer = 0;
                        carrier.MyController.FlagScore++;
                    }

                }
                else
                {
                    IsCarried = false;
                }
            }
            else
            {
                foreach (BasicOrb orb in game.Orbs)
                        if (orb.Alive)
                            if (orb.relevent)
                                    if(!orb.IsPhasing)
                                    if (Position.X + Size.X / 2 + orb.Size.X / 2 > orb.Position.X && Position.X - Size.X / 2 - orb.Size.X / 2 < orb.Position.X)
                                        if (Position.Z + Size.Y / 2 + orb.Size.Y / 2 > orb.Position.Z && Position.Z - Size.Y / 2 - orb.Size.Y / 2 < orb.Position.Z)
                                        {
                                            
                                            
                                            IsCarried = true;
                                            carrier = orb;
                                            //ScoreTimer = 0;
                                            if (orb.MyController.AllMoney < 4&&game.random.Next(100)>49)
                                            {
                                                orb.MyController.Money++;
                                                orb.MyController.AllMoney++;
                                            }

                                            orb.CPUBuy(game);
                                        }

            }
        }

        public void Reset(Game1 game)
        {
            if(hasLight)
            if(PersonalLight!=null)
            PersonalLight.Relevent = false;
            hasLight = false;
            Position = game.AverageBlockPosition;
            Position.Y = 0;
        }

        public void Draw(Game1 game,LocalPlayer player)
        {
            
            game.Draw(game.Loader.FlagModel, player, Position, new Vector3(0, Rotation.Y, 0), 1, Vector3.One, NewDrawNumb, NewDrawList, Vector2.One, DepthStencilState.Default, new Vector3(0.5f,2.5f,5)*5);
           
        }
    }
}
