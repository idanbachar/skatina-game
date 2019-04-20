using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skatina
{
    public class Map
    {
        public int CurrentLevelIndex;
        public Level[] Levels;

        public Map()
        {
            Levels = new Level[1];
            CurrentLevelIndex = 0;
            Load();
        }

        public void Load()
        {
            for (int i = 0; i < Levels.Length; i++)
            {
                Levels[i] = new Level();
                Levels[i].LoadLevel();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Entity entity in Levels[CurrentLevelIndex].LevelEntities)
            {
                if (entity != null)
                    entity.Draw(spriteBatch);
            }
        }
    }
}
