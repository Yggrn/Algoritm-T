using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Data.SQLite;

namespace APIcloud
{
    public partial class Form1 : Form
    {
        public string dataorder;
        SQL_DB insertDB = new SQL_DB();
        public Form1()
        {
            InitializeComponent();
        }
       private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            textBox2.Text="";
            Task.WaitAll(TokenKey.Get(textBox1.Text));
            Organizations.Get();
            for (int i = 0; i < Organizations.orgId.Count; i++)
            {
                textBox2.Text+= "---------------------------"+Environment.NewLine;
                textBox2.Text+= Organizations.orgId[i].name+Environment.NewLine;
                textBox2.Text+= "---------------------------"+Environment.NewLine;
                comboBox1.Items.Add(Organizations.orgId[i].name);
                comboBox1.Text = Organizations.orgId[i].name;
            }
            var term = JsonConvert.DeserializeObject<TerminalGroups.TerminaGroups_>(TerminalGroups.Get());

            for (int i = 0; i < term.terminalGroups.Count; i++)
            {
                for (int j = 0; j < term.terminalGroups[i].items.Count; j++)
                {
                    textBox2.Text+=term.terminalGroups[i].items[j].name+Environment.NewLine;
                    textBox2.Text+=term.terminalGroups[i].items[j].id+Environment.NewLine;
                    textBox2.Text+=term.terminalGroups[i].items[j].timeZone+Environment.NewLine;
                    textBox2.Text+=term.terminalGroups[i].items[j].address+Environment.NewLine;
                    comboBox2.Items.Add(term.terminalGroups[i].items[j].id);
                }
            }
            for (int i = 0; i < comboBox2.Items.Count; i++)
            {
                comboBox2.Text = comboBox2.Items[i].ToString();
            }
            
            try
            {
                
                var items = JsonConvert.DeserializeObject<Menus.Root>(Menus.Get());
                foreach (var item in items.groups)
                {
                    insertDB.InsertToDB(item.id, item.name, "groups", "",item.isDeleted.ToString(), item.isGroupModifier.ToString());
                }
                foreach (var item in items.products)
                {
                    insertDB.InsertToDB(item.id, item.name, "products", item.parentGroup);
                }
                foreach (var item in items.productCategories)
                {
                    insertDB.InsertToDB(item.id, item.name, "productCategories");
                }
                foreach (var item in items.sizes)
                {
                    insertDB.InsertToDB(item.id, item.name, "sizes");
                }
                insertDB.InsertToDB(items.revision);

                    var conn = new SQLiteConnection("Data Source=" + insertDB.dbName + ";Version=3;");
                    string Sequel = "SELECT * FROM groups where isGroupModifier='False'";
                    SQLiteDataAdapter da = new SQLiteDataAdapter(Sequel, conn);
                    DataTable dt = new DataTable();
                    conn.Open();
                    da.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                      var  parentNode = treeView1.Nodes.Add(dr["name"].ToString());
                     PopulateTreeView(dr["id"].ToString(), parentNode);
                    }

