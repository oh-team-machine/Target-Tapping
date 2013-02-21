using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameLibrary.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class NumericUpDown
    {
        /// <summary>
        /// 
        /// </summary>
        public Decimal Increment{ get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Decimal Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal Maximum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal Minimum { get; set; }

        private Label numberLabel;

        private Vector2 position;

        private float width;

        private Texture2D background;

        private Color highlightColor;

        private SpriteFont font;

        private String plus = "+";
        private String minus = "-";

        private Rectangle plusRect;
        private Rectangle minusRect;

        private double scrollTime;
        private bool bScrolling;

        private bool plusButtonDownInside;
        private bool minusButtonDownInside;

        private const double ScrollTimer = 200;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="background"></param>
        /// <param name="position"></param>
        /// <param name="alignment"></param>
        /// <param name="width"></param>
        /// <param name="font"></param>
        /// <param name="Increment"></param>
        /// <param name="Value"></param>
        /// <param name="Maximum"></param>
        /// <param name="Minimum"></param>
        public NumericUpDown(Texture2D background, Vector2 position, int alignment, float width, SpriteFont font, Decimal Increment = 1m, Decimal Value = 0m, Decimal Maximum = 10m, Decimal Minimum = 0m)
        {
            this.Increment = Increment;
            this.Value = Value;
            this.Maximum = Maximum;
            this.Minimum = Minimum;

            this.background = background;
            this.position = position;
            this.width = width;
            this.font = font;

            plusButtonDownInside = false;
            minusButtonDownInside = false;

            numberLabel = new Label(Value.ToString(), this.position, alignment, this.width, this.font);
            
            Vector2 rectSize = this.font.MeasureString(plus);

            plusRect = new Rectangle((int)this.position.X + (int)width + 5, (int)this.position.Y + (int)(rectSize.Y * 0.75f), (int)(rectSize.X * 2.5f), (int)rectSize.Y);
            minusRect = new Rectangle((int)this.position.X + (int)width + (int)(rectSize.X * 2.5f) + 7, (int)this.position.Y + (int)(rectSize.Y * 0.75f), (int)(rectSize.X * 2.5f), (int)rectSize.Y);

            highlightColor = Color.White;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="mouseState"></param>
        public void Update(GameTime gameTime, MouseState mouseState)
        {
            if (bScrolling)
            {
                scrollTime += gameTime.ElapsedGameTime.TotalMilliseconds;

                if (scrollTime >= ScrollTimer)
                {
                    bScrolling = false;
                    scrollTime = 0;
                }
            }

            if (GameLibrary.Collisions.CollisionWithMouse(plusRect, mouseState))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    plusButtonDownInside = true;

                    if (!bScrolling)
                    {
                        Decimal temp = Value;
                        temp += Increment;

                        if (temp <= Maximum)
                            Value = temp;

                        bScrolling = true;

                        numberLabel.SetText(Value.ToString());
                    }
                }

                else
                {
                    plusButtonDownInside = false;
                }
            }

            else if (GameLibrary.Collisions.CollisionWithMouse(minusRect, mouseState))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    minusButtonDownInside = true;

                    if (!bScrolling)
                    {
                        Decimal temp = Value;
                        temp -= Increment;

                        if (temp >= Minimum)
                            Value = temp;

                        bScrolling = true;

                        numberLabel.SetText(Value.ToString());
                    }
                }

                else
                {
                    minusButtonDownInside = false;
                }
            }

            else
            {
                plusButtonDownInside = false;
                minusButtonDownInside = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!plusButtonDownInside)
            {
                spriteBatch.Draw(background, plusRect, Color.SlateGray);
            }
            else
            {
                spriteBatch.Draw(background, plusRect, Color.DarkSlateGray);
            }

            if (!minusButtonDownInside)
            {
                spriteBatch.Draw(background, minusRect, Color.SlateGray);
            }
            else
            {
                spriteBatch.Draw(background, minusRect, Color.DarkSlateGray);
            }

            spriteBatch.DrawString(font, plus, new Vector2((plusRect.X + 8), plusRect.Y), highlightColor);
            spriteBatch.DrawString(font, minus, new Vector2((minusRect.X + 8), plusRect.Y), highlightColor);

            numberLabel.Draw(spriteBatch);
        }
    }
}
