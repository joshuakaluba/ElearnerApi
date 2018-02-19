using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace ElearnerApi.Data.Models
{
    [DataContract]
    public class Lesson : EntityBase
    {
        [DataMember]
        [JsonProperty( "unitId" )]
        public Guid UnitId { get; set; }

        [JsonIgnore]
        public virtual Unit Unit { get; set; }

        [DataMember]
        [XmlAttribute( AttributeName = "Description" )]
        [JsonProperty( "description" )]
        public string Description { get; set; }

        [DataMember]
        [XmlArrayItem( "LearningObject" )]
        [XmlArray( "LearningObjects" )]
        [JsonProperty( "learningObjects" )]
        public virtual List<LearningObject> LearningObjects { get; set; }
    }
}