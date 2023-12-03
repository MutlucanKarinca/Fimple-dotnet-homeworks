namespace homework_1;

public class PhoneBookService
{
    PhoneBookDao phoneBookDao=new();

    public void Create()
    {
        Console.WriteLine("Lütfen isim giriniz: ");
        string name = Console.ReadLine();

        Console.WriteLine("Lütfen soyisim giriniz: ");
        string lastName = Console.ReadLine();

        Console.WriteLine("Lütfen telefon numarası giriniz: ");
        string phoneNumber = Console.ReadLine();

        phoneBookDao.Add(new PhoneBook(name, lastName, phoneNumber));
        Console.WriteLine("Numara başarıyla kaydedildi.");
    }

    public void Delete()
    {
        Console.WriteLine("Lütfen numarasını silmek istediğiniz kişinin adını ya da soyadını giriniz: ");
        string wanted = Console.ReadLine();
        PhoneBook phoneBook = phoneBookDao.Contains(wanted);
        if (phoneBook != null)
        {
            Console.Write($"{phoneBook.Name} isimli kişi rehberden silinmek üzere, onaylıyor musunuz? (y/n): ");
            string confirmation = Console.ReadLine().ToLower();
            if (confirmation == "y")
            {
                phoneBookDao.Delete(phoneBook);
                Console.WriteLine("Kişi başarıyla silindi.");
            }
            else
            {
                Console.WriteLine("Silme işlemi iptal edildi.");
            }
        }
        else
        {
            Console.WriteLine("Aradığınız kriterlere uygun veri rehberde bulunamadı. Lütfen bir seçim yapınız.");
            Console.WriteLine("(1) Silmeyi sonlandırmak için   (2) Yeniden denemek için");

            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 2)
            {
                Delete();
            }
        }
    }

    public void Update()
    {
        Console.WriteLine("Lütfen numarasını güncellemek istediğiniz kişinin adını ya da soyadını giriniz: ");
        string wanted = Console.ReadLine();

        PhoneBook phoneBook = phoneBookDao.Contains(wanted);

        if (phoneBook == null)
        {
            Console.WriteLine("Aradığınız kriterlere uygun veri rehberde bulunamadı. Lütfen bir seçim yapınız.");
            Console.WriteLine("(1) Silmeyi sonlandırmak için   (2) Yeniden denemek için");

            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 2)
            {
                Update();
            }
        }
        else
        {
            Console.WriteLine($"Lütfen yeni telefon numarasını giriniz (Mevcut numara: {phoneBook.PhoneNumber}): ");
            string newPhoneNumber = Console.ReadLine();
            phoneBookDao.Update(phoneBook, newPhoneNumber);
            Console.WriteLine("Numara başarıyla güncellendi.");
        }
    }

    public void GetAll(bool aToZ)
    {
        List<PhoneBook> phoneBooks = phoneBookDao.GetAll();

        phoneBooks.OrderBy(k => k.Name);

        var sortingPhonebooks = aToZ ?  phoneBooks.OrderBy(k => k.Name) : phoneBooks.OrderByDescending(k => k.Name);

        Console.WriteLine("Telefon Rehberi");
        Console.WriteLine("**********************************************");

        foreach (var person in sortingPhonebooks)
        {
            Console.WriteLine($"isim: {person.Name} Soyisim: {person.LastName} Telefon Numarası: {person.PhoneNumber}");
        }

        Console.WriteLine("**********************************************");
    }
    
    public void Search()
    {
        Console.WriteLine("Arama yapmak istediğiniz tipi seçiniz.");
        Console.WriteLine("**********************************************");
        Console.WriteLine("İsim veya soyisime göre arama yapmak için: (1)");
        Console.WriteLine("Telefon numarasına göre arama yapmak için: (2)");

        int choice = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Arama yapmak istediğiniz kelimeyi giriniz: ");
        string wanted = Console.ReadLine().ToLower();

        Console.WriteLine("Arama Sonuçlarınız:");
        Console.WriteLine("**********************************************");

        if (choice == 1 || choice==2)
        {
            List<PhoneBook> phoneBookList = phoneBookDao.ContainsList(wanted);

            foreach (var person in phoneBookList)
            {
                Console.WriteLine($"isim: {person.Name} Soyisim: {person.LastName} Telefon Numarası: {person.PhoneNumber}");
            }

        }
        Console.WriteLine("**********************************************");
    }
}