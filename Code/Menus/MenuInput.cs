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
    public class MenuInput
    {
        public GamePadState NewState;
        public GamePadState OldState;

        public int PlayerNumber = 0;
        public PlayerIndex Currentplayer = PlayerIndex.One;

        public void Load(Game1 game,int i)
        {
            if (i == 0)
                Currentplayer = PlayerIndex.One;
            if (i == 1)
                Currentplayer = PlayerIndex.Two;
            if (i == 2)
                Currentplayer = PlayerIndex.Three;
            if (i == 3)
                Currentplayer = PlayerIndex.Four;

            PlayerNumber = i;
        }

        public void GetInput()
        {
            OldState = NewState;
            NewState = GamePad.GetState(Currentplayer);
        }
    }
}
