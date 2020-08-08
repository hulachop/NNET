using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    /// <summary>
    /// A convolutional layer. Used for spacially relative data.
    /// </summary>
    class Convolution : Layer
    {
        List<Matrix> input = new List<Matrix>();
        List<Matrix> output = new List<Matrix>();
        List<Matrix> kernels = new List<Matrix>();
        int kernelNumber;

        int stride;
        bool padding;
        public Convolution(Vector2Int kernelSize, int _kernelNumber, int stride, bool padding, ActivationFunction _activationFunc)
        {
            activationFunc = _activationFunc;
            kernelNumber = _kernelNumber;
            inputType = datatype.matriceList;
            outputType = datatype.matriceList;
            for(int i = 0; i < kernelNumber; i++)
            {
                kernels.Add(new Matrix(kernelSize.x, kernelSize.y)); // needs work!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            }
        }
        public override object Init(object _inputSize, Random rand)
        {
            List<Vector2Int> inputSize = _inputSize as List<Vector2Int>;
            List<Vector2Int> outputSize = new List<Vector2Int>();
            int sizeX = inputSize[0].x;
            float sizeY = inputSize[0].y;
            if (padding)
            {
                sizeX += (kernels[0].size.x * 2) - 2;
                sizeY += (kernels[0].size.y * 2) - 2;
            }
            int X = (int)((sizeX - kernels[0].size.x + 1) / stride) + 1;
            int Y = (int)((sizeY - kernels[0].size.y + 1) / stride) + 1;
            for(int i = 0; i < inputSize.Count * kernels.Count; i++)
            {
                outputSize.Add(new Vector2Int(X, Y));
            }
            return outputSize;
        }
        public override object FeedForward(object _input)
        {
            input = _input as List<Matrix>;
            output = new List<Matrix>();
            int sizeX = input[0].size.x;
            float sizeY = input[0].size.y;
            int xOffset = 0, yOffset = 0;
            if (padding)
            {
                sizeX += (kernels[0].size.x * 2) - 2;
                sizeY += (kernels[0].size.y * 2) - 2;
                xOffset = kernels[0].size.x - 1;
                yOffset = kernels[0].size.y - 1;
            }
            int xMoves = (int)((sizeX - kernels[0].size.x + 1) / stride) + 1;
            int yMoves = (int)((sizeY - kernels[0].size.y + 1) / stride) + 1;
            for (int m = 0; m < input.Count; m++)
            {
                for (int k = 0; k < kernelNumber; k++)
                {
                    Matrix newMatrix = new Matrix(xMoves, yMoves);
                    for (int x = 0; x < xMoves; x++)
                    {
                        for (int y = 0; y < yMoves; y++)
                        {
                            Matrix slice = new Matrix(input[m], (x * stride) - xOffset, (y * stride) - yOffset, kernels[k].size.x, kernels[k].size.y);
                            newMatrix.Set(x, y, slice.DotSum(kernels[output.Count]));
                        }
                    }
                    output.Add(newMatrix);
                }
            }
            return activationFunc.Apply(output);
        }
        public override object Backpropagate(object error, float LR)
        {
            List<Matrix> errors = error as List<Matrix>;
            List<Matrix> newErrors = new List<Matrix>();

            int xOffset = 0, yOffset = 0;
            if (padding)
            {
                xOffset = kernels[0].size.x - 1;
                yOffset = kernels[0].size.y - 1;
            }

            for (int i = 0; i < input.Count; i++)
            {
                Matrix newError = new Matrix(input[i].size.x, input[i].size.y);
                for(int k = 0; k < kernelNumber; k++)
                {
                    int id = (i * output.Count) + k;
                    output[id] = activationFunc.Derivative(output[id]);
                    errors[id] *= output[id];
                    Matrix kernelChange;
                    Matrix errorAdd;
                    for(int x = 0; x < output[id].size.x; x++)
                        for(int y = 0; y < output[id].size.y; y++)
                        {
                            errorAdd = kernels[id] * errors[id].Get(x,y);
                            newError.AddAt(errorAdd, (x * stride) - xOffset, (y * stride) - yOffset);
                            kernelChange = new Matrix(input[i], (x * stride) - xOffset, (y * stride) - yOffset, kernels[id].size.x, kernels[id].size.y);
                            kernels[id] -= (kernelChange * errors[id].Get(x, y)) * LR * baseLR;
                        }
                }
                newErrors.Add(newError);
            }
            return newErrors;
        }
    }
}
