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
    public class ScoreMenu : BasicMenuObject
    {


        public override void Load(Game1 game)
        {
            MyType = "Score";

            Title = "Player Scores";
            

            DrawPos = new Vector2(150, 100);

            PosY = 0;
            MinY = 0;

        }

        public override void GoToMenu(Game1 game)
        {
            Lines= new List<BasicMenuLine>();
            MaxY = 0;
            PosY = 1;

            ScoreLine baseline = AddScoreLine();
            baseline.Base = true;


            int ScoreCount = 0;

            foreach (ScoreHolder score in game.scores)
                if (score.Used)
                {
                    ScoreCount++;
                    score.Sorted = false;
                }

            ScoreHolder BestPlayer=null;

            List<ScoreHolder> ScoreList = new List<ScoreHolder>(ScoreCount);

            for (int i = 0; i < ScoreCount; i++)
            {
                int BestScore = 0;
                int BestDeaths = 100000;

                foreach (ScoreHolder score in game.scores)
                    if (score.Used&&!score.Sorted)
                    {
                        int ThisScore = score.Kills;
                        if (game.gamemode == Game1.GameMode.KeepAway)
                            ThisScore = score.FlagScore;

                        if (ThisScore > BestScore || ThisScore == BestScore && score.Deaths < BestDeaths)
                        {
                            BestPlayer = score;
                            BestDeaths = score.Deaths;
                            BestScore = ThisScore;
                        }
                    }
                if (BestPlayer != null)
                {
                    BestPlayer.Sorted = true;
                    //BestPlayer.Used = false;
                    ScoreList.Add(BestPlayer);
                }
            }


                foreach (ScoreHolder score in ScoreList)
                    if (score.Used)
                    {
                        ScoreLine line = AddScoreLine();
                        line.MyScore = score;
                        line.Base = false;
                        MaxY += 1;
                    }

            //throw new NotImplementedException();
        }

        public override void Update(Game1 game)
        {

        }

        public override void HitButton(Game1 game, bool AButton, bool BButton, bool Left, bool Right)
        {
            if (AButton||BButton)
                game.menus.GoTo("Main", true);
        }
    }
}
