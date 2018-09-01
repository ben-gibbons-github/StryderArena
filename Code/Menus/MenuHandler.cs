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

namespace Orb
{
    public class MenuHandler
    {
        public BasicMenuObject MenuCurrent;
        public BasicMenuObject MenuTarget;
        public float PageFade = 1;
        public float AlphaTextAlpha = 0;
        public string AlphaText = " ";

        public bool IsFadingOut = false;
        public bool IsFadingIn = true;
        public bool UseFade = true;
        public float FromFade = 0;
        public int CloseTimer = 0;
        public int MaxTiers = 3;
        public bool IsInGame = false;
        public Vector2 DrawPos = new Vector2(800, 200);
        public string OldMenu = "";

        public Vector2 Mult;

        public const int MaxMenus = 15;

        public BasicMenuObject[] AllMenus = new BasicMenuObject[MaxMenus];

        public MenuInputHandler Input = new MenuInputHandler();



        public void Load(Game1 game)
        {
            Mult = new Vector2(game.resolutionx, game.resolutiony) / new Vector2(1320, 840);

            Input.Load(game);

            

            AllMenus[0] = new InGameMenu();
            AllMenus[1] = new StartMenu();
             AllMenus[2] = new MainMenu();
             AllMenus[3] = new GameModeMenu();
             AllMenus[4] = new ScoreMenu();
             AllMenus[5] = new PlayMenu();
             AllMenus[6] = new SignInPickerMenu();
             AllMenus[7] = new GameSettingsMenu();
             AllMenus[8] = new MapMenu();
             AllMenus[9] = new NetworkSessionMenu();
             AllMenus[10] = new OnlineMenu();
             AllMenus[11] = new ErrorMessage();
             AllMenus[12] = new HowToMenu();
             AllMenus[13] = new SplashMenu();
             AllMenus[14] = new OptionsMenu();

                 MenuCurrent = AllMenus[1];
                 MenuTarget = AllMenus[1];
           
            foreach (BasicMenuObject menu in AllMenus)
                menu.Load(game);
            
        }

        public void Update(Game1 game)
        {
            AlphaTextAlpha -= 0.15f;

            if(!IsInGame&&MenuCurrent!=AllMenus[1]&&MenuCurrent!=AllMenus[13])
            {
                if (!game.IsInMenu)
                {
                    game.EndGame();
                    game.NewScreen();
                    
                }

                HandleInput(game);
                HandleSignIn(game);
            }
            else if (MenuCurrent == AllMenus[1]||MenuCurrent==AllMenus[13])
            {
                CloseTimer++;
                if (CloseTimer > 200)
                {
                    CloseTimer = 0;
                    if (MenuCurrent == AllMenus[13])
                        GoTo("Main", true);
                    else
                        GoTo("Splash", true);
                }
            }

            if (MenuCurrent != null)
            {

            }
            if(MenuCurrent!=null)
                MenuCurrent.Update(game);

            foreach (BasicMenuObject menu in AllMenus)
            {
                float ChangeAmount = 0.1f;

               // if (MenuCurrent == AllMenus[1])
                 //   ChangeAmount = 10f;
                if (MenuCurrent==AllMenus[1]&&MenuCurrent!=MenuTarget)
                    ChangeAmount = 0.01f;
                if (MenuCurrent == AllMenus[13] && MenuCurrent != MenuTarget)
                    ChangeAmount = 0.01f;

                if (menu == MenuCurrent && menu == MenuTarget)
                    menu.MenuAlpha += ChangeAmount;
                else
                {
                    if(MenuCurrent.Tier<=menu.Tier&&menu!=MenuCurrent||MenuTarget.Tier<=menu.Tier&&menu!=MenuTarget)
                        menu.MenuAlpha -= ChangeAmount;
                }
                menu.MenuAlpha = MathHelper.Clamp(menu.MenuAlpha, 0, 1);


                if (menu == MenuCurrent)
                    MenuCurrent.ControlLines(game);
                
            }

            int reps = 1;
            if (!UseFade)
                reps = 4;
            for (int i = 0; i < reps; i++)
            {
                if (IsFadingIn)
                {
                    PageFade -= 0.025f;
                    if (PageFade < 0)
                    {
                        IsFadingIn = false;
                        PageFade = 0;
                    }
                }
                if (IsFadingOut)
                {
                    

                    IsFadingIn = false;
                    PageFade += 0.025f;
                    if (PageFade > 1)
                    {
                        GoToNewMenu(game);
                        
                        MenuCurrent = MenuTarget;
                        if (MenuCurrent.MyType != "InGame")
                            IsInGame = false;
                        else
                            IsInGame = true;
                        BeginNewMenu(game);
                        MenuCurrent.GoToMenu(game);
                        IsFadingOut = false;
                        IsFadingIn = true;

                        PageFade = 1;
                    }
                }
            }

        }

        

