using System;
using System.Collections;
using System.Collections.Generic;

namespace Helpers
{
	public interface IPayment {
		int GetId();
	}
	public class Config {
		public static Dictionary<int, IPayment> payments = new Dictionary<int, IPayment>();
		public static Dictionary<string, string> configuration = new Dictionary<string, string>();
	}
}

