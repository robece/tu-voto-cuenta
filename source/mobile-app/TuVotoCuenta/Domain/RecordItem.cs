using System;
using Newtonsoft.Json;

namespace TuVotoCuenta.Domain
{
    public class RecordItem
    {
        [JsonProperty("uID")]
		public Guid UID { get; set; }
        [JsonProperty("deviceHash")]
		public string DeviceHash { get; set; }
        [JsonProperty("boxNumber")]
		public string BoxNumber { get; set; }
        [JsonProperty("boxSection")]
		public string BoxSection { get; set; }
        [JsonProperty("imageLatitude")]
		public double ImageLatitude { get; set; }
        [JsonProperty("imageLongitude")]
		public double ImageLongitude { get; set; }
        [JsonProperty("locationDetails")]
		public string LocationDetails { get; set; }
        [JsonProperty("entity")]
		public string Entity { get; set; }
        [JsonProperty("municipality")]
		public string Municipality { get; set; }
        [JsonProperty("locality")]
		public string Locality { get; set; }
        [JsonProperty("partyPAN")]
		public int PartyPAN { get; set; }
        [JsonProperty("partyPRI")]
		public int PartyPRI { get; set; }
        [JsonProperty("partyPRD")]
		public int PartyPRD { get; set; }
        [JsonProperty("partyVerde")]
		public int PartyVerde { get; set; }
        [JsonProperty("partyPT")]
		public int PartyPT { get; set; }
        [JsonProperty("partyMC")]
		public int PartyMC { get; set; }
        [JsonProperty("partyNA")]
		public int PartyNA { get; set; }
        [JsonProperty("partyMOR")]
		public int PartyMOR { get; set; }
        [JsonProperty("partyES")]
		public int PartyES { get; set; }
        [JsonProperty("partyINDJai")]
		public int PartyINDJai { get; set; }
        [JsonProperty("partyOtro")]
		public int PartyOtro { get; set; }
        [JsonProperty("partyPANMC")]
		public int PartyPANMC { get; set; }
        [JsonProperty("partyPRDPANMC")]
		public int PartyPRDPANMC { get; set; }
        [JsonProperty("partyPRDMC")]
		public int PartyPRDMC { get; set; }
        [JsonProperty("partyMORPT")]
		public int PartyMORPT { get; set; }
        [JsonProperty("partyMORES")]
		public int PartyMORES { get; set; }
        [JsonProperty("partyPTESMOR")]
		public int PartyPTESMOR { get; set; }
        [JsonProperty("partyPRIVERNA")]
		public int PartyPRIVERNA { get; set; }
        [JsonProperty("partyPRIVER")]
		public int PartyPRIVER { get; set; }
        [JsonProperty("partyPRINA")]
		public int PartyPRINA { get; set; }
        [JsonProperty("partyVERNA")]
		public int PartyVERNA { get; set; }
        [JsonProperty("partyPTES")]
		public int PartyPTES { get; set; }
        [JsonProperty("partyPRDPAN")]
		public int PartyPRDPAN { get; set; }
        [JsonProperty("imageBytes")]
		public string ImageBytes { get; set; }
        [JsonProperty("image")]
		public string Image { get; set; }
        [JsonProperty("hash")]
		public string RecordHash { get; set; }
        [JsonProperty("blockchainTransaction")]
		public string BlockchainTransaction { get; set; }
        [JsonProperty("createdDate")]
		public string CreatedDate { get; set; }
        [JsonProperty("username")]
		public string UserName { get; set; }
        [JsonProperty("transactionId")]
		public string TransactionId { get; set; }
    }
}