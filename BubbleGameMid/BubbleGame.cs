using BubbleGameMid.Script;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Vector2 = System.Numerics.Vector2;

namespace BubbleGameMid {
    public class BubbleGame : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<GameObject> _gameobjects;


        public const int WIDTH = 512;
        public const int HIGHT = 600;


        public BubbleGame() {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = WIDTH;
            _graphics.PreferredBackBufferHeight = HIGHT;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = false;
            base.Initialize(); 
        }

        private List<Bullet> allBulletTexture;

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            allBulletTexture = new List<Bullet> {
                new Bullet(Content.Load<Texture2D>("ball")){
                    _Name = "1",
                    _color = Color.Green
                },
                new Bullet(Content.Load<Texture2D>("ball")){
                    _Name = "2",
                    _color = Color.Red
                },new Bullet(Content.Load<Texture2D>("ball")){
                    _Name = "3",
                    _color = Color.Brown
                }
            };

            var GunTexture = Content.Load<Texture2D>("bubbleGun");
            _gameobjects = new List<GameObject>() {

                new Gun(GunTexture){
                    Position = new Vector2(250,550),
                    LifeSpan = 4f,
                    bullet = allBulletTexture
                }
            };
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            foreach (var gameObject in _gameobjects.ToArray()) {
                gameObject.update(gameTime, _gameobjects);
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

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            foreach (var gameObject in _gameobjects) {
                gameObject.draw(_spriteBatch);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}