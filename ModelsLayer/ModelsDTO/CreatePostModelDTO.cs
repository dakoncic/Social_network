using Microsoft.AspNetCore.Http;
using ModelsLayer.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsLayer.ModelsDTO
{
    public class CreatePostModelDTO
    {
        public CreatePostModelDTO()
        {
            StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
        }

        public string postMessage { get; set; }
        public IFormFile image { get; set; }
        public string ImagePath { get; set; }

        public string userID { get; set; }
        public DateTime StartDate { get; set; }

        //GET
        public IEnumerable<UserPost> UserPosts { get; set; }
    }
}
