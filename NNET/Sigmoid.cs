using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    public class Sigmoid : ActivationFunction
    {
        public override float[] Apply(float[] layer)
        {
            for(int i = 0; i < layer.Length; i++)
            {
                if (double.IsNaN(layer[i])) throw new Exception();
                layer[i] = Func(layer[i]);
                if (double.IsNaN(layer[i])) throw new Exception();
            }
            return layer;
        }

        public override List<Matrix> Apply(List<Matrix> layer)
        {
            for(int m = 0; m < layer.Count; m++)
            {
                for(int x = 0; x < layer[m].size.x; x++)
                {
                    for(int y = 0; y < layer[m].size.y; y++)
                    {
                        layer[m].Set(x, y, Func(layer[m].Get(x, y)));
                    }
                }
            }
            return layer;
        }

        public override float Derivative(float applied)
        {
            return applied * (1 - applied);
        }

        private float Func(double value)
        {
            value = Math.Exp(value);
            if(float.IsNaN((float)value)) throw new Exception();
            return (float)(value / (value + 1.0));
        }
    }
}
