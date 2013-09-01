using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartShutdown
{
	/// <summary>
	/// Defines an oracle that that tells whether it's safe to shutdown the PC or not. 
	/// </summary>
	interface IShutdownRule
	{
		/// <summary>
		/// This IShutdownRule thinks it's safe to proceed with a shutdown.
		/// </summary>
		/// <returns>TRUE if the shutdown may proceed. FALSE if it is not safe to shutdown.</returns>
		public bool Check();

		/// <summary>
		/// This IShutdownRule thinks it's safe to proceed with a shutdown. The value may be cached.
		/// </summary>
		public bool IsOkayToShutdown { get; }

		/// <summary>
		/// This indicates whether the rule can wait until its safe to shutdown.
		/// </summary>
		public bool CanWaitForOkay { get; }

		/// <summary>
		/// Wait until the criteria for this IShudownRule are fulfilled. The implementing class must throw an exception if it can't wait or the shutdown should not proceed.
		/// </summary>
		public void Wait();
	}
}
