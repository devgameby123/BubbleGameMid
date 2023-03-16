using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BubbleGameMid.Script {
    internal class Bullet : GameObject {
        private float _timer;
        private float timeMove = 4 * 60;
        public bool isMove = true;
        public Bullet(Texture2D texture) : base(texture) {
            _texture = texture;
        }
        public override void update(GameTime gameTime, List<GameObject> gameobject, GameObject[,] table) {
            _timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            timeMove--;
            isMove = (Position.X < 425 || Position.Y < 80 || Position.X >= 855) ? false : true;
            CheckCollisions(gameobject, table);
            //treeBallRemove(table);
            if (isMove) {
                Position += Direction * LinearVelocity;
            }
            else {
                OneTimeRun(table);
            };
        }

        double timerCount = 0;
        public void OneTimeRun(GameObject[,] table) {

            timerCount--;
            if (timerCount <= 0) {
                int x = (int)Math.Round((Position.X / 70));
                int y = (int)Math.Round((Position.Y / 70));
                if (table[y, x] == null) {
                    table[y, x] = this;
                    //this.Position = new Vector2(x * 70, y * 70);
                }
                else {
                    x = (int)Math.Ceiling((Position.X / 70));
                    y = (int)Math.Ceiling((Position.Y / 70));
                    table[y, x] = this;
                    //this.Position = new Vector2(x * 70, y * 70);
                }
                timerCount = 200 * 60;
            }
        }
        public bool CheckCollisions(List<GameObject> gameobject, GameObject[,] table) {
            for (int i = 1; i < gameobject.Count - 1; i++) {
                for (int j = i + 1; j < gameobject.Count; j++) {
                    if ((gameobject[i].Position - gameobject[j].Position).Length() < (gameobject[i].Origin.X + gameobject[j].Origin.X)) {
                        var obj1 = gameobject[i] as Bullet;
                        var obj2 = gameobject[j] as Bullet;
                        obj1.isMove = false;
                        obj2.isMove = false;
                        for (int k = j + 1; k < gameobject.Count; k++) {
                            if ((gameobject[i].Position - gameobject[j].Position).Length() < (gameobject[i].Origin.X + gameobject[j].Origin.X) && (gameobject[j].Position - gameobject[k].Position).Length() < (gameobject[j].Origin.X + gameobject[k].Origin.X)) {
                                if (gameobject[i]._Name == gameobject[j]._Name && gameobject[j]._Name == gameobject[k]._Name) {
                                    obj1 = gameobject[i] as Bullet;
                                    obj2 = gameobject[j] as Bullet;
                                    var obj3 = gameobject[k] as Bullet;
                                    obj1.isMove = false;
                                    obj2.isMove = false;
                                    obj1.isRemoved = true; obj2.isRemoved = true; obj3.isRemoved = true;
                                    GameManager.pustPoint(10);
                                    return true;
                                }
                                else if (gameobject[i]._Name == gameobject[j]._Name && gameobject[i]._Name == gameobject[k]._Name) {
                                    obj1 = gameobject[i] as Bullet;
                                    obj2 = gameobject[j] as Bullet;
                                    var obj3 = gameobject[k] as Bullet;
                                    obj1.isMove = false;
                                    obj2.isMove = false;
                                    obj1.isRemoved = true; obj2.isRemoved = true; obj3.isRemoved = true;
                                    GameManager.pustPoint(10);
                                    return true;
                                }
                                else if (gameobject[k]._Name == gameobject[i]._Name && gameobject[j]._Name == gameobject[k]._Name) {
                                    obj1 = gameobject[i] as Bullet;
                                    obj2 = gameobject[j] as Bullet;
                                    var obj3 = gameobject[k] as Bullet;
                                    obj1.isMove = false;
                                    obj2.isMove = false;
                                    obj1.isRemoved = true; obj2.isRemoved = true; obj3.isRemoved = true;
                                    GameManager.pustPoint(10);
                                    return true;
                                }
                            }
                        }
                    }

                }
            }
            return false;

        }

    }
}
