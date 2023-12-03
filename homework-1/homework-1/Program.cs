using homework_1;
PhoneBookService phoneBookService = new PhoneBookService();

do
{

    Console.WriteLine("Lütfen yapmak istediğiniz işlemi seçiniz :)");
    Console.WriteLine("*******************************************");
    Console.WriteLine("(1) Yeni Numara Kaydetmek (2) Varolan Numarayı Silmek (3) Varolan Numarayı Güncelleme");
    Console.WriteLine("(4) Rehberi Listelemek (5) Rehberde Arama Yapmak");

    int secim = Convert.ToInt32(Console.ReadLine());

    switch (secim)
    {
        case 1:
            phoneBookService.Create();
            break;
        case 2:
            phoneBookService.Delete();
            break;
        case 3:
            phoneBookService.Update();
            break;
        case 4:
            Console.WriteLine("Rehberi A-Z sıralamak için (1), Z-A sıralamak için (2) tuşlayınız:");
            int choice = Convert.ToInt32(Console.ReadLine());
            phoneBookService.GetAll(choice == 1);
            break;
        case 5:
            phoneBookService.Search();
            break;
        default:
            Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyiniz.");
            break;
    }

    Console.WriteLine("Başka bir işlem yapmak ister misiniz? (Evet için 'e' / Hayır için 'h')");
} while (Console.ReadLine().ToLower() == "e");