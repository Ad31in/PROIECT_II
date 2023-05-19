using CloudinaryDotNet.Actions;
using DataLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Interfaces
{
    public interface IPhotoRepository
    {
        Task<Photo?> AddPhotoAsync(ImageUploadResult result, Guid movieId);
    }
}
