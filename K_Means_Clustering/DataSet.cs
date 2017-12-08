using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
                        DataSet[i][j] = Double.Parse(s2[j]);
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
                    Console.Write(DataSet[i][j]);
                }
                Console.WriteLine("");
            }
        }
    }
}
