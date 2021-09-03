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
        public virtual float Cost(Matrix[] expectedOutput, Matrix[] predictedOutput)
        {
            float cost = 0;
            for(int k = 0; k < expectedOutput.Length; k++)
                for(int i = 0; i < expectedOutput[k].I; i++)
                    for(int j = 0; j < expectedOutput[k].J; j++)
                    {
                        cost += Cost(expectedOutput[k][i, j], predictedOutput[k][i, j]);
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
        public virtual object Derivative(Matrix[] expectedOutput, Matrix[] predictedOutput)
        {
            Matrix[] m = new Matrix[expectedOutput.Length];
            for(int k = 0; k < m.Length; k++)
            {
                m[k] = new Matrix(expectedOutput[k].I, expectedOutput[k].J);
                    for (int i = 0; i < m[k].I; i++)
                        for (int j = 0; j < m[k].J; j++)
                            m[k][i, j] = (float)Derivative(expectedOutput[k][i, j], predictedOutput[k][i, j]);
            }
            return m;
        }
    }
}
