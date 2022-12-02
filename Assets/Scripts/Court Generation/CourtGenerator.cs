using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CourtGenerator
{
    protected int width;
    protected int height;

    protected int indexToStart;

    protected FragmentsPack fragmentsPack = null;

    protected List<Node> nodes;
    protected int numberOfAllNodes;

    public CourtGenerator(FragmentsPack fragmentsPack, int width, int height)
    {
        this.fragmentsPack = fragmentsPack;
        this.width = width;
        this.height = height;
        numberOfAllNodes = width * height;
    }

    public void CreateNewNodes()
    {
        nodes = new List<Node>();
        int nodeNumber = 0;
        Node newNode = null;
        for (int i = 0; i != height; i++)
        {
            for (int j = 0; j != width; j++)
            {
                newNode = new Node(nodeNumber, new Vector3(i * fragmentsPack.fragmentSpacing, 0, j * fragmentsPack.fragmentSpacing));
                
                //establishing adjacencies
                if (nodeNumber - width >= 0) newNode.adjacencyList.Add(nodeNumber - width);
                if (nodeNumber % width > 0) newNode.adjacencyList.Add(nodeNumber - 1);
                if (nodeNumber % width + 1 < width) newNode.adjacencyList.Add(nodeNumber + 1);
                if (nodeNumber + width < numberOfAllNodes) newNode.adjacencyList.Add(nodeNumber + width);

                nodes.Add(newNode);

                nodeNumber++;
            }
        }
    }

    public abstract void PrepareNodes();

    public abstract void CreateMainBranch();

    public abstract void CreateAdditionalBranch();

    public List<Node> GetNodes()
    {
        return nodes;
    }
}
