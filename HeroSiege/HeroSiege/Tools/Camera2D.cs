using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.Tools
{
    class Camera2D
    { //Fields
        protected float zoom;
        protected Matrix transform;
        protected Matrix inverseTransform;

        protected Vector2 pos;
        protected Viewport ViewPort { get; set; }

        public float Zoom_Max = 3.0f;
        public float Zoom_Min = 0.1f;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="viewport"></param>
        public Camera2D(Viewport viewport)
        {
            zoom = 2.0f;
            pos = Vector2.Zero;
            ViewPort = viewport;
        }


        //Methods
        public void Update()
        {
            ClampPosValues(480, 2080, 0, 4565);

            //Create view matrix
            transform = Matrix.CreateTranslation(new Vector3((float)Math.Round(-pos.X,1), (float)Math.Round(-pos.Y,1), 0)) *
                        Matrix.CreateRotationZ(0) *
                        Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
                        Matrix.CreateTranslation(new Vector3(ViewPort.Width / 2, ViewPort.Height / 2, 0));

            //Update inverse matrix
            inverseTransform = Matrix.Invert(transform);
        }

        /// <summary>
        /// Clamp the camera pos so that it will now show outside the map area
        /// </summary>
        /// <param name="Xmin"></param>
        /// <param name="XMax"></param>
        /// <param name="Ymin"></param>
        /// <param name="YMax"></param>
        public void ClampPosValues(float XMin, float XMax, float YMin, float YMax)
        { 
            pos.X = MathHelper.Clamp(pos.X, XMin, XMax);
            pos.Y = MathHelper.Clamp(pos.Y, YMin, YMax);
        }

        //Properties
        public float Zoom
        {
            get { return zoom; }
            set { zoom = value; }
        }
        public float GetWidth()
        {
            return ViewPort.Width;
        }
        public float GetHeight()
        {
            return ViewPort.Height;
        }

        /// <summary>
        /// Camera View Matrix Property
        /// </summary>
        public Matrix Transform
        {
            get { return transform; }
            set { transform = value; }
        }
        /// <summary>
        /// Inverse of the view matrix, can be used to get objects screen coordinates
        /// from its object coordinates
        /// </summary>
        public Matrix InverseTransform
        {
            get { return inverseTransform; }
        }

        public Vector2 Position
        {
            get { return pos; }
            set { pos = value; }
        }

        public Vector2 UnProject(float x, float y)
        {
            return Vector2.Transform(new Vector2(x, y), Matrix.Invert(transform));
        }
        public Vector2 Project(float x, float y)
        {
            return Vector2.Transform(new Vector2(x, y), transform);
        }
        public Vector2 GetViewPortScale()
        {
            return new Vector2(1, 1);
        }
    }
}
