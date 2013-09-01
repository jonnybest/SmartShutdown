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
		private string _procname = "reflect";

		public NoMacriumBackupRunningRule()
		{
			Debug.WriteLine("I exist!", this.ToString());
		}

		#region IShutdownRule Members

		public bool Check()
		{
			Debug.WriteLine("Checking", this.ToString());

			var procs = Process.GetProcessesByName(_procname);
			_lastresult = procs.Count() == 0;

			Debug.WriteLineIf(_lastresult, "macrium is not running", this.ToString());
			Debug.WriteLineIf(!_lastresult, "macrium is running", this.ToString());
			return _lastresult;
		}

		/// <summary>
		/// Returns the cached result of the last query
		/// </summary>
		public bool IsOkayToShutdown
		{
			get
			{
				Debug.WriteLine("last result was " + _lastresult, this.ToString());
				return _lastresult;
			}
		}

		public bool CanWaitForOkay
		{
			get {
				Debug.WriteLine("yeah, I'll wait", this.ToString());
				return true; }
		}

		/// <summary>
		/// Returns when there exist no more of these processes
		/// </summary>
		public void Wait()
		{
			var procs = Process.GetProcessesByName(_procname);
			while (procs.Count() > 0)
			{
				Debug.WriteLine("Waiting for " + procs.First().Id + " to exit", this.ToString());
				procs.First().WaitForExit();

				System.Threading.Thread.Sleep(15000); // sleep 15 seconds to allow new processes to spawn
				procs = Process.GetProcessesByName(_procname);

				Debug.WriteLineIf(procs.Count() > 0, "there's another", this.ToString());
				Debug.WriteLineIf(procs.Count() == 0, "this should have been the last one", this.ToString());
			}
			Debug.WriteLine("Waiting done. All clear.", this.ToString());
		}

		#endregion
	}
}
