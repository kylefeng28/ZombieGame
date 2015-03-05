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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Input
        GamePadState pad1, pad1_old;
        KeyboardState kb, kb_old;

        // Game state
        GameState state = GameState.TitleScreen;
        
        // Sprites
        Ambulance amb1 = new Ambulance();
        Dalek dlk1 = new Dalek();

        // Background
        Texture2D bg;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            IsMouseVisible = true;

            amb1.position = new Vector2(100, 100);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Fonts.LoadContent(Content);
            amb1.LoadContent(Content);
            dlk1.LoadContent(Content);

            bg = Content.Load<Texture2D>("ambulance_bg");
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            pad1_old = pad1;
            kb_old = kb;
            
            pad1 = GamePad.GetState(PlayerIndex.One);
            kb = Keyboard.GetState();

            // Allows the game to exit
            if (pad1.Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape)) {
                this.Exit();
            }

            switch (state) {

            case GameState.TitleScreen: // {
                if (pad1.Buttons.Start == ButtonState.Pressed || kb.IsKeyDown(Keys.Enter)) {
                    state = GameState.Playing;
                }
                break; // }

            case GameState.Playing: // {

                // End game when health is zero
                if (amb1.health <= 0) {
                    state = GameState.End;
                }

                // Reset acceleration
                amb1.acceleration = Vector2.Zero;
                dlk1.acceleration = Vector2.Zero;

                // Move using gamepad
                amb1.MoveWithGamePad(pad1);

                // Move using keyboard
                amb1.MoveWithKeyboard(kb);

                // Dalek follows ambulance
                dlk1.Follow(amb1);
                
                // TEST
                if (pad1.IsButtonDown(Buttons.A) || kb.IsKeyDown(Keys.Space)) {
                    dlk1.Shoot(amb1);
                }

                // Update sprites
                amb1.Update(this);
                dlk1.Update(this);

                // Prevent sprites from moving off screen
                amb1.EnforceBounds(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
                dlk1.EnforceBounds(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
                dlk1.lsr.EnforceBounds(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

                break; // }

            case GameState.End: // {
                if (pad1.Buttons.A == ButtonState.Pressed || kb.IsKeyDown(Keys.Enter)) {
                    Exit();
                }
                break; // }
            }
            
            // Store states for the next frame
            pad1_old = pad1;
            kb_old = kb;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (state) {
            case GameState.TitleScreen: // {
                spriteBatch.DrawString(Fonts.Segoe_UI_Mono, "Zombie Game", Vector2.Zero, Color.White);
                break; // }

            case GameState.Playing: // {
                spriteBatch.Draw(bg, GraphicsDevice.Viewport.Bounds, Color.White);
                amb1.Draw(spriteBatch);
                dlk1.Draw(spriteBatch);
                break; // }

            case GameState.End: // {
                spriteBatch.DrawString(Fonts.Segoe_UI_Mono, "Game over", Vector2.Zero, Color.White);
                break; // }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private bool isKeyTapped(Keys key) {
            return kb.IsKeyDown(key) && kb_old.IsKeyUp(key);
        }

        private bool isButtonTapped(Buttons key) {
            return pad1.IsButtonDown(key) && pad1_old.IsButtonUp(key);
        }

    }
}
