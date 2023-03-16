using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BubbleGameMid.Script {
    internal class GameManager {
        public static int Score = 0;
        static int timeLimit = 0;
        public void Update(GameTime gameTime) {
            timeLimit--;
        }
        public void Draw(SpriteBatch spriteBatch, Texture2D texture) {
            spriteBatch.Draw(texture, new Vector2(0, 0), null, Microsoft.Xna.Framework.Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
        }
        public void Draw(SpriteBatch spriteBatch, SpriteFont font) {
            spriteBatch.DrawString(font, "Score: " + Score, new Vector2(1000, 400), Microsoft.Xna.Framework.Color.AliceBlue);
        }

        public static void pustPoint(int p) {
            if (timeLimit <= 0) {
                Score += 10;
                timeLimit = 1 * 60;
            }



        }

    }
}
