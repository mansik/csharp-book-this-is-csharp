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
            // 운영체제에 설치되어 있는 폰트 목록 검색
            var Fonts = FontFamily.Families;

            // cboFont에 각 폰트 이름 추가
            foreach (FontFamily font in Fonts)
                cboFont.Items.Add(font.Name);
        }

        void ChangeFont()
        {
            // cboFont에서 선택한 항목이 없으면 메소드 종료
            if (cboFont.SelectedIndex < 0)
                return;

            // FontStyle 객체를 초기화
            FontStyle style = FontStyle.Regular;

            // Bold 체크 박스가 선택되어 있으면 Bold 논리합 수행
            if (chkBold.Checked)
                style |= FontStyle.Bold;

            // Italic 체크 박스가 선택되어 있으면 Italic 논리합 수행
            if (chkItalic.Checked)
                style |= FontStyle.Italic;

            // txtSampleText의 Font 프로퍼티를 앞에서 만든 style로 수정
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
            frm.ShowDialog(); // Modal 창을 띄웁니다.
        }

        private void btnModaless_Click(object sender, EventArgs e)
        {
            Form frm = new Form();
            frm.Text = "Modaless Form";
            frm.Width = 300;
            frm.Height = 100;
            frm.BackColor = Color.Green;
            frm.Show(); // Modaless 창을 띄웁니다.
        }

        private void btnMsgBox_Click(object sender, EventArgs e)
        {
            MessageBox.Show(txtSampleText.Text,
                "MessageBox Test", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        // TreeView의 각 노드를 ListView로 옮겨 표시하는 기능
        void TreeToList()
        {
            lvDummy.Items.Clear();
            foreach (TreeNode node in tvDummy.Nodes) // 1번째 노드들에 대한 작업
                TreeToList(node);
        }

        void TreeToList(TreeNode Node)
        {

            lvDummy.Items.Add(
                new ListViewItem(
                    new string[] { Node.Text,
                        Node.FullPath.Count(f => f == '\\').ToString() }));
            // TreeNode 형식의 FullPath 프로퍼티는 루트 노드부터 현재 노드까지의 경로를 나타내며,
            // 각 경로는 \로 구분한다.

            foreach (TreeNode node in Node.Nodes) // 자식 노드들에 대한 작업
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
                MessageBox.Show("선택된 노드가 없습니다.",
                    "TreeView Test", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                return;
            }
            tvDummy.SelectedNode.Nodes.Add(random.Next().ToString());
            tvDummy.SelectedNode.Expand();
            TreeToList();
        }
    }
}