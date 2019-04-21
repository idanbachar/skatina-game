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
                {"_","_","_","|d","_","_","^|v","_"," "," "," "," "," "," "," "},
                {" "," "," "," "," "," "," "," ","|"," "," "," "," "," "," "},
                {" "," "," "," "," "," "," "," ","|"," ","_"," "," "," "," "},
                {" "," "," "," "," "," "," "," ","|"," ","_","^|vd"," "," "," "},
                {" "," "," "," "," "," "," "," "," ","_","_"," "," "," "," "},
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
                    int xPos = 0;
                    int yPos = 0;

                    switch (LevelSchema[y, x])
                    {
                        case " ":
                            LevelEntities[y, x] = new Floor(new Vector2(x * Floor.Width , y * Floor.Height * 2));
                            LevelEntities[y, x].LoadContent(Skatina.GameContent);
                            LevelEntities[y, x].Visible = false;
                            break;
                        case "_":
                            xPos = x * Floor.Width;
                            yPos = y * Floor.Width;

                            LevelEntities[y, x] = new Floor(new Vector2(xPos, yPos));
                            LevelEntities[y, x].LoadContent(Skatina.GameContent);
                            break;
                        case "|":
                            xPos = LevelSchema[y, x - 1] == "_" ? x * Wall.Height + Wall.Width * 2  : x * Wall.Height;
                            yPos = y * Wall.Height - Wall.Width * 2;
                            LevelEntities[y, x] = new Wall(new Vector2(xPos, yPos), WallType.Regular, Direction.Left);
                            LevelEntities[y, x].LoadContent(Skatina.GameContent);
                            break;
                        case "|d":
                            xPos = LevelSchema[y, x - 1] == "_" ? x * Wall.Height + Wall.Width * 2 : x * Wall.Height;
                            yPos = y * Wall.Height - Wall.Width * 2;
                            LevelEntities[y, x] = new Wall(new Vector2(xPos, yPos), WallType.Deadly, Direction.Left);
                            LevelEntities[y, x].LoadContent(Skatina.GameContent);
                            break;
                        case "^|v":
                            xPos = LevelSchema[y, x - 1] == "_" ? x * Wall.Height + Wall.Width * 2 :  x * Wall.Height;
                            yPos = y * Wall.Height - Wall.Width * 2;
                            LevelEntities[y, x] = new Wall(new Vector2(xPos, yPos), WallType.Moving, Direction.Left);
                            LevelEntities[y, x].LoadContent(Skatina.GameContent);
                            break;
                        case "^|vd":
                            xPos = LevelSchema[y, x - 1] == "_" ? x * Wall.Height + Wall.Width * 2 : x * Wall.Height;
                            yPos = y * Wall.Height - Wall.Width * 2;
                            LevelEntities[y, x] = new Wall(new Vector2(xPos, yPos), WallType.DeadlyMoving, Direction.Left);
                            LevelEntities[y, x].LoadContent(Skatina.GameContent);
                            break;
                    }
                }
            }
        }
    }
}
