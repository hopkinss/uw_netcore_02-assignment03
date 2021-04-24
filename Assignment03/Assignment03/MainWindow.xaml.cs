using Assignment03.ViewModels;
using System;
using System.Windows;

namespace Assignment03
{

    public partial class MainWindow : Window
    {
        private ViewModel _viewModel;

        public MainWindow(ViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = viewModel;
            Loaded += MainWindow_Loaded;
            this.Title = "Assignment 03: Shawn Hopkins";
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.Load();
        }
        
    }
}
