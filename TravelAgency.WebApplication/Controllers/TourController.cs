using System.Diagnostics;
using DataGrid.Contracts.Interface;
using DataGrid.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.WebApplication.Models;

namespace TravelAgency.WebApplication.Controllers
{
    public class TourController : Controller
    {

        private readonly ITourManagment tourManagment;
        public TourController(ITourManagment tourManagment)
        {
            this.tourManagment = tourManagment;
        }

        /// <summary>
        /// Отображает список всех туров.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var products = tourManagment.GetAllAsync();
            var stats = tourManagment.GetStatsAsync();
            await Task.WhenAll(products, stats);

            ViewData[nameof(ITourManagment)] = stats.Result;
            return View(products.Result);
        }

        /// <summary>
        /// Отображает страницу создания нового продукта.
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Обрабатывает создание нового продукта.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(Tour tour)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            tour.ID = Guid.NewGuid();
            await tourManagment.AddAsync(tour);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Отображает страницу редактирования продукта по его идентификатору.
        /// </summary>
        public async Task<IActionResult> Edit(Guid id)
        {
            var tours = await tourManagment.GetAllAsync();
            var tour = tours.FirstOrDefault(p => p.ID == id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        /// <summary>
        /// Обрабатывает редактирование существующего тура.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Tour tour)
        {
            if (!ModelState.IsValid)
            {
                return View(tour);
            }

            var products = await tourManagment.GetAllAsync();
            var existingProduct = products.FirstOrDefault(p => p.ID == id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            await tourManagment.EditAsync(tour);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Обрабатывает удаление тура по его идентификатору.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await tourManagment.DeleteAsync(id);
            if (result == false)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Отображает страницу конфиденциальности.
        /// </summary>
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
