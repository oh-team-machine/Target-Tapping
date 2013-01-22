using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameLibrary.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class List
    {
        #region Variables

        private List<string> listElements;
        private List<string> drawnElements;

        private List<Rectangle> listRects;

        private int height;
        private int width;

        private int elementHeight;
        private int numElements;

        private Vector2 position;

        private int selectedIndex;
        private int drawStartIndex;

        private Rectangle upArrowRect;
        private Rectangle downArrowRect;

        private Texture2D background;

        private Rectangle listBackground;

        private SpriteFont font;

        private Color highlightColor;
        private Color textColor;

        private String upArrow = char.ConvertFromUtf32(8593);
        private String downArrow = char.ConvertFromUtf32(8595);

        private const double ScrollTimer = 200;

        private double scrollTime;
        private bool bScrolling;
        private bool upButtonDownInside;
        private bool downButtonDownInside;

        #endregion Variables

        /// <summary>
        /// 
        /// </summary>
        public List(List<string> listElements, int width, int numElements, int elementHeight, Vector2 position, Texture2D background, SpriteFont font)
        {
            this.listElements = listElements;
            this.width = width;
            this.numElements = numElements;
            this.elementHeight = elementHeight;
            this.position = position;
            this.background = background;
            this.font = font;

            this.height = elementHeight * (numElements + 2);

            this.selectedIndex = -1;
            this.drawStartIndex = 0;

            bScrolling = false;
            scrollTime = 0;

            initializeRectList();
            initializeDrawList();

            upArrowRect = new Rectangle((int)position.X, (int)position.Y, width, elementHeight);
            downArrowRect = new Rectangle((int)position.X, (int)position.Y + height - elementHeight, width, elementHeight);

            listBackground = new Rectangle((int)position.X, (int)position.Y, width, height);

            highlightColor = Color.White;
            textColor = Color.Black;
        }

        /// <summary>
        /// 
        /// </summary>
        public List(List<string> listElements, int width, int numElements, int elementHeight, Vector2 position, Texture2D background, SpriteFont font, Color textColor, Color highlightColor)
        {
            this.listElements = listElements;
            this.width = width;
            this.numElements = numElements;
            this.elementHeight = elementHeight;
            this.position = position;
            this.background = background;
            this.font = font;
            this.highlightColor = highlightColor;
            this.textColor = textColor;

            this.height = elementHeight * (numElements + 2);

            this.selectedIndex = -1;
            this.drawStartIndex = 0;

            bScrolling = false;
            scrollTime = 0;

            initializeRectList();
            initializeDrawList();

            upArrowRect = new Rectangle((int)position.X, (int)position.Y, width, elementHeight);
            downArrowRect = new Rectangle((int)position.X, (int)position.Y + height - elementHeight, width, elementHeight);

            listBackground = new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        /// <summary>
        /// 
        /// </summary>
        private void initializeRectList()
        {
            listRects = new List<Rectangle>(numElements);

            Rectangle temp;
            for (int i = 0; i < numElements; i++)
            {
                temp = new Rectangle((int)position.X, (int)position.Y + (elementHeight * (i + 1)), width, elementHeight);

                listRects.Add(temp);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void initializeDrawList()
        {
            drawnElements = new List<string>(numElements);

            for (int i = drawStartIndex; i < numElements + drawStartIndex; i++)
            {
                if (i >= listElements.Count || i < 0)
                    break;
                else
                    drawnElements.Add(listElements[i]);
            }
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

            if (GameLibrary.Collisions.CollisionWithMouse(upArrowRect, mouseState))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    upButtonDownInside = true;

                    if (!bScrolling)
                    {
                        drawStartIndex--;
                        selectedIndex = -1;

                        drawStartIndex = (int)MathHelper.Clamp(drawStartIndex, 0, (listElements.Count - numElements));

                        initializeDrawList();

                        bScrolling = true;
                    }
                }

                else
                {
                    upButtonDownInside = false;
                }
            }

            else if (GameLibrary.Collisions.CollisionWithMouse(downArrowRect, mouseState))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    downButtonDownInside = true;

                    if (!bScrolling)
                    {
                        drawStartIndex++;
                        selectedIndex = -1;

                        drawStartIndex = (int)MathHelper.Clamp(drawStartIndex, 0, (listElements.Count - numElements));

                        initializeDrawList();

                        bScrolling = true;
                    }
                }

                else
                {
                    downButtonDownInside = false;
                }
            }

            else
            {
                upButtonDownInside = false;
                downButtonDownInside = false;
            }

            Rectangle rect;

            for (int i = 0; i < numElements; i++)
            {
                rect = listRects[i];

                if (GameLibrary.Collisions.CollisionWithMouse(rect, mouseState))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        selectedIndex = i;

                        if (i >= listElements.Count)
                            selectedIndex = listElements.Count - 1;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!upButtonDownInside)
            {
                spriteBatch.Draw(background, upArrowRect, Color.SlateGray);
            }
            else
            {
                spriteBatch.Draw(background, upArrowRect, Color.DarkSlateGray);
            }

            if (!downButtonDownInside)
            {
                spriteBatch.Draw(background, downArrowRect, Color.SlateGray);
            }
            else
            {
                spriteBatch.Draw(background, downArrowRect, Color.DarkSlateGray);
            }

            spriteBatch.DrawString(font, upArrow, new Vector2(upArrowRect.X, upArrowRect.Y), highlightColor);
            spriteBatch.DrawString(font, downArrow, new Vector2(downArrowRect.X, downArrowRect.Y), highlightColor);

            for (int i = 0; i < numElements; i++)
            {
                if (i == selectedIndex)
                {
                    spriteBatch.Draw(background, listRects[i], Color.Blue);

                    if (i < drawnElements.Count)
                        spriteBatch.DrawString(font, drawnElements[i], new Vector2(listRects[i].X, listRects[i].Y), highlightColor);
                }
                else
                {
                    spriteBatch.Draw(background, listRects[i], Color.White);

                    if (i < drawnElements.Count)
                        spriteBatch.DrawString(font, drawnElements[i], new Vector2(listRects[i].X, listRects[i].Y), textColor);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int SelectedIndex()
        {
            return drawStartIndex + selectedIndex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public String SelectedElement()
        {
            if(selectedIndex == -1)
                return "";
            else
                return listElements[SelectedIndex()];
        }
    }
}
