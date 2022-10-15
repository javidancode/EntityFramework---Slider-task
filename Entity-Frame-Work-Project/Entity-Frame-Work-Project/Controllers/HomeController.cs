﻿using Entity_Frame_Work_Project.Data;
using Entity_Frame_Work_Project.Models;
using Entity_Frame_Work_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Entity_Frame_Work_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task <IActionResult> Index()
        {
            IEnumerable<Slider> sliders = await _context.Sliders.ToListAsync();
            SliderDetail sliderDetail = await _context.SliderDetails.FirstOrDefaultAsync();
            IEnumerable<Category> categories = await _context.Categories.Where(m => m.IsDeleted == false).ToListAsync();
            IEnumerable<Product> products = await _context.Products
                .Where(m => m.IsDeleted == false)
                .Include(m => m.Category)
                .Include(m => m.ProductImages).Take(8).ToListAsync();

            HomeVM model = new HomeVM
            {
                Sliders = sliders,
                SliderDetail = sliderDetail,
                Categories = categories,
                Products = products

            };

            return View(model);
        }
       
    }
}
