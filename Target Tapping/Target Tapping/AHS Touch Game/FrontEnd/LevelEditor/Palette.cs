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
        private Dictionary<string, PaletteState> States = new Dictionary<string,PaletteState>;

        public Palette(int x, int y)
        {
            Object = null;
            ObjectFactory = new ShapeCreationState();

            //var initialState = new 
            //States.Add("shape",
        }

        void Hide()
        {
        }

        // Changes the state on next update.
        public void RequestStateChange(string stateName)
        {
            if (States.ContainsKey(stateName)) {
                CurrentState = States[stateName];
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
    
internal void Hide()
{
 	throw new NotImplementedException();
}}
}
