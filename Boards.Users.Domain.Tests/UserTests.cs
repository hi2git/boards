using Boards.Users.Domain.Models;


using NUnit.Framework;

namespace Boards.Users.Domain.Tests {
	public class UserTests {

		[Test, TestCaseSource(typeof(UserData), nameof(UserData.Props))]
		public void CtorTest(string propName, object expected) {
			var entity = UserData.Create();
			var actual = entity.GetType().GetProperty(propName).GetValue(entity);
			Assert.That(actual, Is.EqualTo(expected));
		}

	}
}