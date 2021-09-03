using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public class RandomInitialization : InitializationMethod
    {
        public float mean = 0f;
        public float deviation = 0.5f;
        public override Matrix Initialize(int I, int J, int inputSize, Random rnd)
        {
            deviation = 1 / (float)Math.Sqrt(inputSize);
            Matrix m = new Matrix(I, J);
            for(int i = 0; i < I; i++)
                for(int j = 0; j < J; j++)
                {
                    m[i, j] = mean + (RandomGaussian(rnd) * deviation);
                }
            return m;
        }

        public override Vector Initialize(int I, int inputSize, Random rnd)
        {
            deviation = 1 / (float)Math.Sqrt(inputSize);
            Vector v = new Vector(I);
            for(int i = 0; i < I; i++) v[i] = mean + (RandomGaussian(rnd) * deviation);
            return v;
        }

        private float RandomGaussian(Random rnd)
        {
            while(true)
            {
                float u = (float)(rnd.NextDouble() * 2) - 1;
                float v = (float)(rnd.NextDouble() * 2) - 1;
                float w = (u * u) + (v * v);
                if(w <= 1)
                {
                    float z = (float)Math.Sqrt((-2 * Math.Log(w)) / w);
                    return u * z;
                }
            }
        }
    }
}
