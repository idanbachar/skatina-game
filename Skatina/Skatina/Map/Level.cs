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
            LoadLevel();
        }
        public void LoadLevel()
        {
            LevelSchema = new string [,]
            {
                {"P"," "," "},
                {"_","_","_"},
                {" "," "," "}
            };

            LevelEntities = new Entity[LevelSchema.GetLength(0), LevelSchema.GetLength(1)];
        }
    }
}
