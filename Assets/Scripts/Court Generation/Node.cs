using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public enum NodeType
    {
        Empty,
        Start,
        Middle,
        Finish
    }

    public int id;
    public Vector3 position;
    public int positionInCycle;
    public int distanceFromFinish;
    public bool visited;
    public List<int> adjacencyList;
    public Directions directions;
    public NodeType nodeType;

    public Node()
    {
        id = 0;
        position = new Vector3(0, 0, 0);
        visited = false;
        positionInCycle = 0;
        distanceFromFinish = 0;
        adjacencyList = new List<int>();
        directions = new Directions();
        nodeType = NodeType.Empty;
    }
    public Node(int id, Vector3 position)
    {
        this.id = id;
        this.position = position;
        visited = false;
        positionInCycle = 0;
        distanceFromFinish = 0;
        adjacencyList = new List<int>();
        directions = new Directions();
        nodeType = NodeType.Empty;
    }
    public Node(Node node)
    {
        id = node.id;
        position = node.position;
        visited = node.visited;
        positionInCycle = node.positionInCycle;
        distanceFromFinish = node.distanceFromFinish;
        adjacencyList = node.adjacencyList;
        directions = node.directions;
        nodeType = node.nodeType;
    }
}
