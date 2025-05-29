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

        private readonly int _productId;
        private readonly string _itemName;

        public AddStock(int productId, string name)
        {
            InitializeComponent();
            _itemName = name;
            _productId = productId;
            label3.Text = name;
        }

        //INSERT STOCK BUTTON
        private void button1_Click(object sender, EventArgs e)
        {
            int currentStock = 0;
            var currentStockData = _productManager.SelectProductsById(_productId);
            int addedStocks = Convert.ToInt32(textBox2.Text);

            if (currentStockData != null || currentStockData.Rows.Count != 0)
            {
                foreach (DataRow row in currentStockData.Rows)
                {
                    currentStock = Convert.ToInt32(row["Stock"]);
                }
            }

            _productManager.UpdateProductByStock(_productId, currentStock + addedStocks);
            _historyManager.InsertHistory(new Models.History { ProductID = _productId, AddedStocks = addedStocks });

            DialogResult = DialogResult.OK;
        }

        //CANCEL BUTTON
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
