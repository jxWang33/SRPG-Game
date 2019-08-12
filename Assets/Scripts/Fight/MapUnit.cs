using UnityEngine;

namespace Fight
{
    public class MapUnit : MonoBehaviour
    {
        public int ID;
        public Vector2Int pos;

        private SpriteRenderer spriteRenderer;

        public bool isCaptured = false;
        public bool isConsider = false;
        public bool insideMove = false;


        public void SetUp(int id, int x, int y) {
            Vector2Int tempVec = new Vector2Int(x, y);

            ID = id;
            pos = tempVec;

            gameObject.name = "MapUnit(id: " + ID + ")";
            transform.position = FindObjectOfType<MapManager>().GetRenderPos(ID);

            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }


        public void SetInConsider() {
            isConsider = true;
        }

        public void CancelConsider() {
            isConsider = false;
        }

        public void SwitchInsideMove() {
            insideMove = !insideMove;
        }        
    }
}
