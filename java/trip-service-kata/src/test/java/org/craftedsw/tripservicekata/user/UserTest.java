package org.craftedsw.tripservicekata.user;

import org.assertj.core.api.Assertions;
import org.junit.Test;

import java.util.List;

public class UserTest {

    @Test
    public void
    create_empty_user_friends_should_be_zer() {
        User user = new User();
        List<User> friends = user.getFriends();
        Assertions.assertThat(friends).hasSize(0);
    }
}
