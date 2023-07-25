using System.Collections.Generic;

namespace EventBus
{
    public class SubscribersList<TSubscriber> where TSubscriber : class
    {
        public bool Executing;
        public readonly List<TSubscriber> List = new List<TSubscriber>();
        private bool _needsCleanUp = false;
        private List<TSubscriber> _needCleanElements = new List<TSubscriber>();
        public void Add(TSubscriber subscriber)
        {
            List.Add(subscriber);
        }

        public void Remove(TSubscriber subscriber)
        {
            if (Executing)
            {
                var i = List.IndexOf(subscriber);
                if (i >= 0)
                {
                    _needsCleanUp = true;
                    _needCleanElements.Add(subscriber);
                }
            }
            else
            {
                List.Remove(subscriber);
            }
        }
        
        // public void Remove(TSubscriber subscriber)
        //  {
        //      if (Executing)
        //      {
        //          var i = List.IndexOf(subscriber);
        //          if (i >= 0)
        //          {
        //              _needsCleanUp = true;
        //              List[i] = null;
        //          }
        //      }
        //      else
        //      {
        //          List.Remove(subscriber);
        //      }
        //  }

        public void Cleanup()
        {
            if (!_needsCleanUp)
            {
                return;
            }

            foreach (var item in _needCleanElements)
            {
                List.Remove(item);
            }
            _needCleanElements.Clear();
            _needsCleanUp = false;
        }
        // public void Cleanup()
        // {
        //     if (!_needsCleanUp)
        //     {
        //         return;
        //     }
        //
        //     List.RemoveAll(s => s == null);
        //     _needsCleanUp = false;
        // }
    }
}