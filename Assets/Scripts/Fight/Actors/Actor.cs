using System.Collections.Generic;
using UnityEngine;

namespace Fight
{
    public enum ActorKind { Spd, Def, Atc }
    public struct Matrix {
        public Vector2Int size;//x:列的个数
        public List<List<byte>> matrix;//先列后行: matrix[m][n](第m列第n行)

        public Matrix(int columnCount, byte[] data) {
            if ((data.Length % columnCount != 0) || (columnCount > data.Length))
                Debug.LogError("MoveMatrix Init Error");
            size = new Vector2Int(columnCount, data.Length / columnCount);
            if (size.x % 2 == 0 || size.y % 2 == 0)
                Debug.LogError("MoveMatrix Init Odd Error");

            matrix = new List<List<byte>>();
            for (int i = 0; i < size.x; i++) {
                matrix.Add(new List<byte>());
                for (int j = 0; j < size.y; j++) {
                    matrix[i].Add(data[i + size.x * j]);
                }
            }
        }
        public void LogMatrix() {
            for (int i = 0; i < size.y; i++) {
                string tempStr = "";
                for (int j = 0; j < size.x; j++)
                    tempStr = tempStr + matrix[j][i].ToString() + " ";
                Debug.Log(tempStr);
            }
        }

        public List<Vector2Int> GetPoses(Vector2Int pos,MapManager mm) {
            List<Vector2Int> tempList = new List<Vector2Int>();
            for (int i = 0; i < size.x; i++) {
                for (int j = 0; j < size.y; j++) {
                    Vector2Int tempPos = new Vector2Int {
                        x = pos.x - size.x / 2 + i,
                        y = pos.y + size.y / 2 - j
                    };
                    if (mm.PosCheck(tempPos)&& matrix[i][j] == 1)
                        tempList.Add(tempPos);
                }
            }
            return tempList;
        }
    }

    public class Actor : MonoBehaviour
    {
        public int ID;
        public string actorName;
        public Vector2Int pos;
        public bool isPlayer;

        private SpriteRenderer spriteRenderer;

        public ActorKind proKind;
        public Matrix proMoveMatrix;
        public Matrix proAtcMatrix;

        public float proSpeed;
        public float proSkillSpeed;
        public float proMaxHealth;
        public float proHealth;
        public float proAtcPower;
        public float proDefPower;

        public virtual void SetUp(int id) {
            ID = id;
            Move2(pos);
            gameObject.name = actorName + "(id: " + ID + ")";

            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public virtual void Update() {

        }


        public void Move2(Vector2Int v2) {
            if (FindObjectOfType<MapManager>().GetUnit(FindObjectOfType<MapManager>().Pos2ID(v2)).isCaptured)
                return;
            pos = v2;
            transform.position = FindObjectOfType<MapManager>().GetRenderPos(v2);
        }


        public List<Vector2Int> GetMovePoses() {
            return proMoveMatrix.GetPoses(pos, FindObjectOfType<MapManager>());
        }


        public void SetTurn() {
            
        }

        public void SetTurnEnd() {

        }
    }
}
