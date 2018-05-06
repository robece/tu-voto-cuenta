using System;
using System.Collections.Generic;

namespace TuVotoCuenta.Domain
{
	public class Catalogs
	{
		#region VotingValues

		private static Dictionary<string, string> votingValues = new Dictionary<string, string>();

		public static void InitVotingValues()
		{
			votingValues.Clear();
            
			double value = 0;
			while (value <= 1000)
			{
				votingValues.Add(value.ToString(), value.ToString());
				value += 1;
			}
		}

		public static List<string> GetVotingValues()
		{
			List<string> result = new List<string>();

			foreach (KeyValuePair<string, string> kv in votingValues)
				result.Add(kv.Value);

			return result;
		}

		public static string GetVotingValueKey(string value)
		{
			string result = string.Empty;
			foreach (KeyValuePair<string, string> kv in votingValues)
				if (kv.Value == value) result = kv.Key;

			return result;
		}

		public static string GetVotingValue(string key)
		{
			return votingValues[key];
		}

		#endregion
	}
}
