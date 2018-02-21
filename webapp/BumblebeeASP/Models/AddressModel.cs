using System;
using RestSharp.Deserializers;
namespace BumblebeeASP.Models
{
    public class AddressModel
    {
        [DeserializeAs(Name = "id")]
        public int AddressID { get; set; }

        [DeserializeAs(Name = "personID")]
        public int AddressPersonID { get; set; }

        [DeserializeAs(Name = "companyID")]
        public int AddressCompanyID { get; set; }

        [DeserializeAs(Name = "street1")]
        public String AddressStreet1 { get; set; }

        [DeserializeAs(Name = "street2")]
        public String AddressStreet2 { get; set; }

        [DeserializeAs(Name = "city")]
        public String AddressCity { get; set; }

        [DeserializeAs(Name = "zip")]
        public String AddressZip { get; set; }

        [DeserializeAs(Name = "stateID")]
        public int AddressState { get; set; }

        [DeserializeAs(Name = "typeID")]
        public int AddressType { get; set; }
    }
}
