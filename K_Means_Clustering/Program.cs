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

      
            double[][] Iris = DataSet.GetDataSet(@"C:\Users\Łukasz\Documents\Visual Studio 2017\Projects\K_Means_Clustering\iris.txt");
            DataSet.ShowDataSet(Iris);

            Console.WriteLine();
            Clustering.SetQuantity();
            Console.WriteLine();

            double[][][] Matrix = new double[100][][];
            

            sw.Start();
            int[] cluster = Clustering.EnableClustering(Iris, Clustering.quantityOfClusters);
            for (int i = 0; i < Clustering.quantityOfClusters; i++)
            {
                Matrix[Clustering.ClustersMade] = DataSet.AddOneClusterToMatrix(Iris, cluster, Clustering.quantityOfClusters);
                Clustering.ClustersMade++;
            }
            DataSet.numerator = 0;
            sw.Stop();

            
            DataSet.ShowVector(cluster);
            DataSet.SaveVectorToFile(cluster, @"C:\Users\Łukasz\Documents\Visual Studio 2017\Projects\K_Means_Clustering\iris_vector.txt");
           
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Data in cluster:");

            DataSet.ShowClustered(Iris, cluster, Clustering.quantityOfClusters);
            DataSet.SaveClusteredToFile(Iris, cluster, Clustering.quantityOfClusters, @"C:\Users\Łukasz\Documents\Visual Studio 2017\Projects\K_Means_Clustering\iris_clustered.txt");

            DataSet.SaveEachClusterToFile(Iris, cluster, Clustering.quantityOfClusters, @"C:\Users\Łukasz\Documents\Visual Studio 2017\Projects\K_Means_Clustering\cluster");

           




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
                    for (int j = 0; j < Clustering.quantityOfClusters; j++)
                    {
                        Matrix[Clustering.ClustersMade] = DataSet.AddOneClusterToMatrix(Matrix[p], clusterHelper, Clustering.quantityOfClusters);
                       // DataSet.SaveOneClusterToFile(Matrix[Clustering.ClustersMade], clusterHelper, Clustering.quantityOfClusters, @"C:\Users\Łukasz\Documents\Visual Studio 2017\Projects\K_Means_Clustering\cluster");
                        Clustering.ClustersMade++;
                    }
                    DataSet.numerator = 0;
                    p++;
                }
            }










            Console.WriteLine("End of the process at time: " + sw.Elapsed);
            Console.ReadLine();
        }
    }
}



