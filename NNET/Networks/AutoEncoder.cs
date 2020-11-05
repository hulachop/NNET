using System;
using System.Collections.Generic;
using System.Text;

namespace NNET
{
    [Serializable]
    public class AutoEncoder
    {
        public Network encoder;
        public Network decoder;
        public Vector bottleneck;
        public AutoEncoder(Network _encoder, Network _decoder)
        {
            encoder = _encoder;
            decoder = _decoder;
        }

        public Vector Encode(object input)
        {
            return encoder.FeedForward(input) as Vector;
        }

        public object Decode(float[] input)
        {
            return decoder.FeedForward(input);
        }

        public object FeedForward(object input)
        {
            bottleneck = encoder.FeedForward(input) as Vector;
            return decoder.FeedForward(bottleneck);
        }

        public object Backpropagate(object input, float LR)
        {
            object output = FeedForward(input);
            object errors = decoder.BackpropagateRaw(output, input, LR) as Vector;
            errors = encoder.BackpropagateRaw(errors, LR);
            return errors;
        }
    }
}
