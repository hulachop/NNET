using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
class NeuralNetwork : IComparable<NeuralNetwork>
{

    public int[] layers;
    public float[][] neurons;
    public float[][][] weights;
    public float[][] biases;
    private float fitness;

    private List<float[][][]> weightChanges = new List<float[][][]>();
    private List<float[][]> biasChanges = new List<float[][]>();

    public NeuralNetwork(int[] layers)
    {
        this.layers = layers;
        Random r = new Random();
        InitNeurons();
        InitWeights(r);
        InitBiases(r);
    }

    public NeuralNetwork(NeuralNetwork copyNetwork)
    {
        this.layers = new int[copyNetwork.layers.Length];
        for (int i = 0; i < copyNetwork.layers.Length; i++)
        {
            this.layers[i] = copyNetwork.layers[i];
        }

        Random r = new Random();
        InitNeurons();
        InitWeights(r);
        InitBiases(r);
        CopyWeightsAndBiases(copyNetwork.weights, copyNetwork.biases);
    }

    private void CopyWeightsAndBiases(float[][][] copyWeights, float[][] copyBiases)
    {
        for (int i = 0; i < weights.Length; i++)
        {
            for (int j = 0; j < weights[i].Length; j++)
            {
                for (int k = 0; k < weights[i][j].Length; k++)
                {
                    weights[i][j][k] = copyWeights[i][j][k];
                }
            }
        }
        for (int i = 0; i < biases.Length; i++)
        {
            for (int j = 0; j < biases[i].Length; j++)
            {
                biases[i][j] = copyBiases[i][j];
            }
        }
    }

    private void InitNeurons()
    {
        List<float[]> NeuronList = new List<float[]>();
        foreach (var layer in layers)
        {
            NeuronList.Add(new float[layer]);
        }
        neurons = NeuronList.ToArray();
    }

    private void InitBiases(Random r)
    {
        List<float[]> biasList = new List<float[]>();
        foreach (var layer in layers)
        {
            float[] newLayer = new float[layer];
            for (int i = 0; i < layer; i++)
            {
                newLayer[i] = 0;
            }
            biasList.Add(newLayer);
        }
        biases = biasList.ToArray();
    }

    private void InitWeights(Random r)
    {
        List<float[][]> WeightsList = new List<float[][]>();
        for (int i = 1; i < layers.Length; i++)
        {
            List<float[]> layerWeightList = new List<float[]>();
            int neuronsInPreviousLayer = layers[i - 1];

            for (int j = 0; j < neurons[i].Length; j++)
            {
                float[] neuronWeights = new float[neuronsInPreviousLayer];
                for (int k = 0; k < neuronsInPreviousLayer; k++)
                {
                    neuronWeights[k] = (float)(2 * r.NextDouble()) - 1f;
                }
                layerWeightList.Add(neuronWeights);
            }
            WeightsList.Add(layerWeightList.ToArray());
        }
        weights = WeightsList.ToArray();
    }

    public float[] FeedForward(float[] inputs)
    {
        for (int i = 0; i < inputs.Length - 1; i++)
        {
            neurons[0][i] = inputs[i];
        }
        for (int i = 1; i < layers.Length; i++)
        {
            float sum = 0;
            for (int j = 0; j < neurons[i].Length; j++)
            {
                float value = 0;
                for (int k = 0; k < neurons[i - 1].Length; k++)
                {
                    value += (weights[i - 1][j][k] * neurons[i - 1][k]);
                }
                neurons[i][j] = Sigmoid(value + biases[i][j]);
                if(i == layers.Length - 1) sum += Sigmoid(value + biases[i][j]);
            }
            if (i == layers.Length - 1) SoftMax(i, sum);
        }
        return neurons[neurons.Length - 1];
    }

    public float[] Backpropagate(float[] inputs, float[] expectedOutputs, float LR)
    {

        float[] previousLayerError = new float[layers.Length];
        float[] layerError;
        FeedForward(inputs);
        for (int i = layers.Length - 1; i > 0; i--)
        {
            layerError = previousLayerError;
            previousLayerError = new float[layers[i - 1]];
            if (i == neurons.Length - 1)
            {
                for (int k = 0; k < neurons[i - 1].Length; k++)
                {
                    float error = 0;
                    for (int j = 0; j < neurons[i].Length; j++)
                    {
                        float con = 2 * (neurons[i][j] - expectedOutputs[j]) * neurons[i][j] * (1 - neurons[i][j]);
                        error += con * weights[i - 1][j][k];
                        weights[i - 1][j][k] -= con * neurons[i - 1][k] * LR;
                        biases[i][j] -= con * LR;
                    }
                    previousLayerError[k] = error;
                }
            }
            else
            {
                for (int k = 0; k < neurons[i - 1].Length; k++)
                {
                    float error = 0;
                    for (int j = 0; j < neurons[i].Length; j++)
                    {
                        float con = layerError[j] * neurons[i][j] * (1 - neurons[i][j]);
                        error += con * weights[i - 1][j][k];
                        weights[i - 1][j][k] -= con * neurons[i - 1][k] * LR;
                        biases[i][j] -= con * LR;
                    }
                    previousLayerError[k] = error;
                }
            }
        }
        return previousLayerError;
    }

