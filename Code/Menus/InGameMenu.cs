using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orb
{
    public class InGameMenu:BasicMenuObject
    {
        public int Timer = 0;

        public override void Load(Game1 game)
        {
            MyType = "InGame";
          //  throw new NotImplementedException();
        }

        public override void Update(Game1 game)
        {
            game.SendScores();
            if(false)
            if (!game.IsInMenu)
            {
                Timer++;
                if (Timer == 300)
                {
                    //foreach (BasicOrb orb in game.Orbs)
                    if (!game.Orbs[0].IsAI)
                        game.Orbs[0].ChangeToAI(game);

                   // if (!game.Orbs[3].IsLocal)
                    //    game.Orbs[3].ChangeToLocal(game, null);
                }

            }

            if (game.GameOver)
            {


                game.GameOverTime += 1;
                if (game.GameOverTime > 150)
                    game.menus.GoTo("Score", true);
            }
            else
                if (game.gamemode != Game1.GameMode.TeamDeathMatch)
                {
                    foreach (BasicOrb orb in game.Orbs)
                        if (orb.relevent)
                        {
                            if (orb.MyController.Kills >= game.KillsToWin&&game.gamemode!=Game1.GameMode.KeepAway||orb.MyController.FlagScore>=game.KillsToWin&&game.gamemode==Game1.GameMode.KeepAway)
                            {
                                game.Winner = orb;
                                game.GameOver = true;
                            }
                        }
                }
                else
                {
                    for(int i=0;i<2;i++)
                        if (game.TeamScore[i] >= game.KillsToWin)
                        {
                            game.GameOver = true;
                            game.WinningTeam = i;
                        }
                }
           // throw new NotImplementedException();
            //base.Update(game);
        }

        public override void GoToMenu(Game1 game)
        {
            //throw new NotImplementedException();
        }

        public override void HitButton(Game1 game, bool AButton, bool BButton, bool Left, bool Right)
        {
            //throw new NotImplementedException();
        }
    }
}
