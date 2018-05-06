using Prism.Events;

namespace MartUI.Events
{
    public static class GetEventAggregator
    {
        private static readonly IEventAggregator _eventaggregator = new EventAggregator();

        public static IEventAggregator Get()
        {
            return _eventaggregator;
        }
    }
}