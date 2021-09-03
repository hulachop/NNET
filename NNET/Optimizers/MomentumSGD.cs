using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public class MomentumSGD : Optimizer
    {
        public static float gamma = 0.9f;
        float velocity = 0;
        public override float Apply(float value, float gradient, float LR)
        {
            float update = (gamma * velocity) + (gradient * LR);
            velocity = update;
            return value - update;
        }

        public override Optimizer Clone() => new MomentumSGD();
    }
}
