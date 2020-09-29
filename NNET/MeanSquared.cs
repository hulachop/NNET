using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public class MeanSquared : CostFunction
    {
        public override float Cost(object _expectedOutput, object _predictedOutput, datatype dt)
        {
            object cost = Transform.Apply(_predictedOutput, _expectedOutput, (x, y) => (x - y) * (x - y), dt);
            return Transform.Sum(cost, dt);
        }

        public override object Derivative(object _expectedOutput, object _predictedOutput, datatype dt)
        {
            return Transform.Apply(_predictedOutput, _expectedOutput, (x, y) => 2 * (x - y), dt);
        }
    }
}
