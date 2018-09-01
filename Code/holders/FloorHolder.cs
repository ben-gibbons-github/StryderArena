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
    public class FloorHolder
    {
        const int MaxFloorTypes = 1;

        public Vector3[] FloorSize = new Vector3[MaxFloorTypes];
        public Model[] FloorModel = new Model[MaxFloorTypes];
        public float[] SpecularTightness = new float[MaxFloorTypes];
        public float[] SpecularValue = new float[MaxFloorTypes];
        ContentManager Manager;

        

        public void Create(Game1 game)
        {
            Manager = new ContentManager(game.Content.ServiceProvider, "Content");
        }

        public void Load(Game1 game)
        {
            Manager.Unload();

            int temp = -1;

            if (game.SectionToLoad == 0)
            {
                temp += 1;
                FloorSize[temp] = new Vector3(3200, 0, 3200);
                FloorModel[temp] = game.Content.Load<Model>("space_floor");
                SpecularTightness[temp] = 10;
                SpecularValue[temp] = 0.5f;
            }
            if (game.SectionToLoad == 1)
            {
                temp += 1;
                FloorSize[temp] = new Vector3(3200, 0, 3200);
                FloorModel[temp] = game.Content.Load<Model>("stone_floor");
                SpecularTightness[temp] = 25;
                SpecularValue[temp] = 1.5f;
            }
            if (game.SectionToLoad == 2)
            {
                temp += 1;
                FloorSize[temp] = new Vector3(3200, 0, 3200);
                FloorModel[temp] = game.Content.Load<Model>("trainstation_floor");
                SpecularTightness[temp] = 20;
                SpecularValue[temp] = 1.5f;
            }
            if (game.SectionToLoad == 3)
            {
                temp += 1;
                FloorSize[temp] = new Vector3(3200, 0, 3200);
                FloorModel[temp] = game.Content.Load<Model>("ice_floor");
                SpecularTightness[temp] = 20;
                SpecularValue[temp] = 2.5f;
            }
            Asign_Values(game);
        }

        public void Asign_Values(Game1 game)
        {
            int i = -1;
            //for (int i = 0; i < MaxFloorTypes; i++)
            foreach(Model mod in FloorModel)
            {
                i++;
               // foreach(ModelMesh mesh in FloorModel[i].Meshes)
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
