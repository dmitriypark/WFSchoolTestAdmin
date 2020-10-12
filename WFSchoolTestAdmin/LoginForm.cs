using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WFSchoolTestAdmin.Models;

namespace WFSchoolTestAdmin
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            txtPassword.UseSystemPasswordChar = true;
        }

        

        private async void button1_Click(object sender, EventArgs e)
        {
            ApplicationViewModel applicationViewModel = new ApplicationViewModel();
            await applicationViewModel.GetUsers(txtLogin.Text, txtPassword.Text);
            UsersForm usersForm = new UsersForm(txtLogin.Text,txtPassword.Text, applicationViewModel.Users);
            this.Hide();
            usersForm.Show();

        }
    }
}
