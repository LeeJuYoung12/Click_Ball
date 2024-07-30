using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BallManager
{
    public GameObject BallGroup, Ball1, Ball2, Ball3;
    public float _Movespeed, dirX, dirY;
   
    public Vector3 _dir;
    public void GetBall()
    {
        
        BallGroup = GameManager.Resource_Manager.Instantiate("BallGroup");

        GameManager.isLevel1 = true;
        NextBall();
    }

    public void NextBall()
    {
        

        //좌표 생성
        float xPos = Random.Range(-880, 880);
        float yPos = Random.Range(-460, 460);

        //크기 생성
        float size = Random.Range(0.5f, 1.5f);

        if (GameManager.isLevel3 && GameObject.FindWithTag("Ball") == null)
        {
            //instant 생성
            Ball3 = GameManager.Resource_Manager.Instantiate("Ball3");

            Ball3.transform.position = new Vector3(xPos, yPos, -5f);
            Ball3.transform.localScale = new Vector3(size, size, 0f);
            Ball3.transform.parent = BallGroup.transform;

            _Movespeed = 1;
            GameManager.isLevel2 = false;
            do
            {
                dirX = Random.Range(-900, 900);
            } while (Mathf.Abs(dirX) < 600);

            do
            {
                dirY = Random.Range(-900, 900);
            } while (Mathf.Abs(dirY) < 600);
                     

            _dir = new Vector3(dirX, dirY, 0);

        }
        else if (GameManager.isLevel2 && GameObject.FindWithTag("Ball") == null)
        {
            //instant 생성
            Ball2 = GameManager.Resource_Manager.Instantiate("Ball2");

            Ball2.transform.position = new Vector3(xPos, yPos, -5f);
            Ball2.transform.localScale = new Vector3(size, size, 0f);
            Ball2.transform.parent = BallGroup.transform;

            _Movespeed = 1;
            GameManager.isLevel1 = false;
            dirX = Random.Range(-600, 600);
            dirY = Random.Range(-600, 600);

            _dir = new Vector3(dirX, dirY, 0);
        }
        else if(GameManager.isLevel1 && GameObject.FindWithTag("Ball") == null)
        {
            //instant 생성
            Ball1 = GameManager.Resource_Manager.Instantiate("Ball1");

            Ball1.transform.position = new Vector3(xPos, yPos, -5f);
            Ball1.transform.localScale = new Vector3(size, size, 0f);
            Ball1.transform.parent = BallGroup.transform;
            
        }      
    }

    //2번째볼 랜덤으로 움직임
    public void MoveBall()
    {
        //Game 종료시 오류 방지
        if (Ball2 != null)
        {
            Ball2.transform.position += (_dir * _Movespeed * Time.deltaTime);

        }

        if (Ball3 != null) {
            Ball3.transform.position += (_dir * _Movespeed * Time.deltaTime);
            Ball3.transform.Rotate(0, 0, 100 * Time.deltaTime);
        }

    }

}
