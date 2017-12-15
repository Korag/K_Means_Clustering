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

            sw.Start();
            int[] cluster = Clustering.EnableClustering(Iris, Clustering.quantityOfClusters);
            sw.Stop();
            //ss
            DataSet.ShowVector(cluster);
            DataSet.SaveVectorToFile(cluster, @"C:\Users\Łukasz\Documents\Visual Studio 2017\Projects\K_Means_Clustering\iris_vector.txt");
           
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Data in cluster:");
            DataSet.ShowClustered(Iris, cluster, Clustering.quantityOfClusters);
            DataSet.SaveClusteredToFile(Iris, cluster, Clustering.quantityOfClusters, @"C:\Users\Łukasz\Documents\Visual Studio 2017\Projects\K_Means_Clustering\iris_clustered.txt");

            Console.WriteLine("End of the process at time: " + sw.Elapsed);
            Console.ReadLine();
        }
    }
}



