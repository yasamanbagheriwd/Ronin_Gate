using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


[System.Serializable]
public class angel_Holder
{
    public float min_angel = 0f;
    public float max_angel = 0f;
    public Vector3 rotation_image;

};
public class BossScript : MonoBehaviour
{


    [Header("Enemy Settings")]
    public EnemyType enemyType = EnemyType.Boss;

    public GameObject angel_imge;
    public List<angel_Holder> angel_Holder;
    public int index = 0;


    void MoveTowardsTarget()
    {
   
        transform.Translate(Vector2.right * 2f * Time.deltaTime, Space.World);
    }


    private void Start()
    {
        angel_imge.transform.Rotate(angel_Holder[0].rotation_image);
    }



    private void Update()
    {
        MoveTowardsTarget();
    }
    public bool IsInSlice(float angle)
    {
        return angle >= angel_Holder[index].min_angel && angle <= angel_Holder[index].max_angel;
    }


    public void add_index()
    {
        index++;
     
    }

    public void rotate_angel_image()
    {
        // حذف تمامی رویت‌ها قبل از رویت جدید
        angel_imge.transform.rotation = Quaternion.identity;
        angel_imge.transform.Rotate(angel_Holder[index].rotation_image);
    }

}
