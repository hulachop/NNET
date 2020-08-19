using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    public abstract class ActivationFunction
    {
        public abstract float Apply(float value);
        public virtual float[] Apply(float[] layer)
        {
            float[] output = new float[layer.Length];
            for(int i = 0; i < layer.Length; i++)
            {
                output[i] = Apply(layer[i]);
            }
            return output;
        }
        public virtual List<Matrix> Apply(List<Matrix> layer)
        {
            List<Matrix> output = new List<Matrix>();
            for(int i = 0; i < layer.Count; i++)
            {
                output.Add(new Matrix(layer[i].size.x, layer[i].size.y));
                for(int x = 0; x < layer[i].size.x; x++)
                {
                    for(int y = 0; y < layer[i].size.y; y++)
                    {
                        output[i].Set(x, y, Apply(layer[i].Get(x, y)));
                    }
                }
            }
            return output;
        }
        public abstract float Derivative(float applied);
        public virtual Matrix Derivative(Matrix applied)
        {
            for(int x = 0; x < applied.size.x; x++)
            {
                for(int y = 0; y < applied.size.y; y++)
                {
                    applied.Set(x, y, Derivative(applied.Get(x, y)));
                }
            }
            return applied;
        }
        public virtual float[] Derivative(float[] applied)
        {
            float[] output = new float[applied.Length];
            for(int i = 0; i < applied.Length; i++)
            {
                output[i] = Derivative(applied[i]);
            }
            return output;
        }
    }
}
