using System;
using System.Collections.Generic;
using System.Linq;

namespace TuVotoCuenta.Domain
{
	public class Catalogs
	{
		#region Entities

        public static List<Entity> Entities = new List<Entity>();
        public static List<Municipality> Municipalities = new List<Municipality>();
        public static List<Locality> Localities = new List<Locality>();

		public static void InitEntities()
		{
            var file = Helpers.LocalFilesHelper.ReadFileInPackage("entities.txt");
            Entities = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<byte, Entity>>(file).Values.ToList();

		}

        public static void InitMunicipalities(byte entity)
        {
            var file = Helpers.LocalFilesHelper.ReadFileInPackage($"municipalities.{entity}.txt");
            Municipalities = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Municipality>>(file);
        }
		
        public static void InitLocalities(int entity,int municipality)
        {
            var file = Helpers.LocalFilesHelper.ReadCompressedFileInPackage($"localities.{entity}.zip");
            Localities = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Locality>>(file).Where(lo=> lo.MunicipalityId == municipality).ToList();
        }

        public static Entity GetEntityValue(string entityName)
        {
            return Entities.Where(en => en.EntityName == entityName).First();
        }

        public static Municipality GetMunicipalityValue(string municipalityName)
        {
            return Municipalities.Where(mu => mu.MunicipalityName == municipalityName).First();
        }

        public static Locality GetLocalityValue(string localityName)
        {
            return Localities.Where(lo => lo.LocalityName == localityName).First();
        }


		#endregion
	}
}
