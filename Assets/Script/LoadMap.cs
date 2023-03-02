using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class LoadMap : MonoBehaviour
{
    //map files
    private string mapFile1 = "Assets/Map/hrt201n.map";
    private string mapFile2 = "Assets/Map/AR0011SR.map";
    public bool showMap1 = true;
    public bool tileMode = true;

    //map info
    public char[,] map;
    public int height;
    public int width;
    public string mapType;

    //tile prefabs
    public GameObject passableTerrainPrefab;
    public GameObject outOfBoundsPrefab;
    public GameObject unpassableTerrainPrefab;

    //waypoint spawner
    public GameObject Waypoints;


    //GEN WAYPOINTS (WAYPOINT MODE)
    void genWaypoints()
    {
        Waypoints.GetComponent<Waypoints>().GenerateWaypoint(1, 1);
    }

    //GEN TILE CENTERS (TILE MODE)
    void genTilePoints()
    {
        for (int i = 0; i < height - 1; i += 2)
        {
            for (int j = 0; j < width - 1; j += 2)
            {
                if (map[i, j] == '.' || map[i, j + 1] == '.' || map[i + 1, j] == '.' || map[i + 1, j + 1] == '.')
                {
                    Waypoints.GetComponent<Waypoints>().GenerateWaypoint(j, height - i);
                }
            }
        }
    }

    //load a map from file
    void loadMap(string mapFile)
    {
        //load map text
        string[] lines = File.ReadAllLines(mapFile);
        mapType = lines[0].Substring(5); // get the map type from the first line
        height = int.Parse(lines[1].Substring(7)); // get the height from the second line
        Debug.Log(height);
        width = int.Parse(lines[2].Substring(6)); // get the width from the third line
        Debug.Log(width);
        map = new char[height, width]; // create a 2D char array to hold the map data
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                map[i, j] = lines[i + 4][j]; // read the map data from the lines starting from the fourth line
            }
        }

        // Clear any existing tiles attached to the map game object.
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }

        //load map prefabs
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GameObject tilePrefab;
                switch (map[i, j])
                {
                    case '.':
                        tilePrefab = passableTerrainPrefab;
                        break;
                    case '@':
                        tilePrefab = outOfBoundsPrefab;
                        break;
                    case 'T':
                        tilePrefab = unpassableTerrainPrefab;
                        break;
                    default:
                        Debug.LogError("Invalid tile type: " + map[i, j]);
                        continue;
                }
                GameObject tile = Instantiate(tilePrefab, new Vector3(j, height - i - 1, 0), Quaternion.identity, transform);
                // set the tile's name to its coordinates for debugging purposes
                tile.name = "(" + j + "," + i + ")";
            }
        }
    }

    void Start()
    {
        if (showMap1)
            loadMap(mapFile1);
        else
            loadMap(mapFile2);
        if(tileMode)
            genTilePoints();
        else
            genWaypoints();

        // adjust camera to fit entire map in view
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float mapAspect = (float)width / (float)height;
        if (mapAspect > screenAspect)
        {
            Camera.main.orthographicSize = height / 2f;
        }
        else
        {
            Camera.main.orthographicSize = (height / 2f) / mapAspect;
        }

        // center camera on middlemost tile
        Vector3 topLeft = new Vector3(0, height - 1, 0);
        Vector3 bottomRight = new Vector3(width - 1, 0, 0);
        Vector3 middle = (topLeft + bottomRight) / 2f;
        middle.z = -10f;
        Camera.main.transform.position = middle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
