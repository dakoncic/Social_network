using ModelsLayer.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelsLayer.DatabaseEntities
{
    public class Message
    {
        public int Id { get; set; } //message Id
        [Required]
        public string Text { get; set; }
        public DateTime When { get; set; } //when message was sent


        public string UserId { get; set; } //SENDER ID
        public ApplicationUser ApplicationUsers { get; set; } //parent
    }
}
