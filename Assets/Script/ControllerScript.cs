using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerScript : MonoBehaviour {
    public GameObject txt;
    public GameObject EffScore;

    public static int ScorePlayer;
    public static int ScoreComputer;

    public Text Goldtext;
    public static int Gold;
    public Text CuocText;
    public Text MinValue;
    public Text MaxValue;

    public static int DemGiaTri;
    public Slider sliderMoney;

    private Sprite[] Bobai;
    public List<Sprite> ListBoBai;

    public GameObject Card1;
    public GameObject Card2;
    public GameObject Card3;

    public GameObject GameOver;

    public GameObject ComputerCard1;
    public GameObject ComputerCard2;
    public GameObject ComputerCard3;

    public GameObject btnHaBai;
    public GameObject btnChiaBai;
    public GameObject btnContinue;

    private int oldGold;
    private GameObject kq;
    public GameObject Canvas_Eff;
    private void Awake()
    {
       
        
        
        Bobai = Resources.LoadAll<Sprite>("Bobai");
        foreach (var key in Bobai)
        {
            ListBoBai.Add(key);
        }


    }
    // Use this for initialization
    void Start () {

        Time.timeScale = 1;
        GameOver.SetActive(false);
        sliderMoney.minValue = 1;
        MinValue.text = sliderMoney.minValue.ToString();
        ScoreComputer = 0;
        ScorePlayer = 0;
        Gold = 1000;
        sliderMoney.enabled = true;
        DemGiaTri = 0;
        btnHaBai.SetActive(false);
        btnChiaBai.SetActive(true);
        btnContinue.SetActive(false);
        oldGold = Gold;
    }
	
	// Update is called once per frame
	void Update () {        
		if(DemGiaTri == 3)
        {
            btnHaBai.SetActive(true);
        }
        
        sliderMoney.maxValue = Gold;
        Goldtext.text = "Gold: " +Gold;
        CuocText.text = sliderMoney.value.ToString();
        MaxValue.text = sliderMoney.maxValue.ToString();

        if (Gold <= 0 && !kq)
        {
            Time.timeScale = 0;
            GameOver.SetActive(true);
        }

    }

    public void ClickChiaBai()
    {

        ScoreComputer = 0;
        ScorePlayer = 0;

        Card1.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("MatSau/blue_back");
        Card2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("MatSau/blue_back");
        Card3.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("MatSau/blue_back");

        ComputerCard1.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("MatSau/blue_back");
        ComputerCard2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("MatSau/blue_back");
        ComputerCard3.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("MatSau/blue_back");

        btnChiaBai.SetActive(false);
        
    }

    public void HaBai()
    {
        //Debug.Log("Count List Bo Bai:" + ListBoBai.Count);
        for(int i = 1; i <= 3; i++)
        {
            int b = UnityEngine.Random.Range(0, ListBoBai.Count);
            string name = "ComputerCard" + i;
            GameObject.Find(name).GetComponent<SpriteRenderer>().sprite = ListBoBai[b];
            
            string scoreComputerString = ListBoBai[b].name;
            int scoreComputerInt = Int32.Parse(scoreComputerString.Substring(0, 1));

            //Debug.Log("ListBoBai[b].name" + ListBoBai[b].name);
            //Debug.Log("Score int:" + scoreComputerInt);
            ScoreComputer += scoreComputerInt;

            ListBoBai.Remove(ListBoBai[b]);
        }
        

        
        //Debug.Log("Score Computer" + ScoreComputer);        
        btnContinue.SetActive(true);
        SoSanh();
        foreach (var key in Bobai)
        {
            ListBoBai.Remove(key);
        }
       
        foreach (var key in Bobai)
        {
            ListBoBai.Add(key);
        }

    }

    public void SoSanh()
    {
        
        string scoreComputerFinal = ScoreComputer.ToString();
        string scorePlayerFinal = ScorePlayer.ToString();
        
        kq = Instantiate(Canvas_Eff);
        if (scoreComputerFinal.Length >=2)
        {
            scoreComputerFinal = scoreComputerFinal.Substring(1, 1);            
        }
        if (scorePlayerFinal.Length >= 2)
        {
            scorePlayerFinal = scorePlayerFinal.Substring(1, 1);
        }

        if(Int32.Parse(scoreComputerFinal) >Int32.Parse(scorePlayerFinal))
        {            
            Gold -= (int)sliderMoney.value;
           
            kq.transform.GetChild(0).GetComponent<Text>().text = "- " + (int)sliderMoney.value;
            

        }
        else if (Int32.Parse(scoreComputerFinal) < Int32.Parse(scorePlayerFinal))
        {
            Gold += (int)sliderMoney.value;
            
            kq.transform.GetChild(0).GetComponent<Text>().text = "+ " + (int)sliderMoney.value;
            
        }
        else if (Int32.Parse(scoreComputerFinal) == Int32.Parse(scorePlayerFinal))
        {
            kq.transform.GetChild(0).GetComponent<Text>().text = "+ 0";
        }
        Destroy(kq, 2);
        Debug.Log(kq);

        GameObject effScore = Instantiate(EffScore);
        effScore.transform.GetChild(0).GetComponent<Text>().text = "" + scoreComputerFinal;
        effScore.transform.GetChild(1).GetComponent<Text>().text = "" + scorePlayerFinal;

        Destroy(effScore, 3f);

    }

    

    public void ClickReset()
    {
        
        Card1.GetComponent<SpriteRenderer>().sprite = null;
        Card2.GetComponent<SpriteRenderer>().sprite = null;
        Card3.GetComponent<SpriteRenderer>().sprite = null;

        ComputerCard1.GetComponent<SpriteRenderer>().sprite = null;
        ComputerCard2.GetComponent<SpriteRenderer>().sprite = null;
        ComputerCard3.GetComponent<SpriteRenderer>().sprite = null;

        btnHaBai.SetActive(false);
        btnContinue.SetActive(false);
        btnChiaBai.SetActive(true);

        DemGiaTri = 0;
        CardController cc1 = Card1.GetComponent<CardController>();
        Destroy(cc1);
        CardController cc2 = Card2.GetComponent<CardController>();
        Destroy(cc2);
        CardController cc3 = Card3.GetComponent<CardController>();
        Destroy(cc3);


        Card1.AddComponent<CardController>();
        Card2.AddComponent<CardController>();
        Card3.AddComponent<CardController>();

    }

    public void GameOver_Reset()
    {
        
        SceneManager.LoadScene("GamePlay");
        ClickReset();
        GameOver.SetActive(false);
    }

    public void ClickExit()
    {
        Application.Quit();
    }

    
}
