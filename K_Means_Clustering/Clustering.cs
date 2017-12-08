using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_Means_Clustering
{
    abstract class Clustering
    {
        public static int quantityOfClusters;
       

        public static void SetQuantity()
        {
            Console.WriteLine("How many Clusters: ");
            quantityOfClusters = Convert.ToInt32(Console.ReadLine());
        }

        public static double[][] ConvertToNormalize(double[][] DataSet)
        {
            double[][] copyData = new double[DataSet.Length][];
            for (int i = 0; i < DataSet.Length; i++)
            {
                copyData[i] = new double[DataSet[i].Length];
                for (int j = 0; j < DataSet[i].Length; j++)
                {
                    copyData[i][j] = DataSet[i][j];
                }
            }

            
            for (int i = 0; i < copyData[0].Length; i++) 
            {
                double SumInSingleColumn = 0.00;

                for (int j = 0; j < copyData.Length; j++)
                {
                    SumInSingleColumn += copyData[j][i];
                }

                double mean = SumInSingleColumn / copyData.Length;
                double sum = 0.00;

                for (int j = 0; j < copyData.Length; j++)
                {
                    sum += (copyData[j][i] - mean) * (copyData[j][i] - mean);
                }

                double sum_substract = sum / copyData.Length;

                for (int j = 0; j < copyData.Length; j++)
                {
                    copyData[j][i] = (copyData[j][i] - mean) / sum_substract;
                }
            }
            return copyData;
        }

        // First clusters assign
        public static int[] Initialize(int quantityOfSingleData, int quantityOfClusters)
        {
            Random R = new Random();
            int[] cluster = new int[quantityOfSingleData];

            for (int i = 0; i < quantityOfClusters; i++)
            {
                cluster[i] = i;
            }
            for (int i = quantityOfClusters; i < cluster.Length; i++)
            {
                cluster[i] = R.Next(0, quantityOfClusters);
            }
            return cluster;
        }

        // Core of the clustering algorithm
        public static int[] EnableClustering(double[][] DataSet, int quantityOfClusters)
        {
            double[][] NormalizedData = ConvertToNormalize(DataSet);
            bool change_in_claster_occur = true;
            bool succes_mean_compute = true;

            int[] cluster = Initialize(NormalizedData.Length, quantityOfClusters);
            double[][] means = Allocate(quantityOfClusters, NormalizedData[0].Length);

            int maxIterationLimit = NormalizedData.Length * 10;
            int iterator = 0;

            while (change_in_claster_occur == true && succes_mean_compute == true && iterator < maxIterationLimit)
            {
                iterator++;
                succes_mean_compute = UpdateMeans(NormalizedData, cluster, means);
                change_in_claster_occur = UpdateClustering(NormalizedData, cluster, means);

            }

            return cluster;
        }
    }
}
