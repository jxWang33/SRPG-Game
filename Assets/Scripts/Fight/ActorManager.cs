using System.Collections.Generic;
using UnityEngine;
namespace Fight {
    public class ActorManager : EventHolder {
        public List<Actor> actorList;

        void Awake() {
            //InitActors();
            actorList[0].isPlayer = true;

            SetUpActors();
        }

        public void InitActors() {
            //Todo:
            //      根据触发战斗场景生成战斗角色
            //      填充actorList
        }

        public void SetUpActors() {
            int tempID = 0;
            foreach (Actor i in actorList) {
                i.SetUp(tempID++);
                FindObjectOfType<MapManager>().AddEvent(new Event(EventKind.ActorSetUp, i.pos));
            }
        }

        public Actor GetActor(int id) {
            if (id < 0 || id >= actorList.Count)
                Debug.LogWarning("Invalid Actor ID");
            if (actorList[id].ID == id) 
                return actorList[id];
            foreach (Actor i in actorList) {
                if (i.ID == id)
                    return i;
            }
            return null;
        }


        private void Update() {
            EventLoop();
        }

        protected override void ResponseEvent(Event e) {
            switch (e.kind) {
                case EventKind.EndTurn: {
                        GetActor(e.GetParam<int>()).SetTurnEnd();
                        FindObjectOfType<MapManager>().AddEvent(new Event(EventKind.EndTurn, GetActor(e.GetParam<int>())));
                        FindObjectOfType<TurnHandler>().AddEvent(new Event(EventKind.EndTurn, null));
                        break;
                    }
                case EventKind.MoveReady: {
                        FindObjectOfType<MapManager>().SwitchMoveMatrix(GetActor(e.GetParam<int>()));
                        break;
                    }
                default: {
                        Debug.LogWarning("Unknow Event Type");
                        break;
                    }
            }
        }
    }
}
