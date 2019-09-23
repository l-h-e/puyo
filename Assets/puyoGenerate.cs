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
   struct pair
    {
        public int first;
        public int second;
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

    string rp = "red(Clone)(Clone)";
    string bp = "blue(Clone)(Clone)";
    string gp = "green(Clone)(Clone)";
    string yp = "yellow(Clone)(Clone)";
    string pp = "purple(Clone)(Clone)";

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
    bool isFall = false; //自由落下中かどうか

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
            if(!isFall)Move();
        }
        else
        {
            Operatepuyo();
//            Erase();
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
        p = 1;
        puyocom.p1 = p;
        p = Random.Range(1, 5);
        p = 2;
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
        if (pn.p1 && pn.p2)
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

    //置き場所を決めたときに発動する関数
    void Decision()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isFall) //既に落下中でなければ自由落下。落下中に落下させるとバグる
            {
                StartCoroutine(Fall());
            }
        }
    }

    //ぷよの自由落下関数
    IEnumerator Fall()
    {
        isFall = true;
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
            yield return new WaitForSeconds(0.1f);
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
            yield return new WaitForSeconds(0.1f);
            pn.p1.transform.Translate(0, dy, 0, Space.World);
            ip1y--;
        }
        yield return new WaitForSeconds(0.1f);
        //p1x = pn.p1.transform.position.x;
        //p1y = pn.p1.transform.position.y;
        //ip1x = CalcError(p1x);
        //ip1y = CalcError(p1y);
        mapGenerateScript.setMap(ip1y, ip1x, puyocom.p1);
        Debug.Log("p1の色は:"+ puyocom.p1);
        while (mapGenerateScript.getMap(ip2y + dy, ip2x) == 0 && (p2pos.y + dy != p1pos.y))
        {
            //StartCoroutine(Example());
            //Example();
            yield return new WaitForSeconds(0.05f);
            pn.p2.transform.Translate(0, dy, 0, Space.World);
            ip2y--;
        }
        Debug.Log("p2の色は:" + puyocom.p2);

        //p2x = pn.p2.transform.position.x;
        //p2y = pn.p2.transform.position.y;
        //ip2x = CalcError(p2x);
        //ip2y = CalcError(p2y);
        mapGenerateScript.setMap(ip2y, ip2x, puyocom.p2);
        for(int i = 0;i<maxY; i++)
        {
            for(int j = 0;j<maxX; j++)
            {
                Debug.Log(mapGenerateScript.getMap(i,j) + " ");
            }
            Debug.Log("\n");
        }
        yield return new WaitForSeconds(0.1f);
        // isOk = false;
        //        StartCoroutine(Erase());
        Erase();
        isFall = false;
    }
    bool Erase()
    {
        bool a = true;
        for (int i = 1; i < maxY - 1; i++)
        {
            for (int j = 1; j < maxX - 1; j++)
            {
                int c = mapGenerateScript.getMap(i, j);
                // Tuple<string, int> t = new Tuple<string, int>("Hello", 4);
                if (c != 0 && c != -1)
                {
                    if (SamecolorErase(i, j, c))
                    {
                        a = false;
                        StartCoroutine(FreeFall());
                        return true;
                        Debug.Log("aa:" + c);
                        //yield return new WaitForSeconds(0.2f);
                    }
                }
            }
        }
               isOk = false;
        return false;
        //if (a)
        //{
        //    isOk = false;
        //    return false;
        //}
        //return true;
    }
    bool SamecolorErase(int y, int x, int color)
    {
        Queue<int> qy = new Queue<int>();
        Queue<int> qx = new Queue<int>();
        int[] dx = new int[4] { 0 , 1 , 0 , -1 };
        int[] dy = new int[4] { 1 , 0 , -1 , 0 };
        qy.Enqueue(y);
        qx.Enqueue(x);
        int cnt = 0;
        int[,] qm = new int[maxY, maxX];

        for (int i = 0; i < maxY; i++)
        {
            for(int j = 0; j < maxX; j++)
            {
                if (mapGenerateScript.getMap(i, j) == color) qm[i, j] = (int)1e5;
                //else if (mapGenerateScript.getMap(i, j) == color) qm[i, j] = 1;
                else qm[i, j] = -1;
                //qm[i, j] = (int)1e5;
            }
        }
        while (qy.Count>0)
        {
            pair p = new pair();
            p.first = qy.Peek();
            p.second = qx.Peek();
            qy.Dequeue(); qx.Dequeue();
            for (int i = 0; i < 4; i++)
            {
                int ny = p.first + dy[i], nx = p.second + dx[i];
                // int _color = mapGenerateScript.getMap(ny, nx);
                
                if (nx >= 0 && nx < maxX && 0 <= ny && ny < maxY && mapGenerateScript.getMap(ny, nx) == color && qm[ny, nx] == 1e5)
                {
                    Debug.Log("x,y" + x + " " + y);
                    qy.Enqueue(ny); qx.Enqueue(nx);
                    qm[ny, nx] = 0;
                    cnt++;
                }
            }
        }

        if (cnt >= 4)
        {
            foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
            {
                // シーン上に存在するオブジェクトならば処理.
                if (obj.activeInHierarchy)
                {
                    Debug.Log(obj.name);
                    string eraseObject = "";
                    switch (color)
                    {
                        case (int)Puyo.red:
                            eraseObject = rp;
                            break;
                        case (int)Puyo.blue:
                            eraseObject = bp;
                            break;
                        case (int)Puyo.green:
                            eraseObject = gp;
                            break;
                        case (int)Puyo.yellow:
                            eraseObject = yp;
                            break;
                        case (int)Puyo.purple:
                            eraseObject = pp;
                            break;
                        default: break;
                    }

                    // GameObjectの名前を表示.
                    if (obj.name == eraseObject)
                    {
                        int xx = CalcError(obj.transform.position.x);
                        int yy = CalcError(obj.transform.position.y);
                        Debug.Log("yy,xx,qm" + yy + " " + xx + " " + qm[yy, xx]);
                        //if (qm[yy, xx] == 0) obj.tag = "erase";
                        if (qm[yy, xx] == 0)
                        {
                            Destroy(obj);
                            mapGenerateScript.setMap(yy, xx, 0);
                        }
                    }
                }
            }
            return true;
        }
        return false;
    }

    IEnumerator FreeFall()
    {
        GameObject[] objbuf = new GameObject[100];
        int i = 0;
        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
        {
            if (obj.name == rp || obj.name == bp || obj.name == gp || obj.name == yp || obj.name == pp)
            {
                objbuf[i] = obj;
                i++;
            }
        }

        for(int j = 0; j < i; j++)
        {
            for(int k = 0; k < i; k++)
            {
                float a = objbuf[j].transform.position.y;
                float b = objbuf[k].transform.position.y;
                if (a < b)
                {
                    GameObject tmp = objbuf[j];
                    objbuf[j] = objbuf[k];
                    objbuf[k] = tmp;
                }
            }
        }
        foreach (GameObject obj in objbuf)
        {
            if (!obj) break;
            Debug.Log("obj:"+obj+" y:" + obj.transform.position.y);
        }

        foreach (GameObject obj in objbuf)
        {
            //if (obj == (pn.p1 || pn.p2)) continue;
            if (!obj) continue;
            if (obj.name == rp || obj.name == bp || obj.name == gp || obj.name == yp || obj.name == pp)
            {
                int xx = CalcError(obj.transform.position.x);
                int yy = CalcError(obj.transform.position.y);
                int _color = mapGenerateScript.getMap(yy, xx);
                while (mapGenerateScript.getMap(yy - 1, xx) == 0)
                {
                    if (yy -1 != 0)
                    {
                        mapGenerateScript.setMap(yy, xx, 0);
                        yy--;
                        obj.transform.Translate(0, -1, 0, Space.World);
                        yield return new WaitForSeconds(0.1f);
                        mapGenerateScript.setMap(yy , xx, _color);
                    }
                }
            }
        }
        //StartCoroutine(Erase());
        //if (Erase()) isOk = false;
        Erase();
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

