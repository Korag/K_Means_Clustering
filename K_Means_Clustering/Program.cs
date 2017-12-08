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
            
            double[][] Iris = DataSet.GetDataSet(@"C:\Users\user\Documents\Visual Studio 2017\temp\1\1\3.txt");
            DataSet.ShowDataSet(Iris);

            Clustering.SetQuantity();

            int[] cluster = Clustering.EnableClustering(Iris, Clustering.quantityOfClusters);

            

            Console.WriteLine("End of the process");
            Console.ReadLine();
        }
    }
}
