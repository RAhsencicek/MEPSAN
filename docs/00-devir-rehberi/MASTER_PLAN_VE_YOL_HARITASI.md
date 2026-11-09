# MEPSAN — Master Plan ve Yol Haritası

## 1. Müşteri İhtiyaç Analizi ve Yönetici Özeti
Görüşmenin ana problemi, yedek parçaların ana depo, servis araçları ve servis işleri arasında güvenilir biçimde izlenememesidir. Yaklaşık 500–600 parçanın satış, değişim, servis tüketimi, tamir ve iade hareketleri farklı noktalarda oluşmakta; mevcut süreçte araçlara yüklenen veya araçlardan dönen ürünlerin bakiyesi düzenli güncellenememektedir. Bu durum stok farkı, yanlış ürün verme riski, kayıp ürün araştırması ve yöneticinin manuel takip yükünü artırmaktadır.

Önerilen çözüm: Mevcut servis ve muhasebe altyapısını koruyarak depo ve araç stoklarını iş emirlerine bağlayan hafif bir operasyon katmanı kurmaktır.

---

## 2. Uygulama Aşamaları

| Aşama | Başlık | Çıktı ve Tamamlanma Koşulu |
|---|---|---|
| **Aşama 1** | Keşif ve Teyit | Servis/stok iş akışı, ürün listesi, lokasyonlar, kullanıcı ve araç sayıları, cihazlar, veri aktarımı ve API'ler netleştirilir. |
| **Aşama 2** | Başlangıç Verisi | Aktif ürün kapsamı sadeleştirilir; barkod/SKU eşlemesi yapılır; depo ve araç bakiyeleri sayılıp açılış bakiyeleri onaylanır. |
| **Aşama 3** | Çekirdek MVP | Web yönetim paneli, Android/mobil saha akışı, stok hareketleri, iş atama, servis notları, rol yönetimi ve temel raporlar hazırlanır. |
| **Aşama 4** | Pilot ve Düzeltme | Seçilen depo/araç/teknisyen grubunda pilot çalışma yürütülür; farklar, tarama hızı, eğitim ve veri eşleşmeleri optimize edilir. |
| **Aşama 5** | Entegrasyon ve Genişleme | Servis sistemi API ve GPS araç takip bağlantıları eklenir. Muhasebe/e-belge entegrasyonu tamamlanır. |

---

## 3. Kabul Ölçütleri

1. **İzlenebilir Stok:** Her stok hareketinde ürün, miktar, kaynak/hedef, kullanıcı, tarih-saat ve hareket türü bulunur.
2. **Araç Bakiyesi:** Araçtaki ürün miktarı depo transferi, satış, servis tüketimi, iade veya onaylı düzeltmeyle anında değişir.
3. **Doğru Ürün Doğrulaması:** Sipariş/iş emrinde istenen ürün dışında barkod okutulursa sistem uyarı verir ve onaysız hareketi engeller.
4. **Servis Geçmişi:** İstasyon/cihaz kaydında önceki arıza, işlem, teknisyen ve kullanılan parçalar tek ekranda incelenebilir.
5. **İade ve Tamir Ayrımı:** Değişimden dönen veya tamire gidecek parça sağlam stoktan ayrı statüde tutulur.
