using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pollux.Maths
{
    /// <summary>
    /// calculate things such as primes
    /// </summary>
    class maths
    {
        //primes
        #region
        /// <summary>
        /// Method to calculate primes up to a given number
        /// </summary>
        /// <param name="up_to">method caculates the primes up to this parameter</param>
        /// <returns>returns primes in a list of integers</returns>
        public static List<int> Prime(int up_to)
        {
            /* WHAT IS THE IDEA BEHIND THIS ALGORITHM?
             * The Idea is simple. It works kind of like the Eratosthenes-algorithm, but instead of 
             * saving the data in a list of numbers, it saves it in a BitArray, which simply is
             * a collection/ list of booleans. After calculating the primes, it tranlates the 
             * BitArray into a list of numbers. With this way, not just efficency is increased a
             * lot, but it also saves a lot of data. In the usual algorithm, where the data/primes
             * are saved in a list of numbers, every number/prime is represented by 4+ bytes, while
             * this algorithm it is only 1 bit, so only 1/32...
             * 
             * The actual numbers are represented by the indexes in the BitArray (number = index * 2 + 3  -> +3 because 
             * the first number in the BitArray is 3 and *2 because there are no even numbers;
             * index = (number - 3) / 2) and the bool says, whether the number is a prime or not
             */

            //create a new BitArray, which has the right length und set all in the BitArray to true
            BitArray bitArray = new BitArray((up_to - 1) / 2);
            bitArray.SetAll(true);

            /* calculate the primes
             * "i": "i" represents the number, which multiples can't be primes, i always gets added with 2, because even numbers aren't represented in the list
             * "multiples": multiples of "i", they won't be primes, so their bools will be set to false; multiples starting at i*3 because i is a prime and i*2 is even, so not in the list,
             *              also always add +2*i because +i will be even
             */
            for (int i = 3; i * i <= up_to; i += 2)
            {
                for (int multiples = i * 3; multiples <= up_to; multiples += 2 * i)
                {
                    //set multiples of i to false
                    bitArray.Set((multiples - 3) / 2, false);
                }
            }

            //translating the BitArray into a list of numbers and add 2 to it, because 2 is a Prime, but the algorithm only calculates odd primes
            List<int> primes = new List<int>();
            primes.Add(2);

            for (int i = 0; i < bitArray.Count; i++)
            {
                //if the number is prime, add it to the list of numbers (using index*2+3 because that equals the number; see  in the big comment up there)
                if (bitArray.Get(i))
                {
                    primes.Add(i * 2 + 3);
                }
            }

            //return the list of numbers
            return primes;
        }
        #endregion

        //prime factorization
        #region
        /// <summary>
        /// Merhod to calculate a prime factorization; is faster than the other one because it already has primes.
        /// </summary>
        /// <param name="number">number, of which the prime factorization will be calculated</param>
        /// <param name="Primes">list of primes, which has to contain the primes at least up to the number, of which the prime factorization is going to be calculated; this list makes the method faster</param>
        /// <returns>Returns a list of integers which are the primes of the prime factorization. If the number already is a prime, there will be a 1 on the 1st postion of the list.</returns>
        public static List<int> PrimeFactorization(int number, List<int> Primes)
        {
            //declaring list
            List<int> Primfaktorzerlegung = new List<int>();
            int element = 0;
            int Rest = number;
            if (number == Primes[Primes.Count() - 1])
            {
                //if the number is a prime, the programm doesn't calculate the primes
                Primfaktorzerlegung.Add(1);
                Primfaktorzerlegung.Add(number);
            }
            else
            {
                //Programm calculates the prime factorization of the wanted number
                while (Rest != 1)
                {
                    if (Rest % Primes[element] == 0)
                    {
                        Primfaktorzerlegung.Add(Primes[element]);
                        Rest /= Primes[element];
                    }
                    else
                    {
                        element++;
                    }
                }
            }

            return Primfaktorzerlegung;
        }
        #endregion

        //prime factorization without primes
        #region
        /// <summary>
        /// Merhod to calculate a prime factorization.
        /// </summary>
        /// <param name="number">number, of which the prime factorization will be calculated</param>
        /// <returns>Returns a list of integers which are the primes of the prime factorization. If the number already is a prime, there will be a 1 on the 1st postion of the list.</returns>
        public static List<ulong> PrimeFactorization(ulong number)
        {
            //declaring list
            List<ulong> Primfaktorzerlegung = new List<ulong>();
            ulong Rest = number;

            for (; Rest % 2 == 0; Rest /= 2, Primfaktorzerlegung.Add(2)) ;

            //calculating prime factorization
            for (ulong i = 3; Rest != 1;)
            {
                if (Rest % i == 0)
                {
                    Rest /= i;
                    Primfaktorzerlegung.Add(i);
                }
                else
                {
                    i += 2;
                }
            }

            if (Primfaktorzerlegung.Count() == 1)
            {
                Primfaktorzerlegung.Insert(0, 1);
            }

            return Primfaktorzerlegung;
        }
        #endregion

        //ggT with two numbers
        #region
        /// <summary>
        /// calculates the ggT of two numbers
        /// </summary>
        /// <param name="number1">1st number</param>
        /// <param name="number2">2nd number</param>
        /// <returns>returns a ulong, which is the ggT</returns>
        public static ulong ggT(ulong number1, ulong number2)
        {
            //declaring and initialising variables
            ulong clone1 = number1;
            ulong clone2 = number2;

            //calculating the ggT
            while (clone1 != 0 & clone2 != 0)
            {
                if (clone1 > clone2)
                {
                    clone1 %= clone2;
                }
                else
                {
                    clone2 %= clone1;
                }
            }

            if (clone1 == 0)
            {
                return clone2;
            }
            else
            {
                return clone1;
            }
        }
        #endregion

        //ggT but with a list for calculating the ggT of multiple numbers
        #region
        /// <summary>
        /// Method to calculate the ggT of multiple numbers; needs the overload of this method
        /// </summary>
        /// <param name="list">list containing the numbers</param>
        /// <returns>returns the ggT of all the numbers</returns>
        public static ulong ggT(List<ulong> list)
        {
            //calculating the ggt of all  the numbers in the list
            ulong number;
            ulong ggT = list[0];
            for (int i = 1; i < list.Count(); i++)
            {
                number = list[i];
                ggT = maths.ggT(ggT, number);
            }
            return ggT;
        }
        #endregion

        //kgV with two numbers
        #region
        /// <summary>
        /// Method to calculate the kgV of two numbers
        /// </summary>
        /// <param name="number1">1st number</param>
        /// <param name="number2">2nd number</param>
        /// <returns>returns the kgV of the two parameters</returns>
        public static ulong kgV(ulong number1, ulong number2)
        {
            //declaring lists and variables
            ulong counter;
            ulong biggerNumber;
            ulong smallerNumber;

            //finding out which of the numbers is the smaller/bigger one and save it
            if (number1 > number2)
            {
                biggerNumber = number1;
                smallerNumber = number2;
            }
            else
            {
                biggerNumber = number2;
                smallerNumber = number1;
            }

            counter = 1;

            for (; (counter * biggerNumber) % smallerNumber != 0; counter++) ;

            return counter * biggerNumber;
        }
        #endregion

        //kgV but with a list for calculating the kgV of multiple numbers
        #region
        /// <summary>
        /// Method to calculate the kgV of multiple numbers; needs the overload of this method
        /// </summary>
        /// <param name="list">A list, containing all of the numbers</param>
        /// <returns>returns a ulong which is the kgV of all of the numbers in the list</returns>
        public static ulong kgV(List<ulong> list)
        {
            //calculating the kgV of all  the numbers in the list
            ulong number;
            ulong kgV = list[0];
            for (int i = 1; i < list.Count(); i++)
            {
                number = list[i];
                kgV = maths.kgV(kgV, number);
            }
            return kgV;
        }
        #endregion

        //root, with its overloads for different datatypes
        #region
        /// <summary>
        /// Method to calculate the root of a number
        /// </summary>
        /// <param name="radicand">is the radicand</param>
        /// <param name="root">which root it should calculate</param>
        /// <param name="nachkommastellen">how many decimal places it should calculate; cannot be to big</param>
        /// <param name="round">if the value should get rounded, thus it needs to calculate one decimal place more</param>
        /// <returns>a decimal which is the root</returns>
        public static decimal root(decimal radicand, int root, int nachkommastellen, bool? round)
        {
            //calculate the root by trying whether the number in exponent already is bigger than the radicand

            /*always adding the variable plus and looking whether the exponent is bigger than the radicand
            than substracting the variable plus and divide it by 10
            doing all again, as long as it isn't already the solution or the number is long enough*/

            //if the variable "nachkommastellen" is smaller than 0, throw an exception
            if (nachkommastellen < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            //if the root is zero throw an exception
            if (root == 0)
            {
                throw new ArgumentNullException();
            }

            //if radicand is negativ and the root is even, throw an exception
            if (radicand < 0 & root % 2 == 0)
            {
                throw new Exceptions.RadicandCannotBeNegativeException();
            }

            //if the variable Nachkommastellen is too long, throw an exception, because it would end in an endless loop
            if (nachkommastellen > 32423)
            {
                throw new Exceptions.DecimalIsTooLong();
            }

            //if it should round, add one in the variable "nachkommastellen" to be able to round the value
            if (round == true)
            {
                nachkommastellen++;
            }

            decimal solution = 0;
            decimal plus = 100000;
            int i = 0;

            if (root > 0)
            {
                while (i < nachkommastellen + 6 & radicand != maths.exponent(solution, root))
                {
                    while (maths.exponent(solution, root) <= radicand)
                    {
                        solution += plus;
                    }
                    solution -= plus;
                    plus /= 10;
                    i++;
                }
            }
            else
            {
                while (i < nachkommastellen + 6 & radicand != maths.exponent(solution, root))
                {
                    while (maths.exponent(solution, root) >= radicand)
                    {
                        solution += plus;
                    }
                    solution -= plus;
                    plus /= 10;
                    i++;
                }
            }

            //round the value if wanted
            if (round == true)
            {
                solution = Math.Round(solution, nachkommastellen - 1);
            }

            return solution;
        }
        #endregion

        #region
        /// <summary>
        /// Method to calculate the root of a number
        /// </summary>
        /// <param name="radicand">is the radicand</param>
        /// <param name="root">which root it should calculate</param>
        /// <param name="nachkommastellen">how many decimal places it should calculate; cannot be to big</param>
        /// <param name="round">if the value should get rounded, thus it needs to calculate one decimal place more</param>
        /// <returns>a decimal which is the root</returns>
        public static decimal root(long radicand, int root, int nachkommastellen, bool? round)
        {
            //calculate the root by trying whether the number in exponent already is bigger than the radicand

            /*always adding the variable plus and looking whether the exponent is bigger than the radicand
            than substracting the variable plus and divide it by 10
            doing all again, as long as it isn't already the solution or the number is long enough*/

            //if the variable "nachkommastellen" is smaller than 0, throw an exception
            if (nachkommastellen < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            //if the root is zero throw an exception
            if (root == 0)
            {
                throw new ArgumentNullException();
            }

            //if radicand is negativ and the root is even, throw an exception
            if (radicand < 0 & root % 2 == 0)
            {
                throw new Exceptions.RadicandCannotBeNegativeException();
            }

            //if the variable Nachkommastellen is too long, throw an exception, because it would end in an endless loop
            if (nachkommastellen > 32423)
            {
                throw new Exceptions.DecimalIsTooLong();
            }

            //if it should round, add one in the variable "nachkommastellen" to be able to round the value
            if (round == true)
            {
                nachkommastellen++;
            }

            decimal solution = 0;
            decimal plus = 100000;
            int i = 0;

            if (root > 0)
            {
                while (i < nachkommastellen + 6 & radicand != maths.exponent(solution, root))
                {
                    while (maths.exponent(solution, root) <= radicand)
                    {
                        solution += plus;
                    }
                    solution -= plus;
                    plus /= 10;
                    i++;
                }
            }
            else
            {
                while (i < nachkommastellen + 6 & radicand != maths.exponent(solution, root))
                {
                    while (maths.exponent(solution, root) >= radicand)
                    {
                        solution += plus;
                    }
                    solution -= plus;
                    plus /= 10;
                    i++;
                }
            }

            //round the value if wanted
            if (round == true)
            {
                solution = Math.Round(solution, nachkommastellen - 1);
            }

            return solution;
        }
        #endregion

        #region
        /// <summary>
        /// Method to calculate the root of a number
        /// </summary>
        /// <param name="radicand">is the radicand</param>
        /// <param name="root">which root it should calculate</param>
        /// <param name="nachkommastellen">how many decimal places it should calculate; cannot be to big</param>
        /// <param name="round">if the value should get rounded, thus it needs to calculate one decimal place more</param>
        /// <returns>a decimal which is the root</returns>
        public static double root(double radicand, int root, int nachkommastellen, bool? round)
        {
            //calculate the root by trying whether the number in exponent already is bigger than the radicand

            /*always adding the variable plus and looking whether the exponent is bigger than the radicand
            than substracting the variable plus and divide it by 10
            doing all again, as long as it isn't already the solution or the number is long enough*/

            //if the variable "nachkommastellen" is smaller than 0, throw an exception
            if (nachkommastellen < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            //if the root is zero throw an exception
            if (root == 0)
            {
                throw new ArgumentNullException();
            }

            //if radicand is negativ and the root is even, throw an exception
            if (radicand < 0 & root % 2 == 0)
            {
                throw new Exceptions.RadicandCannotBeNegativeException();
            }

            //if the variable Nachkommastellen is too long, throw an exception, because it would end in an endless loop
            if (nachkommastellen > 32423)
            {
                throw new Exceptions.DecimalIsTooLong();
            }

            //if it should round, add one in the variable "nachkommastellen" to be able to round the value
            if (round == true)
            {
                nachkommastellen++;
            }

            double solution = 0;
            double plus = 100000;
            int i = 0;

            if (root > 0)
            {
                while (i < nachkommastellen + 6 & radicand != maths.exponent(solution, root))
                {
                    while (maths.exponent(solution, root) <= radicand)
                    {
                        solution += plus;
                    }
                    solution -= plus;
                    plus /= 10;
                    i++;
                }
            }
            else
            {
                while (i < nachkommastellen + 6 & radicand != maths.exponent(solution, root))
                {
                    while (maths.exponent(solution, root) >= radicand)
                    {
                        solution += plus;
                    }
                    solution -= plus;
                    plus /= 10;
                    i++;
                }
            }

            //round the value if wanted
            if (round == true)
            {
                solution = Math.Round(solution, nachkommastellen - 1);
            }

            return solution;
        }
        #endregion

        //exponent, with its overloads for different datatypes
        #region
        /// <summary>
        /// Method to calculate the power
        /// </summary>
        /// <param name="number1">the base</param>
        /// <param name="number2">the exponent</param>
        /// <returns>returns the solution</returns>
        public static decimal exponent(decimal number1, long number2)
        {
            //calculating the number2nd exponent of number1
            //Note: It's not good for the performance, needs an update

            if (number2 == 0)
            {
                return 1;
            }
            else if (number2 < 0)
            {
                return 1 / exponent(number1, number2 * -1);
            }
            else
            {
                decimal solution = number1;
                for (uint i = 0; i < number2 - 1; i++)
                {
                    solution *= number1;
                }
                return solution;
            }
        }
        #endregion

        #region
        /// <summary>
        /// Method to calculate the power
        /// </summary>
        /// <param name="number1">the base</param>
        /// <param name="number2">the exponent</param>
        /// <returns>returns the solution</returns>
        public static long exponent(long number1, long number2)
        {
            //calculating the number2nd exponent of number1
            //Note: It's not good for the performance, needs an update

            if (number2 == 0)
            {
                return 1;
            }
            else if (number2 < 0)
            {
                return 1 / exponent(number1, number2 * -1);
            }
            else
            {
                long solution = number1;
                for (uint i = 0; i < number2 - 1; i++)
                {
                    solution *= number1;
                }
                return solution;
            }
        }
        #endregion

        #region
        /// <summary>
        /// Method to calculate the power
        /// </summary>
        /// <param name="number1">the base</param>
        /// <param name="number2">the exponent</param>
        /// <returns>returns the solution</returns>
        public static double exponent(double number1, long number2)
        {
            //calculating the number2nd exponent of number1
            //Note: It's not good for the performance, needs an update

            if (number2 == 0)
            {
                return 1;
            }
            else if (number2 < 0)
            {
                return 1 / exponent(number1, number2 * -1);
            }
            else
            {
                double solution = number1;
                for (uint i = 0; i < number2 - 1; i++)
                {
                    solution *= number1;
                }
                return solution;
            }
        }
        #endregion
    }

    public static class Exceptions
    {
        public class RadicandCannotBeNegativeException : Exception
        {
            public new string Message = "Der Radikand darf nicht negativ sein!";
        }

        public class DecimalIsTooLong : Exception
        {
            public new string Message = "Die Zahl kann nicht so viele Nachkommastellen haben.";
        }
    }
}