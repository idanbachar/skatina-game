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

        public Level()
        {

        }
        public void LoadLevel()
        {
            LevelSchema = new string [,]
            {
                {" "," "," "},
                {" "," "," "},
                {" "," "," "},
                {" "," "," "},
                {" "," "," "},
                {" "," "," "},
                {" "," "," "},
                {" "," "," "},
                {" "," "," "},
                {" "," "," "},
                {" "," "," "},
                {" "," "," "},
                {" "," "," "},
                {" "," "," "},
                {" "," "," "},
                {" ","_"," "}
            };

            LevelEntities = new Entity[LevelSchema.GetLength(0), LevelSchema.GetLength(1)];

            for (int y = 0; y < LevelSchema.GetLength(0); y++)
            {
                for (int x = 0; x < LevelSchema.GetLength(1); x++)
                {
                    switch (LevelSchema[y, x])
                    {
                        case " ":
                            LevelEntities[y, x] = new Floor(new Vector2(x * Floor.Width / 2, y * Floor.Height));
                            LevelEntities[y, x].LoadContent(Game1.GameContent);
                            LevelEntities[y, x].Visible = false;
                            break;

                        case "_":
                            LevelEntities[y, x] = new Floor(new Vector2(x * Floor.Width / 2, y * Floor.Height));
                            LevelEntities[y, x].LoadContent(Game1.GameContent);
                            break;
                    }
                }
            }
        }
    }
}
