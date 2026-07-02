package com.shindong.smartmanager.application.process;

import java.util.Optional;

public interface ProcessCodeLookup {

    Optional<ProcessCodeInfo> findActiveProcessCode(long processCodeId);

    record ProcessCodeInfo(long id, String smallCode, String smallName) {
    }
}
