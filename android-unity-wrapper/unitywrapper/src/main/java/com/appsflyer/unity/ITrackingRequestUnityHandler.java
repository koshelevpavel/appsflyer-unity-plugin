package com.appsflyer.unity;

public interface ITrackingRequestUnityHandler {
    void onTrackingRequestSuccess(int callbackId);
    void onTrackingRequestFailure(int callbackId, String message);
}
