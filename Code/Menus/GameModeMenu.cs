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
    public class GameModeMenu : BasicMenuObject
    {


        public override void Load(Game1 game)
        {
            Title = "Game Mode";

            MyType = "GameMode";

            AddLine("DeathMatch",false);
            AddLine("Team DeathMatch", false);
            AddLine("Assasin", false);
            AddLine("Warlord", false);
            AddLine("Downgrade", false);
            AddLine("Keepaway", false);

            DrawPos = new Vector2(600, 200);

            MaxY = 5;
            MinY = 0;
            Tier = 1;

        }

        public override void GoToMenu(Game1 game)
        {
            //throw new NotImplementedException();
        }

        public override void Update(Game1 game)
        {
            game.menus.AlphaTextAlpha = 1;
            if (PosY == 0)
            {
                game.gamemode = Game1.GameMode.DeathMatch;
                game.menus.AlphaText = " standard deathmatch /n players choose new /n upgrades every kill tier";
            }
            if (PosY == 1)
            {
                game.gamemode = Game1.GameMode.TeamDeathMatch;
                game.menus.AlphaText = " same as deathmatch but /n played on teams /n score is commulative /n for the entire team";
            }
            if (PosY == 2)
            {
                game.gamemode = Game1.GameMode.Assasin;
                game.menus.AlphaText = " one player begins as /n the loner and all the /n other players try to assasinate /n him assasins get infinate cloak /n while the loner gets a rocket /n launcher and extra health /n if one of the assains kills the /n loner, he becomes the new loner";
            }
                if (PosY == 3)
                {
                game.gamemode = Game1.GameMode.WarLord;
                game.menus.AlphaText = " same as deathmatch but /n instead of upgrade tiers, /n players gain upgrades randomly /n by killing other players /n if you kill a player with more /n upgrades then you, you gain all /n his upgrades till you kill /n someone any player who dies /n immediatly loses all his /n upgrades";
                }
            if (PosY == 4)
            {
                game.gamemode = Game1.GameMode.DownGrade;
                game.menus.AlphaText = " all players start with /n  the highest upgrades /n players lose upgrades by killing and /n gain upgrades by being killed";
            }
            if (PosY == 5)
            {
                game.gamemode = Game1.GameMode.KeepAway;
                game.menus.AlphaText = " players fight over a flag /n which spawns in the center of /n the map instead of through /n kills, players gain points /n by holding the flag /n players gain upgrades by /n picking up the flag";
            }
        }

        public override void HitButton(Game1 game, bool AButton, bool BButton,bool Left,bool Right)
        {

            if (AButton||BButton)
            {

                game.menus.GoTo("Play",false);
            }
        }
    }
}
