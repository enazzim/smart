package com.shindong.smartmanager.infrastructure.persistence.board;

import com.shindong.smartmanager.domain.board.BoardType;
import com.shindong.smartmanager.domain.board.PostKind;
import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.EnumType;
import jakarta.persistence.Enumerated;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import java.time.Instant;

@Entity
@Table(name = "board_post")
public class BoardPostJpaEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Enumerated(EnumType.STRING)
    @Column(name = "board_type", nullable = false, columnDefinition = "ENUM('NOTICE','PRESIDENT_NOTICE','PRODUCT','LASER','INSTITUTE','SALES_QC')")
    private BoardType boardType;

    @Enumerated(EnumType.STRING)
    @Column(name = "post_kind", nullable = false, columnDefinition = "ENUM('TOP','REPLY')")
    private PostKind postKind;

    @Column(name = "parent_post_id")
    private Long parentPostId;

    @Column(name = "thread_root_id")
    private Long threadRootId;

    @Column(name = "title", length = 500)
    private String title;

    @Column(name = "content", columnDefinition = "MEDIUMTEXT")
    private String content;

    @Column(name = "author_user_id")
    private Long authorUserId;

    @Column(name = "view_count", nullable = false)
    private int viewCount;

    @Column(name = "pinned", nullable = false, columnDefinition = "TINYINT")
    private int pinned;

    @Column(name = "recording_state", nullable = false, columnDefinition = "TINYINT")
    private int recordingState = 1;

    @Column(name = "created_by", length = 100)
    private String createdBy;

    @Column(name = "created_by_id", length = 100)
    private String createdById;

    @Column(name = "created_at", nullable = false)
    private Instant createdAt;

    @Column(name = "updated_by", length = 100)
    private String updatedBy;

    @Column(name = "updated_by_id", length = 100)
    private String updatedById;

    @Column(name = "updated_at")
    private Instant updatedAt;

    protected BoardPostJpaEntity() {
    }

    public Long getId() {
        return id;
    }

    public BoardType getBoardType() {
        return boardType;
    }

    public void setBoardType(BoardType boardType) {
        this.boardType = boardType;
    }

    public PostKind getPostKind() {
        return postKind;
    }

    public void setPostKind(PostKind postKind) {
        this.postKind = postKind;
    }

    public Long getParentPostId() {
        return parentPostId;
    }

    public void setParentPostId(Long parentPostId) {
        this.parentPostId = parentPostId;
    }

    public Long getThreadRootId() {
        return threadRootId;
    }

    public void setThreadRootId(Long threadRootId) {
        this.threadRootId = threadRootId;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getContent() {
        return content;
    }

    public void setContent(String content) {
        this.content = content;
    }

    public Long getAuthorUserId() {
        return authorUserId;
    }

    public void setAuthorUserId(Long authorUserId) {
        this.authorUserId = authorUserId;
    }

    public int getViewCount() {
        return viewCount;
    }

    public void setViewCount(int viewCount) {
        this.viewCount = viewCount;
    }

    public boolean isPinned() {
        return pinned == 1;
    }

    public void setPinned(boolean pinned) {
        this.pinned = pinned ? 1 : 0;
    }

    public int getRecordingState() {
        return recordingState;
    }

    public void setRecordingState(int recordingState) {
        this.recordingState = recordingState;
    }

    public String getCreatedBy() {
        return createdBy;
    }

    public void setCreatedBy(String createdBy) {
        this.createdBy = createdBy;
    }

    public String getCreatedById() {
        return createdById;
    }

    public void setCreatedById(String createdById) {
        this.createdById = createdById;
    }

    public Instant getCreatedAt() {
        return createdAt;
    }

    public void setCreatedAt(Instant createdAt) {
        this.createdAt = createdAt;
    }

    public String getUpdatedBy() {
        return updatedBy;
    }

    public void setUpdatedBy(String updatedBy) {
        this.updatedBy = updatedBy;
    }

    public String getUpdatedById() {
        return updatedById;
    }

    public void setUpdatedById(String updatedById) {
        this.updatedById = updatedById;
    }

    public Instant getUpdatedAt() {
        return updatedAt;
    }

    public void setUpdatedAt(Instant updatedAt) {
        this.updatedAt = updatedAt;
    }
}
