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
            // Sciezka do glownego katalogu programu  --> wazne zeby na koncu zostal '\'
            // W srodku katalogu powinien znajdowac sie plik z danym (iris.txt)
            // Do tego katalogu są zapisywane dodatkowe elementy jak np. wykres, czy pliki tekstowe z klastrami
            string Path = @"C:\Users\Łukasz\Documents\Visual Studio 2017\Projects\K_Means_Clustering\";

            double[][][] Matrix = new double[100][][];
            // Wczytanie danych z pliku
            double[][] Iris = DataSet.GetDataSet(Path + "iris.txt");
            DataSet.ShowDataSet(Iris);

            // Ustawienie liczebnosci klastrow dla 1 iteracji
            Console.WriteLine();
            Clustering.SetQuantity();
            Console.WriteLine();
 

            sw.Start();

            // Glowna funkcja
            int[] cluster = Clustering.EnableClustering(Iris, Clustering.quantityOfClusters);
            Clustering.FirstIteration(Iris, Matrix, cluster);

            sw.Stop();

            // Wyrzucenie na konsole wektora [] cluster oraz jego zapis do pliku
            DataSet.ShowVector(cluster);
            DataSet.SaveVectorToFile(cluster, Path + "iris_vector.txt");
           

            Console.WriteLine();
            Console.WriteLine();

            // Wyrzucenie na konsole podzialu na klastry po 1 iteracji oraz zapis do plikow
            Console.WriteLine("Data in cluster:");
            DataSet.ShowClustered(Iris, cluster, Clustering.quantityOfClusters);
            DataSet.SaveClusteredToFile(Iris, cluster, Clustering.quantityOfClusters, Path + "iris_clustered.txt");
            DataSet.SaveEachClusterToFile(Iris, cluster, Clustering.quantityOfClusters, Path + "cluster");

         
            Console.WriteLine("How many more iterations: ");
            int iterationsNumber = Convert.ToInt32(Console.ReadLine());

            sw2.Start();
            
            // Podzial wczesniej stworzonych klastrow na kolejne 
            Clustering.LoopIteration(Iris, Matrix, iterationsNumber, Path + "cluster");

            sw2.Stop();

            // Rysowanie wykresu z wyborem atrybutow
            Console.WriteLine("Generate Graph from which dimensions: ");
            Console.WriteLine("Choose 2 integers from 0 to " + (Iris[0].Length-1));
            int dimension1 = Convert.ToInt32(Console.ReadLine());
            int dimension2 = Convert.ToInt32(Console.ReadLine());

            // Funkcja rysowania wykresu ---> dostepne rowniez 2 inne funkcje: DrawClusterGraph i DrawClusterGraph3
            Drawing.DrawClusterGraph2(Iris, Matrix, Path + "graph", 3000, 3000, dimension1 , dimension2, iterationsNumber);

            Console.WriteLine("End of the process ");
            Console.WriteLine("First iteration at time: " + sw.Elapsed);
            Console.WriteLine("Others iterations at time: " + sw2.Elapsed);
            Console.ReadLine();
        }
    }
}



