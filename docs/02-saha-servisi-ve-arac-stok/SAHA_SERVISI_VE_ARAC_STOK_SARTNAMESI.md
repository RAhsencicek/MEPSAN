# MEPSAN — Saha Servisi ve Araç Stok Yönetimi Şartnamesi

## 1. Operasyonel Süreç Akışı

1. **İş Emri Açılışı:** Çağrı merkezine gelen istasyon arızası mevcut servis sisteminde açılır ve Mepsan platformuna dış kayıt referansıyla (ExternalId) aktarılır.
2. **Görev Atama:** Operasyon yöneticisi arızayı uygun teknisyene ve araca atar; ihtiyaç duyulan kritik parçalar iş emrinde rezerve edilir.
3. **Depodan Araca Transfer:** Depo sorumlusu parçaları barkod ile okutur; parçalar ana depodan seçilen aracın seyyar stoğuna transfer edilir.
4. **Sahada Müdahale:** Teknisyen istasyona ulaştığında mobil arayüzden iş emrini "Devam Ediyor" durumuna getirir.
5. **Parça Sarfiyatı ve Barkod Okutma:** Değiştirilen veya takılan parça barkodla teyit edilir ve araç stoğundan düşülür.
6. **Sökülen Parça İadesi:** İstasyon pompasından sökülen eski/hasarlı parça "Tamire Gidecek", "İnceleme Bekliyor" veya "Hurda" durumuyla teknisyen aracının iade sepetine alınır.
7. **İş Kapatma & Fotoğraf:** Yapılan iş açıklaması ve tamamlanan işin fotoğrafı eklenerek iş emri başarıyla kapatılır.
