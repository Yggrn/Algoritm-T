using AlgoritmWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static AlgoritmWeb.Models.Menus;
using static AlgoritmWeb.Models.Organizations;

namespace AlgoritmWeb.Controllers
{
    public class AddOrganization : Controller
    {
        HttpClient httpClient;
        public static bool err = false;
        public static TokenKey? dataToken;
        public static string token = string.Empty;
        public static List<OrganizationsArray> OrganizationsList { get; set; } = new List<OrganizationsArray>();
        public static List<ItemsArray> terminals { get; set; } = new List<ItemsArray>();
        public static List<ItemOrder> orderTypes { get; set; } = new List<ItemOrder>();
        public static List<Discounts.Item> _Discounts { get; set; } = new List<Discounts.Item>();
        ApplicationContext? db;
        public AddOrganization(ApplicationContext context)
        {

            db = context;
            dataToken = new();
            httpClient = new();
            httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("MessageService/3.1");
        }
        public async Task<IActionResult> GetTab(string TerminalId)
        {
            List<string> list = new List<string>();
            list.Add(TerminalId);
            await Task.WhenAll(GetTables(list));
            return View("/Views/WorkSpace/Index.cshtml");
        }
        async Task GetTables(List<string> terminalIds)
        {
            var url = "https://api-ru.iiko.services/api/1/reserve/available_restaurant_sections";
            try
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var data = $"{{\"terminalGroupIds\":{JsonConvert.SerializeObject( terminalIds)}, \"returnSchema\":false, \"revision\":0}}";
                var response = await httpClient.PostAsync(url, new StringContent(data, encoding: System.Text.Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                Tables.GetTable? _tab = JsonConvert.DeserializeObject<Tables.GetTable>(result);
                for (int i = 0; i < _tab.restaurantSections.Count; i++)
                {
                    for (int j = 0; j < _tab.restaurantSections[i].tables.Count; j++)
                    {

                        if (db.tables.Any(x => x.id == _tab.restaurantSections[i].tables[j].id && x.tID == _tab.restaurantSections[i].terminalGroupId.ToString()))
                        {
                            db.tables.Where(x => x.id == _tab.restaurantSections[i].tables[j].id && x.tID == _tab.restaurantSections[i].terminalGroupId.ToString()).ToList().ForEach(x =>
                            {
                                x.name = _tab.restaurantSections[i].tables[j].name;
                                x.isDeleted = _tab.restaurantSections[i].tables[j].isDeleted;
                                x.number = _tab.restaurantSections[i].tables[j].number;
                                x.tID = _tab.restaurantSections[i].terminalGroupId.ToString();
                                //x.productCategoryDiscounts = new List<Discounts.ProductCategoryDiscount>
                                //{

                                //};
                                x.revision =_tab.restaurantSections[i].tables[j].revision;
                                x.seatingCapacity = _tab.restaurantSections[i].tables[j].seatingCapacity;
                            });
                            await db!.SaveChangesAsync();
                        }
                        else
                        {
                            _tab.restaurantSections[i].tables[j].tID = _tab.restaurantSections[i].terminalGroupId.ToString();
                            db!.tables.Add(_tab.restaurantSections[i].tables[j]);
                        }
                    }
                    await db!.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                err = true;
            }
        }
                async Task GetDiscounts()
        {
            var url = "https://api-ru.iiko.services/api/1/discounts";
            try
            {
                var data = $"{{\"organizationIds\":{JsonConvert.SerializeObject(GetDataOrg())}}}";
                var response = await httpClient.PostAsync(url, new StringContent(data, encoding: System.Text.Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                Discounts.Root? discounts = JsonConvert.DeserializeObject<Discounts.Root>(result);
                for (int i = 0; i < discounts!.discounts.Count; i++)
                {
                    for (int j = 0; j < discounts.discounts[i].items.Count; j++)
                    {

                    
                    if (db.discountItems.Any(x => x.id == discounts!.discounts[i].items[j].id && x.OID == discounts.discounts[i].organizationId.ToString()))
                    {
                        db.discountItems.Where(x => x.id == discounts!.discounts[i].items[j].id && x.OID == discounts.discounts[i].organizationId.ToString()).ToList().ForEach(x =>
                        {
                            x.name = discounts!.discounts[i].items[j].name;
                            x.percent = discounts!.discounts[i].items[j].percent;
                            x.isCategorisedDiscount = discounts!.discounts[i].items[j].isCategorisedDiscount;
                            x.OID = discounts.discounts[i].organizationId.ToString();
                            //x.productCategoryDiscounts = new List<Discounts.ProductCategoryDiscount>
                            //{

                            //};
                            x.comment = discounts!.discounts[i].items[j].comment;
                            x.canBeAppliedSelectively = discounts!.discounts[i].items[j].canBeAppliedSelectively;
                            x.canApplyByCardNumber = discounts!.discounts[i].items[j].canApplyByCardNumber;
                            x.minOrderSum = discounts!.discounts[i].items[j].minOrderSum;
                            x.mode = discounts!.discounts[i].items[j].mode;
                            x.sum = discounts!.discounts[i].items[j].sum;
                            x.isManual = discounts!.discounts[i].items[j].isManual;
                            x.isCard = discounts!.discounts[i].items[j].isCard;
                            x.isDeleted = discounts!.discounts[i].items[j].isDeleted;
                            x.isAutomatic = discounts!.discounts[i].items[j].isAutomatic;

                        });
                        await db!.SaveChangesAsync();
                    }
                    else
                    {
                        discounts!.discounts[i].items[j].OID = discounts.discounts[i].organizationId.ToString();
                        db!.discountItems.Add(discounts!.discounts[i].items[j]);
                    }
                    }
                }
                
                await db!.SaveChangesAsync();
            }
            catch (Exception e)
            {
                err = true;
            }
        }
        async Task GetOrderTypes()
        {
            var url = "https://api-ru.iiko.services/api/1/deliveries/order_types";
            try
            {
                var data = $"{{\"organizationIds\":{JsonConvert.SerializeObject(GetDataOrg())}}}";
                var response = await httpClient.PostAsync(url, new StringContent(data, encoding: System.Text.Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                RootOrderTypes? orderTypes = JsonConvert.DeserializeObject<RootOrderTypes>(result);
                for (int i = 0; i < orderTypes!.orderTypes.Count; i++)
                {
                    for (int j = 0; j < orderTypes.orderTypes[i].items.Count; j++)
                    {
                        if (db.ItemOrders.Any(x => x.id == orderTypes.orderTypes[i].items[j].id && x.OID == orderTypes.orderTypes[i].organizationId.ToString()))
                        {
                            db.ItemOrders.Where(x => x.id == orderTypes.orderTypes[i].items[j].id && x.OID == orderTypes.orderTypes[i].organizationId.ToString()).ToList().ForEach(x =>
                            {
                                x.externalRevision = orderTypes.orderTypes[i].items[j].externalRevision;
                                x.isDeleted = orderTypes.orderTypes[i].items[j].isDeleted;
                                x.orderServiceType = orderTypes.orderTypes[i].items[j].orderServiceType;
                                x.OID = orderTypes.orderTypes[i].organizationId.ToString();
                                x.name = orderTypes.orderTypes[i].items[j].name;
                            });
                            await db!.SaveChangesAsync();
                        }
                        else
                        {
                            orderTypes.orderTypes[i].items[j].OID = orderTypes.orderTypes[i].organizationId.ToString();
                            db!.OrderTypes.Add(orderTypes.orderTypes[i]);
                        }
                    }
                }
                await db!.SaveChangesAsync();
            }
            catch
            {
                err = true;
            }
        }
        async Task GetToken(string? apitoken)
        {
            if (apitoken != null)
            {
                try
                {
                    string url = "https://api-ru.iiko.services/api/1/access_token";
                    var data = $"{{\"apiLogin\": \"{apitoken}\"}}";
                    var response = await httpClient.PostAsync(url, new StringContent(data, encoding: System.Text.Encoding.UTF8, "application/json"));
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();
                    TokenKey? tkn = JsonConvert.DeserializeObject<TokenKey>(result);
                    dataToken!.correlationId = tkn!.correlationId;
                    token = tkn!.token;
                    dataToken.token = tkn.token;
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + dataToken!.token);
                    await Task.WhenAll(GetOrganizatons(apitoken));
                    await Task.WhenAll(GetTerminalGroups());
                    await Task.WhenAll(GetMenus());
                    await Task.WhenAll(GetOrderTypes());
                    await Task.WhenAll(GetDiscounts());
                }
                catch
                {
                    err = true;
                }
            }
        }
        async Task GetOrganizatons(string key)
        {
            var data = @"{""organizationIds"": null,
                              ""returnAdditionalInfo"": true,
                              ""includeDisabled"": false}"
            ;
            var url = "https://api-ru.iiko.services/api/1/organizations";
            try
            {
                var response = await httpClient.PostAsync(url, new StringContent(data, encoding: System.Text.Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                Organizations_? org = JsonConvert.DeserializeObject<Organizations_>(result);

                for (int i = 0; i < org!.organizations!.Count; i++)
                {
                    orgId = org.organizations;
                    org.organizations[i].apiToken = key;
                    if (db.organizations.Any(x => x.id == org.organizations[i].id))
                    {
                        db.organizations.Where(x => x.id == org.organizations[i].id).ToList().ForEach(x =>
                        {
                            x.version = org.organizations[i].version;
                            x.name = org.organizations[i].name;
                            x.isCloud = org.organizations[i].isCloud;
                            x.inn = org.organizations[i].inn;
                            x.latitude = org.organizations[i].latitude;
                            x.currencyIsoName = org.organizations[i].currencyIsoName;
                            x.longitude = org.organizations[i].longitude;
                            x.restaurantAddress = org.organizations[i].restaurantAddress;
                            x.useUaeAddressingSystem = org.organizations[i].useUaeAddressingSystem;
                            x.responseType = org.organizations[i].responseType;
                        });

                        await db.SaveChangesAsync();
                    }

                    else db.organizations.Add(org.organizations[i]);
                }
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (orgId == null) err = true;
            }
        }
        private static List<string> GetDataOrg()
        {

            List<string> str = new List<string>();
            try
            {
                str = orgId!.Select(x => x.id!).ToList();
                return str;
            }
            catch
            {
                return str = null;
            }
        }
        async Task GetTerminalGroups()
        {
            var url = "https://api-ru.iiko.services/api/1/terminal_groups";
            try
            {
                var data = $"{{\"organizationIds\":{JsonConvert.SerializeObject(GetDataOrg())}, \"includeDisabled\": true}}";
                var response = await httpClient.PostAsync(url, new StringContent(data, encoding: System.Text.Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                TerminaGroups? term = JsonConvert.DeserializeObject<TerminaGroups>(result);

                for (int i = 0; i < term!.terminalGroups!.Count; i++)
                {
                    for (int j = 0; j < term!.terminalGroups[i]!.items!.Count; j++)
                    {
                        if (db.terminalGroups.Any(o => o.id == term!.terminalGroups[i]!.items[j].id))
                        {
                            db.terminalGroups.Where(x => x.id == term!.terminalGroups[i]!.items[j].id).ToList().ForEach(x =>
                            {
                                x.address = term!.terminalGroups[i]!.items[j].address;
                                x.name = term!.terminalGroups[i]!.items[j].name;
                                x.timeZone = term!.terminalGroups[i]!.items[j].timeZone;
                                x.organizationId = term!.terminalGroups[i]!.items[j].organizationId;
                            });
                            await db.SaveChangesAsync();
                        }
                        else db.terminalGroups!.Add(term!.terminalGroups[i]!.items[j]);
                    }
                }
                await db.SaveChangesAsync();
            }
            catch
            {
                err = true;
            }
        }
        async Task GetMenus()
        {
            var url = "https://api-ru.iiko.services/api/1/nomenclature";
            try
            {
                var data = $"{{\"organizationId\":{JsonConvert.SerializeObject(orgId[0].id)},\"startRevision\": 0}}";
                var response = await httpClient.PostAsync(url, new StringContent(data, encoding: System.Text.Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                Menus.Root? root = JsonConvert.DeserializeObject<Menus.Root>(result);
                foreach (var item in root.groups)
                {
                    if (db.menusGroups.Any(o => o.id == item.id))
                    {
                        db.menusGroups.Where(x => x.id == item.id).ToList().ForEach(x =>
                        {
                            x.code = item.code;
                            x.name = item.name;
                            x.isDeleted = item.isDeleted;
                            x.parentGroup = item.parentGroup;
                            x.isIncludedInMenu = item.isIncludedInMenu;
                            x.isGroupModifier = item.isGroupModifier;
                            x.organizationId = orgId[0].id;
                        });

                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        item.organizationId = orgId[0].id;
                        db.menusGroups.Add(item);
                    }
                }
                foreach (var item in root.products)
                {
                    if (db.menusProducts.Any(o => o.id == item.id))
                    {
                        db.menusProducts.Where(x => x.id == item.id).ToList().ForEach(x =>
                        {
                            x.code = item.code;
                            x.name = item.name;
                            x.isDeleted = item.isDeleted;
                            x.parentGroup = item.parentGroup;
                            x.groupId = item.groupId;
                            x.productCategoryId = item.productCategoryId;
                            x.price = item.sizePrices[0].price.currentPrice;
                            x.type = item.type;
                            x.measureUnit = item.measureUnit;
                            x.canSetOpenPrice = item.canSetOpenPrice;
                            x.description = item.description;
                            x.organizationId = orgId[0].id;
                        });

                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        item.organizationId = orgId[0].id;
                        item.price = item.sizePrices[0].price.currentPrice;
                        db.menusProducts.Add(item);
                    }
                }
                await db.SaveChangesAsync();
                db.Database.CloseConnection();
            }
            catch (Exception ex)
            {

            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrg(string key)
        {
            await Task.WhenAll(GetToken(key));
            OrganizationsList = db.organizations.ToList();
            terminals = db.terminalGroups.ToList();
            orderTypes = db.ItemOrders.ToList();
            _Discounts =db.discountItems.ToList();
            return View("/Views/WorkSpace/Index.cshtml");
        }

        public IActionResult AddOrg()
        { return View("/Views/WorkSpace/AddOrg.cshtml"); }
        public IActionResult Index()
        {
            WorkSpaceController.items = null;
            OrganizationsList = db.organizations.ToList();
            terminals = db.terminalGroups.ToList();
            orderTypes = db.ItemOrders.ToList();
            _Discounts = db.discountItems.ToList();
            return View("/Views/WorkSpace/Index.cshtml");
        }
    }


}
