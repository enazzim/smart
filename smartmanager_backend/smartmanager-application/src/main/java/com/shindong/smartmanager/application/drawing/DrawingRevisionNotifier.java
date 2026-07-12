package com.shindong.smartmanager.application.drawing;

public interface DrawingRevisionNotifier {

    void notifyMajorRevision(String partNo, int newMajorVersion);
}
