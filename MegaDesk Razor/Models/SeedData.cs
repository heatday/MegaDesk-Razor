using Microsoft.EntityFrameworkCore;
using MegaDesk_Razor.Data;
using MegaDesk_Razor.Models;

namespace RazorPagesMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MegaDeskContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MegaDeskContext>>()))
        {
            if (context == null || context.DeliveryTypes == null)
            {
                throw new ArgumentNullException("Null MegaDeskContext");
            }

            // Look for any movies.
            if (context.DeliveryTypes.Any() || context.Materials.Any())
            {
                return;   // DB has been seeded
            }

            context.DeliveryTypes.AddRange(
                new DeliveryType
                {
                    Type = "No Rush"
                },

                new DeliveryType
                {
                    Type = "3 Days"
                },

                new DeliveryType
                {
                    Type = "5 Days"
                },

                new DeliveryType
                {
                    Type = "7 Days"
                }
            );

            context.Materials.AddRange(
                new Material
                {
                    Name = "Pine"
                },

                new Material
                {
                    Name = "Oak"
                },

                new Material
                {
                    Name = "Laminate"
                },

                new Material
                {
                    Name = "Redwood"
                },

                new Material
                {
                    Name = "Rosewood"
                },

                new Material
                {
                    Name = "Veneer"
                },

                new Material
                {
                    Name = "Mahogany"
                }
                );
            context.SaveChanges();
        }
    }
}