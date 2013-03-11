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
using GameLibrary;

namespace FirstGame
{
    /// <summary>
    /// This is the main type for our target tapping game. 
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

        //These booleans are used to specify states and what GUI elements should be presented
        bool multiState = false; //for multi button 
        bool levelEditorMenuON = false; //Menu opens with menu button in level editor
        bool pauseMenuON = false; //Menu opens with pause button in game play
        bool shapePalletVisible = true; // Showing the pallet
        bool sizeChoosing = false; // Choosing the size of an object in the pallet
        bool colorChoosing = false; // Color choosing for an object in the pallet
        bool showingShapes = true; //Choosing between shapes in the pallet
        bool showingNumbers = false; //Choosing between numerical object in the pallet
        bool showingAlpha = false; //CHoosing alpha objets in the pallet
        //bool numberChoosing = false; // Unused
        //bool alphaChoosing = true; // Unused

        //Initializing Graphical Elements and set there positions. This includes labels etc.
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
        
        //Shape Pallet Elements, this includes buttons and labels.
        int shapePalletX = 0, shapePalletY = 100;
        Texture2D shapePalletBackground, chooseSize, chooseColor;
        Vector2 shapePalletBackgroundPosition, chooseSizePosition, chooseColorPosition;
        Button btnHidePallet, btnAddLetter, btnAddNumber, btnAddShape;
        Button sizeTiny, sizeSmall, sizeMedium, sizeLarge, sizeXLarge;
        Button btnThumbSquare, btnThumbCircle, btnThumbStar, btnThumbTriangle;
        Button colorGreenBtn, colorGreyBtn, colorDarkGreyBtn, colorBlueBtn, colorBlackBtn, colorRedBtn, colorOrangeBtn;
        Button colorYellowBtn, colorLightBlueBtn, colorDarkBlueBtn, colorPinkBtn, colorLightGreen;
        Button putA, putB, putC, putD, putE, putF, putG, putH, putI, putJ, putK, putL, putM, putN, putO, putP, putQ;
        Button putR, putS, putT, putU, putV, putW, putX, putY, putZ, put1, put2, put3, put4, put5, put6, put7, put8, put9;
        Button put0;
        

        //Initialize Button Elements (There are different Sizes of Buttons)
        Button btnNew, btnLoad, btnExit;
        Button btnCancel, btnCreate, btnOpen, btnBack;
        Button btnUpTime, btnHoldTime;
        Button btnHome, btnMenu, btnMultiple, btnPlay, btnRedo, btnUndo, btnMoreUp, btnLessUp, btnMoreHold, btnLessHold;
        Button a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z, space1, space2, delete1, clear;
        Button clearNameButton, clearDescriptionButton, clearSearchButton;
        Button delName, delDesc, delSearch, goSearch;
        int intUpTime, intHoldTime;
        Vector2 intUpTimePosition = (new Vector2(520, 45));
        Vector2 intHoldTimePosition = (new Vector2(785, 45));
        Button btnLemBack, btnLemClear, btnLemExit, btnLemLoad, btnLemSave;
        Button btnPauseLoad, btnPauseRestart, btnPauseEdit, btnPauseContinue;
    
        //CREATE GAME CONSTRUCTOR//
        /// <summary>
        /// This is out constructor.
        /// 
        /// 
        /// </summary>
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
            // This has not been used by us yet. This methods comes with the template for an xna game.

