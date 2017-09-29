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

        Texture2D myPlayerSprite;
        Texture2D myBulletSprite; // I assume we are going to add more kinds of bullet later
        Texture2D myCrosshair;
        Texture2D myStandardEnemySprite;
        Texture2D myWallSprite;
        Texture2D myWoodParticleSprite;
        Texture2D myBloodParticleSprite;
        Texture2D myButtonSprite;
        Texture2D myHealthPackSprite;
        Texture2D myWeaponSprite;
        Texture2D myHealthBarForeground;
        Texture2D myHealthBarBackground;

        SpriteFont myGameFont;

        Player myPlayer;
        GeneralMethods myMethod;
        List<Bullet> myBulletList;
        List<StandardEnemy> myStandardEnemyList;
        List<EnvironmentObject> myEnvironmentList;
        List<WoodParticle> myWoodParticleList;
        List<BloodParticle> myBloodParticleList;
        List<MenuButton> myMenuButtonList;
        List<HealthPack> myHealthPackList;
        List<DroppedWeapon> myDroppedWeaponList;

        public Vector2 myViewPos = new Vector2(100, 100);
        Vector2 myOldViewPos = new Vector2(100, 100);
        int myWindowHeight = 700;
        int myWindowWidth = 700;
        public Vector2 myMousePosition;
        Random myRNG = new Random();


        enum GameState
        {
            Game, Menu
        }
        GameState myGameState = new GameState();

        string[] menuOptions = new string[2];

        int[,,] map = new int[1, 19, 3];
       
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
            graphics.PreferredBackBufferWidth = myWindowWidth;        // WindowWidth
            graphics.PreferredBackBufferHeight = myWindowHeight;      // WindowHeight
            graphics.ApplyChanges();

            menuOptions[0] = "Start game";
            menuOptions[1] = "Exit game";
            myGameState = GameState.Menu;

            myMenuButtonList = new List<MenuButton>();

            // TODO: Add your initialization logic here
            myBulletList = new List<Bullet>();
            myStandardEnemyList = new List<StandardEnemy>();
            myEnvironmentList = new List<EnvironmentObject>();
            myWoodParticleList = new List<WoodParticle>();
            myBloodParticleList = new List<BloodParticle>();
            myMethod = new GeneralMethods();
            myHealthPackList = new List<HealthPack>();
            myDroppedWeaponList = new List<DroppedWeapon>();
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
            myGameFont = Content.Load<SpriteFont>("GameFont");
            myPlayerSprite = Content.Load<Texture2D>("sprites/Player");
            myBulletSprite = Content.Load<Texture2D>("sprites/bullet");
            myStandardEnemySprite = Content.Load<Texture2D>("sprites/Enemy");
            myCrosshair = Content.Load<Texture2D>("sprites/crosshair");
            myWallSprite = Content.Load<Texture2D>("sprites/Wall");
            myWoodParticleSprite = Content.Load<Texture2D>("sprites/WoodParticle");
            myBloodParticleSprite = Content.Load<Texture2D>("sprites/BloodParticle");
            myButtonSprite = Content.Load<Texture2D>("sprites/Button");
            myHealthPackSprite = Content.Load<Texture2D>("sprites/HealthPack");
            myWeaponSprite = Content.Load<Texture2D>("sprites/BasicPistol");
            myHealthBarForeground = Content.Load<Texture2D>("sprites/HPForeground");
            myHealthBarBackground = Content.Load<Texture2D>("sprites/HPBackground");
            myPlayer = new Player(myMethod, myPlayerSprite, new Vector2(50, 50));
            myStandardEnemyList.Add(new StandardEnemy(myMethod,myStandardEnemySprite, new Vector2(500, 500), 0f, myBulletSprite, 10, 5)); // Just for testing the enemy
            myHealthPackList.Add(new HealthPack(new Vector2(200, 200), myHealthPackSprite));
            myDroppedWeaponList.Add(new DroppedWeapon(new Vector2(300, 300), myWeaponSprite, "basic pistol"));
            for (int i = 0; i < menuOptions.Length; i++)
            {
                myMenuButtonList.Add(new MenuButton(myGameFont, menuOptions[i], i, myButtonSprite, new Vector2(myWindowWidth / 2, myWindowHeight / 2), 128,myMethod));
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
            myMousePosition.X = mouse.X;
            myMousePosition.Y = mouse.Y;

            switch (myGameState)
            {
                case GameState.Menu:
                    for (int i = 0; i < myMenuButtonList.Count; i++)
                    {
                        myMenuButtonList[i].Update(mouse);
                        if (myMenuButtonList[i].myClicked)
                        {
                            switch(myMenuButtonList[i].myOption)
                            {
                                case 0:
                                    myPlayer.myHP = 10;
                                    myPlayer.myAlive = true;
                                    myPlayer.myPos = myPlayer.mySpawnPos;
                                    myGameState = GameState.Game;
                                    break;

                                case 1:
                                    Exit();
                                    break;
                            }
                        }
                    }
                    break;

                case GameState.Game:
                    //viewPos = viewPos + (player.myPos - new Vector2(windowWidth/2-32, windowHeight/2-32) - viewPos)* 0.05f; // This is only based on the position of the player
                    myViewPos = myViewPos + (((myPlayer.myPos - new Vector2(myWindowWidth - 20, myWindowHeight - 20) + myMousePosition + myViewPos) / 2) - myViewPos) * 0.05f; // This is based on both mouse and player

                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        Exit();

                    // TODO: Add your update logic here

                    for (int i = 0; i < myBulletList.Count; i++)
                    {
                        myBulletList[i].Update(myMethod, myEnvironmentList, myPlayer, myStandardEnemyList);
                        if (!myBulletList[i].myAlive)
                        {
                            switch (myBulletList[i].myHit)
                            {
                                case "wood":
                                    for (int j = 0; j < 5; j++)
                                    {
                                        myWoodParticleList.Add(new WoodParticle(myBulletList[i].myPos + new Vector2(32, 32), myWoodParticleSprite, myBulletList[i].myDir - 1.57f - (float)(myRNG.Next(314) / 100f), myMethod, myRNG));
                                    }
                                    break;
                                case "meat":
                                    for (int j = 0; j < 5; j++)
                                    {
                                        myBloodParticleList.Add(new BloodParticle(myBulletList[i].myPos + new Vector2(32, 32)+myBulletList[i].mySpeed, myBloodParticleSprite, myBulletList[i].myDir - 1.57f - (float)(myRNG.Next(314) / 100f), myMethod, myRNG));
                                    }
                                    break;
                            }
                            myBulletList.RemoveAt(i);
                        }
                    }

                    for (int i = 0; i < myHealthPackList.Count; i++)
                    {
                        myHealthPackList[i].Update(myPlayer, myMethod);
                        if (!myHealthPackList[i].myAlive)
                        {
                            myHealthPackList.RemoveAt(i);
                        }
                    }

                    for (int i = 0; i < myWoodParticleList.Count; i++)
                    {
                        myWoodParticleList[i].Update();
                        if (!myWoodParticleList[i].myAlive)
                        {
                            myWoodParticleList.RemoveAt(i);
                        }
                    }

                    for (int i = 0; i < myBloodParticleList.Count; i++)
                    {
                        myBloodParticleList[i].Update();
                        if (!myBloodParticleList[i].myAlive)
                        {
                            myBloodParticleList.RemoveAt(i);
                        }
                    }

                    for (int i = 0; i < myDroppedWeaponList.Count; i++)
                    {
                        myDroppedWeaponList[i].Update(myPlayer, myMethod);
                        if (!myDroppedWeaponList[i].myAlive)
                        {
                            myDroppedWeaponList.RemoveAt(i);
                        }
                    }

                    for (int i = 0; i < myStandardEnemyList.Count; i++)
                    {
                        myStandardEnemyList[i].Update(myPlayer, myEnvironmentList, myBulletList);
                        if (!myStandardEnemyList[i].myIsAlive)
                        {
                            myStandardEnemyList.RemoveAt(i);
                        }
                    }

                    myPlayer.Update(myEnvironmentList, myBulletList, myBulletSprite, mouse, myViewPos, myStandardEnemyList);
                    if (!myPlayer.myAlive)
                    {
                        myGameState = GameState.Menu;
                    }
                    break;

                default:
                    myGameState = GameState.Menu;
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
            spriteBatch.Begin();
            switch (myGameState)
            {
                case GameState.Menu:
                    foreach (MenuButton i in myMenuButtonList) i.Draw(spriteBatch);
                    break;
                case GameState.Game:
                    foreach (HealthPack i in myHealthPackList)          i.Draw(spriteBatch, myViewPos);
                    foreach (WoodParticle i in myWoodParticleList)      i.Draw(spriteBatch, myViewPos);
                    foreach (BloodParticle i in myBloodParticleList)    i.Draw(spriteBatch, myViewPos);
                    foreach (DroppedWeapon i in myDroppedWeaponList)    i.Draw(spriteBatch, myViewPos);
                    foreach (Bullet i in myBulletList)                  i.Draw(spriteBatch, myViewPos);
                    foreach (StandardEnemy i in myStandardEnemyList)    i.Draw(spriteBatch, myViewPos, myHealthBarForeground, myHealthBarBackground);
                    foreach (EnvironmentObject i in myEnvironmentList)  i.Draw(spriteBatch, myViewPos);
                    
                    // TODO: Add your drawing code here

                    myPlayer.Draw(spriteBatch, myViewPos, myHealthBarForeground, myHealthBarBackground); // Draw player
                    spriteBatch.DrawString(myGameFont, Convert.ToString(myPlayer.myHP), new Vector2(100, 100), Color.White);

                    // Drawing crosshair

                    break;
            }

            
            spriteBatch.Draw(myCrosshair, myMousePosition, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        #region map
        private void CreateMap(int aLevel)
        {
            int tempNumberOfObjects = 19;
            //map[levelNumber, object index, object type(0)/x(1)/y(2)]


            //Left wall
            map[0, 0, 0] = 0; // object type
            map[0, 0, 1] = 0; // x pos
            map[0, 0, 2] = 0; // y pos

            map[0, 1, 0] = 0;
            map[0, 1, 1] = 0;
            map[0, 1, 2] = 1;

            map[0, 2, 0] = 0;
            map[0, 2, 1] = 0;
            map[0, 2, 2] = 2;

            map[0, 3, 0] = 0;
            map[0, 3, 1] = 0;
            map[0, 3, 2] = 3;

            map[0, 4, 0] = 0;
            map[0, 4, 1] = 0;
            map[0, 4, 2] = 4;

            map[0, 5, 0] = 0;
            map[0, 5, 1] = 0;
            map[0, 5, 2] = 5;

            map[0, 6, 0] = 0;
            map[0, 6, 1] = 0;
            map[0, 6, 2] = 6;

            map[0, 7, 0] = 0;
            map[0, 7, 1] = 0;
            map[0, 7, 2] = 7;

            map[0, 8, 0] = 0;
            map[0, 8, 1] = 0;
            map[0, 8, 2] = 8;

            map[0, 9, 0] = 0;
            map[0, 9, 1] = 0;
            map[0, 9, 2] = 9;

            //Above wall

            map[0, 10, 0] = 0;
            map[0, 10, 1] = 1;
            map[0, 10, 2] = 0;

            map[0, 11, 0] = 0;
            map[0, 11, 1] = 2;
            map[0, 11, 2] = 0;

            map[0, 12, 0] = 0;
            map[0, 12, 1] = 3;
            map[0, 12, 2] = 0;

            map[0, 13, 0] = 0;
            map[0, 13, 1] = 4;
            map[0, 13, 2] = 0;

            map[0, 14, 0] = 0;
            map[0, 14, 1] = 5;
            map[0, 14, 2] = 0;

            map[0, 15, 0] = 0;
            map[0, 15, 1] = 6;
            map[0, 15, 2] = 0;

            map[0, 16, 0] = 0;
            map[0, 16, 1] = 7;
            map[0, 16, 2] = 0;

            map[0, 17, 0] = 0;
            map[0, 17, 1] = 8;
            map[0, 17, 2] = 0;

            map[0, 18, 0] = 0;
            map[0, 18, 1] = 9;
            map[0, 18, 2] = 0;
            

            for (int i = 0; i < tempNumberOfObjects; i++)
            {
                myEnvironmentList.Add(new EnvironmentObject(map[0, i, 0], new Vector2(map[0, i, 1], map[0, i, 2]), myWallSprite));
            }
        }
        #endregion
    }
}
