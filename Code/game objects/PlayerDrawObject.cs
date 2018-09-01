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
    public class PlayerDrawObject
    {
        public EffectParameter ViewParameter;
        public EffectParameter WorldParameter;
        public EffectParameter ProjectionParameter;
        public EffectParameter LightNumbParameter;
        public EffectParameter ViewPositionParameter;
        public EffectParameter DiffuseColorParameter;
        public EffectParameter DiffusePositionParemeter;
        public EffectParameter DiffuseDistanceParameter;
        public EffectParameter ShadowDepthParameter;
        public EffectParameter AlphaParameter;
        public EffectParameter ColorParameter;
        public Effect MyEffect;
        public int MaxLights;
        public Matrix WorldMatrix;
        public Matrix RotationMatrix;
        public bool HasLoadedEffect = false;
        public EffectType effectType;

        public enum EffectType
        {
            Light,
            Ambient

        }

        public void LoadEffectParameters(Game1 game,Model mod,EffectType effecttype)
        {
            effectType = effecttype;

            HasLoadedEffect = true;
            foreach (ModelMesh mesh in mod.Meshes)
                foreach (Effect effect in mesh.Effects)
                    MyEffect = effect;

            if(effectType==EffectType.Ambient)
            {
                //effectType = EffectType.Ambient;

                MyEffect = game.Loader.Ambient;

            foreach (ModelMesh mesh in mod.Meshes)
                foreach (ModelMeshPart part in mesh.MeshParts)
                        part.Effect = MyEffect;
            }

            EffectParameterCollection Parameters = MyEffect.Parameters;

            
            ViewParameter = Parameters["View"];
            WorldParameter = Parameters["World"];
            ProjectionParameter = Parameters["Projection"];

            if (effectType == EffectType.Light)
            {
                AlphaParameter = Parameters["Alpha"];
                LightNumbParameter = Parameters["NumbLights"];
                ViewPositionParameter = Parameters["ViewPosition"];
                DiffuseColorParameter = Parameters["DiffuseColor"];
                DiffuseDistanceParameter = Parameters["Distance"];
                DiffusePositionParemeter = Parameters["DiffuseLightPosition"];
                ShadowDepthParameter = Parameters["ShadowDepth"];
                MaxLights = Parameters["maxLights"].GetValueInt32();
                Parameters["ShadowDepth"].SetValue(1000);
                WorldParameter.SetValue(WorldMatrix);
                Parameters["Texture"].SetValue(game.Loader.PlayerTexture);
            }
            else
                ColorParameter = Parameters["Color"];
        }
    }
}
