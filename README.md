<img width="1902" height="892" alt="image" src="https://github.com/user-attachments/assets/959cb070-7ee7-433e-8373-ec5021982d8a" /># Bếp Nhà Mình

Website gợi ý món ăn và hướng dẫn nấu ăn, được tái cấu trúc từ prototype HTML/CSS/JavaScript thành ứng dụng **ASP.NET Core MVC + Entity Framework Core + SQLite**.

## Giới thiệu

Bếp Nhà Mình giải quyết câu hỏi “Hôm nay ăn gì?” bằng kho công thức món Việt, tìm kiếm, lọc theo danh mục, chọn món ngẫu nhiên, xem công thức chi tiết và lưu món yêu thích.
https://bepnhaminh.onrender.com/

## Tính năng

- Trang chủ với món nổi bật và danh mục.
- Danh sách 20 món Việt được seed tự động.
- Tìm kiếm bằng Controller + LINQ/EF Core.
- Lọc theo Category bằng query string.
- Chọn món ngẫu nhiên từ database.
- Chi tiết món: nguyên liệu, thời gian, khẩu phần, độ khó, calories, các bước.
- Món liên quan theo category.
- Favorite không cần đăng nhập, định danh theo session.
- Responsive desktop/tablet/mobile.
- Accessibility cơ bản: alt text, label, aria-label.
- Error handling cho route món không tồn tại.

## Công nghệ

- .NET 10 / ASP.NET Core MVC
- C#
- Razor Views
- Entity Framework Core 10
- SQLite
- HTML5 / CSS3 / JavaScript thuần

## Cấu trúc project

```text
BepNhaMinh/
├── Controllers/
├── Data/
├── Models/
├── Services/
├── ViewModels/
├── Views/
├── wwwroot/
├── Migrations/
├── Program.cs
├── appsettings.json
├── BepNhaMinh.csproj
└── README.md
```

## Database

SQLite được lưu tại `bepnhaminh.db`. Khi ứng dụng chạy lần đầu, `EnsureCreatedAsync()` tạo schema và `DbInitializer` seed category, ingredient, recipe, recipe ingredient và cooking step.

`Migrations/` được giữ sẵn để có thể chuyển sang migration workflow khi project bước sang giai đoạn phát triển dài hạn. Với bản demo sinh viên, `EnsureCreated` giúp khởi động nhanh và không phụ thuộc lệnh migration trước lần chạy đầu.

## Cách chạy

Cần cài .NET 10 SDK.

```bash
dotnet restore
dotnet build
dotnet run
```

Sau đó mở URL localhost được ASP.NET Core in trong terminal, ví dụ `https://localhost:7xxx`.

## Các route chính

- `/` — Trang chủ
- `/Recipe` — Danh sách món ăn
- `/Recipe/Random` — Ăn gì hôm nay?
- `/Recipe/Details/1` — Chi tiết món
- `/Favorite` — Món yêu thích
- `/Recipe?search=gà` — Tìm kiếm
- `/Recipe?category=Món nước` — Lọc category

## Ghi chú

Ảnh demo là SVG local trong `wwwroot/images/recipes`, vì vậy ứng dụng không phụ thuộc API ảnh bên ngoài và vẫn chạy offline sau khi restore package.
