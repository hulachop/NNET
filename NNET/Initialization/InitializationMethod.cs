using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public abstract class InitializationMethod
    {
        public abstract Matrix Initialize(int i, int j);
        public abstract Vector Initialize(int i);
    }
    [Serializable]
    public class ZeroInitialization : InitializationMethod
    {
        public override Matrix Initialize(int I, int J)
        {
            return new Matrix(I, J);
        }

        public override Vector Initialize(int i)
        {
            return new Vector(i);
        }
    }
}
