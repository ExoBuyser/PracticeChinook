using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.Data.Entity;
using ChinookSystem.Data.Entities;
#endregion

namespace ChinookSystem.DAL
{
    //internal for security reason
    //access restricted to within the component library
    //inherit DbContext for Entity Framework requires
    //   System.Data.Entity
    internal class ChinookContext : DbContext
    {
        //pass the connection string name to the 
        //DbContext using the :base()
        public ChinookContext() : base("ChinookDB")
        {

        }

        //setup DbSet properties
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<MediaType> MediaTypes { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public DbSet<PlayList> PlayLists { get; set; }

        //Modelling of our many-to-many relation between Tracks and PlayLists
        //No entity was coded for the physical database table PlaylistTracks
        //This table consists of ONLY Pimary Key/Foreign Key attributes
        //Since the navigation properties have been setup to handle the 
        //many-to-many relationship, we need to provide a model of the
        //relationship when clarifing the mapping in our database.

        //this clarification can be handle using the DbContext.OnModelCreating() method
        //By overriding this method, we can add our custom mappings
        //It is important to call the base method's implementation before you
        //exit the method

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<PlayList>().HasMany(r => r.Tracks)
                .WithMany(t => t.PlayLists)
                .Map(mapping =>
                {
                    mapping.ToTable("PlaylistTracks"); //entity not coded
                    mapping.MapLeftKey("PlayListId");  //HasMany key
                    mapping.MapRightKey("TrackId");    //WithMany Key
                });
            //call the base method
            base.OnModelCreating(modelBuilder);
        }
    }
}
