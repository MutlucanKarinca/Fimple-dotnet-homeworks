namespace homework_1;

public class PhoneBook
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    
      public PhoneBook(string name, string lastName, string phoneNumber)
      {
          Name = name;
          LastName = lastName;
          PhoneNumber = phoneNumber;
      }

      public PhoneBook()
      {
          
      }
}