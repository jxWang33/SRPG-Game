using UnityEngine;
namespace Fight
{
    public class RenderActor : MonoBehaviour
    {
        Actor actor;//渲染数据
        SpriteRenderer spriteRenderer;//渲染载体


        void Start() {
            actor = GetComponentInParent<Actor>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            if (!actor || !spriteRenderer)
                Debug.LogError("Setup Actor Render Failed");
        }

        void LateUpdate() {
            RenderColor();
        }

        public void RenderColor() {
            if (!FindObjectOfType<TurnHandler>().Performer)
                spriteRenderer.color = Color.white;
            else if (FindObjectOfType<TurnHandler>().Performer.ID == actor.ID)
                spriteRenderer.color = Color.red;
            else
                spriteRenderer.color = Color.white;
        }
    }
}