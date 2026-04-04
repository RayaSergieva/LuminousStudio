namespace LuminousStudio.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using LuminousStudio.Core.Contracts;
    using LuminousStudio.Infrastructure.Data.Entities;
    using LuminousStudio.Models.LampStyle;
    using LuminousStudio.Models.Manufactorer;
    using LuminousStudio.Models.TiffanyLamp;

    [Authorize(Roles = "Administrator")]
    public class TiffanyLampController : BaseController
    {
        private readonly ITiffanyLampService _tiffanyLampService;
        private readonly ILampStyleService _lampStyleService;
        private readonly IManufacturerService _manufacturerService;

        public TiffanyLampController(ITiffanyLampService tiffanyLampService, ILampStyleService lampStyleService, IManufacturerService manufacturerService)
        {
            _tiffanyLampService = tiffanyLampService;
            _lampStyleService = lampStyleService;
            _manufacturerService = manufacturerService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Index(string searchStringLampStyleName, string searchStringManufacturerName)
        {
            List<TiffanyLampIndexVM> tiffanyLamps = (await _tiffanyLampService.GetTiffanyLampsAsync(searchStringLampStyleName, searchStringManufacturerName))
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

            return View(tiffanyLamps);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var tiffanyLamp = new TiffanyLampCreateVM();
            tiffanyLamp.Manufacturers = (await _manufacturerService.GetManufacturersAsync())
                .Select(x => new ManufacturerPairVM()
                {
                    Id = x.Id,
                    Name = x.ManufacturerName
                }).ToList();

            tiffanyLamp.LampStyles = (await _lampStyleService.GetLampStylesAsync())
                .Select(x => new LampStylePairVM()
                {
                    Id = x.Id,
                    Name = x.LampStyleName
                }).ToList();

            return View(tiffanyLamp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] TiffanyLampCreateVM tiffanyLamp)
        {
            if (ModelState.IsValid)
            {
                var createdId = await _tiffanyLampService.CreateAsync(
                    tiffanyLamp.TiffanyLampName, 
                    tiffanyLamp.ManufacturerId,
                    tiffanyLamp.LampStyleId, 
                    tiffanyLamp.Picture,
                    tiffanyLamp.Quantity, 
                    tiffanyLamp.Price, 
                    tiffanyLamp.Discount);

                if (createdId)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            tiffanyLamp.Manufacturers = (await _manufacturerService.GetManufacturersAsync())
                .Select(x => new ManufacturerPairVM
                {
                    Id = x.Id,
                    Name = x.ManufacturerName
                }).ToList();

            tiffanyLamp.LampStyles = (await _lampStyleService.GetLampStylesAsync())
                .Select(x => new LampStylePairVM
                {
                    Id = x.Id,
                    Name = x.LampStyleName
                }).ToList();

            return View(tiffanyLamp);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            TiffanyLamp? tiffanyLamp = await _tiffanyLampService.GetTiffanyLampByIdAsync(id);
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

            updatedTiffanyLamp.Manufacturers = (await _manufacturerService.GetManufacturersAsync())
                .Select(m => new ManufacturerPairVM()
                {
                    Id = m.Id,
                    Name = m.ManufacturerName
                }).ToList();

            updatedTiffanyLamp.LampStyles = (await _lampStyleService.GetLampStylesAsync())
                .Select(ls => new LampStylePairVM()
                {
                    Id = ls.Id,
                    Name = ls.LampStyleName
                }).ToList();

            return View(updatedTiffanyLamp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, TiffanyLampEditVM tiffanyLamp)
        {
            if (ModelState.IsValid)
            {
                var updated = await _tiffanyLampService.UpdateAsync(
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
                    return RedirectToAction(nameof(Index));
                }
            }

            tiffanyLamp.Manufacturers = (await _manufacturerService.GetManufacturersAsync())
                .Select(x => new ManufacturerPairVM
                {
                    Id = x.Id,
                    Name = x.ManufacturerName
                }).ToList();

            tiffanyLamp.LampStyles = (await _lampStyleService.GetLampStylesAsync())
                .Select(x => new LampStylePairVM
                {
                    Id = x.Id,
                    Name = x.LampStyleName
                }).ToList();

            return View(tiffanyLamp);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Details(Guid id)
        {
            TiffanyLamp? item = await _tiffanyLampService.GetTiffanyLampByIdAsync(id);
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

        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            TiffanyLamp? item = await _tiffanyLampService.GetTiffanyLampByIdAsync(id);
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
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var deleted = await _tiffanyLampService.RemoveByIdAsync(id);

            if (deleted)
            {
                return RedirectToAction(nameof(Success));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }
    }
}
