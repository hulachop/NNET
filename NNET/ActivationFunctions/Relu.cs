using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public class Relu : ActivationFunction
    {
        public override float Apply(float value)
        {
            if (value < 0) return 0;
            else return value;
        }

        public override float Derivative(float Z)
        {
            //if (Z < 0) return 0;
            return 1;
        }
    }
}
