using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
		List<IShutdownRule> _rules = new List<IShutdownRule>();
		TimeSpan _interval = null;

		public ShutdownSafetyStates CurrentState { get; }

		public ShutdownSafetyProtocol(TimeSpan IntervalPerStep)
		{
			_interval = IntervalPerStep;
			CurrentState = ShutdownSafetyStates.NotSafe;
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
			switch (ShutdownSafetyStates)
			{
				case ShutdownSafetyStates.NotSafe:
					bool canTransition = checkRules();
					if (canTransition)
					{
						CurrentState = ShutdownSafetyStates.Checked;
					}
					break;
				case ShutdownSafetyStates.Checked:
					bool canTransition = checkRulesAgain();
					if (canTransition)
					{
						CurrentState = ShutdownSafetyStates.Safe;
					}
					else
					{
						CurrentState = ShutdownSafetyStates.NotSafe;
					}
					break;
				case ShutdownSafetyStates.Safe:
					WaitAndShutdown();
					break;
				default:
					break;
			}
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
			}
			Shutdown();
		}

		private void Shutdown()
		{
			new WPFAboutBox1().ShowDialog(); // fake implementation 
		}
	}
}
