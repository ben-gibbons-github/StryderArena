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
    public class ErrorMessage : BasicMenuObject
    {


        public override void Load(Game1 game)
        {
            QuadAlpha = 2.5f;

            Title = "Error";

            MyType = "Error";

            AddLine(" ",false);
            AddLine(" ",false);
            AddLine(" ",false);

            DrawPos = new Vector2(50, 400);

            MaxY = -1;
            MinY = -1;
            Tier = 1;

        }
        public override void GoToMenu(Game1 game)
        {
            //throw new NotImplementedException();
        }

        public override void Update(Game1 game)
        {
            UpdateLine(1, game.ErrorMessage);
        }

        public override void HitButton(Game1 game, bool AButton, bool BButton, bool Left, bool Right)
        {
            if (AButton||BButton)
            {
                game.menus.GoTo(game.menus.OldMenu, false);
            }
        }
    }
}
