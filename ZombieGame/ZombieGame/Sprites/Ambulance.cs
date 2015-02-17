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
    class Ambulance : Sprite {
        // Textures
        private static Texture2D ambulance_side_left;
        private static Texture2D ambulance_side_right;
        private static Texture2D ambulance_top_up;
        private static Texture2D ambulance_top_down;

        // Properties
        private int gear_ = 1;
        public int gear {
            get { return gear_; }

            set {
                if (5 <= gear_ && gear_ <= 5) {
                    velocity_max = 10 * gear_;
                }
            }
        }

        public Ambulance() {
            scale = 3;
            velocity_max = 10;
            health = 100;
            gear = 1;
        }

        public override void LoadContent(ContentManager Content) {
            ambulance_side_right = Content.Load<Texture2D>("ambulance_side_right");
            ambulance_side_left = Content.Load<Texture2D>("ambulance_side_left");
            ambulance_top_up = Content.Load<Texture2D>("ambulance_top_up");
            ambulance_top_down = Content.Load<Texture2D>("ambulance_top_down");
            texture = ambulance_side_right;

            healthbar = Content.Load<Texture2D>("pixel");

            base.LoadContent(Content);
        }

        public override void Update(GameTime gameTime) {
            // Slow down
            if (acceleration.Equals(Vector2.Zero)) {
                acceleration.X = -Math.Sign(velocity.X);
                acceleration.Y = -Math.Sign(velocity.Y);
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            switch (direction) {
            case Direction.Up:
                texture = ambulance_top_up;
                break;
            case Direction.Down:
                texture = ambulance_top_down;
                break;
            case Direction.Left:
                texture = ambulance_side_left;
                break;
            case Direction.Right:
                texture = ambulance_side_right;
                break;
            }
            
            base.Draw(spriteBatch);
        }


    }
}
