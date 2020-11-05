using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public class CrossEntropy : CostFunction
    {
        public override float Cost(float expectedOutput, float predictedOutput)
        {
            return -expectedOutput * (float)Math.Log10(predictedOutput);
        }
        public override object Derivative(float expectedOutput, float predictedOutput)
        {
            return ((1 - expectedOutput) / (1 - predictedOutput)) - (expectedOutput / predictedOutput);
        }
    }
}
