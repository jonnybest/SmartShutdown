using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartShutdown
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		ShutdownSafetyProtocol currentProtocol;

		public MainWindow()
		{
			InitializeComponent();
			currentProtocol = new ShutdownSafetyProtocol(TimeSpan.FromSeconds(5));
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			currentProtocol.DoTransition();
		}
	}
}
