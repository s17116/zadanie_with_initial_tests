using System;
using System.Collections.Generic;
using System.Threading;
using LegacyApp.Models;

namespace LegacyApp.Repository;

public static class ClientRepository
{
	/// <summary>
	/// This collection is used to simulate remote database
	/// </summary>
	public static readonly Dictionary<int, Client> _database = new Dictionary<int, Client>()
	{
		{1, new Client{ClientId = 1, Name = "Kowalski", Address = "Warszawa, Złota 12", Email = "kowalski@wp.pl", Type = Client.ClientType.NormalClient}},
		{2, new Client{ClientId = 2, Name = "Malewski", Address = "Warszawa, Koszykowa 86", Email = "malewski@gmail.pl", Type = Client.ClientType.VeryImportantClient}},
		{3, new Client{ClientId = 3, Name = "Smith", Address = "Warszawa, Kolorowa 22", Email = "smith@gmail.pl", Type = Client.ClientType.ImportantClient}},
		{4, new Client{ClientId = 4, Name = "Doe", Address = "Warszawa, Koszykowa 32", Email = "doe@gmail.pl", Type = Client.ClientType.ImportantClient}},
		{5, new Client{ClientId = 5, Name = "Kwiatkowski", Address = "Warszawa, Złota 52", Email = "kwiatkowski@wp.pl", Type = Client.ClientType.NormalClient}},
		{6, new Client{ClientId = 6, Name = "Andrzejewicz", Address = "Warszawa, Koszykowa 52", Email = "andrzejewicz@wp.pl", Type = Client.ClientType.NormalClient}}
	};
	/// <summary>
	/// Simulating fetching a client from remote database
	/// </summary>
	/// <returns>Returning client object</returns>
	public static Client GetById(int clientId)
	{
		int randomWaitTime = new Random().Next(2000);
		Thread.Sleep(randomWaitTime);

		if (_database.ContainsKey(clientId))
			return _database[clientId];

		throw new ArgumentException($"User with id {clientId} does not exist in database");
	}
}