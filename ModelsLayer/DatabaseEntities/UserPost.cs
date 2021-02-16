using ModelsLayer.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsLayer.DatabaseEntities
{
    public class UserPost
    {
        public int Id { get; set; } //message Id
        public string postMessage { get; set; }
        public DateTime When { get; set; } //when post was created
        public string ImagePath { get; set; }
        public string linkImagePath { get; set; }
        public string linkURL { get; set; }

        public string UserId { get; set; } //SENDER ID
        public ApplicationUser ApplicationUsers { get; set; } //parent
    }
}
