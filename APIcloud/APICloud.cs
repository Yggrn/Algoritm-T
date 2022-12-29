﻿using APIcloud.Classes;
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
    public partial class APICloud : Form
    {
        const string nullableSeqchildc = "SELECT * FROM orderTypes";
        const string TID = "SELECT * FROM terminalGroups";
        const string oID = "SELECT * FROM organizations";
        string tableID; // ID стола
        string productID; // ID блюда
        double currentPrice; // Цена выбранного блюда
        public string dataorder;
        readonly SQL_DB insertDB = new(); // Общий экземпляр класса для всех методов
        public APICloud() => InitializeComponent();
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
                
                //TreeNode childNode1;
                foreach (DataRow dr in dt.Rows)
                {

                    
                    if (dr["parentGroup"].ToString() == "NULL")
                    {
                        var parentNode = twMenu.Nodes.Add(dr["name"].ToString());
                        PopulateTreeView(dr["id"].ToString(), parentNode);
                    }
                    //else if (dr["parentGroup"].ToString() != "NULL")
                    //{
                    //      childNode1 = twMenu.Nodes.Add(dr["name"].ToString());
                    //      PopulateTreeView(dr["parentGroup"].ToString(), childNode1);
                    //}

                }
                conn.Close();
            }
            catch (Exception)
            {
            }
        } // Заполнение дерева группами блюд, вызов метода PopulateTreeView для заполнениея блюдами в качестве дочерних элемнтов дерева
        private void SelectTerminalGroups(string OrgID)
        {
            try
            {
                var conn = new SQLiteConnection("Data Source=" + insertDB.dbName + ";Version=3;");
                string oID = "";
                string tablesName = "SELECT * FROM organizations WHERE name=" + "'" + OrgID + "'";
                SQLiteDataAdapter daTableName = new(tablesName, conn);
                DataTable dtTableName = new();
                daTableName.Fill(dtTableName);
                foreach (DataRow item in dtTableName.Rows)
                {
                    oID = item["id"].ToString();
                }
                string orgId = "SELECT * FROM terminalGroups WHERE organizationID=" + "'" + oID + "'";
                SQLiteDataAdapter daTableId = new(orgId, conn);
                DataTable dtTableId = new();
                daTableId.Fill(dtTableId);

                foreach (DataRow dr in dtTableId.Rows)
                {
                    cbTGroups.Items.Add($"{dr["name"]}");
                }
                if (cbTables.Items.Count != 0)
                {
                    cbTGroups.Text = cbTGroups.Items[0].ToString();
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
                SelectTerminalGroups(cbOrganizations.Text);
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
        private void ParseLoyaltyProgamm()
        {
            var loyalty = JsonConvert.DeserializeObject<LoyaltySystem.LoyaltyProgamm>(LoyaltySystem.Get());
            for (int i = 0; i < loyalty.Programs.Count; i++)
            {
                if (loyalty.Programs[i].isActive)
                {
                    switch (loyalty.Programs[i].programType)
                    {
                        case 0:
                            tbCard.AppendText($"{loyalty.Programs[i].name} [Депозит / кор. пит]" +Environment.NewLine);
                            break;
                        case 1:
                            tbCard.AppendText($"{loyalty.Programs[i].name} [Бонусная]" +Environment.NewLine);
                            break;
                        case 2:
                            tbCard.AppendText($"{loyalty.Programs[i].name} [Продуктовая]" +Environment.NewLine);
                            break;
                        case 3:
                            tbCard.AppendText($"{loyalty.Programs[i].name} [Скидочная]" +Environment.NewLine);
                            break;
                        case 4:
                            tbCard.AppendText($"{loyalty.Programs[i].name} [Сертификат]" +Environment.NewLine);
                            break;
                        default:
                            break;
                    }

                    tbCard.AppendText($"Период действия: {loyalty.Programs[i].serviceFrom} - {loyalty.Programs[i].serviceTo}"+Environment.NewLine);
                    if (loyalty.Programs[i].hasWelcomeBonus) tbCard.AppendText($"WelcomeBonus: {loyalty.Programs[i].welcomeBonusSum}"+Environment.NewLine);
                    tbCard.AppendText("Маркетинговые акции:"+ Environment.NewLine);
                    for (int j = 0; j < loyalty.Programs[i].marketingCampaigns.Count; j++)
                    {
                        if (loyalty.Programs[i].marketingCampaigns[j].isActive)
                        {
                            tbCard.AppendText($"        -{loyalty.Programs[i].marketingCampaigns[j].name}"+Environment.NewLine);
                            tbCard.AppendText($"        -{loyalty.Programs[i].marketingCampaigns[j].periodFrom} - {loyalty.Programs[i].marketingCampaigns[j].periodTo}"+Environment.NewLine);
                        }

                    }
                }

            }
        } // Get Marketing Compaings
        private async void button1_Click(object sender, EventArgs e) // Выполнение методов для заполнения формы
        {
            insertDB.DropTable();
            cbOrganizations.Items.Clear();
            cbTGroups.Items.Clear();
            cbOrderTypes.Items.Clear();
            cbDiscounts.Items.Clear();
            cbTables.Items.Clear();
            twMenu.Nodes.Clear();
            tbViewData.Clear();
            tbCard.Clear();
            try
            {
                Enabled = false;
                progressBar1.Visible = true;
                await Task.Run(() => { TokenKey.Get(token.Text); }); // Получение токена
                Organizations.Get();
                await Task.Run(() => { insertDB.InsertToDB(); }); // Запись БД из JSON
                SelectOrganization();
                SelectGroups();
                //notParentGroupTreeView();
                SelectOrderTypes();
                SelectDiscounts();
                ParseLoyaltyProgamm();
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
            //string Seqchildc = "SELECT id,name, parentGroup FROM products WHERE parentGroup=" + "'" + parentId + "'" + "";
            string Seqchildc = "SELECT id,name, parentGroup FROM products";
            string Groups = "SELECT id,name, parentGroup FROM groups";

            SQLiteDataAdapter dachildmnuc2 = new(Groups, conn);
            DataTable dtchildc2 = new();
            SQLiteDataAdapter dachildmnuc = new(Seqchildc, conn);
            DataTable dtchildc = new();
            //conn.Open();
            dachildmnuc.Fill(dtchildc);
            TreeNode childNode;

            conn.Open();
            dachildmnuc2.Fill(dtchildc2);
            TreeNode childNode1;
            TreeNode childNode2;
            foreach (DataRow dr in dtchildc2.Rows)
            {
                if (parentId == dr["parentGroup"].ToString())
                {
                    childNode1 = parentNode.Nodes.Add(dr["name"].ToString());
                    PopulateTreeView(dr["name"].ToString(), childNode1);
                    foreach (DataRow dr2 in dtchildc.Rows)
                    {
                        if (dr2["parentGroup"].ToString() == dr["id"].ToString())
                        {
                            childNode2 = childNode1.Nodes.Add(dr2["name"].ToString());
                            PopulateTreeView(dr2["name"].ToString(), childNode2);
                        }

                    }
                    
                }
                else
                {
                    foreach (DataRow dr2 in dtchildc.Rows)
                    {
                        if (dr2["parentGroup"].ToString() == dr["id"].ToString())
                        {
                            childNode = parentNode.Nodes.Add(dr2["name"].ToString());
                            PopulateTreeView(dr2["name"].ToString(), childNode);
                        }
                        //if (parentNode == null)
                        //    childNode = twMenu.Nodes.Add(dr["name"].ToString());
                        //else
                    }
                }

             }


            //else 
            //    childNode1 = parentNode.Nodes.Add(dr["name"].ToString());
            //PopulateTreeView(dr["name"].ToString(), childNode1);





            conn.Close();
        }
        //private void notParentGroupTreeView()
        //{
        //    try
        //    {
        //        var conn = new SQLiteConnection("Data Source=" + insertDB.dbName + ";Version=3;");
        //        string nullableSeqchildc = "SELECT * FROM products WHERE parentGroup='NULL'";
        //        conn.Open();
        //        SQLiteDataAdapter dachildmnuc2 = new(nullableSeqchildc, conn);
        //        DataTable dtchildc2 = new();
        //        dachildmnuc2.Fill(dtchildc2);
        //        foreach (DataRow dr in dtchildc2.Rows)
        //        {
        //            if (!twMenu.Nodes.Equals(dr["name"]))
        //            {
        //                twMenu.Nodes.Add(dr["name"].ToString());
        //            }
        //        }
        //        conn.Close();
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
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
        private string GetIdfromDB(string data, string cb)
        {
            var conn = new SQLiteConnection("Data Source=" + insertDB.dbName + ";Version=3;");
            conn.Open();
            SQLiteDataAdapter dachildmnuc3 = new(data, conn);
            DataTable dtchildc3 = new();
            dachildmnuc3.Fill(dtchildc3);
            string ID = null;
            foreach (DataRow dr in dtchildc3.Rows)
            {
                if (dr["name"].ToString() == cb) ID = dr["id"].ToString();
            }

            return ID;
        }
        private string GenerateOrder(string product, double currentP)
        {
            string strID = GetIdfromDB(TID, cbTGroups.Text);
            string str = GetIdfromDB(nullableSeqchildc, cbOrderTypes.Text);
            string strOrgId = GetIdfromDB(oID, cbOrganizations.Text);
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
                organizationId = strOrgId,
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

                string strID = GetIdfromDB(TID, cbTGroups.Text);
                string str = GetIdfromDB(nullableSeqchildc, cbOrderTypes.Text);
                string strOrgId = GetIdfromDB(oID, cbOrganizations.Text);
                List<CreateDelivery.Discount> DC = new();
                string strDC = null;
                if (checkDiscount.Checked)
                {
                    var conn = new SQLiteConnection("Data Source=" + insertDB.dbName + ";Version=3;");
                    conn.Open();
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
                    conn.Close();
                }

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
                    organizationId = strOrgId,
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
            cbTables.Items.Clear();
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
                cbTables.Items.Add($"Table: {dr["tablenumber"]}  [{dr["sectionname"]}]");
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
                MessageBox.Show(ex.Message, "Error");
            }
        }  // Присвоение ID стола в переменную для передачи в заказ
        private void cbTGroups_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbTables.Items.Clear();
            SelectTables(cbTGroups.Text);
        } // Обновление списка столов
        private void cbOrganizations_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbTGroups.Items.Clear();
            SelectTerminalGroups(cbOrganizations.Text);
        } // Обновление списка Терминалов
    }
}
