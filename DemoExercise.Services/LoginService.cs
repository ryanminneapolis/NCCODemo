using DemoExercise.Interfaces;
using DemoExercise.Models;
using DemoExercise.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoExercise.Services
{
    public class LoginService
    {
        // Do we use "_" for private variables?
        private IUserLoginRepo repo;

        // TODO: Needs logging

        public LoginService(IUserLoginRepo repo)
        {
            this.repo = repo;
        }

        public TResponse<bool> ValidateUsername(string username)
        {
            TResponse<bool> response = new TResponse<bool>();

            if (string.IsNullOrWhiteSpace(username))
            {
                response.Message = "Cannot fetch user without Username.";
                response.IsSuccesful = false;

                return response;
            }

            response.Payload = repo.DoesUserExist(username);

            response.IsSuccesful = response.Payload;

            if (!response.IsSuccesful)
            {
                response.Message = $"Cannot get user with Username: {username}";
            }

            return response;
        }

        public TResponse<bool> ValidatePassword(string username, string password)
        {
            TResponse<bool> response = new TResponse<bool>();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                response.Message = $"Cannot validate without more information: User: {username} with Password: {password}.";
                response.IsSuccesful = false;

                return response;
            }

            response.Payload = repo.IsValidLogin(username, password);

            response.IsSuccesful = response.Payload;

            if (!response.IsSuccesful)
            {
                response.Message = $"Unable to validate User: {username} with Password: {password}.";
            }

            return response;
        }
    }
}
