package com.shindong.smartmanager.infrastructure.persistence.support;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNull;

import java.time.Instant;
import java.time.LocalDateTime;
import org.junit.jupiter.api.Tag;
import org.junit.jupiter.api.Test;

@Tag("core")
class JdbcInstantValuesTest {

    @Test
    void parsesLocalDateTimeWithoutOffsetAsSeoul() {
        LocalDateTime local = LocalDateTime.parse("2026-11-25T06:51:20.398");
        Instant expected = local.atZone(JdbcInstantValues.SEOUL).toInstant();

        assertEquals(expected, JdbcInstantValues.toInstant(local));
        assertEquals(expected, JdbcInstantValues.toInstant("2026-11-25T06:51:20.398"));
    }

    @Test
    void keepsInstantAndOffsetText() {
        Instant instant = Instant.parse("2026-11-25T06:51:20.398Z");
        assertEquals(instant, JdbcInstantValues.toInstant(instant));
        assertEquals(instant, JdbcInstantValues.toInstant("2026-11-25T06:51:20.398Z"));
        assertNull(JdbcInstantValues.toInstant(null));
    }
}
