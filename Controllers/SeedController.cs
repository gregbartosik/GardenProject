using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Garden1;

namespace Garden.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    //[Route("[controller]")]
    public class SeedController : ControllerBase
    {
        private readonly GardenContext _context;
        //private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public SeedController(
            GardenContext context,
            // RoleManager<IdentityRole> roleManager,
            //UserManager<ApplicationUser> userManager,
            IWebHostEnvironment env,
            IConfiguration configuration)
        {
            _context = context;
            // _roleManager = roleManager;
            // _userManager = userManager;
            _env = env;
            _configuration = configuration;
        }

        [HttpGet]
        public void ReadThisYearsCrops()
        //private static void ReadThisYearsCrops(GardenContext db)
        {
            Console.WriteLine("Getting all the crops for this year...");
            var assignments = _context.CropAssignments.AsNoTracking()
                        .Where(ca => ca.Year == 2020)
                        .Include(ca => ca.Bed).ThenInclude(b => b.Garden)
                        .Include(ca => ca.Crop)
                        .OrderBy(ca => ca.Bed.Garden.Name).ThenBy(ca => ca.Crop.Name).ThenBy(ca => ca.Bed.Number)
                        .ToList();
            assignments.ForEach(a => Console.WriteLine($"Growing {a.Crop.Name} on bed {a.Bed.Number} in the garden {a.Bed.Garden.Name}"));
        }

        //private static void CreateSampleData(GardenContext db)
        public void CreateSampleData()
        {
            var garden = (_context.Gardens.Add(new Garden1.Garden { Name = "My first garden" })).Entity;
            _context.SaveChanges();

            var bed1 = (_context.Beds.Add(new Bed { GardenId = garden.GardenId, Number = 1 })).Entity;
            _context.SaveChanges();
            var bed2 = (_context.Beds.Add(new Bed { GardenId = garden.GardenId, Number = 2 })).Entity;
            _context.SaveChanges();
            var bed3 = (_context.Beds.Add(new Bed { GardenId = garden.GardenId, Number = 3 })).Entity;
            _context.SaveChanges();

            var pumpkin = (_context.Crops.Add(new Crop { Name = "Pumpkin" })).Entity;
            _context.SaveChanges();
            var salad = (_context.Crops.Add(new Crop { Name = "Salad" })).Entity;
            _context.SaveChanges();
            var tomatoes = (_context.Crops.Add(new Crop { Name = "Tomatoes" })).Entity;
            _context.SaveChanges();
            var beans = (_context.Crops.Add(new Crop { Name = "Beans" })).Entity;
            _context.SaveChanges();

            var assignment1 = (_context.CropAssignments.Add(
                new CropAssignment
                {
                    CropId = pumpkin.CropId,
                    BedId = bed1.BedId,
                    Year = 2020
                })).Entity;
            _context.SaveChanges();
            var assignment2 = (_context.CropAssignments.Add(
                new CropAssignment
                {
                    CropId = salad.CropId,
                    BedId = bed2.BedId,
                    Year = 2020
                })).Entity;
            _context.SaveChanges();
            var assignment3 = (_context.CropAssignments.Add(
                new CropAssignment
                {
                    CropId = beans.CropId,
                    BedId = bed3.BedId,
                    Year = 2020
                })).Entity;
            _context.SaveChanges();
            var assignment4 = (_context.CropAssignments.Add(
                new CropAssignment
                {
                    CropId = pumpkin.CropId,
                    BedId = bed3.BedId,
                    Year = 2021
                })).Entity;
            _context.SaveChanges();
            var assignment5 = (_context.CropAssignments.Add(
                new CropAssignment
                {
                    CropId = tomatoes.CropId,
                    BedId = bed2.BedId,
                    Year = 2021
                })).Entity;
            _context.SaveChanges();
        }
    }
}
