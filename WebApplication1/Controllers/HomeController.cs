using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //[ActionName("login")]
        //[HttpPost]
        public ActionResult Login()
        {
            TropicalServerEntities1 tc = new TropicalServerEntities1();
            var model = tc.tblUserLogins.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Login(string user, string pass)
        {
            TropicalServerEntities1 tc = new TropicalServerEntities1();
            var model = tc.tblUserLogins.Where(a => a.UserID == user && a.Password == pass);
            System.Diagnostics.Debug.WriteLine("user {0} pass {1}",user,pass);
            if (model.Count() == 1)
            {
                return RedirectToAction("About");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult ViewItems()
        {
            TropicalServerEntities1 tc = new TropicalServerEntities1();
            var model = (from i in tc.tblItems
                         join d in tc.tblItemTypes on i.ItemType equals d.ItemTypeNumber
                         select new joinedTbl
                         {
                             id = i.ItemID,
                             itemnum = i.ItemNumber,
                             desc = i.ItemDescription,
                             upc = i.ItemUPC,
                             shelflife = i.ItemShelfLife,
                             returnAllowed = i.ItemReturnAllowedFlag,
                             itemType = i.ItemType,
                             units = i.ItemUnits,
                             weight = i.ItemWeights,
                             priceGroup = i.ItemPriceGroup,
                             prodGroup = i.ItemProductGroup,
                             promoGroup = i.ItemPromoGroup,
                             preprice = i.PrePrice,
                             itemtypedesc = d.ItemTypeDescription
                         }).ToList();
            return View(model);
        }
    }

    public class joinedTbl
    {
        public int id { get; internal set; }
        public decimal? weight { get; internal set; }
        public int? itemnum { get; internal set; }
        public string desc { get; internal set; }
        public string upc { get; internal set; }
        public int? shelflife { get; internal set; }
        public string returnAllowed { get; internal set; }
        public int? units { get; internal set; }
        public int? priceGroup { get; internal set; }
        public int? itemType { get; internal set; }
        public string preprice { get; internal set; }
        public int? promoGroup { get; internal set; }
        public int? prodGroup { get; internal set; }
        public string itemtypedesc { get; internal set; }
    }
}