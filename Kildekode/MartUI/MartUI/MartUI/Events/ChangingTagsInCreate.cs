using Prism.Events;

namespace MartUI.Events
{
    public class ChangingTagsInCreate : PubSubEvent<TagControl>
    {

    }

    /// <summary>
    ///  Tag used in Tokenizer to update the tag list 
    /// </summary>
    public class TagControl
    {
        public bool Command { get; }
        public string Tag { get; }

        // When command is equal to false, remove last tag, else add with tag
        public TagControl(bool command, string tag = null)
        {
            Command = command;
            Tag = tag;
        }
    }
}