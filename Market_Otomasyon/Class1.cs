using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_Otomasyon
{
    #region Urun tipimiz
    public class Urun
    {
        //public float AlisFiyati { get; set; }
        //public int Miktari { get; set; }
        //public Kategori Kategori { get; set; }

        private float _alisFiyati;
        private int _miktari;
        private Kategori _kategori;
        private string _urunAdi;
        private int _urunID;
        public Urun() { } //0 parametreli (argümanlı) yapıcı metot (constructor), miras işlemlerinde gerekli. 

        public float AlisFiyati  //Encapsulation(sarmalama=gizleme) işlemleri
        {
            get { return _alisFiyati; }
            set { _alisFiyati = value; }
        }
        public int Miktari  //Encapsulation(sarmalama=gizleme) işlemleri
        {
            get { return _miktari; }
            set { _miktari = value; }
        }
        public Kategori Kategori //Encapsulation(sarmalama=gizleme) işlemleri
        {
            get { return _kategori; }
            set { _kategori = value; }
        }
        public string UrunAdi //Encapsulation(sarmalama=gizleme) işlemleri
        {
            get { return _urunAdi; }
            set { _urunAdi = value; }
        }
        public int UrunID //Encapsulation(sarmalama=gizleme) işlemleri
        {
            get { return _urunID; }
            set { _urunID = value; }
        }


        public Urun(Kategori pKategori, int pMiktari, float pAlisFiyati, string pUrunAdi, int pUrunID)
        {

            //Aşağıda yaptığımız işlemin şu an için bir gereği yok. Ancak GetType() metodu ve typeof() için örnek olması açısından yazıyorum.
            //Biz değerleri girerken zaten farklı bir tipte değer giremeyeceğimizden gerekli değil. 
            if (pKategori.GetType() == typeof(Kategori))  //parametre olarak gelen pKategori'nin tipi Kategori tipinde ise eşitle. 
                Kategori = pKategori;
            else
                Kategori = new Kategori("isimsiz Kategori", 0); //değilse bir kategori oluştur. 

            if (pMiktari.GetType() == typeof(int)) //pMiktari'nin tipi int ise atama yap yoksa 0'a eşitle. 
                Miktari = pMiktari;
            else
                Miktari = 0;

            if (pAlisFiyati.GetType() == typeof(float))  //pAlisMiktari'nin tipi float ise kabul et değilse 0f'e eşitle. 
                AlisFiyati = pAlisFiyati;
            else
                AlisFiyati = 0f;  //0f'den kasıt bu sayının bir float tipte olduğunu belirtir. 0 yazsak da bir sorun olmazdı, kendisi otomatik çevirirdi. 
            //C#'da küçük türler büyük türlere otomatik olarak çevrilir. int<float (tip olarak)

            _urunAdi = pUrunAdi;  //Tip kontrolü gerçekleştirmedim. 
            _urunID = pUrunID;
        }
        public virtual float KdvHesapla()  //KDV hesaplayan metodumuz, polimorfizm için. 
        {

            return _alisFiyati * (18 / 100); //Varsayılan olarak ürünlerin KDV'sini %18 olarak hesaplıyor. 
        }
    }
    #endregion

    #region Urun'lerin kategorisini tutan tip.
    public class Kategori
    {
        public string KategoriAdi { get; set; }
        public int KategoriID { get; set; }

        public Kategori(string pKategoriAdi, int pKategoriID)
        {
            if (pKategoriAdi.GetType() == typeof(string))
                KategoriAdi = pKategoriAdi;
            else
                KategoriAdi = pKategoriAdi.ToString();  //Parametre olarak gelen KategoriAdi string'den farklı ise stringe çevir. 

            KategoriID = pKategoriID; //Bu da yukarıdakinden farklı olarak kontrolsüz olarak atanıyor. Bu iki yöntemden birinin yapılmaması durumunda, kategori id'si sürekli kendi varsayılan(default) değeri atanacak (0). 
            //constructor içinde mutlaka sınıf propertilerine atama yapılmalıdır, yoksa hatalarla karşılaşmak olası. 
        }

    }
    #endregion

    #region Yenebilir Ürünler için tip
    public class YenebilirUrun : Urun
    {
        public YenebilirUrun() { }  //Miras için gerekli. 

        public YenebilirUrun(Kategori pKategori, int pMiktari, float pAlisFiyati, string pUrunAdi, int pUrunID)
        {
            base.AlisFiyati = pAlisFiyati;
            base.Kategori = pKategori;
            base.Miktari = pMiktari;
            base.UrunAdi = pUrunAdi;
            base.UrunID = pUrunID;

        }

        public override float KdvHesapla()
        {
            return base.AlisFiyati/100*8;
        }
    }
    #endregion
    #region Diğer Ürünler için tip
    public class DigerUrun:Urun
    {
        public DigerUrun(){}
        public DigerUrun(Kategori pKategori, int pMiktari, float pAlisFiyati, string pUrunAdi, int pUrunID)
        {
            base.AlisFiyati = pAlisFiyati;
            base.Kategori = pKategori;
            base.Miktari = pMiktari;
            base.UrunAdi = pUrunAdi;
            base.UrunID = pUrunID;
        }
        public override float KdvHesapla()
        {
            return base.AlisFiyati/100*18;
        }
    }
    #endregion

}
