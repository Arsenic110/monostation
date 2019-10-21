using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Reflection;

namespace Space_Station_13
{
    public static class Extensions
    {
        public static Texture2D draw2D;
        //public static Atom[] atoms;

        /// <summary>
        /// Draws an array of atoms in the world. Begin() must already be called. 
        /// </summary>
        /// <param name="type">The type of atom drawing this is.</param>
        /// <param name="atoms">The atoms to draw.</param>
        /// <param name="spriteBatch">The spritebatch to draw with.</param>
        public static void Mapper(Type type, Atom[] atoms, Vector2 offset, SpriteBatch spriteBatch)
        {
            Vector2 position;
            for (int i = 0; i < atoms.Length; i++)
            {
                position = new Vector2(atoms[i].rectangle.Location.ToVector2().X + offset.X,
                    atoms[i].rectangle.Location.ToVector2().Y + offset.Y);

                if (position.Distance(Game1.camera2D.Position) < Game1.graphicsDevice.Viewport.Width &&
                    atoms[i].health > 0 && atoms[i].id != ID.Space)
                {
                    Draw(atoms[i], position, spriteBatch, atoms[i].color);
                }
                atoms[i].pos = position;
            }

        }
        /// <summary>
        /// The make-do version to generate an atom array from a basic texture, and the accompanying color table.
        /// </summary>
        /// <param name="type">The type of atoms to generate.</param>
        /// <param name="map">The colormapped texture to create from.</param>
        /// <param name="colorTablePath">The path to the colortable file.</param>
        /// <returns></returns>
        public static Atom[] GenerateAtoms(Type type, Texture2D map, string colorTablePath)
        {
            int multiplier = 32;
            if (type == Type.item)
                multiplier = 8;
            ColorTable[] colorTable;
            string[] colortablecont;
            Color[] mapColorData = new Color[map.Width * map.Height];
            Atom[] atoms = new Atom[mapColorData.Length];
            map.GetData(mapColorData);
            colortablecont = File.ReadAllLines(colorTablePath);
            colorTable = new ColorTable[colortablecont.Length];
            for (int i = 0; i < colortablecont.Length; i++)
            {
                string[] rgbValues;
                rgbValues = colortablecont[i].Split(new char[] { ',', '=' });
                colorTable[i].color = new Color(Convert.ToInt32(rgbValues[0]), Convert.ToInt32(rgbValues[1]), Convert.ToInt32(rgbValues[2]));
                colorTable[i].id = (ID)Enum.Parse(typeof(ID), rgbValues[3], true);
            }
            int x = 0, y = 0;
            for (int i = 0; i < mapColorData.Length; i++)
            {
                for (int n = 0; n < colorTable.Length; n++)
                {
                    if (mapColorData[i] == colorTable[n].color)
                    {
                        atoms[i].id = colorTable[n].id;
                        atoms[i].rectangle = new Rectangle(x * multiplier, y * multiplier, multiplier, multiplier);

                        atoms[i] = PopulateAtom(atoms[i]);
                    }
                }
                if (x >= map.Width - 1)
                {
                    x = 0;
                    y++;
                }
                else
                    x++;
                if (y > map.Height)
                    break;
            }
            return atoms;
        }
        /// <summary>
        /// Draws the atom by ID in the specified position.
        /// </summary>
        /// <param name="id">The ID of the atom to draw.</param>
        /// <param name="position">The position to draw at.</param>
        /// <param name="spriteBatch">The spritebatch to draw with.</param>
        public static void Draw(Atom atom, Vector2 position, SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(atom.texture, position, Color.White);

        }
        /// <summary>
        /// Draws the atom by ID in the specified position.
        /// </summary>
        /// <param name="id">The ID of the atom to draw.</param>
        /// <param name="rectangle">The position to draw at.</param>
        /// <param name="spriteBatch">The spritebatch to draw with.</param>
        public static void Draw(Atom atom, Rectangle rectangle, SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(atom.texture, rectangle, Color.White);

        }
        /// <summary>
        /// Draws the atom by ID in the specified position.
        /// </summary>
        /// <param name="id">The ID of the atom to draw.</param>
        /// <param name="rectangle">The position to draw at.</param>
        /// <param name="spriteBatch">The spritebatch to draw with.</param>
        public static void Draw(Atom atom, Vector2 rectangle, SpriteBatch spriteBatch, Color color)
        {

            spriteBatch.Draw(atom.texture, rectangle, color);

        }
        /// <summary>
        /// Gets the distance between the two vectors.
        /// </summary>
        /// <param name="origin">The first vector.</param>
        /// <param name="point">The second vector</param>
        /// <returns></returns>
        public static double Distance(this Vector2 origin, Vector2 point)
        {
            return Math.Sqrt(((point.X - origin.X) * (point.X - origin.X)) + ((point.Y - origin.Y) * (point.Y - origin.Y)));
        }
        /// <summary>
        /// Rounds off to lower base 32.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static float Nearest32(this float i)
        {
            float mod, re;
            if (i < 0)
                i += -32;
            mod = i % 32;
            re = i - mod;
            return re;
        }
        /// <summary>
        /// Populates the given atom with its default values, based off its type and ID.
        /// </summary>
        /// <param name="atom">The atom to populate.</param>
        /// <returns></returns>
        public static Atom PopulateAtom(Atom atom)
        {
            atom.health = 100;
            atom.hitbox = atom.rectangle;
            atom.color = Color.White;
            
            switch (atom.type)
            {
                case Type.turf:

                    break;
                case Type.wall:

                    break;
                case Type.entity:

                    break;
                case Type.item:

                    break;
            }
            switch (atom.id)
            {
                case ID.Space:
                    atom.health = byte.MaxValue;
                    atom.name = "Space";
                    break;
                case ID.Crate:
                    atom.name = "Metal Crate";
                    atom.type = Type.entity;
                    atom.entityInfo.clickable = true;
                    atom.entityInfo.dynamic = true;
                    atom.texture = Game1.crate_0;
                    atom.entityInfo.dynamicTexture = new Texture2D[] { Game1.crate_0, Game1.crateopen_0 };
                    atom = RemoveTransparency(atom);
                    atom.entityInfo.state = State.Closed;
                    atom.methods = new MethodInfo[] { typeof(Codebase).GetMethod("Click") };
                    break;
                case ID.GreyTile:
                    atom.name = "Grey Tile";
                    atom.texture = Game1.greyTile;
                    break;
                case ID.WhiteTile:
                    atom.name = "White Tile";
                    atom.texture = Game1.whiteTile;
                    break;
                case ID.Plating:
                    atom.name = "Plating";
                    atom.texture = Game1.plating;
                    break;
                case ID.ReinforcedPlating:
                    atom.name = "Reinforced Plating";
                    atom.texture = Game1.plating;
                    break;
                case ID.PlasteelWall:
                    atom.name = "Reinforced Wall";
                    atom.texture = Game1.plasteelWallSingle;
                    break;
                case ID.OrangeGrass:
                    atom.name = "Orange Grass";
                    atom.texture = Game1.greygrass_0;
                    atom.color = Color.Orange;
                    break;
                case ID.GreenGrass:
                    atom.name = "Green Grass";
                    atom.texture = Game1.greygrass_0;
                    atom.color = Color.Green;
                    break;

            }

            return atom;

        }
        /// <summary>
        /// Launches the atom's built-in methodinfo object.
        /// </summary>
        /// <param name="atom">The atom to launch.</param>
        public static void Launch(Atom atom, Atom[][] array)
        {
            if (atom.type == Type.entity && atom.entityInfo.clickable)
            {
                foreach (MethodInfo method in atom.methods)
                {
                    method.Invoke(null, new object[] { atom, array });
                }
            }

        }
        /// <summary>
        /// A smaller method for loading a texture2d from a file directly.
        /// </summary>
        /// <param name="path">The path to the texture file to load.</param>
        /// <returns></returns>
        public static Texture2D Load(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
                return Texture2D.FromStream(Game1.graphicsDevice, fs);
        }
        /// <summary>
        /// Removes the transparency of atom.texture, puts it into atom.rawTexture, and adjusts the hitbox.
        /// </summary>
        /// <param name="atom">The atom to perform the operations on.</param>
        /// <returns></returns>
        public static Atom RemoveTransparency(Atom atom)
        {
            Texture2D rawTexture = atom.texture;
            Color[] rawTextureColors = new Color[rawTexture.Width * rawTexture.Height];
            rawTexture.GetData(rawTextureColors);
            Color[] textureColors;
            Texture2D texture;
            int transparentPixels = 0;
            int pixels = 0;
            int x = 0;
            int y = 0;
            int oldx = 0, oldy = 0;
            int newx = 0, newy = 0;
            for (int i = 0; i < rawTextureColors.Length; i++)
            {
                if (rawTextureColors[i] == new Color(0, 0, 0, 0))
                    transparentPixels++;
                else
                {
                    pixels++;
                    if (oldx == 0 && oldy == 0)
                    {
                        oldx = x;
                        oldy = y;
                    }
                    else
                    {
                        newx = x;
                        newy = y;
                    }
                }

                if (x >= rawTexture.Width - 1)
                {
                    x = 0;
                    y++;
                }
                else
                    x++;
                if (y > rawTexture.Height)
                    break;
            }
            newx -= oldx;
            newy -= oldy;
            newx++;
            newy++;
            atom.hitbox.X += oldx;
            atom.hitbox.Y += oldy;

            textureColors = new Color[pixels];
            texture = new Texture2D(Game1.graphicsDevice, newx, newy);
            texture.SetData(textureColors);
            atom.rawTexture = texture;
            atom.hitbox = new Rectangle(atom.rectangle.X + oldx, atom.rectangle.Y + oldy, atom.rawTexture.Width, atom.rawTexture.Height);
            return atom;
        }
        /// <summary>
        /// Removes the transparency from the texture.
        /// </summary>
        /// <param name="texture2D">The texture.</param>
        /// <returns></returns>
        public static Texture2D RemoveTransparency(Texture2D texture2D)
        {
            Texture2D rawTexture = texture2D;
            Color[] rawTextureColors = new Color[rawTexture.Width * rawTexture.Height];
            rawTexture.GetData(rawTextureColors);
            Color[] textureColors;
            Texture2D texture;
            int transparentPixels = 0;
            int pixels = 0;
            int x = 0;
            int y = 0;
            int oldx = 0, oldy = 0;
            int newx = 0, newy = 0;
            for (int i = 0; i < rawTextureColors.Length; i++)
            {
                if (rawTextureColors[i] == new Color(0, 0, 0, 0))
                    transparentPixels++;
                else
                {
                    pixels++;
                    if (oldx == 0 && oldy == 0)
                    {
                        oldx = x;
                        oldy = y;
                    }
                    else
                    {
                        newx = x;
                        newy = y;
                    }
                }
                if (x >= rawTexture.Width - 1)
                {
                    x = 0;
                    y++;
                }
                else
                    x++;
                if (y > rawTexture.Height)
                    break;
            }
            newx -= oldx;
            newy -= oldy;
            newx++;
            newy++;
            textureColors = new Color[pixels];
            texture = new Texture2D(Game1.graphicsDevice, newx, newy);
            texture.SetData(textureColors);
            return texture;
        }
        /// <summary>
        /// Gets the information from the atom and returns it as a string.
        /// </summary>
        /// <param name="atom">The atom to get info from.</param>
        /// <returns></returns>
        public static string GetDebugInfo(Atom atom)
        {
            string info = "";
            if (atom.id != ID.Space)
                info = "ID: " + atom.id.ToString() + " of type: " + atom.type.ToString() +
                    "\nHitbox: " + atom.hitbox.ToString() + "\nRectangle: " + atom.rectangle.ToString() +
                    "\nPosition: " + atom.pos.ToString() + "\nTexture:\n" + atom.texture.Width + " " + atom.texture.Height;

            if (atom.id == ID.Crate)
            {
                info += "\nState: " + atom.entityInfo.state;
            }

            return info;
        }
        /// <summary>
        /// Adds the specified atom to the atom array, returns the array.
        /// </summary>
        /// <param name="array">The array to add to.</param>
        /// <param name="atom">The atom to add to the end of the array.</param>
        /// <returns></returns>
        public static Atom[] AddAtom(Atom[] array, Atom atom)
        {
            Atom[] re = new Atom[array.Length + 1];
            for (int i = 0; i < array.Length; i++)
            {
                re[i] = array[i];
            }
            re[re.Length - 1] = atom;
            return re;
        }
        /// <summary>
        /// Removes the specified atom from the atom array, returns the array. Returns the original if atom does not exist in array.
        /// </summary>
        /// <param name="array">The array to subtract from.</param>
        /// <param name="atom">The atom to subtract from the array.</param>
        /// <returns></returns>
        public static Atom[] RemoveAtom(Atom[] array, Atom atom)
        {
            Atom[] re = new Atom[array.Length - 1];
            int j = 0;
            for (int i = 0; i < re.Length; i++)
            {
                if (array[i].Equals(atom))
                    j++;
                if (i >= re.Length)
                    break;
                if (j >= array.Length)
                    break;
                //try
                {
                    re[i] = array[j];
                }
                //catch
                {
                }
                j++;
            }
            return re;

        }
        /// <summary>
        /// WIP. Gets the atom at the mouse position.
        /// </summary>
        /// <param name="array">The array to look through.</param>
        /// <param name="m">The MouseState object from which the position is taken.</param>
        /// <returns></returns>
        public static Atom GetAtomAtMouse(Atom[] array, Vector2 m)
        {
            foreach (Atom a in array)
            {
                if (a.hitbox.Contains(m))
                {
                    return a;
                }
            }

            return CreateAtom(array[0].pos, Type.turf, ID.Space);
        }
        /// <summary>
        /// Creates and populates an atom from the given parameters.
        /// </summary>
        /// <param name="m">The mouse position to create at.</param>
        /// <param name="type">The type of atom to create</param>
        /// <param name="ident">The ID of the atom</param>
        /// <returns></returns>
        public static Atom CreateAtom(Vector2 m, Type type, ID ident)
        {
            Atom atom = new Atom();
            int multiplier = 1;
            if (type == Type.item)
                multiplier = 8;

            atom.rectangle = new Rectangle((int)m.X.Nearest32() * multiplier, (int)m.Y.Nearest32() * multiplier, 32, 32);
            atom.id = ident;
            atom.type = type;
            atom = PopulateAtom(atom);
            return atom;
        }
        /// <summary>
        /// Gets the surrounding atoms.
        /// </summary>
        /// <param name="array">The array to search through.</param>
        /// <param name="atom">The atom to search around.</param>
        /// <returns></returns>
        public static Atom[] GetSurroundingAtoms(Atom[] array, Atom atom)
        {
            float x = atom.pos.X;
            float y = atom.pos.Y;
            Atom[] asdf = new Atom[8];
            asdf[0] = GetAtomAtMouse(array, new Vector2(x - 32, y - 32));
            asdf[1] = GetAtomAtMouse(array, new Vector2(x, y - 32));
            asdf[2] = GetAtomAtMouse(array, new Vector2(x + 32, y - 32));
            asdf[3] = GetAtomAtMouse(array, new Vector2(x - 32, y));
            //skip x, y because that should be original atom
            asdf[4] = GetAtomAtMouse(array, new Vector2(x + 32, y));
            asdf[5] = GetAtomAtMouse(array, new Vector2(x - 32, y + 32));
            asdf[6] = GetAtomAtMouse(array, new Vector2(x, y + 32));
            asdf[7] = GetAtomAtMouse(array, new Vector2(x + 32, y + 32));
            return asdf;
        }
        /// <summary>
        /// A specialized method to change wall alignment to make walls appear continuous.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="wall"></param>
        /// <returns></returns>
        public static Atom CorrectWallAlignment(Atom[] array, Atom wall)
        {
            Atom[] asdf = GetSurroundingAtoms(array, wall);

            bool top = false, left = false, right = false, bottom = false;

            if (asdf[1].id == ID.PlasteelWall)
                top = true;
            if (asdf[3].id == ID.PlasteelWall)
                left = true;
            if (asdf[4].id == ID.PlasteelWall)
                right = true;
            if (asdf[6].id == ID.PlasteelWall)
                bottom = true;

            //if (asdf[0].id == ID.PlasteelWall)
            //    topleft = true;
            //if (asdf[2].id == ID.PlasteelWall)
            //    topright = true;
            //if (asdf[5].id == ID.PlasteelWall)
            //    bottomleft = true;
            //if (asdf[7].id == ID.PlasteelWall)
            //    bottomright = true;

            //string s = "";
            //foreach (Atom a in asdf)
            //    s += a.id.ToString();
            //Debug(s);
            //Debug(one + " " + three + " " + four + " " + six);

            //double
            if (top && bottom && !left && !right)
            {
                wall.texture = Game1.plasteelWallTop_Bottom;
                return wall;
            }
            if (top && right && !left && !bottom)
            {
                wall.texture = Game1.plasteelWallTop_Right;
                return wall;
            }
            if (top && left && !bottom && !right)
            {
                wall.texture = Game1.plasteelWallTop_Left;
                return wall;
            }
            if (bottom && left && !top && !right)
            {
                wall.texture = Game1.plasteelWallBottom_Left;
                return wall;
            }
            if (bottom && right && !top && !left)
            {
                wall.texture = Game1.plasteelWallBottom_Right;
                return wall;
            }
            if (left && right && !top && !bottom)
            {
                wall.texture = Game1.plasteelWallLeft_Right;
                return wall;
            }
            if (!top && !left && !right && !bottom)
            {
                wall.texture = Game1.plasteelWallSingle;
                return wall;
            }
            //singles
            if(top && !left && !right && !bottom)
            {
                wall.texture = Game1.plasteelWallSingle_Top;
                return wall;
            }
            if(left && !top && !right && !bottom)
            {
                wall.texture = Game1.plasteelWallSingle_Left;
                return wall;
            }
            if(right && !top && !left && !bottom)
            {
                wall.texture = Game1.plasteelWallSingle_Right;
                return wall;
            }
            if(bottom && !top && !left && !right)
            {
                wall.texture = Game1.plasteelWallSingle_Bottom;
            }
            //t-junctions
            if(top && left && right && !bottom)
            {
                wall.texture = Game1.plasteelWallTTop_Right_Left;
                return wall;
            }
            if(top && bottom && left && !right)
            {
                wall.texture = Game1.plasteelWallTTop_Bottom_Left;
                return wall;
            }
            if(left && bottom && right && !top)
            {
                wall.texture = Game1.plasteelWallTBottom_Right_Left;
                return wall;
            }
            if(top && right && bottom && !left)
            {
                wall.texture = Game1.plasteelWallTTop_Bottom_Right;
                return wall;
            }
            //four
            if(top && left && bottom && right)
            {
                wall.texture = Game1.plasteelWallAll;
                return wall;
            }
            return wall;
        }
        /// <summary>
        /// Shows the data as a box to the screen.
        /// </summary>
        /// <param name="data"></param>
        public static void Debug(string data)
        {
            System.Windows.Forms.MessageBox.Show(data);
        }
        /// <summary>
        /// Adds the two colors together, including the alpha value.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Color Add(this Color x, Color y)
        {
            Color c = x;
            c.R += y.R;
            c.G += y.G;
            c.B += y.B;
            c.A += y.A;
            return c;
        }
        /// <summary>
        /// Adds two textures together. They must be of the same size.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Texture2D Add(this Texture2D x, Texture2D y)
        {
            Color[] xColors = new Color[x.Width * x.Height];
            Color[] yColors = new Color[y.Width * y.Height];
            Color[] finalColors = new Color[x.Width * y.Height];
            int Lenght = xColors.Length;
            if (xColors.Length != yColors.Length)
                return null;
            Texture2D t = new Texture2D(Game1.graphicsDevice, x.Width, x.Height);

            x.GetData(xColors);
            y.GetData(yColors);

            for(int i = 0; i < Lenght; i++)
            {
                finalColors[i] = xColors[i].Add(yColors[i]);
            }
            t.SetData(finalColors);
            return t;
        }
        /// <summary>
        /// Overlays the texture with the other specified texture. Must be the same size.
        /// </summary>
        /// <param name="source">The source texture to put the overlay over.</param>
        /// <param name="overlay">The texture to overlay over the source.</param>
        /// <returns></returns>
        public static Texture2D Overlay(this Texture2D source, Texture2D overlay)
        {
            Color[] sourceColors = new Color[source.Width * source.Height];
            Color[] overlayColors = new Color[overlay.Width * overlay.Height];
            Color[] finalColors = new Color[source.Width * overlay.Height];
            int Lenght = sourceColors.Length;
            if (sourceColors.Length != overlayColors.Length)
                return null;
            Texture2D t = new Texture2D(Game1.graphicsDevice, source.Width, source.Height);

            source.GetData(sourceColors);
            overlay.GetData(overlayColors);

            for (int i = 0; i < Lenght; i++)
            {
                if(overlayColors[i] == new Color(0, 0, 0, 0))
                {
                    finalColors[i] = sourceColors[i];
                }
                else
                {
                    finalColors[i] = overlayColors[i];
                }
                
            }
            t.SetData(finalColors);
            return t;
        }
        
    }



