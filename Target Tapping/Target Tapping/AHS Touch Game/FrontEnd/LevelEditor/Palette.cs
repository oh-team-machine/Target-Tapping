using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TargetTapping.Back_end;
using Microsoft.Xna.Framework;

namespace TargetTapping.FrontEnd.LevelEditor
{
    class Palette : Updatable
    {
        public Object Object { get; private set; }
        public ShapeCreationState ObjectFactory { get; private set; }

        public Rectangle BoundingBox { get; set; }
        public Point Position { get; set; }

        // The current state.
        public PaletteState CurrentState { get; private set; }

        // Contains all of the managed states.
        private Dictionary<string, PaletteState> States = new Dictionary<string, PaletteState>();

        public Palette(int x, int y)
        {
            Object = null;
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
            States.Add("INITIAL", States["Shape"]);
            States.Add("NEXT", States["Size"]);
            CurrentState = States["INITIAL"];

        }

        public void Hide()
        {
        }

        // Changes the state on next update.
        public void RequestStateChange(string stateName)
        {
            if (States.ContainsKey(stateName))
            {
                CurrentState = States[stateName];
            }
            else
            {
                throw new NotSupportedException("State '" + stateName + "' does not exist.");
            }
        }

        public void Update(Microsoft.Xna.Framework.Input.MouseState state)
        {
            CurrentState.Update(state);
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            CurrentState.Draw(spriteBatch);
        }
    
    }
}
