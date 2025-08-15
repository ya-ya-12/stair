using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f; // 移動速度
    GameObject currentFloor; // 當前地板
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() //只會輸出一次
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    //會一直更新 直到遊戲結束或物件被銷毀
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) // 按下右
        {
            transform.Translate(moveSpeed * Time.fixedDeltaTime, 0,0); // 每次更新都會移動 Time.deltaTime:多久時間 跑一次迴圈
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) // 按下左
        {
            transform.Translate(-moveSpeed * Time.fixedDeltaTime, 0, 0);
        }
    }
    void OnCollisionEnter2D(Collision2D other)  //實際碰撞
    {
        if (other.gameObject.tag == "normal")
        {
            if(other.contacts[0].normal == new Vector2(0f,1f)) // 如果碰撞的法線方向是向上的
            {
                Debug.Log("碰到地板"); // 當碰到 會輸出
                currentFloor = other.gameObject; // 紀錄當前地板 
            }
        }
        else if (other.gameObject.tag == "nail")
        {
            if(other.contacts[0].normal == new Vector2(0f,1f)) // 如果碰撞的法線方向是向上的
            {
                Debug.Log("碰到尖刺"); // 當碰到 會輸出
                currentFloor = other.gameObject; // 紀錄當前地板 
            }
        }
        else if (other.gameObject.tag == "ceiling")
        {
            Debug.Log("撞到天花板");
            currentFloor.GetComponent<BoxCollider2D>().enabled = false; // 禁用當前地板的碰撞器
        }
    }
    void OnTriggerEnter2D(Collider2D other)   //進入觸發區域
    {
        if (other.gameObject.tag == "deathline")
        {
            Debug.Log("你輸了"); // 當觸發 會輸出
        }
    }
}
