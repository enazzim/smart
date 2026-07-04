package com.shindong.smartmanager.application.role;

import java.util.List;

public interface RoleRepository {

    List<RoleView> findAllActive();

    boolean allActiveIdsExist(List<Long> roleIds);
}
