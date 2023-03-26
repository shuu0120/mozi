
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterImage_Manager : MonoBehaviour
{
    public Image LetterImage;
    public Sprite[] LetterImages;

    private int previousImageIndex = -1; // 初期値は-1で前回の画像がないことを示す

    private void Setup(int imageIndex)
    {
        LetterImage.sprite = LetterImages[imageIndex];
        previousImageIndex = imageIndex; // 前回表示した画像のインデックスを更新する
    }

    public void OnClickButton(int[] imageIndices)
    {
        int imageIndex;
        do
        {
            imageIndex = Random.Range(0, LetterImages.Length);
        } while (imageIndex == previousImageIndex); // 前回の画像と異なる画像を選択する

        Setup(imageIndex);

        // 選択された画像がボタンに紐づけられた画像と一致する場合、デバックログに「正解」と表示する
        if (System.Array.IndexOf(imageIndices, imageIndex) != -1)
        {
            Debug.Log("正解");
        }
        else
        {
            Debug.Log("不正解");
        }


    }

    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Button Button4;

    void Start()
    {
        // ボタンにOnClickButtonメソッドを紐づける
        Button1.onClick.AddListener(() => OnClickButton(new int[] { 0, 1, 2, 3 }));
        Button2.onClick.AddListener(() => OnClickButton(new int[] { 4, 5, 6, 7 }));
        Button3.onClick.AddListener(() => OnClickButton(new int[] { 8, 9, 10, 11 }));
        Button4.onClick.AddListener(() => OnClickButton(new int[] { 12, 13, 14, 15 }));
    }
}
//    public Image LetterImage;
//    public Sprite[] LetterImages;

//    private int previousImageIndex = -1; // 初期値は-1で前回の画像がないことを示す

//    private void Setup()
//    {
//        int imageIndex;
//        do
//        {
//            imageIndex = Random.Range(0, LetterImages.Length);
//        } while (imageIndex == previousImageIndex); // 前回の画像と異なる画像を選択する

//        LetterImage.sprite = LetterImages[imageIndex];
//        previousImageIndex = imageIndex; // 前回表示した画像のインデックスを保存する
//    }

//    public void OnClickButton()
//    {
//        Setup();
//}

// Start is called before the first frame update
/*void Start()
{

}

// Update is called once per frame
void Update()
{

}*/
//}