using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFragmentsPack", menuName = "ScriptableObjects/FragmentsPack")]
public class FragmentsPack : ScriptableObject
{
    public float fragmentSpacing = 3f;
    public FragmentsList startFragments;
    public FragmentsList middleFragments;
    public FragmentsList finishFragments;
}
