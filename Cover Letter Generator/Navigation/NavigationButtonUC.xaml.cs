﻿using System;
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

namespace Cover_Letter_Generator.Navigation
{
    /// <summary>
    /// Interaction logic for NavigationButtonUC.xaml
    /// </summary>
    public partial class NavigationButtonUC : UserControl
    {
        public NavigationButtonUC()
        {
            InitializeComponent();
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            Background= new SolidColorBrush(Colors.LightGray);

        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            Background = null;
        }
    }
}
