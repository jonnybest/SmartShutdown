using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartShutdown
{
	class UserIdleRule : IShutdownRule
	{
		#region IShutdownRule Members

		public bool Check()
		{
			throw new NotImplementedException();
		}

		public bool IsOkayToShutdown
		{
			get { throw new NotImplementedException(); }
		}

		public bool CanWaitForOkay
		{
			get { return false; }
		}

		public void Wait()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
