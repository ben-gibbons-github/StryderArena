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
    public class HowToMenu : BasicMenuObject
    {

       // int CurrentHowTo = 0;
        public float TimeOut = 0;

        public override void Load(Game1 game)
        {
            QuadAlpha = 100;

            

            Title = "HowTo";

            MyType = "HowTo";


            DrawPos = new Vector2(-1000, -1000);

            MaxY = -1;
            MinY = -1;
            Tier = 1;
            MaxX = game.HowTo.numbSteps;
            MinX = 0;

        }
        public override void GoToMenu(Game1 game)
        {
            //throw new NotImplementedException();
        }

        public override void Update(Game1 game)
        {
            TimeOut += 1;
        }

        public override void HitButton(Game1 game, bool AButton, bool BButton, bool Left, bool Right)
        {
            if (AButton || BButton)
            {
                game.menus.GoTo("Main",false);
            }

            if (TimeOut > 10)
            {
                //if(Right||Left)



                if (Left)
                {
                    game.soundHolder.soundEffects["menu_move"].Play(game.SoundEffectsVolume, 0, 0);
                    PosX -= 1;
                    TimeOut = 0;
                }
                if (Right)
                {
                    game.soundHolder.soundEffects["menu_move"].Play(game.SoundEffectsVolume, 0, 0);
                    PosX += 1;
                    TimeOut = 0;
                }
            }
            //CurrentHowTo = PosX;

            PosX = (int)MathHelper.Clamp(PosX, 0, game.HowTo.numbSteps-2);
        }
    }
}
