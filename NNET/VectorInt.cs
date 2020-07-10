using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNET
{
    class VectorInt
    {
        public int x;
        public int y;

        public VectorInt()
        {
            x = 0;
            y = 0;
        }

        public VectorInt(int X, int Y)
        {
            x = X;
            y = Y;
        }

        public static VectorInt operator *(VectorInt a, int b)
        {
            a.x *= b;
            a.y *= b;
            return a;
        }

        public static VectorInt operator +(VectorInt a, VectorInt b)
        {
            a.x += b.x;
            a.y += b.y;
            return a;
        }

        public static VectorInt operator -(VectorInt a, VectorInt b)
        {
            a.x -= b.x;
            a.y -= b.y;
            return a;
        }

        public static VectorInt Average(List<VectorInt> vs)
        {
            if (vs.Count == 0) return new VectorInt();
            int x = 0;
            int y = 0;
            int n = 0;
            for(int i = 0; i < vs.Count; i++)
            {
                x += vs[i].x;
                y += vs[i].y;
                n++;
            }
            x /= n;
            y /= n;
            return new VectorInt(x, y);
        }
    }
}
