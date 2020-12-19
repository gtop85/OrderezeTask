using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace OrderezeAPI
{
    [DataContract]
    public class ImageModel
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "imagePath")]
        public string ImagePath { get; set; }

    }
}
