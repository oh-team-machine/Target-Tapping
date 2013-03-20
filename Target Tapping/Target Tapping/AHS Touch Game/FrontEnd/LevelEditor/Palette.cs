using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TargetTapping.Back_end;

namespace TargetTapping.FrontEnd.LevelEditor
{
    internal class Palette : Updatable
    {
        private const string InitialStateName = "Num";

        private readonly Dictionary<string, PaletteState> _states =
                new Dictionary<string, PaletteState>();

        private bool _isHidden;

        // Edges of directed state diagram. From state => To State.
        private readonly Dictionary<string, string> _nextStates = new Dictionary
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
            _states.Add("Shape", new ShapePaletteState(this));
            _states.Add("Num", new NumPaletteState(this));
            _states.Add("Alph", new AlphPaletteState(this));

            // The states they go to.
            _states.Add("Size", new SizePaletteState(this));
            _states.Add("Color", new ColorPaletteState(this));
            _states.Add("Position", new PositionPaletteState(this));

            // Setup the initial, and next states.
            _states.Add("INITIAL", _states[InitialStateName]);
                    // First is always Shape
            CurrentState = _states["INITIAL"];
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

                // Change the position of all palette states.
                foreach (var state in _states.Values)
                {
                    state.Position = value;
                }
            }
        }

        // The current state.
        public PaletteState CurrentState { get; private set; }
        public String CurrentStateName { get; private set; }

        public void Update(Microsoft.Xna.Framework.Input.MouseState state)
        {
            CurrentState.Update(state);
        }

        public void LoadContent(RichContentManager content)
        {

            // Load the background.
            _shapePalletBackground =
                    content.Load<Texture2D>("ShapePallet/shapePalletBackground");
            BoundingBox = new Rectangle(
                    Position.X, Position.Y,
                    _shapePalletBackground.Width,
                    _shapePalletBackground.Height);

            // LoadContent for all palette states; do not LoadContent more than once.
            var statesLoaded = new List<PaletteState>();
            foreach (var state in _states.Values.Where(state => !statesLoaded.Contains(state))) {
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

            CurrentState.Draw(spriteBatch);
        }

        public void Reset()
        {
            // Simply get a new ShapeCreationState and let the
            // GC deal with the old one.
            ObjectFactory = new ShapeCreationState();

            Unhide();
            RequestStateChange("INITIAL");
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
                var nextName = _nextStates[CurrentStateName];
                var nextState = _states[nextName];

                CurrentStateName = nextName;
                CurrentState = nextState;

                return;
            }

            if (_states.ContainsKey(stateName))
            {
                if (stateName == "INITIAL")
                {
                    stateName = InitialStateName;
                }
                CurrentStateName = stateName;
                CurrentState = _states[stateName];
            }
            else
            {
                throw new NotSupportedException(
                    string.Format("State '{0}' does not exist.", stateName));
            }
        }
    }
}