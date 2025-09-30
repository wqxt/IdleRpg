using System.Runtime.CompilerServices;
using UnityEngine;
using System.IO;
using System;

namespace Assets._IdleRpgGame.Scripts.Core.Utils
{
    public enum LogLayer
    {
        Debug = 0,
        Warning = 1,
        Error = 2,
    }

    public static class Logger
    {
        private static string _dataPath;

        public static void InitLoggerSavePath()
        {
#if UNITY_EDITOR
            _dataPath = Application.dataPath;
#else
            _dataPath = Application.persistentDataPath;
#endif
        }

        public static void Log(string message, LogLayer logLayer, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "")
        {
            string className = System.IO.Path.GetFileNameWithoutExtension(filePath);
            string timestamp = System.DateTime.Now.ToString("HH:mm:ss");
            string log = $"[{timestamp}] [{className}.{memberName}] {message}";
            switch (logLayer)
            {
                case LogLayer.Debug:
                    Debug.Log(log);
                    SaveLog(log);
                    break;
                case LogLayer.Warning:
                    Debug.LogWarning(log);
                    SaveLog(log);
                    break;
                case LogLayer.Error:
                    Debug.LogError(log);
                    SaveLog(log);
                    break;
            }
        }

        private static void SaveLog(string message)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string logFilePath = Path.Combine(_dataPath, "Logs", $"game_log_{date}.txt ");
            string logDirectory = Path.GetDirectoryName(logFilePath);
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            // Записать в файл
            File.AppendAllText(logFilePath, message + Environment.NewLine);
        }
    }
}