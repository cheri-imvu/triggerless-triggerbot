using System;
using System.Windows.Forms;

namespace Triggerless.PlugIn
{
    public interface IPlugIn: IDisposable
    {
        bool CanPlugIn();
        void OnPlugIn(IPlugInContext context);
        void OnUnplug();
        string Title { get; }
        event PlugInEventHandler PlugInEvent;
    }

    public interface IPlugInContext
    {
        Control Parent { get; set; }
        UserControl Control { get; set; }
    }

    public class PlugInFactory
    {
        public static IPlugIn CreateInstance(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            object obj = Activator.CreateInstance(type);
            return obj as IPlugIn;
        } 
    }

    public class PlugInContext: IPlugInContext
    {
        private UserControl _userControl;
        public Control Parent { get; set; }
        public UserControl Control { 
            get => _userControl; 
            set => _userControl = value; 
        }
    }

    public class PlugInEventArgs: EventArgs
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
