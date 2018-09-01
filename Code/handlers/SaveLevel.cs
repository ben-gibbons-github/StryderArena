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
using System.Xml.Serialization;
using System.Xml;
//using Microsoft.Xna.Framework.Design;


using System.IO;
//using System.Xml.Serialization;
using System.Diagnostics;

namespace Orb
{
    //[Serializable]
    public class SaveLevel
    {

        public const int MaxBlocks = 800;
        public const int MaxSpecials = 400;
        public const int MaxFloors = 400;
        public const int MaxSlots = 5;
        public const int MaxLights = 100;
        public int maxBlocks = MaxBlocks;
        public int maxSpecials = MaxSpecials;
        public int maxFloors = MaxFloors;
        public int maxSlots = MaxSlots;
        public int maxLights = MaxLights;

        public int BlockNumber;
        public bool[] BlockRelevent = new bool[MaxBlocks];
        public int[] BlockType = new int[MaxBlocks];
        public int[] BlockRespawnTime = new int[MaxBlocks];
        public Vector3[] BlockPos = new Vector3[MaxBlocks];
        public Vector3[] BlockRotation = new Vector3[MaxBlocks];
        public Vector2[] BlockSize = new Vector2[MaxBlocks];
        public bool[] BlockDestructible = new bool[MaxBlocks];
        public bool[] BlockSwitchDraw = new bool[MaxBlocks];
        public bool[] BlockPhaseBlock = new bool[MaxBlocks];
        public int[] MaxLife = new int[MaxBlocks];

        public int SpecialNumber=0;
        public Vector3[] SpecialPos = new Vector3[MaxSpecials];
        public bool[] SpecialRelevent = new bool[MaxSpecials];
        public int[] SpecialType = new int[MaxSpecials];
        public int[] SpecialValue = new int[MaxSpecials*5];
        public bool[] SpecialSwitchDraw = new bool[MaxBlocks];
        public Vector3[] SpecialRotation = new Vector3[MaxSpecials];
        public Vector2[] SpecialSize = new Vector2[MaxSpecials];

        public int FloorNumber = 0;
        public Vector3[] FloorPos = new Vector3[MaxFloors];
        public bool[] FloorRelevent = new bool[MaxFloors];
        public int[] FloorType = new int[MaxFloors];
        public bool[] FloorSwitchDraw = new bool[MaxFloors];
        public Vector3[] FloorRotation = new Vector3[MaxFloors];
        public Vector2[] FloorSize = new Vector2[MaxFloors];

        public int LightNumber;
        public bool[] LightRelevent = new bool[MaxLights];
        public int[] LightType = new int[MaxLights];
        public Vector3[] LightPos = new Vector3[MaxLights];
        public Vector3[] LightColor = new Vector3[MaxLights];
        public float[] LightDistance = new float[MaxLights];
    }
}
