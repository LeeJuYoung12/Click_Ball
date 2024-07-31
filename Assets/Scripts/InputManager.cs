using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class InputManager
{
    private int splitCount;
    private bool isClicked;
   public void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0) && !isClicked)
        {
            isClicked = true;
            //Clicked mouse position
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Find object at this position
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            if (hit.collider != null && hit.collider.tag == "Ball")
            {
                GameManager.score += 1;

                if (GameManager.score == 5) GameManager.isLevel2 = true;
                else if (GameManager.score == 10) GameManager.isLevel3 = true;


                if (GameManager.score >= 10)
                {
                    if (GameManager.isLevel3 && GameManager.Ball_Manager.Ball3 == null)
                    {
                        //3단계 ball 생성
                        hit.collider.gameObject.SetActive(false);
                        GameManager.Ball_Manager.NextBall();
                    }
                    else
                    {
                        float xPos = Random.Range(-560, 560);
                        float yPos = Random.Range(-280, 280);
                        float size = Random.Range(0.4f, 1.2f);
                        hit.collider.transform.position = new Vector3(xPos, yPos, -5f);
                        hit.collider.transform.localScale = new Vector3(size, size, 0f);

                        do
                        {
                            GameManager.Ball_Manager.dirX = Random.Range(-900, 900);
                        } while (Mathf.Abs(GameManager.Ball_Manager.dirX) < 600);

                        do
                        {
                            GameManager.Ball_Manager.dirY = Random.Range(-900, 900);
                        } while (Mathf.Abs(GameManager.Ball_Manager.dirY) < 600);
                        GameManager.Ball_Manager._dir = new Vector3(GameManager.Ball_Manager.dirX, GameManager.Ball_Manager.dirY, 0);
                    }
                }
                else if (GameManager.score >= 5)
                {
                    if (GameManager.isLevel2 && GameManager.Ball_Manager.Ball2 == null)
                    {
                        //2단계 ball 생성
                        hit.collider.gameObject.SetActive(false);
                        GameManager.Ball_Manager.NextBall();
                    }
                    else
                    {
                        float xPos = Random.Range(-560, 560);
                        float yPos = Random.Range(-280, 280);
                        float size = Random.Range(0.4f, 1.2f);
                        hit.collider.transform.position = new Vector3(xPos, yPos, -5f);
                        hit.collider.transform.localScale = new Vector3(size, size, 0f);

                        GameManager.Ball_Manager.dirX = Random.Range(-600, 600);
                        GameManager.Ball_Manager.dirY = Random.Range(-600, 600);
                        GameManager.Ball_Manager._dir = new Vector3(GameManager.Ball_Manager.dirX, GameManager.Ball_Manager.dirY, 0);
                    }
                }
                else
                {
                    float xPos = Random.Range(-560, 560);
                    float yPos = Random.Range(-280, 280);
                    float size = Random.Range(0.4f, 1.2f);
                    hit.collider.transform.position = new Vector3(xPos, yPos, -5f);
                    hit.collider.transform.localScale = new Vector3(size, size, 0f);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //중복클릭 방지
            isClicked = false;
        }
    }
}
