using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public int x;
    public int y;

    public List<Waypoint> neighbors = new List<Waypoint>();

    public void SetCoordinates(int xC, int yC)
    {
        x = xC;
        y = yC;
    }

    public void AddNeighbor(Waypoint neighbor)
    {
        if (!neighbors.Contains(neighbor))
        {
            neighbors.Add(neighbor);
        }
    }
}