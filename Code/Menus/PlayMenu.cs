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
    public class PlayMenu : BasicMenuObject
    {


        public override void Load(Game1 game)
        {
            Title = "Local Game Lobby";
            MyType = "Play";

            AddLine("Map:", false);
            AddLine("Game Mode:", false);
            AddLine("Settings", false);
            AddLine("Play", false);

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
            UpdateLine(0, "Map: " + game.map.ToString());
            UpdateLine(1, "Game Mode: " + game.gamemode.ToString());
        }

        public override void HitButton(Game1 game, bool AButton, bool BButton, bool Left, bool Right)
        {
            if (AButton)
            {
                if (PosY == 0)
                    game.menus.GoTo("Map", false);
                if (PosY == 1)
                    game.menus.GoTo("GameMode", false);
                if (PosY == 2)
                    game.menus.GoTo("GameSettings", false);
                if (PosY == 3)
                    if(game.map!=Game1.Map.SelectMap)
                    game.menus.GoTo("InGame", true);
            }
            if (BButton)
                game.menus.GoTo("Main", false);
        }

    }
}
