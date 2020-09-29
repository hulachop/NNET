using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public abstract class CostFunction
    {
        public abstract float Cost(object expectedOutput, object predictedOutput, datatype dt);
        public abstract object Derivative(object expectedOutput, object predictedOutput, datatype dt);
    }
}
