namespace Moc_bat_ky
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

		public static double[] Chiadathua(double[] TuSo, double c)
		{
			int n = TuSo.Length;
			double[] Kq = new double[n];
			for (int i = 0; i < n; i++)
			{
				if (i == 0)
				{
					Kq[i] = TuSo[i];
				}
				else
				{
					Kq[i] = Kq[i - 1] * c + TuSo[i];
				}

			}

			return Kq;
		}
	}
}
