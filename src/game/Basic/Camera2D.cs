using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Station_13
{
    public class Camera2D
    {
        protected float _zoom;
        public Matrix _transform;
        public Vector2 _pos;
        protected float _rotation;
        public GraphicsDevice graphicsDevice;

        public Camera2D(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            _zoom = 1.0f;
            _rotation = 0.0f;
            _pos = Vector2.Zero;
        }
        /// <summary>
        /// Get and set zoom.
        /// </summary>
        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; if (_zoom < 0.1f) _zoom = 0.1f; } // Negative zoom will flip image
        }

        /// <summary>
        /// Get and set rotation.
        /// </summary>
        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        /// <summary>
        /// Get and set position.
        /// </summary>
        public Vector2 Position
        {
            get { return _pos; }
            set { _pos = value; }
        }

        /// <summary>
        /// Get the transformation matrix.
        /// </summary>
        /// <returns></returns>
        public Matrix get_transformation()
        {
            _transform =
              Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                         Matrix.CreateRotationZ(_rotation) *
                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                         Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
            return _transform;
        }
    }
}
