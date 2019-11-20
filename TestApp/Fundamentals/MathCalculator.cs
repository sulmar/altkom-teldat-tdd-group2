using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{
    public interface IMathCalculator
    {
        int Max(int a, int b);
    }


    public class MLCalculator : IMathCalculator
    {
        public int Max(int a, int b)
        {

            // ...

            throw new NotImplementedException();

        }
    }

    public class MathCalculator : IMathCalculator
    {
        public int Add(int a, int b)
        {
            checked
            {
                return a + b;
            }
        }

        public int Max(int a, int b)
        {
            return (a > b) ? a : b;
        }

        public IEnumerable<int> GetPrimeNumbersBelow(int limit)
        {
            for (int i = 0; i < limit; i++)
            {
                if (IsPrime(i)) yield return i;
            }
        }

        private static bool IsPrime(int n)
        {
            if (n == 2)
            {
                return true;
            }
            if (n < 2 || n % 2 == 0)
            {
                return false;
            }
            for (int i = 3; i <= Math.Sqrt(n); i += 2)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
