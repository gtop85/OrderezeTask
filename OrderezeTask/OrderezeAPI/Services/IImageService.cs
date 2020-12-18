﻿using System.Collections.Generic;

namespace OrderezeAPI
{
    /// <summary>
    /// Service responsible managing all Images operations
    /// </summary>
    public interface IImageService
    {
        /// <summary>
        /// Returns all images 
        /// </summary>
        /// <returns></returns>
        List<ImageModel> GetImages();

        /// <summary>
        /// Adds the supplied <paramref name="image"/> to the system and returns the Id.
        /// Part of the operation is to store the Image in the blob storage.
        /// </summary>
        int AddNewImage(ImageModel image);

        /// <summary>
        /// Deletes the Image with the supplied <paramref name="id"/> from the system 
        /// and deletes the file from the blob storage as well.
        /// </summary>
        bool DeleteImage(int id);
    }
}
