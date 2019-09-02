using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puyoGenerate : MonoBehaviour
{

    struct PuyoCom
    {
        public int p1;
        public int p2;
    }
    struct PuyoComNow
    {
        public GameObject p1;
        public GameObject p2;

    }
    System.Random r = new System.Random(1000);
    const int maxX = 8;
    const int maxY = 14;
    [SerializeField] private GameObject red;
    [SerializeField] private GameObject blue;
    [SerializeField] private GameObject green;
    [SerializeField] private GameObject yellow;
    [SerializeField] private GameObject purple;

    GameObject mapGenerater; //Unityちゃんそのものが入る変数
    mapGenerate mapGenerateScript;

    enum Puyo : int
    {
        red = 0,
        blue = 1,
        green = 2,
        yellow = 3,
        purple = 4,
    }
    PuyoCom puyocom = new PuyoCom();
    PuyoComNow pn = new PuyoComNow();

    bool isOk = true;
    // Use this for initialization
    void Start()
    {
        mapGenerater = GameObject.Find("mapGenerater");
        mapGenerateScript = mapGenerater.GetComponent<mapGenerate>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (isOk)
        {
            nextGenerate();
            Operatepuyo();
            isOk = false;
        }
    }

    void Generate(int p1, int p2)
    {
        switch (p1)
        {
            case (int)Puyo.red:
                Instantiate(red, new Vector3(11, 12, 0.0f), Quaternion.identity);
                break;
            case (int)Puyo.blue:
                Instantiate(blue, new Vector3(11, 12, 0.0f), Quaternion.identity);
                break;
            case (int)Puyo.green:
                Instantiate(green, new Vector3(11, 12, 0.0f), Quaternion.identity);
                break;
            case (int)Puyo.yellow:
                Instantiate(yellow, new Vector3(11, 12, 0.0f), Quaternion.identity);
                break;
            case (int)Puyo.purple:
                Instantiate(purple, new Vector3(11, 12, 0.0f), Quaternion.identity);
                break;
            default: break;
        }
        switch (p2)
        {
            case (int)Puyo.red:
                Instantiate(red, new Vector3(11, 11, 0.0f), Quaternion.identity);
                break;
            case (int)Puyo.blue:
                Instantiate(blue, new Vector3(11, 11, 0.0f), Quaternion.identity);
                break;
            case (int)Puyo.green:
                Instantiate(green, new Vector3(11, 11, 0.0f), Quaternion.identity);
                break;
            case (int)Puyo.yellow:
                Instantiate(yellow, new Vector3(11, 11, 0.0f), Quaternion.identity);
                break;
            case (int)Puyo.purple:
                Instantiate(purple, new Vector3(11, 11, 0.0f), Quaternion.identity);
                break;
            default: break;
        }
    }

    void nextGenerate()
    {
        int p = r.Next(4);
        switch (p)
        {
            case (int)Puyo.red:
                puyocom.p1 = (int)Puyo.red;
                break;
            case (int)Puyo.blue:
                puyocom.p1 = (int)Puyo.blue;
                break;
            case (int)Puyo.green:
                puyocom.p1 = (int)Puyo.green;
                break;
            case (int)Puyo.yellow:
                puyocom.p1 = (int)Puyo.yellow;
                break;
            case (int)Puyo.purple:
                puyocom.p1 = (int)Puyo.purple;
                break;
            default: break;
        }
        p = r.Next(4);
        switch (p)
        {
            case (int)Puyo.red:
                puyocom.p2 = (int)Puyo.red;
                break;
            case (int)Puyo.blue:
                puyocom.p2 = (int)Puyo.blue;
                break;
            case (int)Puyo.green:
                puyocom.p2 = (int)Puyo.green;
                break;
            case (int)Puyo.yellow:
                puyocom.p2 = (int)Puyo.yellow;
                break;
            case (int)Puyo.purple:
                puyocom.p2 = (int)Puyo.purple;
                break;
            default: break;
        }
        Generate(puyocom.p1, puyocom.p2);
    }

    void Operatepuyo()
    {
        switch (puyocom.p1)
        {
            case (int)Puyo.red:
                pn.p1 = (GameObject)Instantiate(red, new Vector3(4, 13, 0.0f), Quaternion.identity);
                break;
            case (int)Puyo.blue:
                pn.p1 = (GameObject)Instantiate(blue, new Vector3(4, 13, 0.0f), Quaternion.identity);
                break;
            case (int)Puyo.green:
                pn.p1 = (GameObject)Instantiate(green, new Vector3(4, 13, 0.0f), Quaternion.identity);
                break;
            case (int)Puyo.yellow:
                pn.p1 = (GameObject)Instantiate(yellow, new Vector3(4, 13, 0.0f), Quaternion.identity);
                break;
            case (int)Puyo.purple:
                pn.p1 = (GameObject)Instantiate(purple, new Vector3(4, 13, 0.0f), Quaternion.identity);
                break;
            default: break;
        }
        //Instantiate(pn.p1, new Vector3(4, 13, 0.0f), Quaternion.identity);
        switch (puyocom.p2)
        {
            case (int)Puyo.red:
                pn.p2 = (GameObject)Instantiate(red, new Vector3(4, 12, 0.0f), Quaternion.identity);
                break;
            case (int)Puyo.blue:
                pn.p2 = (GameObject)Instantiate(blue, new Vector3(4, 12, 0.0f), Quaternion.identity);
                break;
            case (int)Puyo.green:
                pn.p2 = (GameObject)Instantiate(green, new Vector3(4, 12, 0.0f), Quaternion.identity);
                break;
            case (int)Puyo.yellow:
                pn.p2 = (GameObject)Instantiate(yellow, new Vector3(4, 12, 0.0f), Quaternion.identity);
                break;
            case (int)Puyo.purple:
                pn.p2 = (GameObject)Instantiate(purple, new Vector3(4, 12, 0.0f), Quaternion.identity);
                break;
            default: break;
        }
        pn.p2.transform.parent = pn.p1.transform;
        //Instantiate(pn.p2, new Vector3(4, 12, 0.0f), Quaternion.identity);
        nextGenerate();
    }

    void Move()
    {
        if (pn.p1)
        {
            float p1x = pn.p1.transform.position.x;
            float p1y = pn.p1.transform.position.y;
            float p2x = pn.p2.transform.position.x;
            float p2y = pn.p2.transform.position.y;

            /*丸め誤差対策*/
            if (p1x % 1 >= 0.5)
            {
                p1x = (int)p1x+1;
            }
            else
            {
                p1x = (int)p1x;
            }
            if (p1y % 1 >= 0.5)
            {
                p1y = (int)p1y + 1;
            }
            else
            {
                p1y = (int)p1y;
            }
            if (p2x % 1 >= 0.5)
            {
                p2x = (int)p2x + 1;
            }
            else
            {
                p2x = (int)p2x;
            }
            if (p2y % 1 >= 5)
            {
                p2y = (int)p2y + 1;
            }
            else
            {
                p2y = (int)p2y;
            }

            float dx = 0, dy = 0;
            if (Input.GetKeyDown(KeyCode.LeftArrow)) dx = -1.0f;
            if (Input.GetKeyDown(KeyCode.RightArrow)) dx = 1.0f;
            if (Input.GetKeyDown(KeyCode.UpArrow)) dy = 1.0f;
            if (Input.GetKeyDown(KeyCode.DownArrow)) dy = -1.0f;
           // Debug.Log((int)(p2y + dy) + " " + (int)(p1y + dy));
           // Debug.Log("x:" + (int)(p2x) + " " + (int)(p1x));
           // Debug.Log("x:" + (int)(p2x) + " " + (int)(p1x));
            //Debug.Log("y:" + (int)(p2y) + " " + (int)(p1y));
            if (p1y + dy >= maxY || p2y + dy >= maxY) dy = 0;
            if ((mapGenerateScript.getMap((int)(p1y + dy), (int)(p1x + dx))) == 0 && (mapGenerateScript.getMap((int)(p2y + dy), (int)(p2x + dx))) == 0)
            {
                pn.p1.transform.Translate(dx, dy, 0, Space.World);
            }



            //右回転
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //  pn.p1.transform.Rotate(0, 0, -90.0f);

                Quaternion quaternion = pn.p1.transform.rotation;
                float z = quaternion.eulerAngles.z;
                Debug.Log((int)p1y + " " + (int)(p1x - 1));
                Debug.Log((int)p1y + " " + (int)(p1y - 1));
                Debug.Log((int)p1y + " " + (int)(p1x + 1));
                if ((z == 0.0f && (mapGenerateScript.getMap((int)p1y, (int)(p1x - 1)) != 0) ) ||
                    (z == 90.0f && (p1y - 1) <= 0) ||
                    (z == 180.0f && (mapGenerateScript.getMap((int)p1y, (int)(p1x + 1)) != 0)) ||
                    (z == 270.0f && (p1y + 1) >= maxY))
                {
                }
                else
                {
                    Debug.Log("bb");
                    Debug.Log(quaternion);
                    Debug.Log(quaternion.eulerAngles.z);
                    pn.p1.transform.Rotate(0, 0, -90.0f);
                }
            }

            //左回転
            if (Input.GetKeyDown(KeyCode.X))
            {
                Quaternion quaternion = pn.p1.transform.rotation;
                float z = quaternion.eulerAngles.z;
                if ((z == 0.0f && (mapGenerateScript.getMap((int)p1y, (int)(p1x + 1)) != 0)) ||
                    (z == 90.0f && (p1y + 1) >= maxY) ||
                    (z == 180.0f && (mapGenerateScript.getMap((int)p1y, (int)(p1x - 1)) != 0)) ||
                    (z == 270.0f && (p1y - 1) <= 0))
                {
                }
                else
                {
                    Debug.Log(quaternion);
                    Debug.Log(quaternion.eulerAngles.z);
                    pn.p1.transform.Rotate(0, 0,90.0f);
                }
            }
        }
    }
}

