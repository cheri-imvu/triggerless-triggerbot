using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Triggerless.TriggerBot.Forms
{
    public partial class SearchTagsForm : Form
    {
        private bool _muteCheck = false;
        private bool _update = true;

        public List<ProductTag> Tags { get; set; }

        public SearchTagsForm()
        {
            InitializeComponent();
        }

        private void SearchTagsForm_Shown(object sender, EventArgs e)
        {
            InitializeUI();
        }
        private void InitializeUI()
        {
            _muteCheck = true;
            var allTags = SQLiteDataAccess.TagsGetAll();
            foreach (var tag in allTags) 
            {
                var lvi = new ListViewItem(tag.Name);
                lvi.Tag = tag;
                var check = Tags.Any(t => t.Id == tag.Id);
                lvi.Checked = check;
                lvTags.Items.Add(lvi);
            }
            _muteCheck = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public event ProductTagEventHandler SearchTagDeleted;
        public event ProductTagEventHandler SearchTagAdded;

        private void lvTags_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_muteCheck) return;
            var tag = lvTags.Items[e.Index].Tag as ProductTag;
            switch (e.NewValue)
            {
                case CheckState.Checked:
                if (SearchTagAdded != null)
                {
                    SearchTagAdded.Invoke(
                        this,
                        new ProductTagEventArgs(tag, _update)
                    );
                }
                break;

                case CheckState.Unchecked:
                if (SearchTagDeleted != null)
                {
                    SearchTagDeleted.Invoke(
                        this,
                        new ProductTagEventArgs(tag, _update)
                    );
                }
                break;
            }
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            _update = false;

            while (lvTags.CheckedItems.Count > 0)
            {
                if (lvTags.CheckedItems.Count == 1)
                {
                    _update = true;   // BEFORE last uncheck
                }

                lvTags.CheckedItems[0].Checked = false;
            }
        }
    }
}
