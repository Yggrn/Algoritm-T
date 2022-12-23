using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APIcloud
{
    public partial class Main : Form
    {
        string tableID; // ID стола
        string productID; // ID блюда
        double currentPrice; // Цена выбранного блюда
        public string dataorder;
        readonly SQL_DB insertDB = new(); // Общий экземпляр класса для всех методов
        public Main() => InitializeComponent();
        private void SelectGroups()
        {
            try
            {
                var conn = new SQLiteConnection("Data Source=" + insertDB.dbName + ";Version=3;");
                string Sequel = "SELECT * FROM groups where isGroupModifier='False'";
                SQLiteDataAdapter da = new(Sequel, conn);
                DataTable dt = new();
                conn.Open();
                da.Fill(dt);
                twMenu.Nodes.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    var parentNode = twMenu.Nodes.Add(dr["name"].ToString());
                    PopulateTreeView(dr["id"].ToString(), parentNode);
                }
                conn.Close();
            }
            catch (Exception)
            {
            }
        } // Заполнение дерева группами блюд, вызов метода PopulateTreeView для заполнениея блюдами в качестве дочерних элемнтов дерева
        private void SelectTerminalGroups()
        {
            try
            {
                var conn = new SQLiteConnection("Data Source=" + insertDB.dbName + ";Version=3;");
                string getTerm = "SELECT * FROM terminalGroups";
                SQLiteDataAdapter da2 = new(getTerm, conn);
                DataTable dt2 = new();
                da2.Fill(dt2);
                foreach (DataRow dr in dt2.Rows)
                {
                    cbTGroups.Items.Add(dr["name"].ToString());
                }
                cbTGroups.Text = cbTGroups.Items[0].ToString();
                SelectTables(cbTGroups.Text);
                conn.Close();
            }
            catch (Exception)
            {
            }
        } // Загрузка из БД терминальных групп
        private void SelectOrganization()
        {
            try
            {
                var conn = new SQLiteConnection("Data Source=" + insertDB.dbName + ";Version=3;");
                string getOrg = "SELECT * FROM organizations";
                SQLiteDataAdapter da2 = new(getOrg, conn);
                DataTable dt2 = new();
                da2.Fill(dt2);
                foreach (DataRow dr in dt2.Rows)
                {
                    cbOrganizations.Items.Add(dr["name"].ToString());
                }
                cbOrganizations.Text = cbOrganizations.Items[0].ToString();
                conn.Close();
            }
            catch (Exception)
            {
            }

        } // Загрузка из БД списка организаций
        private void SelectOrderTypes()
        {
            try
            {
                var conn = new SQLiteConnection("Data Source=" + insertDB.dbName + ";Version=3;");
                string getOrderType = "SELECT * FROM orderTypes";
                SQLiteDataAdapter da2 = new(getOrderType, conn);
                DataTable dt2 = new();
                da2.Fill(dt2);
                foreach (DataRow dr in dt2.Rows)
                {
                    cbOrderTypes.Items.Add(dr["name"].ToString());
                }
                cbOrderTypes.Text = cbOrderTypes.Items[0].ToString();
                conn.Close();
            }
            catch (Exception)
            {
            }
        } // Загрузка из БД типов заказов
        private void SelectDiscounts()
        {
            try
            {
                var conn = new SQLiteConnection("Data Source=" + insertDB.dbName + ";Version=3;");
                string getDiscounts = "SELECT * FROM discounts";
                SQLiteDataAdapter da2 = new(getDiscounts, conn);
                DataTable dt2 = new();
                da2.Fill(dt2);

                foreach (DataRow dr in dt2.Rows)
                {
                    cbDiscounts.Items.Add(dr["name"].ToString());
                }
                cbDiscounts.Text = cbDiscounts.Items[0].ToString();


                conn.Close();
            }
            catch (Exception)
            {
            }
        } // Загрузка из БД скидок
        private async void button1_Click(object sender, EventArgs e) // Выполнение методов для заполнения формы
        {
            insertDB.DropTable();
            cbOrganizations.Items.Clear();
            cbTGroups.Items.Clear();
            cbOrderTypes.Items.Clear();
            cbDiscounts.Items.Clear();
            cbTables.Items.Clear();
            twMenu.Nodes.Clear();
            tbViewData.Text = "";
            try
            {
                Enabled = false;
                progressBar1.Visible = true;
                await Task.Run(() => { TokenKey.Get(textBox1.Text); }); // Получение токена
                Organizations.Get();
                await Task.Run(() => { insertDB.InsertToDB(); }); // Запись БД из JSON
                SelectOrganization();
                SelectGroups();
                SelectTerminalGroups();
                notParentGroupTreeView();
                SelectOrderTypes();
                SelectDiscounts();
                progressBar1.Visible = false;
                Enabled= true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
            btnCreateOrder.Enabled = true;
            btnCreateDelivery.Enabled = true;
        } 
        private void PopulateTreeView(string parentId, TreeNode parentNode)
        {
            var conn = new SQLiteConnection("Data Source=" + insertDB.dbName + ";Version=3;");
            string Seqchildc = "SELECT id,name, parentGroup FROM products WHERE parentGroup=" + "'" + parentId + "'" + "";

            SQLiteDataAdapter dachildmnuc = new(Seqchildc, conn);
            DataTable dtchildc = new();
            conn.Open();
            dachildmnuc.Fill(dtchildc);
            TreeNode childNode;
            foreach (DataRow dr in dtchildc.Rows)
            {
                if (parentNode == null)
                    childNode = twMenu.Nodes.Add(dr["name"].ToString());
                else
                    childNode = parentNode.Nodes.Add(dr["name"].ToString());
                PopulateTreeView(dr["name"].ToString(), childNode);
            }

            conn.Close();
        }
        private void notParentGroupTreeView()
        {
            try
            {
                var conn = new SQLiteConnection("Data Source=" + insertDB.dbName + ";Version=3;");
                string nullableSeqchildc = "SELECT * FROM products WHERE parentGroup='NULL'";
                conn.Open();
                SQLiteDataAdapter dachildmnuc2 = new(nullableSeqchildc, conn);
                DataTable dtchildc2 = new();
                dachildmnuc2.Fill(dtchildc2);
                foreach (DataRow dr in dtchildc2.Rows)
                {
                    if (!twMenu.Nodes.Equals(dr["name"]))
                    {
                        twMenu.Nodes.Add(dr["name"].ToString());
                    }
                }
                conn.Close();
            }
            catch (Exception)
            {
            }
        }
        private async void btnCreateOrder_Click(object sender, EventArgs e)
        {
            var conn = new SQLiteConnection("Data Source=" + insertDB.dbName + ";Version=3;");
            string nullableSeqchildc = "SELECT * FROM products";
            conn.Open();
            SQLiteDataAdapter dachildmnuc2 = new(nullableSeqchildc, conn);
            DataTable dtchildc2 = new();
            dachildmnuc2.Fill(dtchildc2);
            foreach (DataRow dr in dtchildc2.Rows)
            {
                for (int i = 0; i < dtchildc2.Rows.Count; i++)
                {
                    if (dr["name"].ToString() == label2.Text)
                    {
                        currentPrice = Convert.ToDouble(dr["currentPrice"].ToString());
                        productID = dr["id"].ToString();
                    }
                }
            }
            string CID = CreateOrder.Create(GenerateOrder(productID, currentPrice));
            tbViewData.AppendText(await Task.Run(() => CommandStatus.Get(CID))+ Environment.NewLine);
            conn.Close();
        } // Создание заказа на стол
        public string GenerateOrder(string product, double currentP)
        {
            var conn = new SQLiteConnection("Data Source=" + insertDB.dbName + ";Version=3;");
            conn.Open();
            string nullableSeqchildc = "SELECT * FROM orderTypes";
            SQLiteDataAdapter dachildmnuc2 = new(nullableSeqchildc, conn);
            DataTable dtchildc2 = new();
            dachildmnuc2.Fill(dtchildc2);
            string str = null;
            foreach (DataRow dr in dtchildc2.Rows)
            {
                for (int i = 0; i < dtchildc2.Rows.Count; i++)
                {
                    if (dr["name"].ToString() == cbOrderTypes.Text) str = dr["id"].ToString();
                }
            }
            string TID = "SELECT * FROM terminalGroups";

            SQLiteDataAdapter daTID = new(TID, conn);
            DataTable dtTID = new();
            daTID.Fill(dtTID);
            string strID = null;
            foreach (DataRow dr in dtTID.Rows)
            {
                for (int i = 0; i < dtchildc2.Rows.Count; i++)
                {
                    if (dr["name"].ToString() == cbTGroups.Text) strID = dr["id"].ToString();
                }
            }
            conn.Close();
            List<CreateOrder.Item> item1 = new()
            {
                new CreateOrder.Item
                {
                    productId = product,
                    type = "Product",
                    amount = 1,
                    productSizeId = null,
                    comboInformation = null,
                    comment = "Тест не готовить",
                    price = currentP
                }
            };

            List<string> tablesid = new()
            {
                tableID
            };
            CreateOrder.Item.OrderCreate newOrder = new()
            {
                organizationId = Organizations.orgId[0].id.ToString(),
                terminalGroupId = strID,
                order = new CreateOrder.Item.Order
                {
                    id = null,
                    externalNumber = null,
                    tableIds = tableID != null ? new List<string> { tablesid[0] } : null,
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
                    phone = "+73452684305",
                    guests = new CreateOrder.Guests
                    {
                        count = 1
                    },
                    tabName = null,
                    items = item1,
                    combos = null,
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
        } // Заполнение заказа на стол
        private string GenerateDelivery(string product, double currentP)
        {
            try
            {
                var conn = new SQLiteConnection("Data Source=" + insertDB.dbName + ";Version=3;");
                conn.Open();
                string nullableSeqchildc = "SELECT * FROM orderTypes";
                SQLiteDataAdapter dachildmnuc2 = new(nullableSeqchildc, conn);
                DataTable dtchildc2 = new();
                dachildmnuc2.Fill(dtchildc2);
                string str = null;
                foreach (DataRow dr in dtchildc2.Rows)
                {
                    if (dr["name"].ToString() == cbOrderTypes.Text) str = dr["id"].ToString();
                }
                string TID = "SELECT * FROM terminalGroups";

                SQLiteDataAdapter daTID = new(TID, conn);
                DataTable dtTID = new();
                daTID.Fill(dtTID);
                string strID = null;
                foreach (DataRow dr in dtTID.Rows)
                {
                    for (int i = 0; i < dtchildc2.Rows.Count; i++)
                    {
                        if (dr["name"].ToString() == cbTGroups.Text) strID = dr["id"].ToString();
                    }
                }
                List<CreateDelivery.Discount> DC = new();
                string strDC = null;
                if (checkDiscount.Checked)
                {
                    string DCuont = "SELECT * FROM discounts";
                    SQLiteDataAdapter daDC = new(DCuont, conn);
                    DataTable dtDC = new();
                    daDC.Fill(dtDC);
                    foreach (DataRow dr in dtDC.Rows)
                    {
                        for (int i = 0; i < dtDC.Rows.Count; i++)
                        {
                            if (dr["name"].ToString() == cbDiscounts.Text) strDC = dr["id"].ToString();
                        }
                    }
                    DC = new()
                {
                    new CreateDelivery.Discount
                    {
                        discountTypeId = strDC,
                        sum = Convert.ToDouble(tbSum.Text),
                        type = "RMS"
                    }
                };
                }
                conn.Close();
                List<CreateDelivery.Item> item1 = new()
                {
                    new CreateDelivery.Item
                    {
                        productId = product,
                        type = "Product",
                        amount = 1d,
                        productSizeId = null,
                        comboInformation = null,
                        comment = "Тест не готовить",
                        price = currentP
                    }
                };

                CreateDelivery.DeliveryCreate newOrder = new()
                {
                    organizationId = Organizations.orgId[0].id.ToString(),
                    terminalGroupId = strID,
                    createOrderSettings = null,
                    order = new CreateDelivery.Order
                    {
                        id = null,
                        externalNumber = null,
                        completeBefore = null,
                        phone = "+73452684305",
                        orderTypeId = str,
                        orderServiceType = null,
                        deliveryPoint = null,
                        comment = "Test",
                        customer = new CreateDelivery.Customer
                        {
                            id = null,
                            name = "Algoritm-T",
                            type = "regular"
                        },
                        guests = null,
                        marketingSourceId = null,
                        operatorId = null,
                        items = item1,
                        combos = null,
                        payments = null,
                        tips = null,
                        sourceKey = null,
                        discountsInfo = strDC != null ? new CreateDelivery.DiscountsInfo { discounts = DC, card = null } : null,
                        iikoCard5Info = null
                    },
                };
                return JsonConvert.SerializeObject(newOrder);
            }
            catch (Exception ex)
            {
                return tbViewData.Text += ex.Message;
            }
        } // Заполнение доставочного заказа
        private void SelectTables(string TName)
        {
            var conn = new SQLiteConnection("Data Source=" + insertDB.dbName + ";Version=3;");
            string TID = "";
            string tablesName = "SELECT * FROM terminalGroups WHERE name=" + "'" + TName + "'";
            SQLiteDataAdapter daTableName = new(tablesName, conn);
            DataTable dtTableName = new();
            daTableName.Fill(dtTableName);
            foreach (DataRow item in dtTableName.Rows)
            {
                TID = item["id"].ToString();
            }
            string tablesId = "SELECT * FROM restaurantSections WHERE terminalid=" + "'" + TID + "'";
            SQLiteDataAdapter daTableId = new(tablesId, conn);
            DataTable dtTableId = new();
            daTableId.Fill(dtTableId);

            foreach (DataRow dr in dtTableId.Rows)
            {
                cbTables.Items.Add($"Table: {dr["tablenumber"].ToString()}  [{dr["sectionname"].ToString()}]");
            }
            if (cbTables.Items.Count != 0)
            {
                cbTables.Text = cbTables.Items[0].ToString();
            }
            conn.Close();
        } // Загрузка столов
        private void twMenu_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                label2.Text = twMenu.SelectedNode.Text;
            }
            catch (Exception)
            {

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\test.db"))
            {
                insertDB.DropTable();
            }
            else
            {
                insertDB.CreateDB();
            }
        }
        private async void btnCreateDelivery_Click(object sender, EventArgs e)
        {
            var conn = new SQLiteConnection("Data Source=" + insertDB.dbName + ";Version=3;");
            string nullableSeqchildc = "SELECT * FROM products";
            conn.Open();
            SQLiteDataAdapter dachildmnuc2 = new(nullableSeqchildc, conn);
            DataTable dtchildc2 = new();
            dachildmnuc2.Fill(dtchildc2);
            foreach (DataRow dr in dtchildc2.Rows)
            {
                for (int i = 0; i < dtchildc2.Rows.Count; i++)
                {
                    if (dr["name"].ToString() == label2.Text)
                    {
                        currentPrice = Convert.ToDouble(dr["currentPrice"].ToString());
                        productID = dr["id"].ToString();
                    }
                }
            }
            string CID = CreateDelivery.Create(GenerateDelivery(productID, currentPrice));
            tbViewData.AppendText(await Task.Run(() => CommandStatus.Get(CID))+ Environment.NewLine);
            conn.Close();
        } // Создание доставочного заказа
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            insertDB.DropTable();
            Application.Exit();
        }
        private void cbTables_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                var conn = new SQLiteConnection("Data Source=" + insertDB.dbName + ";Version=3;");
                string nullableSeqchildc = "SELECT * FROM restaurantSections";
                conn.Open();
                SQLiteDataAdapter dachildmnuc2 = new(nullableSeqchildc, conn);
                DataTable dtchildc2 = new();
                dachildmnuc2.Fill(dtchildc2);
                if (dtchildc2.Rows.Count != 0)
                {
                    foreach (DataRow item in dtchildc2.Rows)
                    {
                        if (dtchildc2.Rows.IndexOf(item) == cbTables.SelectedIndex) tableID = item["tableid"].ToString();
                    }
                }
                else tableID = null;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "BLA BLA BLA");
            }
        }  // Присвоение ID стола в переменную для передачи в заказ
        private void cbTGroups_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbTables.Items.Clear();
            SelectTables(cbTGroups.Text);
        } // Обновление списка столов
    }
}
