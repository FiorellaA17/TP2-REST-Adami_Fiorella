using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleProduct> SalesProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ApiStore;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasKey(c => c.CategoryId);

                entity.Property(c => c.Name)
                .HasMaxLength(100);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("Sale");

                entity.HasKey(s => s.SaleId);

                entity.Property(s => s.TotalPay)
                .HasColumnType("decimal( 15, 2)")
                .IsRequired();

                entity.Property(s => s.Subtotal)
                .HasColumnType("decimal( 15, 2)")
                .IsRequired();

                entity.Property(s => s.TotalDiscount)
                .HasColumnType("decimal( 15, 2)")
                .IsRequired();

                entity.Property(s => s.Taxes)
                .HasColumnType("decimal( 15, 2)")
                .IsRequired();

                entity.Property(s => s.Date)
                .IsRequired();

            });

            modelBuilder.Entity<SaleProduct>(entity =>
            {
                entity.ToTable("SaleProduct");

                entity.HasKey(sp => sp.ShoppingCartId);

                entity.Property(sp => sp.Sale)
                .IsRequired();

                entity.Property(sp => sp.Product)
                .IsRequired();

                entity.Property(sp => sp.Quantity)
                .IsRequired();

                entity.Property(sp => sp.Price)
                .HasColumnType("decimal( 15, 2)")
                .IsRequired();

                entity.Property(sp => sp.Discount);

                entity.HasOne<Sale>(sp => sp.SaleName)
                    .WithMany(s => s.SaleProducts)
                    .HasForeignKey(sp => sp.Sale);

                entity.HasOne<Product>(sp => sp.ProductName)
                    .WithMany(p => p.SaleProducts)
                    .HasForeignKey(sp => sp.Product);

            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasKey(p => p.ProductId);

                entity.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

                entity.Property(p => p.Description)
                .HasMaxLength(int.MaxValue);

                entity.Property(p => p.Price)
                .HasColumnType("decimal( 15, 2)")
                .IsRequired();

                entity.Property(p => p.Category)
                .IsRequired();

                entity.Property(p => p.Discount);

                entity.Property(p => p.ImageUrl)
                .HasMaxLength(255);

                entity.HasOne(p => p.CategoryName)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.Category);

            });

            modelBuilder.Entity<Category>().HasData(

            new Category { CategoryId = 1, Name = "Electrodomésticos" },
            new Category { CategoryId = 2, Name = "Tecnología y Electrónica" },
            new Category { CategoryId = 3, Name = "Moda y Accesorios" },
            new Category { CategoryId = 4, Name = "Hogar y Decoración" },
            new Category { CategoryId = 5, Name = "Salud y Belleza" },
            new Category { CategoryId = 6, Name = "Deportes y Ocio" },
            new Category { CategoryId = 7, Name = "Juguetes y Juegos" },
            new Category { CategoryId = 8, Name = "Alimentos y Bebidas" },
            new Category { CategoryId = 9, Name = "Libros y Material Educativo" },
            new Category { CategoryId = 10, Name = "Jardinería y Bricolaje" });

            modelBuilder.Entity<Product>().HasData(

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Heladera samsung",
                Description = "308L Rt29k577js8 No Frost Ix Inv",
                Price = 589999,
                Category = 1,
                Discount = 20,
                ImageUrl = "https://www.megatone.net/Images/Articulos/zoom2x/36/HEL2958SSG.jpg"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Lg lavarropas automático",
                Description = "LAvarropas automático WM-85VVC5S6P Inverter vivace 8.5 kg silver",
                Price = 1285713,
                Category = 1,
                Discount = 10,
                ImageUrl = "https://76338a6a.flyingcdn.com/44686-large_default/lg-lavarropas-autwm-85vvc5s6p-inverter-vivace-85kg-silver.jpg"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Tostadora",
                Description = "Tostadora atma eléctrica toat-39dn",
                Price = 39999,
                Category = 1,
                Discount = 0,
                ImageUrl = "https://76338a6a.flyingcdn.com/39415-large_default/atma-tostadora-electrica-toat-39dn.jpg"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Batidora planetaria",
                Description = "Batidora atma planetaria bp-at21rp roja",
                Price = 589999,
                Category = 1,
                Discount = 0,
                ImageUrl = "https://76338a6a.flyingcdn.com/42304-large_default/atma-batidora-planetaria-bp-at21rp-roja.jpg"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Smart Tv 50 pulgadas 4k",
                Description = "Smart Tv 50 Pulgadas 4K Ultra Hd 50Pud7406/77 - PHILIPS",
                Price = 594099,
                Category = 2,
                Discount = 5,
                ImageUrl = "https://www.megatone.net/Images/Articulos/zoom2x/253/TEL5006PHI.jpg"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Consola Sony PlayStation 5",
                Description = "PS5 SLIM Standard con lector CD/DVD 1TB + 1 Joystick Regalo",
                Price = 900000,
                Category = 2,
                Discount = 20,
                ImageUrl = "https://images.fravega.com/f300/dd39a03cd136501e71f52ef06e09c861.jpg.webp"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Proyector",
                Description = "Proyector Gadnic Surr 5500 Lúmenes Con Filtro HEPA HDMI x 2 USB x 2",
                Price = 283179,
                Category = 2,
                Discount = 0,
                ImageUrl = "https://images.fravega.com/f300/5b68c007ab23568b5379ac7708999b9a.jpg.webp"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Drone",
                Description = "Drone Gadnic Con Camara Dual 4K",
                Price = 594099,
                Category = 2,
                Discount = 0,
                ImageUrl = "https://images.fravega.com/f300/60354c20fe3a7afd1b747439ee22d522.jpg.webp"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Reebok zapatillas mujer",
                Description = "Reebok zapatillas mujer - Glide mujer ch. -Dk grey-green",
                Price = 99999,
                Category = 3,
                Discount = 5,
                ImageUrl = "https://megasports.vteximg.com.br/arquivos/ids/224093-1000-1000/41104788055_0.jpg?v=638267497579500000"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Mochila de viaje",
                Description = "Mochila De Viaje Travel Tech Grande Amplia Compartimientos",
                Price = 104500,
                Category = 3,
                Discount = 0,
                ImageUrl = "https://images.fravega.com/f300/eab6e93f47c2a4a5156964361abd6aa0.jpg.webp"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Bolso deportivo",
                Description = "Bolso Amayra Fit Cómodo Gimnasio Deportivo",
                Price = 30800,
                Category = 3,
                Discount = 0,
                ImageUrl = "https://images.fravega.com/f300/c60063d66bd91a656af89bf3bf4e0694.jpg.webp"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Anteojos de sol",
                Description = "Modelo de anteojos de sol para mujer XOLD de la marca RUSTY.",
                Price = 89600,
                Category = 3,
                Discount = 15,
                ImageUrl = "https://static.wixstatic.com/media/617495_87ec75c8071244158aa28802dcdb8108~mv2.jpg/v1/fill/w_500,h_500,al_c,q_80,usm_0.66_1.00_0.01,enc_auto/617495_87ec75c8071244158aa28802dcdb8108~mv2.jpg"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Lampara colgante",
                Description = "Lampara Colgante Led L2020 Dorado Moderno Deco Luz Desing",
                Price = 99999,
                Category = 4,
                Discount = 0,
                ImageUrl = "https://http2.mlstatic.com/D_Q_NP_660955-MLA74649779983_022024-B.jpg"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Juego de Sábanas 1 1/2 plaza Jurassic",
                Description = "",
                Price = 22590,
                Category = 4,
                Discount = 0,
                ImageUrl = "https://images.fravega.com/f300/9b2bee9b6b6518b60eb262be396c44fb.jpg.webp"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Almohada de espuma cervical",
                Description = "La almohada de Piero Cervical está hecha de espuma, mejorando la postura del cuello y hombros. Medida: 65 x 35 cm.",
                Price = 14999,
                Category = 4,
                Discount = 0,
                ImageUrl = "https://images.fravega.com/f300/30df4745cef1263ea72604f24c3a3929.jpg.webp"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Espejo Nam 60cm",
                Description = "Espejo Nam de fibras naturales. Importado de Vietnam Diámetro: 60cm Espejo Diámetro: 19cm",
                Price = 66990,
                Category = 4,
                Discount = 10,
                ImageUrl = "https://images.fravega.com/f300/6c2f4d2d61e3672653e9789598a83334.jpg.webp"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Angel nova EDP 100ml",
                Description = "Una fragancia cautivadora nacida de la fusióninesperada entre el dulzor de notas afrutadas, la exclusiva rosatecnológica de Mugler y un elegante collar amaderado.",
                Price = 286000,
                Category = 5,
                Discount = 25,
                ImageUrl = "https://d3cdlnm7te7ky2.cloudfront.net/media/mf_webp/jpg/media/catalog/product/cache/8cafa0a86860701ec5138a548d16cdc2/a/n/angel-nova-edp-rechargable-100ml_1.webp"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Lash sensational sky high washable - very black",
                Description = "La máscara de pestañas Lash Sensational Sky High de larga duración brinda un volumen completo y una longitud ilimitada.",
                Price = 22659,
                Category = 5,
                Discount = 0,
                ImageUrl = "https://d3cdlnm7te7ky2.cloudfront.net/media/mf_webp/jpg/media/catalog/product/cache/d41ed5f2410977369e7cfa45777ef8e0/1/4/142943-a-lash-sensational-sky-high-washable-very-black_1.webp"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Stronge with you EDT 100ml",
                Description = "Perfume para un hombre que vive en el presente, moldeado por la energía de la modernidad. Imprevisible, sorprende con su originalidad.",
                Price = 174900,
                Category = 5,
                Discount = 0,
                ImageUrl = "https://d3cdlnm7te7ky2.cloudfront.net/media/mf_webp/jpg/media/catalog/product/cache/mtools/300x300/catalog/product//1/1/119078-a-stronger-with-you-edt-100ml_2.webp"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Protector solar facial",
                Description = "UV defender protector solar facil fuido FPS50+ 40g - claro.UV Defender Fluido es resiste al agua y al sudor, con textura fluida.",
                Price = 16253,
                Category = 5,
                Discount = 15,
                ImageUrl = "https://d3cdlnm7te7ky2.cloudfront.net/media/mf_webp/jpg/media/catalog/product/cache/mtools/300x300/catalog/product//1/5/150122-a-uv-defender-protector-solar-facial-fluido-fps50-40g-claro.webp"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Bicicleta mountain bike",
                Description = "Bicicleta Mountain Bike X 1.0 Rodado 29 Talle 20 Negro - NORDIC",
                Price = 589999,
                Category = 6,
                Discount = 0,
                ImageUrl = "https://www.megatone.net/Images/Articulos/zoom2x/104/BIC2922NRD.jpg"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Soga saltar crossfit",
                Description = "Soga Saltar Crossfit/fitness Ranbak 737 Azul - RANBAK",
                Price = 7300,
                Category = 6,
                Discount = 0,
                ImageUrl = "https://www.megatone.net/images/Articulos/zoom2x/304/01/MKT0048RAN.jpg"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Rueda doble para abdominales",
                Description = "Rueda Doble Para Abdominales Ranbak 730 - RANBAK",
                Price = 15799,
                Category = 6,
                Discount = 0,
                ImageUrl = "https://www.megatone.net/images/Articulos/zoom2x/304/01/MKT0060RAN.jpg"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Bote inflable",
                Description = "Bote Inflable Explorer Pro 100 22699/0 - INTEX",
                Price = 42999,
                Category = 6,
                Discount = 10,
                ImageUrl = "https://www.megatone.net/Images/Articulos/zoom2x/116/BOT6990ITX.jpg"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Broly Dragon ball",
                Description = "Dragon Ball Figura 10cm Articulado Flash Super Saiyan Broly",
                Price = 25000,
                Category = 7,
                Discount = 0,
                ImageUrl = "https://wabro.vteximg.com.br/arquivos/ids/187554-320-320/3.jpg?v=638030021553570000"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Legends of Akedo Powerstorm Battle Giants",
                Description = "Legends of Akedo Powerstorm Battle Giants combina 2 figuras de acción de gigantes de batalla.",
                Price = 45000,
                Category = 7,
                Discount = 0,
                ImageUrl = "https://m.media-amazon.com/images/I/51EXTNFIrYL._AC_.jpg"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Hasbro Gaming Simon Juego de memoria",
                Description = "Hasbro Gaming Simon Juego de memoria electrónica portátil con luces y sonidos para niños de 8 años en adelante",
                Price = 30820,
                Category = 7,
                Discount = 0,
                ImageUrl = "https://m.media-amazon.com/images/I/81P7iHZ+1lL._AC_UL320_.jpg"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Bloques magnéticos",
                Description = "Bloques Magneticos Grandes Varias Formas y colores 42 Piezas MG23",
                Price = 50399,
                Category = 7,
                Discount = 15,
                ImageUrl = "https://images.fravega.com/f300/59412558bc3aad5f4a7e9bcfea3dc30c.jpg.webp"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Ferrero rocher",
                Description = "Ferrero Rocher – Chocolate Bombón – 8 Unidades",
                Price = 7000,
                Category = 8,
                Discount = 0,
                ImageUrl = "https://www.argensend.com/wp-content/uploads/2021/09/D_NQ_NP_913338-MLA40945655566_022020-O.jpeg"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Aceite de oliva",
                Description = "ACEITE ORGANICO DE OLIVA 500CC MAELCA",
                Price = 12350,
                Category = 8,
                Discount = 10,
                ImageUrl = "https://biomarket.com.ar/wp-content/uploads/2019/09/7798241870102.jpg"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Yogurt de coco sabor arandanos",
                Description = "Yogurt de coco sabor arandanos 170 g quimya. Apto veganos",
                Price = 2600,
                Category = 8,
                Discount = 0,
                ImageUrl = "https://biomarket.com.ar/wp-content/uploads/2019/07/721450715893.jpg"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Jugo de limón con jengibre",
                Description = "Jugo de limón con jengible orgánico 500 cc Las brisas. Apto veganos",
                Price = 37930,
                Category = 8,
                Discount = 0,
                ImageUrl = "https://biomarket.com.ar/wp-content/uploads/2023/01/BRIS54_1.jpg"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Lapices de colores",
                Description = "Faber castell lapices de colores super soft x 24 escritura artística.",
                Price = 28500,
                Category = 9,
                Discount = 0,
                ImageUrl = "https://www.heavenimagenes.com/heavencommerce/11de0e5d-73b3-4c32-910b-8483d00a7205/images/v2/FABER%20CASTELL/3388_medium.jpg"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Agenda",
                Description = "Mooving agenda 14 x 20 Harry Potter Diaria - Espiralada",
                Price = 25500,
                Category = 9,
                Discount = 0,
                ImageUrl = "https://www.heavenimagenes.com/heavencommerce/11de0e5d-73b3-4c32-910b-8483d00a7205/images/v2/MOOVING/20080_medium.jpg"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Calculadora científica",
                Description = "Casio calculadora científica a bateria FX-82ms",
                Price = 29400,
                Category = 9,
                Discount = 20,
                ImageUrl = "https://www.heavenimagenes.com/heavencommerce/11de0e5d-73b3-4c32-910b-8483d00a7205/images/v2/CASIO/8484_medium.jpg"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Tempera sólida",
                Description = "Sifap tempera sólida x 6 pastel arte",
                Price = 12863,
                Category = 9,
                Discount = 0,
                ImageUrl = "https://www.heavenimagenes.com/heavencommerce/11de0e5d-73b3-4c32-910b-8483d00a7205/images/v2/SIFAP/7597_medium.jpg"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Alegria del hogar",
                Description = "Planta de exterior",
                Price = 550,
                Category = 10,
                Discount = 0,
                ImageUrl = "https://www.viverodeluca.com.ar/1096-home_default/alegria-del-hogar.jpg"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Dracena rubra",
                Description = "Cordyline terminalis rubra",
                Price = 7900,
                Category = 10,
                Discount = 0,
                ImageUrl = "https://www.viverodeluca.com.ar/3334-large_default/cordyline-rubra-dracena-rubra-dracena-rubra.jpg"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Maceta de barro",
                Description = "Maceta de barro Clásica - Nº8",
                Price = 940,
                Category = 10,
                Discount = 0,
                ImageUrl = "https://www.viverodeluca.com.ar/1273-large_default/maceta-de-barro-cl%C3%A1sica.jpg"
            },

            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Desmalezadora",
                Description = "Desmalezadora Gamma Naftera 44Cc G1835ar - GAMMA",
                Price = 242500,
                Category = 10,
                Discount = 15,
                ImageUrl = "https://www.megatone.net/images//Articulos/zoom2x/106/MKT0013GAM-1.jpg",
            });
        }
    }
}
