using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace K_Means_Clustering
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();

            double[][] Iris = DataSet.GetDataSet(@"C:\Users\user\Documents\Visual Studio 2017\Projects\K_Means_Clustering\iris.txt");
            DataSet.ShowDataSet(Iris);

            Console.WriteLine();
            Clustering.SetQuantity();
            Console.WriteLine();

            double[][][] Matrix = new double[100][][];

            sw.Start();
            int[] cluster = Clustering.EnableClustering(Iris, Clustering.quantityOfClusters);
            Matrix = DataSet.AddToMatrix(Iris, cluster, Clustering.quantityOfClusters, Clustering.ClustersMade);
            Clustering.ClustersMade += Clustering.quantityOfClusters;
            sw.Stop();

            
            DataSet.ShowVector(cluster);
            DataSet.SaveVectorToFile(cluster, @"C:\Users\user\Documents\Visual Studio 2017\Projects\K_Means_Clustering\iris_vector.txt");
           
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Data in cluster:");

            DataSet.ShowClustered(Iris, cluster, Clustering.quantityOfClusters);
            DataSet.SaveClusteredToFile(Iris, cluster, Clustering.quantityOfClusters, @"C:\Users\user\Documents\Visual Studio 2017\Projects\K_Means_Clustering\iris_clustered.txt");

            DataSet.SaveEachClusterToFile(Iris, cluster, Clustering.quantityOfClusters, @"C:\Users\user\Documents\Visual Studio 2017\Projects\K_Means_Clustering\cluster");


            Console.WriteLine("How many more iterations: ");
            int s = Convert.ToInt32(Console.ReadLine());

            // pomocnicze iteratory
            int p = 0;
            // ilosc ostatnio stworzonych klastrow jako ograniczenie w petli, a poczatek matrix petla z parametru ClustersMade
            int ClusterMark = 0;
            for (int k = 0; k < s; k++)
            {
                int HigherLevelQuantity = Clustering.quantityOfClusters;
                Clustering.SetQuantity();
                ClusterMark = Clustering.ClustersMade;


                for (int i = 0; i < HigherLevelQuantity; i++)
                {
                    int[] clusterHelper = Clustering.EnableClustering(Matrix[p], Clustering.quantityOfClusters);
                    Matrix = DataSet.AddToMatrix(Matrix[Clustering.ClustersMade], cluster, Clustering.quantityOfClusters, Clustering.ClustersMade);
                    DataSet.SaveEachClusterToFile(Matrix[Clustering.ClustersMade], cluster, Clustering.quantityOfClusters, @"C:\Users\user\Documents\Visual Studio 2017\Projects\K_Means_Clustering\cluster");
                    Clustering.ClustersMade += Clustering.quantityOfClusters;
                    p++;
                }
            }





            //Console.WriteLine("How many more iterations: ");
            //int sumOfFiles = 0;
            //int Level1Quantity = Clustering.quantityOfClusters;
            //int s = Convert.ToInt32(Console.ReadLine());
            //s += Clustering.quantityOfClusters;
            //int m = 0;
            //for (int k = Level1Quantity; k < s; k++)
            //{
            //    int quantityOfLevel = Clustering.quantityOfClusters;
            //    double[][][] ClustersSubData = new double[30][][];
            //    Clustering.SetQuantity();
            //    for (int i = m; i < m + quantityOfLevel; i++)
            //    {

            //        ClustersSubData[i] = DataSet.GetDataSet_($@"C:\Users\user\Documents\Visual Studio 2017\Projects\K_Means_Clustering\cluster{i}.txt");

            //        int[] cluster2Level = Clustering.EnableClustering(ClustersSubData[i], Clustering.quantityOfClusters);
            //        DataSet.SaveEachClusterToFile(ClustersSubData[i], cluster2Level, Clustering.quantityOfClusters, $@"C:\Users\user\Documents\Visual Studio 2017\Projects\K_Means_Clustering\cluster{k}");

            //    }
            //    m += quantityOfLevel;
            //}






            Console.WriteLine("End of the process at time: " + sw.Elapsed);
            Console.ReadLine();
        }
    }
}



