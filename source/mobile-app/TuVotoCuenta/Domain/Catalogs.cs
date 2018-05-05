using System;
using System.Collections.Generic;

namespace TuVotoCuenta.Domain
{
    public class Catalogs
    {
        #region PetKinds

        private static Dictionary<string, string> petKinds = new Dictionary<string, string>();

        public static void InitPetKinds()
        {
            petKinds.Clear();
            petKinds.Add("dog", "Perro");
            petKinds.Add("cat", "Gato");
        }

        public static List<string> GetPetKinds()
        {
            List<string> result = new List<string>();

            foreach (KeyValuePair<string, string> kv in petKinds)
                result.Add(kv.Value);

            return result;
        }

        public static string GetPetKindKey(string value)
        {
            string result = string.Empty;
            foreach (KeyValuePair<string, string> kv in petKinds)
                if (kv.Value == value) result = kv.Key;

            return result;
        }

        public static string GetPetKindValue(string key)
        {
            return petKinds[key];
        }

        #endregion

        #region PetSizes

        private static Dictionary<string, string> petSizes = new Dictionary<string, string>();

        public static void InitPetSizes()
        {
            petSizes.Clear();
            petSizes.Add("small", "Pequeño");
            petSizes.Add("medium", "Mediano");
            petSizes.Add("big", "Grande");
        }

        public static List<string> GetPetSizes()
        {
            List<string> result = new List<string>();

            foreach (KeyValuePair<string, string> kv in petSizes)
                result.Add(kv.Value);

            return result;
        }

        public static string GetPetSizeKey(string value)
        {
            string result = string.Empty;
            foreach (KeyValuePair<string, string> kv in petSizes)
                if (kv.Value == value) result = kv.Key;

            return result;
        }

        public static string GetPetSizeValue(string key)
        {
            return petSizes[key];
        }

        #endregion

        #region PetActivities

        private static Dictionary<string, string> petActivities = new Dictionary<string, string>();

        public static void InitPetActivities()
        {
            petActivities.Clear();
            petActivities.Add("active", "Activo");
            petActivities.Add("sedentary", "Sedentario");
        }

        public static List<string> GetPetActivities()
        {
            List<string> result = new List<string>();

            foreach (KeyValuePair<string, string> kv in petActivities)
                result.Add(kv.Value);

            return result;
        }

        public static string GetPetActivityKey(string value)
        {
            string result = string.Empty;
            foreach (KeyValuePair<string, string> kv in petActivities)
                if (kv.Value == value) result = kv.Key;

            return result;
        }

        public static string GetPetActivityValue(string key)
        {
            return petActivities[key];
        }

        #endregion

        #region PetWeight

        private static Dictionary<string, string> petWeights = new Dictionary<string, string>();

        public static void InitPetWeights()
        {
            petWeights.Clear();

            double value = 1;
            while (value <= 100)
            {
                value += .5;
                petWeights.Add(value.ToString(), value.ToString());
            }
        }

        public static List<string> GetPetWeights()
        {
            List<string> result = new List<string>();

            foreach (KeyValuePair<string, string> kv in petWeights)
                result.Add(kv.Value);

            return result;
        }

        public static string GetPetWeightKey(string value)
        {
            string result = string.Empty;
            foreach (KeyValuePair<string, string> kv in petWeights)
                if (kv.Value == value) result = kv.Key;

            return result;
        }

        public static string GetPetWeightValue(string key)
        {
            return petWeights[key];
        }

        #endregion

        #region PuppyMonths

        private static Dictionary<string, string> puppyMonths = new Dictionary<string, string>();

        public static void InitPuppyMonths()
        {
            puppyMonths.Clear();
            for (int i = 0; i <= 12; i++)
            {
                if (i == 1)
                    continue;

                puppyMonths.Add(i.ToString(), i.ToString());
            } 
        }

        public static List<string> GetPuppyMonths()
        {
            List<string> result = new List<string>();

            foreach (KeyValuePair<string, string> kv in puppyMonths)
                result.Add(kv.Value);

            return result;
        }

        public static string GetPuppyMonthsKey(string value)
        {
            string result = string.Empty;
            foreach (KeyValuePair<string, string> kv in puppyMonths)
                if (kv.Value == value) result = kv.Key;

            return result;
        }

        public static string GetPuppyMonthsValue(string key)
        {
            return puppyMonths[key];
        }

        #endregion
    }
}
