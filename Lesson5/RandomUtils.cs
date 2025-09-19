using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public static class RandomUtils
    {
        private static  readonly Random _rnd = new Random();
        public static int NumberRandomizer(int startNum, int endNUm)
        {
            return _rnd.Next(startNum, endNUm+1);
        }

        public static int OneToFiveNumberRandomizer()
        {
            return _rnd.Next(1, 6);
        }
    }
}
