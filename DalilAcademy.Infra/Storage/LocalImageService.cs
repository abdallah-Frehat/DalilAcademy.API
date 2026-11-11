using DalilAcademy.Core.Exceptions;
using DalilAcademy.Core.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Infra.Storage
{
    public class LocalImageService : IImageService
    {
        private readonly string _folder = Path.Combine
            (Directory.GetCurrentDirectory(), "wwwroot", "images", "Courses");
        private readonly string[] Allowed = [".jpg", ".jpeg", ".png", ".gif", ".webp"];
        public async Task<string> SaveCourseImageAsync(Stream content, string fileName, long length)
        {
            if (length == 0 || length > 2 * 1024 * 1024)
                throw new InvalidFileException("Image must be less than 2 MB");

            var ext = Path.GetExtension(fileName).ToLower();
            if (!Allowed.Contains(ext))
                throw new InvalidFileException("Only .jpg, .jpeg, .png, .webp ");

            Directory.CreateDirectory(_folder);
            var name = $"{Guid.NewGuid()}{ext}";

            await using var file = File.Create(Path.Combine(_folder, name));//والusing استخدمانها عشان  يسكر الملف  بنفسه  
                                                                            //ينشى ملف فاضي  
            await content.CopyToAsync(file); // ينسخ  محتوى  الصورة اللي  وصلتنا  

            return"/images/Courses" + name;
        }
        public void DeleteCourseImage(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl)) return;
            var path = Path.Combine(_folder, Path.GetFileName(imageUrl));
            if (File.Exists(path))
                File.Delete(path);
        }

    }
}
