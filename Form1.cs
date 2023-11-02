using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace indaSoftTZ
{
    public partial class Form1 : Form
    {


        string connectionString = ConfigurationManager.ConnectionStrings["indaSoft"].ConnectionString;

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Получение введенных данных
            int newDepartmentId = int.Parse(textBox1.Text); 
            int newPrecent = int.Parse(textBox2.Text);
            // Выполнение запроса к БД с использованием хранимой процедуры
            using (var connection = new NpgsqlConnection(connectionString))
            {
                // Открытие подключения и выполнение запроса
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM updatesalaryfordepartment2(@newdepartment_id, @percent)", connection))
                {
                    // Добавление входных параметров для процедуры
                    command.Parameters.AddWithValue("newdepartment_id", newDepartmentId);
                    command.Parameters.AddWithValue("percent", newPrecent); 

                    using (var reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        // Отображение результатов запроса в DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                }
                
            }
        }
    

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

    }
}
