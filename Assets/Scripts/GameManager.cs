using UnityEngine;

public class GameManager
{
    public static bool GAME_PAUSED = false;
    public static bool GAME_INITED = false;

    public const KeyCode FUNC_KEY = KeyCode.Space;

    public const string FIGHT_SCENE = "FightScene";
    public const string FIGHT_PREFAB_PATH = "Prefabs/Fight/";

    public static void Init() {
        if (GAME_INITED)
            return;
        QualitySettings.vSyncCount = 0;
        QualitySettings.pixelLightCount = 16;
        Application.targetFrameRate = 60;
        GAME_INITED = true;
    }

    public static void GamePause() {
        GAME_PAUSED = true;
        Time.timeScale = 0;
    }
    public static void GameResume() {
        GAME_PAUSED = false;
        Time.timeScale = 1;
    }


    public static System.Random rd = new System.Random(GetRandomSeed());
    public static void ResetRandom() {
        rd = new System.Random(GetRandomSeed());
    }
    private static int GetRandomSeed() {
        byte[] bytes = new byte[4];
        System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
        rng.GetBytes(bytes);
        return System.BitConverter.ToInt32(bytes, 0);
    }
}
