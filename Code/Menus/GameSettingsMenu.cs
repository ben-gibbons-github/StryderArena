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
    public class GameSettingsMenu : BasicMenuObject
    {


        public override void Load(Game1 game)
        {
            Title = "Game Settings";
            MyType = "GameSettings";

            AddLine("Player Number: ",true);
            AddLine("Kills to Win: ",true);
            AddLine("AI level: ",true);
            AddLine("Friendly Fire: ",true);
           // AddLine("Done");

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
            UpdateLine(0, "Player Number: " + game.NumbDesiredPlayers.ToString());
            UpdateLine(1, "Kills to Win: " + game.KillsToWin.ToString());
            UpdateLine(2, "AI Level: " + game.ailevel.ToString());

            string text = "Friendly Fire: ";
            if (game.FrendlyFire)
                text += "ON";
            else
                text += "OFF";

            UpdateLine(3, text);

        }

        public override void HitButton(Game1 game, bool AButton, bool BButton,bool Left,bool Right)
        {
            if (Right)
            {
                game.soundHolder.soundEffects["menu_move"].Play(game.SoundEffectsVolume, 0, 0);

                if (PosY == 0)
                {
                    game.NumbDesiredPlayers += 1;


                }
                if (PosY == 1)
                {
                    if (game.KillsToWin == 50)
                        game.KillsToWin = 100;
                    if (game.KillsToWin == 35)
                        game.KillsToWin = 50;
                    if (game.KillsToWin == 25)
                        game.KillsToWin = 35;
                    if (game.KillsToWin == 15)
                        game.KillsToWin = 25;
                    if (game.KillsToWin == 10)
                        game.KillsToWin = 15;         
                }
                if (PosY == 2)
                {
                    if (game.ailevel == Game1.AILevel.Hard)
                        game.ailevel = Game1.AILevel.Insane;
                    if (game.ailevel == Game1.AILevel.Medium)
                        game.ailevel = Game1.AILevel.Hard;
                    if (game.ailevel == Game1.AILevel.Easy)
                        game.ailevel = Game1.AILevel.Medium;
                    if (game.ailevel == Game1.AILevel.VeryEasy)
                        game.ailevel = Game1.AILevel.Easy;

                }
                if (PosY == 3)
                    game.FrendlyFire = !game.FrendlyFire;
            }

            if (Left)
            {
                game.soundHolder.soundEffects["menu_move"].Play(game.SoundEffectsVolume, 0, 0);

                if (PosY == 0)
                {
                    game.NumbDesiredPlayers -= 1;


                }
                if (PosY == 1)
                {
                    if (game.KillsToWin == 15)
                        game.KillsToWin = 10;
                    if (game.KillsToWin == 25)
                        game.KillsToWin = 15;
                    if (game.KillsToWin == 35)
                        game.KillsToWin = 25;
                    if (game.KillsToWin == 50)
                        game.KillsToWin = 35;
                    if (game.KillsToWin == 100)
                        game.KillsToWin = 50;
                }
                if (PosY == 2)
                {
                    if (game.ailevel == Game1.AILevel.Easy)
                        game.ailevel = Game1.AILevel.VeryEasy;
                    if (game.ailevel == Game1.AILevel.Medium)
                        game.ailevel = Game1.AILevel.Easy;
                    if (game.ailevel == Game1.AILevel.Hard)
                        game.ailevel = Game1.AILevel.Medium;
                    if (game.ailevel == Game1.AILevel.Insane)
                        game.ailevel = Game1.AILevel.Hard;

                }
                if (PosY == 3)
                    game.FrendlyFire = !game.FrendlyFire;
            }

            if (AButton||BButton)
            {
                game.menus.GoTo("Play", false);
            }
        }
    }
}
