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
                    { new Guid("0ec581e2-a70e-40fc-bdc5-115ac6554aa7"), 2, "Proyector Gadnic Surr 5500 Lúmenes Con Filtro HEPA HDMI x 2 USB x 2", 20, "https://images.fravega.com/f300/5b68c007ab23568b5379ac7708999b9a.jpg.webp", "Proyector Gadnic Surr 5500", 283179m },
                    { new Guid("0ef3c3ce-d2b8-43f1-9f77-b2d80dc1a893"), 6, "Soga Saltar Crossfit/fitness Ranbak 737 Azul - RANBAK", 0, "https://www.megatone.net/images/Articulos/zoom2x/304/01/MKT0048RAN.jpg", "Soga saltar crossfit Ranbak", 7300m },
                    { new Guid("10c9611b-c709-4464-ba4e-4602204c616e"), 3, "Bolso Amayra Fit Cómodo Gimnasio Deportivo", 0, "https://images.fravega.com/f300/c60063d66bd91a656af89bf3bf4e0694.jpg.webp", "Bolso deportivo Amayra", 30800m },
                    { new Guid("10fbd907-a04e-4928-9857-e3dd2d37b98f"), 9, "Sifap tempera sólida x 6 pastel arte", 0, "https://www.heavenimagenes.com/heavencommerce/11de0e5d-73b3-4c32-910b-8483d00a7205/images/v2/SIFAP/7597_medium.jpg", "Tempera sólida", 12863m },
                    { new Guid("182b0b89-3168-4fa2-8b60-bd3bea918840"), 6, "Bicicleta Mountain Bike X 1.0 Rodado 29 Talle 20 Negro - NORDIC", 25, "https://www.megatone.net/Images/Articulos/zoom2x/104/BIC2922NRD.jpg", "Bicicleta mountain bike rodado 29", 589999m },
                    { new Guid("194404ce-d7ac-4909-99c4-fa539bdd8db4"), 6, "Rueda Doble Para Abdominales Ranbak 730 - RANBAK", 10, "https://www.megatone.net/images/Articulos/zoom2x/304/01/MKT0060RAN.jpg", "Rueda doble para abdominales Ranbak", 15799m },
                    { new Guid("1ae43399-86c2-4a42-84fe-e46bd2dd6786"), 7, "Dragon Ball Figura 10cm Articulado Flash Super Saiyan Broly", 10, "https://wabro.vteximg.com.br/arquivos/ids/187554-320-320/3.jpg?v=638030021553570000", "Broly Dragon ball", 25000m },
                    { new Guid("1f2a9e14-928a-4b35-b2c9-9295e15839d6"), 1, "Descripción: 8 niveles de potencia-movimiento planetario para amasado,mezcla y batido-\r\nMarca: Ultracomb\r\nOrigen: Argentina\r\nFunciones: bol acero inoxidable\r\nMateriales: N/A\r\nGarantia de Fabrica (meses): 6\r\nCapacidad: 5 Litros\r\nPotencia (Watts): 800 w\r\naccesorios: 3 accesorios/cabezal rebatible", 20, "https://www.perozzi.com.ar/16649-large_default/ultracomb-batidora-planetaria-gris-bb-2106-37801109.jpg", "Ultracomb batidora planetario Gris BB 2106", 589999m },
                    { new Guid("1f3fb438-bd1b-4fea-b8e8-2b7f51546967"), 5, "Una fragancia cautivadora nacida de la fusióninesperada entre el dulzor de notas afrutadas, la exclusiva rosatecnológica de Mugler y un elegante collar amaderado.", 25, "https://d3cdlnm7te7ky2.cloudfront.net/media/mf_webp/jpg/media/catalog/product/cache/8cafa0a86860701ec5138a548d16cdc2/a/n/angel-nova-edp-rechargable-100ml_1.webp", "Perfume Angel nova EDP 100ml", 286000m },
                    { new Guid("200a1994-0bda-4fb5-890f-f5106568a52c"), 5, "Perfume para un hombre que vive en el presente, moldeado por la energía de la modernidad. Imprevisible, sorprende con su originalidad.", 25, "https://d3cdlnm7te7ky2.cloudfront.net/media/mf_webp/jpg/media/catalog/product/cache/mtools/300x300/catalog/product//1/1/119078-a-stronger-with-you-edt-100ml_2.webp", "Perfume Stronge with you EDT 100ml", 174900m },
                    { new Guid("2ffb0678-7074-4806-ae3b-b1d11b8981c9"), 2, "Smart Tv 50 Pulgadas 4K Ultra Hd 50Pud7406/77 - PHILIPS", 25, "https://www.megatone.net/Images/Articulos/zoom2x/253/TEL5006PHI.jpg", "Smart Tv 50 pulgadas 4k", 594099m },
                    { new Guid("3c453d3b-be0f-451b-935a-3074d49d499c"), 3, "Reebok zapatillas mujer - Glide mujer ch. -Dk grey-green", 25, "https://megasports.vteximg.com.br/arquivos/ids/224093-1000-1000/41104788055_0.jpg?v=638267497579500000", "Zapatillas reebok glide mujer", 99999m },
                    { new Guid("3f14062b-1681-4f5c-8fb2-39094b9b1465"), 7, "Hasbro Gaming Simon Juego de memoria electrónica portátil con luces y sonidos para niños de 8 años en adelante", 10, "https://m.media-amazon.com/images/I/81P7iHZ+1lL._AC_UL320_.jpg", "Hasbro Gaming Simon Juego de memoria", 30820m },
                    { new Guid("423064ab-e4c9-43df-9890-b5d290af3b76"), 3, "Mochila Plegable Travel Tech Viajes Práctica Liviana Cecchini es una empresa marroquinera con presencia en el mercado desde el año 1991. Creada en Rosario hoy cuenta con puntos de ventas en distintas partes del país como San Lorenzo, Villa Gobernador Gálvez, Venado Tuerto, Paraná, Santa Fe, Neuquén y CABA. Características: Confección: Material 100% poliéster Dimensiones de la mochila plegada: alto 18 cm x ancho 20 cm x profundidad 2 cm Dimensiones de la mochila abierta: alto 42 cm x ancho 29cm x profundidad 12 cm", 10, "https://images.fravega.com/f300/eab6e93f47c2a4a5156964361abd6aa0.jpg.webp", "Mochila de viaje travel tech", 104500m },
                    { new Guid("478c2636-c4b5-422d-be08-515f45f72477"), 8, "ACEITE ORGANICO DE OLIVA 500CC MAELCA", 15, "https://biomarket.com.ar/wp-content/uploads/2019/09/7798241870102.jpg", "Aceite de oliva", 12350m },
                    { new Guid("4e85ec95-48cc-4fde-8070-8c2672610a97"), 7, "Bloques Magneticos Grandes Varias Formas y colores 42 Piezas MG23", 20, "https://images.fravega.com/f300/59412558bc3aad5f4a7e9bcfea3dc30c.jpg.webp", "Bloques magnéticos", 50399m },
                    { new Guid("501be9fb-78f2-4a84-a9e4-9402bb4021c4"), 10, "Astrophytum asterias es un cactus sin espinas que puebla las regiones del sur de Estados Unidos y el norte de México. De tamaño pequeño, es considerado como una especie en peligro de extinción.", 10, "https://encrypted-tbn0.gstatic.com/shopping?q=tbn:ANd9GcTG8NlAr6uybZn0eXG-xCGU5wpnAB97GpN7z4m7cJF0LYCirk_Li9eSfzBJ1ci8yHlBI5URFnlg84H57G9vPsmAlekPMBTJwoPP1iwXOcuo&usqp=CAE", "Cactus Asteria Variegata", 30000m },
                    { new Guid("5611d608-f438-4caf-a22b-8c73317d4496"), 4, "La almohada de Piero Cervical está hecha de espuma, mejorando la postura del cuello y hombros. Medida: 65 x 35 cm.", 15, "https://images.fravega.com/f300/30df4745cef1263ea72604f24c3a3929.jpg.webp", "Almohada de espuma cervical", 14999m },
                    { new Guid("5b5428d3-b783-4bb3-b940-4dedc4812809"), 2, "PS5 SLIM Standard con lector CD/DVD 1TB + 1 Joystick Regalo", 20, "https://images.fravega.com/f300/dd39a03cd136501e71f52ef06e09c861.jpg.webp", "Consola Sony PlayStation 5", 900000m },
                    { new Guid("5e15a6c2-450d-4c80-87c3-a3e5715ce1f9"), 2, "Drone Gadnic Con Camara Dual 4K", 30, "https://images.fravega.com/f300/60354c20fe3a7afd1b747439ee22d522.jpg.webp", "Drone Gadnic 4k", 594099m },
                    { new Guid("5f645fd6-67d8-482c-a6f5-24a4822b3714"), 9, "Casio calculadora científica a bateria FX-82ms", 20, "https://www.heavenimagenes.com/heavencommerce/11de0e5d-73b3-4c32-910b-8483d00a7205/images/v2/CASIO/8484_medium.jpg", "Calculadora científica", 29400m },
                    { new Guid("62ef7a6f-98c6-4677-9920-335f9dfb7be8"), 4, "Espejo Nam de fibras naturales. Importado de Vietnam Diámetro: 60cm Espejo Diámetro: 19cm", 10, "https://images.fravega.com/f300/6c2f4d2d61e3672653e9789598a83334.jpg.webp", "Espejo Nam 60cm", 66990m },
                    { new Guid("66d17410-8f3b-47e2-ac63-b1d5d9bce938"), 9, "Faber castell lapices de colores super soft x 24 escritura artística.", 0, "https://www.heavenimagenes.com/heavencommerce/11de0e5d-73b3-4c32-910b-8483d00a7205/images/v2/FABER%20CASTELL/3388_medium.jpg", "Lapices de colores", 28500m },
                    { new Guid("717dc797-619f-448c-b7d1-2d04f5230e96"), 1, "La tostadora Disney tiene un diseño moderno, liviano de color rojo. Hacé las tostadas mas originales con la cara de Mickey Mouse y disfrutá de tus desayunos y meriendas de la manera más divertida. Brinda 7 niveles de tostado y es fácil de limpiar ya que viene con bandeja de migas.", 15, "https://www.perozzi.com.ar/39420-large_default/atma-tostadora-electrica-toat-39dn.jpg", "Tostadora atma eléctrica toat-39dn", 39999m },
                    { new Guid("91ff3094-a179-40c5-be6d-d856b06ad382"), 8, "Jugo de limón con jengible orgánico 500 cc Las brisas. Apto veganos", 0, "https://biomarket.com.ar/wp-content/uploads/2023/01/BRIS54_1.jpg", "Jugo de limón con jengibre", 3793m },
                    { new Guid("9716cbbc-f372-49ca-96e6-2f21c9b7b30e"), 4, "", 10, "https://images.fravega.com/f300/9b2bee9b6b6518b60eb262be396c44fb.jpg.webp", "Juego de Sábanas 1 1/2 plaza Jurassic", 22590m },
                    { new Guid("979162bc-3625-48d7-8530-54cef526a34b"), 10, "Desmalezadora Gamma Naftera 44Cc G1835ar - GAMMA", 15, "https://www.megatone.net/images//Articulos/zoom2x/106/MKT0013GAM-1.jpg", "Desmalezadora", 242500m },
                    { new Guid("bc86ec6f-2fcf-4413-8ea5-d166b434efef"), 8, "Yogurt de coco sabor arandanos 170 g quimya. Apto veganos", 0, "https://biomarket.com.ar/wp-content/uploads/2019/07/721450715893.jpg", "Yogurt de coco sabor arandanos", 2600m },
                    { new Guid("bd369db5-8f06-4aef-a147-5adf28905227"), 5, "UV defender protector solar facil fuido FPS50+ 40g - claro.UV Defender Fluido es resiste al agua y al sudor, con textura fluida.", 15, "https://d3cdlnm7te7ky2.cloudfront.net/media/mf_webp/jpg/media/catalog/product/cache/mtools/300x300/catalog/product//1/5/150122-a-uv-defender-protector-solar-facial-fluido-fps50-40g-claro.webp", "Protector solar facial UV defender", 16253m },
                    { new Guid("c5bc982d-5219-492d-8c4f-24910ce2b42d"), 7, "Legends of Akedo Powerstorm Battle Giants combina 2 figuras de acción de gigantes de batalla.", 15, "https://m.media-amazon.com/images/I/51EXTNFIrYL._AC_.jpg", "Legends of Akedo Powerstorm Battle Giants", 45000m },
                    { new Guid("c5fd9f72-7baf-40f5-ad4f-1007e98ba61e"), 1, "308L Rt29k577js8 No Frost Ix Inv", 20, "https://www.megatone.net/Images/Articulos/zoom2x/36/HEL2958SSG.jpg", "Heladera samsung 308L", 589999m },
                    { new Guid("d4511e5c-ca7d-47f8-bd0c-4f4285fcb130"), 9, "Mooving agenda 14 x 20 Harry Potter Diaria - Espiralada", 15, "https://www.heavenimagenes.com/heavencommerce/11de0e5d-73b3-4c32-910b-8483d00a7205/images/v2/MOOVING/20080_medium.jpg", "Agenda", 25500m },
                    { new Guid("d4968223-01dc-477d-93da-cc237f917a00"), 10, "La Impatiens walleriana, comúnmente conocida como ‘Alegría del hogar’, es una planta que se distingue por su belleza ornamental y su fácil cultivo.", 0, "https://th.bing.com/th/id/OIP.ESv3sAmz6E3HL_WFTlqcfQHaHa?rs=1&pid=ImgDetMain", "Alegria del hogar", 550m },
                    { new Guid("ddfb8fb4-8959-42c1-9679-f9fe5ec8286b"), 5, "La máscara de pestañas Lash Sensational Sky High de larga duración brinda un volumen completo y una longitud ilimitada.", 10, "https://d3cdlnm7te7ky2.cloudfront.net/media/mf_webp/jpg/media/catalog/product/cache/d41ed5f2410977369e7cfa45777ef8e0/1/4/142943-a-lash-sensational-sky-high-washable-very-black_1.webp", "Lash sensational sky high washable - very black", 22659m },
                    { new Guid("ebce1337-8dfc-4499-861a-b3900932b0c9"), 6, "Bote Inflable Explorer Pro 100 22699/0 - INTEX", 10, "https://www.megatone.net/Images/Articulos/zoom2x/116/BOT6990ITX.jpg", "Bote inflable intex", 42999m },
                    { new Guid("ec732784-5f80-442d-bc4c-3a0839486cbe"), 4, "Lampara Colgante Led L2020 Dorado Moderno Deco Luz Desing", 20, "https://http2.mlstatic.com/D_Q_NP_660955-MLA74649779983_022024-B.jpg", "Lampara colgante L2020", 99999m },
                    { new Guid("f4b8dbb4-7ce3-497f-99a7-8a1c805c9d71"), 3, "Modelo de anteojos de sol para mujer XOLD de la marca RUSTY.", 15, "https://static.wixstatic.com/media/617495_87ec75c8071244158aa28802dcdb8108~mv2.jpg/v1/fill/w_500,h_500,al_c,q_80,usm_0.66_1.00_0.01,enc_auto/617495_87ec75c8071244158aa28802dcdb8108~mv2.jpg", "Anteojos de sol XOLD Rusty ", 89600m },
                    { new Guid("fa6d0760-d688-4695-9d40-1bcf47e25b9a"), 10, "Esta maceta de barro rojo tipo coco está diseñada como maceta de suelo. Cuenta con orificio para drenaje del agua. También puede ser utilizada como macetero o cubremaceta, albergando en su interior otras macetas de menores dimensiones. Se ofrece en un diámetro de 23 cm,", 0, "https://www.ceramicaramblena.com/wp-content/uploads/2021/11/coco-terracota-maceta-grande-modelo-03-01-510x382.jpg", "Coco terracota – Maceta grande – Modelo 03", 6000m },
                    { new Guid("fb6d807a-cb90-488d-906e-e00c9b18f636"), 1, "LAvarropas automático WM-85VVC5S6P Inverter vivace 8.5 kg silver", 30, "https://www.perozzi.com.ar/44686-large_default/lg-lavarropas-autwm-85vvc5s6p-inverter-vivace-85kg-silver.jpg", "Lg lavarropas automático 8.5 kg", 1285713m },
                    { new Guid("fe2f9a1e-898b-4295-8f1f-d1c905d26fac"), 8, "Ferrero Rocher – Chocolate Bombón – 8 Unidades", 0, "https://www.argensend.com/wp-content/uploads/2021/09/D_NQ_NP_913338-MLA40945655566_022020-O.jpeg", "Ferrero rocher", 7000m }
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
