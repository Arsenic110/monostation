using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace Space_Station_13
{
    public class Texture2DSurrogate : ISerializationSurrogate
    {

        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Texture2D t = (Texture2D)obj;
            info.AddValue("Width", t.Width);
            info.AddValue("Height", t.Height);
            Color[] colors = new Color[t.Width * t.Height];
            t.GetData(colors);
            for(int i = 0; i < colors.Length; i++)
                info.AddValue("Color" + i, colors[i]);
            return;
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Texture2D t = new Texture2D(Game1.graphicsDevice, info.GetInt32("Width"), info.GetInt32("Height"));
            Color[] colors = new Color[t.Width * t.Height];
            for (int i = 0; i < colors.Length; i++)
                colors[i] = (Color)info.GetValue("Color" + i, typeof(Color));
            t.SetData(colors);
            return t;
        }
    }
}