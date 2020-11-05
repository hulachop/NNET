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
        Random rnd = new Random();
        public RandomInitialization()
        {
            deviation = 0.5f;
        }
        public RandomInitialization(float _variance)
        {
            deviation = _variance;
        }
        public RandomInitialization(float _mean, float _variance)
        {
            mean = _mean;
            deviation = _variance;
        }
        public RandomInitialization(int inputSize)
        {
            deviation = 1 / (float)Math.Sqrt(inputSize);
        }
        public override Matrix Initialize(int I, int J)
        {
            Matrix m = new Matrix(I, J);
            for(int i = 0; i < I; i++)
                for(int j = 0; j < J; j++)
                {
                    m[i, j] = mean + (RandomGaussian() * deviation);
                }
            return m;
        }

        public override Vector Initialize(int I)
        {
            Vector v = new Vector(I);
            for(int i = 0; i < I; i++) v[i] = mean + (RandomGaussian() * deviation);
            return v;
        }

        private float RandomGaussian()
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
