using TuVotoCuenta.Functions.Domain.Models.CosmosDB;

namespace TuVotoCuenta.Functions.Logic.Classes
{
    public class RecordItemParser
    {
        public static RecordItem TransformRecordItem(Domain.Models.Request.RecordItem recordItem)
        {
            RecordItem result = new RecordItem {
                deviceHash = recordItem.deviceHash,
                boxNumber = recordItem.boxNumber,
                boxSection = recordItem.boxSection,
                locationDetails = recordItem.locationDetails,
                entity = recordItem.entity,
                municipality = recordItem.municipality,
                locality = recordItem.locality,
                partyPAN = recordItem.partyPAN,
                partyPRI = recordItem.partyPRI,
                partyPRD = recordItem.partyPRD,
                partyVerde = recordItem.partyVerde,
                partyPT = recordItem.partyPT,
                partyMC = recordItem.partyMC,
                partyNA = recordItem.partyNA,
                partyMOR = recordItem.partyMOR,
                partyES = recordItem.partyES,
                partyINDJai = recordItem.partyINDJai,
                partyOtro = recordItem.partyOtro,
                partyPANMC = recordItem.partyPANMC,
                partyPANPRD = recordItem.partyPANPRD,
                partyPRDPANMC = recordItem.partyPRDPANMC,
                partyPRDMC = recordItem.partyPRDMC,
                partyMORPT = recordItem.partyMORPT,
                partyMORES = recordItem.partyMORES,
                partyPTESMOR = recordItem.partyPTESMOR,
                partyPRIVERNA = recordItem.partyPRIVERNA,
                partyPRIVER = recordItem.partyPRIVER,
                partyPRINA = recordItem.partyPRINA,
                partyVERNA = recordItem.partyVERNA,
                partyPTES = recordItem.partyPTES,
                partyPRDPAN = recordItem.partyPRDPAN,
                image = recordItem.image,
                imageLatitude = recordItem.imageLatitude,
                imageLongitude = recordItem.imageLongitude,
                hash = recordItem.hash,
                username = recordItem.username
            };
            return result;
        }
    }
}
