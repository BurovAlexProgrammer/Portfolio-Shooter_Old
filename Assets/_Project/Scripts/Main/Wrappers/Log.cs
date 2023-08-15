using System;
using UnityEngine;

namespace sm_application.Scripts.Main.Wrappers
{
    public static class Log
    {
        public static void ErrorUnknown()
        {
            Debug.LogError("Unknown error");
        }
        
        public static void Error(string message, UnityEngine.Object context = null, Type type = Type.Default)
        {
            if (context == null)
            {
                Debug.LogError(message);
            }
            else
            {
                Debug.LogError(message, context);
            }
        }
        
        public static void Info(string message, UnityEngine.Object context = null, Type type = Type.Default)
        {
            if (context == null)
            {
                Debug.Log(message);
            }
            else
            {
                Debug.Log(message, context);
            }
        }        
        
        public static void Warn(string message, UnityEngine.Object context = null, Type type = Type.Default)
        {
            if (context == null)
            {
                Debug.LogWarning(message);
            }
            else
            {
                Debug.LogWarning(message, context);
            }
        }

        public static void Exception(Exception exception)
        {
            Debug.LogException(exception);
        }
        
        public enum Type
        {
            Default,
            Service,
        }
    }
}