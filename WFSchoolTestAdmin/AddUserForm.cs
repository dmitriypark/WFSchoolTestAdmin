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
    public partial class AddUserForm : Form
    {
        User user;
        UsersForm usersForm;
        public AddUserForm(User user, IEnumerable<User> users, UsersForm usersForm)
        {
            InitializeComponent();
            this.user = user;
            this.usersForm = usersForm;
        }

        public AddUserForm(User user, IEnumerable<User> users, UsersForm usersForm,User userForCorrect)
        {
            InitializeComponent();
            this.user = user;
            txtId.Text = userForCorrect.id.ToString();
            txtLogin.Text = userForCorrect.login;
            txtFullName.Text = userForCorrect.password;
            txtRoles.Text = userForCorrect.roles.ToString();
            txtPassword.Text = userForCorrect.password;
        }



        private async void btnAdd_Click(object sender, EventArgs e)
        {
            User user = new User();
            if (txtId.Text != "")
            {
                user.id = Int32.Parse(txtId.Text);
            }
            user.login = txtLogin.Text;
            user.fullName = txtFullName.Text;
            user.roles = Int32.Parse(txtRoles.Text);
            user.password = txtPassword.Text;


            ApplicationViewModel applicationViewModel = new ApplicationViewModel();
            await applicationViewModel.AddUser(this.user.login, this.user.password, user);

            await applicationViewModel.GetUsers(this.user.login, this.user.password);

            usersForm.dataGridView.Rows.Clear();
            foreach (var users in applicationViewModel.Users)
            {

                var rowNumber = usersForm.dataGridView.Rows.Add();
                usersForm.dataGridView.Rows[rowNumber].Cells["Id"].Value = users.id;
                usersForm.dataGridView.Rows[rowNumber].Cells["login"].Value = users.login;
                usersForm.dataGridView.Rows[rowNumber].Cells["fullName"].Value = users.fullName;
                usersForm.dataGridView.Rows[rowNumber].Cells["roles"].Value = users.roles;
                usersForm.dataGridView.Rows[rowNumber].Cells["password"].Value = users.password;
            }
        }
    }
}
