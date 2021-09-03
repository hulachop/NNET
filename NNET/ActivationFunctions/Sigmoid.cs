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
            float value2 = (float)(value1 / (value1 + 1.0));
            if (float.IsNaN(value2))
            {
                if (value > 0) return 1;
                else return 0;
            }
            return value2;
        }

        public override float Derivative(float Z)
        {
            return Apply(Z) * (1 - Apply(Z));
        }
    }
}
