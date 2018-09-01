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
    public class SpecialHolder
    {
        const int MaxSpecials = 4;
        public int MaxSpecialsp = MaxSpecials;
        const int MaxControls = 5;

        public string[] SpecialName = new string[MaxSpecials];
        public float[] SpecialControlsNumber = new float[MaxSpecials];
        public Model[] SpecialModel = new Model[MaxSpecials];
        public Vector3[] SpecialSize = new Vector3[MaxSpecials];
        public Vector3[] SpecialMaxSize = new Vector3[MaxSpecials];
        public bool[] SpecialUseModel = new bool[MaxSpecials];
        public bool[] SpecialInGameModel = new bool[MaxSpecials];
        public bool[] AtatchLight = new bool[MaxSpecials];
        public float[] LightDistance = new float[MaxSpecials];
        public Vector3[] LightColor = new Vector3[MaxSpecials];
        public float[] LightZ = new float[MaxSpecials];
        public string[,] ControlName = new string[MaxSpecials,MaxControls];
        public int[,] ControlMin = new int[MaxSpecials, MaxControls];
        public int[,] ControlMax = new int[MaxSpecials, MaxControls];
        public int[,] ControlDefault = new int[MaxSpecials, MaxControls];
        public string[,] ControlType = new string[MaxSpecials, MaxControls];
        public Effect[] SpecialEffect = new Effect[MaxSpecials];
        public bool[] LightIsState = new bool[MaxSpecials];

        int Temp = -1;
        int Temp2 = -1;

        public void Load(Game1 game)
        {

            Temp += 1;
            Temp2 = -1;

            SpecialName[Temp] = "Player Spawn";
            SpecialSize[Temp] = new Vector3(100, 100, 100);
            SpecialMaxSize[Temp] = new Vector3(100, 100, 100);
            SpecialModel[Temp] = game.Content.Load<Model>("spawnring");
            SpecialUseModel[Temp] = true;
            SpecialInGameModel[Temp] = false;
            SpecialEffect[Temp] = game.Loader.Ambient;
            AtatchLight[Temp] = false;
            SpecialControlsNumber[Temp] = 2;
            Temp2 += 1;
            ControlName[Temp, Temp2] = "Team";
            ControlMin[Temp, Temp2] = 1;
            ControlMax[Temp, Temp2] = 4;
            ControlType[Temp, Temp2] = "Int";
            ControlDefault[Temp, Temp2] = 1;
            Temp2 += 1;
            ControlName[Temp, Temp2] = "FFA(no team)";
            ControlMin[Temp, Temp2] = 0;
            ControlMax[Temp, Temp2] = 1;
            ControlType[Temp, Temp2] = "Bool";
            ControlDefault[Temp, Temp2] = 0;



            Temp += 1;
            Temp2 = -1;

            SpecialName[Temp] = "Health Spawn";
            SpecialSize[Temp] = new Vector3(100, 100, 100);
            SpecialMaxSize[Temp] = new Vector3(100, 100, 100);
            SpecialModel[Temp] = game.Content.Load<Model>("glow circle");
            SpecialUseModel[Temp] = true;
            SpecialInGameModel[Temp] = true;
            SpecialEffect[Temp] = game.Loader.CircleGlow;
            AtatchLight[Temp] = true;
            LightColor[Temp] = new Vector3(1, 0.35f, 0.25f) * 3;
            LightDistance[Temp] = 250;
            LightZ[Temp] = -25;
            LightIsState[Temp] = true;

            SpecialControlsNumber[Temp] = 1;
            Temp2 += 1;
            ControlName[Temp, Temp2] = "Respawn Rate";
            ControlMin[Temp, Temp2] = 0;
            ControlMax[Temp, Temp2] = 1200;
            ControlType[Temp, Temp2] = "Int";
            ControlDefault[Temp, Temp2] = 600;




            Temp += 1;
            Temp2 = -1;

            SpecialName[Temp] = "Energy Spawn";
            SpecialSize[Temp] = new Vector3(100, 100, 100);
            SpecialMaxSize[Temp] = new Vector3(100, 100, 100);
            SpecialModel[Temp] = game.Content.Load<Model>("glow circle");
            SpecialUseModel[Temp] = false;
            SpecialInGameModel[Temp] = false;
            SpecialEffect[Temp] = game.Loader.CircleGlow;
            AtatchLight[Temp] = false;

            SpecialControlsNumber[Temp] = 1;
            Temp2 += 1;
            ControlName[Temp, Temp2] = "Respawn Rate";
            ControlMin[Temp, Temp2] = 0;
            ControlMax[Temp, Temp2] = 2000;
            ControlType[Temp, Temp2] = "Int";
            ControlDefault[Temp, Temp2] = 1000;



            Temp += 1;
            Temp2 = -1;

            SpecialName[Temp] = "Death zone";
            SpecialSize[Temp] = new Vector3(100, 100, 100);
            SpecialMaxSize[Temp] = new Vector3(10000, 100, 10000);
            SpecialModel[Temp] = game.Content.Load<Model>("selectbox3");
            SpecialUseModel[Temp] = true;
            SpecialInGameModel[Temp] = false;
            SpecialEffect[Temp] = game.Loader.Ambient;
            AtatchLight[Temp] = false;

            SpecialControlsNumber[Temp] = 0;



        }

    }
}
