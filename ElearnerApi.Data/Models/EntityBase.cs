using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace ElearnerApi.Data.Models
{
    [DataContract]
    public abstract class EntityBase : IEquatable<EntityBase>
    {
        [DataMember]
        [XmlAttribute( AttributeName = "Id" )]
        [JsonProperty( "id" )]
        public Guid Id { get; set; } = Guid.NewGuid( );

        [XmlAttribute( AttributeName = "Name" )]
        [JsonProperty( "name" )]
        [Display( Name = "Name" )]
        public string Name { get; set; } = "";

        [JsonProperty( "dateCreated" )]
        [Display( Name = "Date Created" )]
        [XmlAttribute( AttributeName = "DateCreated" )]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public EntityBase()
        {
            Id = Guid.NewGuid( );
        }

        public bool Equals(EntityBase other)
        {
            return Id == other.Id;
        }
    }
}