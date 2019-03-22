using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Svision
{
    public class EventDeal
    {
        public class TestEventSource
        {
            public class TestEventArgs : EventArgs
            {
                public readonly char KeyToRaiseEvent;

                public TestEventArgs(char keyToRaiseEvent)
                {
                    KeyToRaiseEvent = keyToRaiseEvent;
                }
            }

            public delegate void TestEventHandler(object sender, TestEventArgs e);

            public event TestEventHandler TestEvent;

            protected virtual void OnTestEvent(TestEventArgs e)
            {
                if (TestEvent != null)
                    TestEvent(this, e);
            }

            public void RaiseEvent(char keyToRaiseEvent)
            {
                TestEventArgs e = new TestEventArgs(keyToRaiseEvent);
                OnTestEvent(e);
            }
        }

        public class TestEventListener
        {
            public void KeyPressed(object sender, TestEventSource.TestEventArgs e)
            {
                MessageBox.Show("TEST");
            }

            public void Subscribe(TestEventSource evenSource)
            {
                evenSource.TestEvent += new TestEventSource.TestEventHandler(KeyPressed);
            }

            public void UnSubscribe(TestEventSource evenSource)
            {
                evenSource.TestEvent -= new TestEventSource.TestEventHandler(KeyPressed);
            }
        }

        public static EventDeal tEd;

        public static EventDeal GetInstance()
        {
            if (tEd == null)
            {
                tEd = new EventDeal();
            }
            return tEd;
        }

        public EventDeal()
        {
            TestEventSource es = new TestEventSource();
            TestEventListener el = new TestEventListener();
        }
    }   
}
