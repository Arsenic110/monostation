using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Space_Station_13
{
    public class AtomPicker
    {
        public Button2D[] buttons;
        public AtomPicker()
        {
            DefaultAtoms dA = new DefaultAtoms();
            buttons = new Button2D[dA.Amount];
            Vector2 position = new Vector2(Game1.textView.X + 15, Game1.textView.Y + 15);

            for(int i = 0; i < dA.Amount; i++)
            {
                
                buttons[i] = new Button2D(dA.AtomArray[i].texture, position, Game1.spriteBatch);

                if (position.X > Game1.textView.X + Game1.textView.Width)
                {
                    position.X = Game1.textView.X + 15;
                    position.Y += dA.AtomArray[i].texture.Height + 15;
                }
                else
                    position.X += dA.AtomArray[i].texture.Width + 15;
                if (position.Y > Game1.textView.Y + Game1.textView.Height)
                    position.Y = Game1.textView.Y + 15;
                    
            }
        }
        public void Update(Microsoft.Xna.Framework.Input.MouseState m)
        {
            foreach(Button2D b in buttons)
            {
                b.Update(m);
            }
        }
        public void Update(Microsoft.Xna.Framework.Input.MouseState m, Vector2 v)
        {
            foreach (Button2D b in buttons)
            {
                b.Update(m, v);
            }
        }
        public void Draw()
        {
            foreach (Button2D b in buttons)
                b.Draw();
        }
    }
}
