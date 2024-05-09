using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPay = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    TotalDiscount = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Taxes = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.SaleId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Category_Category",
                        column: x => x.Category,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleProduct",
                columns: table => new
                {
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sale = table.Column<int>(type: "int", nullable: false),
                    Product = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleProduct", x => x.ShoppingCartId);
                    table.ForeignKey(
                        name: "FK_SaleProduct_Product_Product",
                        column: x => x.Product,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleProduct_Sale_Sale",
                        column: x => x.Sale,
                        principalTable: "Sale",
                        principalColumn: "SaleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Electrodomésticos" },
                    { 2, "Tecnología y Electrónica" },
                    { 3, "Moda y Accesorios" },
                    { 4, "Hogar y Decoración" },
                    { 5, "Salud y Belleza" },
                    { 6, "Deportes y Ocio" },
                    { 7, "Juguetes y Juegos" },
                    { 8, "Alimentos y Bebidas" },
                    { 9, "Libros y Material Educativo" },
                    { 10, "Jardinería y Bricolaje" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Category", "Description", "Discount", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("0cc7fc87-36f9-42bb-9855-db85a9527c59"), 4, "Espejo Nam de fibras naturales. Importado de Vietnam Diámetro: 60cm Espejo Diámetro: 19cm", 10, "https://images.fravega.com/f300/6c2f4d2d61e3672653e9789598a83334.jpg.webp", "Espejo Nam 60cm", 66990m },
                    { new Guid("17a27c35-587f-47f3-9903-d43725566f79"), 5, "Perfume para un hombre que vive en el presente, moldeado por la energía de la modernidad. Imprevisible, sorprende con su originalidad.", 0, "https://d3cdlnm7te7ky2.cloudfront.net/media/mf_webp/jpg/media/catalog/product/cache/mtools/300x300/catalog/product//1/1/119078-a-stronger-with-you-edt-100ml_2.webp", "Stronge with you EDT 100ml", 174900m },
                    { new Guid("186bdb57-0273-42c6-8bc2-1d8e6e10e1a3"), 4, "Lampara Colgante Led L2020 Dorado Moderno Deco Luz Desing", 0, "https://http2.mlstatic.com/D_Q_NP_660955-MLA74649779983_022024-B.jpg", "Lampara colgante", 99999m },
                    { new Guid("1a6d0ffc-ba29-48cd-8197-52ea555da5cd"), 5, "Una fragancia cautivadora nacida de la fusióninesperada entre el dulzor de notas afrutadas, la exclusiva rosatecnológica de Mugler y un elegante collar amaderado.", 25, "https://d3cdlnm7te7ky2.cloudfront.net/media/mf_webp/jpg/media/catalog/product/cache/8cafa0a86860701ec5138a548d16cdc2/a/n/angel-nova-edp-rechargable-100ml_1.webp", "Angel nova EDP 100ml", 286000m },
                    { new Guid("1fe0e417-7a02-4b43-9e9f-34a13a32ea24"), 4, "", 0, "https://images.fravega.com/f300/9b2bee9b6b6518b60eb262be396c44fb.jpg.webp", "Juego de Sábanas 1 1/2 plaza Jurassic", 22590m },
                    { new Guid("2094421c-595f-43d0-b7b3-6423e1628018"), 7, "Dragon Ball Figura 10cm Articulado Flash Super Saiyan Broly", 0, "https://wabro.vteximg.com.br/arquivos/ids/187554-320-320/3.jpg?v=638030021553570000", "Broly Dragon ball", 25000m },
                    { new Guid("2728192d-a8d1-4b08-b61e-3599efe217f5"), 9, "Casio calculadora científica a bateria FX-82ms", 20, "https://www.heavenimagenes.com/heavencommerce/11de0e5d-73b3-4c32-910b-8483d00a7205/images/v2/CASIO/8484_medium.jpg", "Calculadora científica", 29400m },
                    { new Guid("2aeb3f24-4096-4629-9c45-34ee6b8e743f"), 4, "La almohada de Piero Cervical está hecha de espuma, mejorando la postura del cuello y hombros. Medida: 65 x 35 cm.", 0, "https://images.fravega.com/f300/30df4745cef1263ea72604f24c3a3929.jpg.webp", "Almohada de espuma cervical", 14999m },
                    { new Guid("37db3c18-d4c9-46dc-9220-35c8e75e35e1"), 1, "Tostadora atma eléctrica toat-39dn", 0, "https://76338a6a.flyingcdn.com/39415-large_default/atma-tostadora-electrica-toat-39dn.jpg", "Tostadora", 39999m },
                    { new Guid("3f009d16-9690-40c6-8853-2823d0f66237"), 8, "Jugo de limón con jengible orgánico 500 cc Las brisas. Apto veganos", 0, "https://biomarket.com.ar/wp-content/uploads/2023/01/BRIS54_1.jpg", "Jugo de limón con jengibre", 37930m },
                    { new Guid("3fdf7770-5cde-44ed-a3aa-1d2680008427"), 10, "Desmalezadora Gamma Naftera 44Cc G1835ar - GAMMA", 15, "https://www.megatone.net/images//Articulos/zoom2x/106/MKT0013GAM-1.jpg", "Desmalezadora", 242500m },
                    { new Guid("47dc66f0-0db6-4fe4-9c12-adcac5414f54"), 9, "Sifap tempera sólida x 6 pastel arte", 0, "https://www.heavenimagenes.com/heavencommerce/11de0e5d-73b3-4c32-910b-8483d00a7205/images/v2/SIFAP/7597_medium.jpg", "Tempera sólida", 12863m },
                    { new Guid("4db7bf99-ded2-430d-957e-b036c2cd5d89"), 3, "Modelo de anteojos de sol para mujer XOLD de la marca RUSTY.", 15, "https://static.wixstatic.com/media/617495_87ec75c8071244158aa28802dcdb8108~mv2.jpg/v1/fill/w_500,h_500,al_c,q_80,usm_0.66_1.00_0.01,enc_auto/617495_87ec75c8071244158aa28802dcdb8108~mv2.jpg", "Anteojos de sol", 89600m },
                    { new Guid("5705d26d-a0aa-46ba-9cc8-26db6062a578"), 2, "PS5 SLIM Standard con lector CD/DVD 1TB + 1 Joystick Regalo", 20, "https://images.fravega.com/f300/dd39a03cd136501e71f52ef06e09c861.jpg.webp", "Consola Sony PlayStation 5", 900000m },
                    { new Guid("5b13da66-b602-4012-a001-d77a79f7494a"), 9, "Mooving agenda 14 x 20 Harry Potter Diaria - Espiralada", 0, "https://www.heavenimagenes.com/heavencommerce/11de0e5d-73b3-4c32-910b-8483d00a7205/images/v2/MOOVING/20080_medium.jpg", "Agenda", 25500m },
                    { new Guid("60b80057-efbb-4c9a-8a9a-00784e542ab2"), 10, "Planta de exterior", 0, "https://www.viverodeluca.com.ar/1096-home_default/alegria-del-hogar.jpg", "Alegria del hogar", 550m },
                    { new Guid("6641151d-6d69-4c69-ad6c-7481309ca2a8"), 8, "Ferrero Rocher – Chocolate Bombón – 8 Unidades", 0, "https://www.argensend.com/wp-content/uploads/2021/09/D_NQ_NP_913338-MLA40945655566_022020-O.jpeg", "Ferrero rocher", 7000m },
                    { new Guid("681b7574-0afd-4db9-a02a-bdbe364dd6f7"), 10, "Cordyline terminalis rubra", 0, "https://www.viverodeluca.com.ar/3334-large_default/cordyline-rubra-dracena-rubra-dracena-rubra.jpg", "Dracena rubra", 7900m },
                    { new Guid("6ec34474-eda8-432f-b99b-950a8ab69444"), 6, "Bicicleta Mountain Bike X 1.0 Rodado 29 Talle 20 Negro - NORDIC", 0, "https://www.megatone.net/Images/Articulos/zoom2x/104/BIC2922NRD.jpg", "Bicicleta mountain bike", 589999m },
                    { new Guid("711a840a-ce5a-47c2-9388-4bf507dcdb98"), 3, "Bolso Amayra Fit Cómodo Gimnasio Deportivo", 0, "https://images.fravega.com/f300/c60063d66bd91a656af89bf3bf4e0694.jpg.webp", "Bolso deportivo", 30800m },
                    { new Guid("7135a466-2efd-4c58-ad8e-7b607ecac5a3"), 5, "La máscara de pestañas Lash Sensational Sky High de larga duración brinda un volumen completo y una longitud ilimitada.", 0, "https://d3cdlnm7te7ky2.cloudfront.net/media/mf_webp/jpg/media/catalog/product/cache/d41ed5f2410977369e7cfa45777ef8e0/1/4/142943-a-lash-sensational-sky-high-washable-very-black_1.webp", "Lash sensational sky high washable - very black", 22659m },
                    { new Guid("847ddf9e-edad-48ef-b301-b36a8837c8cf"), 3, "Reebok zapatillas mujer - Glide mujer ch. -Dk grey-green", 5, "https://megasports.vteximg.com.br/arquivos/ids/224093-1000-1000/41104788055_0.jpg?v=638267497579500000", "Reebok zapatillas mujer", 99999m },
                    { new Guid("a0175162-22ef-4718-b8db-5e13b0c32966"), 7, "Bloques Magneticos Grandes Varias Formas y colores 42 Piezas MG23", 15, "https://images.fravega.com/f300/59412558bc3aad5f4a7e9bcfea3dc30c.jpg.webp", "Bloques magnéticos", 50399m },
                    { new Guid("aad39775-f1c4-47c0-86b2-4c8adabd8b02"), 6, "Soga Saltar Crossfit/fitness Ranbak 737 Azul - RANBAK", 0, "https://www.megatone.net/images/Articulos/zoom2x/304/01/MKT0048RAN.jpg", "Soga saltar crossfit", 7300m },
                    { new Guid("aebe2bd8-c7c5-4be9-b704-3357f8af6f52"), 10, "Maceta de barro Clásica - Nº8", 0, "https://www.viverodeluca.com.ar/1273-large_default/maceta-de-barro-cl%C3%A1sica.jpg", "Maceta de barro", 940m },
                    { new Guid("b76e9bb5-0bed-4c00-861e-5c27a419ac28"), 7, "Hasbro Gaming Simon Juego de memoria electrónica portátil con luces y sonidos para niños de 8 años en adelante", 0, "https://m.media-amazon.com/images/I/81P7iHZ+1lL._AC_UL320_.jpg", "Hasbro Gaming Simon Juego de memoria", 30820m },
                    { new Guid("b9ad0df5-af10-467f-a86a-ef509affecee"), 8, "Yogurt de coco sabor arandanos 170 g quimya. Apto veganos", 0, "https://biomarket.com.ar/wp-content/uploads/2019/07/721450715893.jpg", "Yogurt de coco sabor arandanos", 2600m },
                    { new Guid("c16e9685-0cc9-49a8-849f-34cdb93006c6"), 5, "UV defender protector solar facil fuido FPS50+ 40g - claro.UV Defender Fluido es resiste al agua y al sudor, con textura fluida.", 15, "https://d3cdlnm7te7ky2.cloudfront.net/media/mf_webp/jpg/media/catalog/product/cache/mtools/300x300/catalog/product//1/5/150122-a-uv-defender-protector-solar-facial-fluido-fps50-40g-claro.webp", "Protector solar facial", 16253m },
                    { new Guid("ccb66f89-a3bb-475f-a8b0-24de4d650fa0"), 7, "Legends of Akedo Powerstorm Battle Giants combina 2 figuras de acción de gigantes de batalla.", 0, "https://m.media-amazon.com/images/I/51EXTNFIrYL._AC_.jpg", "Legends of Akedo Powerstorm Battle Giants", 45000m },
                    { new Guid("cd8c6422-9773-4a1d-9f80-dc7f7108f16d"), 2, "Proyector Gadnic Surr 5500 Lúmenes Con Filtro HEPA HDMI x 2 USB x 2", 0, "https://images.fravega.com/f300/5b68c007ab23568b5379ac7708999b9a.jpg.webp", "Proyector", 283179m },
                    { new Guid("d049db6b-ffcf-4ff7-8eed-990bc19e11c2"), 6, "Bote Inflable Explorer Pro 100 22699/0 - INTEX", 10, "https://www.megatone.net/Images/Articulos/zoom2x/116/BOT6990ITX.jpg", "Bote inflable", 42999m },
                    { new Guid("d11bc099-419d-4499-8a32-a7598322eea0"), 9, "Faber castell lapices de colores super soft x 24 escritura artística.", 0, "https://www.heavenimagenes.com/heavencommerce/11de0e5d-73b3-4c32-910b-8483d00a7205/images/v2/FABER%20CASTELL/3388_medium.jpg", "Lapices de colores", 28500m },
                    { new Guid("d5ab1397-8b12-4e48-ab69-dd46df8e3916"), 1, "308L Rt29k577js8 No Frost Ix Inv", 20, "https://www.megatone.net/Images/Articulos/zoom2x/36/HEL2958SSG.jpg", "Heladera samsung", 589999m },
                    { new Guid("d74bfd37-7552-4e69-8df4-df771c966a0a"), 8, "ACEITE ORGANICO DE OLIVA 500CC MAELCA", 10, "https://biomarket.com.ar/wp-content/uploads/2019/09/7798241870102.jpg", "Aceite de oliva", 12350m },
                    { new Guid("d9cc96bc-c0b3-4662-9b7a-e1350f9329e6"), 2, "Drone Gadnic Con Camara Dual 4K", 0, "https://images.fravega.com/f300/60354c20fe3a7afd1b747439ee22d522.jpg.webp", "Drone", 594099m },
                    { new Guid("db875506-8544-47f7-b362-a01d272e2243"), 3, "Mochila De Viaje Travel Tech Grande Amplia Compartimientos", 0, "https://images.fravega.com/f300/eab6e93f47c2a4a5156964361abd6aa0.jpg.webp", "Mochila de viaje", 104500m },
                    { new Guid("dc723fa3-f573-4373-ac2d-fb5068cf300a"), 1, "Batidora atma planetaria bp-at21rp roja", 0, "https://76338a6a.flyingcdn.com/42304-large_default/atma-batidora-planetaria-bp-at21rp-roja.jpg", "Batidora planetaria", 589999m },
                    { new Guid("e93ea063-956e-4950-a2a7-7cdd8370b78f"), 1, "LAvarropas automático WM-85VVC5S6P Inverter vivace 8.5 kg silver", 10, "https://76338a6a.flyingcdn.com/44686-large_default/lg-lavarropas-autwm-85vvc5s6p-inverter-vivace-85kg-silver.jpg", "Lg lavarropas automático", 1285713m },
                    { new Guid("ed7df535-73b7-44b2-9553-2cd87b459881"), 6, "Rueda Doble Para Abdominales Ranbak 730 - RANBAK", 0, "https://www.megatone.net/images/Articulos/zoom2x/304/01/MKT0060RAN.jpg", "Rueda doble para abdominales", 15799m },
                    { new Guid("eef25c7a-27fd-4741-8396-fcd2d9f96bf0"), 2, "Smart Tv 50 Pulgadas 4K Ultra Hd 50Pud7406/77 - PHILIPS", 5, "https://www.megatone.net/Images/Articulos/zoom2x/253/TEL5006PHI.jpg", "Smart Tv 50 pulgadas 4k", 594099m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_Category",
                table: "Product",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_SaleProduct_Product",
                table: "SaleProduct",
                column: "Product");

            migrationBuilder.CreateIndex(
                name: "IX_SaleProduct_Sale",
                table: "SaleProduct",
                column: "Sale");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleProduct");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
