using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace NNET
{
    [Serializable]
    public class Network
    {
        public List<Layer> layers = new List<Layer>();
        public CostFunction costFunction = new CrossEntropy();
        object inputSize;
        public float cost;
        public int costDivider = 0;

        public Network(object _inputSize, Layer[] _layers)
        {
            for(int i = 0; i < _layers.Length; i++)
            {
                layers.Add(_layers[i]);
            }
            inputSize = _inputSize;
            Init();
        }

        private void Init()
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

        public object Backpropagate(object input, object expectedOutput, float LR)
        {
            object error = costFunction.Derivative(expectedOutput, FeedForward(input));
            cost = costFunction.Cost(expectedOutput, FeedForward(input));
            for (int i = layers.Count-1; i > -1; i--)
            {
                error = layers[i].Backpropagate(error, LR);
            }
            return error;
        }

        public object BackpropagateRaw(object output, object expectedOutput, float LR)
        {
            object error = costFunction.Derivative(expectedOutput, output);
            cost = costFunction.Cost(expectedOutput, output);
            for (int i = layers.Count - 1; i > -1; i--)
            {
                error = layers[i].Backpropagate(error, LR);
            }
            return error;
        }

        public object BackpropagateRaw(object error, float LR)
        {
            for (int i = layers.Count - 1; i > -1; i--)
            {
                error = layers[i].Backpropagate(error, LR);
            }
            return error;
        }

        public static void Save(Network net, string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, net);
            stream.Close();
        }

        public static Network Load(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Network output = formatter.Deserialize(stream) as Network;
            stream.Close();
            return output;
        }
    }
}
