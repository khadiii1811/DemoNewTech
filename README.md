# Customer Management API

Ứng dụng ASP.NET Core Web API để quản lý khách hàng với đầy đủ các chức năng CRUD.

## Tính năng

- ✅ Tạo, đọc, cập nhật, xóa khách hàng
- ✅ Validation dữ liệu đầu vào
- ✅ Repository pattern và Service layer
- ✅ Entity Framework Core với SQL Server
- ✅ Swagger/OpenAPI documentation
- ✅ Error handling và logging
- ✅ RESTful API design

## Cấu trúc dự án

```
CustomerManagement/
├── Controllers/          # API Controllers
├── Data/                # DbContext và Database
├── Models/              # Domain Models và DTOs
├── Repositories/        # Data Access Layer
├── Services/            # Business Logic Layer
└── Program.cs           # Application entry point
```

## Yêu cầu hệ thống

- .NET 8.0 SDK
- SQL Server hoặc LocalDB
- Visual Studio 2022 hoặc VS Code

## Cài đặt và chạy

### 1. Clone repository
```bash
git clone <repository-url>
cd CustomerManagement
```

### 2. Cài đặt dependencies
```bash
dotnet restore
```

### 3. Tạo database
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Chạy ứng dụng
```bash
dotnet run
```

Ứng dụng sẽ chạy tại: `https://localhost:7000` (hoặc port được cấu hình)

## API Endpoints

### Customers

| Method | Endpoint | Mô tả |
|--------|----------|-------|
| GET | `/api/customers` | Lấy danh sách tất cả khách hàng |
| GET | `/api/customers/{id}` | Lấy thông tin khách hàng theo ID |
| POST | `/api/customers` | Tạo khách hàng mới |
| PUT | `/api/customers/{id}` | Cập nhật thông tin khách hàng |
| DELETE | `/api/customers/{id}` | Xóa khách hàng |
| PATCH | `/api/customers/{id}/activate` | Kích hoạt khách hàng |
| PATCH | `/api/customers/{id}/deactivate` | Vô hiệu hóa khách hàng |

## Swagger Documentation

Truy cập Swagger UI tại: `https://localhost:7000/swagger`

## Cấu hình Database

Connection string được cấu hình trong `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CustomerManagementDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

## Model Structure

### Customer
- `Id`: Primary key
- `Name`: Tên khách hàng (required, 2-100 ký tự)
- `Email`: Email (required, unique, valid format)
- `Phone`: Số điện thoại (optional, valid format)
- `Address`: Địa chỉ (optional, max 500 ký tự)
- `IsActive`: Trạng thái hoạt động
- `CreatedAt`: Thời gian tạo
- `UpdatedAt`: Thời gian cập nhật cuối

## Validation Rules

- **Name**: Bắt buộc, độ dài 2-100 ký tự
- **Email**: Bắt buộc, định dạng email hợp lệ, unique
- **Phone**: Tùy chọn, định dạng số điện thoại hợp lệ
- **Address**: Tùy chọn, độ dài tối đa 500 ký tự

## Error Handling

API trả về các HTTP status codes chuẩn:
- `200 OK`: Thành công
- `201 Created`: Tạo mới thành công
- `400 Bad Request`: Dữ liệu đầu vào không hợp lệ
- `404 Not Found`: Không tìm thấy resource
- `500 Internal Server Error`: Lỗi server

## Development

### Thêm migration mới
```bash
dotnet ef migrations add <MigrationName>
```

### Cập nhật database
```bash
dotnet ef database update
```

### Xóa database
```bash
dotnet ef database drop
```

## Testing

Sử dụng file `CustomerManagement.http` để test API với REST Client extension trong VS Code.

## Contributing

1. Fork repository
2. Tạo feature branch
3. Commit changes
4. Push to branch
5. Tạo Pull Request
