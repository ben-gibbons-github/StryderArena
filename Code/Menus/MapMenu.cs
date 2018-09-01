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
    public class MapMenu : BasicMenuObject
    {


        public override void Load(Game1 game)
        {
            Title = "Select Map";
            MyType = "Map";

            AddLine("Desert Base Large", false);
            AddLine("Desert Base Small", false);
            AddLine("Botanical Cave 1V1", false);
            AddLine("Botanical Cave Large", false);
            AddLine("Train Station Small", false);
            AddLine("Train Station Medium", false);
            AddLine("Train Station Huge", false);
            AddLine("Ice Mountain Large", false);
            AddLine("Ice Mountain Small", false);
           // AddLine("Ice Mountain Huge", false);
           // AddLine("Assasin");
            //AddLine("Basic");

            DrawPos = new Vector2(600, 100);

            MaxY = 8;
            MinY = 0;
            Tier = 1;

        }

        public override void GoToMenu(Game1 game)
        {
            //throw new NotImplementedException();
        }

        public override void Update(Game1 game)
        {
            if (PosY == 0)
                game.map = Game1.Map.Desert_Base_Large;
            if(PosY==1)
                game.map = Game1.Map.Desert_Base_Small;
            if (PosY == 2)
                game.map = Game1.Map.Botanical_Cave1V1;
            if (PosY == 3)
                game.map = Game1.Map.Botanical_Cave_Large;
            if (PosY == 4)
                game.map = Game1.Map.Train_Station_Small;
            if (PosY == 5)
                game.map = Game1.Map.Train_Station_Medium;
            if (PosY == 6)
                game.map = Game1.Map.Train_Station_Huge;
            if (PosY == 7)
                game.map = Game1.Map.Ice_Mountain_Large;
            if (PosY == 8)
                game.map = Game1.Map.Ice_Mountain_Small;
        }

        public override void HitButton(Game1 game, bool AButton, bool BButton, bool Left, bool Right)
        {

            if (AButton)
            {

                game.menus.GoTo("Play", false);
            }
            if (BButton)
                game.menus.GoTo("Play", false);
        }
    }
}
