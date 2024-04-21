using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CategoriesSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            INSERT INTO shop.""Categories""(""Name"") VALUES
                  ('Ноутбуки'),
                  ('Телевизоры'),
                  ('Фото и видео'),
                  ('Смартфоны'),
                  ('Аудиотехника'),
                  ('Гаджеты'),
                  ('Бытовая техника')
");
            migrationBuilder.Sql(@"
            UPDATE shop.""Items"" SET ""CategoryId"" = (SELECT c.""Id"" FROM shop.""Categories"" c LIMIT 1)
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
