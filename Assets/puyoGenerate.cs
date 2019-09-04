using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

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

    GameObject mapGenerater; 
    mapGenerate mapGenerateScript;

    enum Puyo : int
    {
        red = 1,
        blue = 2,
        green = 3,
        yellow = 4,
        purple = 5,
    }
    PuyoCom puyocom = new PuyoCom(); //色の管理
    PuyoComNow pnext = new PuyoComNow();//nextのぷよ
    PuyoComNow pn = new PuyoComNow(); //操作中のぷよ

    bool isOk = false; //true=操作中

    // Use this for initialization
    void Start()
    {
        mapGenerater = GameObject.Find("mapGenerater");
        mapGenerateScript = mapGenerater.GetComponent<mapGenerate>();
        nextGenerate();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOk == true)
        {
            Move();
        }
        else
        {
            Operatepuyo();
            isOk = true;
            Debug.Log(isOk);
        }
    }

    /*
     nextにぷよを出現させる関数 
     */
    void Generate(int p1, int p2)
    {
        switch (p1)
        {
            case (int)Puyo.red:
                pnext.p1 = (GameObject)Instantiate(red, new Vector3(11, 12, 0.0f), Quaternion.identity);
                break;
            case (int)Puyo.blue:
                pnext.p1 = (GameObject)Instantiate(blue, new Vector3(11, 12, 0.0f), Quaternion.identity);
                break;
            case (int)Puyo.green:
                pnext.p1 = (GameObject)Instantiate(green, new Vector3(11, 12, 0.0f), Quaternion.identity);
                break;
            case (int)Puyo.yellow:
                pnext.p1 = (GameObject)Instantiate(yellow, new Vector3(11, 12, 0.0f), Quaternion.identity);
                break;
            case (int)Puyo.purple:
                pnext.p1 = (GameObject)Instantiate(purple, new Vector3(11, 12, 0.0f), Quaternion.identity);
                break;
            default: break;
        }
        switch (p2)
        {
            case (int)Puyo.red:
                pnext.p2 = (GameObject)Instantiate(red, new Vector3(11, 11, 0.0f), Quaternion.identity);
                break;
            case (int)Puyo.blue:
                pnext.p2 = (GameObject)Instantiate(blue, new Vector3(11, 11, 0.0f), Quaternion.identity);
                break;
            case (int)Puyo.green:
                pnext.p2 = (GameObject)Instantiate(green, new Vector3(11, 11, 0.0f), Quaternion.identity);
                break;
            case (int)Puyo.yellow:
                pnext.p2 = (GameObject)Instantiate(yellow, new Vector3(11, 11, 0.0f), Quaternion.identity);
                break;
            case (int)Puyo.purple:
                pnext.p2 = (GameObject)Instantiate(purple, new Vector3(11, 11, 0.0f), Quaternion.identity);
                break;
            default: break;
        }
        //pnext.p1.tag = "next";
        //pnext.p2.tag = "next";
    }
    /*
     nextのところに出現するぷよを乱数で生成する関数
     */
    void nextGenerate()
    {
        int p = Random.Range(1,5);
        puyocom.p1 = p;
        p = Random.Range(1, 5);
        puyocom.p2 = p;
        Generate(puyocom.p1, puyocom.p2);
    }

    /*
     nextのところからぷよを引っ張り出してくる関数 
     */
    void Operatepuyo()
    {
        pn.p1 = (GameObject)Instantiate(pnext.p1, new Vector3(4, 13, 0.0f), Quaternion.identity);
        pn.p2 = (GameObject)Instantiate(pnext.p2, new Vector3(4, 12, 0.0f), Quaternion.identity);
        pn.p2.transform.parent = pn.p1.transform;
        Destroy(pnext.p1);
        Destroy(pnext.p2);
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
            p1x = CalcError(p1x);
            p1y = CalcError(p1y);
            p2x = CalcError(p2x);
            p2y = CalcError(p2y);

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
                Debug.Log("x:" + (int)(p2x) + " " + (int)(p1x));
                Debug.Log("y:" + (int)(p2y) + " " + (int)(p1y));
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
                if ((z == 0.0f && (mapGenerateScript.getMap((int)p1y, (int)(p1x - 1)) != 0)) ||
                    (z == 90.0f && (p1y - 1) <= 0) ||
                    (z == 180.0f && (mapGenerateScript.getMap((int)p1y, (int)(p1x + 1)) != 0)) ||
                    (z == 270.0f && (p1y + 1) >= maxY))
                {
                }
                else
                {
                    //Debug.Log("bb");
                    //Debug.Log(quaternion);
                    //Debug.Log(quaternion.eulerAngles.z);
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
                    //Debug.Log(quaternion);
                    //Debug.Log(quaternion.eulerAngles.z);
                    pn.p1.transform.Rotate(0, 0, 90.0f);
                }
            }
            Decision();
        }
    }


    IEnumerator Example()
    {
        Debug.Log(Time.time);
        yield return new WaitForSeconds(2.0f);
        pn.p1.transform.Translate(0, -1, 0, Space.World);
        Debug.Log(Time.time);
    }

    //置き場所を決めたときに発動する関数
    void Decision()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bool flag = true;
            FreeFall(flag);
            isOk = false;
        }
    }

    //ぷよの自由落下関数
    void FreeFall(bool f)
    {
        float p1x = pn.p1.transform.position.x;
        float p1y = pn.p1.transform.position.y;
        float p2x = pn.p2.transform.position.x;
        float p2y = pn.p2.transform.position.y;
        int ip1x = CalcError(p1x);
        int ip1y = CalcError(p1y);
        int ip2x = CalcError(p2x);
        int ip2y = CalcError(p2y);
        const int dy = -1;
        while (mapGenerateScript.getMap(ip1y+dy,ip1x)==0 && mapGenerateScript.getMap(ip2y + dy, ip2x) == 0)
        {
            //StartCoroutine(Example());
            //Example();
            pn.p1.transform.Translate(0, dy, 0, Space.World);
            ip1y--;
            ip2y--;
        }
        pn.p2.transform.parent = null;
        Vector3 p1pos = pn.p1.transform.position;
        Vector3 p2pos = pn.p2.transform.position;
        while (mapGenerateScript.getMap(ip1y + dy, ip1x) == 0 && (p1pos.y + dy != p2pos.y))
        {
            //StartCoroutine(Example());
            //Example();
            pn.p1.transform.Translate(0, dy, 0, Space.World);
            ip1y--;
        }
        p1x = pn.p1.transform.position.x;
        p1y = pn.p1.transform.position.y;
        ip1x = CalcError(p1x);
        ip1y = CalcError(p1y);
        mapGenerateScript.setMap(ip1y, ip1x, puyocom.p1);
        Debug.Log("aaa:"+ ip1y);
        while (mapGenerateScript.getMap(ip2y + dy, ip2x) == 0 && (p2pos.y + dy != p1pos.y))
        {
            //StartCoroutine(Example());
            //Example();
            pn.p2.transform.Translate(0, dy, 0, Space.World);
            ip2y--;
        }
        Debug.Log("aasa:" + ip2y);

        p2x = pn.p2.transform.position.x;
        p2y = pn.p2.transform.position.y;
        ip2x = CalcError(p2x);
        ip2y = CalcError(p2y);
        mapGenerateScript.setMap(ip2y, ip2x, puyocom.p2);
        for(int i = 0;i<maxY; i++)
        {
            for(int j = 0;j<maxX; j++)
            {
                Debug.Log(mapGenerateScript.getMap(i,j) + " ");
            }
            Debug.Log("\n");
        }
    }

    void Erase()
    {
        for(int i = 0; i < maxY; i++)
        {
            for(int j = 0; j< maxX; j++)
            {
                int c = mapGenerateScript.getMap(i, j);
                if (CountSamecolor(i,j,1,c) >= 4)
                {

                }
            }
        }
    }

    int CountSamecolor(int y,int x,int cnt,int color)
    {
        int _color = mapGenerateScript.getMap(y,x);
        if (_color == color)
        {
            CountSamecolor(y + 1, x, cnt + 1, color);
            CountSamecolor(y - 1, x, cnt + 1, color);
            CountSamecolor(y, x + 1, cnt + 1, color);
            CountSamecolor(y, x - 1, cnt + 1, color);
        }
        else
        {
            return cnt;
        }
        return 0;
    }

    //丸め誤差対策関数
    int CalcError(float a)
    {
        int res = 0;
        if ((a % 1) >= 0.5)
        {
            res = (int)a + 1;
        }
        else
        {
            res = (int)a;
        }
        return res;
    }
}

