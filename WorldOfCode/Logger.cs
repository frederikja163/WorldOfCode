using System;
using System.Diagnostics;
using System.IO;

namespace WorldOfCode
{
    /// <summary>
    /// The severity of a log message
    /// </summary>
    public enum LogSeverity
    {
        Message = 0, //Just a message to display. Use for debugging
        Warning, //Something might go wrong if not careful
        Error,   //Something has gone wrong but the program is able to continue
        FatalError //Something has gone wrong and the program can not continue
    }
    
    /// <summary>
    /// Logs everything in the application
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// The time at the start of the application
        /// </summary>
        private static DateTime _startTime;

        /// <summary>
        /// False before initialisation, true afterwards
        /// Makes sure the logger is only initialized once
        /// </summary>
        private static bool _isInitialized = false;

        /// <summary>
        /// The level that is logged
        /// </summary>
        private static LogSeverity _logLevel;

        /// <summary>
        /// The directory the application has been run from
        /// </summary>
        private static string _runDirectory;
        
        
        /// <summary>
        /// Initialize the logger and make it ready to log information for us
        /// Called once in the entrypoint of the program and no more
        /// </summary>
        public static void Init(LogSeverity logLevel)
        {
            //If the logger has been initialized it shouldn't initialize again
            if (_isInitialized) { return; }
            
            //Initialize variables
            _startTime = DateTime.Now;
            _logLevel = logLevel;
            _runDirectory = Directory.GetCurrentDirectory().Replace("/bin/Debug","");
            
            //Log the layout of the log messages
            #if DEBUG
            RawLog("[Time since start] - File name [Line number] Method\t\t\tmessage");
            #else
            RawLog("[Time since start] - Method\t\t\tmessage");
            #endif
            Log(LogSeverity.Message, "Logger Initialized");
        }

        #region Logging
        /// <summary>
        /// Log a message with message severity
        /// </summary>
        /// <param name="msg">message to log</param>
        public static void Msg(string msg)
        {
            Log(LogSeverity.Message, msg);
        }
        /// <summary>
        /// Log a message with warning severity
        /// </summary>
        /// <param name="msg">message to log</param>
        public static void Warn(string msg)
        {
            Log(LogSeverity.Warning, msg);
        }
        /// <summary>
        /// Log a message with error severity
        /// </summary>
        /// <param name="msg">message to log</param>
        public static void Error(string msg)
        {
            Log(LogSeverity.Error, msg);
        }
        /// <summary>
        /// Log a message with fatal error severity
        /// </summary>
        /// <param name="msg">message to log</param>
        public static void FatalError(string msg)
        {
            Log(LogSeverity.FatalError, msg);
        }
        
        /// <summary>
        /// Logs a message to the log with a specified layout
        /// </summary>
        /// <param name="logSeverity">The severity of the message</param>
        /// <param name="message">The message to log</param>
        public static void Log(LogSeverity logSeverity, string message)
        {
            //Check if the message is servere enough to be logged
            if (_logLevel > logSeverity) { return; }
            
            //Get the calling method
            StackFrame caller = new StackFrame(1, true);
            
            //Create/define the layout of the message
            #if DEBUG
            string msg = $"[{(DateTime.Now - _startTime):c}] - {caller.GetFileName()?.Replace(_runDirectory, "")} [{caller.GetFileLineNumber()}] {caller.GetMethod().Name}\t\t\t{message}";
            #else
            string msg = $"[{(DateTime.Now - _startTime):c}] - {caller.GetMethod().Name}\t\t\t{message}";
            #endif
            
            //Log the message where we want it to appear
            RawLog(msg);
        }

        /// <summary>
        /// Logs the message as provided without formatting the layout
        /// </summary>
        /// <param name="rawMessage">The message to log as provided</param>
        private static void RawLog(string rawMessage)
        {
            Console.WriteLine(rawMessage);
        }
        #endregion Logging
    }
}
