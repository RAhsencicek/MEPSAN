# MEPSAN — Kalıcı Proje Talimatları ve Sözleşme Disiplini

Bu talimatlar bu depodaki tüm geliştirmeler, yapay zeka ajanları, PR'lar ve kod değişiklikleri için bağlayıcıdır.

## 1. Proje Amacı ve Kapsamı
**MEPSAN Saha Servisi ve Araç Stok Yönetimi Platformu**, akaryakıt istasyonları, pompalar ve otomasyon sistemlerine yönelik saha operasyonlarında yedek parça kayıplarını, stok uyumsuzluklarını ve manuel takip yükünü ortadan kaldırmak üzere tasarlanmış hafif, izlenebilir bir operasyon katmanıdır.

- **Ana Depo & Servis Araçları:** Yaklaşık 500–600 aktif yedek parçanın (elektronik kartlar, pulse üniteleri, nozullar, filtreler, vanalar, hortumlar) ana depo ile servis araçları arasındaki hareketleri anlık izlenir.
- **İş Emirleri & Tüketim:** Saha teknisyenleri iş emrini kapatırken kullanılan, değiştirilen veya iade alınan parçaları barkodla okutarak işler.
- **İade & Hurda Sınıflandırması:** Sökülen veya arızalı parçalar "sağlam", "tamire gidecek", "inceleme bekleyen" veya "hurda" statüleriyle izole edilir.

## 2. Depo Mimarisi (Monorepo Standardı)
```text
├── apps/
│   ├── web/           # Operasyon & Yönetim Paneli (React 19 + Tailwind v4 + Vite) -> Port 5175
│   ├── mobile/        # Saha Personeli Android / Mobil Web (React 19 + Tailwind v4 + Vite) -> Port 5176
│   └── api/           # .NET 10 ASP.NET Core Web API & PostgreSQL EF Core -> Port 5190
│
├── contracts/         # Ortak Veri Sözleşmeleri (JSON Schemas & DTOs)
├── docs/              # Master Plan, Teknik Mimari ve Çözüm Şartnamesi
├── scripts/           # Veritabanı tohumlama ve demo senaryoları
├── compose.yaml       # PostgreSQL & API Docker orkestrasyonu
└── AGENTS.md          # Geliştirici ve ajan sözleşme disiplini talimatları
```

## 3. Sözleşmelere Bağlılık
- `contracts/` altındaki veri sözleşmelerini koru:
  - `stock-movements.v1.json`: Mal kabul, araca yükleme, servis tüketimi, tamir ve sayım farkı.
  - `work-orders.v1.json`: Çağrı merkezi kayıtları, arıza tipleri, teknisyen/araç atamaları.
  - `inventory-catalog.v1.json`: Mepsan aktif yedek parça ve ekipman kataloğu.
  - `vehicles-warehouses.v1.json`: Depolar, araçlar ve bakiye takipleri.
  - `technicians.v1.json`: Teknisyen kimlikleri, roller ve yetki sınırları.

## 4. Geliştirme Kuralları
- **Yalnızca İhtiyaç Duyulan Dosyalar:** Gereksiz paket ve karmaşık mimariler ekleme; mevcut monorepo disiplinine sadık kal.
- **İzlenebilirlik (Audit Trail):** Her stok hareketinde kullanıcı, zaman, kaynak, hedef, iş emri ve gerekçe bulunmalıdır.
- **Türkçe ve Açık İletişim:** Tüm kod açıklamaları, dokümantasyon ve arayüz etiketleri kurumsal Türkçe dil standartlarında olmalıdır.
