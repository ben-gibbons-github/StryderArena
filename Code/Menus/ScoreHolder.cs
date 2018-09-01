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
    public class ScoreHolder
    {
        public bool Used = false;
        public bool Sorted = false;
        public string Name;
        public int Kills;
        public int Deaths;
        public int FlagScore;
        public int Suicides;
        public int DamageGiven;
        public int DamageTaken;
        public int GunUpgrades;
        public int AbilityUpgrades;
        public Vector4 ColorVec=Vector4.One;
    }
}
