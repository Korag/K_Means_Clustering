﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_Means_Clustering
{
    abstract class Clustering
    {
        public static int quantityOfClusters;
        public static int ClustersMade;
        public static int ClustersBasicQuantity;
        public static int[] ClustersInIterations;


        // How many clusters?
        public static void SetQuantity()
        {
            Console.WriteLine("How many Clusters: ");
            quantityOfClusters = Convert.ToInt32(Console.ReadLine());
        }

        // Creating top level of clusters
        public static void FirstIteration(double[][] BaseMatrix, double[][][] Matrix, int[] cluster)
        {
            for (int i = 0; i < Clustering.quantityOfClusters; i++)
            {
                Matrix[Clustering.ClustersMade] = DataSet.AddOneClusterToMatrix(BaseMatrix, cluster, Clustering.quantityOfClusters);
                Clustering.ClustersMade++;
            }
            DataSet.numerator = 0;
            Clustering.ClustersBasicQuantity = Clustering.quantityOfClusters;
        }

        // Loop for nested clusters
        public static void LoopIteration(double[][] BaseMatrix, double[][][] Matrix, int iterationsNumber, string Path)
        {
            // pomocnicze iteratory
            int p = 0;
            // ilosc ostatnio stworzonych klastrow jako ograniczenie w petli, a poczatek matrix petla z parametru ClustersMade
            int ClusterMark = 0;
            int HigherLevelQuantity = Clustering.quantityOfClusters;
            Clustering.ClustersInIterations = new int[iterationsNumber+1];
            Clustering.ClustersInIterations[0] = 0;
            for (int k = 0; k < iterationsNumber; k++)
            {
                int ClustersInIteration = 0;
                Clustering.SetQuantity();
                ClusterMark = Clustering.ClustersMade;
                Clustering.ClustersInIterations[k+1] = Clustering.quantityOfClusters;

                for (int i = 0; i < HigherLevelQuantity; i++)
                {
                    // rozwazyc usuniecie clusterHelper
                    int[] clusterHelper = Clustering.EnableClustering(Matrix[p], Clustering.quantityOfClusters);
                    for (int j = 0; j < Clustering.quantityOfClusters; j++)
                    {
                        Matrix[Clustering.ClustersMade] = DataSet.AddOneClusterToMatrix(Matrix[p], clusterHelper, Clustering.quantityOfClusters);
                        //DataSet.SaveOneClusterToFile(Matrix[Clustering.ClustersMade], clusterHelper, Clustering.quantityOfClusters, @"C:\Users\user\Documents\Visual Studio 2017\Projects\K_Means_Clustering\cluster");
                        Clustering.ClustersMade++;
                        ClustersInIteration++;
                    }
                    DataSet.SaveEachClusterToFile(Matrix[p], clusterHelper, Clustering.quantityOfClusters, Path);
                    DataSet.numerator = 0;
                    p++;
                }
                HigherLevelQuantity = ClustersInIteration;
            }

        }
       

        // Normalized dataset
        private static double[][] ConvertToNormalize(double[][] DataSet)
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
        private static int[] Initialize(int quantityOfSingleData, int quantityOfClusters)
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


        private static double[][] Allocate(int quantityOfClusters, int quantityOfColumnsData)
        {
            double[][] allocateMatrix = new double[quantityOfClusters][];
            for (int i = 0; i < quantityOfClusters; i++)
            {
                allocateMatrix[i] = new double[quantityOfColumnsData];
            }
            return allocateMatrix;
        }

        private static bool UpdateMeans(double[][] NormalizedData, int[] cluster, ref double[][] means, int quantityOfClusters)
        {
            int[] quantityOfSingleDataInSingleCluster = new int[quantityOfClusters];
            for (int i = 0; i < NormalizedData.Length; i++)
            {
                int IndexOfCluster = cluster[i];
                quantityOfSingleDataInSingleCluster[IndexOfCluster]++;
            }

            for (int i = 0; i < quantityOfClusters; i++)
            {
                if (quantityOfSingleDataInSingleCluster[i]==0)
                {
                    // one of cluster is empty
                    return false;
                }
            }

            // update the means
            for (int i = 0; i < means.Length; i++)
            {
                for (int j = 0; j < means[i].Length; j++)
                {
                    means[i][j] = 0.00;
                }
            }

            for (int i = 0; i < NormalizedData.Length; i++)
            {
                int IndexOfCluster = cluster[i];
                for (int j = 0; j < NormalizedData[i].Length; j++)
                {
                    // sum SingleData belongs to each cluster
                    means[IndexOfCluster][j] += NormalizedData[i][j];
                }
            }

            for (int i = 0; i < means.Length; i++)
            {
                for (int j = 0; j < means[i].Length; j++)
                {
                    means[i][j] /= quantityOfSingleDataInSingleCluster[i];
                }
            }
            return true;
        }


        private static bool UpdateClustering(double[][] NormalizedData, int[] cluster, double[][] means, int quantityOfClusters)
        {
            bool changed = false;
            int[] newCluster = new int[cluster.Length];

            for (int i = 0; i < cluster.Length; i++)
            {
                newCluster[i] = cluster[i];
            }

            // distance from vectorOfRow to mean 
            double[] distances = new double[quantityOfClusters];

            for (int i = 0; i < NormalizedData.Length; i++)
            {
                for (int j = 0; j < quantityOfClusters; j++)
                {
                    distances[j] = Distance(NormalizedData[i], means[j]);
                }

                int newClusterID = IndexOfMinValue(distances);
                // update assigns clusters
                if (newClusterID != newCluster[i])
                {
                    changed = true;
                    newCluster[i] = newClusterID;
                }
            }

            if (changed == false)
            {
                return false;
            }

            // check new assign
            int[] quantityOfSingleDataInSingleCluster = new int[quantityOfClusters];

            for (int i = 0; i < NormalizedData.Length; i++)
            {
                int IndexOfCluster = newCluster[i];
                quantityOfSingleDataInSingleCluster[IndexOfCluster]++;
            }

            for (int i = 0; i < quantityOfClusters; i++)
            {
                if (quantityOfSingleDataInSingleCluster[i] == 0)
                {
                    // cluster is empty
                    return false;
                }
            }

            // update original cluster[]
            for (int i = 0; i < newCluster.Length; i++)
            {
                cluster[i] = newCluster[i];
            }

            // at least one change in assing SingleData to clusters and NO empty clusters
            return true;
        }

        private static double Distance(double[] vectorOfSingleRow, double[] mean)
        {
            double sumSquaredDifferencials = 0.00;
            for (int i = 0; i < vectorOfSingleRow.Length; i++)
            {
                sumSquaredDifferencials += Math.Pow((vectorOfSingleRow[i] - mean[i]), 2);
            }
            return Math.Sqrt(sumSquaredDifferencials);
        }

        private static int IndexOfMinValue(double[] distances)
        {
            // index of the smallest value in array
            int IndexOfMin = 0;
            double smallDistance = distances[0];
            for (int i = 0; i < distances.Length; i++)
            {
                if (distances[i] < smallDistance)
                {
                    smallDistance = distances[i];
                    IndexOfMin = i;
                }
            }
            return IndexOfMin;
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
                succes_mean_compute = UpdateMeans(NormalizedData, cluster, ref means, quantityOfClusters);
                change_in_claster_occur = UpdateClustering(NormalizedData, cluster, means, quantityOfClusters);

            }

          
            return cluster;
        }
    }
}
