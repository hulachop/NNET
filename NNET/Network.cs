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
        public CostFunction costFunction = new MeanSquared();
        datatype outputType;
        object inputSize;

        public Network(object _inputSize, Layer[] _layers)
        {
            for(int i = 0; i < _layers.Length; i++)
            {
                layers.Add(_layers[i]);
            }
            inputSize = _inputSize;
            Validate();
            Init();
            outputType = layers[layers.Count - 1].outputType;
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
            object error = costFunction.Derivative(expectedOutput, FeedForward(input), outputType);
            for(int i = layers.Count-1; i > -1; i--)
            {
                error = layers[i].Backpropagate(error, LR);
            }
            return error;
        }

        public object BackpropagateRaw(object output, object expectedOutput, float LR)
        {
            object error = costFunction.Derivative(expectedOutput, output, outputType);
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

        private bool Validate()
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
                        return false;
                    }
                }
            }
            return true;
        }


        public void AddLayer(Layer layer)
        {
            layers.Add(layer);
            if (!Validate()) layers.RemoveAt(layers.Count - 1);
            else Init();
        }

        public void InsertLayer(Layer layer, int index)
        {
            layers.Insert(index, layer);
            if (!Validate()) layers.RemoveAt(index);
            else Init();
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
