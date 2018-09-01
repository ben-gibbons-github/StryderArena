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
    public abstract class BasicMenuObject
    {
        public int MinX = 0;
        public int MinY = 0;
        public int MaxX = 0;
        public int MaxY = 0;

        public float QuadAlpha = 1;

        Rectangle HUDRECT = new Rectangle();

        public string Title = "";

        public int LineCount = 0;

        public float MenuAlpha = 1;

        public float FadeTime = 0.05f;

        public Vector2 Mult = Vector2.One;

        public int PosX = 0;
        public int PosY = 0;

        public bool HasCalculatedWidth = false;

        public Vector2 DrawPos = Vector2.Zero;
        public Vector2 DrawSeperation = new Vector2(0,75);
        public Color DrawColor=Color.DeepSkyBlue;

        public string MyType = "";

        public int Tier=0;

        public List<BasicMenuLine> Lines = new List<BasicMenuLine>();


        public abstract void Update(Game1 game);

        public abstract void Load(Game1 game);

        public abstract void HitButton(Game1 game,bool AButton,bool BButton,bool Left, bool Right);

        public abstract void GoToMenu(Game1 game);

        public void ControlLines(Game1 game)
        {
            foreach (BasicMenuLine Line in Lines)
            {
                Line.Update(game, (int)PosX, (int)PosY, this);

            }
            }

        public void AddLine(string name,bool IsArrows)
        {
            MenuLine line= new MenuLine();

            line.Name = name;
            line.Y = (int)LineCount;
            line.X = 0;
            line.IsArrows = IsArrows;
           // line.Y = 0;

            LineCount += 1;

            Lines.Add(line);
        }

        public void UpdateLine(int Position, string NewName)
        {
            Lines[Position].Name = NewName;
        }

        public ScoreLine AddScoreLine()
        {
            ScoreLine line = new ScoreLine();

            line.Y = (int)LineCount;
            line.X = 0;
            // line.Y = 0;

            LineCount += 1;

            Lines.Add(line);

            return line;
        }

        public void FindWidth(Game1 game)
        {
            HasCalculatedWidth = true;
            Vector2 CurrentDrawPos = DrawPos;
            foreach (BasicMenuLine line in Lines)
            {
                Vector2 StringSize = Vector2.Zero;

                if (line.Name != null)
                    StringSize = game.Loader.font.MeasureString(line.Name) + new Vector2(80, 0);
               // else
                 //   HasCalculatedWidth = true;

                if (StringSize.X*Mult.X > HUDRECT.Width)
                    HUDRECT.Width = (int)(StringSize.X*Mult.X);
                CurrentDrawPos += DrawSeperation;
            }
            // HUDRECT.Width -= (int)DrawPos.X;
            HUDRECT.Height =  (int)(((CurrentDrawPos.Y - DrawPos.Y) + 50)*Mult.Y);

            HUDRECT.X = (int)((DrawPos.X - 40)*Mult.X);
            HUDRECT.Y = (int)((DrawPos.Y - 40)*Mult.Y);
        }

        public virtual void Draw(Game1 game)
        {
            if(Tier>0)
            game.DrawFullscreenQuad(game.Loader.Temp, BlendState.AlphaBlend, null, new Color(0, 0, 0, MenuAlpha * 0.4f*QuadAlpha));
           //// else
            //   
            
            game.spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend);

            //if (!HasCalculatedWidth)
                FindWidth(game);

            


            game.spriteBatch.Draw(game.Loader.MenuWindow, HUDRECT, new Color(1,1,1,MenuAlpha));

            Vector2 CurrentDrawPos = DrawPos;
            int i = 0;
            foreach (BasicMenuLine line in Lines)
            {
                i++;
                line.Update(game, (int)PosX, (int)PosY, this);
               // if (PosY == i)
                 //   line.Alpha = 1;
                line.Draw(game, this, CurrentDrawPos,Mult);
                CurrentDrawPos += DrawSeperation;
            }
            game.spriteBatch.End();
        }
    }
}
