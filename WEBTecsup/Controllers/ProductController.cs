using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WEBTecsup.Models;

namespace WEBTecsup.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public async Task<ActionResult> Index()
        {
            List<ProductModel> model = new List<ProductModel>();
            var client = new HttpClient();
            var urlBase = "https://localhost:44308";
            client.BaseAddress = new Uri(urlBase);
            var url = string.Concat(urlBase, "/Api", "/Products", "/GetProducts");

            var response = client.GetAsync(url).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<List<ProductModel>>(result);

            }

            return View(model);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            // View create
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public async Task<ActionResult> Create(ProductCreateModel model)
        {
            try
            {
                //Class a JSON
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");

                var client = new HttpClient();
                var urlBase = "https://localhost:44308";
                client.BaseAddress = new Uri(urlBase);
                var url = string.Concat(urlBase, "/Api", "/Products", "/PostProduct");

                var response = client.PostAsync(url, content).Result;

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    var result = await response.Content.ReadAsStringAsync();

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}