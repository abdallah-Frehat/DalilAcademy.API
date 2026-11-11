using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Mappings
{
    public static class CourseMapping
    {
        public static CourseResponseDTO ToDTO(this Course course)
        {
            return new CourseResponseDTO
                (course.Id, course.Title,course.Price, course.Instructor?.FullName, course.ImageUrl);
        }
        public static Course ToEntity(this CreateCourseDTO createCourseDTO)
        {
            return new Course
            {
                Title = createCourseDTO.Title,
                Price = createCourseDTO.Price,
                InstructorId = createCourseDTO.InstructorId,
                ImageUrl = createCourseDTO.ImageUrl
            };
        }
        public static void UpdateEntity(this Course course, UpdateCourseDTO updateCourseDTO)
        {
            course.Title = updateCourseDTO.Title;
            course.Price = updateCourseDTO.Price;
        }
    }
}
