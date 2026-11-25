package com.shindong.smartmanager.application.inventory;

public interface PrefixedNumberSequence {

    /**
     * 채번 행을 잠그고 다음 번호를 돌려준다.
     * 행이 없으면 {@code existingMax}를 이미 발급된 최대 번호로 보고 그 다음 번호를 만든다.
     */
    int next(NumberSequenceKind kind, String prefix, int existingMax);
}
