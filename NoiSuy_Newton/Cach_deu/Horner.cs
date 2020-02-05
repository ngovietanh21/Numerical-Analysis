namespace Moc_cach_deu
{
    class HornerPn
    {
        public static double[] Nhandathuc(double[] Mocnoisuy)
        {
            int n = Mocnoisuy.Length;
            double[] multi = new double[n + 1];
            double[] temp = new double[n + 1];

            for (int k = 0; k < n; k++)
            {
                if (k == 0)
                {
                    multi[0] = temp[0] = 1;
                    multi[1] = temp[1] = -Mocnoisuy[0];
                }
                else
                {
                    for (int i = 1; i <= k + 1; i++)
                    {
                        multi[i] = temp[i] - Mocnoisuy[k] * temp[i - 1];
                    }

                    for (int i = 1; i <= k + 1; i++)
                    {
                        temp[i] = multi[i];
                    }
                }
            }

            return multi;
        }

        public static double Giatridathuc(double[] Pn, double c)
        {
            double f = Pn[0];
            int n = Pn.Length;
            for (int i = 1; i < n; i++)
            {
                f = f * c + Pn[i];
            }
            return f;
        }
    }
}
