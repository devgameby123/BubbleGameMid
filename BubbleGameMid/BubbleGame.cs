using BubbleGameMid.Script;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Vector2 = System.Numerics.Vector2;
namespace BubbleGameMid {
    public class BubbleGame : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<GameObject> _gameobjects;
        private Texture2D tableTexture;
        private Texture2D GunTexture;
        private Texture2D EndLine;
        private Texture2D Background;

        private SpriteFont font;
        private SpriteFont fontend;

        private GameManager gm;
        private double _timer = 0;
        public const int WIDTH = 1260;
        public const int HIGHT = 800;

        private int EndLinenumber = 560;

        private int timeFixDown = 10;


        enum GameStateBubble {
            Gamestart,
            Win,
            Lose
        }
        GameStateBubble _gameStage = GameStateBubble.Gamestart;

        GameObject[,] tableplay;
        public BubbleGame() {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = WIDTH;
            _graphics.PreferredBackBufferHeight = HIGHT;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here
            tableplay = new GameObject[12, 19];
            gm = new GameManager();
            base.Initialize();
        }

        private List<Bullet> allBulletTexture;
        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Font");
            fontend = Content.Load<SpriteFont>("EndFont");

            allBulletTexture = new List<Bullet> {
                new Bullet(Content.Load<Texture2D>("ball1")){
                    _Name = "1",
                },new Bullet(Content.Load<Texture2D>("ball1")){
                    _Name = "1",
                },
                new Bullet(Content.Load<Texture2D>("ball2")){
                    _Name = "2",
                },
                new Bullet(Content.Load<Texture2D>("ball3")){
                    _Name = "3",
                },
                new Bullet(Content.Load<Texture2D>("ball4")){
                    _Name = "4",
                }
            };

            GunTexture = Content.Load<Texture2D>("Arrow");
            tableTexture = Content.Load<Texture2D>("Table");
            EndLine = Content.Load<Texture2D>("EndLine");
            Background = Content.Load<Texture2D>("Background");

            _gameobjects = new List<GameObject>() {
                         new Gun(GunTexture){
                                LifeSpan = 4f,
                                bullet = allBulletTexture,
                                _bullet = allBulletTexture[0]
                         },
                         new Bullet(Content.Load<Texture2D>("ball1")) {
                                _Name = "1",
                                Position = new Vector2(6*70,70),
                         },
                         new Bullet(Content.Load<Texture2D>("ball2")) {
                                _Name = "2",
                                Position = new Vector2(7*70,70),
                         },
                         new Bullet(Content.Load<Texture2D>("ball1")) {
                                _Name = "1",
                                Position = new Vector2(8*70,70),
                         },
                         new Bullet(Content.Load<Texture2D>("ball2")) {
                                _Name = "2",
                                Position = new Vector2(9*70,70),
                          },
                         new Bullet(Content.Load<Texture2D>("ball4")) {
                                 _Name = "4",
                                 Position = new Vector2(10*70,70),
                          },
                          new Bullet(Content.Load<Texture2D>("ball3")) {
                          _Name = "3",
                          Position = new Vector2(11*70,70),
                          },
                          new Bullet(Content.Load<Texture2D>("ball4")) {
                          _Name = "4",
                          Position = new Vector2(12*70,70),
                          }
                          };


