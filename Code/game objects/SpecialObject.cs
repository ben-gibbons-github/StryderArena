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
    public class SpecialObject
    {
        public int Type = 0;
        public bool Relevent = false;
        public Vector3 Position = Vector3.Zero;
        public Vector2 Size = new Vector2(100, 100);
        public Vector3 Rotation = Vector3.Zero;
        public bool SwitchDraw = false;
        Vector3 Color = Vector3.One;
        public bool HasLight = false;
        public int RespawnCharge = 0;
        StaticLightObject LightAttached;
        public Pickup pickup;
        public bool[] Visible = new bool[4];
        public BoundingSphere bounders = new BoundingSphere();

        public bool HasPickup = false;
        public float PickupRecharge = 10000;

        const int MaxControls = 5;
        bool Foundlight = false;

        public int[] Value = new int[MaxControls];


        public void Update(Game1 game,GameTime gametime)
        {
            if (Type == 0)
                RespawnCharge += 1;
            if (Type == 1||Type==2)
            {
                if (Type == 1)
                    if (HasLight)
                        if (LightAttached.Position.X == Position.X)
                            LightAttached.NeedShadows = false;
                if (!HasPickup)
                {
                    PickupRecharge += 1 * (float)gametime.ElapsedGameTime.TotalMilliseconds / 16.66f;
                    if (PickupRecharge > Value[0] && !HasPickup)
                    {
                        // PickupRecharge = 0;
                        foreach (Pickup pick in game.Pickups)
                            if (!HasPickup)
                                if (!pick.Relevent)
                                {
                                    if (Type == 1)
                                        pick.Type = 0;
                                    if (Type == 2)
                                        pick.Type = 2;
                                    HasPickup = true;
                                    pick.Relevent = true;
                                    pick.Position = Position + new Vector3(0, 50, 0);

                                    pick.Create(game);
                                    pickup = pick;
                                }
                    }
                    else if (HasPickup)
                        HasPickup = false;
                }
                else
                {
                    if (!pickup.Relevent)
                    {
                        PickupRecharge = 0;
                        HasPickup = false;
                    }
                }
            }
        }

        public void Draw(Game1 game, LocalPlayer player)
        {
           // if(!Relevent)//
            if (Relevent&&game.S_Holder.SpecialUseModel[Type])
            {
                
                //Color = new Vector3(0f, 1, 0f);
                    if (!SwitchDraw)
                        DrawModel(game,game.S_Holder.SpecialModel[Type], player, Position, Rotation, 1, new Vector3(Size.X / game.S_Holder.SpecialSize[Type].X, 1, Size.Y / game.S_Holder.SpecialSize[Type].Z), Color,true);
                    else
                        DrawModel(game,game.S_Holder.SpecialModel[Type], player, Position, Rotation, 1, new Vector3(Size.Y / game.S_Holder.SpecialSize[Type].X, 1, Size.X / game.S_Holder.SpecialSize[Type].Z), Color,true);

                }

        }

        public void DrawModel(Game1 game, Model mod, LocalPlayer player, Vector3 pos, Vector3 Rot, float Alpha, Vector3 Scale,Vector3 Color, bool Change)
        {

            foreach (ModelMesh mesh in mod.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    Effect effect = game.S_Holder.SpecialEffect[Type];
                    if (Change)
                    {
                        
                        part.Effect = game.S_Holder.SpecialEffect[Type];

                    }
                    else
                        effect = part.Effect;
                    
                        Matrix world = Matrix.CreateScale(Scale) * Matrix.CreateFromYawPitchRoll(-Rot.Y, Rot.X, Rot.Z) *
                    Matrix.CreateTranslation(pos);

                        effect.Parameters["World"].SetValue(world);
                        effect.Parameters["View"].SetValue(player.playerView);
                        effect.Parameters["Projection"].SetValue(player.playerProjection);
                        effect.Parameters["Color"].SetValue(new Vector4(Color.X, Color.Y, Color.Z, 1));
                    
                        //effect.Parameters["ViewPosition"].SetValue(new Vector4(player.CameraPos.X, player.CameraPos.Y, player.CameraPos.Z,0));
                }

                mesh.Draw();
            }
        }

        public void Create(Game1 game)
        {

            bounders.Center = Position;
            bounders.Radius = 50;
            SpecialHolder Holder= game.S_Holder;
            if (Holder.AtatchLight[Type])
            {
               
                    foreach(StaticLightObject light in game.StaticLights)
                        if(!Foundlight)
                        if(!light.Relevent)
                        {
                            light.MyCreatedSpecialObject = this;
                            Foundlight=true;
                            light.Position=Position;
                            light.Position.Y=Holder.LightZ[Type];
                            light.Color=Holder.LightColor[Type];
                            light.Distancee=Holder.LightDistance[Type];
                            light.Relevent=true;
                            light.Create(game);
                            LightAttached = light;
                            light.LightIsBitch = true;
                            light.NeedShadows = false;
                            HasLight = true;

                            
                        }
            }

            if (Type == 0)
            {
                if (Value[1] != 1)
                {
                    if (Value[0] == 1)
                        Color = new Vector3(1, 0, 0);
                    if (Value[0] == 2)
                        Color = new Vector3(0, 0f, 1);
                    if (Value[0] == 3)
                        Color = new Vector3(1, 1, 0);
                    if (Value[0] == 4)
                        Color = new Vector3(0, 1, 0);
                }
                else
                    Color = Vector3.One;
            }
            if (Type == 1)
                Color = new Vector3(1, 0.35f, 0.25f);

    
            
        }

        public void Destroy(Game1 game)
        {
            
            Foundlight = false;
            if (HasLight)
            {
                
                LightAttached.LightIsBitch = false;
                LightAttached.Relevent = false;
                HasLight = false;
                LightAttached.NeedShadows = false;
                
            }
            if (HasPickup)
            {
                pickup.Relevent = false;
                HasPickup = false;
            }
            PickupRecharge = 100000;
        }
        
    }

}
