﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Win32_API;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SmartShutdown
{
	class UserIdleRule : IShutdownRule
	{
		long timeout;

		public UserIdleRule(TimeSpan UserIdleTime)
		{
			Debug.WriteLine("I exist!", this.ToString());
			timeout = (long) Math.Abs(UserIdleTime.TotalMilliseconds);
		}

		#region IShutdownRule Members

		public bool Check()
		{
			Debug.WriteLine("Checking", this.ToString());
			if (Win32.GetIdleTime() > timeout)
			{
				Debug.WriteLine(Win32.GetIdleTime(), "idle for");
				Debug.WriteLine("user is idle", this.ToString());
				return true;
			}
			else
			{
				Debug.WriteLine(Win32.GetIdleTime(), "idle for");
				Debug.WriteLine("user is active", this.ToString());
				return false;
			}
		}

		public bool IsOkayToShutdown
		{
			get {
				Debug.WriteLine("okay", this.ToString());
				return Check(); }
		}

		public bool CanWaitForOkay
		{
			get {
				Debug.WriteLine("I cannot wait for a shutdown", this.ToString());
				return false; }
		}

		public Task Wait()
		{
			Debug.WriteLine("dafuq?", this.ToString());
			throw new NotImplementedException();
		}

		#endregion
	}
}
