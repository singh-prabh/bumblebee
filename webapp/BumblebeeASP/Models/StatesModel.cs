using System;
using RestSharp.Deserializers;
namespace BumblebeeASP.Models
{
    public class StatesModel
    {
        [DeserializeAs(Name = "id")]
        public int StateID { get; set; }
        [DeserializeAs(Name = "name")]
        public string StateName { get; set; }
        [DeserializeAs(Name = "abbreviation")]
        public string StateAbbreviation { get; set; }
    }
}
