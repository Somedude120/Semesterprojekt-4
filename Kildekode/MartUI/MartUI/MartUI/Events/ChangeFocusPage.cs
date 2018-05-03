using MartUI.Main;
using Prism.Events;

namespace MartUI.Events
{
    public class ChangeFocusPage : PubSubEvent<IViewModel>
    {

    }

    public static class GetEventAggregator
    {
        public static IEventAggregator eventaggregator = new EventAggregator();

        public static IEventAggregator Get()
        {
            return eventaggregator;
        }
    }
}