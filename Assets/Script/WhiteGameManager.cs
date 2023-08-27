
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WhiteGameManager : MonoBehaviour
{
    //�ŏ��̉摜
    public Image DefaultImage;

    //�؂�ւ���摜�̔z��
    public Sprite[] GameImages;

    //�Q�[���I�[�o�[�p�l��
    public GameObject gameover;

    //�J�n�̃J�E���g����
    public GameObject countDownText;

    //�S�̃{�^��
    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Button Button4;

    //���v�̃X�R�A�J�E���g
    public static int scoreCountAll;

    //�X�R�A�\���e�L�X�g
    public Text scoreText;

    //�������Ԃ̃X���C�_�[
    public Slider timeSlider;

    //�X���C�_�[�̎���
    public float time;
    
    //HP�̕ϐ�
    private int hp;

    //�z���C�g�V�[���̃J�E���g
    private int Whitecount;

    //�V�[���������_���ɐ؂�ւ��邽�߂̕ϐ�
    private int randomscore;

    //�O��̉摜��\���ϐ� - 0�����Ő錾����(Array.IndexOf���\�b�h���g�����߁j
    private int previousImageIndex = -1; 

    private int currentImageIndex = -1;

    /*
     * �摜��\������BonClickButton()�����삷��x��Setup()���\�b�h�͌Ăяo����Ă���B
     */

    private void Setup(int imageIndex)
    {
        //GameImages�z��ɂ�imageIndex���i�[
        DefaultImage.sprite = GameImages[imageIndex];

        //�O��\�������摜�̃C���f�b�N�X���X�V����
        previousImageIndex = imageIndex;
    }

    /*
     * �ŏ��̃J�E���g�_�E������Q�[���X�^�[�g�̏u�Ԃ܂ł̃��\�b�h
     * start���ɌĂяo���Ă���B
     */
    private IEnumerator CountDown()
    {
        //�V�[���X�^�[�g���̃J�E���g�_�E��
        countDownText.SetActive(true);
        int countDownTime = 2;
        while (countDownTime > 0)
        {
            //�J�E���g�_�E���e�L�X�g�ɃJ�E���g�_�E������
            countDownText.GetComponent<Text>().text = countDownTime.ToString();

            //1�b�ԃR���[�`���̎��s��҂�
            yield return new WaitForSeconds(1);
            countDownTime--;
        }

        //�J�E���g�_�E�����I�������Q�[�����ł����Ԃɂ���
        countDownText.SetActive(false);
        Button1.interactable = true;
        Button2.interactable = true;
        Button3.interactable = true;
        Button4.interactable = true;

        //�{�^������񉟂��ꂽ���Ƃɂ���B
        Button1.onClick.Invoke();

        //���Ԑ����̃^�C���X���C�_�[���A�N�e�B�u��
        timeSlider.gameObject.SetActive(true);
       
    }

   /*
    * �S�F�̃{�^���������ꂽ�Ƃ��̃��\�b�h
    * @ �ǂ̃{�^���������ꂽ���̏���GameIndices�Ŏ󂯎���Ă���B
    */

    public void OnClickButton(int[] GameIndices)
    {
        //������\�����邽�߂̕ϐ���錾
        int imageIndex;
        //�X���C�_�[�̎��Ԃ̕�����1.0f
        time = 1.0f;

        //�����_����imageIndex�ɒl������
        do
        {
            imageIndex = Random.Range(0, GameImages.Length);
        }
        //imageIndex�̒��Ƀ����_���őO��Ɠ��������������Ă��܂����ꍇ�ɂ�����xdo�̒��̏���������
        while (imageIndex == previousImageIndex); 

        //�O��ƈႤ��������������A���̐�����Setup()�ɓn���B
        Setup(imageIndex);

        // �I�����ꂽ�摜���{�^���ɕR�Â���ꂽ�摜�ƈ�v����ꍇ�͐����BcurrentImageIndex�̓Q�[���J�n����OnClickButton�ő�������B
        if (System.Array.IndexOf(GameIndices, currentImageIndex) != -1)
        {

            //�����̎�
            Debug.Log("����");
            //���[�J���ƃO���[�o���̃J�E���g���ꂼ��++;
            Whitecount++;
            scoreCountAll++;

            //GameOverPanel�̃e�L�X�g�ɃO���[�o���J�E���g��\��
            scoreText.text = "Score : " + scoreCountAll;

            if (Whitecount == randomscore)
            {
                SceneManager.LoadScene("Play_black");
            }
            
        }
        else
        {
            //�s�����̎�
            Debug.Log("�s����");

            //hp��0�ɂ���
            hp--;

            //hp��0�̎��A�Q�[���I�[�o�[�ɂ���
            if (hp ==0)
            {
                timeSlider.gameObject.SetActive(false);
                gameover.gameObject.SetActive(true);
               
            }
        }
       //���ݏo�Ă���I�u�W�F�N�g��currentImageIndex�ɑ��
        currentImageIndex = imageIndex;
       
    }

    /*
     * �^�C���X���C�_�[�p�̃��\�b�h
     * �X�^�[�g����CountDown���\�b�h���Ăяo����CountDown2�������ŌĂяo��
     */
    private IEnumerator CountDown2()
    {
        //float�^��time�ϐ���������-=��������B�ŏ���1.0�Ő錾���Ă���B
        while (true)
        {

            yield return null;
            time -= Time.deltaTime;

            //���Ԃ�-0.1f�ȉ��ɂȂ�����Q�[���I�[�o�[�ɂ���
            if (time <= -0.1f)
            {
                gameover.SetActive(true);
                timeSlider.gameObject.SetActive(false);
                yield break;
            }
        }
    }

    /*
     * �Q�[���X�^�[�g�܂łQ�b�҂��Ă���CountDown�Q���\�b�h���Ăяo��
     */
    private IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(2);

        StartCoroutine(CountDown2());
    }
    

    void Start()
    {
       

        // �{�^����OnClickButton���\�b�h��R�Â���
        Button1.onClick.AddListener(() => OnClickButton(new int[] { 0, 1, 2 }));
        Button2.onClick.AddListener(() => OnClickButton(new int[] { 3, 4, 5}));
        Button3.onClick.AddListener(() => OnClickButton(new int[] { 6, 7, 8 }));
        Button4.onClick.AddListener(() => OnClickButton(new int[] { 9, 10, 11 }));

        //hp�̏����l���Q
        hp = 2;

        //���݂̃V�[���̏����l���O
        Whitecount = 0;

        //�����_���V�[���؂�ւ��ϐ��̒l
        randomscore = Random.Range(5, 10);

        //�z���C�g�V�[���ɂȂ�����Ăяo����郁�\�b�h
        StartCoroutine(CountDown());
        StartCoroutine(StartCountdown());

        //�X�R�A�e�L�X�g��
        scoreText.text = "Score�F" + scoreCountAll;
        
    }
    void Update()
    {
        //timeSlider��time����ꑱ����
        timeSlider.value = time;

    }
        
}
