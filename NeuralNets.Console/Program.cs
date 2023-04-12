// See https://aka.ms/new-console-template for more information
using NeuralNets.Console.Trainers.Unsupervised;
using NeuralNets.Functions;
using NeuralNets.Model.Configuration;
using NeuralNets.Model.Neural;
using NeuralNets.Model.Neural.Nodes;
using NeuralNets.Mutators;

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
network.Connect(hidden[2], outputs[0]);

float[] predictions = network.Predict(new float[] { 8, 2 });

UnsupervisedTrainingConfiguration config = new()
{
    Population = 10,
    AddConnectionProbability = 0.2f,
    AddNodeProbability = 0.2f,
    ModifyWeightProbability = 0.3f,
};

UnsupervisedTest test = new(network, config);

List<float[]> testInputs = new();
Random random = new Random();
for (int i = 0; i < 1000; i++)
{
    int a = random.Next(0, 20);
    int b = random.Next(0, 20);

    testInputs.Add(new float[] { a, b });
}

Genome elite = test.Train(1000, testInputs.ToArray());
Console.WriteLine($"Nodes added: {NeatMutator.NodesAdded}");
Console.WriteLine($"Connection added: {NeatMutator.ConnectionsAdded}");
Console.WriteLine($"Nodes removed: {NeatMutator.NodesRemoved}");
Console.WriteLine($"Connections removed: {NeatMutator.ConnectionsRemoved}");
Console.WriteLine($"Weights modified: {NeatMutator.WeightsModified}");
Console.WriteLine("All done");