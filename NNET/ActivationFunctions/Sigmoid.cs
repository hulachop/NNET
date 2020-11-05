using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public class Sigmoid : ActivationFunction
    {
        public override float Apply(float value)
        {
            double value1 = Math.Exp(value);
            if (float.IsNaN((float)value)) throw new Exception();
            return (float)(value1 / (value1 + 1.0));
        }

        public override float Derivative(float Z)
        {
            return Apply(Z) * (1 - Apply(Z));
        }
    }
}
