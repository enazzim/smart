package com.shindong.smartmanager.application.code;

import java.util.List;

public interface CodeGroupOptionsRepository {

    List<CodeOptionView> findActiveOptions(String codeGroupKey);
}
