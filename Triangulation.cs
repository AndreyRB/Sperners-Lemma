using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpernerLemma
{
    class Triangulation
    {

        int[][] a = new int[5][];
        int N;

        public Triangulation(int n)
        {
            int z = 1;

            this.N = n;
            Array.Resize(ref a, N + 1);

            for (int i = 0; i < N + 1; i++)
            {
                for (int j = 0; j < z; j++)
                {
                    a[i] = new int[z];
                }
                z++;
            }
        }

        public int[] this[int i]
        {
            get { return a[i]; }            
        }


    public void ToFillTriangulate()
        {
            a[0][0] = 3;
            a[N][0] = 1;
            a[N][N] = 2;

            for (int i = 1; i < N; i++) //Сторона 13
            {
                Random random = new Random(Guid.NewGuid().GetHashCode());
                int k = random.Next(0, 100);
                if (k % 2 == 1) a[i][0] = 1;
                else a[i][0] = 3;
            }

            for (int j = 1; j < N; j++) //Сторона 12
            {
                Random random = new Random(Guid.NewGuid().GetHashCode());
                int k = random.Next(0, 100);
                if (k % 2 == 1) a[N][j] = 1;
                else a[N][j] = 2;
            }

            for (int z = 1; z < N; z++) //Сторона 23
            {
                Random random = new Random(Guid.NewGuid().GetHashCode());
                int k = random.Next(0, 100);
                if (k % 2 == 1) a[z][a[z].Length - 1] = 2;
                else a[z][a[z].Length - 1] = 3;
            }

            for (int i = 0; i < N + 1; i++)
            {
                for (int j = 0; j < a[i].Length; j++)
                {
                    if (a[i][j] == 0)
                    {
                        Random random = new Random(Guid.NewGuid().GetHashCode());
                        int k = random.Next(0, 100);
                        a[i][j] = random.Next(1, 4);
                    }
                }
            }
        }
        public List<List<int>> SearchShperner()
        {
            List<List<int>> triangles = new List<List<int>>();

            for (int i = 0; i < N+1; i++)
            {
                for (int j = 0; j < a[i].Length - 1; j++)
                {

                    if (j > 0 && i + 1 < N + 1)
                        if (Compare(a[i][j], a[i][j - 1], a[i + 1][j]))
                        {
                            List<int> g = new List<int>();
                            g.Add(i); g.Add(j);
                            g.Add(i); g.Add(j - 1);
                            g.Add(i + 1); g.Add(j);
                            triangles.Add(g);
                        }  //1)

                    if (i > 0 && j + 1 < N + 1)
                        if (Compare(a[i][j], a[i - 1][j], a[i][j + 1]))
                        {
                            List<int> g = new List<int>();
                            g.Add(i); g.Add(j);
                            g.Add(i - 1); g.Add(j);
                            g.Add(i); g.Add(j + 1);
                            triangles.Add(g);
                        }//2)

                    if (j > 0 && i > 0)
                        if (Compare(a[i][j], a[i - 1][j], a[i - 1][j - 1]))
                        {
                            List<int> g = new List<int>();
                            g.Add(i); g.Add(j);
                            g.Add(i - 1); g.Add(j - 1);
                            g.Add(i - 1); g.Add(j);
                            triangles.Add(g);
                        } //3)
                   
                }
            }
            return triangles;
        }
        bool Compare(int a, int b, int c)
        {
            if (a != 0 && b != 0 && c != 0)
            {
                if (a != b && a != c && b != c)
                    return true;
            }
            return false;
        }

        public void PrintToCosole()
        {
            for (int i = 0; i < N + 1; i++)
            {
                for (int j = 0; j < a[i].Length; j++)
                {
                    Console.Write(a[i][j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
