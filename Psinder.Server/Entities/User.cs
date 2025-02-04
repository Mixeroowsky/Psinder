﻿using System.ComponentModel.DataAnnotations;

namespace Psinder.Server.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Shelter Shelter { get; set; }
    }
}
