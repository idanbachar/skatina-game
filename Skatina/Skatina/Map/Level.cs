using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skatina
{
    public class Level
    {
        public string[,] LevelSchema;
        public Entity[,] LevelEntities;

        public Level() { }

    
        public int GetWidth()
        {
            return LevelEntities.GetLength(1) * Floor.Width;
        }

        public int GetHeight()
        {
            return LevelEntities.GetLength(0) * Wall.Height;
        }

        public void LoadLevel()
        {
            LevelSchema = new string [,]
            {
                {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," "},
                {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," "},
                {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," "},
                {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," "},
                {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," "},
                {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," "},
                {"_","_","_","|","_","_","^|^","_"," "," "," "," "," "," "," "},
                {" "," "," "," "," "," "," "," ","|"," "," "," "," "," "," "},
                {" "," "," "," "," "," "," "," ","|"," "," "," "," "," "," "},
                {" "," "," "," "," "," "," "," ","|"," "," "," "," "," "," "},
                {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," "},
                {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," "},
                {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," "},
                {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," "},
                {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," "},
                {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," "},
                {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," "}
            };

            LevelEntities = new Entity[LevelSchema.GetLength(0), LevelSchema.GetLength(1)];

            for (int y = 0; y < LevelSchema.GetLength(0); y++)
            {
                for (int x = 0; x < LevelSchema.GetLength(1); x++)
                {
                    switch (LevelSchema[y, x])
                    {
                        case " ":
                            LevelEntities[y, x] = new Floor(new Vector2(x * Floor.Width , y * Floor.Height * 2));
                            LevelEntities[y, x].LoadContent(Skatina.GameContent);
                            LevelEntities[y, x].Visible = false;
                            break;

                        case "_":
                            LevelEntities[y, x] = new Floor(new Vector2(x > 0 && LevelSchema[y, x - 1] == "|"  ? (x * Wall.Height + Wall.Width * 2 - Wall.Height) : (x * Floor.Width) - Wall.Width, y * Floor.Height * 2));
                            LevelEntities[y, x].LoadContent(Skatina.GameContent);
                            break;
                        case "|":
                            LevelEntities[y, x] = new Wall(new Vector2(x * Wall.Height + Wall.Width * 2 - Wall.Width * 2, y * Wall.Height  - Wall.Height * 4 - Wall.Width * 2));
                            LevelEntities[y, x].LoadContent(Skatina.GameContent);
                            break;
                        case "^|^":
                            LevelEntities[y, x] = new Wall(new Vector2(x * Wall.Height + Wall.Width * 2 - Wall.Width * 2, y * Wall.Height - Wall.Height * 4 - Wall.Width * 2));
                            ((Wall)LevelEntities[y, x]).IsMove = true;
                            LevelEntities[y, x].LoadContent(Skatina.GameContent);
                            break; 
                    }
                }
            }
        }
    }
}
