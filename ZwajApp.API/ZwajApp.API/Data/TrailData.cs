using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZwajApp.API.Models;

namespace ZwajApp.API.Data
{
    public class TrailData
    {
        //private readonly DataContext _context;
        //public  TrailData(DataContext context)
        //{
        //    _context = context;
        //}

        //public void TrialUsers()
        //{
        //    var userdata = System.IO.File.ReadAllText("Data/UsersTrialData.json");
        //    var users = JsonConvert.DeserializeObject<List<User>>(userdata);

        //    foreach (var user in users)
        //    {
        //        byte[] passwordHash, PasswordSalt;
        //        CreatePasswordHash("password", out passwordHash, out PasswordSalt);
        //        user.passwordHash = passwordHash;
        //        user.PasswordSalt = PasswordSalt;
        //        user.UserName = user.UserName.ToLower();
        //        _context.Add(user);
        //    }
        //    _context.SaveChanges();
        //}

        //private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        //{
        //    using (var hmac = new System.Security.Cryptography.HMACSHA512())
        //    {
        //        passwordSalt = hmac.Key;
        //        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //    }
        //}
    }
}
