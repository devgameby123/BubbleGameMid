﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubbleGameMid.Script {
    internal class Bullet : GameObject {
        private float _timer;
        public bool isMove = true;
        public Bullet(Texture2D texture): base(texture) {
            _texture= texture;
        }

        public override void update(GameTime gameTime, List<GameObject> gameobject) {
            _timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            isMove = (Position.X < 64 || Position.Y < 64 || Position.X >= 470) ?false:true;
            CheckCollisions(gameobject);
            if (isMove) Position += Direction * LinearVelocity;
        }

        public void CheckCollisions(List<GameObject> gameobject) {
            for (int i = 1;i<gameobject.Count-1;i++) {
                for (int j = i+1; j < gameobject.Count; j++) {
                    if ((gameobject[i].Position - gameobject[j].Position).Length()+7 < (gameobject[i].Origin.X + gameobject[j].Origin.X)) {
                        var obj1 = gameobject[i] as Bullet;
                        var obj2 = gameobject[j] as Bullet;
                        obj1.isMove= false;
                        obj2.isMove= false;
                    }
                    for (int k = j+1; k < gameobject.Count; k++) {
                        if ((gameobject[i].Position - gameobject[j].Position).Length() + 7 < (gameobject[j].Origin.X + gameobject[k].Origin.X) && (gameobject[j].Position - gameobject[k].Position).Length() + 7 < (gameobject[j].Origin.X + gameobject[k].Origin.X)) {
                            if (gameobject[i]._Name == gameobject[j]._Name && gameobject[j]._Name == gameobject[k]._Name) {
                                var obj1 = gameobject[i] as Bullet;
                                var obj2 = gameobject[j] as Bullet;
                                var obj3 = gameobject[k] as Bullet;
                                obj1.isRemoved = true;
                                obj2.isRemoved = true;
                                obj3.isRemoved = true;
                            }
                        }
                        if ((gameobject[i].Position - gameobject[j].Position).Length() + 7 < (gameobject[j].Origin.X + gameobject[k].Origin.X) && (gameobject[i].Position - gameobject[k].Position).Length() + 7 < (gameobject[i].Origin.X + gameobject[k].Origin.X)) {
                            if (gameobject[i]._Name == gameobject[j]._Name && gameobject[i]._Name == gameobject[k]._Name) {
                                var obj1 = gameobject[i] as Bullet;
                                var obj2 = gameobject[j] as Bullet;
                                var obj3 = gameobject[k] as Bullet;
                                obj1.isRemoved = true;
                                obj2.isRemoved = true;
                                obj3.isRemoved = true;
                            }
                            
                        }
                    }
                }
            }
            
        }

       

    }
}