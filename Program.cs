namespace restoran_ödevi2
{   
    class Program
    {
        static List<string> menu = new List<string> { "pizza", "hamburger", "salata" };
        static void Main(string[] args)
        {
            List<Masa> masalar = new List<Masa>();
            for (int i = 0; i < 5; i++)
            {
                masalar.Add(new Masa { MasaNumarası = i + 1, Müşteriler = new List<Müşteri>() });
            }

            bool devam = true;
            while (devam)
            {
                Console.Clear();
                Console.WriteLine("**********ANAMENU*********");
                Console.WriteLine("1 - Sipariş Al");
                Console.WriteLine("2 - Hesap Al");
                Console.WriteLine("3 - Menü Düzenle");
                Console.WriteLine("4 - Çıkış");
                Console.Write("Seçiminizi yapın: ");
                int secim;
                if (int.TryParse(Console.ReadLine(), out secim))
                {
                    switch (secim)
                    {
                        case 1:
                            SiparisAl(masalar);
                            break;
                        case 2:
                            HesapAl(masalar);
                            break;
                        case 3:
                            MenuDuzenle();
                            break;
                        case 4:
                            devam = false;
                            break;
                        default:
                            Console.WriteLine("Geçersiz seçim! Lütfen tekrar deneyin.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Geçersiz giriş! Lütfen bir sayı girin.");
                }
            }
        }

        static void SiparisAl(List<Masa> masalar)
        {
            Masa bosMasa = masalar.Find(m => m.Müşteriler.Count < 4);
            if (bosMasa == null)
            {
                Console.WriteLine("Tüm masalar dolu! Lütfen bekleyin.");
                return;
            }
            Console.Write("Kaç kişisiniz? ");
            int kisiSayisi;
            if (!int.TryParse(Console.ReadLine(), out kisiSayisi) || kisiSayisi < 1)
            {
                Console.WriteLine("Geçersiz sayı! Lütfen tekrar deneyin.");
                return;
            }

            for (int i = 0; i < kisiSayisi; i++)
            {
                Console.WriteLine($"Masa {bosMasa.MasaNumarası}, Müşteri {i + 1}:");
                Müşteri yeniMusteri = new Müşteri();
                do
                {
                    Console.WriteLine("Menü: " + string.Join(", ", menu));
                    Console.Write("Yemek seçimini yapın: ");
                    string yemekSecimi = Console.ReadLine().ToLower();

                    if (menu.Contains(yemekSecimi))
                    {
                        yeniMusteri.Siparişler.Add(yemekSecimi);
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz seçim! Lütfen tekrar deneyin.");
                        continue;
                    }

                    Console.Write("Başka bir arzunuz var mı? (Evet/Hayır): ");
                    string devamMi = Console.ReadLine().ToLower();
                    if (devamMi == "hayır")
                        break;

                } while (true);

                bosMasa.Müşteriler.Add(yeniMusteri);
            }
            Console.WriteLine($"Masa {bosMasa.MasaNumarası} için tüm siparişler alındı.");            
        }

        static void HesapAl(List<Masa> masalar)
        {
            Console.Write("Masa numarasını girin (1-5): ");
            int masaNumarası;
            if (!int.TryParse(Console.ReadLine(), out masaNumarası) || masaNumarası < 1 || masaNumarası > 5)
            {
                Console.WriteLine("Geçersiz masa numarası!");               
                return;
            }

            Masa secilenMasa = masalar[masaNumarası - 1];

            Console.WriteLine($"Masa {secilenMasa.MasaNumarası} için siparişler:");
            foreach (var müşteri in secilenMasa.Müşteriler)
            {
                Console.WriteLine($"Müşteri siparişleri:");
                foreach (var sipariş in müşteri.Siparişler)
                {
                    Console.WriteLine("- " + sipariş);
                    //Thread.Sleep(2000);
                }
            }

            secilenMasa.Müşteriler.Clear();
            Console.WriteLine("Hesap alındı, siparişler temizlendi.");
            Thread.Sleep(5000);

        }

        static void MenuDuzenle()
        {
            Console.WriteLine("Mevcut Menü: " + string.Join(", ", menu));
            Console.Write("Yeni yemek eklemek için yemek adını girin (boş bırakıp Enter'a basınca çıkılır): ");
            string yeniYemek = Console.ReadLine();

            while (!string.IsNullOrEmpty(yeniYemek))
            {
                menu.Add(yeniYemek);
                Console.Write("Yeni yemek eklemek için yemek adını girin (boş bırakıp Enter'a basınca çıkılır): ");
                yeniYemek = Console.ReadLine();
            }
            Console.WriteLine("Menü güncellendi.");
        }
    }
    class Masa
    {
        public int MasaNumarası { get; set; }
        public List<Müşteri> Müşteriler { get; set; }
    }
    class Müşteri
    {
        public List<string> Siparişler { get; set; } = new List<string>();
    }
}
