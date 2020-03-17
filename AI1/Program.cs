using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI1
{
    class Program
    {
        private static Random _rand;

        static void Main(string[] args)
        {
            _rand = new Random();
            Int32[] elements = new Int32[20];
            for (Int32 i = 0; i < elements.Length; i++)
            {
                elements[i] = _rand.Next(0, 100);
            }

            for (Int32 i = 0; i < elements.Length; i++)
                Console.WriteLine(" Element " + (i + 1) + " = " + elements[i]);

            Console.WriteLine();

            //Int32[] shuffled;
            //Boolean success = Shuffle(elements, out shuffled);

            //if (success)
            //    for(Int32 i = 0; i < shuffled.Length; i++)
            //        Console.WriteLine(" Element " + (i + 1) + " = " + shuffled[i]);

            Int32[] normalized;
            Int32 min = elements.Min();
            Int32 max = elements.Max();
            Boolean succ = Normalize(elements, min, max, 20, 30, out normalized);

            //if(succ)
            //    foreach(Int32 item in normalized)
            //        Console.WriteLine(item);

            String path = @"iris.txt";
            String[] flowersData = File.ReadAllLines(path);
            List<Flower> flowers = new List<Flower>();
            int ie = 0;
            foreach(String flowerLine in flowersData)
            {
                ie++;
                String[] flowerDataSep = flowerLine.Split(',');
                FlowerKind kind = FlowerKind.Setosa;
                if (flowerDataSep[4] == "Iris-setosa")
                    kind = FlowerKind.Setosa;
                else if (flowerDataSep[4] == "Iris-versicolor")
                    kind = FlowerKind.VersiColor;
                else if (flowerDataSep[4] == "Iris-virginica")
                    kind = FlowerKind.Virginica;

                flowers.Add(new Flower
                {
                    CupLength = Convert.ToDouble(flowerDataSep[0], new CultureInfo("en-US")),
                    CupWidth = Convert.ToDouble(flowerDataSep[1], new CultureInfo("en-US")),
                    PetalLength = Convert.ToDouble(flowerDataSep[2], new CultureInfo("en-US")),
                    PetalWidth = Convert.ToDouble(flowerDataSep[3], new CultureInfo("en-US")),
                    Kind = kind
                });
            }

            Flower[] flowersShuffled = null;
            Shuffle(flowers.ToArray(), out flowersShuffled);

            Console.ReadKey();
        }

        private static Boolean Shuffle<T>(T[] elements, out T[] shuffled)
        {
            shuffled = null;
            if (elements == null)
                return false;

            List<Int32> indexes = Enumerable.Range(0, elements.Length).ToList();
            List<T> reordered = new List<T>();
            
            for (Int32 i = 0; i < elements.Length; i++)
            {
                Int32 indexToAssign = _rand.Next(0, indexes.Count);
                reordered.Add(elements[indexToAssign]);
                indexes.Remove(indexToAssign);
            }

            shuffled = reordered.ToArray();
            return true;

        }

        private static Boolean Normalize(Int32[] elements, Int32 min, Int32 max, Int32 nmin, Int32 nmax, out Int32[] normalized)
        {
            normalized = null;
            if (elements == null)
                return false;

            normalized = new Int32[elements.Length];

            for(Int32 i = 0; i < elements.Length; i++)
            {
                normalized[i] = (((elements[i] - min) / (max - min)) * (nmax - nmin)) + nmin;
            }

            return true;
        }

    }

    public class Flower
    {
        public Double CupLength { get; set; }

        public Double CupWidth { get; set; }

        public Double PetalLength { get; set; }

        public Double PetalWidth { get; set; }

        public FlowerKind Kind { get; set; }

    }

    public enum FlowerKind
    {
        Setosa = 1,
        VersiColor = 2,
        Virginica = 3
    }
}
