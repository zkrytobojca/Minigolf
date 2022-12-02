using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerSkinsPack", menuName = "ScriptableObjects/PlayerSkinsPack")]
public class PlayerSkinsPack : ScriptableObject
{
    public List<GameObject> playerModels;
    public List<GameObject> ballDisplays;
    public List<GameObject> trails;
}
