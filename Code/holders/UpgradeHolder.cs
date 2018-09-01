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
    public class UpgradeHolder
    {
        public const int MaxUpgradeSections=2;
        public const int MaxUpgrades=4;

        public string[] SectionName =new string[MaxUpgradeSections];
        public string[,] UpgradeDescription = new string[MaxUpgrades,MaxUpgradeSections];
        public Vector2[,] UpgradePosition = new Vector2[MaxUpgrades, MaxUpgradeSections];
        public float[,] UpgradeCost = new float[MaxUpgrades, MaxUpgradeSections];
        public Texture2D[,] UpgradePicture = new Texture2D[MaxUpgrades, MaxUpgradeSections];
        public bool[, ,] UpgradeHasBeenBought = new bool[MaxUpgrades, MaxUpgradeSections, 16];
        public string[,] UpgradeName = new string[MaxUpgrades, MaxUpgradeSections];
        public int[,] UpgradeSet = new int[MaxUpgrades, MaxUpgradeSections];

        int Numb = -1;
        int Section = -1;

        public void load(Game1 game)
        {
            for (int a = 0; a < MaxUpgrades; a++)
                for (int b = 0; b < MaxUpgradeSections; b++)
                    for (int c = 0; c < 16; c++)
                        UpgradeHasBeenBought[a, b, c] = false;

            Section += 1;
            SectionName[Section] = "Select a Gun";
            Numb = -1;

            Numb += 1;
            UpgradeCost[Numb, Section] = 0;
            UpgradeDescription[Numb, Section] = " Standard issue sub machine gun /n Includes attatched grenade launcher";
            UpgradeName[Numb, Section] = "Machine Gun";
            UpgradeSet[Numb, Section] = 0;

            Numb += 1;
            UpgradeCost[Numb, Section] = 75;
            UpgradeDescription[Numb, Section] = " P40 Plasma Gun /n Low bullet velocity is made up for by high damage /n Grenades attatch themselves to nearby enemy targets";
            UpgradeName[Numb, Section] = "Plasma Gun";
            UpgradeSet[Numb, Section] = 1;

            Numb += 1;
            UpgradeCost[Numb, Section] = 200;
            UpgradeDescription[Numb, Section] = " Special Ops Laser Rifle /n Fires supercompressed beams of radiation /n Beams can be combined into an extradimensional dark matter bomb";
            UpgradeName[Numb, Section] = "Laser Rifle";
            UpgradeSet[Numb, Section] = 3;

            Numb += 1;
            UpgradeCost[Numb, Section] = 400;
            UpgradeName[Numb, Section] = "Rocket Launcher";
            UpgradeDescription[Numb, Section] = " Unmatched in power, /n the R102 new issue rocket launcher has a new secondary /n combining the power of the rocket launcher with the /n homing capabilities of the plasma grenade";

            UpgradeSet[Numb, Section] = 4;


            Section += 1;
            SectionName[Section] = "Select an Ability";
            Numb = -1;



            Numb += 1;
            UpgradeCost[Numb, Section] = 50;
            UpgradeName[Numb, Section] = "Shield";
            UpgradeDescription[Numb, Section] = " Standard issue tacticle shield /n can be recharded from the life forces of /n dead opponents /n placing a shield next to an opponent's /n disables his shield";
            UpgradeSet[Numb, Section] = 3;

            Numb += 1;
            UpgradeCost[Numb, Section] = 125;
            UpgradeName[Numb, Section] = "Darts";
            UpgradeDescription[Numb, Section] = " Vampire darts allow allow wounded soldiers to steal /n vitality from enemy combatants /n also colapses shields";
            UpgradeSet[Numb, Section] = 6;

            Numb += 1;
            UpgradeCost[Numb, Section] = 200;
            UpgradeName[Numb, Section] = "Cloak";
            UpgradeDescription[Numb, Section] = " Modern cloak technology allows soldiers to completely /n hide themselves from enemy sensors /n uses up power at a high rate while cloaked";
            UpgradeSet[Numb, Section] = 8;

            Numb += 1;
            UpgradeCost[Numb, Section] = 0;
            UpgradeName[Numb, Section] = "Turret";
            UpgradeDescription[Numb, Section] = " Speical Ops Predator Turrets are for soldiers who want to take /n out heavily armed opponents without putting themselves in harm's way /n Turrets will automaticly self destruct /n once they have discharged all their rounds";
            UpgradeSet[Numb, Section] = 9;

        }

    }
}
