using System.Collections.Generic;
using UnityEngine;

namespace Fight
{
    public class TurnHandler : EventHolder
    {
        public Actor Performer {
            get {
                if (performerID >= 0 && performerID < actorManager.actorList.Count)
                    return actorManager.GetActor(performerID);
                return null;
            }
        }

        [SerializeField]
        private int performerID = -1;//表演者id，-1为空
        private ActorManager actorManager;
        [SerializeField]
        private List<float> actorTurnValues;
        private List<float> actorSkillValues;

        void Awake() {
            actorManager = FindObjectOfType<ActorManager>();
            actorTurnValues = new List<float>();
            actorSkillValues = new List<float>();
            for (int i = 0; i < actorManager.actorList.Count; i++) {
                actorTurnValues.Add(0);
                actorSkillValues.Add(0);
            }
        }
        
        void Update() {
            HandleTurn();

            EventLoop();
        }
               

        void HandleTurn() {
            if (performerID == -1) {
                foreach (Actor i in actorManager.actorList) {
                    actorTurnValues[i.ID] += i.proSpeed * Time.deltaTime;
                    if (actorTurnValues[i.ID] >= 1) {
                        performerID = i.ID;
                    }
                }
                if (performerID != -1) {
                    actorTurnValues[actorManager.GetActor(performerID).ID] = 0;
                    actorManager.GetActor(performerID).SetTurn();
                }
            }
            else {
                if (performerID < 0 || performerID >= actorManager.actorList.Count)
                    Debug.LogWarning("Invalid Actor ID");
            }
        }

        public void SetNewTurn() {
            if (performerID == -1)
                return;
            performerID = -1;
        }


        protected override void ResponseEvent(Event e) {
            throw new System.NotImplementedException();
        }
    }
}