                conn.Close();
                notParentGroupTreeView();

                

            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message, "ERROR");
            }


      
            var orderTypes = JsonConvert.DeserializeObject<Menus.RootOrderTypes>(OrderTypes.Get());

            for (int i = 0; i < orderTypes.orderTypes[0].items.Count; i++)
            {
                comboBox3.Items.Add(orderTypes.orderTypes[0].items[i].name);

            }

            comboBox3.Text = comboBox3.Items[0].ToString();

            button2.Enabled=true;
        }
        double currentPrice;

        private void PopulateTreeView(string parentId, TreeNode parentNode)
        {
            var conn = new SQLiteConnection("Data Source=" + insertDB.dbName + ";Version=3;");
            string Seqchildc = "SELECT id,name, parentGroup FROM products WHERE parentGroup=" + "'"+ parentId + "'"+ "";
            
            SQLiteDataAdapter dachildmnuc = new SQLiteDataAdapter(Seqchildc,conn);
            DataTable dtchildc = new DataTable();
            conn.Open();
            dachildmnuc.Fill(dtchildc);
            TreeNode childNode;
            foreach (DataRow dr in dtchildc.Rows)
            {
                if (parentNode == null)
                    childNode = treeView1.Nodes.Add(dr["name"].ToString());
                else 
                    childNode = parentNode.Nodes.Add(dr["name"].ToString());
                    PopulateTreeView(dr["id"].ToString(), childNode);
            }
            
            conn.Close();
        }

        private void notParentGroupTreeView()
        {
            var conn = new SQLiteConnection("Data Source=" + insertDB.dbName + ";Version=3;");
            string nullableSeqchildc = "SELECT id,name, parentGroup FROM products WHERE parentGroup=" + "''"+"";
            conn.Open();
            SQLiteDataAdapter dachildmnuc2 = new SQLiteDataAdapter(nullableSeqchildc, conn);
            DataTable dtchildc2 = new DataTable();
            dachildmnuc2.Fill(dtchildc2);
            foreach (DataRow dr in dtchildc2.Rows)
            {
                if (!treeView1.Nodes.Equals(dr["name"]))
                {
                    treeView1.Nodes.Add(dr["name"].ToString());
                }

            }
            conn.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            
       //     var price = JsonConvert.DeserializeObject<Menus.Root>(json);
       //     foreach (var item in price.products)
       //     {
       //         for (int i = 0; i < price.products.Count; i++)
       //         {
       //             if (item.id == label2.Text)
       //             {
       //               currentPrice= (double)item.sizePrices[0].price.currentPrice;
       //             }
       //         }
       //     }
       //textBox2.Text+= CreateOrder.Create(Generate(label2.Text, currentPrice));
        }

         public string Generate(string product, double currentP)
        {
            
            var orderTypes = JsonConvert.DeserializeObject<Menus.RootOrderTypes>(OrderTypes.Get());
            
            string str = null;
            for (int i = 0; i < orderTypes.orderTypes[0].items.Count; i++)
            {
                if (comboBox3.Text == orderTypes.orderTypes[0].items[i].name) str = orderTypes.orderTypes[0].items[i].id;
            }
            List<CreateOrder.Item> item1 = new List<CreateOrder.Item>();
            item1.Add(new CreateOrder.Item
            {
                productId = product,
                type = "Product",
                amount = 1,
                productSizeId = null,
                comboInformation = null,
                comment = "Тест не готовить",
                price = currentP
       
            });
            List<string> tablesid = new List<string>() ;
                  tablesid.Add(comboBox4.Text);

            CreateOrder.Item.OrderCreate newOrder = new CreateOrder.Item.OrderCreate
            {
                organizationId = Organizations.orgId[0].id.ToString(),
                terminalGroupId = comboBox2.Text,
                order = new CreateOrder.Item.Order
                {
                    id = null,
                    externalNumber = null,
                    tableIds = new List<string>
                    {
                        tablesid[0]
                    },
                    customer = new CreateOrder.Customer
                    {
                        id = null,
                        name = "Algoritm-T",
                        surname = null,
                        comment = "TEST",
                        birthdate = null,
                        email = "support@algoritm-t.ru",
                        shouldReceiveOrderStatusNotifications = false,
                        gender = "NotSpecified",
                        type = "regular"
                    },
                    phone = "73452684305",
                    guests = new CreateOrder.Guests
                    {
                        count =1
                    },
                    tabName = null,
                    items = item1,
                    combos =null,
                    payments = null,
                    tips = null,    
                    sourceKey = null,
                    discountsInfo = null,
                    iikoCard5Info = null,
                    orderTypeId = str
                },
               createOrderSettings = null 
              
            };

            return JsonConvert.SerializeObject(newOrder);
        }
        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            var tables = JsonConvert.DeserializeObject<Tables.GetTable>(Tables.Get(comboBox2.Text));
            for (int i = 0; i < tables.restaurantSections.Count; i++)
            {
                for (int j = 0; j < tables.restaurantSections[i].tables.Count; j++)
                {
                    comboBox4.Items.Add(tables.restaurantSections[i].tables[j].id);
                }
            }
        }
        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                label2.Text = treeView1.SelectedNode.Text;
            }
            catch (Exception)
            {
                throw;
            }
       
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SQL_DB newDB = new SQL_DB();
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\test.db"))
            {
                
            }
            else 
            {
                newDB.CreateDB();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //SQL_DB test = new SQL_DB();
            //DataTable into = new DataTable();
            //into = test.Data();
            //foreach (DataRow item in into.Rows)
            //{

            //}
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            insertDB.DropTable();
            Application.Exit();
        }
    }
}
