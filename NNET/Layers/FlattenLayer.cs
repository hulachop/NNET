using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public class FlattenLayer : Layer
    {
        Vector3Int inputSize;
        int outputSize;
        public override object Backpropagate(object _error, float LR)
        {
            Vector error = _error as Vector;
            Matrix[] newError = new Matrix[inputSize.z];
            int index = 0;
            for(int n = 0; n < inputSize.z; n++)
            {
                newError[n] = new Matrix(inputSize.y, inputSize.x);
                for (int i = 0; i < inputSize.y; i++)
                    for (int j = 0; j < inputSize.x; j++)
                    {
                        newError[n][i, j] = error[index];
                        index++;
                    }
            }
            return newError;
        }

        public override object FeedForward(object _input)
        {
            Matrix[] input = _input as Matrix[];
            Vector output = new Vector(outputSize);
            int index = 0;
            for(int n = 0; n < input.Length; n++)
                for(int i = 0; i < input[n].I; i++)
                    for(int j = 0; j < input[n].J; j++)
                    {
                        output[index] = input[n][i, j];
                        index++;
                    }
            return output;
        }

        public override object Init(object _inputSize, Random rand)
        {
            inputSize = _inputSize as Vector3Int;
            outputSize = inputSize.x * inputSize.y * inputSize.z;
            return outputSize;
        }
    }
}
