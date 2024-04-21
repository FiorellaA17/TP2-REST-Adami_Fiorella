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
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
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
                    { new Guid("00abc863-b6b2-4bcf-bfae-f8500bc10761"), 8, "Yogurt de coco sabor arandanos 170 g quimya. Apto veganos", 0, "https://biomarket.com.ar/wp-content/uploads/2019/07/721450715893.jpg", "Yogurt de coco sabor arandanos", 2600m },
                    { new Guid("00fdcda3-1fb5-40d3-b7df-a633a38bf2bb"), 10, "Maceta de barro Clásica - Nº8", 0, "https://www.viverodeluca.com.ar/1273-large_default/maceta-de-barro-cl%C3%A1sica.jpg", "Maceta de barro", 940m },
                    { new Guid("018407da-8e54-4a14-8ba6-8d7124f43e55"), 1, "Tostadora atma eléctrica toat-39dn", 0, "https://76338a6a.flyingcdn.com/39415-large_default/atma-tostadora-electrica-toat-39dn.jpg", "Tostadora", 39999m },
                    { new Guid("0c35d554-8fe1-4ce9-8aad-0f447396302a"), 4, "Lampara Colgante Led L2020 Dorado Moderno Deco Luz Desing", 0, "https://http2.mlstatic.com/D_Q_NP_660955-MLA74649779983_022024-B.jpg", "Lampara colgante", 99999m },
                    { new Guid("1056235e-f252-4f61-9119-378ae07b9179"), 2, "Drone Gadnic Con Camara Dual 4K", 0, "https://images.fravega.com/f300/60354c20fe3a7afd1b747439ee22d522.jpg.webp", "Drone", 594099m },
                    { new Guid("12f2fab3-6215-4c43-b401-7c605c93ceea"), 10, "Planta de exterior", 0, "https://www.viverodeluca.com.ar/1096-home_default/alegria-del-hogar.jpg", "Alegria del hogar", 550m },
                    { new Guid("156984a9-42c6-4a0e-8bd5-021147e07f58"), 8, "ACEITE ORGANICO DE OLIVA 500CC MAELCA", 10, "https://biomarket.com.ar/wp-content/uploads/2019/09/7798241870102.jpg", "Aceite de oliva", 12350m },
                    { new Guid("1e5ea14f-7967-4d6b-968b-595af8e6a5b9"), 7, "Legends of Akedo Powerstorm Battle Giants combina 2 figuras de acción de gigantes de batalla.", 0, "https://m.media-amazon.com/images/I/51EXTNFIrYL._AC_.jpg", "Legends of Akedo Powerstorm Battle Giants", 45000m },
                    { new Guid("2f6e62e6-a543-44ed-ba06-2ac6c85a6b3f"), 8, "Ferrero Rocher – Chocolate Bombón – 8 Unidades", 0, "https://www.argensend.com/wp-content/uploads/2021/09/D_NQ_NP_913338-MLA40945655566_022020-O.jpeg", "Ferrero rocher", 7000m },
                    { new Guid("38f40930-b6b8-4379-821b-b22e58ce7c33"), 3, "Bolso Amayra Fit Cómodo Gimnasio Deportivo", 0, "https://images.fravega.com/f300/c60063d66bd91a656af89bf3bf4e0694.jpg.webp", "Bolso deportivo", 30800m },
                    { new Guid("3a7c79c1-fec8-4f89-a00d-7a3e6b5e4f13"), 3, "Mochila De Viaje Travel Tech Grande Amplia Compartimientos", 0, "https://images.fravega.com/f300/eab6e93f47c2a4a5156964361abd6aa0.jpg.webp", "Mochila de viaje", 104500m },
                    { new Guid("4611ee33-47d0-4a3d-8ecf-6685bed65b98"), 6, "Bicicleta Mountain Bike X 1.0 Rodado 29 Talle 20 Negro - NORDIC", 0, "https://www.megatone.net/Images/Articulos/zoom2x/104/BIC2922NRD.jpg", "Bicicleta mountain bike", 589999m },
                    { new Guid("50cb3c07-06e7-4b6a-afec-1e3ab1ad21e1"), 9, "Faber castell lapices de colores super soft x 24 escritura artística.", 0, "https://www.heavenimagenes.com/heavencommerce/11de0e5d-73b3-4c32-910b-8483d00a7205/images/v2/FABER%20CASTELL/3388_medium.jpg", "Lapices de colores", 28500m },
                    { new Guid("5236db35-a078-4341-b732-2ae8a591c83a"), 1, "308L Rt29k577js8 No Frost Ix Inv", 20, "https://www.megatone.net/Images/Articulos/zoom2x/36/HEL2958SSG.jpg", "Heladera samsung", 589999m },
                    { new Guid("534a9b96-d48f-478b-a0af-fd3d5c565675"), 3, "Reebok zapatillas mujer - Glide mujer ch. -Dk grey-green", 5, "https://megasports.vteximg.com.br/arquivos/ids/224093-1000-1000/41104788055_0.jpg?v=638267497579500000", "Reebok zapatillas mujer", 99999m },
                    { new Guid("58a3c08d-cc63-465d-ba04-32da45208ce4"), 2, "Smart Tv 50 Pulgadas 4K Ultra Hd 50Pud7406/77 - PHILIPS", 5, "https://www.megatone.net/Images/Articulos/zoom2x/253/TEL5006PHI.jpg", "Smart Tv 50 pulgadas 4k", 594099m },
                    { new Guid("59777daa-0ac4-4b21-a5f8-2635f3f3887a"), 9, "Sifap tempera sólida x 6 pastel arte", 0, "https://www.heavenimagenes.com/heavencommerce/11de0e5d-73b3-4c32-910b-8483d00a7205/images/v2/SIFAP/7597_medium.jpg", "Tempera sólida", 12863m },
                    { new Guid("5f255ebf-0ce1-498f-a71a-407cc16a81db"), 8, "Jugo de limón con jengible orgánico 500 cc Las brisas. Apto veganos", 0, "https://biomarket.com.ar/wp-content/uploads/2023/01/BRIS54_1.jpg", "Jugo de limón con jengibre", 37930m },
                    { new Guid("5ff62fca-e66e-432d-9988-052c93e55f01"), 1, "LAvarropas automático WM-85VVC5S6P Inverter vivace 8.5 kg silver", 10, "https://76338a6a.flyingcdn.com/44686-large_default/lg-lavarropas-autwm-85vvc5s6p-inverter-vivace-85kg-silver.jpg", "Lg lavarropas automático", 1285713m },
                    { new Guid("67c44c82-26d2-45e4-a4ad-cfdc11219b24"), 4, "", 0, "https://images.fravega.com/f300/9b2bee9b6b6518b60eb262be396c44fb.jpg.webp", "Juego de Sábanas 1 1/2 plaza Jurassic", 22590m },
                    { new Guid("7f72271e-d722-491a-9552-a93db2458d53"), 7, "Dragon Ball Figura 10cm Articulado Flash Super Saiyan Broly", 0, "https://wabro.vteximg.com.br/arquivos/ids/187554-320-320/3.jpg?v=638030021553570000", "Broly Dragon ball", 25000m },
                    { new Guid("89dac5a1-02c1-4e8d-b88c-6680c34cb1d9"), 10, "Cordyline terminalis rubra", 0, "https://www.viverodeluca.com.ar/3334-large_default/cordyline-rubra-dracena-rubra-dracena-rubra.jpg", "Dracena rubra", 7900m },
                    { new Guid("8f639169-4766-41a2-9b16-09ac0f8d58f7"), 9, "Casio calculadora científica a bateria FX-82ms", 20, "https://www.heavenimagenes.com/heavencommerce/11de0e5d-73b3-4c32-910b-8483d00a7205/images/v2/CASIO/8484_medium.jpg", "Calculadora científica", 29400m },
                    { new Guid("8ffbfe6f-a443-4b95-818d-a0fc55bbdfb9"), 6, "Bote Inflable Explorer Pro 100 22699/0 - INTEX", 10, "https://www.megatone.net/Images/Articulos/zoom2x/116/BOT6990ITX.jpg", "Bote inflable", 42999m },
                    { new Guid("b1f9e349-0425-4be6-9b9e-557cb5ccbd0c"), 7, "Hasbro Gaming Simon Juego de memoria electrónica portátil con luces y sonidos para niños de 8 años en adelante", 0, "https://m.media-amazon.com/images/I/81P7iHZ+1lL._AC_UL320_.jpg", "Hasbro Gaming Simon Juego de memoria", 30820m },
                    { new Guid("b350de05-8c6e-4e3c-b14a-3b7e877bb48e"), 4, "Espejo Nam de fibras naturales. Importado de Vietnam Diámetro: 60cm Espejo Diámetro: 19cm", 10, "https://images.fravega.com/f300/6c2f4d2d61e3672653e9789598a83334.jpg.webp", "Espejo Nam 60cm", 66990m },
                    { new Guid("b6ad4474-7b9d-4e96-8d80-cf81cad20503"), 5, "Perfume para un hombre que vive en el presente, moldeado por la energía de la modernidad. Imprevisible, sorprende con su originalidad.", 0, "https://d3cdlnm7te7ky2.cloudfront.net/media/mf_webp/jpg/media/catalog/product/cache/mtools/300x300/catalog/product//1/1/119078-a-stronger-with-you-edt-100ml_2.webp", "Stronge with you EDT 100ml", 174900m },
                    { new Guid("bc9eae98-a596-4cb7-a533-4f2e1209a523"), 2, "PS5 SLIM Standard con lector CD/DVD 1TB + 1 Joystick Regalo", 20, "https://images.fravega.com/f300/dd39a03cd136501e71f52ef06e09c861.jpg.webp", "Consola Sony PlayStation 5", 900000m },
                    { new Guid("bf4dbdcc-6f45-4094-a722-63fe05a795a1"), 9, "Mooving agenda 14 x 20 Harry Potter Diaria - Espiralada", 0, "https://www.heavenimagenes.com/heavencommerce/11de0e5d-73b3-4c32-910b-8483d00a7205/images/v2/MOOVING/20080_medium.jpg", "Agenda", 25500m },
                    { new Guid("c3f20009-bc25-4259-ba46-a3b5493d657a"), 6, "Soga Saltar Crossfit/fitness Ranbak 737 Azul - RANBAK", 0, "https://www.megatone.net/images/Articulos/zoom2x/304/01/MKT0048RAN.jpg", "Soga saltar crossfit", 7300m },
                    { new Guid("c5c266f1-00c9-470d-9961-1a19b409433d"), 5, "UV defender protector solar facil fuido FPS50+ 40g - claro.UV Defender Fluido es resiste al agua y al sudor, con textura fluida.", 15, "https://d3cdlnm7te7ky2.cloudfront.net/media/mf_webp/jpg/media/catalog/product/cache/mtools/300x300/catalog/product//1/5/150122-a-uv-defender-protector-solar-facial-fluido-fps50-40g-claro.webp", "Protector solar facial", 16253m },
                    { new Guid("cde96a03-fe14-46bc-b979-5f4fba14cfe1"), 2, "Proyector Gadnic Surr 5500 Lúmenes Con Filtro HEPA HDMI x 2 USB x 2", 0, "https://images.fravega.com/f300/5b68c007ab23568b5379ac7708999b9a.jpg.webp", "Proyector", 283179m },
                    { new Guid("cf9a3a8b-a7b8-4078-95a5-fcd3409c0912"), 6, "Rueda Doble Para Abdominales Ranbak 730 - RANBAK", 0, "https://www.megatone.net/images/Articulos/zoom2x/304/01/MKT0060RAN.jpg", "Rueda doble para abdominales", 15799m },
                    { new Guid("cfca429a-497b-4a26-b6fa-fdedbc0bcb60"), 4, "La almohada de Piero Cervical está hecha de espuma, mejorando la postura del cuello y hombros. Medida: 65 x 35 cm.", 0, "https://images.fravega.com/f300/30df4745cef1263ea72604f24c3a3929.jpg.webp", "Almohada de espuma cervical", 14999m },
                    { new Guid("d931f8df-480f-4c60-9b81-e896909abcd1"), 3, "Modelo de anteojos de sol para mujer XOLD de la marca RUSTY.", 15, "https://static.wixstatic.com/media/617495_87ec75c8071244158aa28802dcdb8108~mv2.jpg/v1/fill/w_500,h_500,al_c,q_80,usm_0.66_1.00_0.01,enc_auto/617495_87ec75c8071244158aa28802dcdb8108~mv2.jpg", "Anteojos de sol", 89600m },
                    { new Guid("e3b6b6de-4226-4983-9005-002aef62d832"), 1, "Batidora atma planetaria bp-at21rp roja", 0, "https://76338a6a.flyingcdn.com/42304-large_default/atma-batidora-planetaria-bp-at21rp-roja.jpg", "Batidora planetaria", 589999m },
                    { new Guid("e6e5672d-4c89-4680-a09d-cc95db9c830f"), 5, "La máscara de pestañas Lash Sensational Sky High de larga duración brinda un volumen completo y una longitud ilimitada.", 0, "https://d3cdlnm7te7ky2.cloudfront.net/media/mf_webp/jpg/media/catalog/product/cache/d41ed5f2410977369e7cfa45777ef8e0/1/4/142943-a-lash-sensational-sky-high-washable-very-black_1.webp", "Lash sensational sky high washable - very black", 22659m },
                    { new Guid("ea3647bd-ace1-4081-b950-0aa67752f047"), 5, "Una fragancia cautivadora nacida de la fusióninesperada entre el dulzor de notas afrutadas, la exclusiva rosatecnológica de Mugler y un elegante collar amaderado.", 25, "https://d3cdlnm7te7ky2.cloudfront.net/media/mf_webp/jpg/media/catalog/product/cache/8cafa0a86860701ec5138a548d16cdc2/a/n/angel-nova-edp-rechargable-100ml_1.webp", "Angel nova EDP 100ml", 286000m },
                    { new Guid("ef3ac19a-f1ee-4c6e-87de-91ae3bd05427"), 7, "Bloques Magneticos Grandes Varias Formas y colores 42 Piezas MG23", 15, "https://images.fravega.com/f300/59412558bc3aad5f4a7e9bcfea3dc30c.jpg.webp", "Bloques magnéticos", 50399m },
                    { new Guid("ef947405-0abe-4bfa-b1af-1d7e0359b037"), 10, "Desmalezadora Gamma Naftera 44Cc G1835ar - GAMMA", 15, "https://www.megatone.net/images//Articulos/zoom2x/106/MKT0013GAM-1.jpg", "Desmalezadora", 242500m }
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