            base.Initialize();
        }




        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content. Here we load the graphical elements into label sprites and buttons.
        /// </summary>
        //LOAD CONTENT HERE//
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Header on all screens
            myTopHeaderBkGround = Content.Load<Texture2D>("GUI/topHeaderBkGround"); 

            //Sprite font loading
            font = Content.Load<SpriteFont>("font");
            nameOfTherapist = "Enter Your Name Here...";
            descriptionByTherapist = "Enter Your Description Here...";
            searchQuery = "Search...";
            textBackgorund = Content.Load<Texture2D>("GUI/textBackground");

            //INTs for uptime and holdtime, these will dictate times for game.
            //Must be changed when a game is loaded!!!!!!
            intUpTime = 5;
            intHoldTime = 1;

            //Home Screen Elements - these graphics and buttons make up the home screen
            myTitle = Content.Load<Texture2D>("GUI/targetTappingGame");
            btnNew = makeButton(340, 200, "GUI/newButton");
            btnLoad = makeButton(340, 350, "GUI/loadButton");
            btnExit = makeButton(340, 500, "GUI/exitButton");
            
            //NewLevel Screen Elements - these graphics and buttons make up the create a new level screen, OSK defined here..
            delDesc = makeButton(0, 0, "Gui/miniX");
            delDesc.Position = new Point(500, 200);
            delName = makeButton(0, 0, "Gui/miniX");
            delName.Position = new Point(500, 130);
            btnCancel = makeButton(0, 0, "GUI/cancel");
            myNewLevelTitle = Content.Load<Texture2D>("GUI/newLevel");
            myNewLevel = Content.Load<Texture2D>("GUI/newLevel");
            btnCreate = makeButton(0, 0, "GUI/createButton");
            btnCreate.Position = new Point(1160, 0);
            myName = Content.Load<Texture2D>("GUI/name");
            clearNameButton = makeButton(0, 0, "GUI/nothing");
            clearNameButton.Position = new Point(0, 130);
            clearDescriptionButton = makeButton(0, 0, "GUI/nothing");
            clearDescriptionButton.Position = new Point(0, 200);
            myDescription = Content.Load<Texture2D>("GUI/description");
            myOSKBackground = Content.Load<Texture2D>("OSK/keyboardBackground");
            int keyStartingX = 401;
            int keyStartingY = 520;
            a = makeButton(0, 0, "OSK/aButton");
            a.Position = new Point(keyStartingX+10, keyStartingY+50);
            b = makeButton(0, 0, "OSK/bButton");
            b.Position = new Point(keyStartingX+230, keyStartingY+100);
            c = makeButton(0, 0, "OSK/cButton");
            c.Position = new Point(keyStartingX+130, keyStartingY+100);
            d = makeButton(0, 0, "OSK/dButton");
            d.Position = new Point(keyStartingX+110, keyStartingY+50);
            e = makeButton(0, 0, "OSK/eButton");
            e.Position = new Point(keyStartingX+100, keyStartingY);
            f = makeButton(0, 0, "OSK/fButton");
            f.Position = new Point(keyStartingX+160, keyStartingY+50);
            g = makeButton(0, 0, "OSK/gButton");
            g.Position = new Point(keyStartingX+210, keyStartingY+50);
            h = makeButton(0, 0, "OSK/hButton");
            h.Position = new Point(keyStartingX+260, keyStartingY+50);
            i = makeButton(0, 0, "OSK/iButton");
            i.Position = new Point(keyStartingX+350, keyStartingY);
            j = makeButton(0, 0, "OSK/jButton");
            j.Position = new Point(keyStartingX+310, keyStartingY+50);
            k = makeButton(0, 0, "OSK/kButton");
            k.Position = new Point(keyStartingX+360, keyStartingY+50);
            l = makeButton(0, 0, "OSK/lButton");
            l.Position = new Point(keyStartingX+410, keyStartingY+50);
            m = makeButton(0, 0, "OSK/mButton");
            m.Position = new Point(keyStartingX+330, keyStartingY+100);
            n = makeButton(0, 0, "OSK/nButton");
            n.Position = new Point(keyStartingX+280, keyStartingY+100);
            o = makeButton(0, 0, "OSK/oButton");
            o.Position = new Point(keyStartingX+400, keyStartingY);
            p = makeButton(0, 0, "OSK/pButton");
            p.Position = new Point(keyStartingX+450, keyStartingY);
            q = makeButton(0, 0, "OSK/qButton");
            q.Position = new Point(keyStartingX, keyStartingY);
            r = makeButton(0, 0, "OSK/rButton");
            r.Position = new Point(keyStartingX+150, keyStartingY);
            s = makeButton(0, 0, "OSK/sButton");
            s.Position = new Point(keyStartingX+60, keyStartingY+50);
            t = makeButton(0, 0, "OSK/tButton");
            t.Position = new Point(keyStartingX+200, keyStartingY);
            u = makeButton(0, 0, "OSK/uButton");
            u.Position = new Point(keyStartingX+300, keyStartingY);
            v = makeButton(0, 0, "OSK/vButton");
            v.Position = new Point(keyStartingX+180, keyStartingY+100);
            w = makeButton(0, 0, "OSK/wButton");
            w.Position = new Point(keyStartingX+50, keyStartingY);
            x = makeButton(0, 0, "OSK/xButton");
            x.Position = new Point(keyStartingX+80, keyStartingY+100);
            y = makeButton(0, 0, "OSK/yButton");
            y.Position = new Point(keyStartingX+250, keyStartingY);
            z = makeButton(0, 0, "OSK/zButton");
            z.Position = new Point(keyStartingX+30, keyStartingY+100);
            space1 = makeButton(0, 0, "OSK/spaceButton");
            space1.Position = new Point(keyStartingX + 200, keyStartingY + 150);
            space2 = makeButton(0, 0, "OSK/spaceButton");
            space2.Position = new Point(keyStartingX + 250, keyStartingY + 150);
            delete1 = makeButton(0, 0, "OSK/deleteButton");
            delete1.Position = new Point(keyStartingX + 400, keyStartingY + 100);
            clear = makeButton(0, 0, "OSK/clear");
            clear.Position = new Point(keyStartingX + 400, keyStartingY + 150);

            //LoadGame Screen Elements - these buttons and graphics make up the load screen, OSK is defined above
            magnifyGlass = Content.Load<Texture2D>("GUI/magnifyGlass");
            listBackground = Content.Load<Texture2D>("GUI/listBackground");
            myLoadLevelTitle = Content.Load<Texture2D>("GUI/loadGameTitle");
            btnOpen = makeButton(0, 0, "GUI/openButton");
            btnOpen.Position = new Point(1160, 0);
            delSearch = makeButton(0, 0, "Gui/miniX");
            delSearch.Position = new Point(400, 85);
            goSearch = makeButton(0, 0, "Gui/go");
            goSearch.Position = new Point(425, 85);
            clearSearchButton = makeButton(0, 0, "GUI/nothing");
            clearSearchButton.Position = new Point(175, 85);

            //Level Editor Elements - these buttons and graphics make up the level editor screen.
            btnHome = makeButton(0, 0, "LevelEditorGUI/homeButton");
            btnHome.Position = new Point(30, 30);
            btnMenu = makeButton(0, 0, "LevelEditorGUI/menuButton");
            btnMenu.Position = new Point(95, 30);
            btnHoldTime = makeButton(0, 0, "LevelEditorGUI/holdTimeButton");
            btnHoldTime.Position = new Point(700, 30);
            btnMultiple = makeButton(0, 0, "LevelEditorGUI/multipleToggleOff");
            btnMultiple.Position = new Point(355, 30);
            btnPlay = makeButton(0, 0, "LevelEditorGUI/playButton");
            btnPlay.Position = new Point(290, 30);
            btnRedo = makeButton(0, 0, "LevelEditorGUI/redoButton");
            btnRedo.Position = new Point(215, 30);
            btnUndo = makeButton(0, 0, "LevelEditorGUI/undoButton");
            btnUndo.Position = new Point(160, 30);
            btnUpTime = makeButton(0, 0, "LevelEditorGUI/upTimeButton");
            btnUpTime.Position = new Point(440, 30);
            btnMoreUp = makeButton(0, 0, "LevelEditorGUI/moreButton");
            btnMoreUp.Position = new Point(560, 30);
            btnMoreHold = makeButton(0, 0, "LevelEditorGUI/moreButton");
            btnMoreHold.Position = new Point(820, 30);
            btnLessUp = makeButton(0, 0, "LevelEditorGUI/lessButton");
            btnLessUp.Position = new Point(615, 30);
            btnLessHold = makeButton(0, 0, "LevelEditorGUI/lessButton");
            btnLessHold.Position = new Point(875, 30);
            myGrid = Content.Load<Texture2D>("LevelEditorGUI/placementGrid");
            levelEditorMenuBackground = Content.Load<Texture2D>("LevelEditorMenu/menuBackground");
            levelEditorMenuTitle = Content.Load<Texture2D>("LevelEditorMenu/levelEditorMenuGraphic");
            //Vector2 levelEditorMenuGraphicPosition = (new Vector2(630, 300));
            btnLemBack = makeButton(0, 0, "LevelEditorMenu/backButtonGraphic");
            btnLemBack.Position = new Point(630, 355);
            btnLemSave = makeButton(0, 0, "LevelEditorMenu/saveButtonGraphic");
            btnLemSave.Position = new Point(630, 410);
            btnLemLoad = makeButton(0, 0, "LevelEditorMenu/loadButtonGraphic");
            btnLemLoad.Position = new Point(630, 465);
            btnLemClear = makeButton(0, 0, "LevelEditorMenu/clearButtonGraphic");
            btnLemClear.Position = new Point(630, 520);
            btnLemExit = makeButton(0, 0, "LevelEditorMenu/exitButtonGraphic");
            btnLemExit.Position = new Point(630, 575);
            //Shape Pallet in editor screen// Conent loaded below.
            shapePalletBackground = Content.Load<Texture2D>("ShapePallet/shapePalletBackground");
            shapePalletBackgroundPosition = (new Vector2(shapePalletX, shapePalletY));
            btnHidePallet = makeButton(0, 0, "ShapePallet/hidePallet");
            btnHidePallet.Position = new Point(shapePalletX+0, shapePalletY);
            btnThumbCircle = makeButton(0, 0, "ShapePallet/demoCircle");
            btnThumbCircle.Position = new Point(shapePalletX + 20, shapePalletY+60);
            btnThumbSquare = makeButton(0, 0, "ShapePallet/demoSquare");
            btnThumbSquare.Position = new Point(shapePalletX + 20, shapePalletY+160);
            btnThumbStar= makeButton(0, 0, "ShapePallet/demoStar");
            btnThumbStar.Position = new Point(shapePalletX + 20, shapePalletY+260);
            btnThumbTriangle = makeButton(0, 0, "ShapePallet/demoTriangle");
            btnThumbTriangle.Position = new Point(shapePalletX + 20, shapePalletY+360);
            btnAddLetter = makeButton(0, 0, "ShapePallet/addLetter");
            btnAddLetter.Position = new Point(shapePalletX + 15, shapePalletY+550);
            btnAddNumber = makeButton(0, 0, "ShapePallet/addNumber");
            btnAddNumber.Position = new Point(shapePalletX + 15, shapePalletY+500);
            btnAddShape = makeButton(0, 0, "ShapePallet/addShape");
            btnAddShape.Position = new Point(shapePalletX + 15, shapePalletY+500);
            chooseSize = Content.Load<Texture2D>("ShapePallet/chooseSize");
            chooseSizePosition = (new Vector2(shapePalletX+10, shapePalletY+10));
            sizeTiny = makeButton(0, 0, "ShapePallet/sizeTiny");
            sizeTiny.Position = new Point(shapePalletX + 5, shapePalletY+60);
            sizeSmall = makeButton(0, 0, "ShapePallet/sizeSmall");
            sizeSmall.Position = new Point(shapePalletX + 5, shapePalletY+110);
            sizeMedium = makeButton(0, 0, "ShapePallet/sizeMedium");
            sizeMedium.Position = new Point(shapePalletX + 5, shapePalletY+160);
            sizeLarge = makeButton(0, 0, "ShapePallet/sizeLarge");
            sizeLarge.Position = new Point(shapePalletX + 5, shapePalletY+210);
            sizeXLarge = makeButton(0, 0, "ShapePallet/sizeXLarge");
            sizeXLarge.Position = new Point(shapePalletX + 5, shapePalletY+260);
            chooseColor = Content.Load<Texture2D>("ShapePallet/chooseColor");
            chooseColorPosition = (new Vector2(shapePalletX + 10, shapePalletY + 10));
            colorBlackBtn = makeButton(0, 0, "ShapePallet/blackColor");
            colorBlackBtn.Position = new Point(shapePalletX + 5, shapePalletY + 40);
            colorBlueBtn = makeButton(0, 0, "ShapePallet/blueColor");
            colorBlueBtn.Position = new Point(shapePalletX + 55, shapePalletY + 40);
            colorDarkBlueBtn = makeButton(0, 0, "ShapePallet/darkBlueColor");
            colorDarkBlueBtn.Position = new Point(shapePalletX + 5, shapePalletY + 90);
            colorDarkGreyBtn = makeButton(0, 0, "ShapePallet/darkGreyColor");
            colorDarkGreyBtn.Position = new Point(shapePalletX + 55, shapePalletY + 90);
            colorGreenBtn = makeButton(0, 0, "ShapePallet/greenColor");
            colorGreenBtn.Position = new Point(shapePalletX + 5, shapePalletY + 140);
            colorGreyBtn = makeButton(0, 0, "ShapePallet/greyColor");
            colorGreyBtn.Position = new Point(shapePalletX + 55, shapePalletY + 140);
            colorLightBlueBtn = makeButton(0, 0, "ShapePallet/lightBlueColor");
            colorLightBlueBtn.Position = new Point(shapePalletX + 5, shapePalletY + 190);
            colorLightGreen = makeButton(0, 0, "ShapePallet/lightGreenColor");
            colorLightGreen.Position = new Point(shapePalletX + 55, shapePalletY + 190);
            colorOrangeBtn = makeButton(0, 0, "ShapePallet/orangeColor");
            colorOrangeBtn.Position = new Point(shapePalletX + 5, shapePalletY + 240);
            colorPinkBtn = makeButton(0, 0, "ShapePallet/pinkColor");
            colorPinkBtn.Position = new Point(shapePalletX + 55, shapePalletY + 240);
            colorRedBtn = makeButton(0, 0, "ShapePallet/redColor");
            colorRedBtn.Position = new Point(shapePalletX + 5, shapePalletY + 290);
            colorYellowBtn = makeButton(0, 0, "ShapePallet/yellowColor");
            colorYellowBtn.Position = new Point(shapePalletX + 55, shapePalletY + 290);
            putA = makeButton(0, 0, "OSK/aButton");
            putA.Position = new Point(shapePalletX, shapePalletY+50);
            putB = makeButton(0, 0, "OSK/bButton");
            putB.Position = new Point(shapePalletX+48, shapePalletY+50);
            putC = makeButton(0, 0, "OSK/cButton");
            putC.Position = new Point(shapePalletX+96, shapePalletY+50);
            putD = makeButton(0, 0, "OSK/dButton");
            putD.Position = new Point(shapePalletX + 0, shapePalletY + 100);
            putE = makeButton(0, 0, "OSK/eButton");
            putE.Position = new Point(shapePalletX + 48, shapePalletY + 100);
            putF = makeButton(0, 0, "OSK/fButton");
            putF.Position = new Point(shapePalletX + 96, shapePalletY + 100);
            putG = makeButton(0, 0, "OSK/gButton");
            putG.Position = new Point(shapePalletX + 0 , shapePalletY + 150);
            putH = makeButton(0, 0, "OSK/hButton");
            putH.Position = new Point(shapePalletX + 48, shapePalletY + 150);
            putI = makeButton(0, 0, "OSK/iButton");
            putI.Position = new Point(shapePalletX + 96, shapePalletY + 150);
            putJ = makeButton(0, 0, "OSK/jButton");
            putJ.Position = new Point(shapePalletX + 0, shapePalletY + 200);
            putK = makeButton(0, 0, "OSK/kButton");
            putK.Position = new Point(shapePalletX + 48, shapePalletY + 200);
            putL = makeButton(0, 0, "OSK/lButton");
            putL.Position = new Point(shapePalletX + 96, shapePalletY + 200);
            putM = makeButton(0, 0, "OSK/mButton");
            putM.Position = new Point(shapePalletX + 0, shapePalletY + 250);
            putN = makeButton(0, 0, "OSK/nButton");
            putN.Position = new Point(shapePalletX + 48, shapePalletY + 250);
            putO = makeButton(0, 0, "OSK/oButton");
            putO.Position = new Point(shapePalletX + 96, shapePalletY + 250);
            putP = makeButton(0, 0, "OSK/pButton");
            putP.Position = new Point(shapePalletX + 0, shapePalletY + 300);
            putQ = makeButton(0, 0, "OSK/qButton");
            putQ.Position = new Point(shapePalletX + 48, shapePalletY + 300);
            putR = makeButton(0, 0, "OSK/rButton");
            putR.Position = new Point(shapePalletX + 96, shapePalletY + 300);
            putS = makeButton(0, 0, "OSK/sButton");
            putS.Position = new Point(shapePalletX + 0, shapePalletY + 350);
            putT = makeButton(0, 0, "OSK/tButton");
            putT.Position = new Point(shapePalletX + 48, shapePalletY + 350);
            putU = makeButton(0, 0, "OSK/uButton");
            putU.Position = new Point(shapePalletX + 96, shapePalletY + 350);
            putV = makeButton(0, 0, "OSK/vButton");
            putV.Position = new Point(shapePalletX + 0, shapePalletY + 400);
            putW = makeButton(0, 0, "OSK/wButton");
            putW.Position = new Point(shapePalletX + 48, shapePalletY + 400);
            putX = makeButton(0, 0, "OSK/xButton");
            putX.Position = new Point(shapePalletX + 96, shapePalletY + 400);
            putY = makeButton(0, 0, "OSK/yButton");
            putY.Position = new Point(shapePalletX + 0, shapePalletY + 450);
            putZ = makeButton(0, 0, "OSK/zButton");
            putZ.Position = new Point(shapePalletX + 48, shapePalletY + 450);
            put1 = makeButton(0, 0, "ShapePallet/oneBtn");
            put1.Position = new Point(shapePalletX + 0, shapePalletY + 50);
            put2 = makeButton(0, 0, "ShapePallet/twoBtn");
            put2.Position = new Point(shapePalletX + 48, shapePalletY + 50);
            put3 = makeButton(0, 0, "ShapePallet/threeBtn");
            put3.Position = new Point(shapePalletX + 96, shapePalletY + 50);
            put4 = makeButton(0, 0, "ShapePallet/fourBtn");
            put4.Position = new Point(shapePalletX + 0, shapePalletY + 100);
            put5 = makeButton(0, 0, "ShapePallet/fiveBtn");
            put5.Position = new Point(shapePalletX + 48, shapePalletY + 100);
            put6 = makeButton(0, 0, "ShapePallet/sixBtn");
            put6.Position = new Point(shapePalletX + 96, shapePalletY + 100);
            put7 = makeButton(0, 0, "ShapePallet/sevenBtn");
            put7.Position = new Point(shapePalletX + 0, shapePalletY + 150);
            put8 = makeButton(0, 0, "ShapePallet/eightBtn");
            put8.Position = new Point(shapePalletX + 48, shapePalletY + 150);
            put9 = makeButton(0, 0, "ShapePallet/nineBtn");
            put9.Position = new Point(shapePalletX + 96, shapePalletY + 150);
            put0 = makeButton(0, 0, "ShapePallet/zeroBtn");
            put0.Position = new Point(shapePalletX + 0, shapePalletY + 200);

            //game play elements - these elements make up the patient game play screen.
            btnBack = makeButton(0, 0, "GUI/backButton");
            btnBack.Position = new Point(0, 0);
            pauseMenuBackground = Content.Load<Texture2D>("GamePauseMenu/menuBackground");
            pauseMenuTitle = Content.Load<Texture2D>("GamePauseMenu/pauseMenuGraphic");
            btnPauseContinue = makeButton(0, 0, "GamePauseMenu/continueButtonGraphic");
            btnPauseContinue.Position = new Point(630, 355);
            btnPauseEdit = makeButton(0, 0, "GamePauseMenu/editButtonGraphic");
            btnPauseEdit.Position = new Point(630, 410);
            btnPauseLoad = makeButton(0, 0, "GamePauseMenu/changeLevelButtonGraphic");
            btnPauseLoad.Position = new Point(630, 465);
            btnPauseRestart = makeButton(0, 0, "GamePauseMenu/restartButtonGraphic");
            btnPauseRestart.Position = new Point(630, 520);
        }

	// Utility function for making a button at the specified locations.
	protected Button makeButton(int x, int y, string resourceName)
	{
	    Texture2D buttex = Content.Load<Texture2D>(resourceName);
	    Tangibility buttonTang = new Tangibility(x, y, buttex);
	    return new Button(buttonTang);
	}

	// Button to sprite
        protected void drawTo(GiveMeSomethingICanDraw drawable, SpriteBatch spriteBatch)
        {
	    Tangibility tan = drawable.getTangible();
            spriteBatch.Draw(tan.Texture, tan.Dimensions, tan.Color);
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
          //  content.Unload();
            
        }




        /// <summary>
        /// This is what will be updated every frame. In here we switch between game screens and more.
        /// There are many helper function located below that this generic update method calls.
        /// Some helper functions include drawing and updating. This removes a ton of non-logic from the
        /// below method.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        //UPDATE GAME HERE//
        protected override void Update(GameTime gameTime)
        {
            //Mouse state. May or may not work for touch.
            MouseState mouse = Mouse.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //This switch statment dictates what game state we are currently in.
            //The game states switch between the welcome home screen, the create a new level screen, the load level screen,
            //the level editor screen and finally the game playing screen for the patient.
            switch (CurrentGameState)
            {
                    ///////////////////////////////update if in HOME SCREEN///////////////////////////////
                case GameState.HomeScreen:
                    if (btnNew.IsClicked == true)
                    {
                        CurrentGameState = GameState.NewLevelScreen;
                    }
                    if (btnLoad.IsClicked == true)
                    {
                        CurrentGameState = GameState.LoadLevelScreen;
                    }
                    if (btnExit.IsClicked == true)
                    {
                        this.Exit();
                    }
                    UpdateHomeScreen(gameTime, mouse);
                    break;

                    //////////////////////////////update if in NEWLEVEL SCREEN//////////////////////////////
                case GameState.NewLevelScreen:
                    if (btnCancel.IsClicked == true)
                    {
                        CurrentGameState = GameState.HomeScreen;
                    }
                    if (btnCreate.IsClicked == true)
                    {
                        CurrentGameState = GameState.LevelEditor;
                    }
                    if (clearNameButton.IsClicked == true)
                    {
                        nameHighlight = true;
                    }
                    if (clearDescriptionButton.IsClicked == true)
                    {
                        nameHighlight = false;  
                    }
                    if (a.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "a"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "a"; }
                        a.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (b.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "b"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "b"; }
                        b.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (c.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "c"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "c"; }
                        c.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (d.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "d"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "d"; }
                        d.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (e.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "e"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "e"; }
                        e.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (f.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "f"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "f"; }
                        f.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (g.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "g"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "g"; }
                        g.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (h.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "h"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "h"; }
                        h.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (i.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "i"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "i"; }
                        i.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (j.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "j"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "j"; }
                        j.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (k.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "k"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "k"; }
                        k.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (l.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "l"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "l"; }
                        l.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (m.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "m"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "m"; }
                        m.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (n.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "n"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "n"; }
                        n.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (o.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "o"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "o"; }
                        o.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (p.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "p"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "p"; }
                        p.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (q.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "q"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "q"; }
                        q.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (r.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "r"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "r"; }
                        r.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (s.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "s"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "s"; }
                        s.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (t.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "t"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "t"; }
                        t.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (u.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "u"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "u"; }
                        u.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (v.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "v"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "v"; }
                        v.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (w.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "w"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "w"; }
                        w.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (x.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "x"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "x"; }
                        x.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (y.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "y"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "y"; }
                        y.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (z.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "z"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "z"; }
                        z.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (space1.IsClicked == true || space2.IsClicked)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + "_"; }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + "_"; }
                        space1.IsClicked = false;
                        space2.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (delete1.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = nameOfTherapist.Remove((nameOfTherapist.Length)-1); }
                        if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist.Remove((descriptionByTherapist.Length) - 1); }
                        delete1.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (clear.IsClicked == true)
                    {
                        if (nameHighlight == true) { nameOfTherapist = ""; }
                        if (nameHighlight == false) { descriptionByTherapist = ""; }
                        clear.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (delName.IsClicked == true)
                    {
                        nameHighlight = true;
                        nameOfTherapist = "";
                    }
                    if (delDesc.IsClicked == true)
                    {
                        nameHighlight = false;
                        descriptionByTherapist = "";
                    }
                    UpdateNewLevelScreen(gameTime, mouse);
                    break;

                    /////////////////////////////////////update if in LOAD LEVEL SCREEN///////////////////////////////
                case GameState.LoadLevelScreen:
                    if (goSearch.IsClicked == true)
                    {

                    }
                    if (btnCancel.IsClicked == true)
                    {
                        CurrentGameState = GameState.HomeScreen;
                    }
                    if (btnOpen.IsClicked == true)
                    {
                        CurrentGameState = GameState.LevelEditor;
                    }
                    if (delSearch.IsClicked == true)
                    {
                        searchQuery = "";
                    }
                    if (clearSearchButton.IsClicked == true)
                    {
                        if (loadKeyBoard == false) { loadKeyBoard = true; } //else { loadKeyBoard = false; }
                        clearSearchButton.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (a.IsClicked == true)
                    {
                        searchQuery = searchQuery + "a";
                        a.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (b.IsClicked == true)
                    {
                        searchQuery = searchQuery + "b";
                        b.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (c.IsClicked == true)
                    {
                        searchQuery = searchQuery + "c";
                        c.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (d.IsClicked == true)
                    {
                        searchQuery = searchQuery + "d";
                        d.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (e.IsClicked == true)
                    {
                        searchQuery = searchQuery + "e";
                        e.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (f.IsClicked == true)
                    {
                        searchQuery = searchQuery + "f";
                        f.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (g.IsClicked == true)
                    {
                        searchQuery = searchQuery + "g";
                        g.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (h.IsClicked == true)
                    {
                        searchQuery = searchQuery + "h";
                        h.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (i.IsClicked == true)
                    {
                        searchQuery = searchQuery + "i";
                        i.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (j.IsClicked == true)
                    {
                        searchQuery = searchQuery + "j";
                        j.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (k.IsClicked == true)
                    {
                        searchQuery = searchQuery + "k";
                        k.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (l.IsClicked == true)
                    {
                        searchQuery = searchQuery + "l";
                        l.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (m.IsClicked == true)
                    {
                        searchQuery = searchQuery + "m";
                        m.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (n.IsClicked == true)
                    {
                        searchQuery = searchQuery + "n";
                        n.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (o.IsClicked == true)
                    {
                        searchQuery = searchQuery + "o";
                        o.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (p.IsClicked == true)
                    {
                        searchQuery = searchQuery + "p";
                        p.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (q.IsClicked == true)
                    {
                        searchQuery = searchQuery + "q";
                        q.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (r.IsClicked == true)
                    {
                        searchQuery = searchQuery + "r";
                        r.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (s.IsClicked == true)
                    {
                        searchQuery = searchQuery + "s";
                        s.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (t.IsClicked == true)
                    {
                        searchQuery = searchQuery + "t";
                        t.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (u.IsClicked == true)
                    {
                        searchQuery = searchQuery + "u";
                        u.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (v.IsClicked == true)
                    {
                        searchQuery = searchQuery + "v";
                        v.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (w.IsClicked == true)
                    {
                        searchQuery = searchQuery + "w";
                        w.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (x.IsClicked == true)
                    {
                        searchQuery = searchQuery + "x";
                        x.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (y.IsClicked == true)
                    {
                        searchQuery = searchQuery + "y";
                        y.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (z.IsClicked == true)
                    {
                        searchQuery = searchQuery + "z";
                        z.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (space1.IsClicked == true || space2.IsClicked)
                    {
                        searchQuery = searchQuery + "_";
                        space1.IsClicked = false;
                        space2.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (delete1.IsClicked == true)
                    {
                        searchQuery = searchQuery.Remove((searchQuery.Length) - 1);
                        delete1.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (clear.IsClicked == true)
                    {
                        searchQuery = "";
                        clear.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    UpdateLoadLevelScreen(gameTime, mouse);
                    break;

                    ////////////////////////////////update if in LEVEL EDITIOR SCREEN///////////////////////////////
                case GameState.LevelEditor:
                    if (btnHome.IsClicked == true)
                    {
                        CurrentGameState = GameState.HomeScreen;
                        //Call A Method Defined In Another Class
                    }
                    if (btnMenu.IsClicked == true)
                    {
                        if (levelEditorMenuON == true)
                        {
                            levelEditorMenuON = false;
                        }else
                        {
                            levelEditorMenuON = true;
                        }
                        btnMenu.Update(mouse);
                        btnMenu.IsClicked = false;
                        Thread.Sleep(50);
                        //Call A Method Defined In Another Class
                    }
                    if (btnHoldTime.IsClicked == true)
                    {
                        //Call A Method Defined In Another Class
                    }
                    if (btnMultiple.IsClicked == true)
                    {
                        if (multiState == true)
                        {
                            multiState = false;
                            btnMultiple = makeButton(0, 0, "LevelEditorGUI/multipleToggleOff");
                            btnMultiple.Position = new Point(355, 30);
                            btnMultiple.Update(mouse);
                            btnMultiple.IsClicked = false;
                            Thread.Sleep(50);
                        }
                        else if (multiState == false)
                        {
                            multiState = true;
                            btnMultiple = makeButton(0, 0, "LevelEditorGUI/multipleToggleOn");
                            btnMultiple.Position = new Point(355, 30);
                            btnMultiple.Update(mouse);
                            btnMultiple.IsClicked = false;
                            Thread.Sleep(50);
                        }
                        //Call A Method Defined In Another Class
                    }
                    if (btnPlay.IsClicked == true)
                    {
                        CurrentGameState = GameState.PatientGame; 
                        //Call A Method Defined In Another Class
                    }
                    if (btnRedo.IsClicked == true)
                    {
                        //Call A Method Defined In Another Class
                    }
                    if (btnUndo.IsClicked == true)
                    {
                        //Call A Method Defined In Another Class
                    }
                    if (btnUpTime.IsClicked == true)
                    {
                        //Call A Method Defined In Another Class
                    }
                    if (btnMoreUp.IsClicked == true)
                    {
                        intUpTime++;
                        btnMoreUp.IsClicked = false;
                        Thread.Sleep(50);
                        //Call A Method Defined In Another Class
                    }
                    if (btnLessUp.IsClicked == true)
                    {
                        intUpTime--;
                        btnLessUp.IsClicked = false;
                        Thread.Sleep(50);
                        //Call A Method Defined In Another Class
                    }
                    if (btnMoreHold.IsClicked == true)
                    {
                        intHoldTime++;
                        btnMoreHold.IsClicked = false;
                        Thread.Sleep(50);
                        //Call A Method Defined In Another Class
                    }
                    if (btnLessHold.IsClicked == true)
                    {
                        intHoldTime--;
                        btnLessHold.IsClicked = false;
                        Thread.Sleep(50);
                        //Call A Method Defined In Another Class
                    }
                    if (btnLemBack.IsClicked == true) {
                        levelEditorMenuON = false;
                        btnLemBack.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (btnLemSave.IsClicked == true) {
                        levelEditorMenuON = false;
                        btnLemSave.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (btnLemLoad.IsClicked == true) {
                        levelEditorMenuON = false;
                        btnLemLoad.IsClicked = false;
                        Thread.Sleep(50);
                        CurrentGameState = GameState.LoadLevelScreen;
                    }
                    if (btnLemClear.IsClicked == true) {
                        levelEditorMenuON = false;
                        btnLemClear.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (btnLemExit.IsClicked == true) {
                        levelEditorMenuON = false;
                        this.Exit();
                    }
                    if (btnAddLetter.IsClicked == true)
                    {
                        showingAlpha = true;
                        showingNumbers = false;
                        showingShapes = false;
                        btnAddLetter.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (btnAddNumber.IsClicked == true)
                    {
                        showingNumbers = true;
                        showingAlpha = false;
                        showingShapes = false;
                        btnAddNumber.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (btnAddShape.IsClicked == true)
                    {
                        showingShapes = true;
                        showingAlpha = false;
                        showingNumbers = false;
                        btnAddShape.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (btnHidePallet.IsClicked == true)
                    {
                        if (shapePalletVisible){shapePalletVisible = false;}
                        else { shapePalletVisible = true; }
                        btnHidePallet.IsClicked = false;
                        Thread.Sleep(50);

                    }
                    if (btnThumbCircle.IsClicked == true)
                    {
                        showingShapes = false;
                        sizeChoosing = true;
                        btnThumbCircle.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (btnThumbSquare.IsClicked == true)
                    {
                        showingShapes = false;
                        sizeChoosing = true;
                        btnThumbSquare.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (btnThumbStar.IsClicked == true)
                    {
                        showingShapes = false;
                        sizeChoosing = true;
                        btnThumbStar.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (btnThumbTriangle.IsClicked == true)
                    {
                        showingShapes = false;
                        sizeChoosing = true;
                        btnThumbTriangle.IsClicked = false;
                        Thread.Sleep(50);
                    }
                    if (sizeTiny.IsClicked == true)
                    {
                        showingShapes = false;
                        sizeChoosing = false;
                        colorChoosing = true;
                        sizeTiny.IsClicked = false;
                        Thread.Sleep(1500);
                    }
                    if (sizeSmall.IsClicked == true)
                    {
                        showingShapes = false;
                        sizeChoosing = false;
                        colorChoosing = true;
                        sizeSmall.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (sizeMedium.IsClicked == true)
                    {
                        showingShapes = false;
                        sizeChoosing = false;
                        colorChoosing = true;
                        sizeMedium.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (sizeLarge.IsClicked == true)
                    {
                        showingShapes = false;
                        sizeChoosing = false;
                        colorChoosing = true;
                        sizeLarge.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (sizeXLarge.IsClicked == true)
                    {
                        showingShapes = false;
                        sizeChoosing = false;
                        colorChoosing = true;
                        sizeXLarge.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (colorBlackBtn.IsClicked == true)
                    {
                        colorChoosing = false;
                        showingShapes = true;
                        colorBlackBtn.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (colorBlueBtn.IsClicked == true)
                    {
                        colorChoosing = false;
                        showingShapes = true;
                        colorBlueBtn.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (colorDarkBlueBtn.IsClicked == true)
                    {
                        colorChoosing = false;
                        showingShapes = true;
                        colorDarkBlueBtn.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (colorDarkGreyBtn.IsClicked == true)
                    {
                        colorChoosing = false;
                        showingShapes = true;
                        colorDarkGreyBtn.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (colorGreenBtn.IsClicked == true)
                    {
                        colorChoosing = false;
                        showingShapes = true;
                        colorGreenBtn.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (colorGreyBtn.IsClicked == true)
                    {
                        colorChoosing = false;
                        showingShapes = true;
                        colorGreyBtn.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (colorLightBlueBtn.IsClicked == true)
                    {
                        colorChoosing = false;
                        showingShapes = true;
                        colorLightBlueBtn.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (colorLightGreen.IsClicked == true)
                    {
                        colorChoosing = false;
                        showingShapes = true;
                        colorLightGreen.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (colorOrangeBtn.IsClicked == true)
                    {
                        colorChoosing = false;
                        showingShapes = true;
                        colorOrangeBtn.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (colorPinkBtn.IsClicked == true)
                    {
                        colorChoosing = false;
                        showingShapes = true;
                        colorPinkBtn.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (colorRedBtn.IsClicked == true)
                    {
                        colorChoosing = false;
                        showingShapes = true;
                        colorRedBtn.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (colorYellowBtn.IsClicked == true)
                    {
                        colorChoosing = false;
                        showingShapes = true;
                        colorYellowBtn.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (put0.IsClicked == true)
                    {
                        put0.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (put1.IsClicked == true)
                    {
                        put1.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (put2.IsClicked == true)
                    {
                        put2.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (put3.IsClicked == true)
                    {
                        put3.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (put4.IsClicked == true)
                    {
                        put4.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (put5.IsClicked == true)
                    {
                        put5.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (put6.IsClicked == true)
                    {
                        put6.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (put7.IsClicked == true)
                    {
                        put7.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (put8.IsClicked == true)
                    {
                        put8.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (put9.IsClicked == true)
                    {
                        put9.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putA.IsClicked == true)
                    {
                        putA.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putB.IsClicked == true)
                    {
                        putB.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putC.IsClicked == true)
                    {
                        putC.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putD.IsClicked == true)
                    {
                        putD.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putE.IsClicked == true)
                    {
                        putE.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putF.IsClicked == true)
                    {
                        putF.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putG.IsClicked == true)
                    {
                        putG.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putH.IsClicked == true)
                    {
                        putH.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putI.IsClicked == true)
                    {
                        putI.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putJ.IsClicked == true)
                    {
                        putJ.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putK.IsClicked == true)
                    {
                        putK.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putL.IsClicked == true)
                    {
                        putL.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putM.IsClicked == true)
                    {
                        putM.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putN.IsClicked == true)
                    {
                        putN.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putO.IsClicked == true)
                    {
                        putO.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putP.IsClicked == true)
                    {
                        putP.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putQ.IsClicked == true)
                    {
                        putQ.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putR.IsClicked == true)
                    {
                        putR.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putS.IsClicked == true)
                    {
                        putS.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putT.IsClicked == true)
                    {
                        putT.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putU.IsClicked == true)
                    {
                        putU.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putV.IsClicked == true)
                    {
                        putV.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putW.IsClicked == true)
                    {
                        putW.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putX.IsClicked == true)
                    {
                        putX.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putY.IsClicked == true)
                    {
                        putY.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    if (putZ.IsClicked == true)
                    {
                        putZ.IsClicked = false;
                        Thread.Sleep(500);
                    }
                    UpdateLevelEditorScreen(gameTime, mouse);
                    break;

                    ///////////////////////////////update if playing PATIENT GAME////////////////////////////////////////
                case GameState.PatientGame:
                    if (btnBack.IsClicked == true)
                    {
                        btnBack.IsClicked = false;
                        pauseMenuON = true;
                        Thread.Sleep(50);
                        btnBack.Update(mouse);
                    }
                    if (btnPauseContinue.IsClicked == true)
                    {
                        pauseMenuON = false;
                    }
                    if (btnPauseEdit.IsClicked == true)
                    {
                        pauseMenuON = false;
                        CurrentGameState = GameState.LevelEditor;
                    }
                    if (btnPauseLoad.IsClicked == true)
                    {
                        pauseMenuON = false;
                        CurrentGameState = GameState.LoadLevelScreen;
                    }
                    if (btnPauseRestart.IsClicked == true)
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
        /// This is called when the game should DRAW itself. This method also uses helper functions for drawing.
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
                case GameState.LevelEditor: //Draw all elements for Level Editor Screen
                    DrawLevelEdtorScreen(gameTime, spriteBatch);
                    break;
                case GameState.PatientGame: //Draw all elements for Patient Game
                    DrawGameScreen(gameTime, spriteBatch);
                    break;

            }
            spriteBatch.End();
            base.Draw(gameTime);
        }






        ///////////////////////////////////////////helper methods below/////////////////////////////////////////////////////
        /// <summary>
        /// Blow are helper methods for drawing and updating objects.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        void DrawLevelEdtorScreen(GameTime gameTime, SpriteBatch spriteBatch)
        {
            drawTo(btnHome, spriteBatch);
            drawTo(btnMenu, spriteBatch);
            drawTo(btnHoldTime, spriteBatch);
            drawTo(btnMultiple, spriteBatch);
            drawTo(btnPlay, spriteBatch);
            drawTo(btnRedo, spriteBatch);
            drawTo(btnUndo, spriteBatch);
            drawTo(btnUpTime, spriteBatch);
            drawTo(btnMoreUp, spriteBatch);
            drawTo(btnMoreHold, spriteBatch);
            drawTo(btnLessUp, spriteBatch);
            drawTo(btnLessHold, spriteBatch);
            originForRotation.X = 960;//myGrid.Width / 2;
            originForRotation.Y = 485;//(myGrid.Height + 110) / 2;
            spriteBatch.Draw(myGrid, myGridPosition, null, Color.White, rotationAngle, originForRotation, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, intHoldTime.ToString(), intHoldTimePosition, Color.Black);
            spriteBatch.DrawString(font, intUpTime.ToString(), intUpTimePosition, Color.Black);
            if (levelEditorMenuON)
            {
                spriteBatch.Draw(levelEditorMenuBackground, levelEditorMenuBackgroundPosition, Color.White);
                spriteBatch.Draw(levelEditorMenuTitle, levelEditorMenuGraphicPosition, Color.White);
                drawTo(btnLemBack, spriteBatch);
                drawTo(btnLemClear, spriteBatch);
                drawTo(btnLemExit, spriteBatch);
                drawTo(btnLemLoad, spriteBatch);
                drawTo(btnLemSave, spriteBatch);
            }
            if (shapePalletVisible)
            {
                spriteBatch.Draw(shapePalletBackground, shapePalletBackgroundPosition, Color.White);
                drawTo(btnHidePallet, spriteBatch);
                if (showingShapes)
                {
                    drawTo(btnThumbTriangle, spriteBatch);
                    drawTo(btnThumbStar, spriteBatch);
                    drawTo(btnThumbSquare, spriteBatch);
                    drawTo(btnThumbCircle, spriteBatch);
                    btnAddNumber.Position = new Point(shapePalletX + 15, shapePalletY + 500);
                    drawTo(btnAddLetter, spriteBatch);
                    drawTo(btnAddNumber, spriteBatch);
                }
                if (showingAlpha) {
                    drawTo(btnAddNumber, spriteBatch);
                    btnAddNumber.Position = new Point(shapePalletX + 15, shapePalletY + 550);
                    drawTo(btnAddShape, spriteBatch);
                    drawTo(putA, spriteBatch);
                    drawTo(putB, spriteBatch);
                    drawTo(putC, spriteBatch);
                    drawTo(putD, spriteBatch);
                    drawTo(putE, spriteBatch);
                    drawTo(putF, spriteBatch);
                    drawTo(putG, spriteBatch);
                    drawTo(putH, spriteBatch);
                    drawTo(putI, spriteBatch);
                    drawTo(putJ, spriteBatch);
                    drawTo(putK, spriteBatch);
                    drawTo(putL, spriteBatch);
                    drawTo(putM, spriteBatch);
                    drawTo(putN, spriteBatch);
                    drawTo(putO, spriteBatch);
                    drawTo(putP, spriteBatch);
                    drawTo(putQ, spriteBatch);
                    drawTo(putR, spriteBatch);
                    drawTo(putS, spriteBatch);
                    drawTo(putT, spriteBatch);
                    drawTo(putU, spriteBatch);
                    drawTo(putV, spriteBatch);
                    drawTo(putW, spriteBatch);
                    drawTo(putX, spriteBatch);
                    drawTo(putY, spriteBatch);
                    drawTo(putZ, spriteBatch);
                   
                }//Fill this in.
                if (showingNumbers) {
                    drawTo(put1, spriteBatch);
                    drawTo(put2, spriteBatch);
                    drawTo(put3, spriteBatch);
                    drawTo(put4, spriteBatch);
                    drawTo(put5, spriteBatch);
                    drawTo(put6, spriteBatch);
                    drawTo(put7, spriteBatch);
                    drawTo(put8, spriteBatch);
                    drawTo(put9, spriteBatch);
                    drawTo(put0, spriteBatch);
                    drawTo(btnAddLetter, spriteBatch);
                    drawTo(btnAddShape, spriteBatch);
                }///Fill this in.
                
                
            }
            else { drawTo(btnHidePallet, spriteBatch); }
            if (sizeChoosing)
            {
                spriteBatch.Draw(shapePalletBackground, shapePalletBackgroundPosition, Color.White);
                spriteBatch.Draw(chooseSize, chooseSizePosition, Color.White);
                drawTo(sizeTiny, spriteBatch);
                drawTo(sizeSmall, spriteBatch);
                drawTo(sizeMedium, spriteBatch);
                drawTo(sizeLarge, spriteBatch);
                drawTo(sizeXLarge, spriteBatch);
            }
            if (colorChoosing)
            {
                spriteBatch.Draw(shapePalletBackground, shapePalletBackgroundPosition, Color.White);
                drawTo(colorBlackBtn, spriteBatch);
                drawTo(colorBlueBtn, spriteBatch);
                drawTo(colorDarkBlueBtn, spriteBatch);
                drawTo(colorDarkGreyBtn, spriteBatch);
                drawTo(colorGreenBtn, spriteBatch);
                drawTo(colorGreyBtn, spriteBatch);
                drawTo(colorLightBlueBtn, spriteBatch);
                drawTo(colorLightGreen, spriteBatch);
                drawTo(colorOrangeBtn, spriteBatch);
                drawTo(colorPinkBtn, spriteBatch);
                drawTo(colorRedBtn, spriteBatch);
                drawTo(colorYellowBtn, spriteBatch);
            }

        }
        void DrawGameScreen(GameTime gameTime, SpriteBatch spriteBatch)
        {
            drawTo(btnBack, spriteBatch);
            if (pauseMenuON)
            {
                spriteBatch.Draw(pauseMenuBackground, pauseMenuBackgroundPosition, Color.White);
                spriteBatch.Draw(pauseMenuTitle, pauseMenuGraphicPosition, Color.White);
                drawTo(btnPauseRestart, spriteBatch);
                drawTo(btnPauseLoad, spriteBatch);
                drawTo(btnPauseEdit, spriteBatch);
                drawTo(btnPauseContinue, spriteBatch); 
            }
        }

        void DrawHomeScreen (GameTime gameTime, SpriteBatch spriteBatch) {
            drawTo(btnNew, spriteBatch);
            drawTo(btnLoad, spriteBatch);
            drawTo(btnExit, spriteBatch);
            spriteBatch.Draw(myTitle, myTitlePosition, Color.White);
        
        }
        void DrawNewLayoutScreen(GameTime gameTime, SpriteBatch spriteBatch)
        {
            drawTo(btnCancel, spriteBatch);
            drawTo(btnCreate, spriteBatch);
            spriteBatch.Draw(textBackgorund, nameBackgroundPosition, Color.White);
            drawTo(clearDescriptionButton, spriteBatch);
            drawTo(clearNameButton, spriteBatch);
            spriteBatch.Draw(myNewLevelTitle, myNewLevelTitlePosition, Color.White);
            if (nameHighlight == true) { spriteBatch.DrawString(font, nameOfTherapist, nameOfTherapistPosition, Color.Black); }
            else { spriteBatch.DrawString(font, nameOfTherapist, nameOfTherapistPosition, Color.DarkGray); }

            if (nameHighlight == false) { spriteBatch.DrawString(font, descriptionByTherapist, descriptionByTherapistPosition, Color.Black); }
            else { spriteBatch.DrawString(font, descriptionByTherapist, descriptionByTherapistPosition, Color.DarkGray); }

            spriteBatch.Draw(myName, myNamePosition, Color.White);
            spriteBatch.Draw(myDescription, myDescriptionPosition, Color.White);
            spriteBatch.Draw(myOSKBackground, myOSKBackgroundPosition, Color.White);
            drawTo(delName, spriteBatch);
            drawTo(delDesc, spriteBatch);
            drawTo(a, spriteBatch);
            drawTo(b, spriteBatch);
            drawTo(c, spriteBatch);
            drawTo(d, spriteBatch);
            drawTo(e, spriteBatch);
            drawTo(f, spriteBatch);
            drawTo(g, spriteBatch);
            drawTo(h, spriteBatch);
            drawTo(i, spriteBatch);
            drawTo(j, spriteBatch);
            drawTo(k, spriteBatch);
            drawTo(l, spriteBatch);
            drawTo(m, spriteBatch);
            drawTo(n, spriteBatch);
            drawTo(o, spriteBatch);
            drawTo(p, spriteBatch);
            drawTo(q, spriteBatch);
            drawTo(r, spriteBatch);
            drawTo(s, spriteBatch);
            drawTo(t, spriteBatch);
            drawTo(u, spriteBatch);
            drawTo(v, spriteBatch);
            drawTo(w, spriteBatch);
            drawTo(x, spriteBatch);
            drawTo(y, spriteBatch);
            drawTo(z, spriteBatch);
            drawTo(space1, spriteBatch);
            drawTo(space2, spriteBatch);
            drawTo(delete1, spriteBatch);
            drawTo(clear, spriteBatch);

        }
        void DrawLoadLayoutScreen(GameTime gameTime, SpriteBatch spriteBatch)
        {
            drawTo(btnCancel, spriteBatch);
            drawTo(btnOpen, spriteBatch);
            spriteBatch.Draw(myLoadLevelTitle, myLoadLevelTitlePosition, Color.White);
            drawTo(goSearch, spriteBatch);

            if (loadKeyBoard == false) 
            { 
                spriteBatch.DrawString(font, searchQuery, searchQueryPosition, Color.White); 
            }else
            {
                spriteBatch.DrawString(font, searchQuery, searchQueryPosition, Color.Black);
            }
            spriteBatch.Draw(magnifyGlass, magnifyGlassPosition, Color.White);
            spriteBatch.Draw(listBackground, lisBackgroundPosition, Color.White);
            drawTo(delSearch, spriteBatch);

            if (loadKeyBoard == true)
            {
                spriteBatch.Draw(myOSKBackground, myOSKBackgroundPosition, Color.White);
                drawTo(a, spriteBatch);
                drawTo(b, spriteBatch);
                drawTo(c, spriteBatch);
                drawTo(d, spriteBatch);
                drawTo(e, spriteBatch);
                drawTo(f, spriteBatch);
                drawTo(g, spriteBatch);
                drawTo(h, spriteBatch);
                drawTo(i, spriteBatch);
                drawTo(j, spriteBatch);
                drawTo(k, spriteBatch);
                drawTo(l, spriteBatch);
                drawTo(m, spriteBatch);
                drawTo(n, spriteBatch);
                drawTo(o, spriteBatch);
                drawTo(p, spriteBatch);
                drawTo(q, spriteBatch);
                drawTo(r, spriteBatch);
                drawTo(s, spriteBatch);
                drawTo(t, spriteBatch);
                drawTo(u, spriteBatch);
                drawTo(v, spriteBatch);
                drawTo(w, spriteBatch);
                drawTo(x, spriteBatch);
                drawTo(y, spriteBatch);
                drawTo(z, spriteBatch);
                drawTo(space1, spriteBatch);
                drawTo(space2, spriteBatch);
                drawTo(delete1, spriteBatch);
                drawTo(clear, spriteBatch);
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
