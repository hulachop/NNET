using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    public abstract class ActivationFunction
    {
        public abstract float[] Apply(float[] layer);
        public abstract List<Matrix> Apply(List<Matrix> layer);
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
    }
}
