using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SmartShutdown
{
	enum ShutdownSafetyStates
	{
		NotSafe,
		Checked,
		Safe
	}

	/// <summary>
	/// This class is a state machine that implements the shutdown safety protocol.
	/// </summary>
	class ShutdownSafetyProtocol
	{
		private List<IShutdownRule> _rules = new List<IShutdownRule>();
		private TimeSpan _interval;
		private DateTime _lastStep = DateTime.Now;
		private ShutdownSafetyStates _state = ShutdownSafetyStates.NotSafe;

		public ShutdownSafetyStates CurrentState { get { return _state; } }

		public ShutdownSafetyProtocol(TimeSpan IntervalPerStep)
		{
			_interval = IntervalPerStep;
			_state = ShutdownSafetyStates.NotSafe;
		}

		public void AddRule(IShutdownRule Rule)
		{
			if (!_rules.Contains(Rule))
			{
				_rules.Add(Rule);
			}
		}

		public ShutdownSafetyStates DoTransition()
		{
			// only attemt to transition if the last step is _interval in the past
			if (DateTime.Now - _lastStep < _interval)
			{
				// nope, too soon.
				Debug.WriteLine("Too soon", this.ToString());
				return CurrentState;
			}
			switch (_state)
			{
				case ShutdownSafetyStates.NotSafe:
					if (checkRules())
					{
						Debug.WriteLine("leaving notsafe", this.ToString());
						_state = ShutdownSafetyStates.Checked;
					}
					break;
				case ShutdownSafetyStates.Checked:
					if (checkRulesAgain())
					{
						Debug.WriteLine("leaving checked", this.ToString());
						_state = ShutdownSafetyStates.Safe;
					}
					else
					{
						Debug.WriteLine("something came up, back to unsafe", this.ToString());
						_state = ShutdownSafetyStates.NotSafe;
					}
					break;
				case ShutdownSafetyStates.Safe:
					WaitAndShutdown();
					break;
				default:
					break;
			}
			_lastStep = DateTime.Now;
			return CurrentState;
		}

		/// <summary>
		/// Checks only cached results where available
		/// </summary>
		/// <returns></returns>
		private bool checkRulesAgain()
		{
			foreach (var rule in _rules)
			{
				if (!rule.IsOkayToShutdown)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Checks all the rules
		/// </summary>
		/// <returns></returns>
		private bool checkRules()
		{
			foreach (var rule in _rules)
			{
				if (!rule.Check())
				{
					return false;
				}
			}
			return true;
		}

		private void WaitAndShutdown()
		{
			foreach (var rule in _rules)
			{
				if (rule.CanWaitForOkay)
				{
					rule.Wait();
				}
				else
				{
					if (!rule.IsOkayToShutdown)
					{
						Debug.WriteLine("something came up while checking " + rule.ToString(), this.ToString());
						_state = ShutdownSafetyStates.NotSafe;
						return;
					}
				}
			}
			Shutdown();
		}

		private void Shutdown()
		{
			Debug.WriteLine("shutting down. good night", this.ToString());
			new WPFAboutBox1(App.Current.MainWindow).ShowDialog(); // fake implementation 
		}
	}
}
