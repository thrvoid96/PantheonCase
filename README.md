# PantheonCase
Ufak notlar:

-Production menüden herhangi bir objeye sol tıkladığınızda info menü açılıyor

-İnfo menü açık olduğu sürece başka objelerle interaksiyona geçemezsiniz (Kapamak için infoUI üstündeki cancel butonunu kullanın)
-İnfo menü üstünde her zaman ilk aksiyon seçili olarak geliyor.(Sağ grid üstünde sağ tıklayarak aksiyonu gerçekleştiriyorsunuz.)
-Spawn noktalarının üstüne bina atarak spawnı bloklayaibliyorsunuz
(Bayrağın üstüne bina yapılmayacak şekilde olunca askerler sıkışıp kalıyordu, o yüzden sadece asker spawnlanmasını engelliyorum. 
Yeni bir spawn noktası seçip oradan devam edebilirsiniz.)
-Draw call 20-25 arası ama 6 call da skybox'tan alıyor.(Sprite atlas/lighting yerine başka optimizasyon yapmadım)
-Pub and sub yerine eventlere add/remove listener yaptım
-Aspect ratio Info ui üstünde düzgün çalışıyor (çünkü kendi gridlayout kodu var) ama Production UI (infinite scroll) üstünde bazı
sıkıntılar yaşadım. O yüzden çok kötü bir çözümü var(AspectRatioFixer)

Bunlar dışında şimdilik aklıma gelen bir şey yok. Eğer başka sıkıntılar fark ederseniz belirtirsiniz.
