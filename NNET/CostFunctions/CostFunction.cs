using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public abstract class CostFunction
    {
        public float Cost(object expectedOutput, object predictedOutput) => Cost((dynamic)expectedOutput, (dynamic)predictedOutput);
        public virtual float Cost(float expectedOutput, float predictedOutput)
        {
            return 1;
        }
        public virtual float Cost(Vector expectedOutput, Vector predictedOutput)
        {
            float cost = 0;
            for(int i = 0; i < expectedOutput.size; i++)
            {
                cost += Cost(expectedOutput[i], predictedOutput[i]);
            }
            return cost;
        }
        public virtual float Cost(Matrix expectedOutput, Matrix predictedOutput)
        {
            float cost = 0;
            for(int i = 0; i < expectedOutput.I; i++)
                for(int j = 0; j < expectedOutput.J; j++)
                {
                    cost += Cost(expectedOutput[i, j], predictedOutput[i, j]);
                }
            return cost;
        }
        public object Derivative(object expectedOutput, object predictedOutput) => Derivative((dynamic)expectedOutput, (dynamic)predictedOutput);
        public virtual object Derivative(float expectedOutput, float predictedOutput)
        {
            return 1;
        }
        public virtual object Derivative(Vector expectedOutput, Vector predictedOutput)
        {
            Vector v = new Vector(expectedOutput.size);
            for (int i = 0; i < v.size; i++) v[i] = (float)Derivative(expectedOutput[i], predictedOutput[i]);
            return v;
        }
        public virtual object Derivative(Matrix expectedOutput, Matrix predictedOutput)
        {
            Matrix m = new Matrix(expectedOutput.I, expectedOutput.J);
            for (int i = 0; i < m.I; i++)
                for (int j = 0; j < m.J; j++)
                    m[i, j] = (float)Derivative(expectedOutput[i, j], predictedOutput[i, j]);
            return m;
        }
    }
}
