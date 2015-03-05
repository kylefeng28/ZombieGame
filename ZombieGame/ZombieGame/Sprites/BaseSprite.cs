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
    class BaseSprite {
        // Texture
        protected Texture2D texture; // Sprite texture to draw
        public Rectangle rect;

        // Methods
        public virtual void LoadContent(ContentManager Content) {}
        public virtual void Update(Game1 game) {}
        public virtual void Draw(SpriteBatch spriteBatch) {}

    }
}
