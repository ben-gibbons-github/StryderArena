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
    public class EffectDrawObject
    {
        public EffectParameter ViewParameter;
        public EffectParameter WorldParameter;
        public EffectParameter ProjectionParameter;
        public EffectParameter ColorParameter;
        public EffectParameter ViewPositionParameter;
      
        public Effect MyEffect;

        public bool HasLoadedEffect = false;


        public void LoadEffectParameters(Game1 game,Effect effect)
        {
            MyEffect = effect;

            EffectParameterCollection Parameters = MyEffect.Parameters;

            ViewParameter = Parameters["View"];
            WorldParameter = Parameters["World"];
            ProjectionParameter = Parameters["Projection"];
            ColorParameter = Parameters["Color"];
            ViewPositionParameter = Parameters["ViewPosition"];

            if(effect.Parameters["Texture"]!=null)
            effect.Parameters["Texture"].SetValue(game.Loader.HexTexture);
        }
    }
}
