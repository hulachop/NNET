using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public class MeanSquared : CostFunction
    {
        public override float Cost(float expectedOutput, float predictedOutput)
        {
            return (expectedOutput - predictedOutput) * (expectedOutput - predictedOutput);
        }
        public override object Derivative(float expectedOutput, float predictedOutput)
        {
            return 2 * (predictedOutput - expectedOutput);
        }
    }
}
