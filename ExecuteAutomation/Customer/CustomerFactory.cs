using System.Collections.Generic;
using TestStack.Dossier;

namespace ExecuteAutomation.Customer
{
    public class CustomerFactory
    {
        public ICustomer CreateCustomer(Gender gender)
        {
            ICustomer customer = new Customer();
            var value = new AnonymousValueFixture();

            switch (gender)
            {
                case Gender.Male:
                    customer.Title = GetRandomMaleTitle();
                    customer.FirstName = value.Person.NameFirstMale();
                    customer.Gender = "Male";
                    break;
                case Gender.Female:
                    customer.Title = GetRandomFemaleTitle();
                    customer.FirstName = value.Person.NameFirstFemale();
                    customer.Gender = "Female";
                    break;
            }

            customer.Initial = value.Person.NameSuffix();
            customer.MiddleName = value.Person.Username();
            customer.LastName = value.Person.NameLast();
            customer.Email = value.Person.EmailAddress();
            customer.UserName = customer.Initial + "_" + value.Person.Username();
            customer.Password = value.Person.Password();
            customer.Country = GetRandomCountry();

            return customer;
        }

        private static string GetRandomFemaleTitle()
        {
            List<string> femaleTitles = new List<string> { "Ms."/*, "Mrs", "Miss" */};

            return femaleTitles.Random();
        }

        private static string GetRandomMaleTitle()
        {
            List<string> maleTitles = new List<string> { "Mr."/*, "Dr", "Prof" */};

            return maleTitles.Random();
        }

        private static string GetRandomCountry()
        {
            List<string> countryNames = new List<string>{"UK", "India", "USA", "Singapore"};

            return countryNames.Random();
        }
    }
}
