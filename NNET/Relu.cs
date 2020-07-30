using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    public class Relu : ActivationFunction
    {
        public override float[] Apply(float[] layer)
        {
            for(int i = 0; i < layer.Length; i++)
            {
                if (layer[i] < 0) layer[i] = 0;
            }
            return layer;
        }

        public override List<Matrix> Apply(List<Matrix> layer)
        {
            for(int i = 0; i < layer.Count; i++)
            {
                for(int x = 0; x < layer[i].size.x; x++)
                {
                    for(int y = 0; y < layer[i].size.y; y++)
                    {
                        if (layer[i].Get(x, y) < 0) layer[i].Set(x, y, 0);
                    }
                }
            }
            return layer;
        }

        public override float Derivative(float applied)
        {
            if (applied == 0) return 0.01f;
            else return 1f;
        }
    }
}
