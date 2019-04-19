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
        private int LevelIndex;
        private Level[] Levels;

        public Map()
        {
            Levels = new Level[1];
            Levels[0] = new Level();
            LoadLevel();
        }

        public void LoadLevel()
        {
            for(int y = 0; y < Levels[LevelIndex].LevelSchema.GetLength(0); y++)
            {
                for(int x = 0; x < Levels[LevelIndex].LevelSchema.GetLength(1); x++)
                {
                    Vector2 entityPos = new Vector2(x * 50, y * 50);

                    switch (Levels[LevelIndex].LevelSchema[y, x])
                    {
                        case "_":
                            Levels[LevelIndex].LevelEntities[y, x] = new Floor(entityPos);
                            Levels[LevelIndex].LevelEntities[y, x].LoadContent(Game1.GameContent);
                            break;
                    }
                }
            }

            FixEntitiesSize();
        }

        public void FixEntitiesSize()
        {
            for (int y = 0; y < Levels[LevelIndex].LevelSchema.GetLength(0); y++)
            {
                for (int x = 0; x < Levels[LevelIndex].LevelSchema.GetLength(1); x++)
                {
                    if (Levels[LevelIndex].LevelEntities[y, x] is Floor)
                    {
                        Floor floor = (Floor)Levels[LevelIndex].LevelEntities[y, x];

                        Vector2 floorPos = new Vector2 (x * floor.Texture.Width, y * floor.Texture.Height);
                        Rectangle floorRec = new Rectangle((int)floorPos.X, (int)floorPos.Y, floor.Texture.Width, floor.Texture.Height);

                        Levels[LevelIndex].LevelEntities[y, x].SetRectangle(floorRec);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Entity entity in Levels[LevelIndex].LevelEntities)
            {
                if (entity != null)
                    entity.Draw(spriteBatch);
            }
        }
    }
}
