# MEPSAN — Teknik Mimari ve Altyapı Rehberi

## 1. Mimari Genel Bakış

Sistem, dağıtık saha operasyonlarına uygun, yüksek erişilebilirliğe sahip ve monorepo prensipleriyle yapılandırılmış bir mimari üzerine kuruludur:

- **Operasyon & Yönetim Paneli (`apps/web`):** React 19, Tailwind CSS v4, Lucide Icons ve Vite ile geliştirilmiş hızlı masaüstü/tablet yönetim paneli (Port: 5175).
- **Saha Personeli Mobil Arayüzü (`apps/mobile`):** Android cihazlar ve mobil tarayıcılar için optimize edilmiş, kamera barkod okuyucu destekli teknisyen portalı (Port: 5176).
- **Backend API Katmanı (`apps/api/Mepsan.Api`):** .NET 10 ASP.NET Core Web API, PostgreSQL Entity Framework Core, JWT kimlik doğrulama ve RESTful uç noktalar (Port: 5190).
- **Veritabanı Katmanı:** PostgreSQL 16 veritabanı (Docker orkestrasyonu).

---

## 2. Ağ ve Port Dağılımı

| Servis | Adres / Port | Açıklama |
|---|---|---|
| Web Yönetim Paneli | `http://localhost:5175` | Depo, araç, ürün, iş emri ve kullanıcı yönetimi |
| Mobil Saha Portali | `http://localhost:5176` | Teknisyen iş emirleri ve araç stoğu |
| .NET 10 Web API | `http://localhost:5190` | REST API & Swagger servisleri |
| PostgreSQL DB | `localhost:5432` | İlişkisel veritabanı |

---

## 3. Barkod ve Stok İzleme Stratejisi

- Depo içinde yoğun mal kabul ve sayım için kablosuz barkod okuyucular desteklenir.
- Sahada teknisyenler Android telefonlarının kamerası veya manuel barkod aramasıyla parçaları hızlıca seçip iş emrine bağlayabilir.
- Her hareket için kaynak lokasyon (örneğin: `Ana Depo`) ve hedef lokasyon (örneğin: `34 ME 101 - Servis Aracı 1`) sistem tarafından kaydedilir.
