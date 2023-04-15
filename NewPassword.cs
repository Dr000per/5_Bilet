using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5_Bilet
{
    public partial class NewPassword : Form
    {
        DataBase db = new DataBase();
        public NewPassword()
        {
            InitializeComponent();
        }

        private void NewPassword_Load(object sender, EventArgs e)
        {
            textBox_new_pass.MaxLength = 15;
        }

        private void button_accept_Click(object sender, EventArgs e)
        {
            DataTable dt = db.ExecuteSql($"select * from users where login = '{textBox_log.Text}'");
            var newpass = textBox_new_pass.Text;

            var chs = new[] { '@', '#', '%', ')', '(', '.', '<' };
            int s = 0, n = 0, b = 0;
            foreach (var c in newpass)
                if (char.IsDigit(c)) ++n;
                // else if (chs.Contains(c)) ++s; // если надо чтобы был хоть 1 символ из спец. символов, а newpass.Contains удалить
                else if (char.IsLetter(c)) ++b;


            if(n == 3 && b == 5 /* && s > 0 */ && newpass.Contains("@") & newpass.Contains("%") && newpass.Contains("#") && newpass.Contains(".") && newpass.Contains("(") && newpass.Contains(")") && newpass.Contains("<"))
            {
                try
                {
                    if (dt.Rows.Count > 0 && textBox_new_pass.Text.Length > 9)
                    {
                        db.ExecuteSqlNonQuery($"update users set password = '{textBox_new_pass.Text}' where login = '{textBox_log.Text}'");
                        MessageBox.Show("Пароль успешно изменён!");
                    }
                    else
                    {
                        MessageBox.Show("Такого пользователя не существует");
                    }
                }
                catch
                {
                    MessageBox.Show("Что-то пошло не так!");
                }
            }
            else
            {
                MessageBox.Show("Что-то пошло не так! Проверьте пароль на соответствие шаблону (5 букв, 3 цифры и спец. символы ' @#%)(.< ' :");
            }
            
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Close();
        }
    }
}
