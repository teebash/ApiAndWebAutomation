using System;

namespace ExecuteAutomation.Customer
{
    public enum Gender
    {
        Male,
        Female
    }

    public class GetGender
    {
        public Gender GetRandomGender()
        {
            var enumValues = Enum.GetValues(typeof(Gender));
            return (Gender)enumValues.GetValue(new Random().Next(enumValues.Length));
        }
    }
}
