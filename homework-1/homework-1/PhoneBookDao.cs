namespace homework_1;

public class PhoneBookDao
{
    private List<PhoneBook> _phoneBooks;

    public PhoneBookDao()
    {
        _phoneBooks = new List<PhoneBook>
        {
            new PhoneBook { Name = "Dusan", LastName = "Tadic", PhoneNumber = "05519071907" },
            new PhoneBook { Name = "Edin", LastName = "Dzeko", PhoneNumber = "05419071907" },
            new PhoneBook { Name = "Ferdi", LastName = "Kadıoğlu", PhoneNumber = "05319071907" },
            new PhoneBook { Name = "Cengiz", LastName = "Ünder", PhoneNumber = "05019071907" },
            new PhoneBook { Name = "İsmail", LastName = "Yüksek", PhoneNumber = "05559071907" },
        };
    }

    public List<PhoneBook> GetAll()
    {
        return _phoneBooks;
    }

    public void Add(PhoneBook phoneBook)
    {
        _phoneBooks.Add(phoneBook);
    }

    public void Update(PhoneBook phoneBook,String phoneNumber)
    {
        PhoneBook phoneBookToUpdate = _phoneBooks.SingleOrDefault(p => p.PhoneNumber == phoneBook.PhoneNumber);
        if (phoneBookToUpdate != null)
        {
            phoneBookToUpdate.PhoneNumber = phoneNumber;
        }
    }

    public void Delete(PhoneBook phoneBook)
    {
        PhoneBook phoneBookToDelete = _phoneBooks.SingleOrDefault(p => p.PhoneNumber == phoneBook.PhoneNumber);
        if (phoneBookToDelete != null)
        {
            _phoneBooks.Remove(phoneBookToDelete);
        }
    }

    public PhoneBook Contains(String wanted)
    {
        var person = _phoneBooks.FirstOrDefault(k =>
            k.Name.ToLower().Contains(wanted.ToLower()) ||
            k.LastName.ToLower().Contains(wanted.ToLower())||
            k.PhoneNumber.Trim().Contains(wanted.Trim()));

        return person;
    }
    public List<PhoneBook> ContainsList(String wanted)
    {
        var person = _phoneBooks.Where(k =>
            k.Name.ToLower().Contains(wanted.ToLower()) ||
            k.LastName.ToLower().Contains(wanted.ToLower())||
            k.PhoneNumber.Trim().Contains(wanted.Trim()));

        return person.ToList();
    }
}