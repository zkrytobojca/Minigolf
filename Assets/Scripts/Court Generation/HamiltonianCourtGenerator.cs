using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HamiltonianCourtGenerator : CourtGenerator
{
    private readonly int sizeConstraint = 40;
    private int mainBranchLength;

    public HamiltonianCourtGenerator(FragmentsPack fragmentsPack, int width, int height) : base(fragmentsPack, width, height)
    {
        mainBranchLength = UnityEngine.Random.Range(numberOfAllNodes / 2, numberOfAllNodes * 2 / 3);
        if (mainBranchLength < 3) mainBranchLength = 3;
    }

    public override void PrepareNodes()
    {
        if (numberOfAllNodes > sizeConstraint) throw new SizeTooBigForHamiltonianException("Size of nodes list is too big for the algorithm to process it quickly.");

        //find cycle
        indexToStart = UnityEngine.Random.Range(0, numberOfAllNodes - 1);
        bool cycleFound = CreateHamiltonianCycle(indexToStart, numberOfAllNodes - 1);

        if (cycleFound == false) throw new HamiltonianCycleCannotBeFoundException("Current nodes do not contain Hamiltonian cycle.");

        //sort nodes by order in cycle
        nodes.Sort((p, q) => p.positionInCycle.CompareTo(q.positionInCycle));
    }

    public override void CreateAdditionalBranch()
    {
        ClearNodesVisited();

        //get all empty nodes
        List<Node> emptyNodes = nodes.Where(node => node.nodeType == Node.NodeType.Empty).ToList();
        if (emptyNodes.Count == 0) return;

        //choose one of them
        Node branchOrigin = emptyNodes[UnityEngine.Random.Range(0, emptyNodes.Count)];
        int branchOriginId = nodes.FindIndex(node => node.id == branchOrigin.id);

        //go forward and backward in cycle
        int forward = UnityEngine.Random.Range(0, 2);
        for (int i = 0; i != 2; i++)
        {
            int currentNodeId = branchOriginId;
            bool result = false;

            if (forward == 0)
            {
                //go backwards
                if (nodes[(currentNodeId - 1) % numberOfAllNodes].nodeType != Node.NodeType.Empty) result = true;
                else result = false;
                ConnectTwoNodes(currentNodeId, (currentNodeId - 1) % numberOfAllNodes);
                currentNodeId = (currentNodeId - 1) % numberOfAllNodes;
                while (result == false)
                {
                    result = ConnectToFirstAdjacentFragment(currentNodeId);
                    if (result == true) break;
                    ConnectTwoNodes(currentNodeId, (currentNodeId - 1) % numberOfAllNodes);
                    currentNodeId = (currentNodeId - 1) % numberOfAllNodes;
                }
            }
            else
            {
                //go forwards
                if (nodes[(currentNodeId + 1) % numberOfAllNodes].nodeType != Node.NodeType.Empty) result = true;
                else result = false;
                ConnectTwoNodes(currentNodeId, (currentNodeId + 1) % numberOfAllNodes);
                currentNodeId = (currentNodeId + 1) % numberOfAllNodes;
                while (result == false)
                {
                    result = ConnectToFirstAdjacentFragment(currentNodeId);
                    if (result == true) break;
                    ConnectTwoNodes(currentNodeId, (currentNodeId + 1) % numberOfAllNodes);
                    currentNodeId = (currentNodeId + 1) % numberOfAllNodes;
                }
            }

            forward = (forward + 1) % 2;
        }
    }

    public override void CreateMainBranch()
    {
        ClearNodesVisited();
        nodes[0].nodeType = Node.NodeType.Start;

        for (int i = 0; i != mainBranchLength - 1; i++)
        {
            ConnectTwoNodes(i, i + 1);
        }

        nodes[mainBranchLength - 1].visited = true;
        nodes[mainBranchLength - 1].nodeType = Node.NodeType.Finish;
    }

    private bool CreateHamiltonianCycle(int current, int maxDepth, int depth = 0, int start = 0)
    {
        //check node as visited
        nodes[current].visited = true;
        nodes[current].positionInCycle = depth;

        List<int> adjacentToCurrent = nodes[current].adjacencyList;

        if (depth == maxDepth && adjacentToCurrent.Contains(start))
        {
            return true;
        }

        //swap some values in adjacentToCurrent to shuffle it
        ShuffleListInt(ref adjacentToCurrent);

        //go to unvisited nodes
        foreach (int adjacentId in adjacentToCurrent)
        {
            if (nodes[adjacentId].visited == false)
            {
                if (CreateHamiltonianCycle(adjacentId, maxDepth, depth + 1, depth == 0 ? current : start) == true) return true;
            }
        }

        //check node as unvisited while leaving
        nodes[current].visited = false;
        return false;
    }

    private void ClearNodesVisited()
    {
        foreach (Node node in nodes)
        {
            node.visited = false;
        }
    }

    private bool ConnectToFirstAdjacentFragment(int currentNodeId)
    {
        if (nodes[currentNodeId].visited == true) return false;
        foreach (int adjacent in nodes[currentNodeId].adjacencyList)
        {
            int adjacentId = nodes.FindIndex(node => node.id == adjacent);
            if (nodes[adjacentId].nodeType != Node.NodeType.Empty && nodes[adjacentId].visited == false)
            {
                ConnectTwoNodes(currentNodeId, adjacentId);
                return true;
            }
        }
        return false;
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
}

public class SizeTooBigForHamiltonianException : Exception
{
    public SizeTooBigForHamiltonianException(string message) : base(message)
    {
    }
}

public class HamiltonianCycleCannotBeFoundException : Exception
{
    public HamiltonianCycleCannotBeFoundException(string message) : base(message)
    {
    }
}
