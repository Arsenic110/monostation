using System.Runtime.Serialization;
using Microsoft.Xna.Framework;

namespace Space_Station_13
{
    public class ColorSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Color c = (Color)obj;
            byte r = c.R, g = c.G, b = c.B, a = c.A;
            info.AddValue("R", r);
            info.AddValue("G", g);
            info.AddValue("B", b);
            info.AddValue("A", a);
            return;
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Color c = new Color(info.GetByte("R"), info.GetByte("G"), info.GetByte("B"), info.GetByte("A"));
            //System.Windows.Forms.MessageBox.Show(c.ToString());
            return c;
        }
    }
}
