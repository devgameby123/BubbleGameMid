using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Vector2 = System.Numerics.Vector2;
using System.Net.Mime;


namespace BubbleGameMid.Script {
    internal class Gun : GameObject {
        public List<Bullet> bullet;
        private float fireRatefull = 1f * 60f;
        private float currenfireRate = 1f * 60f;
        
        private float _sensitivity = 0.001f;

        private Point _center = new Point(400, 300);
        public Gun(Texture2D texture) : base(texture) {
        }
        public override void update(GameTime gameTime, List<GameObject> gameobject) {
            previonsKey = currenkey;
            currenkey = Keyboard.GetState();
            
            var delta = Mouse.GetState().Position - _center;
            Mouse.SetPosition(_center.X, _center.Y);
            _rotation += delta.X * _sensitivity;
            currenfireRate--;


            /*if (Keyboard.GetState().IsKeyDown(Keys.A)) {
                _rotation -= MathHelper.ToRadians(RotationVelocity);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D)) {
                _rotation += MathHelper.ToRadians(RotationVelocity);
            }*/
            Direction = new Vector2((float)Math.Cos(MathHelper.ToRadians(90) - _rotation), -(float)Math.Sin(MathHelper.ToRadians(90) - _rotation));
            if (currenkey.IsKeyDown(Keys.Space)&& previonsKey.IsKeyUp(Keys.Space) && currenfireRate <=0) {
                Addbullet(gameobject);
                currenfireRate = fireRatefull;
            }

        }

        Random random = new Random();
        private void Addbullet(List<GameObject> gameobject) {
            
            int randomNumber = random.Next(0,bullet.Count);
            var _bullet = bullet[randomNumber].Clone() as Bullet;
            _bullet.Direction = this.Direction;
            _bullet.Position = this.Position;
            _bullet.LinearVelocity = 5f;
            _bullet.LifeSpan = 10f;
            _bullet.isMove = true;
            gameobject.Add(_bullet);
        }
    }
}
