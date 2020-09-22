using System;
using System.Collections.Generic;
using System.Text;
using DemoExercise.Models;

namespace DemoExercise.Interfaces
{
	public interface IUserLoginRepo
	{
		bool DoesUserExist(string username);

		bool IsValidLogin(string username, string password);
	}
}
