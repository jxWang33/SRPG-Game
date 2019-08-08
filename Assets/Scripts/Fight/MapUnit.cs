using UnityEngine;

namespace Fight
{
    public class MapUnit : MonoBehaviour
    {
        public int ID { get; set; }
        public Vector2Int pos{ get; set; }
        
        public void SetUp(int id,int x,int y) {
            Vector2Int tempVec = new Vector2Int(x, y);

            ID = id;
            pos = tempVec;

            gameObject.name = "MapUnit(id: " + ID + ")";
            transform.position =FindObjectOfType<MapManager>().oriPoint + new Vector2(pos.x * .3f + pos.y * .1f, pos.y * .1f);
        }
        
        void Update() {

        }
    }
}
