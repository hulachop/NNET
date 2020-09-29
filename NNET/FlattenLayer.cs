using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public class FlattenLayer : Layer
    {
        List<Vector2Int> inputSize;
        int outputSize;
        public FlattenLayer()
        {
            inputType = datatype.matriceList;
            outputType = datatype.vector;
        }
        public override object Backpropagate(object _error, float LR)
        {
            float[] error = _error as float[];
            List<Matrix> newError = new List<Matrix>();
            int index = 0;
            for(int i = 0; i < inputSize.Count; i++)
            {
                newError.Add(new Matrix(inputSize[i].x, inputSize[i].y));
                for(int x = 0; x < inputSize[i].x; x++)
                {
                    for(int y = 0; y < inputSize[i].y; y++)
                    {
                        newError[i].Set(x, y, error[index]);
                        index++;
                    }
                }
            }
            return newError;
        }

        public override object FeedForward(object _input)
        {
            List<Matrix> input = _input as List<Matrix>;
            float[] output = new float[outputSize];
            int index = 0;
            for(int i = 0; i < input.Count; i++)
            {
                for(int x = 0; x < input[i].size.x; x++)
                {
                    for(int y = 0; y < input[i].size.y; y++)
                    {
                        output[index] = input[i].Get(x, y);
                        index++;
                    }
                }
            }
            return output;
        }

        public override object Init(object _inputSize, Random rand)
        {
            inputSize = _inputSize as List<Vector2Int>;
            outputSize = 0;
            for (int i = 0; i < inputSize.Count; i++)
            {
                outputSize += inputSize[i].x * inputSize[i].y;
            }
            return outputSize;
        }
    }
}
