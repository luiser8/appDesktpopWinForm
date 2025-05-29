using InventoryApp.Data;
using System.Windows.Forms;

namespace InventoryApp.Views.Dashboard
{
    public partial class Dashboard : Form
    {
        private readonly ProductManager _productManager = new ProductManager();
        private readonly CategoryManager _categoryManager = new CategoryManager();
        private readonly TransactionManager _transactionManager = new TransactionManager();
        private readonly int _productCount = 0;
        private readonly int _categoryCount = 0;
        private readonly int _transactionCount = 0;

        public Dashboard()
        {
            InitializeComponent();
            _productCount = _productManager.SelectProductsAll().Rows.Count;
            _categoryCount = _categoryManager.GetCategories().Rows.Count;
            _transactionCount = _transactionManager.SelectTransactionsAll(1).Rows.Count;

            label3.Text = _productCount.ToString();
            label5.Text = _categoryCount.ToString();
            label7.Text = _transactionCount.ToString();
        }

        private void Dashboard_Load(object sender, System.EventArgs e)
        {

        }
    }
}
