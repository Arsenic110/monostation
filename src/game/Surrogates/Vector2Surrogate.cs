using System.Runtime.Serialization;
using Microsoft.Xna.Framework;

namespace Space_Station_13
{
    public class Vector2Surrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Vector2 v = (Vector2)obj;
            info.AddValue("X", v.X);
            info.AddValue("Y", v.Y);
            return;
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Vector2 v = (Vector2)obj;
            v.X = info.GetSingle("X");
            v.Y = info.GetSingle("Y");
            return v;
        }
    }
}
