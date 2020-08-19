using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    public class Network
    {
        bool valid;
        List<Layer> layers = new List<Layer>();
        public CostFunction costFunction = new MeanSquared();
        datatype outputType;
        public float LR = 1f;

        public Network(object inputSize, Layer[] _layers)
        {
            for(int i = 0; i < _layers.Length; i++)
            {
                layers.Add(_layers[i]);
            }
            Validate();
            Init(inputSize);
            outputType = layers[layers.Count - 1].outputType;
        }

        private void Init(object inputSize)
        {
            Random rand = new Random();
            if(inputSize.GetType() == typeof(Vector2Int))
            {
                List<Vector2Int> newSize = new List<Vector2Int>();
                newSize.Add(inputSize as Vector2Int);
                inputSize = newSize;
            }
            for(int i = 0; i < layers.Count; i++)
            {
                inputSize = layers[i].Init(inputSize, rand);
            }
        }

        public object FeedForward(object input)
        {
            for(int i = 0; i < layers.Count; i++)
            {
                input = layers[i].FeedForward(input);
            }
            return input;
        }

        public object BackPropagate(object input, object expectedOutput)
        {
            object error = costFunction.Derivative(expectedOutput, FeedForward(input), outputType);
            for(int i = layers.Count-1; i > -1; i--)
            {
                error = layers[i].Backpropagate(error, LR);
            }
            return error;
        }

        private void Validate()
        {
            for(int i = 1; i < layers.Count; i++)
            {
                if(layers[i-1].outputType != layers[i].inputType)
                {
                    if(layers[i-1].outputType == datatype.matriceList && layers[i].inputType == datatype.vector)
                    {
                        layers.Insert(i, new FlattenLayer());
                    }
                    else
                    {
                        valid = false;
                        return;
                    }
                }
            }
            valid = true;
        }
    }
}
