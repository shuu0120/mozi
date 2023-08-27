
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WhiteGameManager : MonoBehaviour
{
    //最初の画像
    public Image DefaultImage;

    //切り替える画像の配列
    public Sprite[] GameImages;

    //ゲームオーバーパネル
    public GameObject gameover;

    //開始のカウント時間
    public GameObject countDownText;

    //４つのボタン
    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Button Button4;

    //合計のスコアカウント
    public static int scoreCountAll;

    //スコア表示テキスト
    public Text scoreText;

    //制限時間のスライダー
    public Slider timeSlider;

    //スライダーの時間
    public float time;
    
    //HPの変数
    private int hp;

    //ホワイトシーンのカウント
    private int Whitecount;

    //シーンをランダムに切り替えるための変数
    private int randomscore;

    //前回の画像を表す変数 - 0未満で宣言する(Array.IndexOfメソッドを使うため）
    private int previousImageIndex = -1; 

    private int currentImageIndex = -1;

    /*
     * 画像を表示する。onClickButton()が動作する度にSetup()メソッドは呼び出されている。
     */

    private void Setup(int imageIndex)
    {
        //GameImages配列ににimageIndexを格納
        DefaultImage.sprite = GameImages[imageIndex];

        //前回表示した画像のインデックスを更新する
        previousImageIndex = imageIndex;
    }

    /*
     * 最初のカウントダウンからゲームスタートの瞬間までのメソッド
     * start時に呼び出している。
     */
    private IEnumerator CountDown()
    {
        //シーンスタート時のカウントダウン
        countDownText.SetActive(true);
        int countDownTime = 2;
        while (countDownTime > 0)
        {
            //カウントダウンテキストにカウントダウンを代入
            countDownText.GetComponent<Text>().text = countDownTime.ToString();

            //1秒間コルーチンの実行を待つ
            yield return new WaitForSeconds(1);
            countDownTime--;
        }

        //カウントダウンが終わったらゲームができる状態にする
        countDownText.SetActive(false);
        Button1.interactable = true;
        Button2.interactable = true;
        Button3.interactable = true;
        Button4.interactable = true;

        //ボタンが一回押されたことにする。
        Button1.onClick.Invoke();

        //時間制限のタイムスライダーをアクティブに
        timeSlider.gameObject.SetActive(true);
       
    }

   /*
    * ４色のボタンが押されたときのメソッド
    * @ どのボタンが押されたかの情報をGameIndicesで受け取っている。
    */

    public void OnClickButton(int[] GameIndices)
    {
        //文字を表示するための変数を宣言
        int imageIndex;
        //スライダーの時間の部分を1.0f
        time = 1.0f;

        //ランダムでimageIndexに値を入れる
        do
        {
            imageIndex = Random.Range(0, GameImages.Length);
        }
        //imageIndexの中にランダムで前回と同じ数字が入ってしまった場合にもう一度doの中の処理をする
        while (imageIndex == previousImageIndex); 

        //前回と違う数字が入ったら、その数字をSetup()に渡す。
        Setup(imageIndex);

        // 選択された画像がボタンに紐づけられた画像と一致する場合は正解。currentImageIndexはゲーム開始時のOnClickButtonで代入される。
        if (System.Array.IndexOf(GameIndices, currentImageIndex) != -1)
        {

            //正解の時
            Debug.Log("正解");
            //ローカルとグローバルのカウントそれぞれ++;
            Whitecount++;
            scoreCountAll++;

            //GameOverPanelのテキストにグローバルカウントを表示
            scoreText.text = "Score : " + scoreCountAll;

            if (Whitecount == randomscore)
            {
                SceneManager.LoadScene("Play_black");
            }
            
        }
        else
        {
            //不正解の時
            Debug.Log("不正解");

            //hpを0にする
            hp--;

            //hpが0の時、ゲームオーバーにする
            if (hp ==0)
            {
                timeSlider.gameObject.SetActive(false);
                gameover.gameObject.SetActive(true);
               
            }
        }
       //現在出ているオブジェクトをcurrentImageIndexに代入
        currentImageIndex = imageIndex;
       
    }

    /*
     * タイムスライダー用のメソッド
     * スタート時にCountDownメソッドを呼び出してCountDown2をそこで呼び出す
     */
    private IEnumerator CountDown2()
    {
        //float型のtime変数をずっと-=し続ける。最初に1.0で宣言している。
        while (true)
        {

            yield return null;
            time -= Time.deltaTime;

            //時間が-0.1f以下になったらゲームオーバーにする
            if (time <= -0.1f)
            {
                gameover.SetActive(true);
                timeSlider.gameObject.SetActive(false);
                yield break;
            }
        }
    }

    /*
     * ゲームスタートまで２秒待ってからCountDown２メソッドを呼び出す
     */
    private IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(2);

        StartCoroutine(CountDown2());
    }
    

    void Start()
    {
       

        // ボタンにOnClickButtonメソッドを紐づける
        Button1.onClick.AddListener(() => OnClickButton(new int[] { 0, 1, 2 }));
        Button2.onClick.AddListener(() => OnClickButton(new int[] { 3, 4, 5}));
        Button3.onClick.AddListener(() => OnClickButton(new int[] { 6, 7, 8 }));
        Button4.onClick.AddListener(() => OnClickButton(new int[] { 9, 10, 11 }));

        //hpの初期値を２
        hp = 2;

        //現在のシーンの初期値を０
        Whitecount = 0;

        //ランダムシーン切り替え変数の値
        randomscore = Random.Range(5, 10);

        //ホワイトシーンになったら呼び出されるメソッド
        StartCoroutine(CountDown());
        StartCoroutine(StartCountdown());

        //スコアテキストに
        scoreText.text = "Score：" + scoreCountAll;
        
    }
    void Update()
    {
        //timeSliderにtimeを入れ続ける
        timeSlider.value = time;

    }
        
}
