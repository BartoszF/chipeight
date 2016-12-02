using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using eightmulator;

namespace chipeight
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Main : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        IntPtr drawSurface;
        MainForm mainForm;
        Registers regForm;
        Code code;

        Emulator emul8;

        KeyboardState newS, oldS;

        Rectangle canvas_size;

        KeyState[] keys = new KeyState[0xF];
        Keys[] checkKeys = new Keys[] { Keys.X, Keys.D1, Keys.D2, Keys.D3, Keys.Q, Keys.W, Keys.E, Keys.A, Keys.S, Keys.D, Keys.Z, Keys.C, Keys.D4, Keys.R, Keys.V };


        public Main(MainForm form, Emulator emul8)
        {
            graphics = new GraphicsDeviceManager(this);
            this.IsFixedTimeStep = true;
            Content.RootDirectory = "Content";

            mainForm = form;
            regForm = form.regs;
            code = form.code;
            this.drawSurface = form.getDrawSurface();

            graphics.PreparingDeviceSettings +=
            new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);
            System.Windows.Forms.Control.FromHandle((this.Window.Handle)).VisibleChanged +=
            new EventHandler(Game1_VisibleChanged);

            this.emul8 = emul8;
        }

        /// <summary>
        /// Event capturing the construction of a draw surface and makes sure this gets redirected to
        /// a predesignated drawsurface marked by pointer drawSurface
        /// </summary>
        void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
                e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle =
                drawSurface;
        }

        /// <summary>
        /// Occurs when the original gamewindows' visibility changes and makes sure it stays invisible
        /// </summary>
        private void Game1_VisibleChanged(object sender, EventArgs e)
        {
                if (System.Windows.Forms.Control.FromHandle((this.Window.Handle)).Visible == true)
                    System.Windows.Forms.Control.FromHandle((this.Window.Handle)).Visible = false;
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            emul8.Init(graphics.GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            newS = Keyboard.GetState();

            for(int i=0;i<15;i++)
            {
                keys[i] = newS.IsKeyDown(checkKeys[i]) == true ? KeyState.Down : KeyState.Up;
            }

            if (emul8.waitKey)
            {
                if(keys[emul8.key] == KeyState.Down)
                {
                    emul8.waitKey = false;
                }
                Console.WriteLine("KEY!!");
            }
            else
            {
                if (emul8.running || (emul8.ready && !emul8.running && (newS.IsKeyUp(Keys.Space) && oldS.IsKeyDown(Keys.Space))))
                {
                    emul8.keys = keys;
                    //for (int i = 0; i < 90;i++)
                        emul8.Cycle();
                    regForm.RegUpdate();
                    if (!emul8.running)
                    {
                        code.Update();
                    }
                }

            }

            if (newS.IsKeyUp(Keys.P) && oldS.IsKeyDown(Keys.P))
            {
                emul8.running = !emul8.running;

                if(!emul8.running)
                {
                    code.Update();
                }
            }

            

            // TODO: Add your update logic here

            base.Update(gameTime);

            oldS = newS;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.BackToFront,BlendState.Additive,SamplerState.PointClamp,null,null);

            emul8.Draw(spriteBatch);

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
