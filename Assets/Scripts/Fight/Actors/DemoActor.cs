using UnityEngine;
namespace Fight
{
    public class DemoActor : Actor
    {
        private void Awake() {
            proMoveMatrix = new Matrix(3,new byte[] {
                0,1,0,
                1,1,1,
                0,1,0,
            });
        }

    }
}
