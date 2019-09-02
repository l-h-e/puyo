using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mapGenerate : MonoBehaviour {

    const int maxX = 8;
    const int maxY = 14;
    private int[,] m = new int[maxY,maxX]; //m[y][x];

    [SerializeField] private GameObject red;
    [SerializeField] private GameObject blue;
    [SerializeField] private GameObject green;
    [SerializeField] private GameObject yellow;
    [SerializeField] private GameObject purple;
    [SerializeField] private GameObject wall;

    enum Puyo:int
    {
        n = 0,
        red = 1,
        blue = 2,
        green = 3,
        yellow = 4,
        purple = 5,
        wall = -1
    }


    // Use this for initialization
    void Start () {
        for (int i = 1; i < maxY-1; i++)
        {
            for (int j = 1; j < maxX-1; j++)
            {
                m[i,j] = (int)Puyo.n;
            }
        }
        for(int i = 0; i < maxX; i++)
        {
            m[0, i] = (int)Puyo.wall;
        }
        for (int i = 1; i < maxY; i++)
        {
            m[i,0] = (int)Puyo.wall;
        }
        for (int i = 1; i < maxY; i++)
        {
            m[i,maxX-1] = (int)Puyo.wall;
        }
        wallRender();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void wallRender()
    {
        for (int i = 0; i < maxY; i++)
        {
            for (int j = 0; j < maxX; j++)
            {
                switch (m[i,j])
                {
                    case (int)Puyo.red:
                        Instantiate(red, new Vector3(j, i, 0.0f), Quaternion.identity);
                        break;
                    case (int)Puyo.blue:
                        Instantiate(blue, new Vector3(j, i, 0.0f), Quaternion.identity);
                        break;
                    case (int)Puyo.green:
                        Instantiate(green, new Vector3(j, i, 0.0f), Quaternion.identity);
                        break;
                    case (int)Puyo.yellow:
                        Instantiate(yellow, new Vector3(j, i, 0.0f), Quaternion.identity);
                        break;
                    case (int)Puyo.purple:
                        Instantiate(purple, new Vector3(j, i, 0.0f), Quaternion.identity);
                        break;
                    case (int)Puyo.wall:
                        Instantiate(wall, new Vector3(j, i, 0.0f), Quaternion.identity);
                        break;
                    default:break;

                }
            }
        }
    }

    public int getMap(int y,int x)
    {
        return m[y,x];
    }
}
