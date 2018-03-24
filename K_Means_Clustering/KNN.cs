using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_Means_Clustering
{
    abstract class KNN
    {
        private static double[] NewElement;
        private static double[] DistancesToNeighbours;
        private static int[] Discriminant;


        // Wprowadzanie nowego wektora, dla ktorego bedziemy poszukiwac najblizszych sasiadow oraz inicjalizacja macierzy
        private static void Initialize(double [][] DataSet)
        {
            string chain = Console.ReadLine();
            string[] SplittedChain = chain.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            NewElement = new double[SplittedChain.Length];

            for (int i = 0; i < SplittedChain.Length; i++)
            {
                NewElement[i] = Convert.ToDouble(SplittedChain[i]);
            }

            DistancesToNeighbours = new double[DataSet.Length];
            Discriminant = new int[DataSet.Length];
        }
        
        // Obliczanie dystansu do najblizszego sasiada
        private static double CountDistance(double[] vectorOfSingleRow, double[] DataVector)
        {
            double sumSquaredDifferencials = 0.00;
            for (int i = 0; i < vectorOfSingleRow.Length; i++)
            {
                sumSquaredDifferencials += Math.Pow((vectorOfSingleRow[i] - DataVector[i]), 2);
            }
            return sumSquaredDifferencials;
        }


        // Wypelnianie macierzy opisujacej
        private static void FillDiscriminantArray(double [][] DataSet)
        {
            for (int i = 0; i < DataSet.Length; i++)
            {
                Discriminant[i] = i;
            }
        }

        // Obliczanie dystansu wybranego elementu do kazdego wiersza macierzy DataSet
         private static void ComputeDistances(double [][] DataSet)
        {
            for (int i = 0; i < DataSet.Length; i++)
            {
                DistancesToNeighbours[i] = CountDistance(DataSet[i], NewElement);
            }
        }

        // Core KNN
        public static void SearchKNN(double [][] Matrix, int QuantityOfNeighboursToPresent)
        {
            Initialize(Matrix);

            FillDiscriminantArray(Matrix);
            ComputeDistances(Matrix);

            Array.Sort(DistancesToNeighbours, Discriminant);

            DataSet.PresentQuantityOfItems(Matrix, DistancesToNeighbours, Discriminant, QuantityOfNeighboursToPresent);
        }

    }
}
