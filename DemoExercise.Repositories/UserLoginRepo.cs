using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using DemoExercise.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using DemoExercise.Interfaces;
using System.Reflection.Metadata;
using System.Linq;

namespace DemoExercise.Repositories
{
    public class UserLoginRepo : IUserLoginRepo
    {
        // Do we use "_" for private variables?
        private readonly string connectionString;

        // I know this isn't the correct way to run a mock repo (should be its own class) and I don't have access to fix the sprocs!
        private readonly List<UserViewModel> dummyData = new List<UserViewModel>()
        {
            new UserViewModel(){ UserId = 1, Username = "JohnDoe", Password = "Password" },
            new UserViewModel(){ UserId = 2, Username = "JaneDoe", Password = "Password" }
        };

        // TODO: Needs logging

        public UserLoginRepo(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        public bool DoesUserExist(string username)
        {
            //using (SqlConnection cn = new SqlConnection(connectionString))
            //using (SqlCommand cmd = new SqlCommand("dbo.UserExists", cn))
            //{
            //    cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //    cmd.Parameters.Add("@username", System.Data.SqlDbType.NVarChar, 10).Value = username;
            //    cmd.Parameters["@username"].Direction = System.Data.ParameterDirection.Input;

            //    cmd.Parameters.Add("@exists", System.Data.SqlDbType.Bit).Direction = System.Data.ParameterDirection.Output;

            //    try
            //    {
            //        cn.Open();

            //        cmd.ExecuteNonQuery();
            //    }
            //    catch (Exception ex)
            //    {
            //        // TODO: log exception and handle error
            //    }

            //    return (bool)cmd.Parameters["@exists"].Value;
            //}

            // The SQL sproc is case-insensitive by default, but here we need to be explicit.
            return dummyData.Exists(u => u.Username.ToLower() == username.ToLower());
        }

        public bool IsValidLogin(string username, string password)
        {
            //using (SqlConnection cn = new SqlConnection(connectionString))
            //using (SqlCommand cmd = new SqlCommand("dbo.IsLoginValid", cn))
            //{
            //    cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //    cmd.Parameters.Add("@username", System.Data.SqlDbType.NVarChar, 10).Value = username;
            //    cmd.Parameters["@username"].Direction = System.Data.ParameterDirection.Input;

            //    cmd.Parameters.Add("@password", System.Data.SqlDbType.NVarChar, 10).Value = password;
            //    cmd.Parameters["@password"].Direction = System.Data.ParameterDirection.Input;

            //    // In retrospect, I would change the sproc to a simple SELECT and return either a user (mapped to a userVM object) or an empty set instead of an OUTPUT variable.
            //    cmd.Parameters.Add("@isValid", System.Data.SqlDbType.Bit).Direction = System.Data.ParameterDirection.Output;

            //    try
            //    {
            //        cn.Open();

            //        cmd.ExecuteNonQuery();
            //    }
            //    catch (Exception ex)
            //    {
            //        // TODO: Log exception and handle error
            //    }

            //    return (bool)cmd.Parameters["@isValid"].Value;
            //}

            return dummyData.Where(u => u.Username.ToLower() == username.ToLower()).FirstOrDefault().Password == password;
        }
    }
}
