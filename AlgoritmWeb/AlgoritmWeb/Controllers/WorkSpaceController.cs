using AlgoritmWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using static AlgoritmWeb.Models.Organizations;

namespace AlgoritmWeb.Controllers
{
    public class WorkSpaceController : Controller
    {
        public static ApplicationContext db;
        public static string terminal = string.Empty;
        public WorkSpaceController(ApplicationContext context)
        {
            db = context;
            dataToken = new();
            httpClient = new();
            httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("MessageService/3.1");
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> TID(string Organization)
        {
            terminal = Organization;
            await Task.WhenAll(TreeView());
            return View("Index");
        }
        HttpClient httpClient;
        public static bool err = false;
        public static TokenKey? dataToken;


        [HttpPost]
        public async Task<IActionResult> Data(string key)
        {
            await Task.WhenAll(GetToken(key));
            await Task.WhenAll(GetOrganizatons(key));
            await Task.WhenAll(GetTerminalGroups());
            await Task.WhenAll(GetMenus());
            return Redirect("Index");
        }
        async Task GetToken(string? apitoken)
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
                dataToken.token = tkn.token;
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + dataToken!.token);
            }
            catch (Exception)
            {
                err = true;
            }
        }
        async Task GetOrganizatons(string key)
        {
            var data = @"{""organizationIds"": null,
                              ""returnAdditionalInfo"": true,
                              ""includeDisabled"": false}";

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
                    db.organizations.Add(org.organizations[i]);
                }

                await db.SaveChangesAsync();

            }
            catch (Exception)
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
            catch (Exception)
            {
               return str=null;
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
                        if (db.terminalGroups.Any(o => o.id == term!.terminalGroups[i]!.items[j].id)) return;
                        db.terminalGroups!.Add(term!.terminalGroups[i]!.items[j]);
                    }
                }
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
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
                    item.organizationId = orgId[0].id;
                    db.menusGroups.Add(item);

                }
                foreach (var item in root.products)
                {
                    item.organizationId = orgId[0].id;
                    db.menusProducts.Add(item);
                }

                await db.SaveChangesAsync();
                db.Database.CloseConnection();

            }
            catch (Exception ex)
            {

            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task TreeView()
        {
            try
            {

         
            List<TreeViewNode> nodes = new List<TreeViewNode>();
            foreach (Menus.Group type in db.menusGroups)
            {
                if (type.organizationId == terminal && type.parentGroup == null && type.isGroupModifier == false)
                {
                    nodes.Add(new TreeViewNode { id = type.id.ToString(), parent = "#", text = type.name });
                }
            }
            foreach (Menus.Group subType in db.menusGroups)
            {
                if (subType.organizationId == terminal && subType.parentGroup != null && subType.isGroupModifier == false)
                {
                    nodes.Add(new TreeViewNode { id = subType.id.ToString(), parent = subType.parentGroup.ToString(), text = subType.name });
                }
                foreach (Menus.Product item in db.menusProducts)
                {
                    if (item.parentGroup != null && item.organizationId == terminal && item.parentGroup == subType.id  && subType.isGroupModifier == false)
                    {
                        List<double> price = db.menusProducts.Where(x=>x.id == item.id).Select(x=>x.sizePrices.Select(x=>x.price).Select(x=>x.currentPrice)).FirstOrDefault().ToList();

                        nodes.Add(new TreeViewNode { id = item.id.ToString() + "*" + item.id.ToString(), parent = item.parentGroup.ToString(), text = item.name + $" [{price[0]}p]"});
                    }
                    
                }
            }
         
            db.Database.CloseConnection();
            ViewBag.Json = JsonConvert.SerializeObject(nodes);
            }
            catch (Exception ex)
            {

                
            }
        }

        //[HttpPost]
        //public IActionResult Index(string selectedItems)
        //{
        //    List<TreeViewNode> items = JsonConvert.DeserializeObject<List<TreeViewNode>>(selectedItems);
        //    return RedirectToAction("Index");
        //}
    }
}