using System;
using System.Windows.Forms;
using Triggerless.PlugIn;

namespace Triggerless.ChatViewerPlugIn
{
    public class ChatViewerPlugIn : IPlugIn
    {
        private IPlugInContext _context;

        public string Title { 
            get => "Chat Viewer"; 
        }

        public event PlugInEventHandler PlugInEvent;

        public void RaiseEvent(int id, PlugInEventResult result, string message, Exception exception)
        {
            if (PlugInEvent == null) return;
            PlugInEvent.Invoke(this, new PlugInEventArgs(id, result, message, exception));
        }

        public bool CanPlugIn()
        {
            return true;
        }

        public void Dispose()
        {
            _context?.Control?.Dispose();
        }

        public void OnPlugIn(IPlugInContext context)
        {

            if (object.ReferenceEquals(context, null))
            {
                RaiseEvent(99, PlugInEventResult.Error, "Null context is not allowed.",
                    new ArgumentNullException(nameof(context)));
                return;
            }
            _context = context;
            if (context.Parent == null)
            {
                RaiseEvent(99, PlugInEventResult.Error, "Null Parent is not allowed.",
                    new ArgumentNullException(nameof(context.Parent)));
                return;
            }
            _context.Control = new ChatViewer()
            {
                Name = "ChatViewer1",
                Parent = _context.Parent,
                Dock = DockStyle.Fill,
            };
            RaiseEvent(99, PlugInEventResult.PluggedIn, "Successfully plugged in", null);
        }

        public void OnUnplug()
        {
            Dispose();
            _context.Control = null;
            _context = null;
        }
    }
}
