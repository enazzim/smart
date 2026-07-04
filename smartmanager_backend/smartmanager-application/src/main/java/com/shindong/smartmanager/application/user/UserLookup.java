package com.shindong.smartmanager.application.user;

public interface UserLookup {

    boolean existsActive(long userId);

    String findActiveName(long userId);
}
