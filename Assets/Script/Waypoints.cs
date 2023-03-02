using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public GameObject waypointPrefab;
    public GameObject map;

    public void GenerateWaypoint(int x, int y)
    {
        //get pos of bottom left of given tile
        Vector3 position = new Vector3(x + 0.5f, y - 1.5f, 0f);
        //spawn waypoint object
        Waypoint wp = Instantiate(waypointPrefab, position, Quaternion.identity, transform).GetComponent<Waypoint>();
        wp.SetCoordinates(x, y);
        
        //connect adjacent nodes as neighbors if we're in tile mode
        if(map.GetComponent<LoadMap>().tileMode){
            //check for neighbors
            Waypoint neighbor = FindWaypoint(x-2, y);
            if (neighbor != null)
            {
                //add neighbor and add this to the neighbor
                neighbor.AddNeighbor(wp);
                wp.AddNeighbor(neighbor);
            }
            neighbor = FindWaypoint(x, y+2);
            if (neighbor != null)
            {
                //add neighbor and add this to the neighbor
                neighbor.AddNeighbor(wp);
                wp.AddNeighbor(neighbor);
            }
        }
    }

    // Find a waypoint with given x and y coordinates
    public Waypoint FindWaypoint(int x, int y)
    {
        Transform waypointsTransform = transform;
        for (int i = 0; i < waypointsTransform.childCount; i++)
        {
            Waypoint wp = waypointsTransform.GetChild(i).GetComponent<Waypoint>();
            if (wp != null && wp.x == x && wp.y == y)
            {
                return wp;
            }
        }
        return null;
    }
}
