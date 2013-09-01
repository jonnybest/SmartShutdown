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

		public void Wait()
		{
			var procs = Process.GetProcessesByName(_procname);
			if (procs.Count() > 0)
			{
				procs.Select(p => p.WaitForExit());
			}
		}

		#endregion
	}
}
