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
    public class OptionsMenu : BasicMenuObject
    {


        public override void Load(Game1 game)
        {
            Title = "Options";
            MyType = "Options";

            AddLine("Music Volume: ", true);
            AddLine("SFX Volume: ", true);
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


            UpdateLine(0, "Music Volume: " + (Math.Round(game.MusicVolume*10)/10).ToString());
            UpdateLine(1, "SFX Volume: " + (Math.Round(game.SoundEffectsVolume*10)/10).ToString());


        }

        public override void HitButton(Game1 game, bool AButton, bool BButton, bool Left, bool Right)
        {
            if (Right)
            {
                game.soundHolder.soundEffects["menu_move"].Play(game.SoundEffectsVolume, 0, 0);
                if (PosY == 0)
                    game.MusicVolume += 0.1f;
                if (PosY == 1)
                    game.SoundEffectsVolume += 0.1f;

            }

            if (Left)
            {
                game.soundHolder.soundEffects["menu_move"].Play(game.SoundEffectsVolume, 0, 0);
                if (PosY == 0)
                    game.MusicVolume -= 0.1f;
                if (PosY == 1)
                    game.SoundEffectsVolume -= 0.1f;

            }
            game.MusicVolume = MathHelper.Clamp(game.MusicVolume, 0, 1);
            game.SoundEffectsVolume= MathHelper.Clamp(game.SoundEffectsVolume, 0, 1);

            if (AButton || BButton)
            {
                game.menus.GoTo("Main", false);
            }
        }
    }
}
