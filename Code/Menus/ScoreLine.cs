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
    public class ScoreLine : BasicMenuLine
    {

        public ScoreHolder MyScore;
        public bool Base = false;

        public override void Draw(Game1 game, BasicMenuObject host, Vector2 CurrentDrawPos,Vector2 Mult)
        {
            //Alpha = 1;
            //MyScore.Name = "test";

            Vector4 vec = new Vector4(0.3f, 0.3f, 0.6f, 1);
            Color DrawColor=Color.White;
            

            string[] text = new string[6];
            if (game.gamemode != Game1.GameMode.KeepAway)
            {
                if (!Base)
                {
                    text[0] = MyScore.Name;
                    text[1] = MyScore.Kills.ToString();
                    text[2] = MyScore.Deaths.ToString();
                    text[3] = MyScore.Suicides.ToString();

                    if (game.gamemode == Game1.GameMode.DeathMatch || game.gamemode == Game1.GameMode.TeamDeathMatch)
                    {
                        text[4] = MyScore.GunUpgrades.ToString();
                        text[5] = MyScore.AbilityUpgrades.ToString();
                    }
                    else
                    {
                        text[4] = null;
                        text[5] = null;
                    }

                    DrawColor = new Color(MyScore.ColorVec);
                }
                else
                {
                    text[0] = "Player";
                    text[1] = "Kills";
                    text[2] = "Deaths";
                    text[3] = "Suicides";

                    if (game.gamemode == Game1.GameMode.DeathMatch || game.gamemode == Game1.GameMode.TeamDeathMatch)
                    {
                        text[4] = "Gun Upgrades";
                        text[5] = "Ability Upgrades";
                    }
                    else
                    {
                        text[4] = null;
                        text[5] = null;
                    }

                }
            }
            else
            {
                if (!Base)
                {
                    text[0] = MyScore.Name;
                    text[1] = MyScore.FlagScore.ToString();
                    text[2] = MyScore.Kills.ToString();
                    text[3] = MyScore.Deaths.ToString();
                    text[4] = MyScore.Suicides.ToString();
                    text[5] = null;

                    DrawColor = new Color(MyScore.ColorVec);
                }
                else
                {
                    text[0] = "Player";
                    text[1] = "Flag Score";
                    text[2] = "Kills";
                    text[3] = "Deaths";
                    text[4] = "Suicides";
                    text[5] = null;

                }
            }

            Rectangle HUDRECT = new Rectangle();


            HUDRECT.Height = (int)((game.Loader.font.MeasureString("Test").Y + 20)*Mult.Y);
            HUDRECT.Width = (int)(10000*Mult.X);
            HUDRECT.Y = (int)((CurrentDrawPos.Y - 10)*Mult.Y);
            HUDRECT.X = (int)(-2500*Mult.X);
            game.spriteBatch.Draw(game.Loader.MenuWindow, HUDRECT, DrawColor);

            foreach (string Text in text)
                if(Text!=null)
            {
                

                CurrentDrawPos.X += -game.Loader.font.MeasureString(Text).X / 2;



                //Text = "test";
                //game.spriteBatch.DrawString(game.Loader.font, Text, CurrentDrawPos+new Vector2(2,2), Color.Black);
                game.spriteBatch.DrawString(game.Loader.font, Text, CurrentDrawPos*Mult, Color.White,0,Vector2.Zero,Mult,SpriteEffects.None,0);

                CurrentDrawPos.X += game.Loader.font.MeasureString(Text).X / 2;

                CurrentDrawPos.X += 200;
            }
            // CurrentDrawPos += host.DrawSeperation;
        }

    }
}
