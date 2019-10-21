using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Station_13
{
    public class DefaultAtoms
    {
        public int Amount = 4;
        public Atom GreyTile = C(Type.turf, ID.GreyTile);
        public Atom WhiteTile = C(Type.turf, ID.WhiteTile);
        public Atom Plating = C(Type.turf, ID.Plating);
        public Atom ReinforcedPlating = C(Type.turf, ID.ReinforcedPlating);
        public Atom[] AtomArray;
        public DefaultAtoms()
        {
            AtomArray = new Atom[]
            {
                GreyTile,
                WhiteTile,
                Plating,
                ReinforcedPlating
            };
        }

        private static Atom C(Type t, ID id)
        {
            return Extensions.CreateAtom(Vector2.Zero, t, id);
        }
    }
}
