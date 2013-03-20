using System.Collections.Generic;
using System.Linq;
using GameLibrary.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TargetTapping.FrontEnd.LevelEditor
{
    // Class that provides common behaviour for PaletteStates (that is, provides a category of buttons).
    internal abstract class RichPaletteState : PaletteState
    {
        protected string[] ThingNames { get; set; }
        private Point _position;

        protected Dictionary<string, Button> ThingButtons = new Dictionary<string,Button>();

        // Just do what the parent does.
        protected RichPaletteState(Palette p) : base(p) { }

        internal override Point Position
        {
            get { return _position; }
            set
            {
                MoveEverything(value);
                _position = value;
            }
        }

        // Moves all the buttons to the given position.
        protected void MoveEverything(Point newTopLeft)
        {
            var oldTopLeft = _position;
            foreach (var button in ThingButtons.Values)
            {
                // Get the offset from the old top-left.
                var old = button.Rect.Location;
                var offset = new Point(
                    old.X - oldTopLeft.X,
                    old.Y - oldTopLeft.Y);

                // Find out the new position.
                var newPos = new Point(
                    newTopLeft.X + offset.X,
                    newTopLeft.Y + offset.Y);

                button.Rect = new Rectangle(
                   newPos.X, newPos.Y,
                   button.Rect.Width,
                   button.Rect.Height);
            }

        }

        // Given a thing name, gets the resource name.
        protected abstract string ResourceNameFromId(string name);

        // Max number of things in a row. Defaults to 1.
        protected virtual int MaxInRow()
        {
            return 1;
        }

        // Default sets all of the buttons, yo.
        public override void LoadContent(RichContentManager content)
        {
            // Load all of the buttons from thing names.
            LoadButtons(content);
            LoadContentExtra(content);
        }

        protected virtual void LoadContentExtra(RichContentManager content)
        {
            // INTENDED TO BE OVERRIDDEN
        }

        protected void LoadButtons(RichContentManager content)
        {
            int x = Parent.Position.X,
                y = Parent.Position.Y;

            var maxInRow = MaxInRow();
            var inRow = 0;

            foreach (var name in ThingNames)
            {
                var resource = ResourceNameFromId(name);
                var button = content.MakeButton(x, y, resource);

                x += button.Rect.Width;
                inRow++;

                // Start from the beginning for a new row.
                if (inRow >= maxInRow)
                {
                    x = Parent.Position.X;
                    inRow = 0;
                    y += button.Rect.Height;
                }

                ThingButtons.Add(name, button);
            }
        }

        // What happens when a button is clicked. Return true if the button
        // must go to the next state.
        protected abstract bool OnButtonPressed(string name, Button button);

        public override void Update(MouseState state)
        {
            // Get the button that is clicked and do something specified by the subclass.
            foreach (var pair in ThingButtons.Where(pair => pair.Value.IsClicked())) {
                if (OnButtonPressed(pair.Key, pair.Value))
                {
                    Parent.RequestStateChange("NEXT");
                }
                break;
            }

            UpdateThingButtons(state);
        }

        private void UpdateThingButtons(MouseState state)
        {
            // Update the button states.
            foreach (var button in ThingButtons.Values)
            {
                button.Update(state);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawExtras(spriteBatch);
            DrawThingButtons(spriteBatch);
        }

        protected virtual void DrawExtras(SpriteBatch spriteBatch)
        {
        }

        private void DrawThingButtons(SpriteBatch spriteBatch)
        {
            foreach (var button in ThingButtons.Values)
            {
                button.Draw(spriteBatch);
            }
        }
    }
}