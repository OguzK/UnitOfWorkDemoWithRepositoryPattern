using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnitOfWork;
using UnitOfWork.Models;

namespace WinApplication
{
    public partial class Form1 : Form
    {
        Worker worker = new Worker();
        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = bindingSource1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            worker.SaveChanges();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.worker.Dispose();
        }

        private void btnFillCategories_Click(object sender, EventArgs e)
        {
            bindingSource1.DataSource = worker.CategoryRepository.ToBindingList();
        }

        private void btnFillProducts_Click(object sender, EventArgs e)
        {
            IEnumerable<string> items = new[] { "A", "P" };
            bindingSource1.DataSource = worker.ProductRepository.ToBindingList(p => items.Contains(p.ProductName));
        }

        private void btnFillOrders_Click(object sender, EventArgs e)
        {
            bindingSource1.DataSource = worker.OrderRepository.ToBindingList();
        }
    }
}
