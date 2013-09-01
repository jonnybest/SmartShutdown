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
using System.Timers;
using System.Diagnostics;

namespace SmartShutdown
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		ShutdownSafetyProtocol currentProtocol;
		TimeSpan initialGracePeriodBetweenSteps;
		TimeSpan userIdleTime = TimeSpan.FromMinutes(30);
		Timer myTimer = new Timer();

		public MainWindow()
		{
			InitializeComponent();
			// set up protocol
			initialGracePeriodBetweenSteps = TimeSpan.FromSeconds(30);
			TimeSpan desync = TimeSpan.FromSeconds(1); // the shutdownsafety and the timer start out in sync. this is a second to desync them
			currentProtocol = new ShutdownSafetyProtocol(initialGracePeriodBetweenSteps - desync);
			currentProtocol.AddRule(new NoMacriumBackupRunningRule());
			currentProtocol.AddRule(new UserIdleRule(userIdleTime));			
		}

		void myTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			Debug.WriteLine("timer is done", this.ToString());
			var oldstate = currentProtocol.CurrentState;
			var newstate = currentProtocol.DoTransition();
			if (oldstate == newstate)
			{
				Debug.WriteLine("timer came in too soon. increasing interval", this.ToString());
				myTimer.Interval = initialGracePeriodBetweenSteps.TotalMilliseconds * 4;
			}
			else
			{
				Debug.WriteLine("timer changed state. resetting interval", this.ToString());
				myTimer.Interval = initialGracePeriodBetweenSteps.TotalMilliseconds;
			}
			myTimer.Start();
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			currentProtocol.DoTransition();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			// set up timer
			myTimer.AutoReset = false;
			myTimer.Interval = initialGracePeriodBetweenSteps.TotalMilliseconds;
			myTimer.Elapsed += new ElapsedEventHandler(myTimer_Elapsed);			
			myTimer.Start();
		}

		private void button1_Click_1(object sender, RoutedEventArgs e)
		{
			currentProtocol.StopShutdown();
			this.Close();
		}

		private void button2_Click(object sender, RoutedEventArgs e)
		{
			currentProtocol.StopShutdown();
		}

		private void button3_Click(object sender, RoutedEventArgs e)
		{
			currentProtocol.Shutdown();
		}
	}
}
