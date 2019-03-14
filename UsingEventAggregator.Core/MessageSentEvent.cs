using System.Collections.ObjectModel;
using Prism.Events;

namespace UsingEventAggregator.Core
{
    public class MessageSentEvent : PubSubEvent<string>
    {
    }

    public class MessageListSentEvent : PubSubEvent<ObservableCollection<string>>
    {

    }
}
