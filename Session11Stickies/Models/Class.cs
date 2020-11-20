using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Session11Stickies.Models
{
    public class ExampleClass
    {
        public int Add(int x, int y)
        {
            return x + y;
        }

        public void DoStuff()
        {
            int height = 10;
            int length = 20;

            Console.WriteLine(Add(height, length));
        }

        public void DoOtherStuff()
        {
            int width = 10;
            int depth = 20;

            Console.WriteLine(Add(width, depth));
        }
    }
}
