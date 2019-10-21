using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Station_13
{
    public class Button2D
    {
        private Vector2 position;
        public Rectangle hitbox;
        //private string text;
        //private Color btnColor;
        public int offset { get; set; }
        private Texture2D btnTexture;
        private SpriteBatch spriteBatch;
        private float scale;
        public bool isClicked, isHovering, isDefault;

        public Button2D(Texture2D btnTexture, Vector2 position, SpriteBatch spriteBatch, float scale = 1f)
        {
            this.btnTexture = btnTexture;
            this.position = position;
            hitbox = new Rectangle((int)position.X, (int)position.Y, btnTexture.Width * (int)scale, btnTexture.Height * (int)scale);
            this.spriteBatch = spriteBatch;
            this.scale = scale;
            isDefault = true;
            
        }
        public Button2D(Texture2D btnTexture, Rectangle rectangle, Vector2 position, SpriteBatch spriteBatch, float scale = 1f)
        {
            this.btnTexture = btnTexture;
            this.position = position;
            hitbox = rectangle;
            this.spriteBatch = spriteBatch;
            this.scale = scale;
            isDefault = true;

        }
        public void Update(MouseState m)
        {
            if (hitbox.Contains(new Point(m.Position.X, m.Position.Y + offset)))
            {
                if (m.LeftButton == ButtonState.Pressed)
                {
                    isClicked = true;
                    isDefault = false;
                    isHovering = false;
                }
                else
                {
                    isDefault = false;
                    isClicked = false;
                    isHovering = true;
                }
            }
            else
            {
                isDefault = true;
                isClicked = false;
                isHovering = false;
            }
        }
        public void Update(MouseState m, Vector2 position)
        {
            if (hitbox.Contains(new Point(m.Position.X, m.Position.Y + offset)))
            {
                if (m.LeftButton == ButtonState.Pressed)
                {
                    isClicked = true;
                    isDefault = false;
                    isHovering = false;
                }
                else
                {
                    isDefault = false;
                    isClicked = false;
                    isHovering = true;
                }
            }
            else
            {
                isDefault = true;
                isClicked = false;
                isHovering = false;
            }
            this.position = position;
        }
        public void Draw()
        {
            Color color;
            if (isDefault)
                color = Color.White;
            else if (isHovering)
                color = Color.DarkGray;
            else if (isClicked)
                color = Color.Gray;
            else
                color = Color.White;
#pragma warning disable
            spriteBatch.Draw(btnTexture, position, color: color, scale: new Vector2(scale));
        }
    }
}
