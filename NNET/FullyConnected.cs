using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    public class FullyConnected : Layer
    {
        float[][] weights;
        float[] biases;
        float[] input;
        float[] output;
        int inputSize;
        int outputSize;

        public FullyConnected(int _outputSize, ActivationFunction _activationFunction)
        {
            inputType = datatype.vector;
            outputType = datatype.vector;
            activationFunc = _activationFunction;
            outputSize = _outputSize;
        }

        public override object Init(object _inputSize, Random rand)
        {
            inputSize = (int)_inputSize;
            input = new float[inputSize];
            output = new float[outputSize];
            biases = new float[outputSize];
            weights = new float[outputSize][];
            for(int i = 0; i < outputSize; i++)
            {
                biases[i] = (float)rand.NextDouble();
                weights[i] = new float[inputSize];
                for(int j = 0; j < inputSize; j++)
                {
                    weights[i][j] = (float)(rand.NextDouble())-0.5f;
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
            for(int j = 0; j < inputSize; j++)
            {
                for(int i = 0; i < outputSize; i++)
                {
                    float con = error[i] * activationFunc.Derivative(output[i]);
                    newError[j] += weights[i][j] * con;
                    weights[i][j] -= con * input[j] * LR;
                    biases[i] -= con * LR;
                }
            }
            return newError;
        }
    }
}
