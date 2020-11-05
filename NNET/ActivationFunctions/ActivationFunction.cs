using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public abstract class ActivationFunction
    {
        public virtual float Apply(float value)
        {
            return 1;
        }
        public virtual Vector Apply(Vector layer)
        {
            Vector output = new Vector(layer.size);
            for(int i = 0; i < layer.size; i++) output[i] = Apply(layer[i]);
            return output;
        }
        public virtual Matrix[] Apply(Matrix[] layer)
        {
            Matrix[] output = new Matrix[layer.Length];
            for(int m = 0; m < output.Length; m++)
            {
                output[m] = new Matrix(layer[m].I, layer[m].J);
                for(int i = 0; i < layer[m].I; i++)
                    for(int j = 0; j < layer[m].J; j++)
                    {
                        output[m][i, j] = Apply(layer[m][i, j]);
                    }
            }
            return output;
        }
        public virtual float Derivative(float Z)
        {
            return 1;
        }
        public virtual Matrix Derivative(Matrix Z)
        {
            Matrix m = new Matrix(Z.I, Z.J);
            for(int i = 0; i < Z.I; i++)
                for(int j = 0; j < Z.J; j++)
                {
                    m[i, j] = Derivative(Z[i, j]);
                }
            return m;
        }
        public virtual Matrix[] Derivative(Matrix[] Z)
        {
            Matrix[] output = new Matrix[Z.Length];
            for(int n = 0; n < Z.Length; n++)
            {
                output[n] = new Matrix(Z[n].I, Z[n].J);
                for(int i = 0; i < Z[n].I; i++)
                    for(int j = 0; j < Z[n].J; j++)
                    {
                        output[n][i, j] = Apply(Z[n][i, j]);
                    }
            }
            return output;
        }
        public virtual Vector Derivative(Vector Z)
        {
            Vector v = new Vector(Z.size);
            for (int i = 0; i < Z.size; i++) v[i] = Derivative(Z[i]);
            return v;
        }
    }
}
