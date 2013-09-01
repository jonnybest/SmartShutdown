using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartShutdown
{
	enum ShutdownSafetyStates
	{
		NotSafe,
		Checking,
		Checked,
		Safe
	}

	/// <summary>
	/// This class is a state machine that implements the shutdown safety protocol.
	/// </summary>
	class ShutdownSafetyProtocol
	{
		List<IShutdownRule> _rules = new List<IShutdownRule>();

		public ShutdownSafetyProtocol(TimeSpan IntervalPerStep)
		{

		}

	}
}
