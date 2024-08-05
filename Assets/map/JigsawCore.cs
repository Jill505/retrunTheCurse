using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawCore : MonoBehaviour
{
    public int Floor = 0;// 0-"外來強盜幫們" 1-"巴別塔強盜幫和結晶人" 2-"蒙特婁vs圖爾瑪" 3-"死靈法師與戰爭勝利者"
    public int levelCount = 0;

    public List<GameObject> Floor_0;
    public List<GameObject> Floor_1;
    public List<GameObject> Floor_2;
    public List<GameObject> Floor_3;

    public List<GameObject> BossFloor;

    public GameObject JigsawBaseSpeical;

    public float floorConst = 15;

    GameObject JigsawOnLoad;

    public int countDebugJammerCounter;

    public bool[] lastTop = new bool[18];
    // Start is called before the first frame update
    void Start()
    {
        //if (gameReset)
        gameStartReset();
    }

    public void gameStartReset()
    {
        Floor = 0;
        levelCount = 0;

        //set Base Jigsaw
        Instantiate(JigsawBaseSpeical,new Vector3(0,0,0), Quaternion.identity);

        lastTop = JigsawBaseSpeical.GetComponent<JigsawCornerStone>().top;

        GenerateNewJigsaw();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateNewJigsaw()
    {
        levelCount++;

        if (levelCount == 25)//某種條件 載入boss關卡
        {

        }
        else
        {
            int RandomNum;
            //generate
            switch (Floor)
            {
                case 0:
                    GenerateFloor0();
                    break;
                
                case 1:
                    RandomNum = Random.Range(0, Floor_1.Count);
                    break;

                case 2:
                    RandomNum = Random.Range(0, Floor_2.Count);
                    break;

                case 3:
                    RandomNum = Random.Range(0, Floor_3.Count);
                    break;
            }
        }
    }

    public void InsJigsaw()
    {
        Instantiate(JigsawOnLoad, new Vector3(0, floorConst * levelCount, 0), Quaternion.identity);
    }

    public void GenerateFloor0()
    {
        int RandomNum;
        RandomNum = Random.Range(0, Floor_0.Count);

        bool succ =false;

        bool[] compare = Floor_0[RandomNum].GetComponent<JigsawCornerStone>().button;

        for (int i = 0; i < 18; i++)
        {
            if (lastTop[i] == compare[i] && lastTop[i] ==  true)
            {
                //執行生成
                succ = true;
                Debug.Log("生成判定：可");
                break;
            }
        }

        if (succ)
        {
            JigsawOnLoad = Floor_0[RandomNum];
            lastTop = Floor_0[RandomNum].GetComponent<JigsawCornerStone>().top;
            InsJigsaw();
            Debug.Log("生成成功");

            countDebugJammerCounter = 0;
        }
        else
        {
            //re generate
            if (countDebugJammerCounter < 25) {

                GenerateFloor0();
                Debug.Log("條件不符 重新嘗試生成");

                countDebugJammerCounter++;
            }
            else
            {
                Debug.Log("生成過多次導致系統運算過載");
            }
        }
    }
}
