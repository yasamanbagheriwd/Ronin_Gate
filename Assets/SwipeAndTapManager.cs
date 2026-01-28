using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeAndTapManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float tapDetectionThreshold = 0.2f;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private LineRenderer swipeLineRenderer;

    [Header("References")]
    [SerializeField] private game_maneger gameManager;

    float swipeAngle = 0;

    private Vector2 touchStartPosition;
    private bool isTouching = false;
    private float touchStartTime;


    public AudioSource Sound;

    private void Awake()
    {
        Time.timeScale = 1.0f;
        if (swipeLineRenderer != null)
            swipeLineRenderer.positionCount = 0;
    }

    private void Update()
    {
        if (Input.touchCount == 0) return;

        Touch t = Input.GetTouch(0);

        if (t.phase == TouchPhase.Began)
        {
            HandleTouchBegan(t.position);

        }
        else if (t.phase == TouchPhase.Moved)
        { 
            HandleTouchMoved(t.position);
            Sound.Play();
        }
        else if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
        {
            HandleTouchEnded(t.position);
        }
    }

    private void HandleTouchBegan(Vector2 screenPosition)
    {
        Vector3 wp = Camera.main.ScreenToWorldPoint(screenPosition);
        touchStartPosition = new Vector2(wp.x, wp.y);

        touchStartTime = Time.time;
        isTouching = true;

        if (swipeLineRenderer != null)
            swipeLineRenderer.positionCount = 0;
    }

    private void HandleTouchMoved(Vector2 screenPosition)
    {
        if (!isTouching) return;

        Vector3 wp = Camera.main.ScreenToWorldPoint(screenPosition);
        Vector2 currentPos = new Vector2(wp.x, wp.y);

        if (swipeLineRenderer == null) return;

        if (swipeLineRenderer.positionCount != 2)
            swipeLineRenderer.positionCount = 2;

        swipeLineRenderer.SetPosition(0, touchStartPosition);
        swipeLineRenderer.SetPosition(1, currentPos);
    }

    private void HandleTouchEnded(Vector2 screenPosition)
    {
        if (!isTouching) return;
        isTouching = false;

        Vector3 wp = Camera.main.ScreenToWorldPoint(screenPosition);
        Vector2 endPosition = new Vector2(wp.x, wp.y);

        Vector2 delta = endPosition - touchStartPosition;
        float dist = delta.magnitude;

        if (swipeLineRenderer != null)
            swipeLineRenderer.positionCount = 0;

        // تشخیص Tap
        if (dist <= tapDetectionThreshold)
        {
            PerformTapAction(endPosition);
            return;
        }

        // تشخیص Swipe
        if (dist < 0.1f) return;

        delta.Normalize();
        swipeAngle = Vector2.SignedAngle(Vector2.right, delta);

        RaycastHit2D hit = Physics2D.Linecast(touchStartPosition, endPosition, enemyLayerMask);
        if (!hit) return;

        EnemyScript es = hit.collider.GetComponent<EnemyScript>();
        if (es != null)
        {
            ProcessEnemyHit(hit.collider.gameObject, es, swipeAngle);
        }
        else
        {
            BossScript bs = hit.collider.GetComponent<BossScript>();
            if(bs != null)
            {
                ProcessEnemyHit_Boos(hit.collider.gameObject, bs, swipeAngle);
            }
        }


    }
    private void PerformTapAction(Vector2 tapPosition)
    {
        Collider2D col = Physics2D.OverlapPoint(tapPosition, enemyLayerMask);
        if (col == null) return;

        EnemyScript enemyScript = col.GetComponent<EnemyScript>();
        if (enemyScript == null) return;

        GameObject enemy = col.gameObject;

        bool sliced = enemyScript.IsInSlice(swipeAngle);

        if (enemyScript.enemyType == EnemyType.Friend)
        {
            gameManager.AddScore(15);
            gameManager.ADDOneLife();
            KillEnemy(enemy);
            return;
        }

      


    }
    private void ProcessEnemyHit(GameObject enemy, EnemyScript enemyScript, float swipeAngle)
    {
        bool sliced = enemyScript.IsInSlice(swipeAngle);

        if (enemyScript.enemyType == EnemyType.Friend)
        {
            gameManager.LostOneLife();
            KillEnemy(enemy);
            return;
        }

        if (enemyScript.enemyType == EnemyType.Enemy)
        {
            if (sliced)
            {
                gameManager.AddScore(15);
                KillEnemy(enemy);

                // فراخوانی تابع Add_Gost_Kids وقتی دشمن از نوع Reproducible است
                if (enemyScript.enemyType == EnemyType.Reproducible)
                {
                    Transform gost_transform = enemy.transform;
                    gameManager.AddGostKids(gost_transform);
                }
            }
            else
            {
                enemyScript.moveSpeed += 0.5f;
            }
            return;
        }

        if (enemyScript.enemyType == EnemyType.Reproducible)
        {
            if (sliced)
            {
                gameManager.AddScore(15);
                gameManager.AddGostKids(enemy.transform);
                KillEnemy(enemy);
            }
            else
            {
                enemyScript.moveSpeed += 0.5f;
            }
            return;
        }

    }



    private void ProcessEnemyHit_Boos(GameObject enemy, BossScript bs, float swipeAngle)
    {

        bool sliced = bs.IsInSlice(swipeAngle);

       
            if (sliced)
            {
               
            bs.add_index();
           
            if (bs.index >= bs.angel_Holder.Count)
            {
                gameManager.AddScore(25);
                KillEnemy(bs.gameObject);
            }
            bs.rotate_angel_image();



            }
        
       


    }
    private void KillEnemy(GameObject enemy)
    {
        if (enemy != null)
            Destroy(enemy);
    }
}
