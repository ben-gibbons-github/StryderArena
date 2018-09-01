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
    public class OnlineMenu : BasicMenuObject
    {


        public override void Load(Game1 game)
        {
            Title = "Online Game Lobby";
            MyType = "Online";

            AddLine("Network: Online", false);
            AddLine("Desired Players: ", true);
            AddLine("Desired Level: ",true);
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
            if (game.onlineHandler.SessionType == NetworkSessionType.SystemLink)
                UpdateLine(0, "Network: " + game.onlineHandler.SessionType.ToString());
            else
                UpdateLine(0, "Network: XboxLive");

            UpdateLine(1, "Desired Players: " +game.NumbDesiredPlayers.ToString());

            UpdateLine(2, "Desired Level: " + game.ailevel.ToString());

            game.menus.AlphaText = game.onlineHandler.OnlineString+" ";
            if (game.onlineHandler.OnlineString != null)
                game.menus.AlphaTextAlpha = 1;

           // UpdateLine(5, game.onlineHandler.OnlineString+" ");
        }

        public override void HitButton(Game1 game, bool AButton, bool BButton, bool Left, bool Right)
        {
            if (PosY == 1)
            {
                if (Right)
                    game.NumbDesiredPlayers += 1;
                if (Left)
                    game.NumbDesiredPlayers -= 1;
            }

            if (PosY == 2)
            {
                if (Right)
                {
                    if (game.ailevel == Game1.AILevel.Hard)
                        game.ailevel = Game1.AILevel.Insane;
                    if (game.ailevel == Game1.AILevel.Medium)
                        game.ailevel = Game1.AILevel.Hard;
                    if (game.ailevel == Game1.AILevel.Easy)
                        game.ailevel = Game1.AILevel.Medium;
                }
                if (Left)
                {
                    if (game.ailevel == Game1.AILevel.Medium)
                        game.ailevel = Game1.AILevel.Easy;
                    if (game.ailevel == Game1.AILevel.Hard)
                        game.ailevel = Game1.AILevel.Medium;
                    if (game.ailevel == Game1.AILevel.Insane)
                        game.ailevel = Game1.AILevel.Hard;
                }
            }

            if (AButton)
            {
                if (PosY == 0)
                    game.menus.GoTo("NetworkSession", false);
                if (PosY == 3)
                    game.onlineHandler.BeginGameSearch();

            }

            if (BButton)
            {
                game.onlineHandler.EndGameSearch();
                game.menus.GoTo("Main", false);
            }
        }

        

    }
}
