using UnityEngine;
namespace Fight
{
    public class RenderMapUnit : MonoBehaviour
    {
        MapUnit mapUnit;//渲染数据
        SpriteRenderer spriteRenderer;//渲染载体


        void Start() {
            mapUnit = GetComponentInParent<MapUnit>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            if (!mapUnit || !spriteRenderer)
                Debug.LogError("Setup MapUnit Render Failed");
        }

        void LateUpdate() {
            RenderColor();
        }

        public void RenderColor() {
            if (mapUnit.insideMove) {
                if (mapUnit.isCaptured)
                    spriteRenderer.color = Color.red;
                else
                    spriteRenderer.color = Color.green;
            }
            else
                spriteRenderer.color = Color.white;

            if (mapUnit.isConsider)
                spriteRenderer.color = Color.yellow;
        }
    }
}