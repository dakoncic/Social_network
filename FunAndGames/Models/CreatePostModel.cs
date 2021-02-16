using Microsoft.AspNetCore.Http;
using ModelsLayer.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FunAndGames.Models
{
    public class CreatePostModel
    {
        public CreatePostModel()
        {
            StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
        }

        [Required]
        public string postMessage { get; set; }
        public string userID { get; set; }
        public DateTime StartDate { get; set; }
        public IFormFile image { get; set; }

        //GET
        public IEnumerable<UserPost> UserPosts { get; set; }
    }
}
