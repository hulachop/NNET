﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    public abstract class Layer
    {
        public datatype inputType { get; protected set; }
        public datatype outputType { get; protected set; }
        public float baseLR = 1;
        public ActivationFunction activationFunc { get; set; } = new Sigmoid();
        public abstract object Init(object _inputSize, Random rand);
        public abstract object FeedForward(object input);
        public abstract object Backpropagate(object error, float LR);
    }
    public enum datatype { matriceList, vector}
}
