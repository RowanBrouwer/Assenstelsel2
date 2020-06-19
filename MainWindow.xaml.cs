using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        double psize = 4;
        double xdiff = 0;
        double ydiff = 0;
        Color C;
        byte Red = 255;
        byte Green = 255;
        byte Blue = 255;
        double xdiffR;
        double ydiffR;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Click(object sender, MouseButtonEventArgs e)
        {
            if (dba == false)
            {
                mid = Mouse.GetPosition(window);                
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
                double dataxR = Math.Round((double)DataP.X, 2);
                double datayR = Math.Round((double)DataP.Y, 2);
                schermcordinaten.Content = ($"Schermcoordinaten : X = {dataxR}, Y = {datayR}");
                xdiff = DataP.X - mid.X;
                ydiff = mid.Y - DataP.Y;
                xdiffR = Math.Round((double)xdiff, 2);
                ydiffR = Math.Round((double)ydiff, 2);
                schermCverschil.Content = ($"Verschil : X = {xdiffR}, Y = {ydiffR}");
                DotPlacer();
            }
        }


        private void gridadder()
        {
            for (int i = -2400; i < 2400; i = i + 100)
            {
                Rectangle TV = new Rectangle();
                TV.Width = 2;
                TV.Height = canvas.ActualHeight;
                TV.Fill = new SolidColorBrush(Colors.DarkRed);
                TV.Margin = new Thickness(mid.X + i + 2, 0, 0, 0);
                canvas.Children.Add(TV);

                Rectangle TH = new Rectangle();
                TH.Width = canvas.ActualWidth;
                TH.Height = 2;
                TH.Fill = new SolidColorBrush(Colors.DarkRed);
                TH.Margin = new Thickness(0, mid.Y + i + 2, 0, 0);
                canvas.Children.Add(TH);

                Label HN = new Label();
                HN.Width = 70;
                HN.Height = 40;
                HN.FontSize = 20;
                HN.Content = i;
                HN.Margin = new Thickness(mid.X + i, mid.Y , 0, 0);
                canvas.Children.Add(HN);

                Label VN = new Label();
                VN.Width = 70;
                VN.Height = 40;
                VN.FontSize = 20;
                VN.Content = -i;
                VN.Margin = new Thickness(mid.X, mid.Y + i , 0, 0);
                canvas.Children.Add(VN);

                Ellipse GD1 = new Ellipse();
                GD1.Width = 10;
                GD1.Height = 10;
                GD1.Fill = new SolidColorBrush(Colors.Black);
                GD1.Margin = new Thickness(mid.X - 2.5, mid.Y + i - 2.5, 0, 0);
                canvas.Children.Add(GD1);

                Ellipse GD2 = new Ellipse();
                GD2.Width = 10;
                GD2.Height = 10;
                GD2.Fill = new SolidColorBrush(Colors.Black);
                GD2.Margin = new Thickness(mid.X + i - 2.5, mid.Y - 2.5, 0, 0);
                canvas.Children.Add(GD2);

            }
            for (int i = -1900; i < 1900; i = i + 10)
            {
                Rectangle THi = new Rectangle();
                THi.Width = canvas.ActualWidth;
                THi.Height = 0.5;
                THi.Fill = new SolidColorBrush(Colors.DarkRed);
                THi.Margin = new Thickness(0, mid.Y + i + 2, 0, 0);
                canvas.Children.Add(THi);

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
            DT.Width = psize;
            DT.Height = psize;
            DT.Fill = new SolidColorBrush(Color.FromRgb(Red, Green, Blue));
            DT.Margin = new Thickness(DataP.X - psize/2, DataP.Y - psize/2, 0, 0);
            canvas.Children.Add(DT);
        }

        private void Adder_Checked(object sender, RoutedEventArgs e)
        {
            if (dba == true)
            {
                gridadder();
                Adder.IsEnabled = false;
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            reset();
        }

        private void reset()
        {
            canvas.Children.Clear();
            dba = false;
            Adder.IsEnabled = true;
            Adder.IsChecked = false;
            mid.X = 0; mid.Y = 0;
            DataP.X = 0; DataP.Y = 0;
            schermcordinaten.Content = "";
            schermCverschil.Content = "";
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            psize = slid.Value;
        }

        private void cp_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (cp.SelectedColor.HasValue)
            {
                C = cp.SelectedColor.Value;
                Red = C.R;
                Green = C.G;
                Blue = C.B;
            }
        }
    }
}
