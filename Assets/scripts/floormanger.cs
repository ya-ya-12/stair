using UnityEngine;

public class floormanager : MonoBehaviour
{
    [SerializeField] GameObject[] floorPrefabS; // 地板預製件
    public void SpawnFloor()
    {
        int r = Random.Range(0, floorPrefabS.Length); // 隨機選擇地板
        GameObject floor = Instantiate(floorPrefabS[r], transform);  // 在當前物件位置生成地板
        floor.transform.localPosition = new Vector3(Random.Range(-1f, 6f),-6f,0f); // 設定生成地板的位置

    }
}
