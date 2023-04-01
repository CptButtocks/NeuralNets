// See https://aka.ms/new-console-template for more information
using NeuralNets.Functions;
using NeuralNets.Model;

List<Neuron> inputs = new List<Neuron>() 
{ 
    new Neuron(),
    new Neuron()
};

List<Neuron> hidden = new()
{
    new Neuron(),
    new Neuron(),
    new Neuron()
};

List<Neuron> outputs = new()
{
    new Neuron(),
    new Neuron()
};

Network network = new Network(inputs, outputs, Activation.Linear, Aggregation.Average);
network.AddRange(hidden);

// Layer 1 -->  2
network.Connect(inputs[0], hidden[0], 0.23f);
network.Connect(inputs[1], hidden[0], 0.23f);
network.Connect(inputs[1], hidden[1], 0.23f);
network.Connect(inputs[1], hidden[2], 0.23f);

// Layer 2 --> 3
network.Connect(hidden[0], outputs[0], 0.23f);
network.Connect(hidden[1], outputs[0], 0.23f);
network.Connect(hidden[1], outputs[1], 0.23f);
network.Connect(hidden[2], outputs[1], 0.23f);

float[] predictions = network.Predict(new float[] { 8, 2 });

Console.WriteLine("Network built");