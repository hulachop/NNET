using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public class Vector
    {
        float[] values;
        public readonly int size;
        public Vector(int _size)
        {
            values = new float[_size];
            size = _size;
        }
        public float this[int index]
        {
            get { return values[index]; }
            set { values[index] = value; }
        }
        public static Vector operator *(float a, Vector b)
        {
            Vector v = new Vector(b.size);
            for(int i = 0; i < b.size; i++)
            {
                v[i] = b[i] * a;
            }
            return v;
        }
        public static Vector operator +(Vector a, Vector b)
        {
            if(a.size!=b.size) throw new ArgumentOutOfRangeException();
            Vector v = new Vector(a.size);
            for(int i = 0; i < a.size; i++)
            {
                v[i] = a[i] + b[i];
            }
            return v;
        }
        public static Vector operator -(Vector a, Vector b)
        {
            if (a.size != b.size) throw new ArgumentOutOfRangeException();
            Vector v = new Vector(a.size);
            for (int i = 0; i < a.size; i++)
            {
                v[i] = a[i] - b[i];
            }
            return v;
        }
        public static float Dot(Vector a, Vector b)
        {
            if (a.size != b.size) throw new ArgumentOutOfRangeException();
            float value = 0;
            for(int i = 0; i < a.size; i++)
            {
                value += a[i] * b[i];
            }
            return value;
        }

        public static implicit operator Matrix(Vector v)
        {
            Matrix m = new Matrix(v.size, 1);
            m[0] = v;
            return m;
        }
        public static implicit operator Vector(float[] f)
        {
            Vector v = new Vector(f.Length);
            for (int i = 0; i < f.Length; i++) v[i] = f[i];
            return v;
        }
    }
}
