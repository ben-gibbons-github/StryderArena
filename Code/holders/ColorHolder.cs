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
   public class ColorHolder
    {
        public const int MaxColors = 8;
        public int maxColors=MaxColors;
        public Color[] colors = new Color[MaxColors];
        public Vector4[] colorVecs = new Vector4[MaxColors];

        public void Load(Game1 game)
        {
            int i = 0;
            Vector3 vec;
            
            vec= new Vector3(1,0,0);
            colors[i] = new Color(vec);
            colorVecs[i] = new Vector4(vec, 1);
            i++;

            vec = new Vector3(0, 1, 0);
            colors[i] = new Color(vec);
            colorVecs[i] = new Vector4(vec, 1);
            i++;

            vec = new Vector3(0, 0, 1);
            colors[i] = new Color(vec);
            colorVecs[i] = new Vector4(vec, 1);
            i++;

            vec = new Vector3(1, 1, 0);
            colors[i] = new Color(vec);
            colorVecs[i] = new Vector4(vec, 1);
            i++;

            vec = new Vector3(1, 0, 1);
            colors[i] = new Color(vec);
            colorVecs[i] = new Vector4(vec, 1);
            i++;

            vec = new Vector3(0, 1, 1);
            colors[i] = new Color(vec);
            colorVecs[i] = new Vector4(vec, 1);
            i++;

            vec = new Vector3(1, 0.5f, 0.25f);
            colors[i] = new Color(vec);
            colorVecs[i] = new Vector4(vec, 1);
            i++;

            vec = new Vector3(1, 1, 1);
            colors[i] = new Color(vec);
            colorVecs[i] = new Vector4(vec, 1);
            i++;

        }


    }
}
