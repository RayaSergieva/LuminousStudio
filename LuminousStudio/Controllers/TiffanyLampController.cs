using LuminousStudio.Core.Contracts;
using LuminousStudio.Infrastructure.Data.Entities;
using LuminousStudio.Models.LampStyle;
using LuminousStudio.Models.Manufactorer;
using LuminousStudio.Models.TiffanyLamp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuminousStudio.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class TiffanyLampController : Controller
    {
        private readonly ITiffanyLampService _tiffanyLampService;
        private readonly ILampStyleService _lampStyleService;
        private readonly IManufacturerService _manufacturerService;

        public TiffanyLampController(ITiffanyLampService tiffanyLampService, ILampStyleService lampStyleService, IManufacturerService manufacturerService)
        {
            this._tiffanyLampService = tiffanyLampService;
            this._lampStyleService = lampStyleService;
            this._manufacturerService = manufacturerService;
        }

        [AllowAnonymous]
        public ActionResult Index(string searchStringLampStyleName, string searchStringManufacturerName)
        {
            List<TiffanyLampIndexVM> tiffanyLamps = _tiffanyLampService.GetTiffanyLamps(searchStringLampStyleName, searchStringManufacturerName)
                .Select(tiffanyLamp => new TiffanyLampIndexVM
                {
                    Id = tiffanyLamp.Id,
                    TiffanyLampName = tiffanyLamp.TiffanyLampName,
                    ManufacturerName = tiffanyLamp.Manufacturer.ManufacturerName,
                    LampStyleName = tiffanyLamp.LampStyle.LampStyleName,
                    Picture = tiffanyLamp.Picture,
                    Quantity = tiffanyLamp.Quantity,
                    Price = tiffanyLamp.Price,
                    Discount = tiffanyLamp.Discount
                }).ToList();

            return this.View(tiffanyLamps);
        }

        public ActionResult Create()
        {
            var tiffanyLamp = new TiffanyLampCreateVM();
            tiffanyLamp.Manufacturers = _manufacturerService.GetManufacturers()
                .Select(x => new ManufacturerPairVM()
                {
                    Id = x.Id,
                    Name = x.ManufacturerName
                }).ToList();

            tiffanyLamp.LampStyles = _lampStyleService.GetLampStyles()
                .Select(x => new LampStylePairVM()
                {
                    Id = x.Id,
                    Name = x.LampStyleName
                }).ToList();

            return View(tiffanyLamp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] TiffanyLampCreateVM tiffanyLamp)
        {
            if (ModelState.IsValid)
            {
                var created = _tiffanyLampService.Create(
                    tiffanyLamp.TiffanyLampName,
                    tiffanyLamp.ManufacturerId,
                    tiffanyLamp.LampStyleId,
                    tiffanyLamp.Picture,
                    tiffanyLamp.Quantity,
                    tiffanyLamp.Price,
                    tiffanyLamp.Discount);

                if (created)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            tiffanyLamp.Manufacturers = _manufacturerService.GetManufacturers()
                .Select(x => new ManufacturerPairVM
                {
                    Id = x.Id,
                    Name = x.ManufacturerName
                }).ToList();

            tiffanyLamp.LampStyles = _lampStyleService.GetLampStyles()
                .Select(x => new LampStylePairVM
                {
                    Id = x.Id,
                    Name = x.LampStyleName
                }).ToList();

            return View(tiffanyLamp);
        }

        public ActionResult Edit(int id)
        {
            TiffanyLamp tiffanyLamp = _tiffanyLampService.GetTiffanyLampById(id);
            if (tiffanyLamp == null)
            {
                return NotFound();
            }

            TiffanyLampEditVM updatedTiffanyLamp = new TiffanyLampEditVM()
            {
                Id = tiffanyLamp.Id,
                TiffanyLampName = tiffanyLamp.TiffanyLampName,
                ManufacturerId = tiffanyLamp.ManufacturerId,
                LampStyleId = tiffanyLamp.LampStyleId,  
                Picture = tiffanyLamp.Picture,
                Quantity = tiffanyLamp.Quantity,
                Price = tiffanyLamp.Price,
                Discount = tiffanyLamp.Discount
            };

            updatedTiffanyLamp.Manufacturers = _manufacturerService.GetManufacturers()
                .Select(m => new ManufacturerPairVM()
                {
                    Id = m.Id,
                    Name = m.ManufacturerName
                }).ToList();

            updatedTiffanyLamp.LampStyles = _lampStyleService.GetLampStyles()
                .Select(ls => new LampStylePairVM()
                {
                    Id = ls.Id,
                    Name = ls.LampStyleName
                }).ToList();

            return View(updatedTiffanyLamp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TiffanyLampEditVM tiffanyLamp)
        {
            if (ModelState.IsValid)
            {
                var updated = _tiffanyLampService.Update(
                    id,
                    tiffanyLamp.TiffanyLampName,
                    tiffanyLamp.ManufacturerId,
                    tiffanyLamp.LampStyleId,
                    tiffanyLamp.Picture,
                    tiffanyLamp.Quantity,
                    tiffanyLamp.Price,
                    tiffanyLamp.Discount);

                if (updated)
                {
                    return RedirectToAction("Index");
                }
            }

            tiffanyLamp.Manufacturers = _manufacturerService.GetManufacturers()
                .Select(x => new ManufacturerPairVM
                {
                    Id = x.Id,
                    Name = x.ManufacturerName
                }).ToList();

            tiffanyLamp.LampStyles = _lampStyleService.GetLampStyles()
                .Select(x => new LampStylePairVM
                {
                    Id = x.Id,
                    Name = x.LampStyleName
                }).ToList();

            return View(tiffanyLamp);
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            TiffanyLamp item = _tiffanyLampService.GetTiffanyLampById(id);
            if (item == null)
            {
                return NotFound();
            }

            TiffanyLampDetailsVM tiffanyLamp = new TiffanyLampDetailsVM()
            {
                Id = item.Id,
                TiffanyLampName = item.TiffanyLampName,
                ManufacturerId = item.ManufacturerId,
                ManufacturerName = item.Manufacturer.ManufacturerName,
                LampStyleId = item.LampStyleId,
                LampStyleName = item.LampStyle.LampStyleName,
                Picture = item.Picture,
                Quantity = item.Quantity,
                Price = item.Price,
                Discount = item.Discount
            };

            return View(tiffanyLamp);
        }

        public ActionResult Delete(int id)
        {
            TiffanyLamp item = _tiffanyLampService.GetTiffanyLampById(id);
            if (item == null)
            {
                return NotFound();
            }

            TiffanyLampDeleteVM tiffanyLamp = new TiffanyLampDeleteVM()
            {
                Id = item.Id,
                TiffanyLampName = item.TiffanyLampName,
                ManufacturerId = item.ManufacturerId,
                ManufacturerName = item.Manufacturer.ManufacturerName,
                LampStyleId = item.LampStyleId,
                LampStyleName = item.LampStyle.LampStyleName,
                Picture = item.Picture,
                Quantity = item.Quantity,
                Price = item.Price,
                Discount = item.Discount
            };

            return View(tiffanyLamp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var deleted = _tiffanyLampService.RemoveById(id);

            if (deleted)
            {
                return this.RedirectToAction("Success");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
