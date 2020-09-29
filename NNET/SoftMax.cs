using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    class SoftMax : ActivationFunction
    {
        public override float[] Apply(float[] layer)
        {
            float[] output = new float[layer.Length];
            float sum = 0;
            for(int i = 0; i < layer.Length; i++)
            {
                if (layer[i] > 0)
                {
                    sum += layer[i];
                    output[i] = layer[i];
                }
                else output[i] = 0;
            }
            if (sum == 0) sum = 1;
            for(int i = 0; i < layer.Length; i++)
            {
                output[i] /= sum;
            }
            return output;
        }

        public override float Derivative(float applied)
        {
            return applied * (1 - applied);
        }
    }
}
