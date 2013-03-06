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
using FirstGame.Front_end;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace FirstGame
{
    /// <summary>
    /// This is the main type for your game. 
    /// We may want to keep all GUI in here and define classes and methods to act
    /// upon those GUI elements elsewhere. This way we can change the entire GUI
    /// within one file. Which we will need to do to adapt to the final screen size.
    /// We will not use 1920x1080 until the end to help in laptop coding.
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //The screen size will change to 1080P at the end
        int screenWidth = 1280, screenHeight = 720;

        //These are the different game state screens
        enum GameState
        {
            HomeScreen,
            NewLevelScreen,
            LoadLevelScreen,
            LevelEditor,
            PatientGame,
        }
        GameState CurrentGameState = GameState.HomeScreen;
        bool multiState = false;
        bool levelEditorMenuON = false;
        bool pauseMenuON = false;
        bool shapePalletVisible = true;
        bool sizeChoosing = false;
        bool colorChoosing = false;
        bool showingShapes = true;
        bool showingNumbers = false;
        bool showingAlpha = false;
        bool numberChoosing = false;
        bool alphaChoosing = true;

        //Initializing Graphical Elements
        Texture2D myTopHeaderBkGround;
        Vector2 myTopHeaderPosition = Vector2.Zero; //example code
        Texture2D myTitle, myNewLevelTitle, myNewLevel, myName, myDescription, myLoadLevelTitle, myGrid, textBackgorund, magnifyGlass, listBackground;
        Vector2 magnifyGlassPosition = (new Vector2(175, 85));
        Vector2 lisBackgroundPosition = (new Vector2(350, 150));
        Vector2 nameBackgroundPosition = (new Vector2(200, 130));
        Vector2 descriptionBackgroundPosition = (new Vector2(200, 200));
        Vector2 searchBackground = (new Vector2(200, 85));
        Vector2 myNewLevelPosition = (new Vector2(300, 0));
        Vector2 myLoadLevelTitlePosition = (new Vector2(330, 0));
        Vector2 myNamePosition = (new Vector2(0, 130));
        Vector2 myDescriptionPosition = (new Vector2(0, 200));
        Vector2 myTitlePosition = (new Vector2(250, 0));
        Vector2 myNewLevelTitlePosition = (new Vector2(330, 0));
        Vector2 myCancelButtonPosition = (new Vector2(0, 0));
        Vector2 myGridPosition = (new Vector2(0, 110));
        Vector2 originForRotation;
        float rotationAngle = 4.71238898F;
        Texture2D myOSKBackground;
        Vector2 myOSKBackgroundPosition = (new Vector2(390, 510));
        SpriteFont font;
        String nameOfTherapist;
        String descriptionByTherapist;
        Vector2 nameOfTherapistPosition = (new Vector2(200, 130));
        Vector2 descriptionByTherapistPosition = (new Vector2(200, 200));
        bool nameHighlight = true;
        String searchQuery;
        Vector2 searchQueryPosition = (new Vector2(200, 90));
        bool loadKeyBoard = false;
        Texture2D levelEditorMenuBackground, levelEditorMenuTitle;
        Vector2 levelEditorMenuBackgroundPosition = (new Vector2(600, 300));
        Vector2 levelEditorMenuGraphicPosition = (new Vector2(630, 300));
        Texture2D pauseMenuBackground, pauseMenuTitle;
        Vector2 pauseMenuBackgroundPosition = (new Vector2(600, 300));
        Vector2 pauseMenuGraphicPosition = (new Vector2(630, 300));
        //Shape Pallet
        int shapePalletX = 0, shapePalletY = 100;
        Texture2D shapePalletBackground, chooseSize, chooseColor;
        Vector2 shapePalletBackgroundPosition, chooseSizePosition, chooseColorPosition;
        cButton120x50 btnHidePallet, btnAddLetter, btnAddNumber, btnAddShape;
        cButton120x50 sizeTiny, sizeSmall, sizeMedium, sizeLarge, sizeXLarge;
        cButton100x100 btnThumbSquare, btnThumbCircle, btnThumbStar, btnThumbTriangle;
        cButton48x48 colorGreenBtn, colorGreyBtn, colorDarkGreyBtn, colorBlueBtn, colorBlackBtn, colorRedBtn, colorOrangeBtn;
        cButton48x48 colorYellowBtn, colorLightBlueBtn, colorDarkBlueBtn, colorPinkBtn, colorLightGreen;
        cButton48x48 putA, putB, putC, putD, putE, putF, putG, putH, putI, putJ, putK, putL, putM, putN, putO, putP, putQ;
        cButton48x48 putR, putS, putT, putU, putV, putW, putX, putY, putZ, put1, put2, put3, put4, put5, put6, put7, put8, put9;
        cButton48x48 put0;
        

        //Initialize Button Elements (There are different Sizes of Buttons)
        cButton btnNew, btnLoad, btnExit;
        cButton120x50 btnCancel, btnCreate, btnOpen, btnBack;
        cButton120x55 btnUpTime, btnHoldTime;
        cButton55x55 btnHome, btnMenu, btnMultiple, btnPlay, btnRedo, btnUndo, btnMoreUp, btnLessUp, btnMoreHold, btnLessHold;
        cButton48x48 a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z, space1, space2, delete1, clear;
        cButton500x25 clearNameButton, clearDescriptionButton, clearSearchButton;
        cButton25x25 delName, delDesc, delSearch, goSearch;
        int intUpTime, intHoldTime;
        Vector2 intUpTimePosition = (new Vector2(520, 45));
        Vector2 intHoldTimePosition = (new Vector2(785, 45));
        cButton120x50 btnLemBack, btnLemClear, btnLemExit, btnLemLoad, btnLemSave;
        cButton120x50 btnPauseLoad, btnPauseRestart, btnPauseEdit, btnPauseContinue;
    
        //CREATE GAME CONSTRUCTOR//
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = screenWidth; //currently set to 720P for laptop support, will change to 1080P
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

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
        //LOAD CONTENT HERE//
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
          
            //Data Types for holding information
            myTopHeaderBkGround = Content.Load<Texture2D>("GUI/topHeaderBkGround");
            font = Content.Load<SpriteFont>("font");
            nameOfTherapist = "Enter Your Name Here...";
            descriptionByTherapist = "Enter Your Description Here...";
            searchQuery = "Search...";
            textBackgorund = Content.Load<Texture2D>("GUI/textBackground");
            intUpTime = 5;
            intHoldTime = 1;

            //Home Screen Elements - these graphics and buttons make up the home screen
            myTitle = Content.Load<Texture2D>("GUI/targetTappingGame");
            btnNew = new cButton(Content.Load<Texture2D>("GUI/newButton"), graphics.GraphicsDevice);
            btnNew.setPosition(new Vector2(340, 200 ));
            btnLoad = new cButton(Content.Load<Texture2D>("GUI/loadButton"), graphics.GraphicsDevice);
            btnLoad.setPosition(new Vector2(340, 350));
            btnExit = new cButton(Content.Load<Texture2D>("GUI/exitButton"), graphics.GraphicsDevice);
            btnExit.setPosition(new Vector2(340, 500));
            
            //NewLevel Screen Elements - these graphics and buttons make up the create a new level screen, OSK defined here..
            delDesc = new cButton25x25(Content.Load<Texture2D>("Gui/miniX"), graphics.GraphicsDevice);
            delDesc.setPosition(new Vector2(500, 200));
            delName = new cButton25x25(Content.Load<Texture2D>("Gui/miniX"), graphics.GraphicsDevice);
            delName.setPosition(new Vector2(500, 130));
            btnCancel = new cButton120x50(Content.Load<Texture2D>("GUI/cancel"), graphics.GraphicsDevice);
            myNewLevelTitle = Content.Load<Texture2D>("GUI/newLevel");
            myNewLevel = Content.Load<Texture2D>("GUI/newLevel");
            btnCreate = new cButton120x50(Content.Load<Texture2D>("GUI/createButton"), graphics.GraphicsDevice);
            btnCreate.setPosition(new Vector2(1160, 0));
            myName = Content.Load<Texture2D>("GUI/name");
            clearNameButton = new cButton500x25(Content.Load<Texture2D>("GUI/nothing"), graphics.GraphicsDevice);
            clearNameButton.setPosition(new Vector2(0, 130));
            clearDescriptionButton = new cButton500x25(Content.Load<Texture2D>("GUI/nothing"), graphics.GraphicsDevice);
            clearDescriptionButton.setPosition(new Vector2(0, 200));
            myDescription = Content.Load<Texture2D>("GUI/description");
            myOSKBackground = Content.Load<Texture2D>("OSK/keyboardBackground");
            float keyStartingX = 401F;
            float keyStartingY = 520F;
            a = new cButton48x48(Content.Load<Texture2D>("OSK/aButton"), graphics.GraphicsDevice);
            a.setPosition(new Vector2(keyStartingX+10, keyStartingY+50));
            b = new cButton48x48(Content.Load<Texture2D>("OSK/bButton"), graphics.GraphicsDevice);
            b.setPosition(new Vector2(keyStartingX+230, keyStartingY+100));
            c = new cButton48x48(Content.Load<Texture2D>("OSK/cButton"), graphics.GraphicsDevice);
            c.setPosition(new Vector2(keyStartingX+130, keyStartingY+100));
            d = new cButton48x48(Content.Load<Texture2D>("OSK/dButton"), graphics.GraphicsDevice);
            d.setPosition(new Vector2(keyStartingX+110, keyStartingY+50));
            e = new cButton48x48(Content.Load<Texture2D>("OSK/eButton"), graphics.GraphicsDevice);
            e.setPosition(new Vector2(keyStartingX+100, keyStartingY));
            f = new cButton48x48(Content.Load<Texture2D>("OSK/fButton"), graphics.GraphicsDevice);
            f.setPosition(new Vector2(keyStartingX+160, keyStartingY+50));
            g = new cButton48x48(Content.Load<Texture2D>("OSK/gButton"), graphics.GraphicsDevice);
            g.setPosition(new Vector2(keyStartingX+210, keyStartingY+50));
            h = new cButton48x48(Content.Load<Texture2D>("OSK/hButton"), graphics.GraphicsDevice);
            h.setPosition(new Vector2(keyStartingX+260, keyStartingY+50));
            i = new cButton48x48(Content.Load<Texture2D>("OSK/iButton"), graphics.GraphicsDevice);
            i.setPosition(new Vector2(keyStartingX+350, keyStartingY));
            j = new cButton48x48(Content.Load<Texture2D>("OSK/jButton"), graphics.GraphicsDevice);
            j.setPosition(new Vector2(keyStartingX+310, keyStartingY+50));
            k = new cButton48x48(Content.Load<Texture2D>("OSK/kButton"), graphics.GraphicsDevice);
            k.setPosition(new Vector2(keyStartingX+360, keyStartingY+50));
            l = new cButton48x48(Content.Load<Texture2D>("OSK/lButton"), graphics.GraphicsDevice);
            l.setPosition(new Vector2(keyStartingX+410, keyStartingY+50));
            m = new cButton48x48(Content.Load<Texture2D>("OSK/mButton"), graphics.GraphicsDevice);
            m.setPosition(new Vector2(keyStartingX+330, keyStartingY+100));
            n = new cButton48x48(Content.Load<Texture2D>("OSK/nButton"), graphics.GraphicsDevice);
            n.setPosition(new Vector2(keyStartingX+280, keyStartingY+100));
            o = new cButton48x48(Content.Load<Texture2D>("OSK/oButton"), graphics.GraphicsDevice);
            o.setPosition(new Vector2(keyStartingX+400, keyStartingY));
            p = new cButton48x48(Content.Load<Texture2D>("OSK/pButton"), graphics.GraphicsDevice);
            p.setPosition(new Vector2(keyStartingX+450, keyStartingY));
            q = new cButton48x48(Content.Load<Texture2D>("OSK/qButton"), graphics.GraphicsDevice);
            q.setPosition(new Vector2(keyStartingX, keyStartingY));
            r = new cButton48x48(Content.Load<Texture2D>("OSK/rButton"), graphics.GraphicsDevice);
            r.setPosition(new Vector2(keyStartingX+150, keyStartingY));
            s = new cButton48x48(Content.Load<Texture2D>("OSK/sButton"), graphics.GraphicsDevice);
            s.setPosition(new Vector2(keyStartingX+60, keyStartingY+50));
            t = new cButton48x48(Content.Load<Texture2D>("OSK/tButton"), graphics.GraphicsDevice);
            t.setPosition(new Vector2(keyStartingX+200, keyStartingY));
            u = new cButton48x48(Content.Load<Texture2D>("OSK/uButton"), graphics.GraphicsDevice);
            u.setPosition(new Vector2(keyStartingX+300, keyStartingY));
            v = new cButton48x48(Content.Load<Texture2D>("OSK/vButton"), graphics.GraphicsDevice);
            v.setPosition(new Vector2(keyStartingX+180, keyStartingY+100));
            w = new cButton48x48(Content.Load<Texture2D>("OSK/wButton"), graphics.GraphicsDevice);
            w.setPosition(new Vector2(keyStartingX+50, keyStartingY));
            x = new cButton48x48(Content.Load<Texture2D>("OSK/xButton"), graphics.GraphicsDevice);
            x.setPosition(new Vector2(keyStartingX+80, keyStartingY+100));
            y = new cButton48x48(Content.Load<Texture2D>("OSK/yButton"), graphics.GraphicsDevice);
            y.setPosition(new Vector2(keyStartingX+250, keyStartingY));
            z = new cButton48x48(Content.Load<Texture2D>("OSK/zButton"), graphics.GraphicsDevice);
            z.setPosition(new Vector2(keyStartingX+30, keyStartingY+100));
            space1 = new cButton48x48(Content.Load<Texture2D>("OSK/spaceButton"), graphics.GraphicsDevice);
            space1.setPosition(new Vector2(keyStartingX + 200, keyStartingY + 150));
            space2 = new cButton48x48(Content.Load<Texture2D>("OSK/spaceButton"), graphics.GraphicsDevice);
            space2.setPosition(new Vector2(keyStartingX + 250, keyStartingY + 150));
            delete1 = new cButton48x48(Content.Load<Texture2D>("OSK/deleteButton"), graphics.GraphicsDevice);
            delete1.setPosition(new Vector2(keyStartingX + 400, keyStartingY + 100));
            clear = new cButton48x48(Content.Load<Texture2D>("OSK/clear"), graphics.GraphicsDevice);
            clear.setPosition(new Vector2(keyStartingX + 400, keyStartingY + 150));

            //LoadGame Screen Elements - these buttons and graphics make up the load screen, OSK is defined above
            magnifyGlass = Content.Load<Texture2D>("GUI/magnifyGlass");
            listBackground = Content.Load<Texture2D>("GUI/listBackground");
            myLoadLevelTitle = Content.Load<Texture2D>("GUI/loadGameTitle");
            btnOpen = new cButton120x50(Content.Load<Texture2D>("GUI/openButton"), graphics.GraphicsDevice);
            btnOpen.setPosition(new Vector2(1160, 0));
            delSearch = new cButton25x25(Content.Load<Texture2D>("Gui/miniX"), graphics.GraphicsDevice);
            delSearch.setPosition(new Vector2(400, 85));
            goSearch = new cButton25x25(Content.Load<Texture2D>("Gui/go"), graphics.GraphicsDevice);
            goSearch.setPosition(new Vector2(425, 85));
            clearSearchButton = new cButton500x25(Content.Load<Texture2D>("GUI/nothing"), graphics.GraphicsDevice);
            clearSearchButton.setPosition(new Vector2(175, 85));

            //Level Editor Elements - these buttons and graphics make up the level editor screen.
            btnHome = new cButton55x55(Content.Load<Texture2D>("LevelEditorGUI/homeButton"), graphics.GraphicsDevice);
            btnHome.setPosition(new Vector2(30, 30));
            btnMenu = new cButton55x55(Content.Load<Texture2D>("LevelEditorGUI/menuButton"), graphics.GraphicsDevice);
            btnMenu.setPosition(new Vector2(95, 30));
            btnHoldTime = new cButton120x55(Content.Load<Texture2D>("LevelEditorGUI/holdTimeButton"), graphics.GraphicsDevice);
            btnHoldTime.setPosition(new Vector2(700, 30));
            btnMultiple = new cButton55x55(Content.Load<Texture2D>("LevelEditorGUI/multipleToggleOff"), graphics.GraphicsDevice);
            btnMultiple.setPosition(new Vector2(355, 30));
            btnPlay = new cButton55x55(Content.Load<Texture2D>("LevelEditorGUI/playButton"), graphics.GraphicsDevice);
            btnPlay.setPosition(new Vector2(290, 30));
            btnRedo = new cButton55x55(Content.Load<Texture2D>("LevelEditorGUI/redoButton"), graphics.GraphicsDevice);
            btnRedo.setPosition(new Vector2(215, 30));
            btnUndo = new cButton55x55(Content.Load<Texture2D>("LevelEditorGUI/undoButton"), graphics.GraphicsDevice);
            btnUndo.setPosition(new Vector2(160, 30));
            btnUpTime = new cButton120x55(Content.Load<Texture2D>("LevelEditorGUI/upTimeButton"), graphics.GraphicsDevice);
            btnUpTime.setPosition(new Vector2(440, 30));
            btnMoreUp = new cButton55x55(Content.Load<Texture2D>("LevelEditorGUI/moreButton"), graphics.GraphicsDevice);
            btnMoreUp.setPosition(new Vector2(560, 30));
            btnMoreHold = new cButton55x55(Content.Load<Texture2D>("LevelEditorGUI/moreButton"), graphics.GraphicsDevice);
            btnMoreHold.setPosition(new Vector2(820, 30));
            btnLessUp = new cButton55x55(Content.Load<Texture2D>("LevelEditorGUI/lessButton"), graphics.GraphicsDevice);
            btnLessUp.setPosition(new Vector2(615, 30));
            btnLessHold = new cButton55x55(Content.Load<Texture2D>("LevelEditorGUI/lessButton"), graphics.GraphicsDevice);
            btnLessHold.setPosition(new Vector2(875, 30));
            myGrid = Content.Load<Texture2D>("LevelEditorGUI/placementGrid");
            levelEditorMenuBackground = Content.Load<Texture2D>("LevelEditorMenu/menuBackground");
            levelEditorMenuTitle = Content.Load<Texture2D>("LevelEditorMenu/levelEditorMenuGraphic");
            //Vector2 levelEditorMenuGraphicPosition = (new Vector2(630, 300));
            btnLemBack = new cButton120x50(Content.Load<Texture2D>("LevelEditorMenu/backButtonGraphic"),graphics.GraphicsDevice);
            btnLemBack.setPosition(new Vector2(630, 355));
            btnLemSave = new cButton120x50(Content.Load<Texture2D>("LevelEditorMenu/saveButtonGraphic"), graphics.GraphicsDevice);
            btnLemSave.setPosition(new Vector2(630, 410));
            btnLemLoad = new cButton120x50(Content.Load<Texture2D>("LevelEditorMenu/loadButtonGraphic"), graphics.GraphicsDevice);
            btnLemLoad.setPosition(new Vector2(630, 465));
            btnLemClear = new cButton120x50(Content.Load<Texture2D>("LevelEditorMenu/clearButtonGraphic"), graphics.GraphicsDevice);
            btnLemClear.setPosition(new Vector2(630, 520));
            btnLemExit = new cButton120x50(Content.Load<Texture2D>("LevelEditorMenu/exitButtonGraphic"), graphics.GraphicsDevice);
            btnLemExit.setPosition(new Vector2(630, 575));
            //Shape Pallet
            shapePalletBackground = Content.Load<Texture2D>("ShapePallet/shapePalletBackground");
            shapePalletBackgroundPosition = (new Vector2(shapePalletX, shapePalletY));
            btnHidePallet = new cButton120x50(Content.Load<Texture2D>("ShapePallet/hidePallet"), graphics.GraphicsDevice);
            btnHidePallet.setPosition(new Vector2(shapePalletX+0, shapePalletY));
            btnThumbCircle = new cButton100x100(Content.Load<Texture2D>("ShapePallet/demoCircle"), graphics.GraphicsDevice);
            btnThumbCircle.setPosition(new Vector2(shapePalletX + 20, shapePalletY+60));
            btnThumbSquare = new cButton100x100(Content.Load<Texture2D>("ShapePallet/demoSquare"), graphics.GraphicsDevice);
            btnThumbSquare.setPosition(new Vector2(shapePalletX + 20, shapePalletY+160));
            btnThumbStar= new cButton100x100(Content.Load<Texture2D>("ShapePallet/demoStar"), graphics.GraphicsDevice);
            btnThumbStar.setPosition(new Vector2(shapePalletX + 20, shapePalletY+260));
            btnThumbTriangle = new cButton100x100(Content.Load<Texture2D>("ShapePallet/demoTriangle"), graphics.GraphicsDevice);
            btnThumbTriangle.setPosition(new Vector2(shapePalletX + 20, shapePalletY+360));
            btnAddLetter = new cButton120x50(Content.Load<Texture2D>("ShapePallet/addLetter"), graphics.GraphicsDevice);
            btnAddLetter.setPosition(new Vector2(shapePalletX + 15, shapePalletY+550));
            btnAddNumber = new cButton120x50(Content.Load<Texture2D>("ShapePallet/addNumber"), graphics.GraphicsDevice);
            btnAddNumber.setPosition(new Vector2(shapePalletX + 15, shapePalletY+500));
            btnAddShape = new cButton120x50(Content.Load<Texture2D>("ShapePallet/addShape"), graphics.GraphicsDevice);
            btnAddShape.setPosition(new Vector2(shapePalletX + 15, shapePalletY+500));
            chooseSize = Content.Load<Texture2D>("ShapePallet/chooseSize");
            chooseSizePosition = (new Vector2(shapePalletX+10, shapePalletY+10));
            sizeTiny = new cButton120x50(Content.Load<Texture2D>("ShapePallet/sizeTiny"), graphics.GraphicsDevice);
            sizeTiny.setPosition(new Vector2(shapePalletX + 5, shapePalletY+60));
            sizeSmall = new cButton120x50(Content.Load<Texture2D>("ShapePallet/sizeSmall"), graphics.GraphicsDevice);
            sizeSmall.setPosition(new Vector2(shapePalletX + 5, shapePalletY+110));
            sizeMedium = new cButton120x50(Content.Load<Texture2D>("ShapePallet/sizeMedium"), graphics.GraphicsDevice);
            sizeMedium.setPosition(new Vector2(shapePalletX + 5, shapePalletY+160));
            sizeLarge = new cButton120x50(Content.Load<Texture2D>("ShapePallet/sizeLarge"), graphics.GraphicsDevice);
            sizeLarge.setPosition(new Vector2(shapePalletX + 5, shapePalletY+210));
            sizeXLarge = new cButton120x50(Content.Load<Texture2D>("ShapePallet/sizeXLarge"), graphics.GraphicsDevice);
            sizeXLarge.setPosition(new Vector2(shapePalletX + 5, shapePalletY+260));
            chooseColor = Content.Load<Texture2D>("ShapePallet/chooseColor");
            chooseColorPosition = (new Vector2(shapePalletX + 10, shapePalletY + 10));
            colorBlackBtn = new cButton48x48(Content.Load<Texture2D>("ShapePallet/blackColor"), graphics.GraphicsDevice);
            colorBlackBtn.setPosition(new Vector2(shapePalletX + 5, shapePalletY + 40));
            colorBlueBtn = new cButton48x48(Content.Load<Texture2D>("ShapePallet/blueColor"), graphics.GraphicsDevice);
            colorBlueBtn.setPosition(new Vector2(shapePalletX + 55, shapePalletY + 40));
            colorDarkBlueBtn = new cButton48x48(Content.Load<Texture2D>("ShapePallet/darkBlueColor"), graphics.GraphicsDevice);
            colorDarkBlueBtn.setPosition(new Vector2(shapePalletX + 5, shapePalletY + 90));
            colorDarkGreyBtn = new cButton48x48(Content.Load<Texture2D>("ShapePallet/darkGreyColor"), graphics.GraphicsDevice);
            colorDarkGreyBtn.setPosition(new Vector2(shapePalletX + 55, shapePalletY + 90));
            colorGreenBtn = new cButton48x48(Content.Load<Texture2D>("ShapePallet/greenColor"), graphics.GraphicsDevice);
            colorGreenBtn.setPosition(new Vector2(shapePalletX + 5, shapePalletY + 140));
            colorGreyBtn = new cButton48x48(Content.Load<Texture2D>("ShapePallet/greyColor"), graphics.GraphicsDevice);
            colorGreyBtn.setPosition(new Vector2(shapePalletX + 55, shapePalletY + 140));
            colorLightBlueBtn = new cButton48x48(Content.Load<Texture2D>("ShapePallet/lightBlueColor"), graphics.GraphicsDevice);
            colorLightBlueBtn.setPosition(new Vector2(shapePalletX + 5, shapePalletY + 190));
            colorLightGreen = new cButton48x48(Content.Load<Texture2D>("ShapePallet/lightGreenColor"), graphics.GraphicsDevice);
            colorLightGreen.setPosition(new Vector2(shapePalletX + 55, shapePalletY + 190));
            colorOrangeBtn = new cButton48x48(Content.Load<Texture2D>("ShapePallet/orangeColor"), graphics.GraphicsDevice);
            colorOrangeBtn.setPosition(new Vector2(shapePalletX + 5, shapePalletY + 240));
            colorPinkBtn = new cButton48x48(Content.Load<Texture2D>("ShapePallet/pinkColor"), graphics.GraphicsDevice);
            colorPinkBtn.setPosition(new Vector2(shapePalletX + 55, shapePalletY + 240));
            colorRedBtn = new cButton48x48(Content.Load<Texture2D>("ShapePallet/redColor"), graphics.GraphicsDevice);
            colorRedBtn.setPosition(new Vector2(shapePalletX + 5, shapePalletY + 290));
            colorYellowBtn = new cButton48x48(Content.Load<Texture2D>("ShapePallet/yellowColor"), graphics.GraphicsDevice);
            colorYellowBtn.setPosition(new Vector2(shapePalletX + 55, shapePalletY + 290));
            putA = new cButton48x48(Content.Load<Texture2D>("OSK/aButton"), graphics.GraphicsDevice);
            putA.setPosition(new Vector2(shapePalletX, shapePalletY+50));
            putB = new cButton48x48(Content.Load<Texture2D>("OSK/bButton"), graphics.GraphicsDevice);
            putB.setPosition(new Vector2(shapePalletX+48, shapePalletY+50));
            putC = new cButton48x48(Content.Load<Texture2D>("OSK/cButton"), graphics.GraphicsDevice);
            putC.setPosition(new Vector2(shapePalletX+96, shapePalletY+50));
            putD = new cButton48x48(Content.Load<Texture2D>("OSK/dButton"), graphics.GraphicsDevice);
            putD.setPosition(new Vector2(shapePalletX + 0, shapePalletY + 100));
            putE = new cButton48x48(Content.Load<Texture2D>("OSK/eButton"), graphics.GraphicsDevice);
            putE.setPosition(new Vector2(shapePalletX + 48, shapePalletY + 100));
            putF = new cButton48x48(Content.Load<Texture2D>("OSK/fButton"), graphics.GraphicsDevice);
            putF.setPosition(new Vector2(shapePalletX + 96, shapePalletY + 100));
            putG = new cButton48x48(Content.Load<Texture2D>("OSK/gButton"), graphics.GraphicsDevice);
            putG.setPosition(new Vector2(shapePalletX + 0 , shapePalletY + 150));
            putH = new cButton48x48(Content.Load<Texture2D>("OSK/hButton"), graphics.GraphicsDevice);
            putH.setPosition(new Vector2(shapePalletX + 48, shapePalletY + 150));
            putI = new cButton48x48(Content.Load<Texture2D>("OSK/iButton"), graphics.GraphicsDevice);
            putI.setPosition(new Vector2(shapePalletX + 96, shapePalletY + 150));
            putJ = new cButton48x48(Content.Load<Texture2D>("OSK/jButton"), graphics.GraphicsDevice);
            putJ.setPosition(new Vector2(shapePalletX + 0, shapePalletY + 200));
            putK = new cButton48x48(Content.Load<Texture2D>("OSK/kButton"), graphics.GraphicsDevice);
            putK.setPosition(new Vector2(shapePalletX + 48, shapePalletY + 200));
            putL = new cButton48x48(Content.Load<Texture2D>("OSK/lButton"), graphics.GraphicsDevice);
            putL.setPosition(new Vector2(shapePalletX + 96, shapePalletY + 200));
            putM = new cButton48x48(Content.Load<Texture2D>("OSK/mButton"), graphics.GraphicsDevice);
            putM.setPosition(new Vector2(shapePalletX + 0, shapePalletY + 250));
            putN = new cButton48x48(Content.Load<Texture2D>("OSK/nButton"), graphics.GraphicsDevice);
            putN.setPosition(new Vector2(shapePalletX + 48, shapePalletY + 250));
            putO = new cButton48x48(Content.Load<Texture2D>("OSK/oButton"), graphics.GraphicsDevice);
            putO.setPosition(new Vector2(shapePalletX + 96, shapePalletY + 250));
            putP = new cButton48x48(Content.Load<Texture2D>("OSK/pButton"), graphics.GraphicsDevice);
            putP.setPosition(new Vector2(shapePalletX + 0, shapePalletY + 300));
            putQ = new cButton48x48(Content.Load<Texture2D>("OSK/qButton"), graphics.GraphicsDevice);
            putQ.setPosition(new Vector2(shapePalletX + 48, shapePalletY + 300));
            putR = new cButton48x48(Content.Load<Texture2D>("OSK/rButton"), graphics.GraphicsDevice);
            putR.setPosition(new Vector2(shapePalletX + 96, shapePalletY + 300));
            putS = new cButton48x48(Content.Load<Texture2D>("OSK/sButton"), graphics.GraphicsDevice);
            putS.setPosition(new Vector2(shapePalletX + 0, shapePalletY + 350));
            putT = new cButton48x48(Content.Load<Texture2D>("OSK/tButton"), graphics.GraphicsDevice);
            putT.setPosition(new Vector2(shapePalletX + 48, shapePalletY + 350));
            putU = new cButton48x48(Content.Load<Texture2D>("OSK/uButton"), graphics.GraphicsDevice);
            putU.setPosition(new Vector2(shapePalletX + 96, shapePalletY + 350));
            putV = new cButton48x48(Content.Load<Texture2D>("OSK/vButton"), graphics.GraphicsDevice);
            putV.setPosition(new Vector2(shapePalletX + 0, shapePalletY + 400));
            putW = new cButton48x48(Content.Load<Texture2D>("OSK/wButton"), graphics.GraphicsDevice);
            putW.setPosition(new Vector2(shapePalletX + 48, shapePalletY + 400));
            putX = new cButton48x48(Content.Load<Texture2D>("OSK/xButton"), graphics.GraphicsDevice);
            putX.setPosition(new Vector2(shapePalletX + 96, shapePalletY + 400));
            putY = new cButton48x48(Content.Load<Texture2D>("OSK/yButton"), graphics.GraphicsDevice);
            putY.setPosition(new Vector2(shapePalletX + 0, shapePalletY + 450));
            putZ = new cButton48x48(Content.Load<Texture2D>("OSK/zButton"), graphics.GraphicsDevice);
            putZ.setPosition(new Vector2(shapePalletX + 48, shapePalletY + 450));

            put1 = new cButton48x48(Content.Load<Texture2D>("ShapePallet/oneBtn"), graphics.GraphicsDevice);
            put1.setPosition(new Vector2(shapePalletX + 0, shapePalletY + 50));
            put2 = new cButton48x48(Content.Load<Texture2D>("ShapePallet/twoBtn"), graphics.GraphicsDevice);
            put2.setPosition(new Vector2(shapePalletX + 48, shapePalletY + 50));
            put3 = new cButton48x48(Content.Load<Texture2D>("ShapePallet/threeBtn"), graphics.GraphicsDevice);
            put3.setPosition(new Vector2(shapePalletX + 96, shapePalletY + 50));
            put4 = new cButton48x48(Content.Load<Texture2D>("ShapePallet/fourBtn"), graphics.GraphicsDevice);
            put4.setPosition(new Vector2(shapePalletX + 0, shapePalletY + 100));
            put5 = new cButton48x48(Content.Load<Texture2D>("ShapePallet/fiveBtn"), graphics.GraphicsDevice);
            put5.setPosition(new Vector2(shapePalletX + 48, shapePalletY + 100));
            put6 = new cButton48x48(Content.Load<Texture2D>("ShapePallet/sixBtn"), graphics.GraphicsDevice);
            put6.setPosition(new Vector2(shapePalletX + 96, shapePalletY + 100));
            put7 = new cButton48x48(Content.Load<Texture2D>("ShapePallet/sevenBtn"), graphics.GraphicsDevice);
            put7.setPosition(new Vector2(shapePalletX + 0, shapePalletY + 150));
            put8 = new cButton48x48(Content.Load<Texture2D>("ShapePallet/eightBtn"), graphics.GraphicsDevice);
            put8.setPosition(new Vector2(shapePalletX + 48, shapePalletY + 150));
            put9 = new cButton48x48(Content.Load<Texture2D>("ShapePallet/nineBtn"), graphics.GraphicsDevice);
            put9.setPosition(new Vector2(shapePalletX + 96, shapePalletY + 150));
            put0 = new cButton48x48(Content.Load<Texture2D>("ShapePallet/zeroBtn"), graphics.GraphicsDevice);
            put0.setPosition(new Vector2(shapePalletX + 0, shapePalletY + 200));

            //game play elements - these elements make up the patient game play screen.
            btnBack = new cButton120x50(Content.Load<Texture2D>("GUI/backButton"), graphics.GraphicsDevice);
            btnBack.setPosition(new Vector2(0, 0));
            pauseMenuBackground = Content.Load<Texture2D>("GamePauseMenu/menuBackground");
            pauseMenuTitle = Content.Load<Texture2D>("GamePauseMenu/pauseMenuGraphic");
            btnPauseContinue = new cButton120x50(Content.Load<Texture2D>("GamePauseMenu/continueButtonGraphic"), graphics.GraphicsDevice);
            btnPauseContinue.setPosition(new Vector2(630, 355));
            btnPauseEdit = new cButton120x50(Content.Load<Texture2D>("GamePauseMenu/editButtonGraphic"), graphics.GraphicsDevice);
            btnPauseEdit.setPosition(new Vector2(630, 410));
            btnPauseLoad = new cButton120x50(Content.Load<Texture2D>("GamePauseMenu/changeLevelButtonGraphic"), graphics.GraphicsDevice);
            btnPauseLoad.setPosition(new Vector2(630, 465));
            btnPauseRestart = new cButton120x50(Content.Load<Texture2D>("GamePauseMenu/restartButtonGraphic"), graphics.GraphicsDevice);
            btnPauseRestart.setPosition(new Vector2(630, 520));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            //NOT CURRENTLY USED
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        //UPDATE GAME HERE//
        protected override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            switch (CurrentGameState)
            {
                    //update if in HOME SCREEN
                case GameState.HomeScreen:
                    if (btnNew.isClicked == true)
                    {
                        CurrentGameState = GameState.NewLevelScreen;
                    }
                    if (btnLoad.isClicked == true)
                    {
                        CurrentGameState = GameState.LoadLevelScreen;
                    }
                    if (btnExit.isClicked == true)
                    {
                        this.Exit();
                    }
                    UpdateHomeScreen(gameTime, mouse);
                    break;

                    //update if in NEWLEVEL SCREEN
                case GameState.NewLevelScreen:
                    if (btnCancel.isClicked == true)
                    {
                        CurrentGameState = GameState.HomeScreen;
                    }
                    if (btnCreate.isClicked == true)
                    {
                        CurrentGameState = GameState.LevelEditor;
                    }
                    if (clearNameButton.isClicked == true)
                    {
                        nameHighlight = true;
                    }
                    if (clearDescriptionButton.isClicked == true)
                    {
                        nameHighlight = false;  
                    }
                    if (a.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "a"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "a"; }
                        a.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (b.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "b"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "b"; }
                        b.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (c.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "c"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "c"; }
                        c.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (d.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "d"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "d"; }
                        d.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (e.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "e"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "e"; }
                        e.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (f.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "f"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "f"; }
                        f.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (g.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "g"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "g"; }
                        g.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (h.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "h"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "h"; }
                        h.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (i.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "i"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "i"; }
                        i.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (j.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "j"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "j"; }
                        j.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (k.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "k"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "k"; }
                        k.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (l.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "l"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "l"; }
                        l.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (m.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "m"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "m"; }
                        m.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (n.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "n"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "n"; }
                        n.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (o.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "o"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "o"; }
                        o.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (p.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "p"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "p"; }
                        p.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (q.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "q"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "q"; }
                        q.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (r.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "r"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "r"; }
                        r.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (s.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "s"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "s"; }
                        s.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (t.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "t"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "t"; }
                        t.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (u.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "u"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "u"; }
                        u.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (v.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "v"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "v"; }
                        v.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (w.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "w"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "w"; }
                        w.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (x.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "x"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "x"; }
                        x.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (y.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "y"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "y"; }
                        y.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (z.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "z"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "z"; }
                        z.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (space1.isClicked == true || space2.isClicked)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "_"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "_"; }
                        space1.isClicked = false;
                        space2.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (delete1.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist.Remove((nameOfTherapist.Length)-1); }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist.Remove((descriptionByTherapist.Length) - 1); }
                        delete1.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (clear.isClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = ""; }
                        if (nameHighlight == false) { descriptionByTherapist = ""; }
                        clear.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (delName.isClicked == true)
                    {
                        nameHighlight = true;
                        nameOfTherapist = "";
                    }
                    if (delDesc.isClicked == true)
                    {
                        nameHighlight = false;
                        descriptionByTherapist = "";
                    }
                    UpdateNewLevelScreen(gameTime, mouse);
                    break;

                    //update if in LOAD LEVEL SCREEN
                case GameState.LoadLevelScreen:
                    if (goSearch.isClicked == true)
                    {

                    }
                    if (btnCancel.isClicked == true)
                    {
                        CurrentGameState = GameState.HomeScreen;
                    }
                    if (btnOpen.isClicked == true)
                    {
                        CurrentGameState = GameState.LevelEditor;
                    }
                    if (delSearch.isClicked == true)
                    {
                        searchQuery = "";
                    }
                    if (clearSearchButton.isClicked == true)
                    {
                        if (loadKeyBoard == false) { loadKeyBoard = true; } //else { loadKeyBoard = false; }
                        clearSearchButton.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (a.isClicked == true)
                    {
                        searchQuery = searchQuery + "a";
                        a.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (b.isClicked == true)
                    {
                        searchQuery = searchQuery + "b";
                        b.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (c.isClicked == true)
                    {
                        searchQuery = searchQuery + "c";
                        c.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (d.isClicked == true)
                    {
                        searchQuery = searchQuery + "d";
                        d.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (e.isClicked == true)
                    {
                        searchQuery = searchQuery + "e";
                        e.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (f.isClicked == true)
                    {
                        searchQuery = searchQuery + "f";
                        f.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (g.isClicked == true)
                    {
                        searchQuery = searchQuery + "g";
                        g.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (h.isClicked == true)
                    {
                        searchQuery = searchQuery + "h";
                        h.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (i.isClicked == true)
                    {
                        searchQuery = searchQuery + "i";
                        i.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (j.isClicked == true)
                    {
                        searchQuery = searchQuery + "j";
                        j.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (k.isClicked == true)
                    {
                        searchQuery = searchQuery + "k";
                        k.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (l.isClicked == true)
                    {
                        searchQuery = searchQuery + "l";
                        l.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (m.isClicked == true)
                    {
                        searchQuery = searchQuery + "m";
                        m.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (n.isClicked == true)
                    {
                        searchQuery = searchQuery + "n";
                        n.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (o.isClicked == true)
                    {
                        searchQuery = searchQuery + "o";
                        o.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (p.isClicked == true)
                    {
                        searchQuery = searchQuery + "p";
                        p.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (q.isClicked == true)
                    {
                        searchQuery = searchQuery + "q";
                        q.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (r.isClicked == true)
                    {
                        searchQuery = searchQuery + "r";
                        r.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (s.isClicked == true)
                    {
                        searchQuery = searchQuery + "s";
                        s.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (t.isClicked == true)
                    {
                        searchQuery = searchQuery + "t";
                        t.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (u.isClicked == true)
                    {
                        searchQuery = searchQuery + "u";
                        u.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (v.isClicked == true)
                    {
                        searchQuery = searchQuery + "v";
                        v.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (w.isClicked == true)
                    {
                        searchQuery = searchQuery + "w";
                        w.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (x.isClicked == true)
                    {
                        searchQuery = searchQuery + "x";
                        x.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (y.isClicked == true)
                    {
                        searchQuery = searchQuery + "y";
                        y.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (z.isClicked == true)
                    {
                        searchQuery = searchQuery + "z";
                        z.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (space1.isClicked == true || space2.isClicked)
                    {
                        searchQuery = searchQuery + "_";
                        space1.isClicked = false;
                        space2.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (delete1.isClicked == true)
                    {
                        searchQuery = searchQuery.Remove((searchQuery.Length) - 1);
                        delete1.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (clear.isClicked == true)
                    {
                        searchQuery = "";
                        clear.isClicked = false;
                        Thread.Sleep(50);
                    }
                    UpdateLoadLevelScreen(gameTime, mouse);
                    break;

                    //update if in LEVEL EDITIOR SCREEN
                case GameState.LevelEditor:
                    if (btnHome.isClicked == true)
                    {
                        CurrentGameState = GameState.HomeScreen;
                        //Call A Method Defined In Another Class
                    }
                    if (btnMenu.isClicked == true)
                    {
                        if (levelEditorMenuON == true)
                        {
                            levelEditorMenuON = false;
                        }else
                        {
                            levelEditorMenuON = true;
                        }
                        btnMenu.Update(mouse);
                        btnMenu.isClicked = false;
                        Thread.Sleep(50);
                        //Call A Method Defined In Another Class
                    }
                    if (btnHoldTime.isClicked == true)
                    {
                        //Call A Method Defined In Another Class
                    }
                    if (btnMultiple.isClicked == true)
                    {
                        if (multiState == true)
                        {
                            multiState = false;
                            btnMultiple = new cButton55x55(Content.Load<Texture2D>("LevelEditorGUI/multipleToggleOff"), graphics.GraphicsDevice);
                            btnMultiple.setPosition(new Vector2(355, 30));
                            btnMultiple.Update(mouse);
                            btnMultiple.isClicked = false;
                            Thread.Sleep(50);
                        }
                        else if (multiState == false)
                        {
                            multiState = true;
                            btnMultiple = new cButton55x55(Content.Load<Texture2D>("LevelEditorGUI/multipleToggleOn"), graphics.GraphicsDevice);
                            btnMultiple.setPosition(new Vector2(355, 30));
                            btnMultiple.Update(mouse);
                            btnMultiple.isClicked = false;
                            Thread.Sleep(50);
                        }
                        //Call A Method Defined In Another Class
                    }
                    if (btnPlay.isClicked == true)
                    {
                        CurrentGameState = GameState.PatientGame; 
                        //Call A Method Defined In Another Class
                    }
                    if (btnRedo.isClicked == true)
                    {
                        //Call A Method Defined In Another Class
                    }
                    if (btnUndo.isClicked == true)
                    {
                        //Call A Method Defined In Another Class
                    }
                    if (btnUpTime.isClicked == true)
                    {
                        //Call A Method Defined In Another Class
                    }
                    if (btnMoreUp.isClicked == true)
                    {
                        intUpTime++;
                        btnMoreUp.isClicked = false;
                        Thread.Sleep(50);
                        //Call A Method Defined In Another Class
                    }
                    if (btnLessUp.isClicked == true)
                    {
                        intUpTime--;
                        btnLessUp.isClicked = false;
                        Thread.Sleep(50);
                        //Call A Method Defined In Another Class
                    }
                    if (btnMoreHold.isClicked == true)
                    {
                        intHoldTime++;
                        btnMoreHold.isClicked = false;
                        Thread.Sleep(50);
                        //Call A Method Defined In Another Class
                    }
                    if (btnLessHold.isClicked == true)
                    {
                        intHoldTime--;
                        btnLessHold.isClicked = false;
                        Thread.Sleep(50);
                        //Call A Method Defined In Another Class
                    }
                    if (btnLemBack.isClicked == true) {
                        levelEditorMenuON = false;
                        btnLemBack.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (btnLemSave.isClicked == true) {
                        levelEditorMenuON = false;
                        btnLemSave.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (btnLemLoad.isClicked == true) {
                        levelEditorMenuON = false;
                        btnLemLoad.isClicked = false;
                        Thread.Sleep(50);
                        CurrentGameState = GameState.LoadLevelScreen;
                    }
                    if (btnLemClear.isClicked == true) {
                        levelEditorMenuON = false;
                        btnLemClear.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (btnLemExit.isClicked == true) {
                        levelEditorMenuON = false;
                        this.Exit();
                    }
                    if (btnAddLetter.isClicked == true)
                    {
                        showingAlpha = true;
                        showingNumbers = false;
                        showingShapes = false;
                        btnAddLetter.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (btnAddNumber.isClicked == true)
                    {
                        showingNumbers = true;
                        showingAlpha = false;
                        showingShapes = false;
                        btnAddNumber.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (btnAddShape.isClicked == true)
                    {
                        showingShapes = true;
                        showingAlpha = false;
                        showingNumbers = false;
                        btnAddShape.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (btnHidePallet.isClicked == true)
                    {
                        if (shapePalletVisible){shapePalletVisible = false;}
                        else { shapePalletVisible = true; }
                        btnHidePallet.isClicked = false;
                        Thread.Sleep(50);

                    }
                    if (btnThumbCircle.isClicked == true)
                    {
                        showingShapes = false;
                        sizeChoosing = true;
                        btnThumbCircle.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (btnThumbSquare.isClicked == true)
                    {
                        showingShapes = false;
                        sizeChoosing = true;
                        btnThumbSquare.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (btnThumbStar.isClicked == true)
                    {
                        showingShapes = false;
                        sizeChoosing = true;
                        btnThumbStar.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (btnThumbTriangle.isClicked == true)
                    {
                        showingShapes = false;
                        sizeChoosing = true;
                        btnThumbTriangle.isClicked = false;
                        Thread.Sleep(50);
                    }
                    if (sizeTiny.isClicked == true)
                    {
                        showingShapes = false;
                        sizeChoosing = false;
                        colorChoosing = true;
                        sizeTiny.isClicked = false;
                        Thread.Sleep(1500);
                    }
                    if (sizeSmall.isClicked == true)
                    {
                        showingShapes = false;
                        sizeChoosing = false;
                        colorChoosing = true;
                        sizeSmall.isClicked = false;
                        Thread.Sleep(500);
                    }
                    if (sizeMedium.isClicked == true)
                    {
                        showingShapes = false;
                        sizeChoosing = false;
                        colorChoosing = true;
                        sizeMedium.isClicked = false;
                        Thread.Sleep(500);
                    }
                    if (sizeLarge.isClicked == true)
                    {
                        showingShapes = false;
                        sizeChoosing = false;
                        colorChoosing = true;
                        sizeLarge.isClicked = false;
                        Thread.Sleep(500);
                    }
                    if (sizeXLarge.isClicked == true)
                    {
                        showingShapes = false;
                        sizeChoosing = false;
                        colorChoosing = true;
                        sizeXLarge.isClicked = false;
                        Thread.Sleep(500);
                    }
                    if (colorBlackBtn.isClicked == true)
                    {
                        colorChoosing = false;
                        showingShapes = true;
                        colorBlackBtn.isClicked = false;
                        Thread.Sleep(500);
                    }
                    if (colorBlueBtn.isClicked == true)
                    {
                        colorChoosing = false;
                        showingShapes = true;
                        colorBlueBtn.isClicked = false;
                        Thread.Sleep(500);
                    }
                    if (colorDarkBlueBtn.isClicked == true)
                    {
                        colorChoosing = false;
                        showingShapes = true;
                        colorDarkBlueBtn.isClicked = false;
                        Thread.Sleep(500);
                    }
                    if (colorDarkGreyBtn.isClicked == true)
                    {
                        colorChoosing = false;
                        showingShapes = true;
                        colorDarkGreyBtn.isClicked = false;
                        Thread.Sleep(500);
                    }
                    if (colorGreenBtn.isClicked == true)
                    {
                        colorChoosing = false;
                        showingShapes = true;
                        colorGreenBtn.isClicked = false;
                        Thread.Sleep(500);
                    }
                    if (colorGreyBtn.isClicked == true)
                    {
                        colorChoosing = false;
                        showingShapes = true;
                        colorGreyBtn.isClicked = false;
                        Thread.Sleep(500);
                    }
                    if (colorLightBlueBtn.isClicked == true)
                    {
                        colorChoosing = false;
                        showingShapes = true;
                        colorLightBlueBtn.isClicked = false;
                        Thread.Sleep(500);
                    }
                    if (colorLightGreen.isClicked == true)
                    {
                        colorChoosing = false;
                        showingShapes = true;
                        colorLightGreen.isClicked = false;
                        Thread.Sleep(500);
                    }
                    if (colorOrangeBtn.isClicked == true)
                    {
                        colorChoosing = false;
                        showingShapes = true;
                        colorOrangeBtn.isClicked = false;
                        Thread.Sleep(500);
                    }
                    if (colorPinkBtn.isClicked == true)
                    {
                        colorChoosing = false;
                        showingShapes = true;
                        colorPinkBtn.isClicked = false;
                        Thread.Sleep(500);
                    }
                    if (colorRedBtn.isClicked == true)
                    {
                        colorChoosing = false;
                        showingShapes = true;
                        colorRedBtn.isClicked = false;
                        Thread.Sleep(500);
                    }
                    if (colorYellowBtn.isClicked == true)
                    {
                        colorChoosing = false;
                        showingShapes = true;
                        colorYellowBtn.isClicked = false;
                        Thread.Sleep(500);
                    }
                    UpdateLevelEditorScreen(gameTime, mouse);
                    break;

                    //update if playing PATIENT GAME
                case GameState.PatientGame:
                    if (btnBack.isClicked == true)
                    {
                        btnBack.isClicked = false;
                        pauseMenuON = true;
                        Thread.Sleep(50);
                        btnBack.Update(mouse);
                    }
                    if (btnPauseContinue.isClicked == true)
                    {
                        pauseMenuON = false;
                    }
                    if (btnPauseEdit.isClicked == true)
                    {
                        pauseMenuON = false;
                        CurrentGameState = GameState.LevelEditor;
                    }
                    if (btnPauseLoad.isClicked == true)
                    {
                        pauseMenuON = false;
                        CurrentGameState = GameState.LoadLevelScreen;
                    }
                    if (btnPauseRestart.isClicked == true)
                    {
                        pauseMenuON = false;
                    }
                    UpdateGameScreen(gameTime, mouse);
                    break;
            }
            // TODO: Add your update logic here
           // UpdateSprite(gameTime); //example code
            base.Update(gameTime);
        }
        

        /// <summary>
        /// This is called when the game should DRAW itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        //DRAW GAME HERE//
        protected override void Draw(GameTime gameTime) 
        {
            GraphicsDevice.Clear(Color.White);
            // TODO: Add your drawing code here
            //spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.Begin();
            spriteBatch.Draw(myTopHeaderBkGround, myTopHeaderPosition, Color.White); //example code
            switch (CurrentGameState)
            {
                case GameState.HomeScreen: //Draw all elements for Home Screen
                    DrawHomeScreen(gameTime, spriteBatch);
                    break;
                case GameState.NewLevelScreen: //Draw all elements for NewLevel Screen
                    DrawNewLayoutScreen(gameTime, spriteBatch);
                    break;
                case GameState.LoadLevelScreen: //Draw all elements for Loadlevel Screen
                    DrawLoadLayoutScreen(gameTime, spriteBatch);
                    break;
                case GameState.LevelEditor:
                    DrawLevelEdtorScreen(gameTime, spriteBatch);
                    break;
                case GameState.PatientGame:
                    DrawGameScreen(gameTime, spriteBatch);
                    break;

            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        ///////////////////////////////////////////helper methods below/////////////////////////////////////////////////////
        void DrawLevelEdtorScreen(GameTime gameTime, SpriteBatch spriteBatch)
        {
            btnHome.Draw(spriteBatch);
            btnMenu.Draw(spriteBatch);
            btnHoldTime.Draw(spriteBatch);
            btnMultiple.Draw(spriteBatch);
            btnPlay.Draw(spriteBatch);
            btnRedo.Draw(spriteBatch);
            btnUndo.Draw(spriteBatch);
            btnUpTime.Draw(spriteBatch);
            btnMoreUp.Draw(spriteBatch);
            btnMoreHold.Draw(spriteBatch);
            btnLessUp.Draw(spriteBatch);
            btnLessHold.Draw(spriteBatch);
            originForRotation.X = 960;//myGrid.Width / 2;
            originForRotation.Y = 485;//(myGrid.Height + 110) / 2;
            spriteBatch.Draw(myGrid, myGridPosition, null, Color.White, rotationAngle, originForRotation, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, intHoldTime.ToString(), intHoldTimePosition, Color.Black);
            spriteBatch.DrawString(font, intUpTime.ToString(), intUpTimePosition, Color.Black);
            if (levelEditorMenuON)
            {
                spriteBatch.Draw(levelEditorMenuBackground, levelEditorMenuBackgroundPosition, Color.White);
                spriteBatch.Draw(levelEditorMenuTitle, levelEditorMenuGraphicPosition, Color.White);
                btnLemBack.Draw(spriteBatch);
                btnLemClear.Draw(spriteBatch);
                btnLemExit.Draw(spriteBatch);
                btnLemLoad.Draw(spriteBatch);
                btnLemSave.Draw(spriteBatch);
            }
            if (shapePalletVisible)
            {
                spriteBatch.Draw(shapePalletBackground, shapePalletBackgroundPosition, Color.White);
                btnHidePallet.Draw(spriteBatch);
                if (showingShapes)
                {
                    btnThumbTriangle.Draw(spriteBatch);
                    btnThumbStar.Draw(spriteBatch);
                    btnThumbSquare.Draw(spriteBatch);
                    btnThumbCircle.Draw(spriteBatch);
                    btnAddNumber.setPosition(new Vector2(shapePalletX + 15, shapePalletY + 500));
                    btnAddLetter.Draw(spriteBatch);
                    btnAddNumber.Draw(spriteBatch);
                }
                if (showingAlpha) {
                    btnAddNumber.Draw(spriteBatch);
                    btnAddNumber.setPosition(new Vector2(shapePalletX + 15, shapePalletY + 550));
                    btnAddShape.Draw(spriteBatch);
                    putA.Draw(spriteBatch);
                    putB.Draw(spriteBatch);
                    putC.Draw(spriteBatch);
                    putD.Draw(spriteBatch);
                    putE.Draw(spriteBatch);
                    putF.Draw(spriteBatch);
                    putG.Draw(spriteBatch);
                    putH.Draw(spriteBatch);
                    putI.Draw(spriteBatch);
                    putJ.Draw(spriteBatch);
                    putK.Draw(spriteBatch);
                    putL.Draw(spriteBatch);
                    putM.Draw(spriteBatch);
                    putN.Draw(spriteBatch);
                    putO.Draw(spriteBatch);
                    putP.Draw(spriteBatch);
                    putQ.Draw(spriteBatch);
                    putR.Draw(spriteBatch);
                    putS.Draw(spriteBatch);
                    putT.Draw(spriteBatch);
                    putU.Draw(spriteBatch);
                    putV.Draw(spriteBatch);
                    putW.Draw(spriteBatch);
                    putX.Draw(spriteBatch);
                    putY.Draw(spriteBatch);
                    putZ.Draw(spriteBatch);
                   
                }//Fill this in.
                if (showingNumbers) {
                    put1.Draw(spriteBatch);
                    put2.Draw(spriteBatch);
                    put3.Draw(spriteBatch);
                    put4.Draw(spriteBatch);
                    put5.Draw(spriteBatch);
                    put6.Draw(spriteBatch);
                    put7.Draw(spriteBatch);
                    put8.Draw(spriteBatch);
                    put9.Draw(spriteBatch);
                    put0.Draw(spriteBatch);
                    btnAddLetter.Draw(spriteBatch);
                    btnAddShape.Draw(spriteBatch);
                }///Fill this in.
                
                
            }
            else { btnHidePallet.Draw(spriteBatch); }
            if (sizeChoosing)
            {
                spriteBatch.Draw(shapePalletBackground, shapePalletBackgroundPosition, Color.White);
                spriteBatch.Draw(chooseSize, chooseSizePosition, Color.White);
                sizeTiny.Draw(spriteBatch);
                sizeSmall.Draw(spriteBatch);
                sizeMedium.Draw(spriteBatch);
                sizeLarge.Draw(spriteBatch);
                sizeXLarge.Draw(spriteBatch);
            }
            if (colorChoosing)
            {
                spriteBatch.Draw(shapePalletBackground, shapePalletBackgroundPosition, Color.White);
                colorBlackBtn.Draw(spriteBatch);
                colorBlueBtn.Draw(spriteBatch);
                colorDarkBlueBtn.Draw(spriteBatch);
                colorDarkGreyBtn.Draw(spriteBatch);
                colorGreenBtn.Draw(spriteBatch);
                colorGreyBtn.Draw(spriteBatch);
                colorLightBlueBtn.Draw(spriteBatch);
                colorLightGreen.Draw(spriteBatch);
                colorOrangeBtn.Draw(spriteBatch);
                colorPinkBtn.Draw(spriteBatch);
                colorRedBtn.Draw(spriteBatch);
                colorYellowBtn.Draw(spriteBatch);
            }

        }
        void DrawGameScreen(GameTime gameTime, SpriteBatch spriteBatch)
        {
            btnBack.Draw(spriteBatch);
            if (pauseMenuON)
            {
                spriteBatch.Draw(pauseMenuBackground, pauseMenuBackgroundPosition, Color.White);
                spriteBatch.Draw(pauseMenuTitle, pauseMenuGraphicPosition, Color.White);
                btnPauseRestart.Draw(spriteBatch);
                btnPauseLoad.Draw(spriteBatch);
                btnPauseEdit.Draw(spriteBatch);
                btnPauseContinue.Draw(spriteBatch); 
            }
        }

        void DrawHomeScreen (GameTime gameTime, SpriteBatch spriteBatch) {
            btnNew.Draw(spriteBatch);
            btnLoad.Draw(spriteBatch);
            btnExit.Draw(spriteBatch);
            spriteBatch.Draw(myTitle, myTitlePosition, Color.White);
        
        }
        void DrawNewLayoutScreen(GameTime gameTime, SpriteBatch spriteBatch)
        {
            btnCancel.Draw(spriteBatch);
            btnCreate.Draw(spriteBatch);
            spriteBatch.Draw(textBackgorund, nameBackgroundPosition, Color.White);
            clearDescriptionButton.Draw(spriteBatch);
            clearNameButton.Draw(spriteBatch);
            spriteBatch.Draw(myNewLevelTitle, myNewLevelTitlePosition, Color.White);
            if (nameHighlight == true) { spriteBatch.DrawString(font, nameOfTherapist, nameOfTherapistPosition, Color.Black); }
            else { spriteBatch.DrawString(font, nameOfTherapist, nameOfTherapistPosition, Color.DarkGray); }

            if (nameHighlight == false) { spriteBatch.DrawString(font, descriptionByTherapist, descriptionByTherapistPosition, Color.Black); }
            else { spriteBatch.DrawString(font, descriptionByTherapist, descriptionByTherapistPosition, Color.DarkGray); }

            spriteBatch.Draw(myName, myNamePosition, Color.White);
            spriteBatch.Draw(myDescription, myDescriptionPosition, Color.White);
            spriteBatch.Draw(myOSKBackground, myOSKBackgroundPosition, Color.White);
            delName.Draw(spriteBatch);
            delDesc.Draw(spriteBatch);
            a.Draw(spriteBatch);
            b.Draw(spriteBatch);
            c.Draw(spriteBatch);
            d.Draw(spriteBatch);
            e.Draw(spriteBatch);
            f.Draw(spriteBatch);
            g.Draw(spriteBatch);
            h.Draw(spriteBatch);
            i.Draw(spriteBatch);
            j.Draw(spriteBatch);
            k.Draw(spriteBatch);
            l.Draw(spriteBatch);
            m.Draw(spriteBatch);
            n.Draw(spriteBatch);
            o.Draw(spriteBatch);
            p.Draw(spriteBatch);
            q.Draw(spriteBatch);
            r.Draw(spriteBatch);
            s.Draw(spriteBatch);
            t.Draw(spriteBatch);
            u.Draw(spriteBatch);
            v.Draw(spriteBatch);
            w.Draw(spriteBatch);
            x.Draw(spriteBatch);
            y.Draw(spriteBatch);
            z.Draw(spriteBatch);
            space1.Draw(spriteBatch);
            space2.Draw(spriteBatch);
            delete1.Draw(spriteBatch);
            clear.Draw(spriteBatch);

        }
        void DrawLoadLayoutScreen(GameTime gameTime, SpriteBatch spriteBatch)
        {
            btnCancel.Draw(spriteBatch);
            btnOpen.Draw(spriteBatch);
            spriteBatch.Draw(myLoadLevelTitle, myLoadLevelTitlePosition, Color.White);
            goSearch.Draw(spriteBatch);

            if (loadKeyBoard == false) 
            { 
                spriteBatch.DrawString(font, searchQuery, searchQueryPosition, Color.White); 
            }else
            {
                spriteBatch.DrawString(font, searchQuery, searchQueryPosition, Color.Black);
            }
            spriteBatch.Draw(magnifyGlass, magnifyGlassPosition, Color.White);
            spriteBatch.Draw(listBackground, lisBackgroundPosition, Color.White);
            delSearch.Draw(spriteBatch);

            if (loadKeyBoard == true)
            {
                spriteBatch.Draw(myOSKBackground, myOSKBackgroundPosition, Color.White);
                a.Draw(spriteBatch);
                b.Draw(spriteBatch);
                c.Draw(spriteBatch);
                d.Draw(spriteBatch);
                e.Draw(spriteBatch);
                f.Draw(spriteBatch);
                g.Draw(spriteBatch);
                h.Draw(spriteBatch);
                i.Draw(spriteBatch);
                j.Draw(spriteBatch);
                k.Draw(spriteBatch);
                l.Draw(spriteBatch);
                m.Draw(spriteBatch);
                n.Draw(spriteBatch);
                o.Draw(spriteBatch);
                p.Draw(spriteBatch);
                q.Draw(spriteBatch);
                r.Draw(spriteBatch);
                s.Draw(spriteBatch);
                t.Draw(spriteBatch);
                u.Draw(spriteBatch);
                v.Draw(spriteBatch);
                w.Draw(spriteBatch);
                x.Draw(spriteBatch);
                y.Draw(spriteBatch);
                z.Draw(spriteBatch);
                space1.Draw(spriteBatch);
                space2.Draw(spriteBatch);
                delete1.Draw(spriteBatch);
                clear.Draw(spriteBatch);
            }

        }

        void UpdateHomeScreen(GameTime gameTime, MouseState mouse)
        {
            btnNew.Update(mouse);
            btnLoad.Update(mouse);
            btnExit.Update(mouse);
        }
        void UpdateGameScreen(GameTime gameTime, MouseState mouse)
        {
            btnBack.Update(mouse);
            btnPauseContinue.Update(mouse);
            btnPauseEdit.Update(mouse);
            btnPauseLoad.Update(mouse);
            btnPauseRestart.Update(mouse);
        }

        void UpdateLevelEditorScreen(GameTime gameTime, MouseState mouse)
        {
            //update level editior
            btnHome.Update(mouse);
            btnMenu.Update(mouse);
            btnHoldTime.Update(mouse);
            btnMultiple.Update(mouse);
            btnPlay.Update(mouse);
            btnRedo.Update(mouse);
            btnUndo.Update(mouse);
            btnUpTime.Update(mouse);
            btnMoreHold.Update(mouse);
            btnMoreUp.Update(mouse);
            btnLessHold.Update(mouse);
            btnLessUp.Update(mouse);
            //end of update level editor
            btnLemSave.Update(mouse);
            btnLemLoad.Update(mouse);
            btnLemExit.Update(mouse);
            btnLemClear.Update(mouse);
            btnLemBack.Update(mouse);
            
            btnHidePallet.Update(mouse);
            if (showingShapes)
            {
                btnThumbTriangle.Update(mouse);
                btnThumbStar.Update(mouse);
                btnThumbSquare.Update(mouse);
                btnThumbCircle.Update(mouse);
                btnAddLetter.Update(mouse);
                btnAddNumber.Update(mouse);
                btnAddLetter.Update(mouse);
                btnAddNumber.Update(mouse);
            }
            if (showingAlpha)
            {
                btnAddNumber.Update(mouse);
                btnAddShape.Update(mouse);
                putA.Update(mouse);
                putB.Update(mouse);
                putC.Update(mouse);
                putD.Update(mouse);
                putE.Update(mouse);
                putF.Update(mouse);
                putG.Update(mouse);
                putH.Update(mouse);
                putI.Update(mouse);
                putJ.Update(mouse);
                putK.Update(mouse);
                putL.Update(mouse);
                putM.Update(mouse);
                putN.Update(mouse);
                putO.Update(mouse);
                putP.Update(mouse);
                putQ.Update(mouse);
                putR.Update(mouse);
                putS.Update(mouse);
                putT.Update(mouse);
                putU.Update(mouse);
                putV.Update(mouse);
                putW.Update(mouse);
                putX.Update(mouse);
                putY.Update(mouse);
                putZ.Update(mouse);
            }
            if (showingNumbers)
            {
                put1.Update(mouse);
                put2.Update(mouse);
                put3.Update(mouse);
                put4.Update(mouse);
                put5.Update(mouse);
                put6.Update(mouse);
                put7.Update(mouse);
                put8.Update(mouse);
                put9.Update(mouse);
                put0.Update(mouse);
                btnAddLetter.Update(mouse);
                btnAddShape.Update(mouse);
            }
            
          
            if (sizeChoosing)
            {
                sizeTiny.Update(mouse);
                sizeSmall.Update(mouse);
                sizeMedium.Update(mouse);
                sizeLarge.Update(mouse);
                sizeXLarge.Update(mouse);
            }
            if (colorChoosing)
            {
                colorBlackBtn.Update(mouse);
                colorBlueBtn.Update(mouse);
                colorDarkBlueBtn.Update(mouse);
                colorDarkGreyBtn.Update(mouse);
                colorGreenBtn.Update(mouse);
                colorGreyBtn.Update(mouse);
                colorLightBlueBtn.Update(mouse);
                colorLightGreen.Update(mouse);
                colorOrangeBtn.Update(mouse);
                colorPinkBtn.Update(mouse);
                colorRedBtn.Update(mouse);
                colorYellowBtn.Update(mouse);
            }
        }

        void UpdateLoadLevelScreen(GameTime gameTime, MouseState mouse)
        {
            // update load screen
            btnCancel.Update(mouse);
            btnOpen.Update(mouse);
            delSearch.Update(mouse);
            clearSearchButton.Update(mouse);
            goSearch.Update(mouse);
            if (loadKeyBoard == true)
            {
                a.Update(mouse);
                b.Update(mouse);
                c.Update(mouse);
                d.Update(mouse);
                e.Update(mouse);
                f.Update(mouse);
                g.Update(mouse);
                h.Update(mouse);
                i.Update(mouse);
                j.Update(mouse);
                k.Update(mouse);
                l.Update(mouse);
                m.Update(mouse);
                n.Update(mouse);
                o.Update(mouse);
                p.Update(mouse);
                q.Update(mouse);
                r.Update(mouse);
                s.Update(mouse);
                t.Update(mouse);
                u.Update(mouse);
                v.Update(mouse);
                w.Update(mouse);
                x.Update(mouse);
                y.Update(mouse);
                z.Update(mouse);
                space1.Update(mouse);
                space2.Update(mouse);
                delete1.Update(mouse);
                clear.Update(mouse);
            }
            // end of update load screen
        }

        void UpdateNewLevelScreen(GameTime gameTime, MouseState mouse)
        {
            
            btnCancel.Update(mouse);
            btnCreate.Update(mouse);
            clearNameButton.Update(mouse);
            clearDescriptionButton.Update(mouse);
            a.Update(mouse);
            b.Update(mouse);
            c.Update(mouse);
            d.Update(mouse);
            e.Update(mouse);
            f.Update(mouse);
            g.Update(mouse);
            h.Update(mouse);
            i.Update(mouse);
            j.Update(mouse);
            k.Update(mouse);
            l.Update(mouse);
            m.Update(mouse);
            n.Update(mouse);
            o.Update(mouse);
            p.Update(mouse);
            q.Update(mouse);
            r.Update(mouse);
            s.Update(mouse);
            t.Update(mouse);
            u.Update(mouse);
            v.Update(mouse);
            w.Update(mouse);
            x.Update(mouse);
            y.Update(mouse);
            z.Update(mouse);
            space1.Update(mouse);
            space2.Update(mouse);
            delete1.Update(mouse);
            clear.Update(mouse);
            delName.Update(mouse);
            delDesc.Update(mouse);
        }
    }
}
