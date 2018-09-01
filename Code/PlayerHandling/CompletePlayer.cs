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
using Microsoft.Xna.Framework.Net;
namespace Orb
{
    public class CompletePlayer
    {
        public PlayerIndex MyIndex;
        public string MyName;
       // public SignedInGamer MyGamer;
        public bool InUse;
        //public Game1.Sensitivy sensitivy = Game1.Sensitivy.Medium;
       // public bool IsXboxProfile;
        //public bool IsXboxController;
        public bool IsProfile = false;
        //public int Team = 0;
        public float Sensitivity = 1f;
        public SignedInGamer MyGamer;

        public NetworkGamer MyNetworkGamer;
       // public Color color;
        //public Vector4 colorvec;

        public void Load(int i)
        {
            if (i == 0)
                MyIndex = PlayerIndex.One;
            if (i == 1)
                MyIndex = PlayerIndex.Two;
            if (i == 2)
               MyIndex= PlayerIndex.Three;
            if (i == 3)
                MyIndex = PlayerIndex.Four;

            MyName = "Player " + MyIndex.ToString();       
        }

        public void Create(string Name, SignedInGamer gamer)
        {
            IsProfile = false;
            InUse = true;
            MyGamer = gamer;

            if (Name != "")
            {
                MyName = Name;
                IsProfile = true;
                MyIndex = gamer.PlayerIndex;
            }
            else
                MyName = "Player " + MyIndex.ToString();
        }

        public void Destroy()
        {
            IsProfile = false;
            InUse = false;
            MyName = "";
        }

        public void Draw(Game1 game, MenuHandler Handler,Vector2 Mult)
        {
            game.spriteBatch.Begin();

            Rectangle HUDRECT = new Rectangle();

            HUDRECT.Height = (int)(game.Loader.font.MeasureString(MyName).Y*Mult.Y + 20);
            HUDRECT.Width = (int)(200*Mult.X);
            HUDRECT.Y = (int)((Handler.DrawPos.Y-10)*Mult.Y);
            HUDRECT.X = (int)((Handler.DrawPos.X-10)*Mult.X);
            game.spriteBatch.Draw(game.Loader.MenuWindow2, HUDRECT, Color.Gray);

            float rot = 0;

            HUDRECT.Height = (int)(48*Mult.X);
            HUDRECT.Width = (int)(48*Mult.X);
            HUDRECT.Y = (int)((Handler.DrawPos.Y - 0)*Mult.Y);
            HUDRECT.X = (int)((Handler.DrawPos.X - 70)*Mult.X);

            if (MyIndex == PlayerIndex.Two)
            {
                rot = 90;
                HUDRECT.X += (int)(48*Mult.X);
            }
            if (MyIndex == PlayerIndex.Four)
            {
                rot = 180;
                HUDRECT.X += (int)(48 * Mult.X);
                HUDRECT.Y += (int)(48 * Mult.Y);
            }
            if (MyIndex == PlayerIndex.Three)
            {
                rot = 270;
                HUDRECT.Y += (int)(48 * Mult.Y);
            }
            game.spriteBatch.Draw(game.Loader.ControllerRing, HUDRECT,null, Color.White,MathHelper.ToRadians(rot),new Vector2(0.5f),SpriteEffects.None,0);

            game.spriteBatch.DrawString(game.Loader.font, MyName,Handler.DrawPos*Mult, Color.White,0,Vector2.Zero,Mult,SpriteEffects.None,0);
            if (!IsProfile)
            {
                string Text = "Press back (<) to sign out";
                float SizeMult = 0.66f;

                game.spriteBatch.DrawString(game.Loader.font, Text, Handler.DrawPos * Mult - new Vector2((game.Loader.font.MeasureString(Text).X ) * Mult.X, 0), Color.White, 0, Vector2.Zero, Mult * SizeMult, SpriteEffects.None, 0);
            }

            //game.spriteBatch.DrawString(game.Loader.font, Mult.X.ToString()+" "+Mult.Y.ToString(), Handler.DrawPos * Mult, Color.White, 0, Vector2.Zero, Mult, SpriteEffects.None, 0);
          
            game.spriteBatch.End();


            Handler.DrawPos += new Vector2(0, 75);
        }
    }
}
