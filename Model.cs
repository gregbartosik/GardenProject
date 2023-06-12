//namespace Garden
//{
//    public class Model
//    {
//    }
//}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Garden1
{
    public class GardenContext : DbContext
    {
        public GardenContext() : base()
        {
        }

        public GardenContext(DbContextOptions options)
         : base(options)
        {
        }
        public DbSet<Garden> ?Gardens { get; set; }
        public DbSet<Bed> ?Beds { get; set; }
        public DbSet<Crop> ?Crops { get; set; }
        public DbSet<CropAssignment> ?CropAssignments { get; set; }

        public DbSet<Image> ?Images { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=localhost\\SQLEXPRESS; Database=GardenDb1; User Id=Garden; Password=MyVeryOwn$721; Integrated Security=False; MultipleActiveResultSets=True; TrustServerCertificate=True");
                //"Server=localhost,1433; Database=GardenDb1;User=sa; Password=1StrongPassword!"
     

    }

    public class Garden
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GardenId { get; set; }
        public string ?Name { get; set; }

        public List<Bed> Beds { get; } = new List<Bed>();
    }

    public class Bed
    {
        public int BedId { get; set; }
        public int Number { get; set; }

        public int GardenId { get; set; }
        public Garden ?Garden { get; set; }
        public List<CropAssignment> ?CropAssignments { get; set; }
    }

    public class Crop
    {
        public int CropId { get; set; }
        public string ?Name { get; set; }
        public List<CropAssignment> ?CropAssignments { get; set; }
    }

    public class CropAssignment
    {
        public int CropAssignmentId { get; set; }
        public int CropId { get; set; }
        public Crop ?Crop { get; set; }
        public int BedId { get; set; }
        public Bed ?Bed { get; set; }
        public int Year { get; set; }

        public int ?ImageId { get; set; }
        public Image ?Image { get; set; }
    }

    public class Image
    {
        public int Id { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public List<CropAssignment> ?CropAssignments { get; set; }
    }
}