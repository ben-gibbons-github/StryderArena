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
    public class MenuInputHandler
    {
        public bool ApressedOnce;
        public bool LeftOnce;
        public bool RightOnce;
        public bool DownOnce;
        public bool UpOnce;
        public bool BpressedOnce;
        public MenuInput[] Inputs = new MenuInput[4];
        public int LastPressedA;
        public bool[] Allowed= new bool[4];
        public bool InSignIn = false;

        public void Load(Game1 game)
        {
            FreeAll();
            for (int i = 0; i < 4; i++)
            {
                Inputs[i] = new MenuInput();
                Inputs[i].Load(game, i);
            }
        }

        public void LimitToOne(int Numb)
        {
            for (int i = 0; i < 4; i++)
                Allowed[i] = false;
            Allowed[Numb] = true;
        }

        public void FreeAll()
        {
            for (int i = 0; i < 4; i++)
                Allowed[i] = true;
        }

        public void Update(Game1 game)
        {
            ApressedOnce = false;
            BpressedOnce = false;
            LeftOnce = false;
            RightOnce = false;
            UpOnce = false;
            DownOnce = false;
           if(!game.menus.IsInGame)
            if(!Guide.IsVisible)
            foreach (MenuInput input in Inputs)
                if(Allowed[input.PlayerNumber])
                {
                    input.GetInput();

                    if (game.ThisGamesPlayers[input.PlayerNumber].InUse||InSignIn)
                    //if(true)
                    {
                        if (!game.menus.IsInGame && input.NewState.IsButtonDown(Buttons.Back) && !input.OldState.IsButtonDown(Buttons.Back))
                            game.ThisGamesPlayers[input.PlayerNumber].Destroy();

                        if (input.NewState.IsButtonDown(Buttons.A) && !input.OldState.IsButtonDown(Buttons.A))
                        {
                            ApressedOnce = true;
                            LastPressedA = input.PlayerNumber;
                        }
                        if (input.NewState.IsButtonDown(Buttons.B) && !input.OldState.IsButtonDown(Buttons.B))
                            BpressedOnce = true;

                        Vector2 Pos = new Vector2(input.NewState.ThumbSticks.Left.X, input.NewState.ThumbSticks.Left.Y);
                        Vector2 OldPos = new Vector2(input.OldState.ThumbSticks.Left.X, input.OldState.ThumbSticks.Left.Y);

                        if (Vector2.Distance(OldPos, Vector2.Zero) < 0.1)
                        {
                            if (Pos.X > 0.1)
                                RightOnce = true;
                            if (Pos.X < -0.1)
                                LeftOnce = true;
                            if (Pos.Y > 0.1)
                                UpOnce = true;
                            if (Pos.Y < -0.1)
                                DownOnce = true;
                        }
                        if (input.NewState.IsButtonDown(Buttons.RightTrigger) && !input.OldState.IsButtonDown(Buttons.RightTrigger))
                            RightOnce = true;
                        if (input.NewState.IsButtonDown(Buttons.LeftTrigger) && !input.OldState.IsButtonDown(Buttons.LeftTrigger))
                            LeftOnce = true;

                    }
                    else if (!game.onlineHandler.IsSearching && !game.onlineHandler.IsCreating)
                    {
                        if (input.NewState.IsButtonDown(Buttons.A) && !input.OldState.IsButtonDown(Buttons.A) && game.menus.MenuCurrent.MyType != "SignInPicker")
                        {

                            if (game.onlineHandler.networkSession == null)
                            {

                                LimitToOne(input.PlayerNumber);
                                game.menus.GoTo("SignInPicker", false);
                                InSignIn = true;

                            }
                            else
                            {

                                game.ErrorMessage = "Players cannot sign in while you are in an online game lobby /n leave the game to sign in more players";
                                game.menus.GoTo("Error", false);

                            }
                        }
                    }
                }

            PlaySounds(game);
        }

        public void PlaySounds(Game1 game)
        {
            if (ApressedOnce)
                game.soundHolder.soundEffects["menu_select"].Play(game.SoundEffectsVolume, 0, 0);
            if (BpressedOnce)
                game.soundHolder.soundEffects["menu_back"].Play(game.SoundEffectsVolume, 0, 0);
            if (UpOnce||DownOnce)
                game.soundHolder.soundEffects["menu_move"].Play(game.SoundEffectsVolume, 0, 0);
        }
    }
}
