using Microsoft.AspNetCore.Identity;
using ModelsLayer.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsLayer.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base()
        {
        }

        public List<Message> Messages { get; set; } //child
        public List<UserPost> UserPosts { get; set; } //child
    }
}
