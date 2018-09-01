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
    public class BlockHolder
    {
        
        const int MaxBlockTypes = 6;
        public int MaxBlockTypesP = MaxBlockTypes;
        public Vector3[] BlockSize = new Vector3[MaxBlockTypes];
        public Model[] BlockModel = new Model[MaxBlockTypes];
        public int[] BlockDestructable = new int[MaxBlockTypes];
        public int[] BlockLife = new int[MaxBlockTypes];
        public int[] BlockPhaseBlock = new int[MaxBlockTypes];
        public int[] BlockRespawnTime = new int[MaxBlockTypes];
        public bool[] BlockSolid = new bool[MaxBlockTypes];
        public float[] SpecularValue = new float[MaxBlockTypes];
        public bool[] BlockOverDraw = new bool[MaxBlockTypes];
        public float[] SpecularTightness = new float[MaxBlockTypes];
        public int[] MaxPerPixelLights = new int[MaxBlockTypes];
        public DepthStencilState[] BlockStencil = new DepthStencilState[MaxBlockTypes];
        public CullMode[] BlockCull = new CullMode[MaxBlockTypes];
        public bool[] DrawShadow = new bool[MaxBlockTypes];
        public bool[] DrawAsWall= new bool[MaxBlockTypes];
        public bool[] BlockStopRail = new bool[MaxBlockTypes];
        public bool[,] EffectHasSetCamera = new bool[MaxBlockTypes,4];
        ContentManager Manager;
        public void Create(Game1 game)
        {
            Manager = new ContentManager(game.Content.ServiceProvider, "Content");
        }

        public void Load(Game1 game)
        {
           BlockOverDraw = new bool[MaxBlockTypes];
            Manager.Unload();
            int temp = -1;


            #region 0
            if (game.SectionToLoad == 0)
            {
                temp += 1;
                BlockSize[temp] = new Vector3(100, 450, 600);
                if(game.LocalPlayerNumb<2)
                BlockModel[temp] = Manager.Load<Model>("TiledWall");
                else
                    BlockModel[temp] = Manager.Load<Model>("TiledWall2");
                BlockDestructable[temp] = 0;
                BlockLife[temp] = 100;
                BlockPhaseBlock[temp] = 1;
                BlockRespawnTime[temp] = 100;
                BlockSolid[temp] = true;
                SpecularTightness[temp] = 25;
                SpecularValue[temp] = 1;
                MaxPerPixelLights[temp] = 2;
                BlockStencil[temp] = DepthStencilState.Default;
                BlockCull[temp] = CullMode.CullClockwiseFace;
                DrawShadow[temp] = true;
                DrawAsWall[temp] = true;
                BlockStopRail[temp] = true;

                temp += 1;
                BlockSize[temp] = new Vector3(200, 200, 200);
                if (game.LocalPlayerNumb < 2)
                BlockModel[temp] = Manager.Load<Model>("generator");
                else
                    BlockModel[temp] = Manager.Load<Model>("generator2");
                BlockDestructable[temp] = 0;
                BlockLife[temp] = 100;
                BlockPhaseBlock[temp] = 0;
                BlockRespawnTime[temp] = 1000;
                BlockSolid[temp] = true;
                SpecularTightness[temp] = 12;
                SpecularValue[temp] = 0.5f;
                MaxPerPixelLights[temp] = 2;
                BlockStencil[temp] = DepthStencilState.Default;
                BlockCull[temp] = CullMode.CullClockwiseFace;
                DrawShadow[temp] = false;
                DrawAsWall[temp] = false;
                BlockStopRail[temp] = false;

                temp += 1;
                BlockSize[temp] = new Vector3(200, 100, 800);
                if (game.LocalPlayerNumb <2)
                    BlockModel[temp] = Manager.Load<Model>("pipes");
                else
                    BlockModel[temp] = Manager.Load<Model>("pipes2");
                BlockDestructable[temp] = 0;
                BlockLife[temp] = 100;
                BlockPhaseBlock[temp] = 0;
                BlockRespawnTime[temp] = 1000;
                BlockSolid[temp] = true;
                SpecularTightness[temp] = 34;
                SpecularValue[temp] = 2.5f;
                MaxPerPixelLights[temp] = 1;
                BlockStencil[temp] = DepthStencilState.Default;
                BlockCull[temp] = CullMode.CullClockwiseFace;
                DrawShadow[temp] = false;
                BlockStopRail[temp] = false;

                DrawAsWall[temp] = false;

                temp += 1;
                BlockSize[temp] = new Vector3(100, 800, 100);
                BlockModel[temp] = Manager.Load<Model>("tree");
                BlockDestructable[temp] = 0;
                BlockLife[temp] = 100;
                BlockPhaseBlock[temp] = 0;
                BlockRespawnTime[temp] = 1000;
                BlockSolid[temp] = true;
                SpecularTightness[temp] = 25;
                SpecularValue[temp] = 0.25f;
                MaxPerPixelLights[temp] = 1;
                BlockStencil[temp] = DepthStencilState.Default;
                BlockCull[temp] = CullMode.None;
                DrawShadow[temp] = false;
                DrawAsWall[temp] = false;
                BlockStopRail[temp] = false;

                temp += 1;
                BlockSize[temp] = new Vector3(100, 50, 100);
                BlockModel[temp] = Manager.Load<Model>("plants");
                BlockDestructable[temp] = 0;
                BlockLife[temp] = 100;
                BlockPhaseBlock[temp] = 0;
                BlockRespawnTime[temp] = 1000;
                BlockSolid[temp] = false;
                SpecularTightness[temp] = 30;
                SpecularValue[temp] = 0.5f;
                MaxPerPixelLights[temp] = 0;
                BlockStencil[temp] = DepthStencilState.DepthRead;
                BlockCull[temp] = CullMode.None;
                DrawShadow[temp] = false;
                DrawAsWall[temp] = false;
                BlockStopRail[temp] = false;

                temp += 1;
                BlockSize[temp] = new Vector3(1000, 50, 1000);
                BlockModel[temp] = Manager.Load<Model>("grass");
                BlockDestructable[temp] = 0;
                BlockLife[temp] = 100;
                BlockPhaseBlock[temp] = 0;
                BlockRespawnTime[temp] = 1000;
                BlockSolid[temp] = false;
                SpecularTightness[temp] = 30;
                SpecularValue[temp] = 0.5f;
                MaxPerPixelLights[temp] = 0;
                BlockStencil[temp] = DepthStencilState.None;
                BlockCull[temp] = CullMode.None;
                DrawAsWall[temp] = false;
                BlockStopRail[temp] = false;
            }
            #endregion


            #region 1
            if (game.SectionToLoad == 1)
            {
                temp += 1;
                BlockSize[temp] = new Vector3(100, 1000, 600);
                BlockModel[temp] = Manager.Load<Model>("stone_wall");
                BlockDestructable[temp] = 0;
                BlockLife[temp] = 100;
                BlockPhaseBlock[temp] = 1;
                BlockRespawnTime[temp] = 100;
                BlockSolid[temp] = true;
                SpecularTightness[temp] = 25;
                SpecularValue[temp] = 0.75f;
                MaxPerPixelLights[temp] = 2;
                BlockStencil[temp] = DepthStencilState.Default;
                BlockCull[temp] = CullMode.CullClockwiseFace;
                DrawShadow[temp] = true;
                DrawAsWall[temp] = true;
                BlockStopRail[temp] = true;

                temp += 1;
                BlockSize[temp] = new Vector3(200, 200, 200);
                BlockModel[temp] = Manager.Load<Model>("stone_rock");
                BlockDestructable[temp] = 0;
                BlockLife[temp] = 100;
                BlockPhaseBlock[temp] = 0;
                BlockRespawnTime[temp] = 1000;
                BlockSolid[temp] = true;
                SpecularTightness[temp] = 25;
                SpecularValue[temp] = 0.75f;
                MaxPerPixelLights[temp] = 2;
                BlockStencil[temp] = DepthStencilState.Default;
                BlockCull[temp] = CullMode.CullClockwiseFace;
                DrawShadow[temp] = false;
                DrawAsWall[temp] = false;
                BlockStopRail[temp] = false;

                temp += 1;
                BlockSize[temp] = new Vector3(100, 100, 600);
                BlockModel[temp] = Manager.Load<Model>("stone_box");
                BlockDestructable[temp] = 0;
                BlockLife[temp] = 100;
                BlockPhaseBlock[temp] = 0;
                BlockRespawnTime[temp] = 1000;
                BlockSolid[temp] = true;
                SpecularTightness[temp] = 25;
                SpecularValue[temp] = 0.75f;
                MaxPerPixelLights[temp] = 1;
                BlockStencil[temp] = DepthStencilState.Default;
                BlockCull[temp] = CullMode.CullClockwiseFace;
                DrawShadow[temp] = false;
                BlockStopRail[temp] = false;

                DrawAsWall[temp] = false;

                temp += 1;
                BlockSize[temp] = new Vector3(400, 700, 400);
                BlockModel[temp] = Manager.Load<Model>("stone_arch");
                BlockDestructable[temp] = 0;
                BlockLife[temp] = 100;
                BlockPhaseBlock[temp] = 0;
                BlockRespawnTime[temp] = 1000;
                BlockSolid[temp] = true;
                SpecularTightness[temp] = 25;
                SpecularValue[temp] = 0.85f;
                MaxPerPixelLights[temp] = 1;
                BlockStencil[temp] = DepthStencilState.Default;
                BlockCull[temp] = CullMode.None;
                DrawShadow[temp] = false;
                DrawAsWall[temp] = false;
                BlockStopRail[temp] = false;

                temp += 1;
                BlockSize[temp] = new Vector3(100, 50, 100);
                BlockModel[temp] = Manager.Load<Model>("plants");
                BlockDestructable[temp] = 0;
                BlockLife[temp] = 100;
                BlockPhaseBlock[temp] = 0;
                BlockRespawnTime[temp] = 1000;
                BlockSolid[temp] = false;
                SpecularTightness[temp] = 30;
                SpecularValue[temp] = 0.5f;
                MaxPerPixelLights[temp] = 0;
                BlockStencil[temp] = DepthStencilState.DepthRead;
                BlockCull[temp] = CullMode.None;
                DrawShadow[temp] = false;
                DrawAsWall[temp] = false;
                BlockStopRail[temp] = false;

                temp += 1;
                BlockSize[temp] = new Vector3(1000, 50, 1000);
                BlockModel[temp] = Manager.Load<Model>("grass");
                BlockDestructable[temp] = 0;
                BlockLife[temp] = 100;
                BlockPhaseBlock[temp] = 0;
                BlockRespawnTime[temp] = 1000;
                BlockSolid[temp] = false;
                SpecularTightness[temp] = 30;
                SpecularValue[temp] = 0.5f;
                MaxPerPixelLights[temp] = 0;
                BlockStencil[temp] = DepthStencilState.None;
                BlockCull[temp] = CullMode.None;
                DrawAsWall[temp] = false;
                BlockStopRail[temp] = false;
            }
            #endregion


            #region 2
            if (game.SectionToLoad == 2)
            {
                temp += 1;
                BlockSize[temp] = new Vector3(600, 600, 600);
                BlockModel[temp] = Manager.Load<Model>("support");
                BlockDestructable[temp] = 0;
                BlockLife[temp] = 100;
                BlockPhaseBlock[temp] = 1;
                BlockRespawnTime[temp] = 100;
                BlockSolid[temp] = true;
                SpecularTightness[temp] = 20;
                SpecularValue[temp] = 1.5f;
                MaxPerPixelLights[temp] = 2;
                BlockStencil[temp] = DepthStencilState.Default;
                BlockCull[temp] = CullMode.CullClockwiseFace;
                DrawShadow[temp] = true;
                DrawAsWall[temp] = true;
                BlockStopRail[temp] = true;

                temp += 1;
                BlockSize[temp] = new Vector3(200, 200, 200);
                BlockModel[temp] = Manager.Load<Model>("cardboard_box");
                BlockDestructable[temp] = 0;
                BlockLife[temp] = 100;
                BlockPhaseBlock[temp] = 0;
                BlockRespawnTime[temp] = 1000;
                BlockSolid[temp] = true;
                SpecularTightness[temp] = 5;
                SpecularValue[temp] = 0.15f;
                MaxPerPixelLights[temp] = 2;
                BlockStencil[temp] = DepthStencilState.Default;
                BlockCull[temp] = CullMode.CullClockwiseFace;
                DrawShadow[temp] = false;
                DrawAsWall[temp] = false;
                BlockStopRail[temp] = false;

                temp += 1;
                BlockSize[temp] = new Vector3(400, 100, 200);
                BlockModel[temp] = Manager.Load<Model>("bench");
                BlockDestructable[temp] = 0;
                BlockLife[temp] = 100;
                BlockPhaseBlock[temp] = 0;
                BlockRespawnTime[temp] = 1000;
                BlockSolid[temp] = true;
                SpecularTightness[temp] = 15;
                SpecularValue[temp] = 0.5f;
                MaxPerPixelLights[temp] = 1;
                BlockStencil[temp] = DepthStencilState.Default;
                BlockCull[temp] = CullMode.CullClockwiseFace;
                DrawShadow[temp] = false;
                BlockStopRail[temp] = false;

                DrawAsWall[temp] = false;

                temp += 1;
                BlockSize[temp] = new Vector3(100, 100, 100);
                BlockModel[temp] = Manager.Load<Model>("trash_can");
                BlockDestructable[temp] = 0;
                BlockLife[temp] = 100;
                BlockPhaseBlock[temp] = 0;
                BlockRespawnTime[temp] = 1000;
                BlockSolid[temp] = true;
                SpecularTightness[temp] = 25;
                SpecularValue[temp] = 0.85f;
                MaxPerPixelLights[temp] = 1;
                BlockStencil[temp] = DepthStencilState.Default;
                BlockCull[temp] = CullMode.None;
                DrawShadow[temp] = false;
                DrawAsWall[temp] = false;
                BlockStopRail[temp] = false;

                temp += 1;
                BlockSize[temp] = new Vector3(100, 50, 100);
                BlockModel[temp] = Manager.Load<Model>("plants");
                BlockDestructable[temp] = 0;
                BlockLife[temp] = 100;
                BlockPhaseBlock[temp] = 0;
                BlockRespawnTime[temp] = 1000;
                BlockSolid[temp] = false;
                SpecularTightness[temp] = 30;
                SpecularValue[temp] = 0.5f;
                MaxPerPixelLights[temp] = 0;
                BlockStencil[temp] = DepthStencilState.DepthRead;
                BlockCull[temp] = CullMode.None;
                DrawShadow[temp] = false;
                DrawAsWall[temp] = false;
                BlockStopRail[temp] = false;

                temp += 1;
                BlockSize[temp] = new Vector3(1000, 50, 1000);
                BlockModel[temp] = Manager.Load<Model>("grass");
                BlockDestructable[temp] = 0;
                BlockLife[temp] = 100;
                BlockPhaseBlock[temp] = 0;
                BlockRespawnTime[temp] = 1000;
                BlockSolid[temp] = false;
                SpecularTightness[temp] = 30;
                SpecularValue[temp] = 0.5f;
                MaxPerPixelLights[temp] = 0;
                BlockStencil[temp] = DepthStencilState.None;
                BlockCull[temp] = CullMode.None;
                DrawAsWall[temp] = false;
                BlockStopRail[temp] = false;
            }
            #endregion


            #region 3
            if (game.SectionToLoad == 3)
            {
                temp += 1;
                BlockSize[temp] = new Vector3(200, 300, 200);
                BlockModel[temp] = Manager.Load<Model>("snowman");
                BlockDestructable[temp] = 0;
                BlockLife[temp] = 100;
                BlockPhaseBlock[temp] = 0;
                BlockRespawnTime[temp] = 100;
                BlockSolid[temp] = true;
                SpecularTightness[temp] = 20;
                SpecularValue[temp] = 1.5f;
                MaxPerPixelLights[temp] = 2;
                BlockStencil[temp] = DepthStencilState.Default;
                BlockCull[temp] = CullMode.CullClockwiseFace;
                DrawShadow[temp] = true;
                DrawAsWall[temp] = true;
                BlockStopRail[temp] = true;

                temp += 1;
                BlockSize[temp] = new Vector3(400, 200, 400);
                BlockModel[temp] = Manager.Load<Model>("ice_mound");
                BlockDestructable[temp] = 0;
                BlockLife[temp] = 100;
                BlockPhaseBlock[temp] = 0;
                BlockRespawnTime[temp] = 1000;
                BlockSolid[temp] = true;
                SpecularTightness[temp] = 5;
                SpecularValue[temp] = 0.15f;
                MaxPerPixelLights[temp] = 2;
                BlockStencil[temp] = DepthStencilState.Default;
                BlockCull[temp] = CullMode.CullClockwiseFace;
                DrawShadow[temp] = false;
                DrawAsWall[temp] = false;
                BlockStopRail[temp] = false;

                temp += 1;
                BlockSize[temp] = new Vector3(400, 600, 400);
                BlockModel[temp] = Manager.Load<Model>("ice_mound2");
                BlockDestructable[temp] = 0;
                BlockLife[temp] = 100;
                BlockPhaseBlock[temp] = 1;
                BlockRespawnTime[temp] = 1000;
                BlockSolid[temp] = true;
                SpecularTightness[temp] = 15;
                SpecularValue[temp] = 0.5f;
                MaxPerPixelLights[temp] = 1;
                BlockStencil[temp] = DepthStencilState.Default;
                BlockCull[temp] = CullMode.CullClockwiseFace;
                DrawShadow[temp] = false;
                BlockStopRail[temp] = false;

                DrawAsWall[temp] = false;

                temp += 1;
                BlockSize[temp] = new Vector3(100, 700, 100);
                BlockModel[temp] = Manager.Load<Model>("snow_tree");
                BlockDestructable[temp] = 0;
                BlockLife[temp] = 100;
                BlockPhaseBlock[temp] = 0;
                BlockRespawnTime[temp] = 1000;
                BlockSolid[temp] = true;
                SpecularTightness[temp] = 12;
                BlockOverDraw[temp] = true;
                SpecularValue[temp] = 0.5f;
                MaxPerPixelLights[temp] = 1;
                BlockStencil[temp] = DepthStencilState.Default;
                BlockCull[temp] = CullMode.CullClockwiseFace;
                DrawShadow[temp] = false;
                DrawAsWall[temp] = false;
                BlockStopRail[temp] = false;

                temp += 1;
                BlockSize[temp] = new Vector3(400, 600, 400);
                BlockModel[temp] = Manager.Load<Model>("ice_mound3");
                BlockDestructable[temp] = 0;
                BlockLife[temp] = 100;
                BlockPhaseBlock[temp] = 1;
                BlockRespawnTime[temp] = 1000;
                BlockSolid[temp] = true; ;
                SpecularTightness[temp] = 12;
                SpecularValue[temp] = 0.5f;
                MaxPerPixelLights[temp] = 0;
                BlockStencil[temp] = DepthStencilState.Default;
                BlockCull[temp] = CullMode.CullClockwiseFace;
                DrawShadow[temp] = false;
                DrawAsWall[temp] = false;
                BlockStopRail[temp] = false;

                temp += 1;
                BlockSize[temp] = new Vector3(200, 200, 200);
                BlockModel[temp] = Manager.Load<Model>("generator");
                BlockDestructable[temp] = 0;
                BlockLife[temp] = 100;
                BlockPhaseBlock[temp] = 0;
                BlockRespawnTime[temp] = 1000;
                BlockSolid[temp] = true;
                SpecularTightness[temp] = 12;
                SpecularValue[temp] = 0.5f;
                MaxPerPixelLights[temp] = 2;
                BlockStencil[temp] = DepthStencilState.Default;
                BlockCull[temp] = CullMode.CullClockwiseFace;
                DrawShadow[temp] = false;
                DrawAsWall[temp] = false;
                BlockStopRail[temp] = false;
            }
            #endregion


            Asign_Values(game);

        }

        public void Asign_Values(Game1 game)
        {
            int i = -1;
            //for (int i = 0; i < MaxBlockTypes; i++)
            foreach(Model mod in BlockModel)
            {
                i++;
                //foreach (ModelMesh mesh in BlockModel[i].Meshes)
                foreach(ModelMesh mesh in mod.Meshes)
                    foreach (Effect effect in mesh.Effects)
                    {
                        effect.Parameters["Shininess"].SetValue(SpecularValue[i]);
                        effect.Parameters["SpecularPower"].SetValue(SpecularTightness[i]);
                    }
            }
        }
    }
}
