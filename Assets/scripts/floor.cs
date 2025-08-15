using UnityEngine;

public class floor : MonoBehaviour
{   
    [SerializeField] public float moveSpeed = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, moveSpeed * Time.deltaTime, 0); // 每次更新都會移動 Time.deltaTime:多久時間 跑一次迴圈
        if (transform.position.y > 6f) // 當物件位置超過y軸6f時
        {
            Destroy(gameObject); // 銷毀物件
            transform.parent.GetComponent<floormanager>().SpawnFloor(); // 呼叫FloorManager的SpawnFloor方法生成新的地板
        }
    }
}
