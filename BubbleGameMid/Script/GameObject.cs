using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Vector2 = System.Numerics.Vector2;

namespace BubbleGameMid.Script {
    internal class GameObject : ICloneable {
        public Texture2D _texture;
        protected float _rotation;
        protected KeyboardState currenkey;
        protected KeyboardState previonsKey;

        public string _Name;
        public Vector2 Position;
        public Vector2 Origin;
        public Color _color = Color.White;

        public Vector2 Direction;
        public float RotationVelocity = 3f;
        public float LinearVelocity = 4f;

        public GameObject Parent;
        public float LifeSpan = 0f;
        public bool isRemoved = false;

        public GameObject(Texture2D texture) {
            _texture = texture;
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
        }
        public virtual void update(GameTime gameTime, List<GameObject> gameobject, GameObject[,] table) {
        }
        public virtual void draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(_texture, Position, null, _color, _rotation, Origin, 1, SpriteEffects.None, 0);
        }
        public virtual void start() { }
        public object Clone() {
            return MemberwiseClone();
        }
    }
}
