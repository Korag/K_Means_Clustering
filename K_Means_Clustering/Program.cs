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
            Stopwatch sw2 = new Stopwatch();

            double[][][] Matrix = new double[100][][];
            double[][] Iris = DataSet.GetDataSet(@"C:\Users\user\Documents\Visual Studio 2017\Projects\K_Means_Clustering\iris.txt");
            DataSet.ShowDataSet(Iris);

            Console.WriteLine();
            Clustering.SetQuantity();
            Console.WriteLine();
 

            sw.Start();

            int[] cluster = Clustering.EnableClustering(Iris, Clustering.quantityOfClusters);
            Clustering.FirstIteration(Iris, Matrix, cluster);

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
            int iterationsNumber = Convert.ToInt32(Console.ReadLine());

            sw2.Start();
            
            Clustering.LoopIteration(Iris, Matrix, iterationsNumber, @"C:\Users\user\Documents\Visual Studio 2017\Projects\K_Means_Clustering\cluster");

            sw2.Stop();



            Console.WriteLine("End of the process ");
            Console.WriteLine("1 iteration at time: " + sw.Elapsed);
            Console.WriteLine("Others iterations at time: " + sw2.Elapsed);
            Console.ReadLine();
        }
    }
}



