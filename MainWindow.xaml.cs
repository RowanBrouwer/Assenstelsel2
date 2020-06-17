using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assenstelsel2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool dba = false;
        Point mid;
        Point DataP;
        int[] pcoll = { 255, 255, 255 };
        double psize = 4;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Click(object sender, MouseButtonEventArgs e)
        {
            if (dba == false)
            {
                mid = Mouse.GetPosition(window);
                double mv = mid.X;
                double mh = mid.Y;
                dba = true;

                Rectangle V1 = new Rectangle();
                V1.Width = 5;
                V1.Height = canvas.ActualHeight;
                V1.Fill = new SolidColorBrush(Colors.DarkRed);
                V1.Margin = new Thickness(mid.X, 0, 0, 0);
                canvas.Children.Add(V1);

                Rectangle H1 = new Rectangle();
                H1.Width = canvas.ActualWidth;
                H1.Height = 5;
                H1.Fill = new SolidColorBrush(Colors.DarkRed);
                H1.Margin = new Thickness(0, mid.Y, 0, 0);
                canvas.Children.Add(H1);

            }

            else if (dba == true)
            {
                DataP = Mouse.GetPosition(window);
                double xdiff = 0;
                double ydiff = 0;
                schermcordinaten.Content = ($"Schermcoordinaten : X = {DataP.X}, Y = {DataP.Y}");
                xdiff = DataP.X - mid.X;
                ydiff = mid.Y - DataP.Y;
                schermCverschil.Content = ($"Verschil : X = {xdiff}, Y = {ydiff}");

            }
        }


        private void gridadder()
        {
            for (int i = -900; i < 900; i = i + 100)
            {
                Rectangle TV = new Rectangle();
                TV.Width = 2;
                TV.Height = canvas.ActualHeight;
                TV.Fill = new SolidColorBrush(Colors.DarkRed);
                TV.Margin = new Thickness(mid.X + i + 2, 0, 0, 0);
                canvas.Children.Add(TV);
            }
            for (int i = -900; i < 900; i = i + 100)
            {
                Rectangle TH = new Rectangle();
                TH.Width = canvas.ActualWidth;
                TH.Height = 2;
                TH.Fill = new SolidColorBrush(Colors.DarkRed);
                TH.Margin = new Thickness(0, mid.Y + i + 2, 0, 0);
                canvas.Children.Add(TH);
            }
            for (int i = -1900; i < 1900; i = i + 10)
            {
                Rectangle THi = new Rectangle();
                THi.Width = canvas.ActualWidth;
                THi.Height = 0.5;
                THi.Fill = new SolidColorBrush(Colors.DarkRed);
                THi.Margin = new Thickness(0, mid.Y + i + 2, 0, 0);
                canvas.Children.Add(THi);
            }
            for (int i = -1900; i < 1900; i = i + 10)
            {
                Rectangle TVi = new Rectangle();
                TVi.Width = 0.5;
                TVi.Height = canvas.ActualHeight;
                TVi.Fill = new SolidColorBrush(Colors.DarkRed);
                TVi.Margin = new Thickness(mid.X + i + 2, 0, 0, 0);
                canvas.Children.Add(TVi);
            }
        }

        private void DotPlacer()
        {
            Ellipse DT = new Ellipse();
        }

        private void Adder_Checked(object sender, RoutedEventArgs e)
        {
            if (dba == true)
            {
                gridadder();
                Adder.IsEnabled = false;
            }
        }
    }
}
