using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace Space_Station_13
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        SpriteFont Font;

        public static int currentLenght, oldLenght = 0;

        Atom[][] world;

        Atom mousePlaceAtom;

        public static DefaultAtoms defaultAtoms;

        string fps = "";

        bool menu, playing, debugView, mouseOutOfBounds, editingMap, loading;

        float rotation = 0;

        Vector2 worldMouse, absPosition, camPosition, mouseTextPosition;

        public static Texture2D whiteTextScreen, plating,  greyTile, whiteTile, ss13Logo, menuClown, playButton, exitButton, turfmap, entitymap, crate_0, crateopen_0, wallmap;

        public static Texture2D plasteelWallSingle, plasteelWallBottom_Left, plasteelWallBottom_Right, plasteelWallLeft_Right, plasteelWallTop_Bottom, plasteelWallTop_Left, plasteelWallTop_Right, plasteelWallSingle_Left, plasteelWallTTop_Right_Left, plasteelWallSingle_Bottom, plasteelWallSingle_Right, plasteelWallSingle_Top, plasteelWallTTop_Bottom_Left, plasteelWallTTop_Bottom_Right, plasteelWallTBottom_Right_Left, plasteelWallAll, plasteelWallFull;

        public static Texture2D human_0, fleet_shorts_0, expedition_s_0;

        public static Texture2D greygrass_0;

        public Player player;

        Texture2D[] RectangleHoloView;

        Button2D[] menuButtons;

        Text textbox;

        Output console;

        private Song menuMusic;

        public static KeyboardState newstate, oldstate;

        public static MouseState newstateM, oldstateM;

        public static Viewport textView, gameView, defaultView;

        public static GraphicsDevice graphicsDevice;

        public static Camera2D camera2D, cameraTwo;

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
            IsMouseVisible = true;
            menu = true;
            loading = true;
            graphicsDevice = GraphicsDevice;
            graphics.PreferredBackBufferWidth = graphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = graphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            defaultView = GraphicsDevice.Viewport;
            Window.AllowUserResizing = true;
            debugView = false;
            editingMap = true;

            gameView = new Viewport(0,
                0, graphicsDevice.DisplayMode.Width / 2, graphicsDevice.DisplayMode.Height);
            textView = new Viewport(graphicsDevice.DisplayMode.Width / 2,
                0, graphicsDevice.DisplayMode.Width / 2, graphicsDevice.DisplayMode.Height);

            camera2D = new Camera2D(GraphicsDevice);
            absPosition = new Vector2(0, 0);
            camPosition = new Vector2(absPosition.X + 16, absPosition.Y + 16);
            camera2D.Position = camPosition;
            camera2D.Zoom = 2f;


            RectangleHoloView = new Texture2D[2];
            RectangleHoloView[0] = new Texture2D(GraphicsDevice, 1, 1);
            RectangleHoloView[1] = new Texture2D(GraphicsDevice, 1, 1);
            RectangleHoloView[0].SetData<Color>(new Color[] { new Color(Color.Aqua, 0.01f) });
            RectangleHoloView[1].SetData<Color>(new Color[] { Color.Red });


            world = new Atom[4][];

            cameraTwo = new Camera2D(GraphicsDevice);
            cameraTwo.Position = new Vector2(textView.Width / 2, textView.Height / 2);
            menu = true;
            menuButtons = new Button2D[3];

            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            try
            {
                turfmap = Content.Load<Texture2D>("turfmap");
                entitymap = Content.Load<Texture2D>("entitymap");
                wallmap = Content.Load<Texture2D>("entitymap");
                Font = Content.Load<SpriteFont>("font");
                menuMusic = Content.Load<Song>("menu1");
                plating = Content.Load<Texture2D>("reinforcedPlating");
                plasteelWallSingle = Content.Load<Texture2D>("wall");
                whiteTile = Content.Load<Texture2D>("whiteTile");
                greyTile = Content.Load<Texture2D>("greyTile");
                menuClown = Content.Load<Texture2D>("clown");
                ss13Logo = Content.Load<Texture2D>("ss13Logo");
                playButton = Content.Load<Texture2D>("play");
                exitButton = Content.Load<Texture2D>("exit");
                crate_0 = Content.Load<Texture2D>("crate_0");
                crateopen_0 = Content.Load<Texture2D>("crateopen_0");

                plasteelWallBottom_Left = Content.Load<Texture2D>("bottom-left");
                plasteelWallBottom_Right = Content.Load<Texture2D>("bottom-right");
                plasteelWallLeft_Right = Content.Load<Texture2D>("left-right");
                plasteelWallTop_Bottom = Content.Load<Texture2D>("top-bottom");
                plasteelWallTop_Left = Content.Load<Texture2D>("top-left");
                plasteelWallTop_Right = Content.Load<Texture2D>("top-right");
                plasteelWallSingle_Left = Content.Load<Texture2D>("single-left");
                plasteelWallTTop_Right_Left = Content.Load<Texture2D>("T-top-right-left");
                plasteelWallSingle_Bottom = Content.Load<Texture2D>("single-bottom");
                plasteelWallSingle_Right = Content.Load<Texture2D>("single-right");
                plasteelWallSingle_Top = Content.Load<Texture2D>("single-top");
                plasteelWallTTop_Bottom_Left = Content.Load<Texture2D>("T-top-bottom-left");
                plasteelWallTTop_Bottom_Right = Content.Load<Texture2D>("T-top-bottom-right");
                plasteelWallTBottom_Right_Left = Content.Load<Texture2D>("T-bottom-right-left");
                plasteelWallAll = Content.Load<Texture2D>("all");
                plasteelWallFull = Content.Load<Texture2D>("full");

                greygrass_0 = Content.Load<Texture2D>("greygrass_0");

                human_0 = Content.Load<Texture2D>("mob/human/body_m_s_0");
                fleet_shorts_0 = Content.Load<Texture2D>("mob/human/fleet_shorts_0");
                expedition_s_0 = Content.Load<Texture2D>("mob/human/expedition_s_0");


                menuButtons[0] = new Button2D(playButton, new Vector2(200, 500), spriteBatch, 3);
                menuButtons[1] = new Button2D(exitButton, new Vector2(200, 700), spriteBatch, 3);
                menuButtons[2] = new Button2D(Content.Load<Texture2D>("map-maker"), new Vector2(200, 590), spriteBatch, 3);
                player = new Player(Vector2.Zero, human_0, new Texture2D[] { fleet_shorts_0, expedition_s_0 });
                mousePlaceAtom.id = ID.OrangeGrass;
                mousePlaceAtom.type = Type.turf;
                mousePlaceAtom = Extensions.PopulateAtom(mousePlaceAtom);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Critical error loading files. Program Exiting.", "Error loading!");
                Exit();
            }
            foreach(Button2D b in menuButtons)
            {
                b.offset = 10;
            }
            whiteTextScreen = new Texture2D(GraphicsDevice, 1, 1);
            whiteTextScreen.SetData(new Color[] { Color.White });
            
            Extensions.draw2D = new Texture2D(graphicsDevice, 32, 32);
            
            if (!menu)
            {
                if (!loading)
                    world[0] = Extensions.GenerateAtoms(Type.turf, turfmap, @"C:\development\ss13\src\launch\game\map\turfcolortable.txt");
                else
                {
                    try
                    {
                        world[0] = AtomArraySerializer.DeserializeAtom("save");
                    }
                    catch
                    {
                        System.Windows.Forms.MessageBox.Show("Critical error loading files. Program Exiting.", "Error loading!");
                        Exit();
                    }
                }
                world[2] = Extensions.GenerateAtoms(Type.entity, entitymap, @"C:\development\ss13\src\launch\game\map\entitycolortable.txt");
                for(int i = 0; i < world[0].Length; i++)
                {
                    if(world[0][i].id == ID.PlasteelWall)
                    {
                        world[0][i] = Extensions.CorrectWallAlignment(world[0], world[0][i]);
                    }
                }
            }
            textbox = new Text(new Rectangle((int)cameraTwo.Position.X - textView.Width / 2,
            (int)cameraTwo.Position.Y - textView.Height / 2,
            textView.Width, textView.Height), spriteBatch, Font);
            console = new Output(textbox);
            defaultAtoms = new DefaultAtoms();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            newstate = Keyboard.GetState();
            newstateM = Mouse.GetState();
            
            
            if (menu)
            {
                rotation += 0.01f;
                foreach (Button2D b in menuButtons)
                    b.Update(Mouse.GetState());

                if (menuButtons[0].isClicked)
                {
                    menu = false;
                    loading = true;
                    //loading = false;
                    LoadContent();
                }
                if (menuButtons[1].isClicked)
                    Exit();
                if (menuButtons[2].isClicked)
                {
                    editingMap = true;
                    menu = false;
                    loading = false;
                    LoadContent();
                }
                if (!playing)
                {
                   // MediaPlayer.Play(menuMusic);
                    MediaPlayer.IsRepeating = true;
                    playing = true;
                }
                return;
            }

            currentLenght = world[0].Length;

            if (currentLenght != oldLenght)
                for (int i = 0; i < world[0].Length; i++)
                {
                    if (world[0][i].id == ID.PlasteelWall)
                    {
                        world[0][i] = Extensions.CorrectWallAlignment(world[0], world[0][i]);
                    }
                }
            //MediaPlayer.Stop();
            //playing = false;
            worldMouse = new Vector2(Vector2.Transform(Mouse.GetState().Position.ToVector2(), Matrix.Invert(camera2D.get_transformation())).X - 240 + textView.Width / 2, Vector2.Transform(Mouse.GetState().Position.ToVector2(), Matrix.Invert(camera2D.get_transformation())).Y + 5);

            mouseTextPosition = new Vector2(Vector2.Transform(Mouse.GetState().Position.ToVector2(), Matrix.Invert(cameraTwo.get_transformation())).X - gameView.Width / 2 , Vector2.Transform(Mouse.GetState().Position.ToVector2(), Matrix.Invert(cameraTwo.get_transformation())).Y + 10);

            #region CameraControls
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                camera2D.Position = new Vector2(camera2D.Position.X, camera2D.Position.Y - 5f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                camera2D.Position = new Vector2(camera2D.Position.X - 5f, camera2D.Position.Y);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                camera2D.Position = new Vector2(camera2D.Position.X, camera2D.Position.Y + 5f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                camera2D.Position = new Vector2(camera2D.Position.X + 5f, camera2D.Position.Y);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
                camera2D.Zoom += 0.01f;
            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
                camera2D.Zoom -= 0.01f;
            #endregion

            if (!menu && newstate.IsKeyDown(Keys.Escape) && !oldstate.IsKeyDown(Keys.Escape))
                menu = true;

            mouseOutOfBounds = true;
            fps = "Out of bounds. " + mouseOutOfBounds + "\n" + currentLenght + " " + oldLenght;

            for (int i = 0; i < world[0].Length; i++)
            {
                if (world[0][i].rectangle.Contains(worldMouse))
                {
                    mouseOutOfBounds = false;
                    fps = Extensions.GetDebugInfo(world[0][i]) + "\n" + worldMouse.ToString() + mouseOutOfBounds;
                    break;
                }
            }
            if (mouseOutOfBounds)
                for (int i = 0; i < world[2].Length; i++)
                {
                    if (world[2][i].hitbox.Contains(worldMouse))
                    {
                        mouseOutOfBounds = false;
                        fps = Extensions.GetDebugInfo(world[2][i]) + "\n" + worldMouse.ToString() + mouseOutOfBounds;
                        break;
                    }
                }

            if (!mouseOutOfBounds && Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                world[0] = Extensions.RemoveAtom(world[0], Extensions.GetAtomAtMouse(world[0], worldMouse));
            }
            if (mouseOutOfBounds && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                world[0] = Extensions.AddAtom(world[0], Extensions.CreateAtom(worldMouse, mousePlaceAtom.type, mousePlaceAtom.id));
            }
            if(!mouseOutOfBounds && Mouse.GetState().MiddleButton == ButtonState.Pressed)
            {

                mousePlaceAtom.type = Extensions.GetAtomAtMouse(world[0], worldMouse).type;
                mousePlaceAtom.id = Extensions.GetAtomAtMouse(world[0], worldMouse).id;

            }
            if (Keyloop(Keys.U))
            {
                string a = Prompt.ShowDialog("Enter atom name: ", "Spawn");
                string[] names = System.Enum.GetNames(typeof(ID));
                for(int i = 0; i < names.Length; i++)
                {
                    if(names[i].ToLower() == a.ToLower())
                    {
                        Atom gh = new Atom();
                        gh.id = (ID)System.Enum.Parse(typeof(ID), names[i], true);

                        mousePlaceAtom = Extensions.PopulateAtom(gh);//(ID)System.Enum.Parse(typeof(ID), names[i], true);
                    }
                }
            }
            if (Keyloop(Keys.T))
            {
                string text = Prompt.ShowDialog("Say:", "Talk");
                console.Add(text);
            }

            for (int i = 0; i < world[2].Length; i++)
            {
                if(world[2][i].entityInfo.clickable && world[2][i].hitbox.Contains(worldMouse) && newstateM.LeftButton == ButtonState.Pressed && oldstateM.LeftButton == ButtonState.Released)
                {
                    for(int n = 0; n < world[2][i].methods.Length; n++)
                    {
                        world[2][i] = (Atom)world[2][i].methods[n].Invoke(null, new object[] { world[2][i], world });
                    }
                }
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.F7) && !menu)
            {
                //string name = Prompt.ShowDialog("Enter the name of the save:", "Save");
                //if (name == "")
                 //   Exit();
                    AtomArraySerializer.SerializeAtom(world[0], "save");
                System.Windows.Forms.MessageBox.Show("Saved!");
            }
            //fps += world[0].Length + " " + world[2].Length;
            oldstate = newstate;
            oldstateM = newstateM;
            oldLenght = currentLenght;
            base.Update(gameTime);
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            

            if(menu)
            {
                GraphicsDevice.Clear(Color.Black);
                GraphicsDevice.Viewport = defaultView;
                spriteBatch.Begin(samplerState: SamplerState.PointClamp);
#pragma warning disable CS0618
                spriteBatch.Draw(ss13Logo, 
                    new Vector2(defaultView.Width / 2 - ss13Logo.Width * 0.125f, defaultView.Height / 2 - ss13Logo.Height * 0.25f - 100),
                    scale: new Vector2(0.25f));
                spriteBatch.Draw(menuClown, new Vector2(1300, 500),
                    origin: new Vector2(menuClown.Width / 2, menuClown.Height / 2), rotation: rotation, scale: new Vector2(5));
                foreach(Button2D b in menuButtons)
                {
                    b.Draw();
                    //spriteBatch.Draw(RectangleHoloView[0], b.hitbox, Color.White);
                }
                
                spriteBatch.End();
                base.Draw(gameTime);
                return;
            }

            GraphicsDevice.Clear(Color.Black);

            GraphicsDevice.Viewport = gameView;

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
                SamplerState.PointClamp, transformMatrix: camera2D.get_transformation());

            //Draw any game textures here.

            Extensions.Mapper(Type.turf, world[0], Vector2.Zero, spriteBatch);
            Extensions.Mapper(Type.entity, world[2], Vector2.Zero, spriteBatch);
            if (debugView)
            {
                for (int i = 0; i < world[0].Length; i++)
                {
                    if (world[0][i].id == ID.PlasteelWall)
                        spriteBatch.Draw(RectangleHoloView[1], world[0][i].hitbox, Color.White);
                    if (world[0][i].id == ID.ReinforcedPlating)
                        spriteBatch.Draw(RectangleHoloView[0], world[0][i].hitbox, Color.White);
                    
                }
                for (int i = 0; i < world[2].Length; i++)
                {

                    if (world[2][i].id == ID.Crate)
                        spriteBatch.Draw(RectangleHoloView[0], world[2][i].hitbox, Color.White);
                }
                Atom[] supersu = Extensions.GetSurroundingAtoms(world[0], Extensions.GetAtomAtMouse(world[0], worldMouse));
                foreach(Atom a in supersu)
                {
                    spriteBatch.Draw(RectangleHoloView[0], a.hitbox, Color.White);
                }

            }
            spriteBatch.Draw(RectangleHoloView[0], new Vector2(worldMouse.X.Nearest32(), worldMouse.Y.Nearest32()), Color.White);

            spriteBatch.Draw(RectangleHoloView[0], worldMouse, Color.White);


            spriteBatch.End();

            GraphicsDevice.Viewport = textView;
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, transformMatrix: cameraTwo.get_transformation());
            spriteBatch.Draw(whiteTextScreen, new Rectangle((int)cameraTwo.Position.X - graphicsDevice.DisplayMode.Width / 2, (int)cameraTwo.Position.Y - graphicsDevice.DisplayMode.Height / 2,  graphicsDevice.DisplayMode.Width, graphicsDevice.DisplayMode.Height), Color.White);

            if (editingMap)
            {
                //Draw anything here that has to go into the selection box of atoms while editing WIP!

                //spriteBatch.Draw(defaultAtoms.GreyTile.texture, Vector2.Zero, Color.White);

                //spriteBatch.End();
                //GraphicsDevice.Viewport = defaultView;
                //return;
            }

            //Draw anything here that goes in the textbox.

            //spriteBatch.Draw(mousePlaceAtom.texture, new Vector2(200, 100), Color.White);

            DrawTexbox();
            spriteBatch.Draw(RectangleHoloView[1], new Vector2(mouseTextPosition.X.Nearest32(), mouseTextPosition.Y.Nearest32()), Color.White);
            //spriteBatch.Draw(greygrass_0, mouseTextPosition, Color.LightGreen);
            spriteBatch.End();

            GraphicsDevice.Viewport = defaultView;

            base.Draw(gameTime);
        }

        #region Methods
        bool Keyloop(Keys k)
        {
            if (newstate.IsKeyDown(k) && !oldstate.IsKeyDown(k))
            {
                return true;
            }
            return false;
        }
        void DrawTexbox()
        {
            fps = textbox.WrapText(fps) + "\n" + textbox.WrapText(camera2D.Position.ToString());
            fps += mouseTextPosition.ToString();
            fps = textbox.FormatText(fps);
            spriteBatch.DrawString(Font, fps, new Vector2(0, 0), Color.Black);

            console.Draw(new Vector2(0, textView.Height - 150));
        }
        #endregion
    }

}
