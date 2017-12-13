using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace K_Means_Clustering
{
    class DataSet
    {
        public static double[][] GetDataSet(string path)
        {
            double[][] DataSet;
            try
            {
                string[] s1 = File.ReadAllLines(path);
                DataSet = new double[s1.Length][];
                for (int i = 0; i < s1.Length; i++)
                {
                    string[] s2 = s1[i].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    DataSet[i] = new double[s2.Length];
                    for (int j = 0; j < s2.Length; j++)
                    {
                        DataSet[i][j] = Double.Parse(s2[j], CultureInfo.InvariantCulture);
                    }
                }
                return DataSet;
            }

            catch
            {

            }
            return null;
        }

        public static void ShowDataSet(double[][] DataSet)
        {
            for (int i = 0; i < DataSet.Length; i++)
            {
                for (int j = 0; j < DataSet[i].Length; j++)
                {
                    Console.Write(DataSet[i][j].ToString("F" + 1) + " ");
                }
                Console.WriteLine("");
            }
        }

        public static void ShowClustered(double[][] DataSet, int[] cluster, int quantityOfClusters)
        {
            for (int k = 0; k < quantityOfClusters; k++)
            {
                Console.WriteLine("===================");
                for (int i = 0; i < DataSet.Length; i++)
                {
                    int clusterID = cluster[i];
                    if (clusterID != k) continue;
                    Console.Write(i.ToString().PadLeft(3) + " ");
                    for (int j = 0; j < DataSet[i].Length; ++j)
                    {
                        if (DataSet[i][j] >= 0.0) Console.Write(" ");
                        Console.Write(DataSet[i][j].ToString("F" + 1) + " ");
                    }
                    Console.WriteLine("");
                }
                Console.WriteLine("===================");
            } 
        }
    }
}
