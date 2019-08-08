using System.Collections.Generic;
using UnityEngine;

namespace Fight
{
    public class MapManager : MonoBehaviour
    {
        private List<MapUnit> maps;
        public Vector2 oriPoint;
        public Vector2Int size { get; set; }
        void Awake() {
            maps = new List<MapUnit>();
            InitTestMap();
        }

        void Update() {

        }
        
        public void InitMapFromFile() {

        }
        public void InitTestMap() {
            GameObject tempObj = Resources.Load(GameManager.FIGHT_PREFAB_PATH + "test") as GameObject;
            oriPoint = new Vector2(-1.5f, -1);
            size = new Vector2Int(9, 3);
            //横向优先遍历
            int tempID = 0;
            for (int i=0;i<size.y;i++) {
                for (int j = 0; j < size.x; j++) {
                    MapUnit tempUnit = Instantiate(tempObj, transform).GetComponent<MapUnit>();
                    if (!tempUnit)
                        Debug.LogWarning("No MapUnit in Instance");
                    maps.Add(tempUnit);
                    tempUnit.SetUp(tempID++, j, i);
                }
            }
        }
    }
}
