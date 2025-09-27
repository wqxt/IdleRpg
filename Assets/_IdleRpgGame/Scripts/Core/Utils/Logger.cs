using System.Runtime.CompilerServices;
using UnityEngine;

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
            string log = $"[{className}.{memberName}] {message}";
            Debug.Log(log);
        }
    }
}