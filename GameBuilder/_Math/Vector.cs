using System;

namespace GameBuilder._Math
{
    internal class Vector
    {
        public float x;
        public float y;

        public Vector()
        {

        }

        public Vector copy()
        {
            return new Vector(x,y);
        }

        public float magnitude()
        {
            return (float) Math.Sqrt(x * x + y * y);
        }


        public static float Distance(Vector a, Vector b)
        {
            float Fx = a.x - b.x;
            float Fy = a.y - b.y;

            float aK = (float) Math.Pow((double) Fx, 2);
            float bK = (float) Math.Pow((double)Fy, 2);

            return (float) Math.Sqrt(aK + bK);
        }
        public Vector Round()
        {
            float X = (float) Math.Round(x);
            float Y = (float) Math.Round(y);

            return new Vector(X,Y);
        }

        public Vector(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector operator -(Vector a)
        {
            return new Vector(-a.x, -a.y);
        }

        public static Vector operator +(Vector a, Vector b)
        {
            float x = a.x + b.x;
            float y = a.y + b.y;
            return new Vector(x, y);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            float x = a.x - b.x;
            float y = a.y - b.y;
            return new Vector(x, y);
        }

        public static Vector operator *(Vector a, float b)
        {
            float x = a.x * b;
            float y = a.y * b;
            return new Vector(x, y);
        }

        public static Vector operator *(Vector a, Vector b)
        {
            float x = a.x * b.x;
            float y = a.y * b.y;
            return new Vector(x, y);
        }

        public static Vector operator /(Vector a, Vector b)
        {
            float x = a.x / b.x;
            float y = a.y / b.y;
            return new Vector(x, y);
        }

        public static Vector operator /(Vector a, float b)
        {
            float x = a.x / b;
            float y = a.y / b;
            return new Vector(x, y);
        }

        public void normalize()
        {
            float magnitude = this.magnitude();

            if (magnitude != 0)
            {
                this.x /= magnitude;
                this.y /= magnitude;
            }
        }
    }
}
