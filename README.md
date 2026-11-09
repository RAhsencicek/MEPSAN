# MEPSAN — Saha Servisi ve Araç Stok Yönetimi Platformu

MEPSAN akaryakıt pompaları, dağıtıcı cihazlar ve otomasyon altyapılarına yönelik saha operasyonlarında yedek parça ve servis süreçlerini uçtan uca dijitalleştiren monorepo platformudur.

---

## 📌 Yönetici Özeti ve Problem Tanımı

Akaryakıt istasyonu servis operasyonlarında ana depo, servis araçları ve teknisyenler arasında yaklaşık 500–600 kritik parçanın (elektronik kartlar, pulse üniteleri, otomatik tabancalar, filtreler, hidrolik üniteler) satışı, değişimi, tamiri ve iadesi gerçekleşmektedir. 

Bu platform:
1. **Araç Stok Kaçağını Önler:** Ana depodan araca transfer barkod ile kayda alınır; araçtaki parça ancak iş emrine bağlandığında stoktan düşer.
2. **Yanlış Parça Kullanımını Engeller:** Barkod doğrulaması ile teknisyenin iş emrine uygun olmayan parça takması anında engellenir.
3. **İade & Tamir Süreçlerini Ayrıştırır:** Değişimden dönen veya arızalı sökülen parçalar sağlam stokla karışmaz; *tamire gidecek*, *inceleme bekleyen* veya *hurda* durumlarında izlenir.
4. **Mevcut Servis & Muhasebe Sistemleriyle Uyumlu:** Dış kayıt numaraları (Dış Referans ID) ile çağrı merkezi ve mevcut ERP/servis sistemini aksatmadan hafif operasyon katmanı olarak çalışır.

---

## 🏗️ Depo Mimarisi

```text
├── apps/
│   ├── web/           # Operasyon & Yönetim Paneli (React 19 + Tailwind CSS + Vite) -> Port 5175
│   ├── mobile/        # Saha Personeli Android / Mobil Web Arayüzü -> Port 5176
│   └── api/           # .NET 10 ASP.NET Core Web API & PostgreSQL EF Core -> Port 5190
│
├── contracts/         # Ortak Veri Sözleşmeleri ve JSON Şemaları
├── docs/              # Master Plan, Çözüm Şartnamesi ve Teknik Mimari
├── scripts/           # Veritabanı ve demo tohumlama (seed) scriptleri
├── compose.yaml       # PostgreSQL & API Docker orkestrasyonu
└── AGENTS.md          # Proje geliştirme ve sözleşme disiplini talimatları
```

---

## 🚀 Hızlı Başlangıç

### 1. Bağımlılıkları Yükleme
```bash
npm install
```

### 2. Operasyon & Yönetim Panelini Başlatma (Port 5175)
```bash
npm run dev
```
Tarayıcıda açın: `http://localhost:5175`

### 3. Saha Personeli Mobil Arayüzünü Başlatma (Port 5176)
```bash
npm run dev:mobile
```
Tarayıcıda veya Android cihazda açın: `http://localhost:5176`

### 4. .NET 10 Web API'yi Başlatma (Port 5190)
```bash
cd apps/api/Mepsan.Api
dotnet run
```
Sağlık kontrolü: `http://localhost:5190/api/health`

### 5. Docker ile PostgreSQL Başlatma
```bash
docker compose up -d db
```

---

## 📋 Temel Veri Modelleri ve Roller

- **Kullanıcı Rolleri:**
  - `Sistem Yöneticisi`: Tüm yetkiler, kullanıcı/rol tanımları, parametreler.
  - `Operasyon Yöneticisi / Çağrı Merkezi`: İş emri oluşturma, teknisyen/araç atama, raporlama.
  - `Depo Sorumlusu`: Mal kabul, araca yükleme/transfer, sayım farkı düzeltme.
  - `Saha Teknisyeni`: Yalnızca kendine atanan işleri ve kendi araç stoğunu görme, barkod okuma, parça tüketimi ve iş kapatma.
