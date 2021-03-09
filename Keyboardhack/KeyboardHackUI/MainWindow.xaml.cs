using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace KeyboardHackUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private KeyEventHandler _handler;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_OnClick(object sender, RoutedEventArgs e)
        {
            _handler = new KeyEventHandler(Form1_KeyDown);
            
            Keyboard.AddKeyDownHandler(Main, _handler);

            StopButton.IsEnabled = true;
            StartButton.IsEnabled = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void StopButton_OnClick(object sender, RoutedEventArgs e)
        {
            Keyboard.RemoveKeyDownHandler(Main, _handler);


            StopButton.IsEnabled = false;
            StartButton.IsEnabled = true;
            Activate.IsEnabled = true;
        }

        private void Activate_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
