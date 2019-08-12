using System.Collections.Generic;
using UnityEngine;

namespace Fight
{
    public class MapManager : EventHolder
    {
        private List<MapUnit> maps;
        public Vector2 oriPoint;
        public Vector2Int size;
        public Vector3 unitSize;//x,y,y对x影响

        void Awake() {
            maps = new List<MapUnit>();
            InitTestMap();
        }

        private void Update() {
            EventLoop();
        }


        public bool PosCheck(Vector2Int pos) {
            return !(pos.x < 0 || pos.x >= size.x || pos.y < 0 || pos.y >= size.y);
        }

        public int Pos2ID(Vector2Int v2) {
            return Pos2ID(v2.x, v2.y);
        }

        public int Pos2ID(int x, int y) {
            if (x < 0 || x >= size.x || y < 0 || y >= size.y) {
                Debug.LogWarning("Invalid Map Pos");
            }
            return (size.y - y - 1) * size.x + x;
        }

        public Vector2Int ID2Pos(int id) {
            if (id >= maps.Count) {
                Debug.LogWarning("Invalid Map ID");
            }
            return maps[id].pos;
        }

        public Vector2 GetRenderPos(int id) {
            return GetRenderPos(ID2Pos(id));
        }

        public Vector2 GetRenderPos(Vector2Int pos) {
            return oriPoint + new Vector2(pos.x * unitSize.x + pos.y * unitSize.z, pos.y * unitSize.y);
        }

        public MapUnit GetUnit(int id) {
            if (id < 0 || id >= maps.Count)
                Debug.LogWarning("Invalid MapUnit ID");
            if (maps[id].ID == id)
                return maps[id];
            foreach (MapUnit i in maps) {
                if (i.ID == id)
                    return i;
            }
            return null;
        }

        public MapUnit GetUnit(Vector2Int pos) {
            return GetUnit(Pos2ID(pos));
        }

        public void SwitchMoveMatrix(Actor ac) {
            List<Vector2Int> tempList = ac.GetMovePoses();
            foreach (Vector2Int i in tempList) {
                MapUnit tempUnit = GetUnit(Pos2ID(i));
                tempUnit.SwitchInsideMove();
            }
        }

        public void CancelMoveMatrix(Actor ac) {
            List<Vector2Int> tempList = ac.GetMovePoses();
            foreach (Vector2Int i in tempList) {
                MapUnit tempUnit = GetUnit(Pos2ID(i));
                tempUnit.insideMove = false;
            }
        }


        public void InitMapFromFile() {

        }

        public void InitTestMap() {
            GameObject tempObj = Resources.Load(FightSceneManager.PREFAB_PATH + "test") as GameObject;
            oriPoint = new Vector2(-1.5f, 0);
            size = new Vector2Int(9, 3);
            unitSize = new Vector3(.3f, .1f, .1f);
            //横向优先遍历
            int tempID = 0;
            for (int i = size.y - 1; i >= 0; i--) {
                for (int j = 0; j < size.x; j++) {
                    MapUnit tempUnit = Instantiate(tempObj, transform).GetComponent<MapUnit>();
                    if (!tempUnit)
                        Debug.LogWarning("No MapUnit in Instance");
                    maps.Add(tempUnit);
                    tempUnit.SetUp(tempID++, j, i);
                }
            }
        }


        protected override void ResponseEvent(Event e) {
            switch (e.kind) {
                case EventKind.ActorSetUp: {
                        GetUnit(e.GetParam<Vector2Int>()).isCaptured = true;
                        break;
                    }
                case EventKind.EndTurn: {
                        CancelMoveMatrix(e.GetParam<Actor>());
                        break;
                    }
                case EventKind.MouseAbove: {
                        foreach (MapUnit i in maps)
                            i.CancelConsider();
                        GetUnit(e.GetParam<int>()).SetInConsider();
                        break;
                    }
                case EventKind.MouseSelect: {
                        MapUnit tempUnit = GetUnit(e.GetParam<int>());
                        if (FindObjectOfType<TurnHandler>().Performer && tempUnit.insideMove && !tempUnit.isCaptured) {
                            SwitchMoveMatrix(FindObjectOfType<TurnHandler>().Performer);

                            GetUnit(FindObjectOfType<TurnHandler>().Performer.pos).isCaptured = false;
                            FindObjectOfType<TurnHandler>().Performer.Move2(tempUnit.pos);
                            tempUnit.isCaptured = true;

                            FindObjectOfType<ActorManager>().AddEvent(new Event(EventKind.EndTurn, FindObjectOfType<TurnHandler>().Performer.ID));
                        }
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