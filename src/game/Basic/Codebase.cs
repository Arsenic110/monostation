using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Station_13
{

    public static class Codebase
    {
        public static Atom Click(Atom atom, Atom[][] array)
        {
            if (atom.entityInfo.clickable)
            {
                atom.entityInfo.onClick = !atom.entityInfo.onClick;
                if(atom.id == ID.Crate)
                {
                    if (atom.texture == atom.entityInfo.dynamicTexture[0])
                    {
                        atom.texture = atom.entityInfo.dynamicTexture[1];
                        atom.entityInfo.state = State.Open;

                    }
                    else
                    {
                        atom.texture = atom.entityInfo.dynamicTexture[0];
                        atom.entityInfo.state = State.Closed;
                    }
                }
            }

            return atom;
        }
    }
}