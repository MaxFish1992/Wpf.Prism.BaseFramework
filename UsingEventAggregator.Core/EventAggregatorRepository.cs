using Prism.Events;

namespace UsingEventAggregator.Core
{
    /// <summary>
    /// 功能描述    ：EventAggregatorRepository  
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2019/2/12 10:21:44 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2019/2/12 10:21:44 
    /// </summary>
    public class EventAggregatorRepository
    {
        public EventAggregatorRepository()
        {
            eventAggregator = new EventAggregator();
        }

        public IEventAggregator eventAggregator;
        public static EventAggregatorRepository eventRepository = null;

        //单例，保持内存唯一实例
        public static EventAggregatorRepository GetInstance()
        {
            if (eventRepository == null)
            {
                eventRepository = new EventAggregatorRepository();
            }
            return eventRepository;
        }

        #region 调用代码
        public void UseCode()
        {
            EventAggregatorRepository
                .GetInstance()
                .eventAggregator
                .GetEvent<MessageSentEvent>().Publish("");

            EventAggregatorRepository
                .GetInstance()
                .eventAggregator
                .GetEvent<MessageSentEvent>()
                .Subscribe(ProgressValueChanged, ThreadOption.UIThread, true);
        }

        public void ProgressValueChanged(string str)
        {

        }
        #endregion

    }
}
