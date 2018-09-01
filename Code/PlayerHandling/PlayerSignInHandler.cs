using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orb
{
    public class PlayerSignInHandler
    {
        public CompletePlayer[] Players = new CompletePlayer[4];

        public void Load(Game1 game)
        {
            for (int i = 0; i < 4; i++)
            {
                Players[i] = new CompletePlayer();
                Players[i].Load(i);
            }
        }
    }
}
