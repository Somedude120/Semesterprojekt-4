namespace MartUI.Login
{
    public class PersonModel
    {
        public PersonModel()
        {

        }

        public PersonModel(string name, string pass)
        {
            Username = name;
            Password = pass;
        }
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
