using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public static class RandomUtils
    {
        public static int NumberRandomizer(int startNum, int endNUm)
        {
            Random rnd = new Random();
            return rnd.Next(startNum, endNUm);
        }

        public static int DefaultNumberRandomizer()
        {
            Random rnd = new Random();
            return rnd.Next(1, 5);
        }
    }
}
