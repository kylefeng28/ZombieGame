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
    class Dalek : CharSprite {
        // Textures
        private static Texture2D dalek_left;
        private static Texture2D dalek_right;
        private static Texture2D dalek_front;
        private static Texture2D dalek_back;

        // Sounds
        private static SoundEffectInstance snd;
        private static SoundEffect exterminate_snd;
        private static SoundEffect beam_snd;

        // Target
        private CharSprite target;

        // Laser
        public Laser lsr = new Laser();
        private int framesSinceShot = -1; // -1 is ready to shoot

        public Dalek() {
            scale = 15;
            velocity_max = 2;
            health = 100;
            lsr.velocity = 50;
        }

        public override void LoadContent(ContentManager Content) {
            dalek_right = Content.Load<Texture2D>("dalek_right");
            dalek_left = Content.Load<Texture2D>("dalek_left");
            dalek_front = Content.Load<Texture2D>("dalek_front");
            dalek_back = Content.Load<Texture2D>("dalek_back");
            texture = dalek_right;

            exterminate_snd = Content.Load<SoundEffect>("exterminate");
            beam_snd = Content.Load<SoundEffect>("beam");

            lsr.LoadContent(Content);

            base.LoadContent(Content);
        }

        public override void Update(Game1 game) {
            // Increment frames since shot
            if (framesSinceShot >= 0) {
                framesSinceShot++;
            }

            // If more than 60 frames has passed, reload
            if (framesSinceShot >= 60) {
                framesSinceShot = -1;
            }

            // Slow down
            if (acceleration.Equals(Vector2.Zero)) {
                acceleration.X = -Math.Sign(velocity.X);
                acceleration.Y = -Math.Sign(velocity.Y);
            }

            // Update laser
            lsr.Update(game);

            // Laser collision detection
            if (lsr.Intersects(target)) {
                lsr.StopFiring();
                target.LoseHealth(10);
            }

            base.Update(game);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            switch (direction) {
            case Direction.Up:
                texture = dalek_back;
                break;
            case Direction.Down:
                texture = dalek_front;
                break;
            case Direction.Left:
                texture = dalek_left;
                break;
            case Direction.Right:
                texture = dalek_right;
                break;
            }

            lsr.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

        public override void Move(Vector2 v) {
            position += v;

            if (Math.Abs(v.X) > Math.Abs(v.Y)) {
                if (v.X > 0) { direction = Direction.Right; }
                else { direction = Direction.Left; }
            }
            else {
                if (v.Y > 0) { direction = Direction.Down; }
                else { direction = Direction.Up; }
            }
        }

        public override void Follow(CharSprite s) {
            target = s;

            Vector2 diff = s.position - position;
            Vector2 dir = diff;
            dir.Normalize();

            if (diff.Length() <= 150) {
                Exterminate();
            }

            if (diff.Length() <= 200) {
                Shoot(s);
            }

            Move(velocity_max * dir);
        }

        public void Exterminate() {
            if (snd == null || snd.State != SoundState.Playing) {
                snd = exterminate_snd.CreateInstance();
                snd.Play();
            }
        }

        public void Shoot(CharSprite s) {
            Vector2 dir =  s.position - position;
            dir.Normalize();

            if (framesSinceShot == -1) {
                beam_snd.Play();
                lsr.start = position;
                lsr.direction = dir;
                
                lsr.Fire();
                framesSinceShot = 0;
            }
        }

    }
}
