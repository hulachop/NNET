# NNET
NNET is a C# class library for modeling and training neural networks using various layer types, activation functions, cost functions and optimizers.
## Modeling
You can create a network using the Network class:

`public Network(object inputSize, Layer[] layers)`

Input size supports int for one dimensional inputs and Vector2Int for two dimensional ones. The layers array specifies the layers within the network.

Example network:

```
using NNET;

Network network = new Network(10, new Layer[]{
  new FullyConnected(20),
  new FullyConnected(20),
  new FullyConnected(2)
});
```
## Training
You can train your network using the `Network.Backpropagate` function.

Example training loop:

```
Vector[,] data = new Vector[1000,2];
// Initialize your data
float LR = 0.5f;
for(int epoch = 0; epoch < 10; epoch++){
  Console.WriteLine("epoch " + epoch);
  for(int i = 0; i < 1000; i++){
    network.Backpropagate(data[i,0], data[i,1], LR);
    Console.WriteLine("sample " + (i+1) + "/1000:\ncost: " + network.cost);
  }
}
```
## Layer types
### FullyConnected
A regular fully connected perceptron layer.

Configurable variables:
  - output size
### Convolution
A convolution layer.

Configurable variables:
  - kernel size
  - kernel number
  - stride
  - padding
### MaxPooling
A max pooling layer.

Configurable variables:
  - pool size
  - stride
### Flatten layer
A layer which flattens a matrix. (Converts it into a vector)
## Activation functions
 - Relu
 - Sigmoid
 - Softmax
 - Tanh
## Cost functions
 - Mean squared
 - Mean absolute
 - Cross entropy
## Optimizers
 - MiniBatch
 - SGD (Stochastic gradient descent)
 - Momentum SGD
 - Adam

