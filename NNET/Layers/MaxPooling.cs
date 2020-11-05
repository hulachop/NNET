using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public class MaxPooling : Layer
    {
        Vector2Int poolSize;
        Vector3Int outputSize;
        Matrix[] input;

        int stride;
        public MaxPooling(Vector2Int _poolSize, int _stride)
        {
            poolSize = _poolSize;
            stride = _stride;
        }
        public override object Backpropagate(object _error, float LR)
        {
            Matrix[] error = _error as Matrix[];
            Matrix[] newError = new Matrix[input.Length];
            for(int n = 0; n < input.Length; n++)
            {
                newError[n] = new Matrix(input[n].I, input[n].J);
                for(int x = 0; x < outputSize.x; x++)
                    for(int y = 0; y < outputSize.y; y++)
                    {
                        float highest = float.MinValue;
                        Vector2Int highestID = new Vector2Int();
                        for(int xP = 0; xP < poolSize.x; xP++)
                            for(int yP = 0; yP < poolSize.y; yP++)
                            {
                                if(input[n][(y*stride) + yP,(x*stride) + xP] > highest)
                                {
                                    highest = input[n][(y * stride) + yP, (x * stride) + xP];
                                    highestID = new Vector2Int(xP, yP);
                                }
                            }
                        newError[n][(y * stride) + highestID.y, (x * stride) + highestID.x] = error[n][y, x];
                    }
            }
            return newError;
        }

        public override object FeedForward(object _input)
        {
            input = _input as Matrix[];
            Matrix[] output = new Matrix[input.Length];
            for(int n = 0; n < input.Length; n++)
            {
                output[n] = new Matrix(outputSize.y, outputSize.x);
                for(int x = 0; x < outputSize.x; x++)
                    for(int y = 0; y < outputSize.y; y++)
                    {
                        float highest = float.MinValue;
                        for (int xP = 0; xP < poolSize.x; xP++)
                            for (int yP = 0; yP < poolSize.y; yP++)
                            {
                                if (input[n][(y * stride) + yP, (x * stride) + xP] > highest) highest = input[n][(y * stride) + yP, (x * stride) + xP];
                            }
                        output[n][y, x] = highest;
                    }
            }
            return output;
        }

        public override object Init(object _inputSize, Random rand)
        {
            Vector3Int inputSize = _inputSize as Vector3Int;
            outputSize = new Vector3Int((int)((inputSize.x - poolSize.x) / stride) + 1, (int)((inputSize.y - poolSize.y) / stride) + 1,inputSize.z);
            return outputSize;
        }
    }
}
