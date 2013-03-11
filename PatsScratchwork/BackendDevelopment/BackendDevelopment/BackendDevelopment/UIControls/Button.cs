using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BackendDevelopment.UIControls
{
    /// <summary>
    /// A clickable element. Only responds to clicks that happen inside of its constraints. Customizeable size and icon.
    /// 
    /// This class takes care of all initialization, updates and drawing of this object.
    /// </summary>
    public class Button
    {
        private Texture2D icon;

        /// <summary>
        /// 
        /// </summary>
        public Rectangle Rect { get; set; }

        private bool bMouseDownInside;
        private bool bIsClicked;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="icon"></param>
        /// <param name="rect"></param>
        public Button(Texture2D icon, Rectangle rect)
        {
            this.icon = icon;
            this.Rect = rect;
            bMouseDownInside = false;
            bIsClicked = false;
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
                    bIsClicked = true;
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
            if (bMouseDownInside)
            {
                spriteBatch.Draw(icon, new Vector2(Rect.X + Rect.Width / 2, Rect.Y + Rect.Height / 2), null, Color.Blue, 0.0f, new Vector2(Rect.Width / 2f, Rect.Height / 2f), 0.8f, SpriteEffects.None, 0);
            }
            else
                spriteBatch.Draw(icon, Rect, Color.White);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsClicked()
        {
            bool temp = bIsClicked;

            bIsClicked = false;

            return temp;
        }
    }
}
