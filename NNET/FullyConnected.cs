using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    /// <summary>
    /// A regular, fully connected layer.
    /// </summary>
    [Serializable]
    public class FullyConnected : Layer
    {
        float[][] weights;
        float[] biases;
        float[] input;
        float[] output;
        int inputSize;
        int outputSize;

        public FullyConnected(int _outputSize)
        {
            inputType = datatype.vector;
            outputType = datatype.vector;
            outputSize = _outputSize;
        }

        public override object Init(object _inputSize, Random rand)
        {
            inputSize = (int)_inputSize;
            input = new float[inputSize];
            output = new float[outputSize];
            biases = new float[outputSize];
            weights = new float[outputSize][];
            for (int i = 0; i < outputSize; i++)
            {
                biases[i] = 0;
                weights[i] = new float[inputSize];
                for (int j = 0; j < inputSize; j++)
                {
                    weights[i][j] = (float)(rand.NextDouble()) - 0.5f;
                }
            }
            return outputSize;
        }
        
        public override object FeedForward(object _input)
        {
            input = _input as float[];
            for(int i = 0; i < outputSize; i++)
            {
                float value = 0;
                for(int j = 0; j < inputSize; j++)
                {
                    value += input[j] * weights[i][j];
                }
                output[i] = value + biases[i];
            }
            output = activationFunc.Apply(output);
            return output;
        }

        public override object Backpropagate(object _error, float LR)
        {
            float[] error = _error as float[];
            float[] newError = new float[inputSize];
            output = activationFunc.Derivative(output);
            for(int i = 0; i < error.Length; i++)
            {
                error[i] *= output[i];
            }
            for(int j = 0; j < inputSize; j++)
            {
                for(int i = 0; i < outputSize; i++)
                {
                    newError[j] += weights[i][j] * error[i];
                    weights[i][j] -= error[i] * input[j] * LR * baseLR;
                    biases[i] -= error[i] * LR * baseLR;
                }
            }
            return newError;
        }
    }
}
