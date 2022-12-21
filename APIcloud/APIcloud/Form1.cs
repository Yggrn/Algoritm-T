using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APIcloud
{
    public partial class Form1 : Form
    {
        public string dataorder; 
        public static string json;
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
            comboBox2.Text = comboBox2.Items[1].ToString();
            try
            {
                json = Menus.Get();
                JObject obj = JObject.Parse(json);
                treeView1.Nodes.Clear();
                TreeNode parent = Json2Tree(obj);
                parent.Text = "Root Object";
                treeView1.Nodes.Add(parent);
            }
            catch (Exception ex)
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
        private TreeNode Json2Tree(JObject obj)
        {

            TreeNode parent = new TreeNode();
            foreach (var token in obj)
            {
                parent.Text = token.Key.ToString();
                TreeNode child = new TreeNode();
                child.Text = token.Key.ToString();
                if (token.Value.Type.ToString() == "Object")
                {
                    JObject o = (JObject)token.Value;
                    child = Json2Tree(o);
                    parent.Nodes.Add(child);
                }
                else if (token.Value.Type.ToString() == "Array")
                {
                    int ix = -1;
                    foreach (var itm in token.Value)
                    {
                        if (itm.Type.ToString() == "Object")
                        {
                            TreeNode objTN = new TreeNode();
                            ix++;
                            JObject o = (JObject)itm;
                            objTN = Json2Tree(o);
                            objTN.Text = token.Key.ToString() + "[" + ix + "]";
                            child.Nodes.Add(objTN);
                        }
                        else if (itm.Type.ToString() == "Array")
                        {
                            ix++;
                            TreeNode dataArray = new TreeNode();
                            foreach (var data in itm)
                            {
                                dataArray.Text = token.Key.ToString() + "[" + ix + "]";
                                dataArray.Nodes.Add(data.ToString());
                            }
                            child.Nodes.Add(dataArray);
                        }
                        else
                        {
                            child.Nodes.Add(itm.ToString());
                        }
                    }
                    parent.Nodes.Add(child);
                }
                else
                {
                    if (token.Value.ToString() == "")
                        child.Nodes.Add("N/A");
                    else
                        child.Nodes.Add(token.Value.ToString());
                    parent.Nodes.Add(child);
                }
            }
            return parent;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            
            var price = JsonConvert.DeserializeObject<Menus.Root>(json);
            foreach (var item in price.products)
            {
                for (int i = 0; i < price.products.Count; i++)
                {
                    if (item.id == label2.Text)
                    {
                      currentPrice= (double)item.sizePrices[0].price.currentPrice;
                    }
                }
            }
       textBox2.Text+= CreateOrder.Create(Generate(label2.Text, currentPrice));
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
            CreateOrder.Item.OrderCreate newOrder = new CreateOrder.Item.OrderCreate
            {
            organizationId = Organizations.orgId[0].id.ToString(),
                terminalGroupId = comboBox2.Text,
                order = new CreateOrder.Item.Order
                {
                    id = null,
                    externalNumber = null,
                    tableIds = null,
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
            label2.Text = treeView1.SelectedNode.Text;
        }
    }
}
