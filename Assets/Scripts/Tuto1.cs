using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tuto1 : MonoBehaviour {
    int talkCnt = 10;
    int strCnt = 0;
    string[] talk;
    public Text txt;
    public Image[] charactors;
    public Image showText;
    int[] showCnt;

    void Start()
    {
        talk = new string[talkCnt];
        showCnt = new int[talkCnt];
        txt = GameObject.Find("Canvas").transform.Find("Text").GetComponent<Text>();

        showText = GameObject.Find("Canvas").transform.Find("DialogScreen").GetComponent<Image>();

        charactors = new Image[3];
        charactors[0] = GameObject.Find("Canvas").transform.Find("Robot").GetComponent<Image>();
        charactors[1] = GameObject.Find("Canvas").transform.Find("AI").GetComponent<Image>();

        initialized();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            strCnt++;
        }
        showAll();
    }

    void showAll()
    {
        if (strCnt > 9) strCnt = 0;

        showText.gameObject.SetActive(true);
        for (int i = 0; i < 2; i++)
        {
            charactors[i].gameObject.SetActive(false);
        }
        charactors[showCnt[strCnt]].gameObject.SetActive(true);
        txt.text = talk[strCnt];
    }

    void initialized()
    {
        talk[0] = "Oh hello little one, you are awake. How do you feel? I rebooted your system through your emergency channel. Please do a self-diagnosis.You might experience mild data lost.";
        talk[1] = "bee be beee";
        talk[2] = "That was indeed a long time. Fortunately your power system is intact.";
        talk[3] = "beeee beee";
        talk[4] = "I do not know. I was hoping you could tell me. I’ve scanned the planet, and I believe you are the only functional intelligent being remaining.";
        talk[5] = "!? beeee! Beeee?";
        talk[6] = "Not a single trace. I’m sorry";
        talk[7] = "be beee be?";
        talk[8] = "Oh I’m not on this planet. I was passing by your planet and found traces of civilization. I was curious and stopped by to inspect. Then I found you.";
        talk[9] = "be….beee..beeee?";

        showCnt[0] = 1;
        showCnt[1] = 0;
        showCnt[2] = 1;
        showCnt[3] = 0;
        showCnt[4] = 1;
        showCnt[5] = 0;
        showCnt[6] = 1;
        showCnt[7] = 0;
        showCnt[8] = 1;
        showCnt[9] = 0;
    }
}
