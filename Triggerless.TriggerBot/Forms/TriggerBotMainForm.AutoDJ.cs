using Triggerless.TriggerBot.Models;
using static Triggerless.TriggerBot.ProductCtrl;

namespace Triggerless.TriggerBot
{
    public partial class TriggerBotMainForm
    {
        private LiveList<ProductDisplayInfo> _autoDJList;

        private void InitializeAutoDJ()
        {
            _autoDJList = new LiveList<ProductDisplayInfo>();
            _autoDJList.FrontChanged += LiveListFrontChanged;
            autoDJCtrl.LiveList = _autoDJList;
        }

        private void AttachToProductCtrl(ProductCtrl pc)
        {
            pc.OnAddToAutoDJ += AddToAutoDJ;
        }

        private void LiveListFrontChanged(object sender, FrontChangedEventArgs<ProductDisplayInfo> e)
        {
            if (_autoDJList == null || !_autoDJList.IsLive || e.Current is null)
            {
                return;
            }
            productOnDeck.ProductInfo = e.Current;
        }

        private void AddToAutoDJ(object sender, AddToAutoDJEventArgs e)
        {
            if (_autoDJList is null || e.Product is null || e.Product.Id < 1)
            {
                e.Cancel = true;
                return;
            }
            if (e.Position == AddToAutoDJPosition.BackOfQueue)
            {
                _autoDJList.Push(e.Product);
            }
            else
            {
                _autoDJList.Unshift(e.Product);
            }
        }

    }
}
