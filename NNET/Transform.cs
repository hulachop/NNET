using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    class Transform
    {
        public static object Apply(object input, object input2, Func<float,float,float> func, datatype dt)
        {
            switch(dt)
            {
                case datatype.vector:
                    return Apply(input as float[], input2 as float[], func);
                case datatype.matriceList:
                    return Apply(input as List<Matrix>, input2 as List<Matrix>, func);
                default:
                    return null;
            }
        }

        public static float Sum(object input, datatype dt)
        {
            switch(dt)
            {
                case datatype.vector:
                    return Sum(input as float[]);
                case datatype.matriceList:
                    return Sum(input as List<Matrix>);
            }
            return 0;
        }

        private static List<Matrix> Apply(List<Matrix> input, List<Matrix> input2, Func<float,float,float> func)
        {
            List<Matrix> output = new List<Matrix>();
            for(int i = 0; i < input.Count; i++)
            {
                output.Add(new Matrix(input[i].size.x, input[i].size.y));
                for(int x = 0; x < input[i].size.x; x++)
                {
                    for(int y = 0; y < input[i].size.y; y++)
                    {
                        output[i].Set(x, y, func.Invoke(input[i].Get(x, y), input[i].Get(x,y)));
                    }
                }
            }
            return output;
        }

        private static float[] Apply(float[] input, float[] input2, Func<float,float,float> func)
        {
            float[] output = new float[input.Length];
            for(int i = 0; i < input.Length; i++)
            {
                output[i] = func(input[i],input2[i]);
            }
            return output;
        }

        private static float Sum(float[] input)
        {
            float sum = 0;
            for(int i = 0; i < input.Length; i++)
            {
                sum += input[i];
            }
            return sum;
        }

        private static float Sum(List<Matrix> input)
        {
            float sum = 0;
            for(int i = 0; i < input.Count; i++)
            {
                for (int x = 0; x < input[i].size.x; x++)
                {
                    for(int y = 0; y < input[i].size.y; y++)
                    {
                        sum += input[i].Get(x, y);
                    }
                }
            }
            return sum;
        }
    }
}
