using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public abstract class Layer
    {

        public float LR = 1;
        public ActivationFunction activationFunc { get; set; } = new Sigmoid();
        public abstract object Init(object _inputSize, Random rand);
        public abstract object FeedForward(object input);
        public abstract object Backpropagate(object error, float LR);
    }
}
