using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BachecaBadanti.Controllers
{
    public class HomeController : Controller
    {

        private bakecaEntities db = new bakecaEntities();
        // GET: Annuncis
        public  ActionResult Index()
        {

            //var query = await (from p in db.Annunci
            //                   where p.Phone != null
            //                   orderby p.DataOra descending
            //                   select p).Take(200).ToListAsync();

            //ViewBag.Total = query.Count();

            var provincie = db.Annunci.GroupBy(test => test.City)
             .Select(grp => grp.FirstOrDefault())
             .Select(x =>
                     new SelectListItem
                     {
                         Value = x.City,
                         Text = x.City
                     });

            

                  


            var model = new ItalianCity { CityList = provincie };


           // return new SelectList(roles, "Value", "Text");

            //return View(await db.Annunci.OrderByDescending(p => p.DataOra).ToListAsync());
            return View(model);
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
    }

    public class ItalianCity
    {
        // Display Attribute will appear in the Html.LabelFor
        [Display(Name = "Provincie")]
        public string Selezione { get; set; }

        public IEnumerable<SelectListItem> CityList { get; set; }
    }
}