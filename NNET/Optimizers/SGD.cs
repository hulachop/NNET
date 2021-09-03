using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public class SGD : Optimizer
    {
        public override float Apply(float value, float gradient, float LR) => value - (gradient * LR);

        public override Optimizer Clone() => new SGD();
    }
}
