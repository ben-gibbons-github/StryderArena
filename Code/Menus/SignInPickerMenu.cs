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
    public class SignInPickerMenu : BasicMenuObject
    {


        public override void Load(Game1 game)
        {
            Title = "";
            MyType = "SignInPicker";

            AddLine("Sign In",false);
            AddLine("Play Without Profile",false);

            DrawPos = new Vector2(600, 600);

            MaxY = 1;
            MinY = 0;
            Tier = 2;

        }
        public override void GoToMenu(Game1 game)
        {
            //throw new NotImplementedException();
        }

        public override void Update(Game1 game)
        {
            Title = "Player " + game.menus.Input.LastPressedA.ToString();
        }

        public override void HitButton(Game1 game, bool AButton, bool BButton, bool Left, bool Right)
        {
            if (AButton&&!Guide.IsVisible)
            {
                if (PosY == 1)
                {
                    game.ThisGamesPlayers[game.menus.Input.LastPressedA].Create("", null);
                    game.menus.GoTo(game.menus.OldMenu, false);
                    game.menus.Input.FreeAll();
                    game.menus.Input.InSignIn = false;
                }
                else
                {
                    try
                    {
                        Guide.ShowSignIn(4, false);
                    }
                    catch (Exception e)
                    {

                    }
                }
            }

        }
    }
}
