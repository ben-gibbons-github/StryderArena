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
using Microsoft.Xna.Framework.Net;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;

namespace Orb
{
    public class NetworkSessionMenu : BasicMenuObject
    {


        public override void Load(Game1 game)
        {
            Title = "Network Type";

            MyType = "NetworkSession";

            AddLine("Xbox Live", false);
            AddLine("System Link", false);
         

            DrawPos = new Vector2(600, 200);

            MaxY = 1;
            MinY = 0;
            Tier = 1;

        }

        public override void GoToMenu(Game1 game)
        {
            //throw new NotImplementedException();
        }

        public override void Update(Game1 game)
        {
            if (PosY == 0)
                game.onlineHandler.SessionType = NetworkSessionType.PlayerMatch;
            else
                game.onlineHandler.SessionType = NetworkSessionType.SystemLink;
        }

        public override void HitButton(Game1 game, bool AButton, bool BButton, bool Left, bool Right)
        {

            if (AButton||BButton)
            {

                game.menus.GoTo("Online", false);
            }
        }
    }
}
