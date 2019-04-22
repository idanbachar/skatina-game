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
        public List<Entity> LevelEntities;
        public int Tries;

        public Level() {
            Tries = 0;
        }
    
        public int GetWidth()
        {
            return LevelEntities.Count * Floor.Width;
        }

        public int GetHeight()
        {
            return LevelEntities.Count * Wall.Height;
        }

        public void AddTry()
        {
            Tries++;
        }

        public void LoadLevel()
        {
            LevelSchema = new string [,]
            {
                {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," ", " ", " "},
                {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," ", " ", " "},
                {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," ", " ", " "},
                {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," ", " ", " "},
                {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," ", " ", " "},
                {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," ", " ", " "},
                {"_","_","_","|d","_","_","^|v","_"," "," "," "," "," "," "," ", " ", " "},
                {" "," "," "," "," "," "," "," ","|"," "," "," "," "," "," ", " ", " "},
                {" "," "," "," "," "," "," "," ","|"," ","_"," "," "," "," ", " ", " "},
                {" "," "," "," "," "," "," "," ","|"," ","_","^|vd"," "," "," ", " ", " "},
                {" "," "," "," "," "," "," "," "," ","_","_"," "," ","<_>"," ", " ", "_f"},
                {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," ", " ", " "},
                {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," ", " ", " "},
                {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," ", " ", " "},
                {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," ", " ", " "},
                {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," ", " ", " "},
                {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," ", " ", " "}
            };

            LevelEntities = new List<Entity>();

            for (int y = 0; y < LevelSchema.GetLength(0); y++)
            {
                for (int x = 0; x < LevelSchema.GetLength(1); x++)
                {
                    int xPos = 0;
                    int yPos = 0;
                    Entity entity;

                    switch (LevelSchema[y, x])
                    {
                        case "_":
                            xPos = x * Floor.Width;
                            yPos = y * Floor.Width;
                            entity = new Floor(new Vector2(xPos, yPos), FloorType.Regular);
                            entity.LoadContent(Skatina.GameContent);
                            LevelEntities.Add(entity);
                            break;
                        case "<_>":
                            xPos = x * Floor.Width;
                            yPos = y * Floor.Width;
                            entity = new Floor(new Vector2(xPos, yPos), FloorType.Moving);
                            entity.LoadContent(Skatina.GameContent);
                            LevelEntities.Add(entity);
                            break;
                        case "_f":
                            xPos = x * Floor.Width;
                            yPos = y * Floor.Width;
                            entity = new Floor(new Vector2(xPos, yPos), FloorType.Finish);
                            entity.LoadContent(Skatina.GameContent);
                            LevelEntities.Add(entity);
                            break;
                        case "|":
                            xPos = LevelSchema[y, x - 1] == "_" ? x * Wall.Height + Wall.Width * 2  : x * Wall.Height;
                            yPos = y * Wall.Height - Wall.Width * 2;
                            entity = new Wall(new Vector2(xPos, yPos), WallType.Regular, Direction.Left);
                            entity.LoadContent(Skatina.GameContent);
                            LevelEntities.Add(entity);
                            break;
                        case "|d":
                            xPos = LevelSchema[y, x - 1] == "_" ? x * Wall.Height + Wall.Width * 2 : x * Wall.Height;
                            yPos = y * Wall.Height - Wall.Width * 2;
                            entity = new Wall(new Vector2(xPos, yPos), WallType.Deadly, Direction.Left);
                            entity.LoadContent(Skatina.GameContent);
                            LevelEntities.Add(entity);
                            break;
                        case "^|v":
                            xPos = LevelSchema[y, x - 1] == "_" ? x * Wall.Height + Wall.Width * 2 :  x * Wall.Height;
                            yPos = y * Wall.Height - Wall.Width * 2;
                            entity = new Wall(new Vector2(xPos, yPos), WallType.Moving, Direction.Left);
                            entity.LoadContent(Skatina.GameContent);
                            LevelEntities.Add(entity);
                            break;
                        case "^|vd":
                            xPos = LevelSchema[y, x - 1] == "_" ? x * Wall.Height + Wall.Width * 2 : x * Wall.Height;
                            yPos = y * Wall.Height - Wall.Width * 2;
                            entity = new Wall(new Vector2(xPos, yPos), WallType.DeadlyMoving, Direction.Left);
                            entity.LoadContent(Skatina.GameContent);
                            LevelEntities.Add(entity);
                            break;
                    }
                }
            }
        }
    }
}
