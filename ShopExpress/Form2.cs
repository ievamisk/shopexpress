using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopExpress
{
    public partial class Form2 : Form
    {
        private string state;

        public Form2(string state)
        {
            this.state = state;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'userData.Product' table. You can move, or remove it, as needed.
            this.productTableAdapter2.Fill(this.userData.Product);
            this.productTableAdapter.Fill(this.products.Product);

            switch (this.state)
            {
                case "registration":
                    registerPanel.BringToFront();
                    break;
                case "login":
                    userPanel.BringToFront();
                    dataSetter();
                    break;
            }
            comboBox_Load();
        }

        private void dataSetter()
        {
            using (var context = new ShopExpressEntities())
            {
                searchCategoryBox.DataSource = context.Categories.ToList();
                searchCategoryBox.ValueMember = "categoryID";
                searchCategoryBox.DisplayMember = "categoryName";
                searchCategoryBox.Text = "Choose category";
                fullName.Text = UserControl.userFullname();

                dataGridView1.DataSource = context.Products.ToList();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    int uID = Convert.ToInt32(row.Cells[8].Value);
                    int cID = Convert.ToInt32(row.Cells[9].Value);
                    int ccID = Convert.ToInt32(row.Cells[10].Value);

                    var userL = context.Users
                        .Where(x => x.userID == uID)
                        .Select(y => new { y.firstName, y.lastName }).ToList();

                    var categoryL = context.Categories
                        .Where(x => x.categoryID == cID)
                        .Select(y => new { y.categoryName }).ToList();

                    var colorL = context.Colors
                        .Where(x => x.colorID == ccID)
                        .Select(y => new { y.color }).ToList();

                    row.Cells[2].Value = (userL[0].firstName + " " + userL[0].lastName).ToString();
                    row.Cells[4].Value = categoryL[0].categoryName.ToString();
                    row.Cells[5].Value = colorL[0].color.ToString();
                }

                int uIDu = UserControl.ID();
                dataGridView2.DataSource = context.Products
                                            .Where(x => x.userID == uIDu)
                                            .Select(x => new { x.userID, x.price, x.productID, x.productName, x.size, x.genderCategory, x.colorID, x.categoryID })
                                            .ToList();

                foreach (DataGridViewRow row2 in dataGridView2.Rows)
                {
                    int uID = Convert.ToInt32(row2.Cells[8].Value);
                    int cID = Convert.ToInt32(row2.Cells[9].Value);
                    int ccID = Convert.ToInt32(row2.Cells[10].Value);

                    var userL = context.Users
                        .Where(x => x.userID == uID)
                        .Select(y => new { y.firstName, y.lastName }).ToList();

                    var categoryL = context.Categories
                        .Where(x => x.categoryID == cID)
                        .Select(y => new { y.categoryName }).ToList();

                    var colorL = context.Colors
                        .Where(x => x.colorID == ccID)
                        .Select(y => new { y.color }).ToList();

                    row2.Cells[2].Value = (userL[0].firstName + " " + userL[0].lastName).ToString();
                    row2.Cells[4].Value = categoryL[0].categoryName.ToString();
                    row2.Cells[5].Value = colorL[0].color.ToString();

                    //Seller, category and color name:
                    //MessageBox.Show(row2.Cells[2].Value.ToString() + ":" + row2.Cells[4].Value.ToString() + ":" + row2.Cells[5].Value.ToString());
                }
            }
            
        }

        //REGISTER PANEL
        private void register_Click(object sender, EventArgs e)
        {

            using (ShopExpressEntities context = new ShopExpressEntities())
            {
                try
                {
                    context.Users.Add(new User
                    {
                        firstName = firstName.Text,
                        lastName = lastName.Text,
                        address = address.Text,
                        city = city.Text,
                        country = country.Text,
                        email = email.Text,
                        phone = phone.Text,
                        password = password.Text,

                    });

                    context.SaveChanges();
                    MessageBox.Show("Success! Please login now!");
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Oops!");//exc.Message);
                }
            }
        }

        // ADD NEW ITEM PANEL
        private void uploadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            DialogResult result = open.ShowDialog();
            itemPicture.Image = Image.FromFile(open.FileName);
            itemPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            itemPicture.Size = new System.Drawing.Size(300, 350);
            picPath.Text = open.FileName;
        }

        private void comboBox_Load()
        {
            using (ShopExpressEntities context = new ShopExpressEntities())
            {
                List<Category> categoryList = new List<Category>();
                List<Color> colorList = new List<Color>();
                categoryList = context.Categories.ToList();
                colorList = context.Colors.ToList();
                itemCategory.DataSource = categoryList;
                itemCategory.ValueMember = "categoryID";
                itemCategory.DisplayMember = "categoryName";
                itemCategory.Text = "Choose category";

                itemColor.DataSource = colorList;
                itemColor.ValueMember = "colorID";
                itemColor.DisplayMember = "color";
                itemColor.Text = "Choose color";
            }
        }

        private void uploadItemButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (ShopExpressEntities context = new ShopExpressEntities())
                {
                    var catID = context.Categories
                                .Where(x => x.categoryName == itemCategory.Text)
                                .Select(x => new { x.categoryID }).ToList();

                    var colID = context.Colors
                                .Where(y => y.color == itemColor.Text)
                                .Select(y => new { y.colorID }).ToList();

                    context.Products.Add(new Product
                    {
                        categoryID = catID[0].categoryID,
                        userID = UserControl.ID(),
                        productName = itemName.Text,
                        productDescription = itemDescription.Text,
                        genderCategory = gender.Text,
                        size = itemSize.Text,
                        colorID = colID[0].colorID,
                        price = Convert.ToInt64(itemPrice.Text),
                        picture = picPath.Text
                    }
                    );

                    context.SaveChanges();
                    MessageBox.Show("New item is added!");
                    userPanel.BringToFront();
                    dataSetter();
                }
            }
            catch
            {
                MessageBox.Show("Oops! Some data is invalid!");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            using (ShopExpressEntities context = new ShopExpressEntities())
            {
                try
                {
                    var catID = context.Categories
                            .Where(g => g.categoryName == searchCategoryBox.Text)
                            .Select(x => new { x.categoryID }).ToList();

                    string selectedGender = searchGenderBox.Text;
                    int selectedCategory = catID[0].categoryID;

                    var products = context.Products
                        .Where(x => x.categoryID == selectedCategory && x.genderCategory == selectedGender)
                        .Select(y => new { y.productID, y.userID, y.genderCategory, y.categoryID, y.productName, y.size, y.colorID, y.price }).ToList();

                    dataGridView1.DataSource = products;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        int uID = Convert.ToInt32(row.Cells[8].Value);
                        int cID = Convert.ToInt32(row.Cells[9].Value);
                        int ccID = Convert.ToInt32(row.Cells[10].Value);

                        var userL = context.Users
                            .Where(x => x.userID == uID)
                            .Select(y => new { y.firstName, y.lastName }).ToList();

                        var categoryL = context.Categories
                            .Where(x => x.categoryID == cID)
                            .Select(y => new { y.categoryName }).ToList();

                        var colorL = context.Colors
                            .Where(x => x.colorID == ccID)
                            .Select(y => new { y.color }).ToList();

                        row.Cells[2].Value = userL[0].firstName.ToString() + " " + userL[0].lastName.ToString();
                        row.Cells[4].Value = categoryL[0].categoryName.ToString();
                        row.Cells[5].Value = colorL[0].color.ToString();
                    }
                }
                catch
                {
                    MessageBox.Show("Choose gender and category please!");
                }
            };
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            addItemPanel.BringToFront();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            userPanel.BringToFront();
            dataSetter();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();

        }

        private void fullName_Click(object sender, EventArgs e)
        {
            userProfilePanel.BringToFront();
            profileFullName2.Text = UserControl.userFullname();
            profileCountry2.Text = UserControl.getCountry();
            profileCity2.Text = UserControl.getCity();

            using (ShopExpressEntities context = new ShopExpressEntities())
            {
                int getUserID = UserControl.ID();

                var products = context.Products
                    .Where(g => g.userID == getUserID)
                    .Select(y => new { y.productID, y.userID, y.genderCategory, y.categoryID, y.productName, y.size, y.colorID, y.price }).ToList();

                dataGridView2.DataSource = products;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            userPanel.BringToFront();
            dataSetter();
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            userPanel.BringToFront();
            dataSetter();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int prodID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            displayProduct(prodID);
        }


        private int productIDtemp;
        private void displayProduct(int prodID)
        {
            using (ShopExpressEntities context = new ShopExpressEntities())
            {
                productIDtemp = prodID;

                var prod = context.Products
                        .Where(x => x.productID== prodID)
                        .Select(y => new {y.userID, y.genderCategory, y.categoryID, y.productDescription, y.productName, y.size, y.colorID, y.price, y.picture}).ToList();

                productPanel.BringToFront();
                int sellerID = prod[0].userID;
                int catID = prod[0].categoryID;
                int colID = prod[0].colorID;
                string pic = prod[0].picture;


                var seller = context.Users
                        .Where(x => x.userID == sellerID)
                        .Select(y => new { y.firstName, y.lastName }).ToList();

                var category = context.Categories
                    .Where(x => x.categoryID == catID)
                    .Select(y => new { y.categoryName }).ToList();

                var color = context.Colors
                    .Where(x => x.colorID == colID)
                    .Select(y => new { y.color }).ToList();

                pl1.Text = prod[0].productName;
                productSeller.Text = seller[0].firstName + " " + seller[0].lastName;
                productCategory.Text = category[0].categoryName;
                productColor.Text = color[0].color;
                productDescription.Text = prod[0].productDescription;
                productGender.Text = prod[0].genderCategory;
                productSize.Text = prod[0].size;
                productPrice.Text = prod[0].price + " €";
                productPicture.Image = Image.FromFile(Path.GetFullPath(pic));

                productPicture.SizeMode = PictureBoxSizeMode.StretchImage;
                productPicture.Size = new System.Drawing.Size(300, 370);

                if (UserControl.ID() == sellerID)
                {
                    try { order.Hide(); }
                    catch { }
                }
                else
                {
                    try { order.Show(); }
                    catch { }
                }
            };

        }

        private void order_Click(object sender, EventArgs e)
        {
            string message = "From : " + productSeller.Text + Environment.NewLine;
            message += "To : " + UserControl.userFullname() + Environment.NewLine;
            message += "Price : " + productPrice.Text + Environment.NewLine;
            DialogResult result = MessageBox.Show(message, "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var context = new ShopExpressEntities())
                    {
                        var product = new Product { productID = productIDtemp };
                        context.Products.Attach(product);
                        context.Products.Remove(product);
                        context.SaveChanges();
                    }

                    MessageBox.Show("Order confirmed!", "Confirmation");

                    userPanel.BringToFront();
                    dataSetter();
                }
                catch
                {
                    MessageBox.Show("Oops, something went wrong! Please try again!", "Error!");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}


