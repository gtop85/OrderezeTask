using System.ComponentModel.DataAnnotations;

namespace EFDataAccessLibrary
{
    public class Image 
    {
        /// <summary>
        /// The Id of the entity
        /// </summary>
        /// 
        public int Id { get; set; }

        /// <summary>
        /// The name of the image. It can be different than actual file name.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// The description of the image
        /// </summary>
        [MaxLength(300)]
        public string Description { get; set; }

        /// <summary>
        /// The path the actual image is stored (normally the blob storage reference)
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string ImagePath { get; set; }
    }
}