    public float[] Backpropagate(float[] inputs, float[] expectedOutputs, float LR, ref float cost)
    {
        cost = 0;
        float[] previousLayerError = new float[layers.Length];
        float[] layerError;
        FeedForward(inputs);
        for (int i = layers.Length - 1; i > 0; i--)
        {
            layerError = previousLayerError;
            previousLayerError = new float[layers[i - 1]];
            if (i == neurons.Length - 1)
            {
                for (int k = 0; k < neurons[i - 1].Length; k++)
                {
                    float error = 0;
                    for (int j = 0; j < neurons[i].Length; j++)
                    {
                        float con = 2 * (neurons[i][j] - expectedOutputs[j]) * neurons[i][j] * (1 - neurons[i][j]);
                        if(k == 0) cost += (neurons[i][j] - expectedOutputs[j]) * (neurons[i][j] - expectedOutputs[j]);
                        error += con * weights[i - 1][j][k];
                        weights[i - 1][j][k] -= con * neurons[i - 1][k] * LR;
                        biases[i][j] -= con * LR;
                    }
                    previousLayerError[k] = error;
                }
            }
            else
            {
                for (int k = 0; k < neurons[i - 1].Length; k++)
                {
                    float error = 0;
                    for (int j = 0; j < neurons[i].Length; j++)
                    {
                        float con = layerError[j] * neurons[i][j] * (1 - neurons[i][j]);
                        error += con * weights[i - 1][j][k];
                        weights[i - 1][j][k] -= con * neurons[i - 1][k] * LR;
                        biases[i][j] -= con * LR;
                    }
                    previousLayerError[k] = error;
                }
            }
        }
        return previousLayerError;
    }

    public void Mutate()
    {
        Random r = new Random();
        for (int i = 0; i < weights.Length; i++)
        {
            for (int j = 0; j < weights[i].Length; j++)
            {
                for (int k = 0; k < weights[i][j].Length; k++)
                {
                    float weight = weights[i][j][k];
                    int randomNumber = r.Next(0, 1000);

                    if (randomNumber <= 2f)
                    {
                        weight *= -1f;
                    }
                    else if (randomNumber <= 4f)
                    {
                        weight = (float)r.NextDouble() - 0.5f;
                    }
                    else if (randomNumber <= 6f)
                    {
                        float factor = (float)r.NextDouble() + 1f;
                        weight *= factor;
                    }
                    else if (randomNumber <= 8f)
                    {
                        float factor = (float)r.NextDouble();
                        weight *= factor;
                    }
                    weights[i][j][k] = weight;
                    float bias = biases[i][k];
                    randomNumber = r.Next(0, 1000);

                    if (randomNumber <= 2f)
                    {
                        bias *= -1f;
                    }
                    else if (randomNumber <= 4f)
                    {
                        bias = (float)r.NextDouble() - 0.5f;
                    }
                    else if (randomNumber <= 6f)
                    {
                        float factor = (float)r.NextDouble() + 1f;
                        bias *= factor;
                    }
                    else if (randomNumber <= 8f)
                    {
                        float factor = (float)r.NextDouble();
                        bias *= factor;
                    }
                    biases[i][k] = bias;
                }
            }
        }
    }

    public void AddFitness(float fit)
    {
        fitness += fit;
    }

    public void SetFitness(float fit)
    {
        fitness = fit;
    }

    public float GetFitness()
    {
        return fitness;
    }

    public static float Sigmoid(double value)
    {
        value = Math.Exp(value);
        return (float)(value / (value + 1.0));
    }

    public float Swish(double value)
    {
        return (float)(value * Sigmoid(value));
    }

    public float SigmoidDerivative(float value)
    {
        return Sigmoid(value) * (1 - Sigmoid(value));
    }

    public static float ReluDerivative(float value)
    {
        if (value > 0) return 1;
        else return 0;
    }

    public static float Relu(float value)
    {
        if (value < 0) return 0;
        return value;
    }

    private void SoftMax(int layer, float sum)
    {
        for (int i = 0; i < neurons[layer].Length; i++)
        {
            if (sum != 0) neurons[layer][i] /= sum;
            else neurons[layer][i] = 1/neurons[layer].Length;
            if (neurons[layer][i] == float.NaN)
            {
                throw new Exception();
            }
        }
    }

    public enum ActivationFunction { Sigmoid, Relu, SoftMax}

    public int CompareTo(NeuralNetwork other)
    {
        if (other == null) return 1;
        if (fitness > other.fitness) return 1;
        else if (fitness < other.fitness) return -1;
        else return 0;
    }
}
