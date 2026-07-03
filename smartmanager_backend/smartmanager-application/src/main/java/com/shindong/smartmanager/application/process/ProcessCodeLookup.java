package com.shindong.smartmanager.application.process;

import java.util.Optional;

public interface ProcessCodeLookup {

    Optional<ProcessCodeInfo> findActiveProcessCode(long processCodeId);

    Optional<ProcessCodeInfo> findActiveProcessCodeBySmallCode(String smallCode);

    record ProcessCodeInfo(long id, String smallCode, String smallName) {
    }
}
