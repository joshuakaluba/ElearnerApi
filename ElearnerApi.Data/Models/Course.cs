using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace ElearnerApi.Data.Models
{
    [DataContract]
    [Serializable, XmlRoot( "Course" )]
    public class Course : EntityBase
    {
        [DataMember]
        [XmlAttribute( AttributeName = "CategoryId" )]
        [JsonProperty( "categoryId" )]
        [Display( Name = "Category Id" )]
        public string CategoryId { get; set; } = "";


        [DataMember]
        [XmlAttribute( AttributeName = "Description" )]
        [JsonProperty( "description" )]
        [Display( Name = "Description" )]
        public string Description { get; set; }

        [DataMember]
        [JsonProperty( "units" )]
        [XmlArrayItem( "Unit" )]
        [XmlArray( "Units" )]
        public virtual List<Unit> Units { get; set; }
    }
}