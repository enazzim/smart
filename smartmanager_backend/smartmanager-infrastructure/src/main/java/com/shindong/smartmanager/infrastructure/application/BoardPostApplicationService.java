package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.board.BoardAttachmentDownload;
import com.shindong.smartmanager.application.board.BoardAttachmentView;
import com.shindong.smartmanager.application.board.BoardPostCommand;
import com.shindong.smartmanager.application.board.BoardPostDetailView;
import com.shindong.smartmanager.application.board.BoardPostListCriteria;
import com.shindong.smartmanager.application.board.BoardPostPageView;
import com.shindong.smartmanager.application.board.BoardPostService;
import com.shindong.smartmanager.application.board.BoardReplyCommand;
import com.shindong.smartmanager.application.board.BoardUploadFile;
import com.shindong.smartmanager.application.board.DashboardWidgetView;
import com.shindong.smartmanager.domain.board.BoardType;
import java.util.List;
import java.util.Locale;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class BoardPostApplicationService {

    private final BoardPostService boardPostService;

    public BoardPostApplicationService(BoardPostService boardPostService) {
        this.boardPostService = boardPostService;
    }

    @Transactional(readOnly = true)
    public String editorTemplate() {
        return boardPostService.defaultEditorTemplate();
    }

    @Transactional(readOnly = true)
    public BoardPostPageView list(BoardPostListCriteria criteria) {
        return boardPostService.list(criteria);
    }

    @Transactional
    public BoardPostDetailView getDetail(long postId, long actorUserId, boolean incrementViewCount) {
        return boardPostService.getDetail(postId, actorUserId, incrementViewCount);
    }

    @Transactional
    public BoardPostDetailView createTopPost(
            BoardPostCommand command,
            List<BoardUploadFile> uploadFiles,
            long authorUserId,
            String actorLoginId,
            String actorUserId
    ) {
        return boardPostService.createTopPost(command, uploadFiles, authorUserId, actorLoginId, actorUserId);
    }

    @Transactional
    public BoardPostDetailView createReply(
            long parentPostId,
            BoardReplyCommand command,
            List<BoardUploadFile> uploadFiles,
            long authorUserId,
            String actorLoginId,
            String actorUserId
    ) {
        return boardPostService.createReply(parentPostId, command, uploadFiles, authorUserId, actorLoginId, actorUserId);
    }

    @Transactional
    public BoardPostDetailView updatePost(
            long postId,
            String title,
            String content,
            long actorUserId,
            boolean moderator,
            String actorLoginId,
            String actorUserIdText
    ) {
        return boardPostService.updatePost(postId, title, content, actorUserId, moderator, actorLoginId, actorUserIdText);
    }

    @Transactional
    public void deletePost(long postId, long actorUserId, boolean moderator, String actorLoginId, String actorUserIdText) {
        boardPostService.deletePost(postId, actorUserId, moderator, actorLoginId, actorUserIdText);
    }

    @Transactional
    public List<BoardAttachmentView> addAttachments(
            long postId,
            List<BoardUploadFile> uploadFiles,
            long actorUserId,
            boolean moderator,
            String actorLoginId,
            String actorUserIdText
    ) {
        return boardPostService.addAttachments(postId, uploadFiles, actorUserId, moderator, actorLoginId, actorUserIdText);
    }

    @Transactional
    public BoardAttachmentView replaceAttachment(
            long attachmentId,
            BoardUploadFile uploadFile,
            long actorUserId,
            boolean moderator
    ) {
        return boardPostService.replaceAttachment(attachmentId, uploadFile, actorUserId, moderator);
    }

    @Transactional
    public void deleteAttachment(long attachmentId, long actorUserId, boolean moderator) {
        boardPostService.deleteAttachment(attachmentId, actorUserId, moderator);
    }

    @Transactional(readOnly = true)
    public BoardAttachmentDownload downloadAttachment(long attachmentId) {
        return boardPostService.downloadAttachment(attachmentId);
    }

    @Transactional(readOnly = true)
    public List<DashboardWidgetView> dashboardWidgets(int limit) {
        return boardPostService.dashboardWidgets(limit);
    }

    public static BoardType parseBoardType(String boardType) {
        try {
            return BoardType.valueOf(boardType.toUpperCase(Locale.ROOT));
        } catch (IllegalArgumentException ex) {
            throw new IllegalArgumentException("지원하지 않는 게시판 유형입니다: " + boardType);
        }
    }
}
