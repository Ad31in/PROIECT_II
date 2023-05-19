using CloudinaryDotNet.Actions;
using DataLogic.Data;
using DataLogic.Entities;
using DataLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DataContext _dataContext;

        public PhotoRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Photo?> AddPhotoAsync(ImageUploadResult result, Guid movieId)
        {
            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                MovieId = movieId
            };

            await _dataContext.Photos.AddAsync(photo);
            var status = await _dataContext.SaveChangesAsync();

            if (status > 0)
            {
                return photo;
            }

            return null;
        }

    }
}
