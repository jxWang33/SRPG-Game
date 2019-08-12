using System.Collections.Generic;
using UnityEngine;
namespace Fight
{
    public class InputHandler : EventHolder
    {
        #region KEY_CODE
        const KeyCode CANCEL_KEY = KeyCode.X;
        const KeyCode MOVE_KEY = KeyCode.Z;
        #endregion

        private TurnHandler turnHandler;
        private MapManager mapManager;
        private ActorManager actorManager;

        void Awake() {
            mapManager = FindObjectOfType<MapManager>();
            actorManager = FindObjectOfType<ActorManager>();
            turnHandler = FindObjectOfType<TurnHandler>();
        }

        void Update() {
            HandleKeyboardInput();
            HandleMouseInput();

            EventLoop();
        }

        void HandleKeyboardInput() {

            if (Input.GetKeyDown(CANCEL_KEY)) {
                if (turnHandler.Performer) {
                    actorManager.AddEvent(new Event(EventKind.EndTurn,turnHandler.Performer.ID));
                }
            }

            if (Input.GetKeyDown(MOVE_KEY)) {
                if (turnHandler.Performer) {
                    actorManager.AddEvent(new Event(EventKind.MoveReady,turnHandler.Performer.ID));
                }
            }

        }

        void HandleMouseInput() {

            //Mouse Move
            {
                RaycastHit2D tempHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100,
                    1 << LayerMask.NameToLayer(FightSceneManager.MAPUNIT_LAYER));
                if (tempHit) {
                    mapManager.AddEvent(new Event(EventKind.MouseAbove, tempHit.collider.GetComponent<MapUnit>().ID));
                }
            }

            if (Input.GetMouseButtonDown(0)) {
                RaycastHit2D tempHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100,
                    1 << LayerMask.NameToLayer(FightSceneManager.MAPUNIT_LAYER));
                if (tempHit) {
                    mapManager.AddEvent(new Event(EventKind.MouseSelect, tempHit.collider.GetComponent<MapUnit>().ID));
                }
            }

        }


        protected override void ResponseEvent(Event e) {
            switch (e.kind) {
                default: {
                        Debug.LogWarning("Unknow Event Type");
                        break;
                }
            }
        }
    }
}
