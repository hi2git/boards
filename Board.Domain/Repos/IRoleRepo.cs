using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Board.Domain.Enums;
using Board.Domain.Models;

namespace Board.Domain.Repos {
	public interface IRoleRepo : IRepo<Role> {

		Task<Role> Get(RoleEnum id);


	}
}
