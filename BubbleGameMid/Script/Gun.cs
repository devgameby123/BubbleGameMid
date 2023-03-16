using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Vector2 = System.Numerics.Vector2;


namespace BubbleGameMid.Script {
    internal class Gun : GameObject {
        public List<Bullet> bullet;
        private float fireRatefull = 1f * 60f;
        private float currenfireRate = 1f * 60f;

        private float _sensitivity = 0.001f;

        private Point _center = new Point(400, 300);

        Random random = new Random();
        public Bullet _bullet;
        public Gun(Texture2D texture) : base(texture) {
            Position = new Vector2(640, 630);
        }
        public override void start() {
            _bullet = bullet[0].Clone() as Bullet;
        }
        public override void update(GameTime gameTime, List<GameObject> gameobject, GameObject[,] table) {
            previonsKey = currenkey;
            currenkey = Keyboard.GetState();

            var delta = Mouse.GetState().Position - _center;
            Mouse.SetPosition(_center.X, _center.Y);
            _rotation += delta.X * _sensitivity;
            currenfireRate--;

            Direction = new Vector2((float)Math.Cos(MathHelper.ToRadians(90) - _rotation), -(float)Math.Sin(MathHelper.ToRadians(90) - _rotation));

            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed && currenfireRate <= 0) {
                Addbullet(gameobject);
                currenfireRate = fireRatefull;
            }

        }
        private void Addbullet(List<GameObject> gameobject) {
            _bullet.Direction = this.Direction;
            _bullet.Position = this.Position;
            _bullet.LinearVelocity = 5f;
            _bullet.LifeSpan = 10f;
            _bullet.isMove = true;
            gameobject.Add(_bullet);

            if (gameobject.Count < 4) {
                var obj = gameobject[1] as Bullet;
                _bullet = obj;
            }
            else {
                int randomNumber = random.Next(1, bullet.Count);
                _bullet = bullet[randomNumber].Clone() as Bullet;
            }
        }
        public override void draw(SpriteBatch spriteBatch) {
            base.draw(spriteBatch);
            spriteBatch.Draw(_bullet._texture, new Vector2(640, 730), null, _bullet._color, 0, _bullet.Origin, 1, SpriteEffects.None, 0);
        }
    }
}
