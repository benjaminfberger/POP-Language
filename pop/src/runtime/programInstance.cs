using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.runtime
{
    public class programInstance
    {
        public readonly string identity;
        public programInstance(string identity, string[] args)
        {
            this.identity = identity;
            init();
        }
        private void init()
        {

        }
    }
}
