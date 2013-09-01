using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SmartShutdown
{
	class NoMacriumBackupRunningRule : IShutdownRule
	{
		private bool _lastresult = false;
		private string _procname = "Reflect.exe";

		#region IShutdownRule Members

		public bool Check()
		{
			var procs = Process.GetProcessesByName(_procname);
			_lastresult = procs.Count() > 0;
			return _lastresult;
		}

		/// <summary>
		/// Returns the cached result of the last query
		/// </summary>
		public bool IsOkayToShutdown
		{
			get
			{
				return _lastresult;
			}
		}

		public bool CanWaitForOkay
		{
			get { return true; }
		}

		/// <summary>
		/// Returns when there exist no more of these processes
		/// </summary>
		public void Wait()
		{
			var procs = Process.GetProcessesByName(_procname);
			while (procs.Count() > 0)
			{
				procs.First().WaitForExit();
				System.Threading.Thread.Sleep(15000); // sleep 15 seconds to allow new processes to spawn
				procs = Process.GetProcessesByName(_procname);
			}
		}

		#endregion
	}
}
