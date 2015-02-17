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
    class Fonts {
        public static SpriteFont Segoe_UI_Mono;

        public static void LoadContent(ContentManager Content) {
            Segoe_UI_Mono = Content.Load<SpriteFont>("Segoe_UI_Mono");
        }
    }
}
