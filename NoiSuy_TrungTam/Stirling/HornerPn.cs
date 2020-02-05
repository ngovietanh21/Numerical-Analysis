using System;
namespace Solution
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

        public static double[] Cong2DaThuc(double[] Pn1, double[] Pn2)
        {
            int Max = Pn1.Length >= Pn2.Length ? Pn1.Length : Pn2.Length;
            int Min = Pn1.Length < Pn2.Length ? Pn1.Length : Pn2.Length;
            double[] Pn = new double[Max];
            if (Max == Pn1.Length) Pn1.CopyTo(Pn, 0);
            else Pn2.CopyTo(Pn, 0);

            if (Pn1.Length == Pn2.Length)
            {
                for(int i = 0; i<Pn1.Length; i++)
                {
                    Pn[i] = Pn1[i] + Pn2[i];
                }
            }
            else
            {
                for (int i = 0; i < Min; i++)
                {
                    if (Min == Pn2.Length)
                        Pn[Min + i - 1] += Pn2[i];
                    else
                        Pn[Min + i - 1] += Pn1[i];
                }
            }

            return Pn;
        }
	}
}
