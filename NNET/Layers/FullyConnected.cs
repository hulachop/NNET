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
        public InitializationMethod weightInit = new RandomInitialization();
        public InitializationMethod biasInit = new ZeroInitialization();
        public Optimizer optimizer = new SGD();

        Optimizer[,] weightOptimizers;
        Optimizer[] biasOptimizers;
        Matrix weights;
        Vector biases;
        Vector input;
        Vector output;
        int outputSize;

        public FullyConnected(int _outputSize)
        {
            outputSize = _outputSize;
        }

        public override object Init(object _inputSize, Random rand)
        {
            int inputSize = (int)_inputSize;
            weights = weightInit.Initialize(outputSize, inputSize, inputSize, rand);
            weightOptimizers = new Optimizer[outputSize, inputSize];
            biasOptimizers = new Optimizer[outputSize];
            for (int i = 0; i < outputSize; i++)
            {
                biasOptimizers[i] = optimizer.Clone();
                for (int j = 0; j < inputSize; j++) weightOptimizers[i, j] = optimizer.Clone();
            }
            biases = biasInit.Initialize(outputSize, inputSize, rand);
            return outputSize;
        }
        
        public override object FeedForward(object _input)
        {
            input = _input as Vector;
            output = (weights * input);
            output += biases;
            return activationFunc.Apply(output);
        }

        public override object Backpropagate(object _error, float LR)
        {
            Vector error = _error as Vector;
            Vector newError = new Vector(input.size);
            output = activationFunc.Derivative(output);
            for(int i = 0; i < error.size; i++)
            {
                error[i] *= output[i];
            }
            for(int j = 0; j < input.size; j++)
            {
                for(int i = 0; i < outputSize; i++)
                {
                    newError[j] += weights[i,j] * error[i];
                    weights[i, j] = weightOptimizers[i, j].Apply(weights[i, j], error[i] * input[j], LR * base.LR);
                    biases[i] = biasOptimizers[i].Apply(biases[i], error[i], LR * base.LR);
                }
            }
            return newError;
        }
    }
}
