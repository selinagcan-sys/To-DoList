namespace To_DoList
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadTasks(); // uygulama açılırken görevleri dosyadan okur
        }
        private void LoadTasks()
        {
            listBox1.Items.Clear();
            if (File.Exists("todo.txt"))
            {
                string[] lines = File.ReadAllLines("todo.txt"); // dosadaki tüm satırları okur
                foreach (string line in lines)
                {
                    listBox1.Items.Add(line); // her satırı listeye ekler
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text)) return; // boş girişleri engeler

            listBox1.Items.Add("⬜ " + textBox1.Text);
            File.AppendAllText("todo.txt", "⬜ " + textBox1.Text + Environment.NewLine);

            textBox1.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return; // görev seçilmemişse işlem yapmaz

            var lines = File.ReadAllLines("todo.txt").ToList(); // dosyadaki tüm satırları listeye alır
            lines.Remove(listBox1.SelectedItem.ToString());  // seçili satırı bulup siler
            File.WriteAllLines("todo.txt", lines); // dosyayı yeniden yazar

            LoadTasks();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return; // görev seçilmemişse işlem yapmaz

            string selected = listBox1.SelectedItem.ToString(); 
            if (selected.StartsWith("⬜"))
                selected = selected.Replace("⬜", "✅");
            else if (selected.StartsWith("✅"))
                selected = selected.Replace("✅", "⬜");

            var lines = File.ReadAllLines("todo.txt").ToList(); // dosyadaki satırları okur
            int index = lines.IndexOf(listBox1.SelectedItem.ToString()); // seçili satırın dosyadaki yerini bulur
            if (index >= 0)
            {
                lines[index] = selected; // satırı günceller
                File.WriteAllLines("todo.txt", lines);
            }

            LoadTasks();

        }
    }
}
