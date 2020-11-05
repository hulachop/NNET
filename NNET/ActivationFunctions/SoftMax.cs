using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public class SoftMax : ActivationFunction
    {
        public override Vector Apply(Vector layer)
        {
            Vector output = new Vector(layer.size);
            float sum = 0;
            for(int i = 0; i < layer.size; i++)
            {
                output[i] = (float)Math.Exp(layer[i]);
                sum += output[i];
            }
            return (1 / sum) * output;
        }
        public override Vector Derivative(Vector Z)
        {
            Vector applied = Apply(Z);
            Vector output = new Vector(Z.size);
            for(int i = 0; i < Z.size; i++)
            {
                for(int j = 0; j < Z.size; j++)
                {
                    if (i == j) output[i] -= applied[j] - (applied[j] * applied[i]);
                    else output[i] -= 0 - (applied[j] * applied[i]);
                }
            }
            return output;
        }
    }
}
