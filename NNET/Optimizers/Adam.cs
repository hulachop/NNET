using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public class Adam : Optimizer
    {
        public static float beta = 0.9f;
        public static float beta2 = 0.999f;
        public static float epsilon = 1E-8f;
        float betat = beta;
        float betat2 = beta2;
        float m = 0;
        float v = 0;
        public override float Apply(float value, float gradient, float LR)
        {
            m = (beta * m) + ((1 - beta) * gradient);
            v = (beta2 * v) + ((1 - beta2) * gradient * gradient);
            float mhat = m / (1 - betat);
            float vhat = v / (1 - betat2);
            betat *= beta;
            betat2 *= beta2;
            return value - ((mhat * LR) / ((float)Math.Sqrt(vhat) + epsilon));
        }

        public override Optimizer Clone() => new Adam();
    }
}