    [Serializable]
    public struct Atom
    {
        public Rectangle hitbox;
        public Texture2D texture;
        public Rectangle rectangle;
        public Color color;
        public string name;
        public Vector2 pos;
        public Texture2D rawTexture;
        public Type type;
        public ID id;
        public Turf turfInfo;
        public Entity entityInfo;
        public Item itemInfo;
        public Wall wallInfo;
        public byte health;
        public MethodInfo[] methods;
    }
    [Serializable]
    public struct Turf
    {

    }
    [Serializable]
    public struct Entity
    {
        public bool dynamic, clickable, onClick;
        public Texture2D[] dynamicTexture;
        public Rectangle[] dynamicHitbox;
        public State state;
    }
    [Serializable]
    public struct Item
    {

    }
    [Serializable]
    public struct Wall
    {

    }

    public struct ColorTable
    {
        public ID id;
        public Color color;
    }
    [Serializable]
    public enum Type
    {
        turf,
        entity,
        item,
        wall
    }
    [Serializable]
    public enum ID
    {
        Space,
        Plating,
        ReinforcedPlating,
        GreyTile,
        WhiteTile,
        PlasteelWall,
        Crate,
        OrangeGrass,
        GreenGrass
    }
    [Serializable]
    public enum State
    {
        Open,
        Closed
    }
}