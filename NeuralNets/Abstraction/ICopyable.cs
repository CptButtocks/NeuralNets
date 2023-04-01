using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNets.Abstraction
{
    public interface ICopyable<T>
    {
        public T DeepCopy();
    }
}
