using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace listele
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection bagla = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=deneme.mdb");
        public void temizle() {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text==""||textBox3.Text==""||textBox4.Text==""||textBox5.Text=="")
            {
                MessageBox.Show("Lütfen Boş Bırakmayınız");
                return;
            }
            else
            {
                bagla.Open();
                OleDbCommand komut = new OleDbCommand();
                komut.Connection = bagla;
                komut.CommandText = "insert into bilgi(ad,soyad,tc,telefon)values('"+textBox2.Text+"','"+textBox3.Text+"',"+textBox4.Text+","+textBox5.Text+")";
                OleDbDataReader okut = komut.ExecuteReader();
                while (okut.Read())
                {
                    dataGridView1.Rows.Add(okut["ad"],okut["soyad"],okut["tc"],okut["telefon"]);
                }
             
                bagla.Close();
                MessageBox.Show("Başarıyla Eklendi");
                dataGridView1.Rows.Clear();
                temizle();
                listele();
            }
        }
        private void listele() {
            bagla.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = bagla;
            komut.CommandText = "select * from bilgi";
            OleDbDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                dataGridView1.Rows.Add(oku["id"], oku["ad"], oku["soyad"], oku["tc"], oku["telefon"]);
            }
            bagla.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Lütfen Boş Bırakmayınız");
                return;
            }
            else
            {
                bagla.Open();
                OleDbCommand komut = new OleDbCommand();
                komut.Connection = bagla;
                komut.CommandText = "update bilgi set ad='" + textBox2.Text + "',soyad='" + textBox3.Text + "',tc=" + textBox4.Text + ",telefon=" + textBox5.Text + " where id="+textBox1.Text+"";
                komut.ExecuteNonQuery();
                bagla.Close();
                MessageBox.Show("Başarıyla Güncellendi");
                dataGridView1.Rows.Clear();
                temizle();
                listele();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
           textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
           textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
           textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
           textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Lütfen Boş Bırakmayınız");
                return;
            }
            else
            {
                try
                {
                    bagla.Open();
                    OleDbCommand komut = new OleDbCommand();
                    komut.Connection = bagla;
                    komut.CommandText = "delete from bilgi where id=" + textBox1.Text + "";
                    komut.ExecuteNonQuery();
                    bagla.Close();
                    MessageBox.Show("Başarıyla Silindi");
                    dataGridView1.Rows.Clear();
                    temizle();
                    listele();
                }
                catch (Exception)
                {
                    MessageBox.Show("Hata");
                    throw;
                }
               
            }
        }
    }
}
