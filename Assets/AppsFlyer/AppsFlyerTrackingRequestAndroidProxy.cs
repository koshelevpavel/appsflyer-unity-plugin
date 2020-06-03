using System;
using System.Collections.Generic;
using UnityEngine;

namespace AppsFlyerSDK
{
    public class AppsFlyerTrackingRequestAndroidProxy : AndroidJavaProxy
    {
        private struct Callbacks
        {
            public Action onSuccess;
            public Action<string> onFailure;
        }
        
        private readonly Dictionary<int, Callbacks> _callbackMap = new Dictionary<int, Callbacks>(8);
        public AppsFlyerTrackingRequestAndroidProxy() : base("com.appsflyer.unity.ITrackingRequestUnityHandler")
        {
        }

        public int RegisterCallback(Action onSuccess, Action<string> onFailure)
        {
            int id = onSuccess?.GetHashCode() ?? onFailure.GetHashCode();
            _callbackMap.Add(id, new Callbacks{onSuccess = onSuccess, onFailure = onFailure});
            return id;
        }

        public void onTrackingRequestSuccess(int callbackId)
        {
            if (_callbackMap.TryGetValue(callbackId, out Callbacks value))
            {
                value.onSuccess?.Invoke();
                _callbackMap.Remove(callbackId);
            }
        }

        public void onTrackingRequestFailure(int callbackId, string message)
        {
            if (_callbackMap.TryGetValue(callbackId, out Callbacks value))
            {
                value.onFailure?.Invoke(message);
                _callbackMap.Remove(callbackId);
            }
        }
    }
}