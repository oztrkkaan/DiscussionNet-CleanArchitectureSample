using Eskisehirspor.Domain.Common;
using Eskisehirspor.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Eskisehirspor.Domain.Entities
{
    public class User : AuditableEntity, ISoftDelete
    {
        public const int USERNAME_MAX_LENGTH = 16;
        public const int USERNAME_MIN_LENGTH = 3;
        public const int DISPLAYNAME_MAX_LENGTH = 30;
        public const int DISPLAYNAME_MIN_LENGTH = 1;
        public const int PASSWORD_MIN_LENGTH = 6;
        public const int EMAIL_MAX_LENGTH = 100;
        public const int LOCATION_MAX_LENGTH = 20;
        public const int SIGNATURE_MAX_LENGTH = 255;


        public User(string username, string displayName, string password, string passwordConfirm, string email)
        {
            Username = username;
            DisplayName = displayName;
            SetPassword(password, passwordConfirm);
            SetEmail(email);
            SetAuthorStatus(AuthorStatus);
            SetCreationDate();
        }
        public string Username { get; private set; }
        public string DisplayName { get; private set; }
        public byte[] Password { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        public string Email { get; private set; }
        public string? Location { get; private set; }
        public char TimeOffset { get; private set; }
        public byte TimeOffsetHour { get; private set; }
        public string Signature { get; private set; }
        public AuthorStatuses AuthorStatus { get; private set; }
        public string Roles { get; private set; }
        public bool IsEmailVerified { get; set; }

        public enum AuthorStatuses
        {
            [Display(Name = "Acemi Yazar")]
            Newbie,
            [Display(Name = "Yazar")]
            Author,
            [Display(Name = "Uzaklaştırılmış Yazar")]
            Banned
        }

        public bool IsDeleted { get; private set; }
        public DateTime? DeletionDate { get; private set; }

        public void SoftDelete()
        {
            IsDeleted = true;
            DeletionDate = DateTime.Now;
        }

        private bool IsPasswordValid(string password, string passwordConfirm)
        {
            //TODO: password kuralları tanımlanmalı
            return true;
        }
        private (byte[] password, byte[] passwordSalt) CryptPassword(string password)
        {
            //TODO: password şifrelenmeli
            return (new byte[1], new byte[1]);
        }

        public void SetEmail(string email)
        {
            bool isValidEmail = IsValidEmail(email);
            if (!isValidEmail)
            {
                throw new Exception();
            }
            Email = email;
        }
        private bool IsValidEmail(string email)
        {
            return true;
        }

        public void SetPassword(string password, string passwordConfirm)
        {
            bool isPasswordValid = IsPasswordValid(password, passwordConfirm);
            if (!isPasswordValid)
            {
                throw new Exception();
            }

            var cryptedPassword = CryptPassword(password);
            Password = cryptedPassword.password;
            PasswordSalt = cryptedPassword.passwordSalt;
        }
        public void SetAuthorStatus(AuthorStatuses authorStatus)
        {
            AuthorStatus = authorStatus;
        }
    }
}
