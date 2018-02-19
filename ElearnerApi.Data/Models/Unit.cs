using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace ElearnerApi.Data.Models
{
    [DataContract]
    public class Unit : EntityBase
    {
        [DataMember]
        [JsonProperty( "courseId" )]
        public Guid CourseId { get; set; }

        [JsonIgnore]
        public virtual Course Course { get; set; }

        [DataMember]
        [JsonProperty( "lessons" )]
        [XmlArrayItem( "Lesson" )]
        [XmlArray( "Lessons" )]
        public virtual List<Lesson> Lessons { get; set; }
    }
}