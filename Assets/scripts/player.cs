using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f; // 移動速度
    GameObject currentFloor; // 當前地板
    [SerializeField] int Hp; // 玩家生命值
    [SerializeField] GameObject hpbar; // 玩家血量
    [SerializeField] private TextMeshProUGUI scoretext;
    int score; // 玩家分數
    float scoreTimer; // 計時器
    Animator anim; // 動畫控制器
    SpriteRenderer render; // 精靈渲染器
    AudioSource deathsound; // 音效來源
    [SerializeField] GameObject replaybutton; // 按鈕
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() //只會輸出一次
    {
        Hp = 10;
        score = 0;
        scoreTimer = 0f;
        anim = GetComponent<Animator>(); // 獲取動畫控制器
        render = GetComponent<SpriteRenderer>(); // 獲取精靈渲染器
        deathsound = GetComponent<AudioSource>(); // 獲取音效來源
    }

    // Update is called once per frame
    void FixedUpdate()
    //會一直更新 直到遊戲結束或物件被銷毀
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) // 按下右
        {
            transform.Translate(moveSpeed * Time.fixedDeltaTime, 0, 0); // 每次更新都會移動 Time.deltaTime:多久時間 跑一次迴圈
            render.flipX = false; // 右頭
            anim.SetBool("run", true); // 設定動畫為跑步
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) // 按下左
        {
            transform.Translate(-moveSpeed * Time.fixedDeltaTime, 0, 0);
            render.flipX = true; // 左頭
            anim.SetBool("run", true); // 設定動畫為跑步
        }
        else
        {
            anim.SetBool("run", false); // 停止跑步動畫
        }
        Updatescore(); // 更新分數
    }
    void OnCollisionEnter2D(Collision2D other)  //實際碰撞
    {
        if (other.gameObject.tag == "normal")
        {
            if (other.contacts[0].normal == new Vector2(0f, 1f)) // 如果碰撞的法線方向是向上的
            {
                currentFloor = other.gameObject; // 紀錄當前地板 
                ModifyHp(1); // 修改玩家的生命值
                other.gameObject.GetComponent<AudioSource>().Play(); // 播放音效
            }
        }
        else if (other.gameObject.tag == "nail")
        {
            if (other.contacts[0].normal == new Vector2(0f, 1f)) // 如果碰撞的法線方向是向上的
            {
                currentFloor = other.gameObject; // 紀錄當前地板 
                ModifyHp(-3);
                anim.SetTrigger("hurt"); // 受傷動畫
                other.gameObject.GetComponent<AudioSource>().Play(); // 播放音效
            }
        }
        else if (other.gameObject.tag == "ceiling")
        {
            currentFloor.GetComponent<BoxCollider2D>().enabled = false; // 禁用當前地板的碰撞器
            ModifyHp(-3);
            anim.SetTrigger("hurt"); // 受傷動畫
            other.gameObject.GetComponent<AudioSource>().Play(); // 播放音效
        }
    }
    void OnTriggerEnter2D(Collider2D other)   //進入觸發區域
    {
        if (other.gameObject.tag == "deathline")
        {
            die();
        }
    }
    void ModifyHp(int num)
    {
        Hp += num;
        if (Hp > 10)
        {
            Hp = 10; // 生命值上限
        }
        else if (Hp < 0)
        {
            Hp = 0;
            die();
        }
        Updatehpbar(); // 更新血量條
    }
    void Updatehpbar()
    {
        for (int i = 0; i < hpbar.transform.childCount; i++)
        {
            if (i < Hp)
            {
                hpbar.transform.GetChild(i).gameObject.SetActive(true); // 啟用血量條
            }
            else
            {
                hpbar.transform.GetChild(i).gameObject.SetActive(false); // 禁用血量條
            }
        }
    }
    void Updatescore()
    {
        scoreTimer += Time.deltaTime; // 計時器增加
        if (scoreTimer >= 2f) // 每秒增加一次分數
        {
            score++;
            scoreTimer = 0f; // 重置計時器
            scoretext.text = "地下" + score.ToString() + "層"; // 更新分數顯示
        }
    }
    void die()
    {
        deathsound.Play(); // 播放死亡
        Time.timeScale = 0f; // 停止
        replaybutton.SetActive(true); // 顯示按鈕
    }
    public void replay()
    {
        Time.timeScale = 1f; // 恢復時間
        SceneManager.LoadScene("SampleScene"); // 重新載入當前場景

    }
}
