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
    }
}
