using System;
using System.Collections.Generic;
using System.Drawing;

namespace NNET
{
    [Serializable]
    public class Matrix
    {
        Vector[] values;
        public readonly int I, J;
        public Matrix(int _I, int _J)
        {
            I = _I;
            J = _J;
            values = new Vector[J];
            for (int i = 0; i < J; i++) values[i] = new Vector(I);
        }
        public float this[int i, int j]
        {
            get { return values[j][i]; }
            set { values[j][i] = value; }
        }
        public Vector this[int j]
        {
            get { return values[j]; }
            set { values[j] = value; }
        }
        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.J != b.J || a.I != b.I) throw new ArgumentOutOfRangeException();
            Matrix m = new Matrix(a.I, a.J);
            for (int j = 0; j < a.J; j++) m[j] = a[j] + b[j];
            return m;
        }
        public static Matrix operator -(Matrix a, Matrix b)
        {
            if (a.J != b.J || a.I != b.I) throw new ArgumentOutOfRangeException();
            Matrix m = new Matrix(a.I, a.J);
            for (int j = 0; j < a.J; j++) m[j] = a[j] - b[j];
            return m;
        }
        public static Vector operator *(Matrix a, Vector b)
        {
            if (a.J != b.size) throw new ArgumentOutOfRangeException();
            Vector v = new Vector(a.I);
            for(int j = 0; j < a.J; j++)
            {
                v += b[j] * a[j];
            }
            return v;
        }
        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.J != b.I) throw new ArgumentOutOfRangeException();
            Matrix m = new Matrix(a.I, b.J);
            for(int j = 0; j < b.J; j++)
            {
                m[j] = a * b[j];
            }
            return m;
        }
        public static Matrix Dot(Matrix a, Matrix b)
        {
            if (a.J != b.J || a.I != b.I) throw new ArgumentOutOfRangeException();
            Matrix m = new Matrix(a.I, a.J);
            for(int i = 0; i < a.I; i++)
            {
                for(int j = 0; j < a.J; j++)
                {
                    m[i, j] = a[i, j] * b[i, j];
                }
            }
            return m;
        }
        public static Matrix FromBitmap(Bitmap bmp)
        {
            Matrix m = new Matrix(bmp.Height, bmp.Width);
            for(int x = 0; x < m.J; x++)
                for(int y = 0; y < m.I; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    m[y,x] = (c.R + c.B + c.G) * 0.00130718f;
                }
            return m;
        }
        public static Matrix[] FromBitmapRGB(Bitmap bmp)
        {
            Matrix[] m = new Matrix[3];
            m[0] = new Matrix(bmp.Height, bmp.Width);
            m[1] = new Matrix(bmp.Height, bmp.Width);
            m[2] = new Matrix(bmp.Height, bmp.Width);
            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    m[0][y, x] = c.R * 0.0039215f;
                    m[1][y, x] = c.G * 0.0039215f;
                    m[2][y, x] = c.B * 0.0039215f;
                }
            return m;
        }

        public static explicit operator Vector(Matrix m)
        {
            Vector v = new Vector(m.I * m.J);
            int index = 0;
            for (int i = 0; i < m.I; i++)
                for(int j = 0; j < m.J; j++)
                {
                    v[index] = m[i, j];
                    index++;
                }
            return v;
        }

        public static implicit operator Matrix(float[,] f)
        {
            Matrix m = new Matrix(f.GetLength(1), f.GetLength(0));
            for (int i = 0; i < m.I; i++) for (int j = 0; j < m.J; j++) m[i, j] = f[j,i];
            return m;
        }
    }
}
