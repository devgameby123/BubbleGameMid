using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubbleGameMid.Script {
    internal class GameManager {
        public double Timer = 0;

        public void Update(GameTime gameTime) {
            Timer += gameTime.ElapsedGameTime.Ticks;
        }
        public void Draw(SpriteBatch spriteBatch,Texture2D texture) {
            spriteBatch.Draw(texture, new Vector2(0,0), null, Microsoft.Xna.Framework.Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
        }

    }
}
