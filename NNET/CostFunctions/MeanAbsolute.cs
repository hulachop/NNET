using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public class MeanAbsolute : CostFunction
    {
        public override float Cost(float expectedOutput, float predictedOutput)
        {
            return Math.Abs(expectedOutput - predictedOutput);
        }
        public override object Derivative(float expectedOutput, float predictedOutput)
        {
            if (predictedOutput > expectedOutput) return 1f;
            else return -1f;
        }
    }
}
