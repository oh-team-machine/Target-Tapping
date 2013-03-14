using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TargetTapping.Back_end;

namespace TargetTapping.FrontEnd.LevelEditor
{
    internal class Palette : Updatable
    {
        private const string InitialStateName = "Shape";

        private Dictionary<string, PaletteState> States =
                new Dictionary<string, PaletteState>();

        private bool _isHidden;

        private Dictionary<string, string> nextStates = new Dictionary
                <string, string>
            {
                    {"Shape", "Size"},
                    {"Num", "Size"},
                    {"Alph", "Size"},
                    {"Size", "Color"},
                    {"Color", "Position"},
                    {"Position", "INITIAL"}
            };

        private Texture2D _shapePalletBackground;

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
            States.Add("INITIAL", States[InitialStateName]);
                    // First is always Shape
            CurrentState = States["INITIAL"];
                    // Set the current state to the initial state.
            CurrentStateName = InitialStateName;

            // Make sure that it's unhidden!
            Unhide();
        }

        public ShapeCreationState ObjectFactory { get; private set; }

        public Rectangle BoundingBox { get; private set; }

        public Point Position
        {
            get { return BoundingBox.Location; }
            private set
            {
                var newBox = new Rectangle(
                        value.X, value.Y,
                        BoundingBox.Width, BoundingBox.Height);
                BoundingBox = newBox;
            }
        }

        // The current state.
        public PaletteState CurrentState { get; private set; }
        public String CurrentStateName { get; private set; }

        // TEMPORARY

        public void Update(Microsoft.Xna.Framework.Input.MouseState state)
        {
            CurrentState.Update(state);
        }

        // Resets the ObjectFactory.

        public void LoadContent(RichContentManager content)
        {
            var statesLoaded = new List<PaletteState>();

            _shapePalletBackground =
                    content.Load<Texture2D>("ShapePallet/shapePalletBackground");
            BoundingBox = new Rectangle(
                    Position.X, Position.Y,
                    _shapePalletBackground.Width,
                    _shapePalletBackground.Height);

            foreach (var state in States.Values)
            {
                if (statesLoaded.Contains(state))
                {
                    continue;
                }

                statesLoaded.Add(state);
                state.LoadContent(content);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_isHidden)
                return;

            // Draw the background first.
            spriteBatch.Draw(_shapePalletBackground, BoundingBox, Color.White);

            // WHY WON'T YOU DRAW?
            CurrentState.Draw(spriteBatch);
        }

        public void Reset()
        {
            // Simply get a new ShapeCreationState and let the
            // GC deal with the old one.
            ObjectFactory = new ShapeCreationState();
        }

        public void Hide()
        {
            _isHidden = true;
        }

        public void Unhide()
        {
            _isHidden = false;
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
                if (stateName == "INITIAL")
                {
                    stateName = InitialStateName;
                }
                CurrentStateName = stateName;
                CurrentState = States[stateName];
            }
            else
            {
                throw new NotSupportedException("State '" + stateName +
                                                "' does not exist.");
            }
        }
    }
}