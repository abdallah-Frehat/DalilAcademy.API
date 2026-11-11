using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Storage
{
    public interface IImageService
    {
        Task<string> SaveCourseImageAsync(Stream contact, string filename, long length);
        void DeleteCourseImage(string imageUrl);
        

    }
}
