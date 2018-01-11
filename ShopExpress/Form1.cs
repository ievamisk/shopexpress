using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopExpress
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            

        }
            TextBox email;
            TextBox password;

        public void Form1_Load(object sender, EventArgs e)
        {
            generateForm();
        }

        private void login_Click(object sender, EventArgs e)
        {
            if (UserControl.logIn(email.Text, password.Text) > 0)
            {
                MessageBox.Show("Hello, " + UserControl.userFullname());
                this.Hide();
                Form2 Form2 = new Form2("login");
                Form2.FormClosed += Form2_FormClosed;
                Form2.ShowDialog();
            }
            else
            {
                MessageBox.Show("Error! Incorrect credentials!");
            }
        }

        private void register_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 Form2 = new Form2("registration");
            Form2.FormClosed += Form2_FormClosed;
            Form2.ShowDialog();
        }
              

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        public void generateForm()
        {
            Panel loginPanel = new Panel();
            Label loginLabel = new Label();
            Label line = new Label();
            PictureBox loginPicture = new PictureBox();
            Label emailLabel = new Label();
            email = new TextBox();
            Label passwordLabel = new Label();
            password = new TextBox();
            Button login = new Button();
            Label notUser = new Label();
            LinkLabel register = new LinkLabel();

            loginPanel.Width = this.Width;
            loginPanel.Height = this.Height;

            loginLabel.Parent = loginPanel;
            loginLabel.Text = "USER LOGIN";
            loginLabel.Location = new Point(loginPanel.Width / 2 - 60, 20);
            loginLabel.AutoSize = true;
            loginLabel.Font = new Font("Arial", 12);

            loginPicture.ImageLocation = "loginpicture.png";
            loginPicture.Parent = loginPanel;
            loginPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            loginPicture.Size = new System.Drawing.Size(100, 100);
            loginPicture.Location = new Point(loginPanel.Width / 2 - 60, 50);

            line.AutoSize = false;
            line.Height = 2;
            line.BorderStyle = BorderStyle.Fixed3D;
            line.Location = new Point(50, 185);
            line.Width = (loginPanel.Width - 100);

            notUser.Parent = loginPanel;
            notUser.Text = "Not a user? ";
            notUser.Location = new Point(loginPanel.Width / 2 - 65, 160);
            notUser.AutoSize = true;

            register.Parent = loginPanel;
            register.Text = "REGISTER!";
            register.Location = new Point(loginPanel.Width / 2, 160);
            register.AutoSize = true;

            emailLabel.Parent = loginPanel;
            emailLabel.Text = "Email";
            emailLabel.Location = new Point(loginPanel.Width / 2 - 30, 200);
            emailLabel.AutoSize = true;
            email.Parent = loginPanel;
            email.Size = new System.Drawing.Size(170, 22);
            email.TextAlign = HorizontalAlignment.Center;
            email.Location = new Point(loginPanel.Width / 2-95, 220);

            passwordLabel.Parent = loginPanel;
            passwordLabel.Text = "Password";
            passwordLabel.Location = new Point(loginPanel.Width / 2 - 40, 250);
            passwordLabel.AutoSize = true;
            password.Size = new System.Drawing.Size(170, 22);
            password.TextAlign = HorizontalAlignment.Center;
            password.Parent = loginPanel;
            password.Location = new Point(loginPanel.Width / 2-95, 270);

            login.Parent = loginPanel;
            login.Text = "Login";
            login.Location = new Point(loginPanel.Width / 2 - 50, 300);
            login.AutoSize = true;

            login.Click += login_Click;
            register.Click += register_Click;

            this.Controls.Add(loginLabel);
            this.Controls.Add(emailLabel);
            this.Controls.Add(email);
            this.Controls.Add(passwordLabel);
            this.Controls.Add(password);
            this.Controls.Add(login);
            this.Controls.Add(loginPicture);
            this.Controls.Add(line);
            this.Controls.Add(notUser);
            this.Controls.Add(register);
        }
    }
}

