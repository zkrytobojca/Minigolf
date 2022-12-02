using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleCourtGenerator : CourtGenerator
{
    public SimpleCourtGenerator(FragmentsPack fragmentsPack, int width, int height) : base(fragmentsPack, width, height)
    {
    }

    public override void PrepareNodes()
    {
        int randomId = Random.Range(0, 4);
        int[] corners = new int[4] { 0, width - 1, numberOfAllNodes - width, numberOfAllNodes - 1 };
        indexToStart = corners[randomId];
        SetNodesDistance(indexToStart);
    }

    public override void CreateMainBranch()
    {
        ClearNodesVisited();
        nodes[indexToStart].nodeType = Node.NodeType.Finish;

        int current = indexToStart;
        int last = current;
        nodes[current].visited = true;
        List<int> adjacentToCurrent = new List<int>();
        for (int i=0; i!= nodes[current].adjacencyList.Count; i++)
        {
            if (nodes[nodes[current].adjacencyList[i]].visited == false && nodes[nodes[current].adjacencyList[i]].distanceFromFinish > nodes[current].distanceFromFinish) adjacentToCurrent.Add(nodes[current].adjacencyList[i]);
        }

        while(adjacentToCurrent.Count > 0)
        {
            last = current; 
            current = adjacentToCurrent[Random.Range(0, adjacentToCurrent.Count)];

            ConnectTwoNodes(current, last);
            adjacentToCurrent = new List<int>();
            for (int i = 0; i != nodes[current].adjacencyList.Count; i++)
            {
                if (nodes[nodes[current].adjacencyList[i]].visited == false && nodes[nodes[current].adjacencyList[i]].distanceFromFinish > nodes[current].distanceFromFinish) adjacentToCurrent.Add(nodes[current].adjacencyList[i]);
            }
        }

        nodes[current].nodeType = Node.NodeType.Start;
    }

    public override void CreateAdditionalBranch()
    {
        ClearNodesVisited();

        //get all empty nodes
        List<Node> emptyNodes = nodes.Where(node => node.nodeType == Node.NodeType.Empty).ToList();
        if (emptyNodes.Count == 0) return;

        //choose one of them
        Node branchOrigin = emptyNodes[Random.Range(0, emptyNodes.Count)];
        int branchOriginId = nodes.FindIndex(node => node.id == branchOrigin.id);

        int currentId = branchOriginId;
        int lastId = currentId;

        bool end = false;
        while(end == false)
        {
            lastId = currentId;
            List<int> adjacentToCurrent = new List<int>();
            for (int i=0; i!=nodes[currentId].adjacencyList.Count; i++)
            {
                if (nodes[nodes[currentId].adjacencyList[i]].visited == false && nodes[nodes[currentId].adjacencyList[i]].distanceFromFinish < nodes[currentId].distanceFromFinish) adjacentToCurrent.Add(nodes[currentId].adjacencyList[i]);
            }
            currentId = adjacentToCurrent[Random.Range(0, adjacentToCurrent.Count)];
            ConnectTwoNodes(lastId, currentId);
            if (nodes[currentId].nodeType != Node.NodeType.Empty) end = true;
        }

        currentId = branchOriginId;
        end = false;
        while (end == false)
        {
            lastId = currentId;
            List<int> adjacentToCurrent = new List<int>();
            for (int i = 0; i != nodes[currentId].adjacencyList.Count; i++)
            {
                if (nodes[nodes[currentId].adjacencyList[i]].visited == false && nodes[nodes[currentId].adjacencyList[i]].distanceFromFinish > nodes[currentId].distanceFromFinish) adjacentToCurrent.Add(nodes[currentId].adjacencyList[i]);
            }
            currentId = adjacentToCurrent[Random.Range(0, adjacentToCurrent.Count)];
            ConnectTwoNodes(lastId, currentId);
            if (nodes[currentId].nodeType != Node.NodeType.Empty) end = true;
        }
    }

    private void SetNodesDistance(int finishId)
    {
        ClearNodesVisited();
        int distance = 0;
        Queue<int> queue = new Queue<int>();

        nodes[finishId].distanceFromFinish = distance;
        nodes[finishId].visited = true;
        queue.Enqueue(finishId);
        
        while(queue.Count > 0)
        {
            int currentId = queue.Dequeue();

            for (int i=0; i!= nodes[currentId].adjacencyList.Count; i++)
            {
                if (nodes[nodes[currentId].adjacencyList[i]].visited == false)
                {
                    nodes[nodes[currentId].adjacencyList[i]].distanceFromFinish = nodes[currentId].distanceFromFinish + 1;
                    nodes[nodes[currentId].adjacencyList[i]].visited = true;
                    queue.Enqueue(nodes[currentId].adjacencyList[i]);
                }
            }
        }
    }

    private void ClearNodesVisited()
    {
        foreach (Node node in nodes)
        {
            node.visited = false;
        }
    }

    private void ShuffleListInt(ref List<int> list)
    {
        int tempId = 0;
        int temp = 0;
        for (int i = 0; i != list.Count; i++)
        {
            tempId = UnityEngine.Random.Range(0, list.Count);
            temp = list[tempId];
            list[tempId] = list[i];
            list[i] = temp;
        }
    }

    private bool ConnectTwoNodes(int middle, int other)
    {
        nodes[middle].visited = true;
        if (nodes[middle].nodeType == Node.NodeType.Empty) nodes[middle].nodeType = Node.NodeType.Middle;

        if (nodes[middle].position.x > nodes[other].position.x)
        {
            nodes[middle].directions.negativeX = true;
            nodes[other].directions.positiveX = true;
        }
        if (nodes[middle].position.x < nodes[other].position.x)
        {
            nodes[middle].directions.positiveX = true;
            nodes[other].directions.negativeX = true;
        }
        if (nodes[middle].position.z > nodes[other].position.z)
        {
            nodes[middle].directions.negativeZ = true;
            nodes[other].directions.positiveZ = true;
        }
        if (nodes[middle].position.z < nodes[other].position.z)
        {
            nodes[middle].directions.positiveZ = true;
            nodes[other].directions.negativeZ = true;
        }
        return true;
    }
}
