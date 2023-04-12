using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNets.Model.Neural.Nodes
{
    public class InputNode : Node
    {
        private float _value;

        public override float Output => _value;

        public InputNode(float value)
        {
            _value = value;
        }

        public void SetInput(float value) => _value = value;
    }
}
