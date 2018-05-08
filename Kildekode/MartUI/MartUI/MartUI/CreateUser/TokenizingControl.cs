using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using MartUI.Events;

namespace MartUI.CreateUser
{
    public class TokenizingControl : RichTextBox
    {
        // INSPIRATION:
        // https://blog.pixelingene.com/2010/10/tokenizing-control-convert-text-to-tokens/

        // New property called TokenTemplateProperty 
        // with property type DataTemplate and owner type TokenizingControl
        public static readonly DependencyProperty TokenTemplateProperty =
            DependencyProperty.Register("TokenTemplate", typeof(DataTemplate), typeof(TokenizingControl));

        // Declare DataTemplate TokenTemplate with get and setters to the property
        public DataTemplate TokenTemplate
        {
            get => (DataTemplate) GetValue(TokenTemplateProperty);
            set => SetValue(TokenTemplateProperty, value);
        }

        public ItemsControl Items
        {
            get => (ItemsControl)GetValue(TokenTemplateProperty);
            set => SetValue(TokenTemplateProperty, value);
        }

        // Method "TokenMatcher" has input string and returns an object
        public Func<string, object> TokenMatcher { get; set; }


        public TokenizingControl()
        {
            TextChanged += OnTokenTextChanged;
        }

        private void OnTokenTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            var txt = CaretPosition.GetTextInRun(LogicalDirection.Backward);

            if (txt == "")
                GetEventAggregator.Get().GetEvent<ChangingTagsInCreate>().Publish(new TagControl(false));

            // If not null TokenMatcher will return token with input txt
            var token = TokenMatcher?.Invoke(txt);

            if (token != null)
                ReplaceTextWithToken(txt, token);

        }

        private void ReplaceTextWithToken(string txt, object token)
        {
            // Remove handler temporary to not cause TextChanged events
            TextChanged -= OnTokenTextChanged;

            // Get the paragraph
            var para = CaretPosition.Paragraph;


            var matchedRun = para.Inlines.FirstOrDefault(inline =>
            {
                var run = inline as Run;
                return (run != null && run.Text.EndsWith(txt));
            }) as Run;


            if (matchedRun != null) // Found a Run that matched the inputText
            {
                var tokenContainer = CreateTokenContainer(txt, token);
                para.Inlines.InsertBefore(matchedRun, tokenContainer);

                // Remove only if the Text in the Run is the same as txt, else split up
                if (matchedRun.Text == txt)
                {
                    para.Inlines.Remove(matchedRun);
                    GetEventAggregator.Get().GetEvent<ChangingTagsInCreate>().Publish(new TagControl(true, txt));

                }
                else // Split up
                {
                    var index = matchedRun.Text.IndexOf(txt) + txt.Length;
                    var tailEnd = new Run(matchedRun.Text.Substring(index));
                    para.Inlines.InsertAfter(matchedRun, tailEnd);
                    para.Inlines.Remove(matchedRun);

                }
            }
            TextChanged += OnTokenTextChanged;
        }

        private InlineUIContainer CreateTokenContainer(string inputText, object token)
        {
            // Note: we are not using the inputText here, but could be used in future

            var presenter = new ContentPresenter()
            {
                Content = token,
                ContentTemplate = TokenTemplate,
            };

            // BaselineAlignment is needed to align with Run
            return new InlineUIContainer(presenter) {BaselineAlignment = BaselineAlignment.TextBottom};
        }
    }
}
