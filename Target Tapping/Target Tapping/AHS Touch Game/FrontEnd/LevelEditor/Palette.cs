using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TargetTapping.Back_end;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TargetTapping.FrontEnd.LevelEditor
{
    class Palette : Updatable
    {

        public ShapeCreationState ObjectFactory { get; private set; }

        public Rectangle BoundingBox { get; private set; }

        public Point Position
        {
            get
            {
                return BoundingBox.Location;
            }
            private set
            {
                Rectangle newBox = new Rectangle(
                    value.X, value.Y,
                    BoundingBox.Width, BoundingBox.Height);
                BoundingBox = newBox;
            }
        }

        // The current state.
        public PaletteState CurrentState { get; private set; }
        public String CurrentStateName { get; private set; }

        // TEMPORARY
        bool isHidden = false;

        // Contains all of the managed states.
        private Dictionary<string, PaletteState> States = new Dictionary<string, PaletteState>();
        private Texture2D shapePalletBackground;

        // A dictionary to the NEXT state.
        private Dictionary<string, string> nextStates = new Dictionary<string, string>
        {
            { "Shape", "Size" },
            { "Num",  "Size" },
            { "Alph", "Size" },
            { "Size", "Color" },
            { "Color", "Position" },
            { "Position", "INITIAL" }
        };

        public Palette(int x, int y)
        {
            Position = new Point(x, y);

            ObjectFactory = new ShapeCreationState();

            // Instantiate new start states.
            States.Add("Shape", new ShapePaletteState(this));
            States.Add("Num", new NumPaletteState(this));
            States.Add("Alph", new AlphPaletteState(this));

            // The states they go to.
            States.Add("Size", new SizePaletteState(this));
            States.Add("Color", new ColorPaletteState(this));
            States.Add("Position", new PositionPaletteState(this));

            // Setup the initial, and next states.
            var initialStateName = "Shape";
            States.Add("INITIAL", States[initialStateName]); // First is always Shape
            CurrentState = States["INITIAL"]; // Set the current state to the initial state.
            CurrentStateName = initialStateName;

            // Make sure that it's unhidden!
            Unhide();
        }

        public void Update(Microsoft.Xna.Framework.Input.MouseState state)
        {
            CurrentState.Update(state);
        }

        // Resets the ObjectFactory.
        public void Reset()
        {
            // Simply get a new ShapeCreationState and let the
            // GC deal with the old one.
            ObjectFactory = new ShapeCreationState();
        }

        public void LoadContent(RichContentManager content)
        {
            List<PaletteState> statesLoaded = new List<PaletteState>();

            shapePalletBackground = content.Load<Texture2D>("ShapePallet/shapePalletBackground");
            BoundingBox = new Rectangle(
                Position.X, Position.Y,
                shapePalletBackground.Width,
                shapePalletBackground.Height);

            foreach (var state in States.Values)
            {
                if (statesLoaded.Contains(state)) {
                    continue;
                }

                statesLoaded.Add(state);
                state.LoadContent(content);
            }
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if (isHidden)
                return;

            // Draw the background first.
            spriteBatch.Draw(shapePalletBackground, BoundingBox, Color.White);

            // WHY WON'T YOU DRAW?
            CurrentState.Draw(spriteBatch);
        }

        public void Hide()
        {
            isHidden = true;
        }

        public void Unhide()
        {
            isHidden = false;
        }

        // Changes the state on next update.
        public void RequestStateChange(string stateName)
        {
            if (stateName == "NEXT")
            {
                var nextName = nextStates[CurrentStateName];
                var nextState = States[nextName];

                CurrentStateName = nextName;
                CurrentState = nextState;

                return;
            }

            if (States.ContainsKey(stateName))
            {
                CurrentState = States[stateName];
            }
            else
            {
                throw new NotSupportedException("State '" + stateName + "' does not exist.");
            }
        }

    }
}
