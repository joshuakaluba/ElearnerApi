using Microsoft.AspNetCore.Identity;
using System;

/// <summary>
/// Author: Joshua Kaluba
/// </summary>
namespace ElearnerApi.Data.Models.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public bool Active { get; set; } = true;

        public void ChangeEmail(string email)
        {
            UserName = email.ToLower( );
            Email = email.ToLower( );
            NormalizedEmail = email.ToUpper( );
            NormalizedUserName = email.ToUpper( );
        }
    }
}