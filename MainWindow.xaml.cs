﻿using System;
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
    public partial class MainWindow : Window
    {

        // values used throughout the code //
        bool dba = false;
        Point mid;
        Point DataP;
        double psize = 4;
        double pthickness = 2;
        double xdiff = 0;
        double ydiff = 0;
        Color C1;
        Color C2;
        byte dRed = 0;
        byte dGreen = 0;
        byte dBlue = 0;
        byte eRed = 0;
        byte eGreen = 0;
        byte eBlue = 0;
        double xdiffR;
        double ydiffR;

        public MainWindow()
        {
            InitializeComponent();
        }

        // When there is an click within the program it looks if dba is true or false, if false it will mark the new center point and saves the cordinates of it as mid. //
        // When dba is true it will place the Dots and log the cordinate of the last placed dot as DataP and round them to 2 decimals. //
        // The calculation for the difference between mid and DataP is for x cordinates = DataP.X - mid.X and for Y cordinates mid.Y - DataP.Y, these values are saved as xdiff and ydiff //;
        private void Click(object sender, MouseButtonEventArgs e)
        {
            if (dba == false)
            {
                mid = Mouse.GetPosition(window);                
                dba = true;

                // these are the rectangle lines that serve as the new middlepoint // 
                // they simply take the mid cordinates as middlepoint //

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
                schermCverschil.Content = ($"Wetenschappelijk : X = {xdiffR}, Y = {ydiffR}");
                DotPlacer();
            }
        }

        private void gridadder()
        {
            // here the grid will be added, this is done when the checkbox is checked // 
            // it takes the mid cordinates as center and starts stepping from - to + with steps of 100//

            for (int i = -2400; i < 2400; i = i + 100)
            {
                // These rectangles are the Thick numbered lines of the grid //
                // the margin has +2 added to it to center it // 
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

                // these are the labels for the numbers generated the same as the thick lines above //

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

                // here are the dots that mark where the actual location of the numbers above are // 
                // their margin is adjusted for the size so they line up perfectly // 

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
                // These rectangles are the Thin lines of the grid //
                // the margin has +2 added to it to center it //
                // this is done the same way as the ones above except that it starts at -1900 and ends at 1900, the steps are only 10 pixels now //

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

        // Places the dots for the clicks after the new center is decided //
        // The size is controlled by the slider further down // 
        // To center the dots the cordinates are taken and the size is devided by 2 and subtracted of the cordinates //
        // The Color of the dots is decided by the colorpicker further down //

        private void DotPlacer()
        {
            Ellipse DT = new Ellipse();
            DT.Width = psize;
            DT.Height = psize;
            DT.Stroke = new SolidColorBrush(Color.FromRgb(eRed, eGreen, eBlue));
            DT.Fill = new SolidColorBrush(Color.FromRgb(dRed, dGreen, dBlue));
            DT.Margin = new Thickness(DataP.X - psize/2, DataP.Y - psize/2, 0, 0);
            DT.StrokeThickness = pthickness;
            canvas.Children.Add(DT);
        }

        // Simple checkbox that locks itself when activated, this controls the gridadder //
        // It can only be activated when the center is decided for now // 

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

        // Made an Reset that can be used more often if needed //

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
            eRed = 0; dRed = 0;
            eGreen = 0;dGreen = 0;
            eBlue = 0;dBlue = 0;
        }

        // Slider for the dot size // 

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            psize = slid.Value;
        }

        // Slider for the dot edge thickness

        private void slid2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            pthickness = slid2.Value;
        }

        // Colorpicker sets the values of red green and blue //
        // This Colorpicker is included in the extended WPF toolkit //

        private void cp_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (cp.SelectedColor.HasValue)
            {
                C1 = cp.SelectedColor.Value;
                dRed = C1.R;
                dGreen = C1.G;
                dBlue = C1.B;
            }
        }
        private void ecp_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (ecp.SelectedColor.HasValue)
            {
                C2 = ecp.SelectedColor.Value;
                eRed = C2.R;
                eGreen = C2.G;
                eBlue = C2.B;
            }
        }
    }
}
