using Eskisehirspor.Domain.Common;
using Eskisehirspor.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Eskisehirspor.Domain.Entities
{
    public class User : AuditableEntity, ISoftDelete
    {
        public const int USERNAME_MAX_LENGTH = 16;
        public const int USERNAME_MIN_LENGTH = 3;
        public const string USERNAME_REGEX = @"^@?(\w){{0},{1}}$";

        public const int DISPLAYNAME_MAX_LENGTH = 30;
        public const int DISPLAYNAME_MIN_LENGTH = 1;

        public const int PASSWORD_MIN_LENGTH = 8;
        public const string PASSWORD_RULE_MESSAGE = "Şifre en az {0} karakterden oluşmalı";

        public const int EMAIL_MAX_LENGTH = 100;
        public const int LOCATION_MAX_LENGTH = 20;
        public const int SIGNATURE_MAX_LENGTH = 255;


        public User(string username, string displayName, string password, string passwordConfirm, string email)
        {
            SetUsername(username);
            SetDisplayName(displayName);
            SetPassword(password, passwordConfirm);
            SetEmail(email);
            SetAuthorStatus(AuthorStatuses.Newbie);
        }
        public string Username { get; private set; }
        public string DisplayName { get; private set; }
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        public string Email { get; private set; }
        public string? Location { get; private set; }
        public char TimeOffset { get; private set; }
        public byte TimeOffsetHour { get; private set; }
        public string Signature { get; private set; }
        public AuthorStatuses AuthorStatus { get; private set; }
        public string Roles { get; private set; }
        public bool IsEmailVerified { get; set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletionDate { get; private set; }

        public enum AuthorStatuses
        {
            [Display(Name = "Acemi Yazar")]
            Newbie,
            [Display(Name = "Yazar")]
            Author,
            [Display(Name = "Uzaklaştırılmış Yazar")]
            Banned
        }


        public void SetDisplayName(string displayName)
        {
            if (!IsValidDisplayName(displayName))
            {
                throw new Exception($"DisplayName length must be {DISPLAYNAME_MIN_LENGTH}-{DISPLAYNAME_MAX_LENGTH}");
            }
            DisplayName = displayName;
        }
        public static bool IsValidDisplayName(string displayName)
        {
            if (displayName.Length < DISPLAYNAME_MIN_LENGTH || displayName.Length > DISPLAYNAME_MAX_LENGTH)
            {
                return false;
            }
            return true;
        }
        public static bool IsValidUsername(string username)
        {
            var usernameRegex = string.Format(USERNAME_REGEX, USERNAME_MIN_LENGTH, USERNAME_MAX_LENGTH);
            var regex = new Regex(usernameRegex);

            return regex.IsMatch(username);
        }
        private void SetUsername(string username)
        {
            if (!IsValidUsername(username))
            {
                throw new Exception("Invalid username");
            }
            Username = username;
        }
        public void SoftDelete()
        {
            IsDeleted = true;
            DeletionDate = DateTime.Now.ToUniversalTime();
        }
        public static bool IsValidPassword(string password, string passwordConfirm)
        {
            if (password != passwordConfirm)
            {
                throw new Exception();
            }
            if (password.Length < PASSWORD_MIN_LENGTH)
            {
                throw new Exception();
            }
            return true;
        }
        private (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            return (hmac.Key, hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
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
        public static bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void SetPassword(string password, string passwordConfirm)
        {
            bool isValidPassword = IsValidPassword(password, passwordConfirm);
            if (!isValidPassword)
            {
                throw new Exception();
            }

            var cryptedPassword = CreatePasswordHash(password);
            PasswordHash = cryptedPassword.passwordHash;
            PasswordSalt = cryptedPassword.passwordSalt;
        }
        public void SetAuthorStatus(AuthorStatuses authorStatus)
        {
            AuthorStatus = authorStatus;
        }
    }
}
