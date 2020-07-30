using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    public class FlattenLayer : Layer
    {
        List<Vector2Int> inputSize;
        public FlattenLayer()
        {
            inputType = datatype.matriceList;
            outputType = datatype.vector;
        }
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
