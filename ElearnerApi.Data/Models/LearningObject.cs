using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace ElearnerApi.Data.Models
{
    [DataContract]
    public class LearningObject : EntityBase
    {
        [DataMember]
        [XmlAttribute( AttributeName = "LearningTask" )]
        [JsonProperty( "learningTask" )]
        public string LearningTask { get; set; }

        [DataMember]
        [JsonProperty( "lessonId" )]
        public Guid LessonId { get; set; }

        [JsonIgnore]
        public virtual Lesson Lesson { get; set; }
    }
}