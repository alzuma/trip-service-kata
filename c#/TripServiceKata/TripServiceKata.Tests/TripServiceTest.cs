using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using TripServiceKata.Exception;
using TripServiceKata.Trip;

namespace TripServiceKata.Tests
{
    [TestFixture]
    public class TripServiceTest
    {
        private User.User _loggedUser = null;
        private readonly User.User _guest = null;
        private readonly User.User _notUsedUser = null;
        private readonly User.User _registeredUser = new User.User();
        private readonly User.User _john = new User.User();
        private readonly Trip.Trip _toRiga = new Trip.Trip();
        private readonly Trip.Trip _toAmsterdam = new Trip.Trip();

        [Test]
        public void GetTripsByUser_UserNotLoggedIn_ShouldThrowException()
        {
            _loggedUser = _guest;
            var service = new TestableTripService(this);
            Should.Throw<UserNotLoggedInException>(() => service.GetTripsByUser(_notUsedUser));
        }

        [Test]
        public void GetTripsByUser_UserNotFriends_ShouldNotReturnAnyTrips()
        {
            _loggedUser = _registeredUser;
            var service = new TestableTripService(this);
            
            User.User friend = new User.User();
            friend.AddFriend(_john);
            friend.AddTrip(_toRiga);

            var friendTrips = service.GetTripsByUser(friend);

            friendTrips.ShouldBeEmpty();
        }
        
        
        [Test]
        public void GetTripsByUser_UserAreFriends_ShouldReturnTrips()
        {
            _loggedUser = _registeredUser;
            var service = new TestableTripService(this);
            
            User.User friend = new User.User();
            friend.AddFriend(_john);
            friend.AddFriend(_loggedUser);
            friend.AddTrip(_toRiga);
            friend.AddTrip(_toAmsterdam);

            var friendTrips = service.GetTripsByUser(friend);

            friendTrips.Count.ShouldBe(2);
        }

        private class TestableTripService: TripService
        {
            private readonly TripServiceTest _instance;
            
            public TestableTripService(TripServiceTest instance)
            {
                _instance = instance;
            }
            
            protected override User.User GetLoggedUser()
            {
                return _instance._loggedUser;
            }

            protected override List<Trip.Trip> tripBy(User.User user)
            {
                return user.Trips();
            }
        }
    }
}
