using System;
using Contracts;
using Entities.Models;

namespace Repository
{
	public class ClientMessageRepository : RepositoryBase<ClientMessage>, IClientMessageRepository
	{
		public ClientMessageRepository(RepositoryContext repositoryContext)
			:base(repositoryContext)
		{
		}

        public void CreateClientMessage(ClientMessage message)
        {
            Create(message);
        }
    }
}

