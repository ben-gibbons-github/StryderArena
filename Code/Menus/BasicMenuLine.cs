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
    public abstract class BasicMenuLine
    {
        public bool IsArrows = false;
        public string Name;
        public float Alpha;
        public int X=0;
        public int Y=0;
        //public float DrawVal = 0;

        public void Update(Game1 game,int InputX,int InputY,BasicMenuObject Host)
        {
           // DrawVal = InputY;
            if(Y==InputY)
                Alpha += 0.05f;
            else
                Alpha -= 0.05f;

            //if (Alpha > 1)
            //    Alpha = 1;
            //if (Alpha < 0)
            //    Alpha = 0;
            

            Alpha = MathHelper.Clamp(Alpha, 0, 1);

        }
        public abstract void Draw(Game1 game, BasicMenuObject host, Vector2 CurrentDrawPos,Vector2 Mult);

    }
}
