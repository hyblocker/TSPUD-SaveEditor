using System;
using System.Linq;
using System.Windows.Forms;

namespace StanleySaveEditor
{
    public partial class MainWindow : Form
    {
        private StanleyParableSaveContainer currentSave;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(null, null);
        }

        private void jsonTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var currentObject = jsonTree.SelectedNode.Tag;
            propsView.SelectedObject = currentObject;
        }

        #region TreeNode Gen

        private TreeNode BuildMiscTreeNode(FBPPFileModel.StringItem[] prop, TreeNode node)
        {
            if (prop.Length == 0) return null;

            TreeNode treeNode = new TreeNode("StringData");

            if (node != null)
                node.Nodes.Add(treeNode);

            for (int i = 0; i < prop.Length; i++)
            {
                // Hide the data key
                if (prop[i].Key == "data") continue;
                TreeNode currentProp = new TreeNode(prop[i].Key);
                currentProp.Tag = prop[i];
                treeNode.Nodes.Add(currentProp);
            }

            return treeNode;
        }

        private TreeNode BuildMiscTreeNode(FBPPFileModel.IntItem[] prop, TreeNode node)
        {
            if (prop.Length == 0) return null;

            TreeNode treeNode = new TreeNode("IntData");

            if (node != null)
                node.Nodes.Add(treeNode);

            for (int i = 0; i < prop.Length; i++)
            {
                TreeNode currentProp = new TreeNode(prop[i].Key);
                currentProp.Tag = prop[i];
                treeNode.Nodes.Add(currentProp);
            }

            return treeNode;
        }

        private TreeNode BuildMiscTreeNode(FBPPFileModel.FloatItem[] prop, TreeNode node)
        {
            if (prop.Length == 0) return null;

            TreeNode treeNode = new TreeNode("FloatData");

            if (node != null)
                node.Nodes.Add(treeNode);

            for (int i = 0; i < prop.Length; i++)
            {
                TreeNode currentProp = new TreeNode(prop[i].Key);
                currentProp.Tag = prop[i];
                treeNode.Nodes.Add(currentProp);
            }

            return treeNode;
        }

        private TreeNode BuildMiscTreeNode(FBPPFileModel.BoolItem[] prop, TreeNode node)
        {
            if (prop.Length == 0) return null;

            TreeNode treeNode = new TreeNode("BoolData");

            if (node != null)
                node.Nodes.Add(treeNode);

            for (int i = 0; i < prop.Length; i++)
            {
                TreeNode currentProp = new TreeNode(prop[i].Key);
                currentProp.Tag = prop[i];
                treeNode.Nodes.Add(currentProp);
            }

            return treeNode;
        }
        
        private TreeNode BuildCacheTreeNode(StanleyParableSaveCacheEntry[] saveDataCache, TreeNode node)
        {
            if (saveDataCache.Length == 0) return null;

            TreeNode treeNode = new TreeNode("Saved Data");

            if (node != null)
                node.Nodes.Add(treeNode);

            for (int i = 0; i < saveDataCache.Length; i++)
            {
                TreeNode currentProp = new TreeNode(saveDataCache[i].key);
                currentProp.Tag = saveDataCache[i];
                treeNode.Nodes.Add(currentProp);
            }

            return treeNode;
        }
        #endregion

        #region Menu stuff
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentSave = StanleyParableSaveManager.LoadSaveToMemory();

            jsonTree.Nodes.Clear();
            TreeNode treenodeBuffer = BuildMiscTreeNode(currentSave.saveDataMisc.StringData, null);
            if (treenodeBuffer != null)
                jsonTree.Nodes.Add(treenodeBuffer);

            treenodeBuffer = BuildMiscTreeNode(currentSave.saveDataMisc.IntData, null);
            if (treenodeBuffer != null)
                jsonTree.Nodes.Add(treenodeBuffer);

            treenodeBuffer = BuildMiscTreeNode(currentSave.saveDataMisc.FloatData, null);
            if (treenodeBuffer != null)
                jsonTree.Nodes.Add(treenodeBuffer);

            treenodeBuffer = BuildMiscTreeNode(currentSave.saveDataMisc.BoolData, null);
            if (treenodeBuffer != null)
                jsonTree.Nodes.Add(treenodeBuffer);

            treenodeBuffer = BuildCacheTreeNode(currentSave.saveData.saveDataCache, null);
            if (treenodeBuffer != null)
                jsonTree.Nodes.Add(treenodeBuffer);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                currentSave.Save();
                MessageBox.Show("Thanks to the power of the Mind Control Facility, we have re-engineered your state of mind, and now you will experience \"the game\" in an entirely new way!", "Attention Employee 427");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "An unexpected error has occured!");
            }
            
        }

        private void aboutToolStripMenuItem_Click (object sender, EventArgs e)
        {
            MessageBox.Show (
                "This program was developed by Hyblocker.\nThe Stanley Parable: Ultra Deluxe is Copyright © Crows Crows Crows 2022",
                string.Concat ( Enumerable.Repeat ( "THE CREDITS NEVER END ", 24 ) ) );
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.R:
                case Keys.O:
                    if (e.Control)
                        openToolStripMenuItem_Click(null, null);
                    break;
                case Keys.F5:
                    openToolStripMenuItem_Click(null, null);
                    break;
                case Keys.S:
                    if (e.Control)
                        saveToolStripMenuItem_Click(null, null);
                    break;
                case Keys.F1:
                    aboutToolStripMenuItem_Click(null, null);
                    break;
            }
        }

        private void splitContainer1_KeyDown(object sender, KeyEventArgs e) => MainWindow_KeyDown(sender, e);
        private void jsonTree_KeyDown(object sender, KeyEventArgs e) => MainWindow_KeyDown(sender, e);
        #endregion
    }
}
