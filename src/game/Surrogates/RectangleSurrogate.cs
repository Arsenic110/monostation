using System.Runtime.Serialization;
using Microsoft.Xna.Framework;

namespace Space_Station_13
{
    public class RectangleSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Rectangle r = (Rectangle)obj;
            info.AddValue("X", r.X);
            info.AddValue("Y", r.Y);
            info.AddValue("Width", r.Width);
            info.AddValue("Height", r.Height);
            return;
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Rectangle r = (Rectangle)obj;
            r.X = info.GetInt32("X");
            r.Y = info.GetInt32("Y");
            r.Width = info.GetInt32("Width");
            r.Height = info.GetInt32("Height");
            return r;
        }
    }
}
