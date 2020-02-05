using System.IO;

namespace Moc_cach_deu
{
    class ExportData
    {
        public static void ExportData2D(double[,] Data)
        {

            using (StreamWriter Output = new StreamWriter(@"/Users/VietAnh/Projects/NoiSuy_Newton/Cach_deu/File_Output/Saiphan.csv"))
            {
                for (int i = 0; i < Data.GetLength(0); i++)
                {
                    string content = "";
                    for (int j = i; j < Data.GetLength(1); j++)
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

        public static void ExportData1D(double[] Data)
        {
            using (StreamWriter Output = new StreamWriter(@"/Users/VietAnh/Projects/NoiSuy_Newton/Cach_deu/File_Output/Dathuc.csv"))
            {
                string content = "";
                int n = Data.Length;

                for (int i = 0; i < Data.Length; i++)
                {
                    if (i == Data.Length - 1)
                    {
                        content += Data[i].ToString();
                    }
                    else content += Data[i].ToString() + "\r\n";
                }
                Output.WriteLine(content);
            }
        }
    }
}
