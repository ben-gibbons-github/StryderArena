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
    public class MainMenu : BasicMenuObject
    {


        public override void Load(Game1 game)
        {
            Title = "Main Menu";

            MyType = "Main";

            //AddLine("Single Player", false);
            AddLine("Play Local", false);
            AddLine("Play Online", false);
            AddLine("How to Play", false);
            AddLine("Options", false);

            DrawPos = new Vector2(200, 300);

            MaxY = 3;
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
            if (AButton)
            {
                if (PosY == 1)
                    game.menus.GoTo("Online", false);
                if (PosY == 0)
                    game.menus.GoTo("Play", false);
                if (PosY == 2)
                    game.menus.GoTo("HowTo", true);
                if (PosY == 3)
                    game.menus.GoTo("Options",false);
            }
        }
    }
}
