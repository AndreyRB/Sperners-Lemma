using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace SpernerLemma
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            
        }
        int n;
        bool check = false;

        public void PrintElipse(int Xpos, int Ypos)
        {
            Point point = new Point(Xpos, Ypos);
            Ellipse elipse = new Ellipse();

            elipse.Width = 4;
            elipse.Height = 4;

            elipse.StrokeThickness = 2;
            elipse.Stroke = Brushes.Blue;
            elipse.Margin = new Thickness(point.X - 2, point.Y - 2, 3, 5);

            Canvas.Children.Add(elipse);
        }

        public void PrintLine(int X1, int Y1, int X2, int Y2)
        {
            Line a = new Line();
            a.X1 = X1; a.Y1 = Y1;
            a.X2 = X2; a.Y2 = Y2;

            a.StrokeThickness = 2;
            a.HorizontalAlignment = HorizontalAlignment.Left;
            a.VerticalAlignment = VerticalAlignment.Center;
            a.Stroke = Brushes.Black;
            Canvas.Children.Add(a);
        }

        Point p = new Point(0,0);
        public void Move(Point[][] map, int Xpos, int Ypos, int move, int i = 0, int j =0)
        {
            if (i == 0 && j == 0)
            {
                map[i][j] = new Point(Xpos, Ypos);
                //PrintElipse(Xpos, Ypos);
            }

            if (j - 1 >= 0) // влево
            {
                if (map[i][j - 1] == p)
                {
                    map[i][j - 1] = new Point(Xpos - 2*move, Ypos);
                    //PrintElipse(Xpos - 2*move, Ypos);
                    Move(map, Xpos - 2*move, Ypos, move, i, j - 1);
                }
                PrintLine(Xpos, Ypos, Xpos - 2 * move, Ypos);
            }
            if(i + 1 < n + 1)
                if (j + 1 < map[i + 1].Length) //вправо вниз
                {
                    if (map[i + 1][j + 1] == p)
                    {
                        map[i + 1][j + 1] = new Point(Xpos + move, Ypos + move);
                        //PrintElipse(Xpos + move, Ypos + move);
                        Move(map, Xpos + move, Ypos + move, move, i + 1, j + 1);
                    }
                    PrintLine(Xpos, Ypos, Xpos + move, Ypos + move);
                }
            if (i + 1 < n + 1) //влево вниз
            {
                if (map[i + 1][j] == p)
                {
                    map[i + 1][j] = new Point(Xpos - 2 * move, Ypos + move);
                    //PrintElipse(Xpos - 2 * move, Ypos + move);
                    Move(map, Xpos - 2 *move, Ypos + move, move, i + 1, j);
                }
                PrintLine(Xpos, Ypos, Xpos - move, Ypos + move);
            }
            
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Amount.Text == "")
                MessageBox.Show("Введите n");
            else
            { 
                check = !check;
                if (check) Start.Content = "Остановить";
                else Start.Content = "Запуск";
                if (!check) Canvas.Children.Clear();
                if (check) 
                {
                    Triangulation tr = new Triangulation(n);
                    tr.ToFillTriangulate();
                    Point[][] map = new Point[n+1][];
                    for (int i = 0; i < n+1; i++)
                    {
                        map[i] = new Point[i+1];
                    }
                    
                    int Xpos = 400;
                    int Ypos = 200;
                    int move = 200 / n;

                    Move(map,Xpos,Ypos,move);

                    for (int i = 0; i < n+1; i++)
                    {
                        for (int j = 0; j < map[i].Length; j++)
                        {
                            PrintElipse((int)map[i][j].X, (int)map[i][j].Y);

                            Label tb = new Label();
                            tb.Content = tr[i][j].ToString();
                            tb.RenderTransform = new TranslateTransform
                            {
                                X = (int)map[i][j].X - 7,
                                Y = (int)map[i][j].Y - 25
                            };
                            Canvas.Children.Add(tb);
                        }
                    }

                    List<List<int>> result = tr.SearchShperner();
                    int k = 0;
                    for (int i = 0; i < result.Count; i++)
                    {
                        Polygon polygon = new Polygon();
                        polygon.Points.Add(map[result[k][0]][result[k][1]]);
                        polygon.Points.Add(map[result[k][2]][result[k][3]]);
                        polygon.Points.Add(map[result[k][4]][result[k][5]]);
                        polygon.Stroke = Brushes.Red;
                        polygon.StrokeThickness = 4;
                        Canvas.Children.Add(polygon);
                        k++;
                    }

                }
            }
        }
        private void Mouse_Down(object sender, MouseButtonEventArgs e)
        {
            if(check)
            {
                var point1 = e.GetPosition(this);
                var point2 = e.GetPosition(this);

            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Amount.Text != "")
                n = int.Parse(Amount.Text);
            
        }
    }
}
