using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class Label
    {
        /// <summary>
        /// 
        /// </summary>
        public const int LEFT = 0;
        /// <summary>
        /// 
        /// </summary>
        public const int CENTER = 1;
        /// <summary>
        /// 
        /// </summary>
        public const int RIGHT = 2;

        private SpriteFont font;

        private Vector2 position;

        private float width;

        /// <summary>
        /// 
        /// </summary>
        public int Alignment { get; set; }

        private List<String> text;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="position"></param>
        /// <param name="alignment"></param>
        /// <param name="width"></param>
        /// <param name="font"></param>
        public Label(String text, Vector2 position, int alignment, float width, SpriteFont font)
        {
            this.position = position;
            this.width = width;
            this.font = font;
            this.Alignment = alignment;

            WordWrap(text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        private void WordWrap(String text)
        {
            this.text = new List<string>();

            Vector2 size;

            String[] words = text.Split(' ');

            StringBuilder sb = new StringBuilder();

            float lineWidth = 0f;

            float spaceWidth = this.font.MeasureString(" ").X;

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] == "\n")
                {
                    lineWidth = 0;

                    this.text.Add(sb.ToString());
                    sb.Clear();

                    this.text.Add(words[i]);
                }

                else
                {
                    size = font.MeasureString(words[i]);

                    if (lineWidth + size.X < this.width)
                    {
                        sb.Append(words[i]);

                        if (i != words.Length - 1)
                            sb.Append(" ");

                        lineWidth += size.X + spaceWidth;
                    }

                    else if (size.X > this.width)
                    {
                        CharWrap(words[i], sb, lineWidth);
                    }

                    else
                    {
                        this.text.Add(sb.ToString());

                        sb.Clear();

                        sb.Append(words[i]);

                        if (i != words.Length - 1)
                            sb.Append(" ");

                        lineWidth = size.X + spaceWidth;
                    }
                }
            }

            this.text.Add(sb.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <param name="sb"></param>
        /// <param name="lineWidth"></param>
        private void CharWrap(String word, StringBuilder sb, float lineWidth)
        {
            Vector2 size;

            for (int i = 0; i < word.Length; i++)
            {
                size = font.MeasureString(word.Substring(i, 1));

                if (lineWidth + size.X < this.width)
                {
                    sb.Append(word.Substring(i, 1));

                    lineWidth += size.X;
                }

                else
                {
                    this.text.Add(sb.ToString());

                    sb.Clear();

                    sb.Append(word.Substring(i, 1));

                    lineWidth = size.X;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public void SetText(String text)
        {
            WordWrap(text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = Vector2.Zero;
            Vector2 size;
            Vector2 drawPosition = this.position;

            for (int i = 0; i < this.text.Count(); i++)
            {
                String line = this.text[i];

                size = font.MeasureString(line);

                switch (Alignment)
                {
                    case LEFT:
                        break;
                    case CENTER:
                        origin = new Vector2((size.X / 2.0f), 0.0f);
                        drawPosition.X += (this.width / 2.0f);
                        break;
                    case RIGHT:
                        origin = new Vector2(size.X, 0.0f);
                        drawPosition.X += this.width;
                        break;
                }

                drawPosition.Y += (size.Y * 0.75f);

                spriteBatch.DrawString(this.font, line, drawPosition, Color.White, 0.0f, origin, 1.0f, SpriteEffects.None, 0.0f);

                drawPosition.X = this.position.X;
            }
        }
    }
}
