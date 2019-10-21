using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Space_Station_13
{
    public class Output
    {
        private List<string> log;
        private List<Color> colorList;
        private Text t;
        public Output(Text t)
        {
            this.t = t;
            log = new List<string>();
            colorList = new List<Color>();
        }
        /// <summary>
        /// Add a line to the log. Adds black by default.
        /// </summary>
        /// <param name="line">The line to add.</param>
        public void Add(string line)
        {
            log.Add(line);
            colorList.Add(Color.Black);
            return;
        }
        /// <summary>
        /// Add a line to the log, with a custom color.
        /// </summary>
        /// <param name="line">The line to add.</param>
        /// <param name="c">The color to add.</param>
        public void Add(string line, Color c)
        {
            log.Add(line);
            colorList.Add(c);
            return;
        }
        /// <summary>
        /// Draws the log in the specified location using the provided spritebatch and spritefont.
        /// </summary>
        /// <param name="location">The location to draw at.</param>
        /// <param name="f">The font to draw with.</param>
        /// <param name="s">The spritebatch to draw with.</param>
        public void Draw(Vector2 location, SpriteFont f, SpriteBatch s)
        {
            Vector2 lineDrawLocation = location;
            string[] logArray = log.ToArray();
            Color[] colorArray = colorList.ToArray();
            string st;
            int lineCountMultiPlier = 5; ;
            for(int i = 0; i < log.Count; i++)
            {
                if(lineDrawLocation.Y > 0)
                {
                    st = t.WrapText(logArray[i]);
                    s.DrawString(f, st, lineDrawLocation, colorArray[i]);
                    lineDrawLocation.Y -= lineCountMultiPlier * st.Split(new string[] { System.Environment.NewLine }, System.StringSplitOptions.None).Length;
                }
            }
        }
        /// <summary>
        /// Draws the log in the specified location using the spritebatch and spritefont from the Text object.
        /// </summary>
        /// <param name="location">The location to draw at.</param>
        public void Draw(Vector2 location)
        {
            SpriteBatch s = t.spriteBatch;
            SpriteFont f = t.font;
            if (log == null)
                return;
            Vector2 lineDrawLocation = location;
            string[] logArray = log.ToArray();
            //logArray
            System.Array.Reverse(logArray);
            Color[] colorArray = colorList.ToArray();
            System.Array.Reverse(colorArray);
            string st;
            int lineCountMultiPlier = 15;
            for (int i = 0; i < log.Count; i++)
            {
                if (lineDrawLocation.Y > 0)
                {
                    st = t.WrapText(logArray[i]);
                    lineDrawLocation.Y -= lineCountMultiPlier * st.Split(new string[] { System.Environment.NewLine }, System.StringSplitOptions.None).Length;
                    s.DrawString(f, st, lineDrawLocation, colorArray[i]);
                    //lineDrawLocation.Y -= lineCountMultiPlier * st.Split(new string[] { System.Environment.NewLine }, System.StringSplitOptions.None).Length;
                }
            }
        }
    }
}
