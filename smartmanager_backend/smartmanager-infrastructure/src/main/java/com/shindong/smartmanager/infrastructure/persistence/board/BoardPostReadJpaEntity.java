package com.shindong.smartmanager.infrastructure.persistence.board;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.time.Instant;

@Entity
@Table(name = "board_post_read")
public class BoardPostReadJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "post_id", nullable = false)
    private Long postId;

    @Column(name = "reader_user_id", nullable = false)
    private Long readerUserId;

    @Column(name = "read_at", nullable = false)
    private Instant readAt;

    protected BoardPostReadJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public Long getPostId() {
        return postId;
    }

    public void setPostId(Long postId) {
        this.postId = postId;
    }

    public Long getReaderUserId() {
        return readerUserId;
    }

    public void setReaderUserId(Long readerUserId) {
        this.readerUserId = readerUserId;
    }

    public Instant getReadAt() {
        return readAt;
    }

    public void setReadAt(Instant readAt) {
        this.readAt = readAt;
    }
}
