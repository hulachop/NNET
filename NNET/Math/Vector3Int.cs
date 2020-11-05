using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public class Vector3Int
    {
        public int x;
        public int y;
        public int z;

        public static implicit operator Vector3Int((int,int,int) values) => new Vector3Int(values.Item1,values.Item2,values.Item3);

        public Vector3Int()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        public Vector3Int(int X, int Y, int Z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        public static Vector3Int operator *(int a, Vector3Int b)
        {
            b.x *= a;
            b.y *= a;
            b.z *= a;
            return b;
        }

        public static Vector3Int operator +(Vector3Int a, Vector3Int b)
        {
            a.x += b.x;
            a.y += b.y;
            a.z += b.z;
            return a;
        }

        public static Vector3Int operator -(Vector3Int a, Vector3Int b)
        {
            a.x -= b.x;
            a.y -= b.y;
            a.z -= b.z;
            return a;
        }
    }
}
