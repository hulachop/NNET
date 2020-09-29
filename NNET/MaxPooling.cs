using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public class MaxPooling : Layer
    {
        Vector2Int poolSize;
        int stride;
        List<Matrix> input;
        public MaxPooling(Vector2Int _poolSize, int _stride)
        {
            poolSize = _poolSize;
            stride = _stride;
        }
        public override object Backpropagate(object _error, float LR)
        {
            List<Matrix> error = _error as List<Matrix>;
            List<Matrix> newErrors = new List<Matrix>();
            for(int i = 0; i < error.Count; i++)
            {
                Matrix newError = new Matrix(input[i].size.x, input[i].size.y);
                for(int x = 0; x < error[i].size.x; x++)
                {
                    for(int y = 0; y < error[i].size.y; y++)
                    {
                        Matrix slice = new Matrix(input[i], x * stride, y * stride, poolSize.x, poolSize.y);
                        Matrix errorAdd = new Matrix(slice.size.x, slice.size.y);
                        errorAdd.Set(slice.maxID.x, slice.maxID.y, error[i].Get(x, y));
                        newError.AddAt(errorAdd, x * stride, y * stride);
                    }
                }
                newErrors.Add(newError);
            }
            return newErrors;
        }

        public override object FeedForward(object _input)
        {
            input = _input as List<Matrix>;
            List<Matrix> output = new List<Matrix>();
            for(int i = 0; i < input.Count; i++)
            {
                int X = (int)((input[i].size.x - poolSize.x + 1) / stride) + 1;
                int Y = (int)((input[i].size.y - poolSize.y + 1) / stride) + 1;
                Matrix o = new Matrix(X, Y);
                for(int x = 0; x < X; x++)
                    for(int y = 0; y < Y; y++)
                    {
                        Matrix slice = new Matrix(input[i], x * stride, y * stride, poolSize.x, poolSize.y);
                        o.Set(x, y, slice.max);
                    }
                output.Add(o);
            }
            return output;
        }

        public override object Init(object _inputSize, Random rand)
        {
            List<Vector2Int> inputSize = _inputSize as List<Vector2Int>;
            List<Vector2Int> outputSize = new List<Vector2Int>();
            for(int i = 0; i < inputSize.Count; i++)
            {
                int X = (int)((inputSize[i].x - poolSize.x + 1) / stride) + 1;
                int Y = (int)((inputSize[i].y - poolSize.y + 1) / stride) + 1;
                outputSize.Add(new Vector2Int(X, Y));
            }
            return outputSize;
        }
    }
}
