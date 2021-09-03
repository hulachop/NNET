using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public class MiniBatch : Optimizer
    {
        public static int batchSize = 10;
        float sum = 0;
        int index = 0;
        public override float Apply(float value, float gradient, float LR)
        {
            float result = value;
            sum += gradient;
            index++;
            if (index == batchSize)
            {
                result = value - ((sum / batchSize) * LR);
                index = 0;
                sum = 0;
            }
            return result;
        }

        public override Optimizer Clone() => new MiniBatch();
    }
}
