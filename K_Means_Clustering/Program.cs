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


            #region Inicjalizacja

            // W srodku katalogu powinien znajdowac sie plik z danymi (np. iris.txt)
            // Do tego katalogu są zapisywane dodatkowe elementy jak np. wykres, czy pliki tekstowe z klastrami
            string Path = @"..\..\..\";

            double[][][] Matrix = new double[100][][];
            // Wczytanie danych z pliku

            Console.WriteLine("Enter filename of dataset: (without extension: .txt)");
            string ShortName = Console.ReadLine();
            string FileName = ShortName + ".txt";
            Console.WriteLine();

            // Zapytanie czy ostatnim parametrem w naszym zbiorze danych jest parametr typu string
            Console.WriteLine("Is the last column in your dataset a string parameter? y/n");
            string stringParam = Console.ReadLine();
            int LastColumnInterpreter = 0;
            if (stringParam == "y")
            {
                LastColumnInterpreter = 1;
            }

            string[] stringLastParam = null;
            double[][] FileNameMatrix = DataSet.GetDataSet(Path + FileName, LastColumnInterpreter);
            if (stringParam == "y")
            {
                stringLastParam = DataSet.GetStringVector(Path + FileName);
            }
            //DataSet.ShowDataSet(FileNameMatrix);

            #endregion

            #region Clustering 1 iteracja

            // Ustawienie liczebnosci klastrow dla 0 iteracji
            Console.WriteLine();
            Clustering.SetQuantity();
            Console.WriteLine();
 

            sw.Start();

            // Glowna funkcja
            int[] cluster = Clustering.EnableClustering(FileNameMatrix, Clustering.quantityOfClusters);
            Clustering.FirstIteration(FileNameMatrix, Matrix, cluster);

            sw.Stop();

            // Wyrzucenie na konsole wektora [] cluster oraz jego zapis do pliku

            //DataSet.ShowVector(cluster);
            //DataSet.SaveVectorToFile(cluster, Path + ShortName + "_vector.txt");


            // Wyrzucenie na konsole podzialu na klastry po 0 iteracji oraz zapis do plikow

            //Console.WriteLine("Data in cluster:");
            //DataSet.ShowClustered(FileNameMatrix, cluster, Clustering.quantityOfClusters);
            //DataSet.SaveClusteredToFile(FileNameMatrix, cluster, Clustering.quantityOfClusters, Path + ShortName + "_clustered.txt");

            DataSet.SaveEachClusterToFile_Basic(FileNameMatrix, stringLastParam, cluster, Clustering.quantityOfClusters, Path + ShortName);

            #endregion


            #region Clustering kolejne iteracje

            Console.WriteLine("Enter loop execution count (cluster generation): ");
            int iterationsNumber = Convert.ToInt32(Console.ReadLine());

            sw2.Start();
            
            // Podzial wczesniej stworzonych klastrow na kolejne 
            Clustering.LoopIteration(FileNameMatrix, stringLastParam, Matrix, iterationsNumber, Path + ShortName);

            sw2.Stop();

            #endregion


            #region Rysowanie wykresu dla Clusteringu

            // Rysowanie wykresu z wyborem atrybutow
            Console.WriteLine();
            Console.WriteLine("Generate Graph");
            Console.WriteLine("Choose 2 integers(dimensions) from 0 to " + (FileNameMatrix[0].Length-1));
            Console.Write("Dimension 1: ");
            int dimension1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Dimension 2: ");
            int dimension2 = Convert.ToInt32(Console.ReadLine());

            // Funkcja rysowania wykresu ---> dostepne rowniez 2 inne funkcje: DrawClusterGraph i DrawClusterGraph3
            Drawing.DrawClusterGraph2(FileNameMatrix, Matrix, Path + ShortName, 3000, 3000, dimension1 , dimension2, iterationsNumber);

            Console.WriteLine("End of the process ");
            Console.WriteLine("Zero iteration at time: " + sw.Elapsed);
            Console.WriteLine("Others iterations at time: " + sw2.Elapsed);

            #endregion


            #region KNN bez wykorzystania podzialu na klastry

            KNN.SearchKNN(FileNameMatrix,2);

            #endregion


            Console.ReadLine();
        }
    }
}