            // TODO: use this.Content to load your game content here
        }

        //Time Cieling Down
        private float TimeDown = 10 * 60;
        protected override void Update(GameTime gameTime) {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            switch (_gameStage) {
                case GameStateBubble.Gamestart:
                    Random ran = new Random();
                    _timer += gameTime.ElapsedGameTime.Ticks;
                    gm.Update(gameTime);
                    foreach (var gameObject in _gameobjects.ToArray()) {
                        gameObject.update(gameTime, _gameobjects, tableplay);
                    }
                    TimeDown--;
                    if (TimeDown <= 0) {
                        TimeDown = 0;
                        foreach (var gameObject in _gameobjects) {
                            if (!(gameObject is Gun)) {
                                gameObject.Position.Y += 70;
                            }
                        }
                        string ranNub = ran.Next(1, 4).ToString();
                        int ranpos = ran.Next(6, 12);
                        _gameobjects.Add(new Bullet(Content.Load<Texture2D>("ball" + ranNub)) {
                            _Name = ranNub,
                            Position = new Vector2(ranpos * 70, 70),
                        });
                        ranNub = ran.Next(1, 4).ToString();
                        int newranNub = ran.Next(6, 12);

                        int checkRanPos = 1;
                        while (checkRanPos > 0) {
                            newranNub = ran.Next(6, 12);
                            if (ranpos != newranNub) {
                                checkRanPos = 0;
                            }
                            else {
                                checkRanPos = 1;
                            }

                        }
                        _gameobjects.Add(new Bullet(Content.Load<Texture2D>("ball" + ranNub)) {
                            _Name = ranNub,
                            Position = new Vector2(newranNub * 70, 70),
                        });
                        foreach (var gameObject in _gameobjects) {
                            if (gameObject is Bullet) {
                                var obj1 = gameObject as Bullet;
                                if (obj1.isMove == false) {
                                    if (obj1.Position.Y >= EndLinenumber) {
                                        _gameStage = GameStateBubble.Lose;
                                    }
                                }
                            }
                        }
                        TimeDown = timeFixDown * 60;
                    }
                    foreach (var gameObject in _gameobjects) {
                        if (gameObject is Bullet) {
                            var obj1 = gameObject as Bullet;
                            if (obj1.isMove == false) {
                                if (obj1.Position.Y >= EndLinenumber) {
                                    _gameStage = GameStateBubble.Lose;
                                }
                            }
                        }
                    }
                    if (_gameobjects.Count <= 1) {
                        _gameStage = GameStateBubble.Win;
                    }
                    postUpdate();
                    break;
                case GameStateBubble.Win:
                    if (Keyboard.GetState().IsKeyDown(Keys.Space)) {
                        _gameobjects.Clear();
                        _gameobjects = new List<GameObject>() {
                         new Gun(GunTexture){
                                LifeSpan = 4f,
                                bullet = allBulletTexture,
                                _bullet = allBulletTexture[0]
                         },
                         new Bullet(Content.Load<Texture2D>("ball1")) {
                                _Name = "1",
                                Position = new Vector2(6*70,70),
                         },
                         new Bullet(Content.Load<Texture2D>("ball2")) {
                                _Name = "2",
                                Position = new Vector2(7*70,70),
                         },
                         new Bullet(Content.Load<Texture2D>("ball1")) {
                                _Name = "1",
                                Position = new Vector2(8*70,70),
                         },
                         new Bullet(Content.Load<Texture2D>("ball2")) {
                                _Name = "2",
                                Position = new Vector2(9*70,70),
                          },
                         new Bullet(Content.Load<Texture2D>("ball4")) {
                                 _Name = "4",
                                 Position = new Vector2(10*70,70),
                          },
                          new Bullet(Content.Load<Texture2D>("ball3")) {
                          _Name = "3",
                          Position = new Vector2(11*70,70),
                          },
                          new Bullet(Content.Load<Texture2D>("ball4")) {
                          _Name = "4",
                          Position = new Vector2(12*70,70),
                          }
                          };
                        GameManager.Score = 0;
                        _timer = 0;
                        _gameStage = GameStateBubble.Gamestart;
                    }
                    break;
                case GameStateBubble.Lose:
                    if (Keyboard.GetState().IsKeyDown(Keys.Space)) {
                        _gameobjects.Clear();
                        _gameobjects = new List<GameObject>() {
                         new Gun(GunTexture){
                                LifeSpan = 4f,
                                bullet = allBulletTexture,
                                _bullet = allBulletTexture[0]
                         },
                         new Bullet(Content.Load<Texture2D>("ball1")) {
                                _Name = "1",
                                Position = new Vector2(6*70,70),
                         },
                         new Bullet(Content.Load<Texture2D>("ball2")) {
                                _Name = "2",
                                Position = new Vector2(7*70,70),
                         },
                         new Bullet(Content.Load<Texture2D>("ball1")) {
                                _Name = "1",
                                Position = new Vector2(8*70,70),
                         },
                         new Bullet(Content.Load<Texture2D>("ball2")) {
                                _Name = "2",
                                Position = new Vector2(9*70,70),
                          },
                         new Bullet(Content.Load<Texture2D>("ball4")) {
                                 _Name = "4",
                                 Position = new Vector2(10*70,70),
                          },
                          new Bullet(Content.Load<Texture2D>("ball3")) {
                          _Name = "3",
                          Position = new Vector2(11*70,70),
                          },
                          new Bullet(Content.Load<Texture2D>("ball4")) {
                          _Name = "4",
                          Position = new Vector2(12*70,70),
                          }
                          };
                        GameManager.Score = 0;
                        _timer = 0;
                        _gameStage = GameStateBubble.Gamestart;
                    }

                    break;
            }

            base.Update(gameTime);
        }

        private void postUpdate() {
            for (int i = 0; i < _gameobjects.Count; i++) {
                if (_gameobjects[i].isRemoved) {
                    _gameobjects.RemoveAt(i);
                    i--;
                }
            }
        }
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Black);

            // TODO: Add your drawing code here
            switch (_gameStage) {
                case GameStateBubble.Gamestart:
                    _spriteBatch.Begin();

                    _spriteBatch.Draw(Background, new Vector2(0, 0), null, Microsoft.Xna.Framework.Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                    gm.Draw(_spriteBatch, tableTexture);
                    _spriteBatch.Draw(EndLine, new Vector2(335, 560), null, Microsoft.Xna.Framework.Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                    gm.Draw(_spriteBatch, font);
                    _spriteBatch.DrawString(font, "Timer: " + String.Format("{0:00}", (_timer / 10000000) % 60), new Vector2(150, 400), Microsoft.Xna.Framework.Color.White);
                    foreach (var gameObject in _gameobjects) {
                        gameObject.draw(_spriteBatch);
                    }
                    _spriteBatch.End();
                    break;
                case GameStateBubble.Win:
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(Background, new Vector2(0, 0), null, Microsoft.Xna.Framework.Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                    gm.Draw(_spriteBatch, tableTexture);
                    _spriteBatch.DrawString(fontend, "WINER", new Vector2(600, 300), Microsoft.Xna.Framework.Color.White);

                    _spriteBatch.DrawString(fontend, "\nTime Total: " + _timer, new Vector2(550, 300), Microsoft.Xna.Framework.Color.Gold);
                    _spriteBatch.DrawString(fontend, "\n\nScore Total: " + GameManager.Score, new Vector2(550, 300), Microsoft.Xna.Framework.Color.Gold);
                    _spriteBatch.DrawString(fontend, "\n\n\nPrees Space bar new game", new Vector2(450, 300), Microsoft.Xna.Framework.Color.White);
                    _spriteBatch.End();
                    break;
                case GameStateBubble.Lose:
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(Background, new Vector2(0, 0), null, Microsoft.Xna.Framework.Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
                    gm.Draw(_spriteBatch, tableTexture);
                    _spriteBatch.DrawString(fontend, " You Lose", new Vector2(560, 350), Microsoft.Xna.Framework.Color.White);
                    _spriteBatch.DrawString(fontend, "\nPrees Space bar new game", new Vector2(450, 350), Microsoft.Xna.Framework.Color.White);
                    _spriteBatch.End();
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
