package com.shindong.smartmanager.application.publiccode;

public interface PublicCodeReferenceChecker {

    String usageType();

    void assertNotReferenced(long publicCodeId, String smallCode, String smallName);
}
