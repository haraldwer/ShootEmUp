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
        Texture2D wallSprite;
        Texture2D woodParticleSprite;
        Texture2D bloodSprite;

        Player player;
        GeneralMethods method;
        List<Bullet> bulletList;
        List<EnemyBullet> enemyBulletList;
        List<StandardEnemy> standardEnemyList;
        List<EnvironmentObject> environmentList;
        List<WoodParticle> woodParticleList;
        List<BloodParticle> bloodParticleList;
        List<MenuButton> menuButtonList;

        public Vector2 viewPos = new Vector2(100, 100);
        Vector2 oldViewPos = new Vector2(100, 100);
        int windowHeight = 700;
        int windowWidth = 700;
        public Vector2 mousePosition;
        Random rnd = new Random();

        string gameState = "menu";

        string[] menuOptions = new string[2];

        int[,,] map = new int[1, 4, 3];
        
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
            // Setting the window-size
            graphics.PreferredBackBufferWidth = windowWidth;        // WindowWidth
            graphics.PreferredBackBufferHeight = windowHeight;      // WindowHeight
            graphics.ApplyChanges();

            menuOptions[0] = "Start game";
            menuOptions[1] = "Exit game";

            menuButtonList = new List<MenuButton>();

            // TODO: Add your initialization logic here
            bulletList = new List<Bullet>();
            enemyBulletList = new List<EnemyBullet>();
            standardEnemyList = new List<StandardEnemy>();
            environmentList = new List<EnvironmentObject>();
            woodParticleList = new List<WoodParticle>();
            bloodParticleList = new List<BloodParticle>();
            method = new GeneralMethods();
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

            playerSprite = Content.Load<Texture2D>("sprites/Player");
            bulletSprite = Content.Load<Texture2D>("sprites/bullet");
            standardEnemySprite = Content.Load<Texture2D>("sprites/Enemy");
            crosshair = Content.Load<Texture2D>("sprites/crosshair");
            wallSprite = Content.Load<Texture2D>("sprites/Wall");
            woodParticleSprite = Content.Load<Texture2D>("sprites/WoodParticle");
            bloodSprite = Content.Load<Texture2D>("sprites/WoodParticle");
            player = new Player(playerSprite);
            standardEnemyList.Add(new StandardEnemy(standardEnemySprite, new Vector2(50, 50), 0f, bulletSprite, 10, 5)); // Just for testing the enemy
            for (int i = 0; i < menuOptions.Length; i++)
            {
                menuButtonList.Add(new MenuButton(menuOptions[i], i, wallSprite, new Vector2(windowWidth / 2, windowHeight / 2), 100));
            }
            CreateMap(0);
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
            MouseState mouse = Mouse.GetState();
            mousePosition.X = mouse.X;
            mousePosition.Y = mouse.Y;

            switch (gameState)
            {
                case "menu":
                    for (int i = 0; i < menuButtonList.Count; i++)
                    {
                        menuButtonList[i].Update(mouse, method);
                        if (menuButtonList[i].myClicked)
                        {
                            switch(menuButtonList[i].myOption)
                            {
                                case 0:
                                    player.myHP = 10;
                                    player.myAlive = true;
                                    player.myPos = new Vector2(0, 0);
                                    gameState = "game";
                                    break;

                                case 1:
                                    Exit();
                                    break;
                            }
                        }
                    }
                    break;

                case "game":
                    //viewPos = viewPos + (player.myPos - new Vector2(windowWidth/2-32, windowHeight/2-32) - viewPos)* 0.05f; // This is only based on the position of the player
                    viewPos = viewPos + (((player.myPos - new Vector2(windowWidth - 20, windowHeight - 20) + mousePosition + viewPos) / 2) - viewPos) * 0.05f; // This is based on both mouse and player

                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        Exit();

                    // TODO: Add your update logic here

                    for (int i = 0; i < bulletList.Count; i++)
                    {
                        bulletList[i].Update(method, environmentList, player, standardEnemyList);
                        if (!bulletList[i].myAlive)
                        {
                            switch (bulletList[i].myHit)
                            {
                                case "wood":
                                    for (int j = 0; j < 5; j++)
                                    {
                                        woodParticleList.Add(new WoodParticle(bulletList[i].myPos + new Vector2(32, 32), woodParticleSprite, bulletList[i].myDir - 1.57f - (float)(rnd.Next(314) / 100f), method, rnd));
                                    }
                                    break;
                            }
                            bulletList.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < enemyBulletList.Count; i++)
                    {
                        enemyBulletList[i].Update(method, player);
                        if (!enemyBulletList[i].myAlive)
                        {
                            enemyBulletList.RemoveAt(i);
                        }
                    }

                    for (int i = 0; i < woodParticleList.Count; i++)
                    {
                        woodParticleList[i].Update();
                        if (!woodParticleList[i].myAlive)
                        {
                            woodParticleList.RemoveAt(i);
                        }
                    }

                    for (int i = 0; i < bloodParticleList.Count; i++)
                    {
                        bloodParticleList[i].Update();
                        if (!bloodParticleList[i].myAlive)
                        {
                            bloodParticleList.RemoveAt(i);
                        }
                    }

                    for (int i = 0; i < standardEnemyList.Count; i++)
                    {
                        standardEnemyList[i].Update(player, environmentList, enemyBulletList);
                        if (!standardEnemyList[i].myIsAlive)
                        {
                            standardEnemyList.RemoveAt(i);
                        }
                    }

                    player.Update(method, environmentList, bulletList, bulletSprite, mouse, viewPos);
                    if (!player.myAlive)
                    {
                        gameState = "menu";
                    }
                    break;

                default:
                    gameState = "menu";
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DimGray); // Background
            switch (gameState)
            {
                case "menu":
                    foreach (MenuButton i in menuButtonList) i.Draw(spriteBatch);
                    break;
                case "game":
                    foreach (WoodParticle i in woodParticleList) i.Draw(spriteBatch, viewPos);
                    foreach (BloodParticle i in bloodParticleList) i.Draw(spriteBatch, viewPos);
                    foreach (Bullet i in bulletList) i.Draw(spriteBatch, viewPos);
                    foreach (EnemyBullet i in enemyBulletList) i.Draw(spriteBatch, viewPos);
                    foreach (StandardEnemy i in standardEnemyList) i.Draw(spriteBatch, viewPos);
                    foreach (EnvironmentObject i in environmentList) i.Draw(spriteBatch, viewPos);
                    // TODO: Add your drawing code here

                    player.Draw(spriteBatch, viewPos); // Draw player

                    // Drawing crosshair

                    break;
            }

            spriteBatch.Begin();
            spriteBatch.Draw(crosshair, mousePosition, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        #region map
        private void CreateMap(int aLevel)
        {
            int tempNumberOfObjects = 4;
            map[0, 0, 0] = 0; // object type
            map[0, 0, 1] = 2; // x pos
            map[0, 0, 2] = 2; // y pos

            map[0, 1, 0] = 0;
            map[0, 1, 1] = 2;
            map[0, 1, 2] = 3;

            map[0, 2, 0] = 0;
            map[0, 2, 1] = 2;
            map[0, 2, 2] = 4;

            map[0, 3, 0] = 0;
            map[0, 3, 1] = 3;
            map[0, 3, 2] = 4;

            for (int i = 0; i < tempNumberOfObjects; i++)
            {
                environmentList.Add(new EnvironmentObject(map[0, i, 0], new Vector2(map[0, i, 1], map[0, i, 2]), wallSprite));
            }
        }
        #endregion
    }
}
