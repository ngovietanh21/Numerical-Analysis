using System;
using System.IO;

namespace DataProcess
{
    class DataProcess
    {
        public static double[] GetData1D(string fileData)
        {
            string[] Input = fileData.Split(',');
            double[] Data = new double[Input.Length];

            for (int i = 0; i < Input.Length; i++)
            {
                Data[i] = Convert.ToDouble(Input[i]);
            }

            return Data;
        }

        public static double[,] GetData2D(string fileData)
        {
            //file data CSV
            fileData = fileData.Replace('\n', '\r');

            string[] lines = fileData.Split(new char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);

            int Rows = lines.Length;
            int Cols = lines[0].Split(',').Length;
            double[,] Data = new double[Rows, Cols];

            for (int i = 0; i < Rows; i++)
            {
                string[] line_r = lines[i].Split(',');

                for (int j = 0; j < Cols; j++)
                {
                    Data[i, j] = Convert.ToDouble(line_r[j]);
                }
            }
            return Data;
        }

        public static void ExportData1D(double[] Data)
        {
            using (StreamWriter Output = new StreamWriter(@"/Users/VietAnh/Projects/Lagrange/Lagrange/Data/Output.csv"))
            {
                string content = "";
                int n = Data.Length;

                for (int i = 0; i < Data.Length; i++)
                {
                    if (i == Data.Length - 1)
                    {
                        content += Data[i].ToString();
                    }
                    else content += Data[i].ToString() + ",";
                }
                Output.WriteLine(content);
            }
        }

        public static void ExportData2D(double[,] Data)
        {

            using (StreamWriter Output = new StreamWriter(@"/Users/VietAnh/Projects/Lagrange/Lagrange/Data/Output.csv"))
            {
                for (int i = 0; i < Data.GetLength(0); i++)
                {
                    string content = "";
                    for (int j = 0; j < Data.GetLength(1); j++)
                    {
                        if (j != Data.GetLength(1) - 1)
                        {
                            content += Data[i, j].ToString() + ",";
                        }
                        else content += Data[i, j].ToString();
                    }
                    Output.WriteLine(content);
                }
            }
        }

        static void Main(string[] args)
        {
            //string fileData = File.ReadAllText(@"/Users/VietAnh/Projects/Lagrange/Lagrange/Data/Input.csv");
        }
    }
}
