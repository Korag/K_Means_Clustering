using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_Means_Clustering
{
    class Program
    {
        static void Main(string[] args)
        {
            
            double[][] Iris = DataSet.GetDataSet(@"C:\Users\Łukasz\Documents\Visual Studio 2017\Projects\K_Means_Clustering\iris.txt");
            DataSet.ShowDataSet(Iris);

            Clustering.SetQuantity();

            int[] cluster = Clustering.EnableClustering(Iris, Clustering.quantityOfClusters);
           
            for (int i = 0; i < cluster.Length; i++)
            {
                Console.Write(cluster[i] + " ");
            }

            Console.WriteLine("Data in cluster:\n");
            DataSet.ShowClustered(Iris, cluster, Clustering.quantityOfClusters);

            Console.WriteLine("End of the process");
            Console.ReadLine();
        }
    }
}
