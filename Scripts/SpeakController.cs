using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SpeakController : MonoBehaviour
{
    public Canvas textBoxCanvas;

    public GameObject chapterNumber;

    private GameObject king;

    public MCMovement mc;
    public mcOOP mcoop;
    private AIBattle aiBattle;
    private GameController gc;
    public BossFight bf;
    public GameObject trainigMirror;


    public Image mcTextBox;
    public Image mobTextBox;
    public Image bossTextBox;
    public Image catTXTbox;

    public Sprite mctextBox;
    public Sprite ch1mob1;
    public Sprite ch1mob2;
    public Sprite ch1mob3;
    public Sprite ch1boss;

    public Sprite ch2mob1;
    public Sprite ch2mob2;
    public Sprite ch2mob3;
    public Sprite ch2boss;

    public Sprite ch3mob1;
    public Sprite ch3mob2;
    public Sprite ch3mob3;
    public Sprite ch3boss;

    private int phase;
    private int speakID;
    private int doorComb;

    private void Awake()
    {
        aiBattle = GameObject.Find("BattleMode").GetComponent<AIBattle>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }
    private void Start()
    {
        speakID = 0;
        mcTextBox.sprite = mctextBox;
        doorComb = 0;
    }
    private void Update()
    {
        if (gc.gamePaused)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            phase += 1;
            switch (speakID)
            {
                case 0:
                    break;
                case 1:
                    doorTo1B();
                    break;
                case 2:
                    stairToRoof();
                    break;
                case 3:
                    doorTo3B();
                    break;
                case 4:
                    afterBattleSpeakMCWON();
                    break;
                case 5:
                    afterEnWon();
                    break;
                case 6:
                    afterAlreadyWon();
                    break;
                case 7:
                    firstSchool();
                    break;
                case 8:
                    enoghToGetInClass();
                    break;
                case 9:
                    conversationWithTeacher();
                    break;
                case 10:
                    conversationAfterWinAgainstTeacher();
                    break;
                case 11:
                    banditBossConversationfirst();
                    break;
                case 12:
                    battleStartwithBandit();
                    break;
                case 13:
                    battleWonAgainstBandit();
                    break;
                case 14:
                    battleloseAgainstBandit();
                    break;
                case 15:
                    portreSpeak();
                    break;
                case 16:
                    kingSpeech(king);
                    break;
                case 17:
                    kingManupulationTas();
                    break;
                case 18:
                    kingManupulationKagt();
                    break;
                case 19:
                    kingManupulationMakas();
                    break;
                case 20:
                    kingWon();
                    break;
                case 21:
                    kingLost();
                    break;
                case 22:
                    training();
                    break;
                case 23:
                    afterTrainingWin();
                    break;
                case 24:
                    afterTrainingLost();
                    break;
                case 25:
                    hubStarter();
                    break;
                case 26:
                    schoolStarter();
                    break;
                case 27:
                    streetStarter();
                    break;
                case 28:
                    palaceStarter();
                    break;
            }
        }
    }
    public void speakEnd()
    {
        speakID = 0;
        mc.movable = true;
        mc.speakModeOpenned = false;
        aiBattle.setBattleMode(false);
        mcTextBox.gameObject.SetActive(false);
        mobTextBox.gameObject.SetActive(false);
        bossTextBox.gameObject.SetActive(false);
        catTXTbox.gameObject.SetActive(false);
    }
    public void speakHimself()
    {
        phase = 0;
        mc.movable = false;
        mc.interactable = false;
        mc.speakModeOpenned = true;
        mcTextBox.gameObject.SetActive(true);
        return;
    }
    public void doorTo1B()
    {
        speakID = 1;
        doorComb += 1;
        if(phase == 0)
        {
            mcTextBox.GetComponentInChildren<Text>().text = "Burasi 1/B sinifi";
        }
        else if (phase == 1)
        {
            mcTextBox.GetComponentInChildren<Text>().text = "Benim sinifim deðil";
        }
        else if(phase == 2 && doorComb > 2)
        {
            mcTextBox.GetComponentInChildren<Text>().text = "Acaba neden okulumda sadece B sýnýf var";
        }
        else if(phase == 3 && doorComb > 2)
        {
            mcTextBox.GetComponentInChildren<Text>().text = "Neyse ben en iyisi olmaya bakmaliyim";
        }
        else
        {
            speakEnd();
        }
    }
    public void stairToRoof()
    {
        speakID = 2;
        if (phase == 0)
        {
            mcTextBox.GetComponentInChildren<Text>().text = "Burasi catiya cikiyor";
        }
        else if (phase == 1)
        {
            mcTextBox.GetComponentInChildren<Text>().text = "Benim burada isim yok";
        }
        else
        {
            speakEnd();
        }
    }
    public void doorTo3B()
    {
        speakID = 3;
        doorComb += 1;
        if (phase == 0)
        {
            mcTextBox.GetComponentInChildren<Text>().text = "Burasi 3/B sinifi";
        }
        else if (phase == 1)
        {
            mcTextBox.GetComponentInChildren<Text>().text = "Benim sinifim deðil";
        }
        else if (phase == 2 && doorComb == 2)
        {
            mcTextBox.GetComponentInChildren<Text>().text = "Acaba neden okulumda sadece B sýnýf var";
        }
        else if (phase == 3 && doorComb == 2)
        {
            mcTextBox.GetComponentInChildren<Text>().text = "Neyse ben en iyisi olmaya bakmaliyim";
        }
        else
        {
            speakEnd();
        }
    }
    public void speakBetweentTwo(GameObject opposite)
    {
        phase = 0;
        mc.movable = false;
        mc.interactable = false;
        mc.speakModeOpenned = true;
        mobTextBox.gameObject.SetActive(true);
        mcTextBox.gameObject.SetActive(true);
        switch (opposite.GetComponent<Enemy>().enID)
        {
            case 0:
                mobTextBox.sprite = ch1mob1;
                break;
            case 1:
                mobTextBox.sprite = ch1mob2;
                break;
            case 2:
                mobTextBox.sprite = ch1mob3;
                break;
            case 3:
                mobTextBox.sprite = ch2mob1;
                break;
            case 4:
                mobTextBox.sprite = ch2mob2;
                break;
            case 5:
                mobTextBox.sprite = ch2mob3;
                break;
            case 6:
                mobTextBox.sprite = ch3mob1;
                break;
            case 7:
                mobTextBox.sprite = ch3mob2;
                break;
            case 8:
                mobTextBox.sprite = ch3mob3;
                break;


        }
    }
        public void afterBattleWithMob(GameObject opposite)
    {
        phase = 0;
        mc.movable = false;
        mc.interactable = false;
        mc.speakModeOpenned = true;
        mobTextBox.gameObject.SetActive(true);
        switch (opposite.GetComponent<Enemy>().enID)
        {
            case 0:
                mobTextBox.sprite = ch1mob1;
                break;
            case 1:
                mobTextBox.sprite = ch1mob2;
                break;
            case 2:
                mobTextBox.sprite = ch1mob3;
                break;
            case 3:
                mobTextBox.sprite = ch2mob1;
                break;
            case 4:
                mobTextBox.sprite = ch2mob2;
                break;
            case 5:
                mobTextBox.sprite = ch2mob3;
                break;
            case 6:
                mobTextBox.sprite = ch3mob1;
                break;
            case 7:
                mobTextBox.sprite = ch3mob2;
                break;
            case 8:
                mobTextBox.sprite = ch3mob3;
                break;


        }
    }
    public void afterBattleSpeakMCWON()
    {
        speakID = 4;
        if (phase == 0)
        {
            int a = Random.Range(1, 4);
            switch (a)
            {
                case 1:
                    mobTextBox.GetComponentInChildren<Text>().text = "Tuh kaybettim";
                    break;
                case 2:
                    mobTextBox.GetComponentInChildren<Text>().text = "Iyi oyundu";
                    break;
                case 3:
                    mobTextBox.GetComponentInChildren<Text>().text = "Iste buna kaza derim";
                    break;

            }
        }
        else
        {
            speakEnd();
        }

    }
    public void afterEnWon()
    {
        speakID = 5;
        if (phase == 0)
        {
            int a = Random.Range(1, 4);
            switch (a)
            {
                case 1:
                    mobTextBox.GetComponentInChildren<Text>().text = "Tekrar Dene!!";
                    break;
                case 2:
                    mobTextBox.GetComponentInChildren<Text>().text = "Iyi oyundu";
                    break;
                case 3:
                    mobTextBox.GetComponentInChildren<Text>().text = "Iste adami boyle bozarlar";
                    break;
            }
        }
        else
        {
            speakEnd();
        }

    }
    public void afterAlreadyWon()
    {
        speakID = 6;
        if (phase == 0)
        {
            mobTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            mobTextBox.GetComponentInChildren<Text>().text = "Yeter artik vurma kafama";
        }
        else if(phase == 1)
        {
            mobTextBox.gameObject.SetActive(false);
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "Aqla";
        }
        else
        {
            speakEnd();
            mc.resetTarget();
        }
    }
    public void firstSchool()
    {
        speakID = 7;
        if (phase == 0)
        {
            mcTextBox.GetComponentInChildren<Text>().text = "Ah! Okul gercekten ozlemisim";
        }
        else if(phase == 1)
        {
            mcTextBox.GetComponentInChildren<Text>().text = "En azýndan tas kagit makasla calisiyor";
        }
        else
        {
            speakEnd();
        }
    }
    public void enoghToGetInClass()
    {
        speakID = 8;
        if (phase == 0)
        {
            if (chapterNumber.name == "1")
            {
                mcTextBox.GetComponentInChildren<Text>().text = "Bu günkü ödevi yapmadan giremem";
            }
            else if(chapterNumber.name == "2")
            {
                mcTextBox.GetComponentInChildren<Text>().text = "Buradaki insanlar bana ters bakýyor\nIlk once onlari hallettmem lazim";
            }
            else if (chapterNumber.name == "3")
            {
                mcTextBox.GetComponentInChildren<Text>().text = "Kralin yanina kolayca girebileceðimden supheliyim";
            }
        }
        else if (phase == 1)
        {
            if (chapterNumber.name == "3")
            {
                mcTextBox.GetComponentInChildren<Text>().text = (7 - mcoop.wonNumber).ToString() + " kisi daha yenmem gerek";
            }
            else
            {
                mcTextBox.GetComponentInChildren<Text>().text = (5 - mcoop.wonNumber).ToString() + " kisi daha yenmem gerek";
            }
        }
        else
        {
            speakEnd();
        }
    }
    public void preparetionToTeacherBoss()
    {
        phase = 0;
        mc.movable = false;
        mc.interactable = false;
        mc.speakModeOpenned = true;
        bossTextBox.sprite = ch1boss;
        mcTextBox.gameObject.SetActive(true);
        bossTextBox.gameObject.SetActive(true);
    }
    public void conversationWithTeacher()
    {
        speakID = 9;
        if(phase == 0)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text= "Vay vay vay kimleri goruyorum";
        }
        else if(phase == 1)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Mert Can Bey nerelerdeydiniz";
        }
        else if (phase == 2)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Yolunuzu gozler olduk";
        }
        else if (phase == 3)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Sinifa bu kadar gec gelmenizin onemli bir sebebi vardir diye duþunuyorum.";
        }
        else if (phase == 4)
        {
            bossTextBox.gameObject.SetActive(false);
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "Uyuyakalmýþým";
        }
        else if (phase == 5)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Ooo hic istifinizi bozmasaydýnýz";
        }
        else if (phase == 6)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Hatta direk evinize geri gidip devam edin guzellik uykunuza";
        }
        else if (phase == 7)
        {
            bossTextBox.gameObject.SetActive(false);
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "Ne demek istiyorsunuz hocam?";
        }
        else if (phase == 8)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Mert Can hemen simdi sinifi terk et ailenle gorusmek istedigimi soyle";
        }
        else if (phase == 9)
        {
            bossTextBox.gameObject.SetActive(false);
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "Bunu yapamam hocam sizi tas kagit makasa davet ediyorum!";
        }
        else if (phase == 10)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Ho Ho bana meydan okumaya curret mi ediyorsun";
        }
        else if (phase == 11)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "O zaman yaklas da aliyim boyunun olcusunu";
        }
        else
        {
            phase = 0;
            speakID = 0;
            bf.bossFightStart();
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.gameObject.SetActive(false);
        }
    }
    public void conversationAfterWinAgainstTeacher()
    {
        speakID = 10;
        if (phase == 0)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Kazanmana izin verdim aslinda";
        }
        else
        {
            gc.sceneChange(false);
        }
    }
    public void preparetionToBanditBoss()
    {
        phase = 0;
        mc.movable = false;
        mc.interactable = false;
        mc.speakModeOpenned = true;
        bossTextBox.sprite = ch2boss;
        mcTextBox.gameObject.SetActive(true);
        bossTextBox.gameObject.SetActive(true);
    }
    public void banditBossConversationfirst()
    {
        speakID = 11;
        if (phase == 0)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Adamlamlarýmý yere sermissin\nBu yüzden 5 tl vereceksin";
        }
        else if (phase == 1)
        {
            bossTextBox.gameObject.SetActive(false);
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "50 kurus degil miydi?";
        }
        else if (phase == 2)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Zam geldi birader,Sokul paralari";
        }
        else if (phase == 3)
        {
            bossTextBox.gameObject.SetActive(false);
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "Sana kuruþ koklatmam zaten param da yok. Seni taþ kaðýt makasa davet ediyorum!";
        }
        else if(phase == 4)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Ne!! tas kagit makas mi cocuk sen kiminle asik atmaya çalistiginin farkinda misin?\nBen serseri Usman";
        }
        else
        {
            speakEnd();
        }

    }
    public void battleStartwithBandit()
    {
        speakID = 12;
        if (phase == 0)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Gel bakalim sana dunyanin kac bucak oldugunu gostereyim";
        }
        else
        {
            phase = 0;
            speakID = 0;
            bf.bossFightStart();
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.gameObject.SetActive(false);
        }
    }
    public void battleWonAgainstBandit()
    {
        speakID = 13;
        if (phase == 0)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = " Beni yendin... Artýk çoplugun basi sensin";
        }
        else
        {
            gc.sceneChange(false);
        }
    }
    public void battleloseAgainstBandit()
    {
        speakID = 14;
        if (phase == 0)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Ha ha dersini aldýn galiba. bu yenilgi sana yeter diye düþünüyorum. Uza";
        }
        else
        {
            speakEnd();
        }
    }
    public void portreSpeak()
    {
        speakID = 15;
        if (phase == 0)
        {
            mcTextBox.GetComponentInChildren<Text>().text = "En azindan yakisikli birini bekliyordum";
        }
        else
        {
            speakEnd();
        }
    }
    public void initialKingSPEECH()
    {
        phase = 0;
        mc.movable = false;
        mc.interactable = false;
        mc.speakModeOpenned = true;
        bossTextBox.sprite = ch3boss;
        mcTextBox.gameObject.SetActive(true);
        bossTextBox.gameObject.SetActive(true);
    }
    public void kingSpeech(GameObject gm)
    {
        king = gm;
        speakID = 16;
        if (phase == 0)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Ajanlarim çok iyi bir taþ kaðýt makas ustasinin geleceginden haber vermiþti";
        }
        else if(phase == 1)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Goruyorum da haklilarmis";
        }
        else if(phase == 2)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Ben taskamas kralliginin krali Bob";
            gm.transform.localScale = new Vector3(gm.transform.localScale.x * -1, gm.transform.localScale.y, gm.transform.localScale.z);//if mc lost it will be reverted
        }
        else if (phase == 3)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Bu da kedim minnos";
        }
        else if(phase == 4)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Cok tatli degil mi ";
        }
        else if(phase == 5)
        {
            bossTextBox.gameObject.SetActive(false);
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "O kadarda tatli degil";
        }
        else if(phase == 6)
        {
            bossTextBox.gameObject.SetActive(false);
            mcTextBox.gameObject.SetActive(false);
            catTXTbox.gameObject.SetActive(true);
            catTXTbox.GetComponentInChildren<Text>().text = "Miyav";
        }
        else if(phase == 7)
        {
            catTXTbox.gameObject.SetActive(false);
            bossTextBox.gameObject.SetActive(false);
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "Cok tatli";
        }
        else if(phase == 8)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Tanistigimiza memnun oldum";
        }
        else if(phase == 9)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Ancak buraya gelme amanýcý biliyorum";
        }
        else if(phase == 10)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Lafi uzatmaya gerek yok baslayalim";
        }
        else
        {
            phase = 0;
            speakID = 0;
            bf.bossFightStart();
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.gameObject.SetActive(false);
        }
    }
    public void kingManupulationTas()
    {
        speakID = 17;
        if (phase == 0)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Emrediyorum!\n Tasi kullanamazsýn";
        }
        else
        {
            bf.battleMode = true;
            bf.setCBMCAction(true);
            bf.manupTas();
            phase = 0;
            speakID = 0;
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.gameObject.SetActive(false);
        }
    }
    public void kingManupulationKagt()
    {
        speakID = 18;
        if (phase == 0)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Emrediyorum!\n Kagiti kullanamazsýn";
        }
        else
        {
            bf.battleMode = true;
            bf.setCBMCAction(true);
            bf.manupKagt();
            phase = 0;
            speakID = 0;
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.gameObject.SetActive(false);
        }
    }
    public void kingManupulationMakas()
    {
        speakID = 19;
        if (phase == 0)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Emrediyorum!\n Makasi kullanamazsýn";
        }
        else
        {
            bf.battleMode = true;
            bf.setCBMCAction(true);
            bf.manupMakas();
            phase = 0;
            speakID = 0;
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.gameObject.SetActive(false);
        }
    }
    public void kingWon()
    {
        speakID = 20;
        if (phase == 0)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Bu...bu... imkansýz... nasýl yenilebilirim...";
        }
        else if(phase == 1)
        {
            bossTextBox.gameObject.SetActive(false);
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "Agla";
        }
        else if (phase == 2)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Ben kaybettim artýk kral sensin evlat";
        }
        else
        {
            gc.sceneChange(false);
        }
    }
    public void kingLost()
    {
        speakID = 21;
        if (phase == 0)
        {
            bossTextBox.gameObject.SetActive(true);
            mcTextBox.gameObject.SetActive(false);
            bossTextBox.GetComponentInChildren<Text>().text = "Olmasý gerektiði gibi";
        }
        else
        {
            speakEnd();
        }
    }
    public void training()
    {
        speakID = 22;
        if (phase == 0)
        {
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "Bosluk tusu ile konusmalarda ilerleyebilirsin gecebilirsin.";
        }
        else if (phase == 1)
        {
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "Z: Tas\nX: Kagit\nC: Makas";
        }
        else if(phase == 2)
        {
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "Her tik sesinde rakibin elini tahmin edebilecek";
        }
        else if (phase == 3)
        {
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "Bakalim bugun kendimde miyim?";
        }
        else
        {
            phase = 0;
            speakID = 0;
            mc.movable = false;
            mc.battleModeOpenned = true;
            mcTextBox.gameObject.SetActive(false);
            aiBattle.startBattle(trainigMirror);
        }
    }
    public void afterTrainingWin()
    {
        speakID = 23;
        if (phase == 0)
        {
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "Kendini gecmek bu olsa gerek.";
        }
        else
        {
            speakEnd();

        }
    }
    public void afterTrainingLost()
    {
        speakID = 24;
        if (phase == 0)
        {
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "Hmm...";
        }
        else
        {
            speakEnd();

        }
    }
    public void hubStarter()
    {
        speakID = 25;
        if (phase == 0)
        {
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "Bugün tas kagit makas oynamak için harika bir gün";
        }
        else
        {
            speakEnd();
        }
    }
    public void schoolStarter()
    {
        speakID = 26;
        if (phase == 0)
        {
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "Okul...!";
        }
        else if(phase == 1)
        {
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "Okul birþeyler öðrettiklerini ve birþeyler öðrendiklerini sanan insanlarla dolu";
        }
        else if(phase == 2)
        {
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "Hmm...Sinirlerimi yatýþtýrmam gerek";
        }
        else if (phase == 3)
        {
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "En iyisi biraz öðrenci tokatlayayým";
        }
        else
        {
            speakEnd();
        }
    }
    public void streetStarter()
    {
        speakID = 27;
        if (phase == 0)
        {
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "Burdaki insanlar bana dik dik bakýyor";
        }
        else if (phase == 1)
        {
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "Kimse bana dik dik bakamaz";
        }
        else if(phase == 2)
        {
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "Bunlarý bi yamuklaþtýralým";
        }
        else
        {
            speakEnd();
        }
    }
    public void palaceStarter()
    {
        speakID = 28;
        if (phase == 0)
        {
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "Demek Taskamas'ýn kralý burada yaþýyor";
        }
        else if (phase == 1)
        {
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "Zevksizmiþ..";
        }
        else if (phase == 2)
        {
            mcTextBox.gameObject.SetActive(true);
            mcTextBox.GetComponentInChildren<Text>().text = "Krala ulaþmak istiyorsam buralarý temizlemem gerekecek";
        }
        else
        {
            speakEnd();
        }
    }
}