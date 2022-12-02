using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourtController : MonoBehaviour
{

    public MapSettings.MapSize mapSize;
    public bool autoChangeSize = true;
    [SerializeField] private FragmentsPack fragmentsPack = null;

    struct MinMax
    {
        public int minSize;
        public int maxSize;
        public int minAdditionalBranches;
        public int maxAdditionalBranches;

        public MinMax(int minSize,  int maxSize, int minAdditionalBranches, int maxAdditionalBranches)
        {
            this.minSize = minSize;
            this.maxSize = maxSize;
            this.minAdditionalBranches = minAdditionalBranches;
            this.maxAdditionalBranches = maxAdditionalBranches;
        }
    }

    private static readonly MinMax SmallMapMinMax = new MinMax(2, 4, 0, 1);
    private static readonly MinMax MediumMapMinMax = new MinMax(3, 5, 0, 3);
    private static readonly MinMax LargeMapMinMax = new MinMax(4, 7, 1, 5);

    private CourtGenerator courtGenerator = null;

    private void Awake()
    {
        if (autoChangeSize) mapSize = MapSettings.mapSize;
        int width, height, additionalBranches;
        switch(mapSize)
        {
            case MapSettings.MapSize.Small:
                width = Random.Range(SmallMapMinMax.minSize, SmallMapMinMax.maxSize);
                height = Random.Range(SmallMapMinMax.minSize, SmallMapMinMax.maxSize - width + SmallMapMinMax.minSize);
                additionalBranches = Random.Range(SmallMapMinMax.minAdditionalBranches, SmallMapMinMax.maxAdditionalBranches);
                break;
            case MapSettings.MapSize.Medium:
                width = Random.Range(MediumMapMinMax.minSize, MediumMapMinMax.maxSize);
                height = Random.Range(MediumMapMinMax.minSize, MediumMapMinMax.maxSize - width + MediumMapMinMax.minSize);
                additionalBranches = Random.Range(MediumMapMinMax.minAdditionalBranches, MediumMapMinMax.maxAdditionalBranches);
                break;
            case MapSettings.MapSize.Large:
                width = Random.Range(LargeMapMinMax.minSize, LargeMapMinMax.maxSize);
                height = Random.Range(LargeMapMinMax.minSize, LargeMapMinMax.maxSize - width + LargeMapMinMax.minSize);
                additionalBranches = Random.Range(LargeMapMinMax.minAdditionalBranches, LargeMapMinMax.maxAdditionalBranches);
                break;
            default:
                width = Random.Range(SmallMapMinMax.minSize, SmallMapMinMax.maxSize);
                height = Random.Range(SmallMapMinMax.minSize, SmallMapMinMax.maxSize - width + SmallMapMinMax.minSize);
                additionalBranches = Random.Range(SmallMapMinMax.minAdditionalBranches, SmallMapMinMax.maxAdditionalBranches);
                break;
        }

        courtGenerator = new HamiltonianCourtGenerator(fragmentsPack, width, height);

        courtGenerator.CreateNewNodes();
        try
        {
            courtGenerator.PrepareNodes();
        }
        catch
        {
            courtGenerator = new SimpleCourtGenerator(fragmentsPack, width, height);
            courtGenerator.CreateNewNodes();
            courtGenerator.PrepareNodes();
        }
        courtGenerator.CreateMainBranch();
        for(int i = 0; i!= additionalBranches; i++)
        {
            courtGenerator.CreateAdditionalBranch();
        }
        InstantiateModels(courtGenerator.GetNodes());
    }

    public void InstantiateModels(List<Node> nodes)
    {
        GameObject court = new GameObject("Court");
        foreach (Node node in nodes)
        {
            if (node.nodeType != Node.NodeType.Empty)
            {
                List<GameObject> fragmentsToInstantiate = null;
                GameObject chosenFragment = null;
                Quaternion fragmentQuaternion;

                switch (node.nodeType)
                {
                    case Node.NodeType.Start:
                        fragmentsToInstantiate = fragmentsPack.startFragments.GetFragmentsBasedOnDirections(node.directions);
                        chosenFragment = fragmentsToInstantiate[Random.Range(0, fragmentsToInstantiate.Count)];
                        fragmentQuaternion = fragmentsPack.startFragments.GetQuaternionBasedOnDirections(node.directions);
                        Instantiate(chosenFragment, node.position, fragmentQuaternion, court.transform);
                        break;
                    case Node.NodeType.Middle:
                        fragmentsToInstantiate = fragmentsPack.middleFragments.GetFragmentsBasedOnDirections(node.directions);
                        chosenFragment = fragmentsToInstantiate[Random.Range(0, fragmentsToInstantiate.Count)];
                        fragmentQuaternion = fragmentsPack.middleFragments.GetQuaternionBasedOnDirections(node.directions);
                        Instantiate(chosenFragment, node.position, fragmentQuaternion, court.transform);
                        break;
                    case Node.NodeType.Finish:
                        fragmentsToInstantiate = fragmentsPack.finishFragments.GetFragmentsBasedOnDirections(node.directions);
                        chosenFragment = fragmentsToInstantiate[Random.Range(0, fragmentsToInstantiate.Count)];
                        fragmentQuaternion = fragmentsPack.finishFragments.GetQuaternionBasedOnDirections(node.directions);
                        Instantiate(chosenFragment, node.position, fragmentQuaternion, court.transform);
                        break;
                }
            }
        }
    }
}
