using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    class BatchNorm : Layer
    {
        public override object Backpropagate(object error, float LR)
        {
            throw new NotImplementedException();
        }

        public override object FeedForward(object input)
        {
            throw new NotImplementedException();
        }

        public override object Init(object _inputSize, Random rand)
        {
            throw new NotImplementedException();
        }
    }
}
