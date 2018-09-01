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
    public class HowToHolder
    {
        

        public bool HasLoaded = false;
        public ContentManager Manager;

       // public const int NumbSteps = 8;
        public int numbSteps;

        public string[] StepNames =
        {
            "basic",
            "xbox",
            "move",
            "primary",
            "secondary",
            "dash",
            "dashwall",
            "damage",
            "damagedash",
            "dead",
            "arrow",
            "ability",
            "upgrade",
            "thats_all",
            "arrows",
        };

        public Texture2D[] HowToTexture;

        public void Create(Game1 game)
        {
            HowToTexture = new Texture2D[StepNames.Count()];
            numbSteps = StepNames.Count();
            Manager = new ContentManager(game.Content.ServiceProvider, "Content");
        }

        public void Load(Game1 game)
        {
            int temp = -1;

           foreach(string text in StepNames)
           {
               temp++;
               HowToTexture[temp] = game.Content.Load<Texture2D>("how_to_play/" + text);
           }
           HasLoaded = true;
        }

        public void UnLoad(Game1 game)
        {
            Manager.Unload();
            HasLoaded = false;
        }
    }
}
