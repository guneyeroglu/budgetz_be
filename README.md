# BudgetZ API

BudgetZ API, kişisel harcamaları takip etmek için geliştirilmiş bir .NET Web API projesidir.

## Gereksinimler

- .NET 9.0
- PostgreSQL (Çalışır durumda olmalı)

## Kurulum

1. Projeyi klonlayın:

```bash
git clone https://github.com/your-username/budgetz-be.git
cd budgetz-be
```

2. Paketleri yükleyin:

```bash
dotnet restore
```

3. `.env.example` dosyasını `.env` olarak kopyalayın ve gerekli değişkenleri ayarlayın:

```bash
cp .env.example .env
# .env dosyasını düzenleyin
```

4. Projeyi derleyin:

```bash
dotnet build
```

5. Veritabanını oluşturun:

```bash
dotnet ef database update
```

6. Projeyi çalıştırın:

```bash
dotnet run
```

## Ortam Değişkenleri

`.env` dosyasında aşağıdaki değişkenleri ayarlamanız gerekmektedir:

- `DB_CONNECTION_STRING`: PostgreSQL bağlantı bilgileri (örn: "Host=localhost;Database=budget_z;Username=your_username;Password=your_password")
- `JWT_KEY`: JWT token için gizli anahtar (HMAC-SHA256 algoritması için minimum 32 karakter olmalı)
- `JWT_ISSUER`: JWT token issuer (örn: "https://localhost:5001")
- `JWT_AUDIENCE`: JWT token audience (örn: "https://localhost:5001")

## API Endpoints

### Kullanıcı İşlemleri

- `POST /api/users/register`: Yeni kullanıcı kaydı
- `POST /api/users/login`: Kullanıcı girişi

### Kategori İşlemleri (Yakında)

- `GET /api/categories`: Tüm kategorileri listele

### Harcama İşlemleri (Yakında)

- `GET /api/expenses`: Tüm harcamaları listele
- `GET /api/expenses/{id}`: Spesifik bir harcamayı getir
- `POST /api/expenses`: Yeni harcama ekle
- `PUT /api/expenses/{id}`: Harcama güncelle
- `DELETE /api/expenses/{id}`: Harcama sil
