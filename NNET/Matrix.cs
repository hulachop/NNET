using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNET
{
    class Matrix
    {
        float[,] values;
        public Size size;
        public float max;
        public Point maxID;

        public static Matrix operator *(Matrix a, float b)
        {
            Matrix m = new Matrix(a.size.Width, a.size.Height);
            for(int x = 0; x < a.size.Width; x++)
                for(int y = 0; y < a.size.Height; y++)
                {
                    m.values[x, y] = a.values[x,y] * b;
                }
            return m;
        }

        public static Matrix operator /(Matrix a, float b)
        {
            Matrix m = new Matrix(a.size.Width, a.size.Height);
            for (int x = 0; x < a.size.Width; x++)
                for (int y = 0; y < a.size.Height; y++)
                {
                    m.values[x, y] = a.values[x, y] / b;
                }
            return m;
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            if (a.size != b.size) return new Matrix(0, 0);
            Matrix m = new Matrix(a.size.Width, a.size.Height);
            for(int x = 0; x < a.size.Width; x++)
                for(int y = 0; y < a.size.Height; y++)
                {
                    m.values[x, y] = a.values[x, y] - b.values[x, y];
                }
            return m;
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.size != b.size) return new Matrix(0, 0);
            Matrix m = new Matrix(a.size.Width, a.size.Height);
            for (int x = 0; x < a.size.Width; x++)
                for (int y = 0; y < a.size.Height; y++)
                {
                    m.values[x, y] = a.values[x, y] + b.values[x, y];
                }
            return m;
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.size != b.size) return new Matrix(0, 0);
            Matrix m = new Matrix(a.size.Width, a.size.Height);
            for (int x = 0; x < a.size.Width; x++)
                for (int y = 0; y < a.size.Height; y++)
                {
                    m.values[x,y] = a.values[x, y] * b.values[x, y];
                }
            return m;
        }

        public Matrix(int x, int y)
        {
            size = new Size(x, y);
            values = new float[x, y];
            max = 0;
        }

        public Matrix(Matrix matrix, int posX, int posY, int sizeX, int sizeY)
        {
            float highest = float.MinValue;
            values = new float[sizeX, sizeY];
            size = new Size(sizeX, sizeY);
            for(int x = 0; x < sizeX; x++)
            {
                for(int y = 0; y < sizeY; y++)
                {
                    if (posX + x > matrix.size.Width - 1 || posY + y > matrix.size.Height - 1) values[x, y] = 0;
                    else values[x, y] = matrix.values[posX + x, posY + y];
                    if (values[x, y] > highest)
                    {
                        highest = values[x, y];
                        maxID = new Point(x, y);
                    }
                }
            }
            max = highest;
        }

        public Matrix(Bitmap bmp)
        {
            size = new Size(bmp.Width, bmp.Height);
            values = new float[size.Width, size.Height];
            for(int x = 0; x < size.Width; x++)
            {
                for(int y = 0; y < size.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    values[x, y] = 1 - (c.R * 0.0039215f + c.G * 0.0039215f + c.B * 0.0039215f)*0.33f;
                }
            }
        }

        public void Dot(Matrix other)
        {
            if (other.size != size) return;
            for(int x = 0; x < size.Width; x++)
            {
                for(int y = 0; y < size.Height; y++)
                {
                    values[x, y] *= other.Get(x, y);
                }
            }
        }

        public float DotSum(Matrix other)
        {
            float o = 0;
            if (other.size != size) return 0;
            for (int x = 0; x < size.Width; x++)
            {
                for (int y = 0; y < size.Height; y++)
                {
                    o += values[x, y] * other.Get(x, y);
                }
            }
            return o;
        }

        public float Sum()
        {
            float s = 0;
            for(int x = 0; x < size.Width; x++)
                for(int y = 0; y < size.Height; y++)
                {
                    s += values[x, y];
                }
            return s;
        }

        public void Relu()
        {
            for(int x = 0; x < size.Width; x++)
            {
                for(int y = 0; y < size.Height; y++)
                {
                    values[x, y] = NeuralNetwork.Relu(values[x, y]);
                }
            }
        }

        public void Sigmoid()
        {
            for(int x = 0; x < size.Width; x++)
                for(int y = 0; y < size.Height; y++)
                {
                    values[x, y] = NeuralNetwork.Sigmoid(values[x, y]);
                }
        }

        public void SigmoidDerivativeA()
        {
            for (int x = 0; x < size.Width; x++)
                for (int y = 0; y < size.Height; y++)
                {
                    values[x, y] *= 1 - values[x, y];
                }
        }

        public void ReluDerivative()
        {
            for (int x = 0; x < size.Width; x++)
            {
                for (int y = 0; y < size.Height; y++)
                {
                    values[x, y] = NeuralNetwork.ReluDerivative(values[x, y]);
                }
            }
        }

        public void AddAt(Matrix matrix, int X, int Y)
        {
            for (int x = X; x < X + matrix.size.Width; x++)
                for (int y = Y; y < Y + matrix.size.Height; y++)
                {
                    if (x < size.Width && y < size.Height)
                    {
                        values[x, y] += matrix.values[x - X, y - Y];
                    }
                }
        }

        public float Get(int x, int y)
        {
            return values[x, y];
        }

        public void Set(int x, int y, float value)
        {
            values[x, y] = value;
        }

        public static Matrix RandomMatrix(int x, int y, Random r)
        {
            Matrix m = new Matrix(x, y);
            for(int i = 0; i < x; i++)
            {
                for(int j = 0; j < y; j++)
                {
                    m.values[i, j] = (4 * (float)r.NextDouble()) - 2f;
                }
            }
            return m;
        }

        public Bitmap ToBitmap()
        {
            Bitmap bmp = new Bitmap(size.Width, size.Height);
            for(int x = 0; x < size.Width; x++)
            {
                for(int y = 0; y < size.Height; y++)
                {
                    int color = 0;
                    if (values[x, y] > 1) color = 255;
                    else if (values[x, y] < 0) color = 0;
                    else color = (int)(values[x, y] * 255);
                    bmp.SetPixel(x, y, Color.FromArgb(color, color, color));
                }
            }
            return bmp;
        }
    }
}
