namespace LuminousStudio.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using LuminousStudio.Services.Admin.Contracts;
    using LuminousStudio.Services.Core.Contracts;
    using LuminousStudio.Web.ViewModels.Admin.LampManagement;
    using LuminousStudio.Web.ViewModels.LampStyle;
    using LuminousStudio.Web.ViewModels.Manufactorer;
    using LuminousStudio.Web.ViewModels.TiffanyLamp;

    public class LampManagementController : BaseAdminController
    {
        private readonly ILampManagementService _lampManagementService;
        private readonly IManufacturerService _manufacturerService;
        private readonly ILampStyleService _lampStyleService;

        public LampManagementController(
            ILampManagementService lampManagementService,
            IManufacturerService manufacturerService,
            ILampStyleService lampStyleService)
        {
            _lampManagementService = lampManagementService;
            _manufacturerService = manufacturerService;
            _lampStyleService = lampStyleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var lamps = (await _lampManagementService.GetAllLampsAsync())
                .Select(l => new LampManagementIndexVM
                {
                    Id = l.Id,
                    TiffanyLampName = l.TiffanyLampName,
                    ManufacturerName = l.Manufacturer.ManufacturerName,
                    LampStyleName = l.LampStyle.LampStyleName,
                    Picture = l.Picture,
                    Quantity = l.Quantity,
                    Price = l.Price,
                    Discount = l.Discount
                }).ToList();

            return View(lamps);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new TiffanyLampCreateVM
            {
                Manufacturers = (await _manufacturerService.GetManufacturersAsync())
                    .Select(m => new ManufacturerPairVM { Id = m.Id, Name = m.ManufacturerName })
                    .ToList(),
                LampStyles = (await _lampStyleService.GetLampStylesAsync())
                    .Select(ls => new LampStylePairVM { Id = ls.Id, Name = ls.LampStyleName })
                    .ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TiffanyLampCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Manufacturers = (await _manufacturerService.GetManufacturersAsync())
                    .Select(m => new ManufacturerPairVM { Id = m.Id, Name = m.ManufacturerName })
                    .ToList();
                model.LampStyles = (await _lampStyleService.GetLampStylesAsync())
                    .Select(ls => new LampStylePairVM { Id = ls.Id, Name = ls.LampStyleName })
                    .ToList();
                return View(model);
            }

            await _lampManagementService.CreateLampAsync(
                model.TiffanyLampName, model.ManufacturerId, model.LampStyleId,
                model.Picture, model.Quantity, model.Price, model.Discount);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var lamp = await _lampManagementService.GetLampByIdAsync(id);
            if (lamp == null)
            {
                return NotFound();
            }

            var model = new TiffanyLampEditVM
            {
                Id = lamp.Id,
                TiffanyLampName = lamp.TiffanyLampName,
                ManufacturerId = lamp.ManufacturerId,
                LampStyleId = lamp.LampStyleId,
                Picture = lamp.Picture,
                Quantity = lamp.Quantity,
                Price = lamp.Price,
                Discount = lamp.Discount,
                Manufacturers = (await _manufacturerService.GetManufacturersAsync())
                    .Select(m => new ManufacturerPairVM { Id = m.Id, Name = m.ManufacturerName })
                    .ToList(),
                LampStyles = (await _lampStyleService.GetLampStylesAsync())
                    .Select(ls => new LampStylePairVM { Id = ls.Id, Name = ls.LampStyleName })
                    .ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, TiffanyLampEditVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Manufacturers = (await _manufacturerService.GetManufacturersAsync())
                    .Select(m => new ManufacturerPairVM { Id = m.Id, Name = m.ManufacturerName })
                    .ToList();
                model.LampStyles = (await _lampStyleService.GetLampStylesAsync())
                    .Select(ls => new LampStylePairVM { Id = ls.Id, Name = ls.LampStyleName })
                    .ToList();
                return View(model);
            }

            await _lampManagementService.UpdateLampAsync(
                id, model.TiffanyLampName, model.ManufacturerId, model.LampStyleId,
                model.Picture, model.Quantity, model.Price, model.Discount);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var lamp = await _lampManagementService.GetLampByIdAsync(id);
            if (lamp == null)
            {
                return NotFound();
            }

            var model = new TiffanyLampDeleteVM
            {
                Id = lamp.Id,
                TiffanyLampName = lamp.TiffanyLampName,
                ManufacturerName = lamp.Manufacturer.ManufacturerName,
                LampStyleName = lamp.LampStyle.LampStyleName,
                Picture = lamp.Picture,
                Quantity = lamp.Quantity,
                Price = lamp.Price,
                Discount = lamp.Discount
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _lampManagementService.DeleteLampAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}