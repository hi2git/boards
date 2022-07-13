using System;
using System.Linq;
using System.Security.Claims;

using Board.Domain.Enums;

using Boards.Commons.Application.Services;

using Microsoft.AspNetCore.Http;

namespace Board.Infrastructure.Jwt.Implementation {
	internal class UserManager : IUserManager {
		private readonly IHttpContextAccessor _httpContextAccessor;

		public UserManager(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

		/// <inheritdoc/>
		public Guid CurrentUserId => IsExist ? Guid.Parse(NameIdentifier) : Guid.Empty;

		/// <inheritdoc/>
		public RoleEnum CurrentRole => (RoleEnum)Enum.Parse(typeof(RoleEnum), RoleId);

		/// <inheritdoc/>
		public string UserName => this.ValueOf(ClaimTypes.Surname);

		/// <inheritdoc/>
		public bool IsExist => this.NameIdentifier != null;

		#region Private

		private string NameIdentifier => this.ValueOf(ClaimTypes.NameIdentifier);

		private string RoleId => this.ValueOf(ClaimTypes.Role);

		private string ValueOf(string claim) => _httpContextAccessor?.HttpContext?.User?.Claims?.SingleOrDefault(x => x.Type == claim)?.Value;

		#endregion

	}
}
