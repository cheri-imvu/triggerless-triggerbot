using System;
using System.Windows.Forms;

namespace Triggerless.PlugIn
{
    public class PlugInEventArgs : EventArgs
    {
        public int Id { get; private set; }
        public PlugInEventResult Result { get; private set; }
        public string Message { get; private set; }
        public Exception Exception { get; private set; }

        public PlugInEventArgs(int id, PlugInEventResult result, string message, Exception exception)
        {
            Id = id;
            Result = result;
            Message = message;
            Exception = exception;
        }
    }
    public delegate void PlugInEventHandler(IPlugIn sender, PlugInEventArgs e);

    public enum PlugInEventResult
    {
        None = 0,
        PluggedIn,
        Success,
        Warning,
        Error,
        Unplugging,
    }
}