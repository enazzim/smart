package com.shindong.smartmanager.infrastructure.persistence.support;

import java.sql.Timestamp;
import java.time.Instant;
import java.time.LocalDateTime;
import java.time.OffsetDateTime;
import java.time.ZoneId;
import java.time.ZonedDateTime;
import java.time.format.DateTimeParseException;

/**
 * Native query 시각 컬럼을 Instant로 맞춘다.
 * JDBC URL의 serverTimezone=Asia/Seoul 기준으로, 시간대 없는 LocalDateTime은 서울 시각이다.
 */
public final class JdbcInstantValues {

    static final ZoneId SEOUL = ZoneId.of("Asia/Seoul");

    private JdbcInstantValues() {
    }

    public static Instant toInstant(Object value) {
        if (value == null) {
            return null;
        }
        if (value instanceof Instant instant) {
            return instant;
        }
        if (value instanceof Timestamp timestamp) {
            return timestamp.toInstant();
        }
        if (value instanceof OffsetDateTime offsetDateTime) {
            return offsetDateTime.toInstant();
        }
        if (value instanceof ZonedDateTime zonedDateTime) {
            return zonedDateTime.toInstant();
        }
        if (value instanceof LocalDateTime localDateTime) {
            return localDateTime.atZone(SEOUL).toInstant();
        }
        String text = value.toString();
        try {
            return Instant.parse(text);
        } catch (DateTimeParseException ex) {
            return LocalDateTime.parse(text).atZone(SEOUL).toInstant();
        }
    }
}
