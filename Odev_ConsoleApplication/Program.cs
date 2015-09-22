using Market_Otomasyon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev_ConsoleApplication
{
    class Program
    {
        static YenebilirUrun[] yenebilirUrunlerim = new YenebilirUrun[20];  //static erişim belirleyicisini şu amaçla kullandık: Sınıf üyesine direct erişim için 
        static DigerUrun[] digerUrunlerim = new DigerUrun[20];  //böylelikle static kullanarak bir sınıf üyesine, sınıfın yeni instanse'ı (örneği) alınmadan erişilebilir.
        static Kategori[] kategorilerim = new Kategori[20];  //KategorileriGetir() adlı metotta yapıldığı gibi. 
        static void Main(string[] args)
        {
            //Kategori meyveKategorisi = new Kategori("Mevye", 1);
            //Kategori tekelKategorisi = new Kategori("Sigara", 2);
            //YenebilirUrun yeniUrun = new YenebilirUrun(meyveKategorisi, 15, 15.4f, "Muz", 1); //float olan parametre için sonuna f eklemek zorundayız(onun float olduğunu belirtmiş olduk) . Varsayılan (default) olarak noktalı bir sayı double türündedir ve double floattan daha büyük bir tiptir (double>float). Bu yüzden kendisi otomatik tür dönüşümü yapamaz. 
            //DigerUrun digerYeniUrun = new DigerUrun(tekelKategorisi, 20, 9, "Marlboro", 2);
            //Console.WriteLine(yeniUrun.AlisFiyati + " KDV :" + yeniUrun.KdvHesapla() + " " + yeniUrun.Kategori.KategoriAdi + " " + yeniUrun.UrunID);
            //Console.WriteLine(digerYeniUrun.AlisFiyati + " KDV :" + digerYeniUrun.KdvHesapla());

            do
            {
                Console.WriteLine("Lütfen yapmak istediğiniz işlemi seçiniz :\nYeni Eklemek için(y), Mevcutları Görüntülemek için (g), Çıkış için (q)"); //Ana menü seçenekleri
                var tercih = Console.ReadLine();
                if (tercih == "q") break; //q ya basılmışsa çık. 
                else if (tercih == "y") //y'ye basılmışsa yeni giriş ekranına git. 
                {
                    var icDongu = true;
                    while (icDongu)
                    {
                        Console.WriteLine("Yeni Kategori oluşturmak için (k), Yeni Ürün oluşturmak için (u), bu ekrandan çıkmak için (q)");
                        var girilen = Console.ReadLine();
                        switch (girilen)
                        {
                            case "k": //Yeni kategori oluşturulup kategori dizimize ekleniyor.
                                KategorileriGetir();
                                YeniKategoriEkle();
                                break;
                            case "u": //Yeni ürün oluşturulup ürün dizimize ekleniyor. 

                                break;
                            case "q":
                                icDongu = false;
                                break;
                            default:
                                Console.WriteLine("Lütfen doğru bir tuşa basınız...");
                                break;
                        }

                    }


                }
                else if (tercih == "g") //g'ye basılmışsa oluşturulmuşları göster. 
                {

                }
                else
                {
                    Console.WriteLine("Yanlış bir tuşa bastınız, Lütfen tekrar deneyin");
                    Console.Clear();
                    continue;
                }
            } while (true);


            Console.ReadLine();
        }

        private static void YeniKategoriEkle()
        {
            Console.Write("Lütfen KategoriID giriniz :");
            try
            {
                var girilenKategoriID = Convert.ToInt32(Console.ReadLine());  
                foreach (Kategori kategori in kategorilerim)  //hata oluşmamışsa kategoriID kontrolü yapılıyor.
                {
                    if(kategori!=null) //sıradaki kategori eğer boş ise aşağıdaki hata vereceğinden boş değilse devam etsin istedik. 
                    if (girilenKategoriID == kategori.KategoriID)
                    {
                        Console.WriteLine("Girmeye çalıştığınız ID, {0} kategorisi için daha önce tanımlanmış\nLütfen benzersiz bir ID girin", kategori.KategoriAdi);
                        return; //bu return'ün amacı YeniKategoriEkle() metotundan çıkış yapmak için kullanıldı. Geri dönüş değeri olmayan metotlardan bu şekilde çıkılabilir. 
                    }
                }
                //Buraya kadar herhangi bir sorun yoksa (catch bloğuna girilmediyse veya kategori id farklı ise), buradan sonra yeni kategori ekleme işlemi yapılabilir. 
                int bostakiIlkIndex = -1;
                for (int i = 0; i < kategorilerim.Length; i++)
                {
                    if (kategorilerim[i]==null) //kategorilerim dizisindeki i numaralı index boş ise bostakiIlkIndex degerine i'yi ata. 
                    {
                        bostakiIlkIndex = i;
                        break; //buradan sonra döngünün çalışmasına artık gerek yok. 
                    }
                }
                if (bostakiIlkIndex==-1) //Bu değer hala böyle kalmış ise dizide hiç boşyer kalmamış demektir. 
                {
                    Console.WriteLine("Kategorilerde hiç boş yeriniz kalmamış");
                    return; //bu ise YeniKategoriEkle() metotundan çıkmayı sağlayacak. 
                }
                //Bu satıra gelmeyi başarmışsak bostakiIlkIndex -1'den farklıdır (yani kategorilerde boş index mevcut=boş yer mevcut). 
                Console.Write("Lütfen Kategori Adı giriniz :");
                var girilenKategoriAdi = Console.ReadLine();
                kategorilerim[bostakiIlkIndex] = new Kategori(girilenKategoriAdi, girilenKategoriID);

            }
            catch (FormatException)
            {
                Console.WriteLine("Lütfen bir tam sayı girişi yapın");
            }
            
            
        }

        private static void KategorileriGetir() //static erişim belirleyicisi burada da kullanıldı. 
        {
            
                foreach (Kategori kategori in kategorilerim)
                {
                    if (kategori == null) //sıradaki kategori eğer boş ise bunu atlıyoruz. 
                        continue;
                    //Tüm dizi elemanları boş ise zaten aşağıya inmek mümkün olmayacağından herhangi bir NullException hatası almayacağız. 
                    Console.WriteLine("Kategori ID\tKategori Adı");
                    Console.WriteLine(kategori.KategoriID + "\t\t" + kategori.KategoriAdi);
                }
           
        }
    }
}
