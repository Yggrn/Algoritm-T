using AlgoritmWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;

namespace AlgoritmWeb.Controllers
{
    public class WorkSpaceController : Controller
    {
        ApplicationContext db;
        public WorkSpaceController(ApplicationContext context)
        {
            db = context;   
        }
         public static string terminal = string.Empty;
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TID(string Organization)
        {
            terminal = Organization;
            await Task.WhenAll(TreeView());
            items = null;
            return View("Index");
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
            List<TreeViewNode> nodes = (from Menus.Group type in db.menusGroups
                                        where type.organizationId == terminal && type.parentGroup == null && type.isGroupModifier == false
                                        select new TreeViewNode { id = type.id.ToString(), parent = "#", text = type.name }).ToList();
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
                        nodes.Add(new TreeViewNode { id = item.id.ToString() + "*" + item.id.ToString(), parent = item.parentGroup.ToString(), text = item.name + $" [{item.price}p]"});
                    }
                    
                }
            }
         
            db.Database.CloseConnection();
            ViewBag.Json = JsonConvert.SerializeObject(nodes);
            }
            catch
            {
            
            }
        }
    public static  List<TreeViewNode>? items;
        [HttpPost]
        public IActionResult CreateOrder(string selectedItems)
        {
            items = JsonConvert.DeserializeObject<List<TreeViewNode>>(selectedItems);
             Task.WhenAll(TreeView());
            return View("Index");
        }
    }
}