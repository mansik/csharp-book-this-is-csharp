namespace AsyncFileIOWinForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        // �񵿱� ���� ����
        private async Task<long> CopyAsync(string fromPath, string toPath)
        {
            btnAsyncCopy.Enabled = false;
            long totalCopied = 0;

            using (FileStream fromStream = new FileStream(fromPath, FileMode.Open))
            {
                using (FileStream toStream = new FileStream(toPath, FileMode.Create))
                {
                    byte[] butter = new byte[1024 * 1024];
                    int nRead = 0;
                    while ((nRead = await fromStream.ReadAsync(butter, 0, butter.Length)) != 0)
                    {
                        await toStream.WriteAsync(butter, 0, nRead);
                        totalCopied += nRead;

                        // ���α׷����ٿ� ���� ���� ���� ���� ǥ�� , �Ҽ�(double)�� �Ǿ�� "/" �����ڰ� �Ҽ��� ǥ����
                        pgCopy.Value = (int)(((double)totalCopied / (double)fromStream.Length) * pgCopy.Maximum);
                    }
                }
            }

            btnAsyncCopy.Enabled = true;
            return totalCopied;
        }

        private long CopySync(string fromPath, string toPath)
        {
            btnSyncCopy.Enabled = false;
            long totalCopied = 0;

            using (FileStream fromStream = new FileStream(fromPath, FileMode.Open))
            {
                using (FileStream toStream = new FileStream(toPath, FileMode.Create))
                {
                    byte[] buffer = new byte[1024 * 1024];
                    int nRead = 0;
                    while ((nRead = fromStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        toStream.Write(buffer, 0, nRead);
                        totalCopied += nRead;

                        // ���α׷����ٿ� ���� ���� ���� ���� ǥ��
                        pgCopy.Value = (int)(((double)totalCopied / (double)fromStream.Length) * pgCopy.Maximum);
                    }
                }
            }

            btnSyncCopy.Enabled = true;
            return totalCopied;
        }

        private void btnFindSource_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                txtSource.Text = dlg.FileName;
            }
        }

        private void btnFindTarget_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if ( dlg.ShowDialog() == DialogResult.OK )
            {
                txtTarget.Text = dlg.FileName;
            }
        }

        private async void btnAsyncCopy_Click(object sender, EventArgs e)
        {
            long totalCopied = await CopyAsync(txtSource.Text, txtTarget.Text);
        }

        private void btnSyncCopy_Click(object sender, EventArgs e)
        {
            long totalCopied = CopySync(txtSource.Text, txtTarget.Text);
        }

        // ASyncCopy�� �����߿��� Cancel ��ư Ŭ���Ǹ�, SyncCopy�� ���� �߿��� Ŭ�� �ȵȴ�. 
        private void btnCancel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("UI ���� �׽�Ʈ ����.");
        }
    }
}