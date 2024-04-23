using System;
using Entities.Models;
using System.Security.Cryptography;

namespace Contracts
{
	public interface IClientMessageRepository
	{
        void CreateClientMessage(ClientMessage message);

    }
}

