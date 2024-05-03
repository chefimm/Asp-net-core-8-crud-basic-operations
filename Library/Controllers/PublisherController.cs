using Library.Models;
using Library.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IPublisherService service;
        public PublisherController(IPublisherService service)
        {
            this.service = service;
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Publisher model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = service.Add(model);
            if (result)
            {
                TempData["msg"] = "Ekleme Başarılı.";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Hata Başarısız.";
            return View(model);
        }
        public IActionResult Update(int id)
        {
            var record = service.FindById(id);
            return View(record);
        }
        [HttpPost]
        public IActionResult Update(Publisher model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = service.Update(model);
            if (result)
            {
                TempData["msg"] = "Güncelleme Başarılı.";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Hata Başarısız.";
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var result = service.Delete(id);
            return RedirectToAction("GetAll");
        }
        public IActionResult GetAll()
        {
            var data = service.GetAll();
            return View(data);
        }
    }
}
