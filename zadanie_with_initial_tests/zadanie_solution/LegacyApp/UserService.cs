using LegacyApp.Models;
using LegacyApp.Repository;
using LegacyApp.Services;
using System;
using System.Linq.Expressions;

namespace LegacyApp;

public class UserService
{
	private bool ValidateParameters(string firstName, string lastName, string email)
	{
		if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(lastName))
		{
			return false;
		}
		return true;
	}

	private bool ValidateEmail(string email)
	{
		if (!email.Contains("@") && !email.Contains("."))
		{
			return false;
		}
		return true;
	}
	private bool ValidateAge(DateTime dateOfBirth)
	{
		var now = DateTime.Now;
		int age = now.Year - dateOfBirth.Year;
		if (now.Month < dateOfBirth.Month || now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day) age--;

		//if (age < 21)
		//{
		//    return false;
		//}
		//return true;
		return age >= 21;
	}

	private bool ValidateCreditLimitForNormalUser(User user)
	{
		return user.CreditLimit >= 500;
	}

	private bool SetUsersCredit(Client client, User user)
	{
		if (client.Type == Client.ClientType.ImportantClient)
		{
			int creditLimit = UserCreditService.GetCreditLimit(user.LastName);
			creditLimit *= 2;
			user.CreditLimit = creditLimit;
		}
		else if (client.Type == Client.ClientType.NormalClient)
		{
			user.HasCreditLimit = true;
			int creditLimit = UserCreditService.GetCreditLimit(user.LastName);
			user.CreditLimit = creditLimit;
			if (!ValidateCreditLimitForNormalUser(user))
			{
				return false;
			}
		}
		return true;
	}
	public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
	{
		if (!ValidateAge(dateOfBirth) || !ValidateEmail(email) || !ValidateParameters(firstName, lastName, email))
		{
			return false;
		}
		

		var client = ClientRepository.GetById(clientId);
		var user = new User
		{
			Client = client,
			DateOfBirth = dateOfBirth,
			EmailAddress = email,
			FirstName = firstName,
			LastName = lastName
		};

		if (!SetUsersCredit(client, user))
		{
			return false;
		}

		UserDataAccess.AddUser(user);
		return true;
	}
}
