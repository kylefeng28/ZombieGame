using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ZombieGame {
    class Laser : BaseSprite {
        public Vector2 start;
        public Vector2 position;
        public Vector2 direction;
        public float velocity = 50;
        public bool isActive = false;

        public Laser() {
        }

        public override void LoadContent(ContentManager Content) {
            texture = Content.Load<Texture2D>("pixel");
        }

        public override void Update(Game1 game) {
            if (isActive) {
                position += velocity * direction;
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {

            // TODO
            rect = new Rectangle((int) position.X - 20, (int) position.Y - 20,
                                 (int) 20, 3);

            float angle = (float) Math.Atan2(direction.Y, direction.X);

            if (isActive) {
                spriteBatch.Draw(texture, rect, null, Color.Red, angle, Vector2.Zero, SpriteEffects.None, 0);
            }

        }

        public void Fire() {
            position = start;
            isActive = true;
        }

        public void StopFiring() {
            isActive = false;
        }

        public bool Intersects(CharSprite s) {
            return (isActive && (new Rectangle((int) position.X, (int) position.Y, 1, 1)).Intersects(s.rect));
        }

        public void EnforceBounds(int width, int height) {
            if (position.X < 0 || position.X > width ||
                position.Y < 0 || position.Y > height) {
                StopFiring();
            }
        }
    }
}
