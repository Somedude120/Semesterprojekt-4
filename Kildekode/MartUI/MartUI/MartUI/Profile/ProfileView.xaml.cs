namespace MartUI.Profile
{
    /// <summary>
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView
    {
        public ProfileView()
        {
            InitializeComponent();

            Tokenizer.TokenMatcher = text =>
            {
                if (text.EndsWith(" "))
                {
                    // Remove the ' '
                    return text.Substring(0, text.Length - 1).Trim().ToUpper();
                }

                return null;
            };
        }
    }
}
