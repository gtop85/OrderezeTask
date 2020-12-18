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
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be between 1 and 100 characters long.", MinimumLength = 1)]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        [StringLength(300, ErrorMessage = "The {0} must be at max 300 characters long.")]
        public string Description { get; set; }

        [DataMember(Name = "imagePath")]
        public string ImagePath { get; set; }

    }
}
