using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    public static PlayerSkinsPack playerSkins = null;
    public static int skinId = 0;
    public static int trailId = 0;

    private void Awake()
    {
        if (playerSkins == null) playerSkins = Resources.Load<PlayerSkinsPack>("ScriptableObjects/PlayerSkinsPack");

        if (PlayerPrefs.HasKey("skin")) skinId = PlayerPrefs.GetInt("skin");
        if (PlayerPrefs.HasKey("trail")) trailId = PlayerPrefs.GetInt("trail");
    }

    public static void ChangeSkinId(int value)
    {
        skinId = (skinId + value) % playerSkins.playerModels.Count;
        if (skinId < 0) skinId += playerSkins.playerModels.Count;

        PlayerPrefs.SetInt("skin", skinId);
        PlayerPrefs.Save();
    }

    public static void ChangeTrailId(int value)
    {
        trailId = (trailId + value) % playerSkins.trails.Count;
        if (trailId < 0) trailId += playerSkins.trails.Count;

        PlayerPrefs.SetInt("trail", trailId);
        PlayerPrefs.Save();
    }

    public GameObject InstantiateModel(int skinId)
    {
        if (playerSkins == null) playerSkins = Resources.Load<PlayerSkinsPack>("ScriptableObjects/PlayerSkinsPack");
        GameObject model = Instantiate(playerSkins.playerModels[skinId], transform.position, Quaternion.identity, transform);
        Instantiate(playerSkins.trails[trailId], transform.position, Quaternion.identity, model.transform);
        return model;
    }
    public GameObject InstantiateDisplay(int skinId)
    {
        if (playerSkins == null) playerSkins = Resources.Load<PlayerSkinsPack>("ScriptableObjects/PlayerSkinsPack");
        GameObject display = Instantiate(playerSkins.ballDisplays[skinId], transform.position, Quaternion.identity, transform);
        GameObject trail = Instantiate(playerSkins.trails[trailId], transform.position, Quaternion.identity, display.transform);
        return display;
    }
}
