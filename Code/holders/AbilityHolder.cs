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
    public class AbilityHolder
    {
        public const int AbilityNumb=6;
        public Texture2D[] AbilityIcon = new Texture2D[AbilityNumb];
        public float[] AbilityCost = new float[AbilityNumb];
        public Texture2D[] AbilityTexture = new Texture2D[AbilityNumb];
        
         public int Temp = 0;

        public void Load(Game1 game)
        {
            AbilityIcon[Temp] = game.Content.Load<Texture2D>("Ability_icons/icon_shield");
            AbilityCost[Temp] = 50;
            AbilityTexture[Temp] = game.Content.Load<Texture2D>("gun_outline/shield_outline");
            Temp+=1;

            AbilityIcon[Temp] = game.Content.Load<Texture2D>("Ability_icons/airstrike_icon");
            AbilityCost[Temp] = 50;
            Temp += 1;


            AbilityIcon[Temp] = game.Content.Load<Texture2D>("Ability_icons/icon_dart");
            AbilityTexture[Temp] = game.Content.Load<Texture2D>("gun_outline/dart_outline");
            AbilityCost[Temp] = 25;
            Temp += 1;

            AbilityIcon[Temp] = game.Content.Load<Texture2D>("Ability_icons/icon_mine");
            AbilityCost[Temp] = 25;
            Temp += 1;

            AbilityIcon[Temp] = game.Content.Load<Texture2D>("Ability_icons/icon_cloak");
            AbilityTexture[Temp] = game.Content.Load<Texture2D>("gun_outline/cloak_outline");
            AbilityCost[Temp] = 25;
            Temp += 1;


            AbilityIcon[Temp] = game.Content.Load<Texture2D>("Ability_icons/icon_turret");
            AbilityTexture[Temp] = game.Content.Load<Texture2D>("gun_outline/turret_outline");
            AbilityCost[Temp] = 25;
            Temp += 1;
        }
        public int Translate(float Val)
        {
            int Ret = 0;
            if (Val == 4)
                Ret= 1;
            if(Val==6)
                Ret= 2;
            if (Val == 7)
                Ret = 3;
            if (Val == 8)
                Ret = 4;
            if (Val == 9)
                Ret = 5;
            return Ret;
        }
        
    }
}
