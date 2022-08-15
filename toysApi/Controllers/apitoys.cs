using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace toysApi.Controllers
{
    public class apitoys : Controller
    {
        // GET: apitoys
        public ActionResult Index()
        {
            return View();
        }

        // GET: apitoys/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: apitoys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: apitoys/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: apitoys/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: apitoys/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: apitoys/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: apitoys/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
