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
    public class LightDrawObject2
    {
        public EffectParameter ViewParameter;
        public EffectParameter WorldParameter;
        public EffectParameter ProjectionParameter;
        public EffectParameter RotationParameter;
        public EffectParameter TexMultParameter;
        public EffectParameter LightNumbParameter;
        public EffectParameter ViewPositionParameter;
        public EffectParameter DiffuseColorParameter;
        public EffectParameter DiffusePositionParemeter;
        public EffectParameter DiffuseDistanceParameter;
        public EffectParameter ShadowDepthParameter;
        public EffectParameter AlphaParameter;
        public EffectParameter ShadowCubeParameter;
        public Effect MyEffect;
        public int MaxLights;
        public Matrix WorldMatrix;
        public Matrix RotationMatrix;
        public bool IsFloorEffect = false;
        public bool HasLoadedEffect = false;
        public Vector2 TexMult;

        public void LoadEffectParameters(Model mod, Vector2 Size)
        {
            if (mod != null)
            {
                HasLoadedEffect = true;
                foreach (ModelMesh mesh in mod.Meshes)
                    foreach (Effect effect in mesh.Effects)
                        MyEffect = effect;

                EffectParameterCollection Parameters = MyEffect.Parameters;

                ShadowCubeParameter = Parameters["ShadowCube"];
                AlphaParameter = Parameters["Alpha"];
                ViewParameter = Parameters["View"];
                WorldParameter = Parameters["World"];
                ProjectionParameter = Parameters["Projection"];
                RotationParameter = Parameters["Rotation"];
                TexMultParameter = Parameters["TexMult"];
                LightNumbParameter = Parameters["NumbLights"];
                ViewPositionParameter = Parameters["ViewPosition"];
                DiffuseColorParameter = Parameters["DiffuseColor"];
                DiffuseDistanceParameter = Parameters["Distance"];
                DiffusePositionParemeter = Parameters["DiffuseLightPosition"];
                ShadowDepthParameter = Parameters["ShadowDepth"];
                MaxLights = Parameters["maxLights"].GetValueInt32();
                IsFloorEffect = Parameters["IsFloorEffect"].GetValueBoolean();
                Parameters["ShadowDepth"].SetValue(1000);
                TexMult = new Vector2(1, (Size.X + Size.Y) / 600);
                WorldParameter.SetValue(WorldMatrix);
                RotationParameter.SetValue(RotationMatrix);
                if (MaxLights == 1)
                    TexMultParameter.SetValue(TexMult);
            }

        }
    }
}
