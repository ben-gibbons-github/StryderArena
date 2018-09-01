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
    public class MenuLine:BasicMenuLine
    {



        public override void Draw(Game1 game,BasicMenuObject host,Vector2 CurrentDrawPos,Vector2 Mult)
        {
            Vector4 vec = new Vector4(0.3f, 0.3f, 0.6f, 1) * Alpha;

            Color DrawColor = new Color(vec);

            

            string Text = Name;
            Rectangle HUDRECT= new Rectangle();

            HUDRECT.Height = (int)((game.Loader.font.MeasureString(Text).Y + 20 )*Mult.Y);
            HUDRECT.Width = (int)((game.Loader.font.MeasureString(Text).X + 10  )*Mult.X);
            HUDRECT.Y = (int)((CurrentDrawPos.Y - 10 )*Mult.Y);
            HUDRECT.X = (int)((CurrentDrawPos.X - 5)*Mult.X);
            game.spriteBatch.Draw(game.Loader.Temp, HUDRECT, DrawColor);

            if (IsArrows)
            {
                HUDRECT.Width = (int)((game.Loader.font.MeasureString(Text).Y + 10) * Mult.Y);
                HUDRECT.Y = (int)((CurrentDrawPos.Y - 10) * Mult.Y);
                HUDRECT.X = (int)((CurrentDrawPos.X - 5) * Mult.X - ((game.Loader.font.MeasureString(Text).Y + 10)*1f * Mult.Y));
                game.spriteBatch.Draw(game.Loader.MenuArrow, HUDRECT, DrawColor);

                HUDRECT.X = (int)((CurrentDrawPos.X - 5) * Mult.X + ((game.Loader.font.MeasureString(Text).X +10) * Mult.X));
                game.spriteBatch.Draw(game.Loader.MenuArrow, HUDRECT, null, DrawColor, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
            }

            Text = Name;

            //game.spriteBatch.DrawString(game.Loader.font, Text, CurrentDrawPos+new Vector2(2), Color.Black);
            game.spriteBatch.DrawString(game.Loader.font, Text, CurrentDrawPos*Mult, Color.White,0,Vector2.Zero,Mult,SpriteEffects.None,0);

           // CurrentDrawPos += host.DrawSeperation;
        }
        
    }
}
