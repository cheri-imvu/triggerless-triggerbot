using System.Collections.Specialized;
using System.Windows.Forms;
using Triggerless.TriggerBot.Models;

namespace Triggerless.TriggerBot.Components
{
    public partial class AutoDJCtrl : UserControl
    {
        public AutoDJCtrl()
        {
            InitializeComponent();
        }


        private LiveList<ProductDisplayInfo> _liveList;
        public LiveList<ProductDisplayInfo> LiveList {
            get => _liveList;
            set {
                _liveList = value;
                if (value != null)
                {
                    _liveList.FrontChanged += LiveList_FrontChanged;
                    _liveList.CollectionChanged += LiveList_CollectionChanged;
                    _liveList.IsLiveChanged += LiveList_IsLiveChanged;
                }
            }
        }

        private void LiveList_IsLiveChanged(object sender, IsLiveChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public bool IsLive
        {
            get => _liveList.IsLive;
            set => _liveList.IsLive = value;
        }

        private void LiveList_FrontChanged(object sender, FrontChangedEventArgs<ProductDisplayInfo> e)
        {
        }

        private void LiveList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }




    }
}
