using System;
using InventoryApp.Data;
using System.Windows.Forms;
using System.Data;

namespace InventoryApp
{
    public partial class AddStock : Form
    {
        private readonly ProductManager _productManager = new ProductManager();
        private readonly HistoryManager _historyManager = new HistoryManager();

        readonly private string itemName;

        public AddStock(string name)
        {
            InitializeComponent();
            itemName = name;
            label3.Text = name;
        }

        //INSERT STOCK BUTTON
        private void button1_Click(object sender, EventArgs e)
        {
            int currentStock = 0;
            int productId = 0;
            var productIdByName = _productManager.SelectProductsByName(itemName);
            var currentStockData = _productManager.SelectProductsById(productId);
            int addedStocks = Convert.ToInt32(textBox2.Text);

            if (productIdByName != null || productIdByName.Rows.Count != 0)
            {
                foreach (DataRow row in productIdByName.Rows)
                {
                    productId = Convert.ToInt32(row["Id"]);
                }
            }

            if (currentStockData != null || currentStockData.Rows.Count != 0)
            {
                foreach (DataRow row in currentStockData.Rows)
                {
                    currentStock = Convert.ToInt32(row["Stock"]);
                }
            }

            _productManager.UpdateProductByStock(productId, currentStock + addedStocks);
            _historyManager.InsertHistory(new Models.History { ProductID = productId, AddedStocks = addedStocks });

            DialogResult = DialogResult.OK;
        }

        //CANCEL BUTTON
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