        public void HandleSignIn(Game1 game)
        {
           
          //  foreach (CompletePlayer player in game.ThisGamesPlayers)
               // if(player.MyGamer.IsDisposed) //if(player.IsProfile)
              //  player.InUse = false;
            
            foreach (SignedInGamer gamer in Gamer.SignedInGamers)
            {
                int i=0;
                if(gamer.PlayerIndex==PlayerIndex.One)
                    i=0;
                if(gamer.PlayerIndex==PlayerIndex.Two)
                    i=1;
                if(gamer.PlayerIndex==PlayerIndex.Three)
                    i=2;
                if(gamer.PlayerIndex==PlayerIndex.Four)
                    i=3;

                if (!game.ThisGamesPlayers[i].InUse)
                {
                    
                    game.menus.GoTo("Main", false);
                    game.menus.Input.FreeAll();
                    game.menus.Input.InSignIn = false;
                }
                game.ThisGamesPlayers[i].Create(gamer.Gamertag,gamer);
              //  game.ThisGamesPlayers[i].MyGamer = gamer;
            }


        }

        public void HandleInput(Game1 game)
        {
            if (!IsInGame)
            {
                Input.Update(game);

                //if (Input.RightOnce)
                //     MenuCurrent.PosX += 1;
                //if (Input.LeftOnce)
                //     MenuCurrent.PosX -= 1;
                if (Input.UpOnce)
                    MenuCurrent.PosY -= 1;
                if (Input.DownOnce)
                    MenuCurrent.PosY += 1;

                MenuCurrent.PosX = (int)MathHelper.Clamp(MenuCurrent.PosX, MenuCurrent.MinX, MenuCurrent.MaxX);
                MenuCurrent.PosY = (int)MathHelper.Clamp(MenuCurrent.PosY, MenuCurrent.MinY, MenuCurrent.MaxY);


                MenuCurrent.HitButton(game, Input.ApressedOnce, Input.BpressedOnce, Input.LeftOnce, Input.RightOnce);
            }
        }

        public void GoTo(string Target,bool usefade)
        {
            
            foreach (BasicMenuObject menu in AllMenus)
                if (menu.MyType == Target)
                {
                    OldMenu = MenuCurrent.MyType;
                    UseFade = usefade;
                    IsFadingOut = true;
                    IsFadingIn = false;
                    MenuTarget = menu;
                }
        }

        public void GoToHighest()
        {
            IsFadingIn = true;
            IsFadingOut = false;
            UseFade = false;

            bool found=false;
            for (int i = 0; i <= MaxTiers;i++ )
                if (!found)
                foreach (BasicMenuObject menu in AllMenus)
                    if(!found)
                        if(menu.MenuAlpha==1)
                            if(menu!=MenuCurrent)
                    if (menu.Tier == i)
                    {
                        found = true;
                        MenuTarget = menu;
                    }

        }

        public void GoToNewMenu(Game1 game)
        {
            if (MenuCurrent.MyType == "InGame")
            {
                game.EndGame();
                game.NewScreen();
            }
            if (MenuTarget.MyType == "InGame")
                game.EndGame();
        }

