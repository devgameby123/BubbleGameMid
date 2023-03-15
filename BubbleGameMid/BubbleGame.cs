using BubbleGameMid.Script;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Xml;
using Vector2 = System.Numerics.Vector2;
namespace BubbleGameMid {
    public class BubbleGame : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<GameObject> _gameobjects;
        private Texture2D tableTexture;
        private SpriteFont font;
        private GameManager gm;
        private double _timer = 0;

        public const int WIDTH = 1260;
        public const int HIGHT = 800;


        private float TimerRate = 5 * 60;

        int[,] tableplay;
        public BubbleGame() {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = WIDTH;
            _graphics.PreferredBackBufferHeight = HIGHT;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here
            tableplay = new int[12, 19]
            {
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
            };
            

            gm = new GameManager();
            base.Initialize(); 
        }

        private List<Bullet> allBulletTexture;
        private List<Texture2D> textureBall;
        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Font");
            
            allBulletTexture = new List<Bullet> {
                new Bullet(Content.Load<Texture2D>("ball1")){
                    _Name = "1",
                },
                new Bullet(Content.Load<Texture2D>("ball2")){
                    _Name = "2",
 
                },new Bullet(Content.Load<Texture2D>("ball3")){
                    _Name = "3",
                }
            };

            var GunTexture = Content.Load<Texture2D>("bubbleGun");
            tableTexture = Content.Load<Texture2D>("Table");
            _gameobjects = new List<GameObject>() {
     
                new Gun(GunTexture){
                    Position = new Vector2(640,750),
                    LifeSpan = 4f,
                    bullet = allBulletTexture,
                    _bullet = allBulletTexture[0] 
                },
            };
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime) {

            TimerRate--;
            if (TimerRate <= 0) {
                for (int i = 0; i < tableplay.GetLength(0); i++) {
                    for (int j = 0; j < tableplay.GetLength(1); j++) {
                        Debug.Write(tableplay[i, j] + "\t");
                    }
                    Debug.WriteLine("");
                }
                Debug.WriteLine("");
                TimerRate = 6 * 50;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _timer += gameTime.ElapsedGameTime.Ticks;
            foreach (var gameObject in _gameobjects.ToArray()) {
                gameObject.update(gameTime, _gameobjects,tableplay);
            }
            postUpdate();

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
        private int dimention = 70;
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            gm.Draw(_spriteBatch,tableTexture);
            _spriteBatch.DrawString(font, "Timer: " + String.Format("{0:00}",(_timer/10000000)%60), new Vector2(200, 0), Microsoft.Xna.Framework.Color.Black);
            foreach (var gameObject in _gameobjects) {
                gameObject.draw(_spriteBatch);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}