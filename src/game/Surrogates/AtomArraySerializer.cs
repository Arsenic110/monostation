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
    public class AtomArraySerializer : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Atom[] atom = (Atom[])obj;
            Extensions.Debug(atom.Length.ToString() + " Get");
            for (int i = 0; i < atom.Length; i++)
            {
                info.AddValue("atom" + i, atom[i]);
            }
            return;
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Atom[] atom = (Atom[])obj;
            Extensions.Debug(atom.Length.ToString() + " Set" + "\nInfotest: " + atom[0].hitbox.ToString());
            for (int i = 0; i < atom.Length; i++)
            {
                atom[i] = (Atom)info.GetValue("atom" + i, typeof(Atom));
            }

            return atom;
        }
        public static Atom[] SerializeAtom(Atom[] atom, string directory)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                SurrogateSelector surrogateSelector = new SurrogateSelector();
                surrogateSelector.AddSurrogate(typeof(Atom[]), new StreamingContext(StreamingContextStates.All), new AtomArraySerializer());
                surrogateSelector.AddSurrogate(typeof(Atom), new StreamingContext(StreamingContextStates.All), new AtomSerializer());
                surrogateSelector.AddSurrogate(typeof(Rectangle), new StreamingContext(StreamingContextStates.All), new RectangleSurrogate());
                surrogateSelector.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), new Vector2Surrogate());
                surrogateSelector.AddSurrogate(typeof(Texture2D), new StreamingContext(StreamingContextStates.All), new Texture2DSurrogate());
                surrogateSelector.AddSurrogate(typeof(Color), new StreamingContext(StreamingContextStates.All), new ColorSurrogate());
                IFormatter formatter = new BinaryFormatter();
                formatter.SurrogateSelector = surrogateSelector;
                try
                {
                    Extensions.Debug("Serializing...");
                    formatter.Serialize(memoryStream, atom);
                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("Failed. ");
                }

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                using (FileStream fs = new FileStream(directory + @"\savedata.atom", FileMode.Create))
                {
                    memoryStream.WriteTo(fs);
                    memoryStream.Seek(0, SeekOrigin.Begin);

                }
                using (FileStream fs = new FileStream(directory + @"\saveinfo", FileMode.Create))
                {
                    byte[] b = Encoding.UTF8.GetBytes(atom.Length.ToString());
                    fs.Write(b, 0, b.Length);
                }
                return DeserializeAtom(directory);
            }

        }
        public static Atom[] DeserializeAtom(string directory)
        {
            Atom[] a;

            if (!File.Exists(directory + @"\saveinfo"))
                throw new System.Exception("Missing saveinfo file.");

            string asdf = File.ReadAllText(directory + @"\saveinfo");
            a = new Atom[int.Parse(asdf)];
            //Extensions.Debug(a.Length.ToString());

            using (FileStream fs = new FileStream(directory + @"\savedata.atom", FileMode.Open, FileAccess.ReadWrite))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    fs.CopyTo(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    SurrogateSelector surrogateSelector = new SurrogateSelector();
                    surrogateSelector.AddSurrogate(typeof(Atom), new StreamingContext(StreamingContextStates.All), new AtomSerializer());
                    surrogateSelector.AddSurrogate(typeof(Rectangle), new StreamingContext(StreamingContextStates.All), new RectangleSurrogate());
                    surrogateSelector.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), new Vector2Surrogate());
                    surrogateSelector.AddSurrogate(typeof(Texture2D), new StreamingContext(StreamingContextStates.All), new Texture2DSurrogate());
                    surrogateSelector.AddSurrogate(typeof(Color), new StreamingContext(StreamingContextStates.All), new ColorSurrogate());
                    IFormatter formatter = new BinaryFormatter();
                    formatter.SurrogateSelector = surrogateSelector;
                    a = (Atom[])formatter.Deserialize(memoryStream);
                }
            }
            return a;
        }
    }
}
