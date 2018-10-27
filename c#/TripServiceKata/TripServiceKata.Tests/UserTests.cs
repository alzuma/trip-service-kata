using NUnit.Framework;
using Shouldly;

namespace TripServiceKata.Tests
{
    [TestFixture]
    public class UserTests
    {
        private readonly User.User _bob = new User.User();
        private readonly User.User _john = new User.User();
        
        [Test]
        public void IsFriendsWith_NotFriends_ShouldReturnFalse()
        {
            var user = new User.User();
            user.AddFriend(_bob);
            user.IsFriendsWith(_john).ShouldBeFalse();
        }
        
        [Test]
        public void IsFriendsWith_Friends_ShouldReturnTrue()
        {
            var user = new User.User();
            user.AddFriend(_bob);
            user.AddFriend(_john);
            user.IsFriendsWith(_john).ShouldBeTrue();
        }
    }
}