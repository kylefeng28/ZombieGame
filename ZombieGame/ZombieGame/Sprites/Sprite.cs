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
    class Sprite {
        // Textures
        protected Texture2D texture; // Sprite texture to draw
        protected Texture2D healthbar;
        protected int scale = 1; // Amount to scale the texture
        protected Color color = Color.White;

        // Rectangles
        public Rectangle rect;
        protected Rectangle hbar_rect;

        // Position, velocity, acceleration
        public Vector2 position = Vector2.Zero;
        public Vector2 velocity = Vector2.Zero;
        public Vector2 acceleration = Vector2.Zero;

        // Max velocity, direction
        protected float velocity_max;
        public Direction direction;

        // Health
        public int health;
        protected int framesSinceHit = -1; // -1 is not hit

        public virtual void LoadContent(ContentManager Content) {
            healthbar = Content.Load<Texture2D>("pixel");
        }

        public virtual void Update(GameTime t) {
            // Increment frames since hit, and change color to red
            if (framesSinceHit >= 0) {
                framesSinceHit++;
                color = Color.Red;
            }

            // If more than 60 frames has passed, change color back to white
            if (framesSinceHit >= 60) {
                framesSinceHit = -1;
                color = Color.White;
            }

            // Integrate!
            position += velocity;
            velocity += acceleration;
        }

        public virtual void Draw(SpriteBatch spriteBatch) {
            int width = texture.Width / scale;
            int height = texture.Height / scale;
            int x = (int) position.X - width / 2;
            int y = (int) position.Y - height / 2;
            
            // Create rectangles
            rect = new Rectangle(x, y,
                                 width, height);

            hbar_rect = new Rectangle(rect.Left, rect.Top - 20,
                                      health, 10);

            // Draw textures and rectangles
            spriteBatch.Draw(texture, rect, color);
            spriteBatch.Draw(healthbar, hbar_rect, Color.Red);

            // Display health amount
            spriteBatch.DrawString(Fonts.Segoe_UI_Mono, health.ToString(),
                                   new Vector2(hbar_rect.X, hbar_rect.Y - 10),
                                   Color.White);

        }

        public void CheckOutOfBounds(int width, int height) {
            if (position.X < 0) {
                position.X = 0;
            }

            if (position.X > width) {
                position.X = width;
            }

            if (position.Y < 0) {
                position.Y = 0;
            }

            if (position.Y > height) {
                position.Y = height;
            }
        }

        public virtual void Move(Vector2 v) {
            if (velocity.Length() <= velocity_max) {
                acceleration += v;
            }
            if (Math.Abs(v.X) > Math.Abs(v.Y)) {
                if (v.X > 0) { direction = Direction.Right; }
                else { direction = Direction.Left; }
            }
            else {
                if (v.Y > 0) { direction = Direction.Down; }
                else { direction = Direction.Up; }
            }
        }

        public void MoveDown(float n = 1) { Move(new Vector2(0, n)); }
        public void MoveUp(float n = 1) { MoveDown(-n); }
        public void MoveRight(float n = 1) { Move(new Vector2(n, 0)); }
        public void MoveLeft(float n = 1) { MoveRight(-n); }

        public virtual void Follow(Sprite s) {
            Vector2 diff = s.position - position;

            Move(diff);
        }

        public void LoseHealth(int damageAmount) {
            health -= damageAmount;
            framesSinceHit = 0;
        }

    }
}
