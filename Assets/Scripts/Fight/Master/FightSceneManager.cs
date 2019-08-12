using UnityEngine;

public class FightSceneManager : MonoBehaviour
{
    public const string PREFAB_PATH = "Prefabs/Fight/";

    public const string MAPUNIT_LAYER = "MapUnit";


    void Awake()
    {
        GameManager.Init();
    }

    void Update()
    {
        
    }
}
