using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace ShootEmUp
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D playerSprite;
        Texture2D bulletSprite; // I assume we are going to add more kinds of bullet later
        Texture2D crosshair;
        Texture2D standardEnemySprite;
        Player player;
        List<Bullet> bulletList;
        List<StandardEnemy> standardEnemyList;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            bulletList = new List<Bullet>();
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

            playerSprite = Content.Load<Texture2D>("sprites/player");
            bulletSprite = Content.Load<Texture2D>("sprites/bullet");
            standardEnemySprite = Content.Load<Texture2D>("sprites/enemy");
            player = new Player(playerSprite);
            standardEnemyList.Add(new StandardEnemy(standardEnemySprite, new Vector2(200, 200), 0)); // Just for testing the enemy
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            
            for (int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].Update();
            }

            for (int i = 0; i < standardEnemyList.Count; i++)
            {
                standardEnemyList[i].Update();
            }

            player.Update(bulletList, bulletSprite);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DimGray); // Background

            foreach (Bullet b in bulletList) b.Draw(spriteBatch);
            // TODO: Add your drawing code here

            player.Draw(spriteBatch); // Draw player

            // Drawing crosshair
            MouseState mousePosition = Mouse.GetState();
            spriteBatch.Begin();
            spriteBatch.Draw(playerSprite, new Vector2(mousePosition.X, mousePosition.Y), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        
        public void GameUpdate()
        {

        }
    }
}
