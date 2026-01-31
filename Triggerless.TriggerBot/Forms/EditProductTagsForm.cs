using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Triggerless.TriggerBot.Forms
{
    public partial class EditProductTagsForm : Form
    {
        bool _muteCheck = false;
        public EditProductTagsForm()
        {
            InitializeComponent();
        }

        private void PopulateTags()
        {
            if (_productDisplayInfo == null) {
                StyledMessageBox.Show(this, "Product Info not supplied", "Coding Error");
                return;
            }
            var tags = SQLiteDataAccess.TagsGetAll();
            lvTags.Items.Clear();
            _muteCheck = true;
            foreach (var tag in tags) 
            {
                SQLiteDataAccess.MuteCheck();
                var lvi = new ListViewItem(tag.Name);
                var hasTag = _productDisplayInfo.Tags.Any(pt => pt.Id == tag.Id);
                lvi.Checked = hasTag;
                lvi.Tag = tag.Id;
                lvTags.Items.Add(lvi);            
            }
            SQLiteDataAccess.UnMuteCheck();
            _muteCheck = false;
        }

        private ProductDisplayInfo _productDisplayInfo;
        public ProductDisplayInfo ProductDiscplayInfo
        {
            get { return _productDisplayInfo; }
            set
            {
                if (value != null)
                {
                    lblName.Text = value.Name;
                    lblCreator.Text = "by " + value.Creator;
                    if (value.ImageBytes != null && value.ImageBytes.Length > 10)
                    {
                        Image bmp = null;
                        var converter = new ImageConverter();
                        if (converter.CanConvertFrom(typeof(byte[])))
                        {
                            bmp = (Bitmap)((new ImageConverter()).ConvertFrom(value.ImageBytes));
                            picProductImage.Image = bmp;
                        }
                    }
                }
                _productDisplayInfo = value;
            }
        }

        private void EditProductTagsForm_Shown(object sender, EventArgs e)
        {
            PopulateTags();
        }

        private void CheckChanging(object sender, ItemCheckEventArgs e)
        {
            if (_muteCheck) return;
            var item = lvTags.Items[e.Index];
            var tid = (int)item.Tag;
            bool assigning = e.NewValue == CheckState.Checked;
            var pid = _productDisplayInfo.Id;

            var worked = SQLiteDataAccess.TagAssignToProduct(pid, tid, assigning);
            if (!worked)
            {
                e.NewValue = e.CurrentValue;
            }
            else
            {
                if (assigning)
                {
                    var tag = SQLiteDataAccess.TagGetById(tid);
                    if (tag != null) _productDisplayInfo.Tags.Add(tag);
                }
                else
                {
                    var tag = _productDisplayInfo.Tags.First(pd => pd.Id == tid);
                    if (tag != null) _productDisplayInfo.Tags.Remove(tag);
                }
            }
        }

        private void btnNewTag_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewTag.Text)) return;
            var name = txtNewTag.Text.Trim().Replace('|', ' ');
            ProductTag tag = SQLiteDataAccess.TagCreateNew(name);
            if (tag == null) return;
            _productDisplayInfo.Tags.Add(tag);
            SQLiteDataAccess.TagAssignToProduct(_productDisplayInfo.Id, tag.Id, true);
            PopulateTags();
            if (ProductTagAdded != null)
            {
                ProductTagAdded.Invoke(this,
                    new ProductTagEventArgs(tag)
                );
            }

        }

        private void deleteTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvTags.SelectedItems.Count == 0)
                return;

            var item = lvTags.SelectedItems[0];

            var tagId = (int)item.Tag;   // if you stored your ID in Tag
            var tagName = item.Text;      // first column

            var msg = "Are you sure you want to delete this tag? It will be removed from all other products if you do. Continue?";
            var caption = "Permanently Delete Tag";
            var icon = MessageBoxIcon.Warning;
            var dr = StyledMessageBox.Show(this, msg, caption, MessageBoxButtons.OKCancel, icon, MessageBoxDefaultButton.Button2);
            if (dr != DialogResult.OK) return;

            SQLiteDataAccess.TagDelete(tagId);
            PopulateTags();
            if (ProductTagDeleted != null)
            {
                ProductTagDeleted.Invoke(this, 
                    new ProductTagEventArgs(new ProductTag { Id = tagId, Name = tagName })
                );
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lvTags_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = lvTags.HitTest(e.Location);

                if (hit.Item != null)
                {
                    // Select the item under the mouse
                    lvTags.SelectedItems.Clear();
                    hit.Item.Selected = true;
                }
            }
        }

        public event ProductTagEventHandler ProductTagDeleted;
        public event ProductTagEventHandler ProductTagAdded;
    }

    public class ProductTagEventArgs: EventArgs
    {
        private ProductTag _tag;

        public ProductTag Tag { 
            get => _tag; 
            private set => _tag = value;
        }

        public ProductTagEventArgs(ProductTag tag)
        {
            _tag = tag;
        }
    }

    public delegate void ProductTagEventHandler (object sender, ProductTagEventArgs e);
}
