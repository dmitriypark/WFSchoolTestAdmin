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
    public partial class UsersForm : Form
    {
        string loginStart;
        string passwordStart;
        public UsersForm(string login, string password, IEnumerable<User> users)
        {
            InitializeComponent();
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView.ReadOnly = true;

            loginStart = login;
            passwordStart = password;


            dataGridView.AllowUserToAddRows = false;
            foreach (var user in users)
            {

                var rowNumber = dataGridView.Rows.Add();
                dataGridView.Rows[rowNumber].Cells["Id"].Value = user.id;
                dataGridView.Rows[rowNumber].Cells["login"].Value = user.login;
                dataGridView.Rows[rowNumber].Cells["fullName"].Value = user.fullName;
                dataGridView.Rows[rowNumber].Cells["roles"].Value = user.roles;
                dataGridView.Rows[rowNumber].Cells["password"].Value = user.password;
            }
        }

        //private async void btnAdd_Click(object sender, EventArgs e)
        //{
        //    User user=new User();
        //    if(txtId.Text !="")
        //    {
        //        user.id = Int32.Parse(txtId.Text);
        //    }
        //    user.login = txtLogin.Text;
        //    user.fullName = txtFullName.Text;
        //    user.roles = Int32.Parse(txtRoles.Text);
        //    user.password = txtPassword.Text;


        //    ApplicationViewModel applicationViewModel = new ApplicationViewModel();
        //    await applicationViewModel.AddUser(loginStart, passwordStart, user);

        //    await applicationViewModel.GetUsers(loginStart, passwordStart);

        //    dataGridView.Rows.Clear();
        //    foreach (var users in applicationViewModel.Users)
        //    {

        //        var rowNumber = dataGridView.Rows.Add();
        //        dataGridView.Rows[rowNumber].Cells["Id"].Value = users.id;
        //        dataGridView.Rows[rowNumber].Cells["login"].Value = users.login;
        //        dataGridView.Rows[rowNumber].Cells["fullName"].Value = users.fullName;
        //        dataGridView.Rows[rowNumber].Cells["roles"].Value = users.roles;
        //        dataGridView.Rows[rowNumber].Cells["password"].Value = users.password;
        //    }

        //}

        private async void dataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            User userForCorrect = new User();
            userForCorrect.id = Int32.Parse(dataGridView.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString());
            userForCorrect.login = dataGridView.Rows[e.RowIndex].Cells["login"].FormattedValue.ToString();
            userForCorrect.password = dataGridView.Rows[e.RowIndex].Cells["fullName"].FormattedValue.ToString();
            userForCorrect.roles = Int32.Parse(dataGridView.Rows[e.RowIndex].Cells["roles"].FormattedValue.ToString());
            userForCorrect.password = dataGridView.Rows[e.RowIndex].Cells["password"].FormattedValue.ToString();


            ApplicationViewModel applicationViewModel = new ApplicationViewModel();
            await applicationViewModel.GetUsers(loginStart, passwordStart);
            var user = applicationViewModel.Users.Where(u => u.login == loginStart).FirstOrDefault();
            AddUserForm userForm = new AddUserForm(user, applicationViewModel.Users, this, userForCorrect);
            userForm.Show();




        }

        //private async void btnDelete_Click(object sender, EventArgs e)
        //{
        //    bool success;

        //    int id = Int32.Parse(txtId.Text);
        //    ApplicationViewModel applicationViewModel = new ApplicationViewModel();
        //    success = await applicationViewModel.DeleteUser(loginStart, passwordStart, id);

        //    dataGridView.Rows.Clear();


        //    await applicationViewModel.GetUsers(loginStart, passwordStart);
        //    foreach (var user in applicationViewModel.Users)
        //    {

        //        var rowNumber = dataGridView.Rows.Add();
        //        dataGridView.Rows[rowNumber].Cells["Id"].Value = user.id;
        //        dataGridView.Rows[rowNumber].Cells["login"].Value = user.login;
        //        dataGridView.Rows[rowNumber].Cells["fullName"].Value = user.fullName;
        //        dataGridView.Rows[rowNumber].Cells["roles"].Value = user.roles;
        //        dataGridView.Rows[rowNumber].Cells["password"].Value = user.password;

        //    }

        //    if (success)
        //    {
        //        MessageBox.Show("Subject delete");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Subject cannot be deleted because other tables are linked to it");
        //    }
        //}

        private void UsersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        //Add button
        private async void toolStripButton1_Click(object sender, EventArgs e)
        {
            ApplicationViewModel applicationViewModel = new ApplicationViewModel();
            await applicationViewModel.GetUsers(loginStart, passwordStart);
            var user = applicationViewModel.Users.Where(u => u.login == loginStart).FirstOrDefault();
            AddUserForm userForm = new AddUserForm(user, applicationViewModel.Users,this);
            userForm.Show();
        }

        private void dataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.txtId.Text = dataGridView.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString();
        }


        //Delete button
        private async void toolStripButton2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                bool success;

                int id = Int32.Parse(txtId.Text);
                ApplicationViewModel applicationViewModel = new ApplicationViewModel();
                success = await applicationViewModel.DeleteUser(loginStart, passwordStart, id);

                dataGridView.Rows.Clear();


                await applicationViewModel.GetUsers(loginStart, passwordStart);
                foreach (var user in applicationViewModel.Users)
                {

                    var rowNumber = dataGridView.Rows.Add();
                    dataGridView.Rows[rowNumber].Cells["Id"].Value = user.id;
                    dataGridView.Rows[rowNumber].Cells["login"].Value = user.login;
                    dataGridView.Rows[rowNumber].Cells["fullName"].Value = user.fullName;
                    dataGridView.Rows[rowNumber].Cells["roles"].Value = user.roles;
                    dataGridView.Rows[rowNumber].Cells["password"].Value = user.password;

                }

                if (success)
                {
                    MessageBox.Show("Subject delete");
                }
                else
                {
                    MessageBox.Show("Subject cannot be deleted because other tables are linked to it");
                }

                
            }
        }
    }
}
