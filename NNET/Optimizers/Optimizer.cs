using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public abstract class Optimizer
    {
        public abstract float Apply(float value, float gradient, float LR);
        public abstract Optimizer Clone();
    }
}
