using DocumentFormat.OpenXml.Office2010.Excel;
using InventoryApp.Data;
using System.Windows.Forms;

namespace InventoryApp.InventoryApp.Views
{
    public partial class CategoryView : Form
    {
        private readonly CategoryManager categoryManager = new CategoryManager();
        public CategoryView()
        {
            InitializeComponent();
            dataGridView1.DataSource = categoryManager.GetCategories();
            SetDatGridViewColumns();
        }

        private void SetDatGridViewColumns()
        {
            dataGridView1.Columns["CategoryItem"].HeaderText = "Nombre";
            dataGridView1.Columns["StatusString"].HeaderText = "Estado";
            dataGridView1.Columns["CreatedAt"].HeaderText = "Creacion";
        }

        //ADD BUTTON - Category
        private void button1_Click(object sender, System.EventArgs e)
        {
            CartDialog dlg = new CartDialog(categoryManager);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.DataSource = categoryManager.GetCategories();
            }
        }

        //UPDATE BUTTON - Category
        private void button2_Click(object sender, System.EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the data from the selected row
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                var category = new Models.Category { Id = (int)row.Cells["Id"].Value, CategoryItem = row.Cells["CategoryItem"].Value.ToString(), Status = row.Cells["StatusString"].Value.ToString() == "Activo" };

                // Pass the data to EditCat
                CartDialog dlg = new CartDialog(categoryManager, category);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dataGridView1.DataSource = categoryManager.GetCategories();
                }
            }
            else
            {
                MessageBox.Show("No category is available for editing.", "Empty Category",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //DELETE BUTTON - Category
        private void button3_Click(object sender, System.EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = (int)dataGridView1.SelectedRows[0].Cells["ID"].Value;

                if (MessageBox.Show("Are you sure want to delete this category?", "Warning!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    categoryManager.DeleteCategory(id);
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                }
            }
            else
            {
                MessageBox.Show("Please select a category to delete.", "Empty Category",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CategoryView_Load(object sender, System.EventArgs e)
        {

        }
    }
}
