using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SQLite;

namespace APIcloud
{
    class SQL_DB
    {

        public string dbName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\test.db";
        SQLiteCommand command = new();
        public void DropTable()
        {
            SQLiteConnection db = new("Data Source=" + dbName + ";Version=3;");
            try
            {
                db.Open();
                command.Connection = db;
                command.CommandText = "delete FROM organizations; REINDEX organizations; VACUUM";
                command.ExecuteNonQuery();
                command.CommandText = "delete FROM orderTypes; REINDEX orderTypes; VACUUM";
                command.ExecuteNonQuery();
                command.CommandText = "delete FROM restaurantSections; REINDEX restaurantSections; VACUUM";
                command.ExecuteNonQuery();
                command.CommandText = "delete FROM terminalGroups; REINDEX terminalGroups; VACUUM";
                command.ExecuteNonQuery();
                command.CommandText = "delete FROM discounts; REINDEX discounts; VACUUM";
                command.ExecuteNonQuery();
                command.CommandText = "delete FROM groups; REINDEX groups; VACUUM";
                command.ExecuteNonQuery();
                command.CommandText = "delete FROM productCategories; REINDEX productCategories; VACUUM";
                command.ExecuteNonQuery();
                command.CommandText = "delete FROM products; REINDEX products; VACUUM";
                command.ExecuteNonQuery();
                command.CommandText = "delete FROM sizes; REINDEX sizes; VACUUM";
                command.ExecuteNonQuery();
                command.CommandText = "delete FROM revision; REINDEX revision; VACUUM";
                command.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception)
            {

            }
        }
        public void CreateDB()
        {
            SQLiteConnection db = new("Data Source=" + dbName + ";Version=3;");
            try
            {
                SQLiteConnection.CreateFile(dbName);
                db.Open();
                command.Connection = db;
                command.CommandText = $"CREATE TABLE IF NOT EXISTS organizations (id TEXT PRIMARY KEY, " +
                "name TEXT)";
                command.ExecuteNonQuery();
                command.CommandText = $"CREATE TABLE IF NOT EXISTS restaurantSections (tableid TEXT, " +
                "name TEXT, tablenumber INTEGER, sectionid TEXT, sectionname TEXT, terminalid TEXT)";
                command.ExecuteNonQuery();
                command.CommandText = $"CREATE TABLE IF NOT EXISTS terminalGroups (id TEXT PRIMARY KEY, " +
                "name TEXT, organizationID TEXT)";
                command.ExecuteNonQuery();
                command.CommandText = $"CREATE TABLE IF NOT EXISTS discounts (id TEXT PRIMARY KEY, " +
                "name TEXT, persent TEXT)";
                command.ExecuteNonQuery();
                command.CommandText = $"CREATE TABLE IF NOT EXISTS groups (id TEXT PRIMARY KEY, " +
                "name TEXT, isDeleted TEXT, isGroupModifier TEXT, parentGroup TEXT)";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE IF NOT EXISTS productCategories (id TEXT PRIMARY KEY, " +
                "name TEXT)";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE IF NOT EXISTS products (id TEXT, " +
                "name TEXT, parentGroup TEXT, currentPrice TEXT)";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE IF NOT EXISTS sizes (id TEXT PRIMARY KEY, " +
                "name TEXT)";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE IF NOT EXISTS revision (revision TEXT PRIMARY KEY, " +
                    "name TEXT)";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE IF NOT EXISTS orderTypes (id TEXT PRIMARY KEY, " +
                    "name TEXT)";
                command.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception)
            {

            }
        }
        public void InsertToDB()
        {
            SQLiteConnection db = new("Data Source=" + dbName + ";Version=3;");
            db.Open();
            if (db.State == ConnectionState.Open)
            {
                var menu = JsonConvert.DeserializeObject<Menus.Root>(Menus.Get());
                var org = JsonConvert.DeserializeObject<Organizations.Organizations_>(Organizations.Get());
                var term = JsonConvert.DeserializeObject<TerminalGroups.TerminaGroups_>(TerminalGroups.Get());
                var discounts = JsonConvert.DeserializeObject<Discounts.Root>(Discounts.Get());
                var orderTypes = JsonConvert.DeserializeObject<Menus.RootOrderTypes>(OrderTypes.Get());
                try
                {
                    SQLiteCommand sqlgroups = new(@"INSERT INTO groups('id', 'name', 'isDeleted', 'isGroupModifier', 'parentGroup') values (@id, @name, @isDeleted, @isGroupModifier, @parentGroup)", db);
                    foreach (var grp in menu.groups)
                    {
                        sqlgroups.Parameters.AddWithValue("@id", grp.id);
                        sqlgroups.Parameters.Add("@name", DbType.String).Value = grp.name;
                        sqlgroups.Parameters.Add("@isDeleted", DbType.String).Value = grp.isDeleted.ToString();
                        sqlgroups.Parameters.Add("@isGroupModifier", DbType.String).Value = grp.isGroupModifier.ToString();
                        if (grp.parentGroup != null) sqlgroups.Parameters.Add("@parentGroup", DbType.String).Value = grp.parentGroup.ToString(); 
                        else sqlgroups.Parameters.Add("@parentGroup", DbType.String).Value = "NULL";
                        sqlgroups.ExecuteNonQuery();
                    }
                    SQLiteCommand sqlproductCategories = new(@"INSERT INTO productCategories ('id', 'name') values (@id, @name)", db);
                    foreach (var pcategories in menu.productCategories)
                    {
                        sqlproductCategories.Parameters.AddWithValue("@id", pcategories.id);
                        sqlproductCategories.Parameters.Add("@name", DbType.String).Value = pcategories.name;
                        sqlproductCategories.ExecuteNonQuery();
                    }
                    SQLiteCommand sqlProducts = new(@"INSERT INTO products ('id', 'name', 'parentGroup', 'currentPrice') values (@id,@name,@parentGroup, @currentPrice)", db);
                    foreach (var products in menu.products)
                    {
                        sqlProducts.Parameters.AddWithValue("@id", products.id);
                        sqlProducts.Parameters.Add("@name", DbType.String).Value = products.name;
                        if (products.parentGroup == null) sqlProducts.Parameters.Add("@parentGroup", DbType.String).Value = "NULL";
                        else sqlProducts.Parameters.Add("@parentGroup", DbType.String).Value = products.parentGroup;
                        sqlProducts.Parameters.Add("@currentPrice", DbType.String).Value = products.sizePrices[0].price.currentPrice.ToString();
                        sqlProducts.ExecuteNonQuery();
                    }
                    SQLiteCommand sqlterminalGroups = new(@"INSERT INTO terminalGroups ('id', 'name', 'organizationID') values (@id,@name,@organizationID)", db);
                    for (int i = 0; i < term.terminalGroups.Count; i++)
                    {
                        for (int j = 0; j < term.terminalGroups[i].items.Count; j++)
                        {
                            sqlterminalGroups.Parameters.AddWithValue("@id", term.terminalGroups[i].items[j].id);
                            sqlterminalGroups.Parameters.Add("@name", DbType.String).Value = term.terminalGroups[i].items[j].name;
                            sqlterminalGroups.Parameters.Add("@organizationID", DbType.String).Value = term.terminalGroups[i].items[j].organizationId;
                            sqlterminalGroups.ExecuteNonQuery();
                        }

                    }
                    SQLiteCommand sqlOrganizations = new(@"INSERT INTO organizations ('id', 'name') values (@id, @name)", db);
                    foreach (var orgitm in org.organizations)
                    {
                        sqlOrganizations.Parameters.AddWithValue("@id", orgitm.id);
                        sqlOrganizations.Parameters.Add("@name", DbType.String).Value = orgitm.name;
                        sqlOrganizations.ExecuteNonQuery();

                    }
                    SQLiteCommand sqlSizes = new(@"INSERT INTO sizes ('id', 'name') values (@id, @name)", db);
                    foreach (var sizes in menu.sizes)
                    {
                        sqlSizes.Parameters.AddWithValue("@id", sizes.id);
                        sqlSizes.Parameters.Add("@name", DbType.String).Value = sizes.name;
                        sqlSizes.ExecuteNonQuery();

                    }
                    SQLiteCommand sqlDiscounts = new(@"INSERT INTO discounts ('id', 'name', 'persent') values (@id, @name, @persent)", db);
                    for (int i = 0; i < discounts.discounts.Count; i++)
                    {
                        for (int j = 0; j < discounts.discounts[i].items.Count; j++)
                        {
                            sqlDiscounts.Parameters.AddWithValue("id", discounts.discounts[i].items[j].id);
                            sqlDiscounts.Parameters.Add("name", DbType.String).Value = discounts.discounts[i].items[j].name;
                            sqlDiscounts.Parameters.Add("persent", DbType.String).Value = discounts.discounts[i].items[j].percent.ToString();
                            sqlDiscounts.ExecuteNonQuery();
                        }
                    }
                    SQLiteCommand sqlOrderT = new(@"INSERT INTO orderTypes ('id', 'name') values (@id, @name)", db);
                    foreach (var orderT in orderTypes.orderTypes[0].items)
                    {
                        sqlOrderT.Parameters.AddWithValue("@id", orderT.id);
                        sqlOrderT.Parameters.Add("@name", DbType.String).Value = orderT.name;
                        sqlOrderT.ExecuteNonQuery();
                    }
                    SQLiteCommand sqlRevision = new(@"INSERT INTO revision ('revision', 'name') values (@revision, @name)", db);
                    sqlRevision.Parameters.AddWithValue("@revision", menu.revision);
                    sqlRevision.Parameters.Add("@name", DbType.String).Value = "MENU";
                    sqlRevision.ExecuteNonQuery();
                    string Seqchildc = "SELECT id FROM terminalGroups";
                    SQLiteDataAdapter dachildmnuc = new(Seqchildc, db);
                    DataTable dtchildc = new();
                    dachildmnuc.Fill(dtchildc);
                    SQLiteCommand sqlTables = new(@"INSERT INTO restaurantSections ('tableid', 'name', 'tablenumber', 'sectionid', 'sectionname', 'terminalid') values (@tableid, @name, @number, @sectionid, @sectionname, @terminalid)", db);
                    foreach (DataRow dr in dtchildc.Rows)
                    {
                        var tables = JsonConvert.DeserializeObject<Tables.GetTable>(Tables.Get(dr["id"].ToString()));
                        for (int i = 0; i < tables.restaurantSections.Count; i++)
                        {
                            for (int j = 0; j < tables.restaurantSections[i].tables.Count; j++)
                            {
                                sqlTables.Parameters.AddWithValue("tableid", tables.restaurantSections[i].tables[j].id);
                                sqlTables.Parameters.Add("name", DbType.String).Value = tables.restaurantSections[i].tables[j].name;
                                sqlTables.Parameters.Add("sectionid", DbType.String).Value = tables.restaurantSections[i].id;
                                sqlTables.Parameters.Add("number", DbType.Int32).Value = tables.restaurantSections[i].tables[j].number;
                                sqlTables.Parameters.Add("sectionname", DbType.String).Value = tables.restaurantSections[i].name;
                                sqlTables.Parameters.Add("terminalid", DbType.String).Value = dr["id"].ToString();
                                sqlTables.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    db.Close();
                }
            }
        }
    }
}
