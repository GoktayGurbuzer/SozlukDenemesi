# Proje Sözlük42

Kullanıcıların bilgiye ve farklı bakış açılarına erişebileceği bir platform oluşturma vizyonuyla yola çıktı. Bu vizyon doğrultusunda, evrenin sırlarını çözmeye çalışan kurgusal bir süper bilgisayar olan "Derin Düşünür"den ilham alındı. Derin Düşünür'ün evrenin anlamını bulmak için sorduğu nihai sorunun cevabı olan 42 sayısı, projemin ismine ilham kaynağı oldu.

Sözlük42, kullanıcıların her konuda özgürce düşüncelerini paylaşabildiği, tartışabildiği ve bilgiye ulaşabildiği bir platform olarak tasarlandı. İsmimizdeki 42 sayısı, hem projenin 4ARC firması tarafından geliştirildiğine gönderme yapıyor hem de evrensel bilgiye ulaşma arayışını simgeliyor.

Sözlük42'de kullanıcılar, tıpkı Ekşi Sözlük'te olduğu gibi, çeşitli başlıklar altında entry'ler girebilir, yorum yapabilir ve diğer kullanıcılarla etkileşime geçebilirler.

Sözlük42, sadece bir sözlük platformu değil, aynı zamanda kullanıcıların bilgiye ulaşma, düşüncelerini paylaşma ve tartışma ihtiyaçlarını karşılayan bir topluluk platformudur.

## I. Kullanıcı Yönetimi:

### Kayıt ve Giriş:
Kullanıcılar e-posta adresleri veya sosyal medya hesapları (Google, Facebook, Twitter vb.) ile kolayca kayıt olabilirler.
Güvenli şifreleme yöntemleri kullanılarak kullanıcı bilgileri korunur.
Kullanıcılar, "Beni Hatırla" seçeneği ile oturumlarını açık tutabilirler.
- Profil Yönetimi:
Kullanıcılar profillerinde kişisel bilgilerini (avatar, kullanıcı adı, biyografi) düzenleyebilirler.
Takip ettikleri ve kendilerini takip eden kullanıcıları görebilirler.
Girdilerini ve yorumlarını listeleyebilirler.
Şifrelerini sıfırlayabilirler.
- Gizlilik Ayarları:
Kullanıcılar, profillerinin ve girdilerinin gizlilik düzeyini ayarlayabilirler (herkese açık, sadece takipçilere açık, özel).

### II. Başlıklar ve Entry'ler (Girdiler):

- Başlık Oluşturma:
Kullanıcılar yeni başlıklar oluşturabilir ve başlıklara uygun etiketler ekleyebilirler.
Başlıklar, belirli kategorilere (teknoloji, sanat, spor vb.) ayrılabilir.
- Entry (Girdi) Oluşturma:
Kullanıcılar başlıklar altında entry'ler girebilirler.
Entry'ler metin, resim, video veya link içerebilir.
Kullanıcılar entry'lerine etiketler ekleyebilirler.
- Entry Düzenleme ve Silme:
Kullanıcılar kendi entry'lerini düzenleyebilir veya silebilirler.
Yorumlar:
Kullanıcılar entry'lere yorum yapabilirler.
Yorumlar başlık altında kronolojik olarak sıralanır.
- Beğeni ve Beğenmeme:
Kullanıcılar entry'leri beğenebilir veya beğenmeyebilirler.
Beğeni sayıları entry'lerin popülerliğini belirlemede kullanılır.
- Favorilere Ekleme:
Kullanıcılar entry'leri favorilerine ekleyebilir ve daha sonra kolayca erişebilirler.

### III. Arama:

- Genel Arama:
Kullanıcılar başlık, entry veya kullanıcı adı ile arama yapabilirler.
Arama sonuçları alaka düzeyine göre sıralanır.
Etiket Bazlı Arama:
Kullanıcılar belirli etiketlere göre arama yapabilirler.
IV. Sıralama ve Filtreleme:

- Entry Sıralama:
Kullanıcılar entry'leri popülerliğe (beğeni sayısına), zamana (en yeni veya en eski) veya rastgele sıralayabilirler.
Etiket Filtreleme:
Kullanıcılar belirli etiketlere göre entry'leri filtreleyebilirler.

### V. Bildirimler:

- Anlık Bildirimler:
Kullanıcılar, takip ettikleri kullanıcıların yeni entry'leri, yorumlara gelen yanıtlar, beğeni ve favorilere ekleme bildirimleri gibi etkinlikler hakkında anlık bildirimler alırlar.
- E-posta Bildirimleri:
Kullanıcılar, isteğe bağlı olarak bildirimleri e-posta yoluyla da alabilirler.
Bu temel özellikler, Sözlük42'nin kullanıcıların etkileşimde bulunabileceği, bilgi paylaşabileceği ve topluluk oluşturabileceği bir platform olmasını sağlayacaktır.

Sözlük42 projesini hayata geçirmek için kullanılabilecek teknolojiler ve çatılar:

**Backend (Arka Uç):**

*   **Programlama Dili:** C# (.NET 8)
*   **Web Çerçevesi:** ASP.NET Core 8 (MVC)
    *   **MVC (Model-View-Controller):** Büyük ve karmaşık projeler için daha uygun, daha iyi organize edilebilir bir yapı sunar.
*   **ORM (Object-Relational Mapping):** Entity Framework Core
    *   Veritabanı işlemlerini kolaylaştırır ve nesne yönelimli programlama ile veritabanı arasındaki uyumu sağlar.
*   **Veritabanı:** PostgreSQL
    *   Proje ihtiyaçlarına ve ölçeklenebilirlik gereksinimlerine göre seçim yapılabilir.
*   **Kimlik Doğrulama ve Yetkilendirme:** ASP.NET Core Identity
    *   Kullanıcı yönetimi, kayıt, giriş, yetkilendirme gibi işlemleri kolaylaştırır.
*   **API Geliştirme:** RESTful API'ler
    *   Farklı platformlardan (web, mobil) uygulamaya erişimi sağlar.
*   **Caching:** Redis
    *   Veri önbellekleme ile performansı artırır.

**Frontend (Ön Uç):**

*   **HTML, CSS, JavaScript:** Temel web teknolojileri
*   **JavaScript Çerçevesi:** React
    *   Modern ve etkileşimli kullanıcı arayüzleri oluşturmayı kolaylaştırır.
*   **CSS Framework:** Tailwind CSS
    *   Hızlı ve duyarlı tasarım yapmayı sağlar.
*   **State Management:** Redux
    *   Uygulama durumunu yönetmeyi kolaylaştırır.

**Ek Teknolojiler:**

*   **Real-time Communication:** SignalR
    *   Anlık bildirimler ve gerçek zamanlı etkileşimler için kullanılabilir.
*   **Search Engine:** Elasticsearch
    *   Hızlı ve etkili arama deneyimi sağlar.

Bu teknolojiler ve çatılar, Sözlük42 projesini geliştirmek için güçlü bir temel oluşturacaktır. Ancak, projenin özel gereksinimlerine göre bu listede değişiklikler yapılabilir veya eklemeler yapılabilir.
