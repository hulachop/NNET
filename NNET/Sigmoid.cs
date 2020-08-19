using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    public class Sigmoid : ActivationFunction
    {
        public override float Apply(float value)
        {
            value = (float)Math.Exp(value);
            if (float.IsNaN((float)value)) throw new Exception();
            return (float)(value / (value + 1.0));
        }

        public override float Derivative(float applied)
        {
            return applied * (1 - applied);
        }
    }
}
