using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Triggerless.TriggerBot.Forms
{
    public partial class SearchTagsForm : Form
    {
        bool _muteCheck = false;

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
                        new ProductTagEventArgs(tag)
                    );
                }
                break;

                case CheckState.Unchecked:
                if (SearchTagDeleted != null)
                {
                    SearchTagDeleted.Invoke(
                        this,
                        new ProductTagEventArgs(tag)
                    );
                }
                break;
            }
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvTags.Items)
            {
                if (lvi.Checked)
                {
                    lvi.Checked = false;
                }
            }
        }
    }
}
