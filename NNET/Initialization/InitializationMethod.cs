using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public abstract class InitializationMethod
    {
        public abstract Matrix Initialize(int i, int j, int inputSize, Random rnd);
        public abstract Vector Initialize(int i, int inputSize, Random rnd);
    }
    [Serializable]
    public class ZeroInitialization : InitializationMethod
    {
        public override Matrix Initialize(int I, int J, int inputSize, Random rnd)
        {
            return new Matrix(I, J);
        }

        public override Vector Initialize(int i, int inputSize, Random rnd)
        {
            return new Vector(i);
        }
    }
}
