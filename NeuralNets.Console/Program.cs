// See https://aka.ms/new-console-template for more information
using NeuralNets.Console.Trainers.Unsupervised;
using NeuralNets.Functions;
using NeuralNets.Model.Configuration;
using NeuralNets.Model.Neural;
using NeuralNets.Model.Neural.Nodes;

Layer<InputNode> inputs = new Layer<InputNode>(0) 
{ 
    new InputNode(1),
    new InputNode(1)
};

Layer<Node> hidden = new(1)
{
    new Node(),
    new Node(),
    new Node()
};

Layer<Node> outputs = new(2)
{
    new Node(),
    new Node()
};

Network network = new Network(inputs, outputs, Activation.Linear, Aggregation.Average);
network.Add(hidden);

// Layer 1 -->  2
network.Connect(inputs[0], hidden[0]);
network.Connect(inputs[1], hidden[0]);
network.Connect(inputs[1], hidden[1]);
network.Connect(inputs[1], hidden[2]);

// Layer 2 --> 3
network.Connect(hidden[0], outputs[0]);
network.Connect(hidden[1], outputs[0]);
network.Connect(hidden[1], outputs[1]);
network.Connect(hidden[2], outputs[1]);

float[] predictions = network.Predict(new float[] { 8, 2 });

UnsupervisedTrainingConfiguration config = new()
{
    Population = 10
};

UnsupervisedTest test = new(network, config);

Console.WriteLine("Network built");