using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChargeDataComboBox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AutoComplete();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Data Source=192.168.1.65;User Id=sa;Password=N6AZwKXNzI62xr+hCknvlQ==;Connection Timeout=10000;
            SqlConnection connection = new SqlConnection("Data Source=10.0.0.87,14033; User Id=sa; Password=Passw0rd;Connection Timeout=300;");
            SqlCommand command = new SqlCommand("SELECT [username] from [SgInvoiceV5_ant].[dbo].[CE_comprobantes]", connection);
            connection.Open();
            SqlDataReader registers = command.ExecuteReader();
            while (registers.Read())
            {
                if (comboBox1.Items.Contains(registers["username"].ToString()))
                {

                }
                else
                {
                    comboBox1.Items.Add(registers["username"].ToString());
                }
                
            }
        }

        void AutoComplete()
        {
            SqlConnection connection = new SqlConnection("Data Source=10.0.0.87,14033; User Id=sa; Password=Passw0rd;Connection Timeout=300;");
            DataTable TableData = new DataTable();

            AutoCompleteStringCollection list = new AutoCompleteStringCollection();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT [receptor_nombre] from [SgInvoiceV5_ant].[dbo].[CE_comprobantes]", connection);
            adapter.Fill(TableData);

            for (int i = 0; i < TableData.Rows.Count; i++)
            {
                list.Add(TableData.Rows[i]["receptor_nombre"].ToString());
            }

            textBox1.AutoCompleteCustomSource = list;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string User =  comboBox1.Text;
            string Name = textBox1.Text;
            if (User.Equals(null) || User.Equals("") || Name.Equals(null) || Name.Equals(""))
            {
                MessageBox.Show("The search need the fields User and Name");
            }
            else
            {
                SqlConnection connection = new SqlConnection("Data Source=10.0.0.87,14033; User Id=sa; Password=Passw0rd;Connection Timeout=300;");
                
                SqlCommand command = new SqlCommand("SELECT * from [SgInvoiceV5_ant].[dbo].[CE_comprobantes] where [receptor_nombre]='" + Name + "' and [username]='" + User + "'", connection);
                SqlDataAdapter Adapter = new SqlDataAdapter();
                Adapter.SelectCommand = command;
                DataTable TableDat = new DataTable();
                Adapter.Fill(TableDat);
                dataGridView1.DataSource = TableDat;
            }
        }
    }
}
