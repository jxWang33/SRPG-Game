using System.Collections.Generic;
using UnityEngine;

namespace Fight
{
    public enum EventKind
    {
        Mouse,          //vector3: mouse position
        MouseAbove,
        MouseSelect,
        MoveLock,       
        MoveReady,      
        EndTurn,        //int: ID
        ActorSetUp,
    };

    public struct Event
    {
        public EventKind kind;
        object eventParam;

        public Event(EventKind ek, object pm) {
            kind = ek;
            eventParam = pm;
        }

        public T GetParam<T>() {
            return (T)eventParam;
        }
    }

    public abstract class EventHolder : MonoBehaviour
    {
        protected List<Event> eventList;

        protected void EventLoop() {
            if (eventList == null) {
                eventList = new List<Event>();
            }
            if (eventList.Count > 0) {
                foreach (Event i in eventList)
                    ResponseEvent(i);
                eventList = new List<Event>();
            }
        }

        protected abstract void ResponseEvent(Event e);

        public void AddEvent(Event e) {
            if (eventList == null) {
                eventList = new List<Event>();
            }
            eventList.Add(e);
        }
    }
}
