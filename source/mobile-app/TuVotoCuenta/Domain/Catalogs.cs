using System;
using System.Collections.Generic;

namespace TuVotoCuenta.Domain
{
	public class Catalogs
	{
		#region Entities

		static Dictionary<string, string> entities = new Dictionary<string, string>();

		public static void InitEntities()
		{
			entities.Clear();
			//TODO: Implement logic to retrieve real data.
			entities.Add("CDMX", "Ciudad de MX");
			entities.Add("CAM", "Campeche");
		}

		public static List<string> GetEntities()
		{
			List<string> result = new List<string>();

			foreach (KeyValuePair<string, string> kv in entities)
				result.Add(kv.Value);

			return result;
		}

		public static string GetEntityKey(string value)
		{
			string result = string.Empty;
			foreach (KeyValuePair<string, string> kv in entities)
				if (kv.Value == value) result = kv.Key;

			return result;
		}

		public static string GetEntityValue(string key)
		{
			return entities[key];
		}

		#endregion

		#region Municipalities

		static Dictionary<string, string> municipalities = new Dictionary<string, string>();

		public static void InitMunicipalities(string entityKey)
		{
			municipalities.Clear();
			//TODO: Implement logic to retrieve real data.

			if (entityKey == "CDMX")
			{
				municipalities.Add("CDMX001", "MUNICIPIO CDMX 001");
				municipalities.Add("CDMX002", "MUNICIPIO CDMX 002");
			}
			else
			{
				municipalities.Add("CAM001", "CAMPECHE 001");
				municipalities.Add("CAM002", "CAMPECHE 002");
			}
		}

		public static List<string> GetMunicipalities()
		{
			List<string> result = new List<string>();

			foreach (KeyValuePair<string, string> kv in municipalities)
				result.Add(kv.Value);

			return result;
		}

		public static string GetMunicipalityKey(string value)
		{
			string result = string.Empty;
			foreach (KeyValuePair<string, string> kv in municipalities)
				if (kv.Value == value) result = kv.Key;

			return result;
		}

		public static string GetMunicipalityValue(string key)
		{
			return (municipalities.ContainsKey(key)) ? municipalities[key] : string.Empty;
		}

		#endregion

		#region Localities

		static Dictionary<string, string> localities = new Dictionary<string, string>();

		public static void InitLocalities(string municipalityKey)
		{
			localities.Clear();
			//TODO: Implement logic to retrieve real data.

            if (municipalityKey.StartsWith("CDMX", StringComparison.InvariantCultureIgnoreCase))
			{
				localities.Add("CDMX001", "LOCALIDAD CDMX 001");
				localities.Add("CDMX002", "LOCALIDAD CDMX 002");
			}
			else
			{
				localities.Add("CAM001", "LOCALIDAD CAMPECHE 001");
				localities.Add("CAM002", "LOCALIDAD CAMPECHE 002");
			}
		}

		public static List<string> GetLocalities()
		{
			List<string> result = new List<string>();

			foreach (KeyValuePair<string, string> kv in localities)
				result.Add(kv.Value);

			return result;
		}

		public static string GetLocalityKey(string value)
		{
			string result = string.Empty;
			foreach (KeyValuePair<string, string> kv in localities)
				if (kv.Value == value) result = kv.Key;

			return result;
		}

		public static string GetLocalityValue(string key)
		{
			return (localities.ContainsKey(key)) ? localities[key] : string.Empty;
		}

		#endregion
	}
}
