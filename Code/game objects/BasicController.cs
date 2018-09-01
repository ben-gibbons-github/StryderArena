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

namespace Orb.game_objects
{
    public class BasicController
    {
        public Vector2 MoveStickTrack = Vector2.Zero;
        public int Kills = 0;
        public int Deaths = 0;
        public float Money = 0;
        public int Number = 0;
        public int Suicides = 0;
        public int DamageTaken = 0;
        public int FlagScore = 0;
        public int DamageGiven=0;
        public int GunUpgrades=0;
        public int AbilityUpgrades = 0;
        public bool Taken = false;
        public bool[,] UnLocked = new bool[4, 2];
        public BasicOrb MyOrb;
        public string Name;
        public float AllMoney = 0;
        public Color color=new Color(1,1,0);
        public Vector4 colorVec=new Vector4(1,1,0,1);
        public int ColorTaken = 0;
        public int Updateddeaths = 0;
        public int UpdatedKills = 0;

        public void Reset(Game1 game)
        {
            MoveStickTrack = Vector2.Zero;
            FlagScore = 0;
            Updateddeaths = 0;
            UpdatedKills = 0;
            Kills = 0;
            Deaths = 0;
            Money = 0;
            Number = 0;
            Taken = false;
            UnLocked = new bool[4, 2];
            UnLocked[0, 0] = true;
            UnLocked[0, 1] = true;
            MyOrb = null;
            Name = "";
            AllMoney = 0;
            Suicides = 0;
            DamageGiven = 0;
            DamageTaken = 0;
            color = Color.Blue;
            GunUpgrades = 0;
            AbilityUpgrades = 0;
            
        }
    }
}
