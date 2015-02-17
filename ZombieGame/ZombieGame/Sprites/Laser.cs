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
    class Laser {
        private static Texture2D texture;
        public Rectangle rect;
        public Vector2 position = Vector2.Zero;
        public Vector2 direction;
        public float velocity = 50;
        public bool isActive = true;

        public Laser() {
            direction = new Vector2(1, 0);
        }

        public void LoadContent(ContentManager Content) {
            texture = Content.Load<Texture2D>("pixel"); // Same texture as healthbar, for now TEST
        }

        public void Update(GameTime gameTime) {
            if (isActive) {
                position += velocity * direction;
            }
            else {
                position = Vector2.Zero;
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            rect = new Rectangle((int) position.X, (int) position.Y,
                                 20, 2);

            if (isActive) {
                spriteBatch.Draw(texture, rect, Color.Red);
            }

            // Test
            Console.WriteLine(position);

        }

        public void Fire() {
            isActive = true;
        }

        public void StopFiring() {
            isActive = false;
        }

    }
}
