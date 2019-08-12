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

        void Update() {
            StateReset();
        }        


        private void StateReset() {

            if (insideMove) {
                if (isCaptured)
                    spriteRenderer.color = Color.red;
                else
                    spriteRenderer.color = Color.green;
            }
            else
                spriteRenderer.color = Color.white;

            if(isConsider)
                spriteRenderer.color = Color.yellow;
            isConsider = false;

        }

        public void SetInConsider() {
            isConsider = true;
        }

        public void SetMoveMatrix() {
            if (!insideMove) {
                insideMove = true;
                if (isCaptured)
                    spriteRenderer.color = Color.red;
                else
                    spriteRenderer.color = Color.green;
            }
            else {
                insideMove = false;
                spriteRenderer.color = Color.white;
            }
        }        
    }
}
