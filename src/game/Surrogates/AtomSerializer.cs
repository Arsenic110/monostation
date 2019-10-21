using System.Reflection;
using System.Text;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;

namespace Space_Station_13
{
    public class AtomSerializer : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Atom atom = (Atom)obj;
            info.AddValue("hitbox" , atom.hitbox);
            info.AddValue("rectangle" , atom.rectangle);
            info.AddValue("pos" , atom.pos);
            info.AddValue("texture" , atom.texture);
            info.AddValue("rawTexture" , atom.rawTexture);
            info.AddValue("type" , atom.type);
            info.AddValue("id" , atom.id);
            info.AddValue("turfInfo" , atom.turfInfo);
            info.AddValue("entityInfo" , atom.entityInfo);
            info.AddValue("itemInfo" , atom.itemInfo);
            info.AddValue("wallInfo" , atom.wallInfo);
            info.AddValue("health", atom.health);
            info.AddValue("methods", atom.methods);
            info.AddValue("name", atom.name);
            info.AddValue("color" , atom.color);

            return;
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {

            Atom atom = (Atom)obj;

            atom.hitbox = (Rectangle)info.GetValue("hitbox", typeof(Rectangle));
            atom.rectangle = (Rectangle)info.GetValue("rectangle", typeof(Rectangle));
            atom.pos = (Vector2)info.GetValue("pos", typeof(Vector2));
            atom.texture = (Texture2D)info.GetValue("texture", typeof(Texture2D));
            atom.rawTexture = (Texture2D)info.GetValue("rawTexture", typeof(Texture2D));
            atom.type = (Type)info.GetValue("type", typeof(Type));
            atom.id = (ID)info.GetValue("id", typeof(ID));
            atom.turfInfo = (Turf)info.GetValue("turfInfo", typeof(Turf));
            atom.entityInfo = (Entity)info.GetValue("entityInfo", typeof(Entity));
            atom.itemInfo = (Item)info.GetValue("itemInfo", typeof(Item));
            atom.wallInfo = (Wall)info.GetValue("wallInfo", typeof(Wall));
            atom.health = info.GetByte("health");
            atom.methods = (MethodInfo[])info.GetValue("methods", typeof(MethodInfo[]));
            atom.name = info.GetString("name");
            atom.color = (Color)info.GetValue("color", typeof(Color));




            return atom;
        }

        #region
        //public static Atom[] SerializeAtom(Atom[] atom, string directory)
        //{
        //    using(MemoryStream memoryStream = new MemoryStream())
        //    {
        //        SurrogateSelector surrogateSelector = new SurrogateSelector();
        //        surrogateSelector.AddSurrogate(typeof(Atom[]), new StreamingContext(StreamingContextStates.All), new AtomSerializer());
        //        surrogateSelector.AddSurrogate(typeof(Rectangle), new StreamingContext(StreamingContextStates.All), new RectangleSurrogate());
        //        surrogateSelector.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), new Vector2Surrogate());
        //        surrogateSelector.AddSurrogate(typeof(Texture2D), new StreamingContext(StreamingContextStates.All), new Texture2DSurrogate());
        //        surrogateSelector.AddSurrogate(typeof(Color), new StreamingContext(StreamingContextStates.All), new ColorSurrogate());
        //        IFormatter formatter = new BinaryFormatter();
        //        formatter.SurrogateSelector = surrogateSelector;
        //        try
        //        {
        //            Extensions.Debug("Serializing...");
        //            formatter.Serialize(memoryStream, atom);
        //        }
        //        catch
        //        {
        //            System.Windows.Forms.MessageBox.Show("Failed. ");
        //        }

        //        if (!Directory.Exists(directory))
        //        {
        //            Directory.CreateDirectory(directory);
        //        }
        //        else
        //        {
        //            Directory.Delete(directory, true);
        //            Directory.CreateDirectory(directory);
        //        }
        //        using(FileStream fs = new FileStream(directory + @"\savedata.atom", FileMode.Create))
        //        {
        //            memoryStream.WriteTo(fs);
        //            memoryStream.Seek(0, SeekOrigin.Begin);

        //        }
        //        using (FileStream fs = new FileStream(directory + @"\saveinfo", FileMode.Create))
        //        {
        //            byte[] b = Encoding.UTF8.GetBytes(atom.Length.ToString());
        //            fs.Write(b, 0, b.Length);
        //        }
        //        return DeserializeAtom(directory);//(Atom[])formatter.Deserialize(memoryStream);//DeserializeAtom(directory);
        //    }

        //}
        //public static Atom[] DeserializeAtom(string directory)
        //{
        //    Atom[] a;

        //    if (!File.Exists(directory + @"\saveinfo"))
        //        throw new System.Exception("Missing saveinfo file.");

        //    string asdf = File.ReadAllText(directory + @"\saveinfo");
        //    a = new Atom[int.Parse(asdf)];
        //    //Extensions.Debug(a.Length.ToString());

        //    using(FileStream fs = new FileStream(directory + @"\savedata.atom" , FileMode.Open, FileAccess.ReadWrite))
        //    {
        //        using(MemoryStream memoryStream = new MemoryStream())
        //        {
        //            fs.CopyTo(memoryStream);
        //            memoryStream.Seek(0, SeekOrigin.Begin);
        //            SurrogateSelector surrogateSelector = new SurrogateSelector();
        //            surrogateSelector.AddSurrogate(typeof(Atom[]), new StreamingContext(StreamingContextStates.All), new AtomSerializer());
        //            surrogateSelector.AddSurrogate(typeof(Rectangle), new StreamingContext(StreamingContextStates.All), new RectangleSurrogate());
        //            surrogateSelector.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), new Vector2Surrogate());
        //            surrogateSelector.AddSurrogate(typeof(Texture2D), new StreamingContext(StreamingContextStates.All), new Texture2DSurrogate());
        //            surrogateSelector.AddSurrogate(typeof(Color), new StreamingContext(StreamingContextStates.All), new ColorSurrogate());
        //            IFormatter formatter = new BinaryFormatter();
        //            formatter.SurrogateSelector = surrogateSelector;
        //            a = (Atom[])formatter.Deserialize(memoryStream);
        //        }
        //    }
        //    return a;
        //}
        #endregion
    }
}
