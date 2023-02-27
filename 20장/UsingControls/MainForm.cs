using System.Diagnostics;

namespace UsingControls
{
    public partial class MainForm : Form
    {
        Random random = new Random(37);

        public MainForm()
        {
            InitializeComponent();

            lvDummy.View = View.Details;
            lvDummy.Columns.Add("Name");
            lvDummy.Columns.Add("Depth");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // �ü���� ��ġ�Ǿ� �ִ� ��Ʈ ��� �˻�
            var Fonts = FontFamily.Families;

            // cboFont�� �� ��Ʈ �̸� �߰�
            foreach (FontFamily font in Fonts)
                cboFont.Items.Add(font.Name);
        }

        void ChangeFont()
        {
            // cboFont���� ������ �׸��� ������ �޼ҵ� ����
            if (cboFont.SelectedIndex < 0)
                return;

            // FontStyle ��ü�� �ʱ�ȭ
            FontStyle style = FontStyle.Regular;

            // Bold üũ �ڽ��� ���õǾ� ������ Bold ���� ����
            if (chkBold.Checked)
                style |= FontStyle.Bold;

            // Italic üũ �ڽ��� ���õǾ� ������ Italic ���� ����
            if (chkItalic.Checked)
                style |= FontStyle.Italic;

            // txtSampleText�� Font ������Ƽ�� �տ��� ���� style�� ����
            txtSampleText.Font = new Font((string)cboFont.SelectedItem, 10, style);
        }

        private void cboFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeFont();
        }

        private void chkBold_CheckedChanged(object sender, EventArgs e)
        {
            ChangeFont();
        }

        private void chkItalic_CheckedChanged(object sender, EventArgs e)
        {
            ChangeFont();
        }

        private void tbDummy_Scroll(object sender, EventArgs e)
        {
            pgDummy.Value = tbDummy.Value;
        }

        private void btnModal_Click(object sender, EventArgs e)
        {
            Form frm = new Form();
            frm.Text = "Modal Form";
            frm.Width = 300;
            frm.Height = 100;
            frm.BackColor = Color.Red;
            frm.ShowDialog(); // Modal â�� ���ϴ�.
        }

        private void btnModaless_Click(object sender, EventArgs e)
        {
            Form frm = new Form();
            frm.Text = "Modaless Form";
            frm.Width = 300;
            frm.Height = 100;
            frm.BackColor = Color.Green;
            frm.Show(); // Modaless â�� ���ϴ�.
        }

        private void btnMsgBox_Click(object sender, EventArgs e)
        {
            MessageBox.Show(txtSampleText.Text,
                "MessageBox Test", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        // TreeView�� �� ��带 ListView�� �Ű� ǥ���ϴ� ���
        void TreeToList()
        {
            lvDummy.Items.Clear();
            foreach (TreeNode node in tvDummy.Nodes) // 1��° ���鿡 ���� �۾�
                TreeToList(node);
        }

        void TreeToList(TreeNode Node)
        {

            lvDummy.Items.Add(
                new ListViewItem(
                    new string[] { Node.Text,
                        Node.FullPath.Count(f => f == '\\').ToString() }));
            // TreeNode ������ FullPath ������Ƽ�� ��Ʈ ������ ���� �������� ��θ� ��Ÿ����,
            // �� ��δ� \�� �����Ѵ�.

            foreach (TreeNode node in Node.Nodes) // �ڽ� ���鿡 ���� �۾�
                TreeToList(node);
        }

        private void btnAddRoot_Click(object sender, EventArgs e)
        {
            tvDummy.Nodes.Add(random.Next().ToString());
            TreeToList();
        }

        private void btnAddChild_Click(object sender, EventArgs e)
        {
            if (tvDummy.SelectedNode == null)
            { 
                MessageBox.Show("���õ� ��尡 �����ϴ�.",
                    "TreeView Test", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                return;
            }
            tvDummy.SelectedNode.Nodes.Add(random.Next().ToString());
            tvDummy.SelectedNode.Expand();
            TreeToList();
        }
    }
}