using System;
using System.ComponentModel.DataAnnotations;

namespace EricNee.AutoStartDesktop.Library
{
    public class AdminEntity
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Password { get; set; }

        public string Settings { get; set; }
    }
}