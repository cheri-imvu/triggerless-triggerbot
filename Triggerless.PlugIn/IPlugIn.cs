using System;
using System.Windows.Forms;

namespace Triggerless.PlugIn
{
    public interface IPlugIn: IDisposable
    {
        bool CanPlugIn { get; }
        bool ActivateByDefault {  get; }
        void OnPlugIn(IPlugInContext context);
        void OnUnplug();
        string Title { get; }
        event PlugInEventHandler PlugInEvent;
    }

    public interface IPlugInContext
    {
        Control Parent { get; set; }
        UserControl Control { get; set; }
        bool SendTrigger(string trigger);
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
}
