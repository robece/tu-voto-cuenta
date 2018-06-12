using System;
using System.Collections.Generic;
using System.Text;

namespace TuVotoCuenta.Functions.Domain.Models.Base
{
    public class RecordItem
    {
        public string deviceHash { get; set; }
        public string boxNumber { get; set; }
        public string boxSection { get; set; }
        public string locationDetails { get; set; }
        public string entity { get; set; }
        public string municipality { get; set; }
        public string locality { get; set; }
        public int partyPAN { get; set; }
        public int partyPRI { get; set; }
        public int partyPRD { get; set; }
        public int partyVerde { get; set; }
        public int partyPT { get; set; }
        public int partyMC { get; set; }
        public int partyNA { get; set; }
        public int partyMOR { get; set; }
        public int partyES { get; set; }
        public int partyINDJai { get; set; }
        public int partyOtro { get; set; }
        public int partyPANMC { get; set; }
        public int partyPRDPANMC { get; set; }
        public int partyPRDMC { get; set; }
        public int partyMORPT { get; set; }
        public int partyMORES { get; set; }
        public int partyPTESMOR { get; set; }
        public int partyPRIVERNA { get; set; }
        public int partyPRIVER { get; set; }
        public int partyPRINA { get; set; }
        public int partyVERNA { get; set; }
        public int partyPTES { get; set; }
        public int partyPRDPAN { get; set; }
        public string image { get; set; }
        public double imageLatitude { get; set; }
        public double imageLongitude { get; set; }
        public string hash { get; set; }
        public string username { get; set; }
    }
}
