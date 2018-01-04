﻿using System;
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
        private static int t = 0;
        public static int numerator = 0;

        // Pobranie danych z pliku do tablicy
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

        // Prezentacja 2 wymiarowej tablicy
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

        // Prezentacja podzialu na klastry dla danego zbioru danych
        public static void ShowClustered(double[][] DataSet, int[] cluster, int quantityOfClusters)
        {
            for (int k = 0; k < quantityOfClusters; k++)
            {
                Console.WriteLine("-----------" + k + "-----------");
                Console.WriteLine("========================");
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
                Console.WriteLine("========================");
            }
        }

        // Zapis do pliku podzialu na klastry
        public static void SaveClusteredToFile(double[][] DataSet, int[] cluster, int quantityOfClusters, string Path)
        {
            using (StreamWriter sw = new StreamWriter(Path))
            {
                for (int k = 0; k < quantityOfClusters; k++)
                {
                    sw.WriteLine("-----------" + k + "-----------");
                    sw.WriteLine("========================");
                    for (int i = 0; i < DataSet.Length; i++)
                    {
                        int clusterID = cluster[i];
                        if (clusterID != k) continue;
                        sw.Write(i.ToString().PadLeft(3) + " ");
                        for (int j = 0; j < DataSet[i].Length; j++)
                        {
                            if (DataSet[i][j] >= 0.0) sw.Write(" ");
                            sw.Write(DataSet[i][j].ToString("F" + 1) + " ");
                        }
                        sw.WriteLine("");
                    }
                    sw.WriteLine("========================");
                }
            }
        }

        // Zapis kazdego klastra do oddzielnego pliku
        public static void SaveEachClusterToFile(double[][] DataSet, int[] cluster, int quantityOfClusters, string Path)
        {
            for (int k = 0; k < quantityOfClusters; k++)
            {
                using (StreamWriter sw = new StreamWriter(Path + $"{t}.txt"))
                {

                    for (int i = 0; i < DataSet.Length; i++)
                    {
                        int clusterID = cluster[i];
                        if (clusterID != k) continue;


                        for (int j = 0; j < DataSet[i].Length; j++)
                        {
                            if (DataSet[i][j] == 0)
                            {
                                break;
                            }
                            sw.Write(DataSet[i][j].ToString("F" + 1) + " ");
                        }
                        sw.WriteLine("");
                    }
                }
                t++;
            }
        }

        // Dodanie pojedynczego klastra do tablicy 
        public static double[][] AddOneClusterToMatrix(double[][] DataSet, int[] cluster, int quantityOfClusters)
        {
            double[][] Matrix = new double[DataSet.Length][];

            for (int i = 0; i < DataSet.Length; i++)
            {
                Matrix[i] = new double[DataSet[i].Length];

                int clusterID = cluster[i];
                if (clusterID != numerator) continue;

                for (int j = 0; j < DataSet[i].Length; j++)
                {

                    Matrix[i][j] = DataSet[i][j];
                }
            }

            numerator++;
            return Matrix;
        }

        // Prezentacja wektora
        public static void ShowVector(int[] Vector)
        {
            for (int i = 0; i < Vector.Length; i++)
            {
                Console.Write(Vector[i] + " ");
            }
        }

        // Zapis wektora do pliku
        public static void SaveVectorToFile(int[] Vector, string Path)
        {
            using (StreamWriter sw = new StreamWriter(Path))
            {
                for (int i = 0; i < Vector.Length; i++)
                {
                    sw.WriteLine("index: " + i + "\t cluster: " + Vector[i] + " ");
                }
            }
        }
    }
}
