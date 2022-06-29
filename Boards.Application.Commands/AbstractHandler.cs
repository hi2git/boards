using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Boards.Domain.Contracts;

using MassTransit;

using MediatR;

namespace Boards.Application.Commands {
	internal class AbstractHandler {

		protected static Unit ThrowIfError(IResponse response) => !response.IsError ? Unit.Value : throw new CommandException(response.Message);


	}
}
