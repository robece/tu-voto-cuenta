using System;
using Newtonsoft.Json;

namespace TuVotoCuenta.Domain
{
    public class RecordItem
    {
		public Guid UID { get; set; }
		public string DeviceHash { get; set; }
		public string BoxNumber { get; set; }
		public string BoxSection { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public string LocationDetails { get; set; }
		public string Entity { get; set; }
		public string Municipality { get; set; }
		public string Locality { get; set; }
		public int PartyPAN { get; set; }
		public int PartyPRI { get; set; }
		public int PartyPRD { get; set; }
		public int PartyVerde { get; set; }
		public int PartyPT { get; set; }
		public int PartyMC { get; set; }
		public int PartyNA { get; set; }
		public int PartyMOR { get; set; }
		public int PartyES { get; set; }
		public int PartyINDJai { get; set; }
		public int PartyOtro { get; set; }
        public int PartyPANMC { get; set; }
        public int PartyPRDPANMC { get; set; }
        public int PartyPRDMC { get; set; }
        public int PartyMORPT { get; set; }
        public int PartyMORES { get; set; }
        public int PartyPTESMOR { get; set; }
        public int PartyPRIVERNA { get; set; }
        public int PartyPRIVER { get; set; }
        public int PartyPRINA { get; set; }
        public int PartyVERNA { get; set; }
        public int PartyPTES { get; set; }
        public int PartyPRDPAN { get; set; }
		public string Image { get; set; }
        [JsonProperty("hash")]
		public string RecordHash { get; set; }
		public string BlockchainTransaction { get; set; }
		public string CreatedDate { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
    }
}