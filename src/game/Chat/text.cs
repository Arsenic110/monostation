using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace Space_Station_13
{
    public class Text
    {
        public Rectangle bounds;
        public SpriteBatch spriteBatch;
        public SpriteFont font;
        public Text( Rectangle bounds, SpriteBatch spriteBatch, SpriteFont font)
        {
            this.bounds = bounds;
            this.spriteBatch = spriteBatch;
            this.font = font;
        }
        /// <summary>
        /// Wraps the text inside the bounds.
        /// </summary>
        /// <param name="text">The text to wrap.</param>
        /// <returns></returns>
        public string WrapText(string text)
        {
            string[] words = text.Split(' ');
            StringBuilder sb = new StringBuilder();
            float lineWidth = 0f;
            float spaceWidth = font.MeasureString(" ").X;
            Vector2 size;
            foreach(string word in words)
            {
                size = font.MeasureString(word);
                if(lineWidth + size.X < bounds.Width)
                {
                    sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    sb.Append("\n" + word + " ");
                    lineWidth = size.X + spaceWidth;
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// Draws the text at the upper-left corner of the bounds. Manual Method.
        /// </summary>
        /// <param name="t">The text to draw.</param>
        public void Draw(string t)
        {
            spriteBatch.DrawString(font, t, bounds.Location.ToVector2(), Color.Black);
        }
        /// <summary>
        /// Grammatical formatting.
        /// </summary>
        /// <param name="t">The text to format.</param>
        /// <returns></returns>
        public string FormatText(string t)
        {
            t = t[0].ToString().ToUpper() + t.Remove(0, 1);
            if (t[t.Length - 1] == ' ')
                t.Remove(t.Length - 1, 1);
            if (t[t.Length - 1] != '.' && t[t.Length - 1] != '?' && t[t.Length - 1] != '!')
                t += ".";
            return t;
        }
    }
}