        public void BeginNewMenu(Game1 game)
        {
            if (MenuCurrent.MyType == "InGame")
                game.Newgame();
        }

        public void Draw(Game1 game)
        {
            if (!IsInGame)
            {

               
                game.DrawFullscreenQuad(game.Loader.Temp, BlendState.AlphaBlend, null, new Color(0, 0, 0, 0.4f));
               


                DrawPos = new Vector2(1000, 200);
                if (MenuCurrent.MyType != "Score" && MenuCurrent.MyType != "SignInPicker"&&MenuCurrent.MyType!="GameMode")
                    foreach (CompletePlayer player in game.ThisGamesPlayers)
                        if (player.InUse)
                            player.Draw(game, this, Mult);


                for (int i = 0; i <= MaxTiers; i++)
                    foreach (BasicMenuObject menu in AllMenus)
                        if (menu.Tier == i)
                            if (menu.MenuAlpha > 0&&menu!=AllMenus[1]||menu==MenuCurrent)
                            {
                                menu.Mult = Mult;
                                menu.Draw(game);
                            }


                game.spriteBatch.Begin();
                game.spriteBatch.DrawString(game.Loader.font, AlphaText.Replace("/n", "\n"), new Vector2(850, 300) * Mult, new Color(Vector4.One * AlphaTextAlpha), 0, Vector2.Zero, Mult, SpriteEffects.None, 0);

                Rectangle HUDRECT = new Rectangle();

                if (MenuCurrent.MyType == "HowTo")
                {

                    if (!game.HowTo.HasLoaded)
                        game.HowTo.Load(game);
                   

                    Color Col = new Color(Vector4.One*MenuCurrent.MenuAlpha);
                    HUDRECT.Height = (int)(789 * Mult.X);
                    HUDRECT.Width = (int)(1320 * Mult.X);
                    HUDRECT.Y = (int)(game.resolutiony / 2 - 789 * Mult.X / 2);
                    HUDRECT.X = 0;
                    game.spriteBatch.Draw(game.HowTo.HowToTexture[MenuCurrent.PosX], HUDRECT, Col);
                    game.spriteBatch.Draw(game.HowTo.HowToTexture[game.HowTo.numbSteps - 1], HUDRECT, new Color(Vector4.One * MenuCurrent.MenuAlpha*0.2f));

                }
                if (MenuCurrent.MyType != "Score" && MenuCurrent.MyType != "Start" && MenuCurrent.MyType != "Splash" && MenuCurrent.MyType != "HowTo" && MenuCurrent.MyType != "SignIn" && MenuCurrent.MyType != "Map")
                {
                    HUDRECT.Height = (int)(1024 / 1.25f * Mult.Y);
                    HUDRECT.Width = (int)(1024 / 1.25f * Mult.Y);
                    HUDRECT.Y = (int)(game.resolutiony / 2 - 1024 * Mult.Y / 2) - (int)(1024 / 5f * Mult.Y);
                    HUDRECT.X = (int)(game.resolutionx / 2 - 1024 * Mult.Y / 2) - (int)(1024 / 30 * Mult.Y);
                    game.spriteBatch.Draw(game.TitleTexture, HUDRECT, Color.White);
                }

                game.spriteBatch.End();

                if (AllMenus[1] != MenuCurrent && FromFade > 0 && AllMenus[13] != MenuCurrent)
                {
                    FromFade -= 0.01f;
                    game.DrawFullscreenQuad(game.Loader.Temp, BlendState.AlphaBlend, null, new Color(0, 0, 0, FromFade));
                }
                else if (AllMenus[1] == MenuCurrent || AllMenus[13] == MenuCurrent)
                    FromFade = 1;
            }
            if (PageFade != 0&&UseFade)
            {
                game.DrawFullscreenQuad(game.Loader.Temp,BlendState.AlphaBlend, null,new Color(0,0,0,PageFade));
            }


        }

        
    }
}
