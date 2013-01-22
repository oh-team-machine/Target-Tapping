using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameLibrary.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class CheckBox
    {
        private Texture2D uncheckedIcon;
        private Texture2D checkedIcon;

        /// <summary>
        /// 
        /// </summary>
        public Rectangle Rect { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsClicked { get; set; }

        private bool bMouseDownInside;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="checkedIcon"></param>
        /// <param name="uncheckedIcon"></param>
        /// <param name="location"></param>
        public CheckBox(Texture2D checkedIcon, Texture2D uncheckedIcon, Vector2 location)
        {
            this.uncheckedIcon = uncheckedIcon;
            this.checkedIcon = checkedIcon;

            Rect = new Rectangle((int)location.X, (int)location.Y, uncheckedIcon.Width, uncheckedIcon.Height);

            bMouseDownInside = false;
            IsClicked = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        public void Update(MouseState state)
        {
            if (Collisions.CollisionWithMouse(Rect, state))
            {
                if (state.LeftButton == ButtonState.Released && bMouseDownInside)
                {
                    IsClicked = !IsClicked;
                }

                if (state.LeftButton == ButtonState.Pressed)
                    bMouseDownInside = true;
                else
                    bMouseDownInside = false;
            }
            else
                bMouseDownInside = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsClicked)
            {
                spriteBatch.Draw(checkedIcon, Rect, Color.White);
            }
            else
                spriteBatch.Draw(uncheckedIcon, Rect, Color.White);
        }
    }
}
