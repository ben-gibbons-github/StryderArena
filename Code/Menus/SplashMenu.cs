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
    public class SplashMenu : BasicMenuObject
    {


        public override void Load(Game1 game)
        {
            Title = "";
            MyType = "Splash";

            AddLine("Splash", false);


            DrawPos = new Vector2(200, 300);

            MaxY = 0;
            MinY = 0;

        }

        public override void GoToMenu(Game1 game)
        {
            //throw new NotImplementedException();
        }

        public override void Update(Game1 game)
        {

        }

        public override void HitButton(Game1 game, bool AButton, bool BButton, bool Left, bool Right)
        {
            //   if(AButton)
            //          game.menus.GoTo("Main",true);
        }

        public override void Draw(Game1 game)
        {
            game.DrawFullscreenQuad(game.Loader.Temp, BlendState.AlphaBlend, null, Color.Black);

            game.spriteBatch.Begin();

            Rectangle HUDRECT = new Rectangle();

            Color Col = new Color(Vector4.One * MenuAlpha);


            HUDRECT.Height = (int)(game.resolutiony);
            HUDRECT.Width = (int)(game.resolutionx);
            HUDRECT.Y = 0;// (int)(game.resolutiony / 2 - 1024 * Mult.Y / 2);
            HUDRECT.X = 0;// (int)(game.resolutionx / 2 - 1024 * Mult.Y / 2);
            game.spriteBatch.Draw(game.SplashTexture, HUDRECT, Color.White);

            game.spriteBatch.End();
        }


    }
}
