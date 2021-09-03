using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public class Tanh : ActivationFunction
    {
        public override float Apply(float value) => (float)Math.Tanh(value);
        public override float Derivative(float Z)
        {
            float t = (float)Math.Tanh(Z);
            return 1 - (t * t);
        }
    }
}
