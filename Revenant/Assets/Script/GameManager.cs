using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject player;
    GameObject aim;
    public GameObject[] emptyCrystal;
    public int emptyCrystalLenght;

    // Start is called before the first frame update
    void Start()
    {
        if (emptyCrystal != null)
            emptyCrystalLenght = emptyCrystal.Length;
        else
            Debug.Log("GameManager Error : emptyCrystal not find");

        player = GameObject.Find("PC");
        aim = GameObject.Find("Aim");
        if (player == null)
            Debug.Log("GameManager Error : player not find");
        if (aim == null)
            Debug.Log("GameManager Error : aim not find");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 크리스탈 색 빼기
        {
            player.GetComponent<Player>().PlayerAnimation("UseStaff");
            UseStaff();
        }
        if (Input.GetMouseButtonDown(1))//범위 크리스탈 색 Empty로 바꾸기
        {
            player.GetComponent<Player>().PlayerAnimation("UseStaff");
            player.GetComponent<Player>().ChangeStaffMaterial();
            player.GetComponent<Player>().ChangeStaffState(C_STATE.EMPTY);
        }
    }

    void UseStaff()
    {
        if (aim.GetComponent<PlayerAimState>().isCol == true)
        {
            if (aim.GetComponent<PlayerAimState>().col.tag == "Empty_Crystal") // 에임과 충돌한것->내스테프와 같은것
            {
                //크리스탈이 달라야만 바꿔준다.
                if (aim.GetComponent<PlayerAimState>().col.GetComponent<CrystalState>().state !=
                    player.GetComponent<Player>().GetStaffState())
                {
                    //마테리얼을 서로 교체해줌
                    int saveNum = player.GetComponent<Player>().GetStaffCryNumber();
                    C_STATE saveState = player.GetComponent<Player>().GetStaffState(); //스태프 상태 저장
                    player.GetComponent<Player>().ChangeStaffNum(aim.GetComponent<PlayerAimState>().col.GetComponent<CrystalState>().myNum); //빈크리스탈과 Link되잇는 넘버정보넘김
                    player.GetComponent<Player>().ChangeStaffMaterial(aim.GetComponent<PlayerAimState>().col.GetComponent<CrystalState>().myMat.material); //크리스탈 메테리얼 넘김(스태프색바뀜)
                    player.GetComponent<Player>().ChangeStaffState(aim.GetComponent<PlayerAimState>().col.GetComponent<CrystalState>().state); //빈크리스탈의 상태를 스태프에게 전달
                    aim.GetComponent<PlayerAimState>().col.GetComponent<CrystalState>().myNum = saveNum; //저장되있던 스태프와 Link되있는 넘버정보를 넘김
                    aim.GetComponent<PlayerAimState>().col.GetComponent<CrystalState>().state = saveState; // 저장되있던 스태프 상태를 넘김(크리스탈색바뀜)
                    aim.GetComponent<PlayerAimState>().col.GetComponent<CrystalState>().changeMat = true;
                }
            }
            else if (aim.GetComponent<PlayerAimState>().col.tag == "Crystal")
            {

                ResetCrystal(aim.GetComponent<PlayerAimState>().col.GetComponent<CrystalState>().myNum);
                //완전체 크리스탈의 정보를 스태프로 가져옴
                player.GetComponent<Player>().ChangeStaffNum(aim.GetComponent<PlayerAimState>().col.GetComponent<CrystalState>().myNum);
                player.GetComponent<Player>().ChangeStaffMaterial(aim.GetComponent<PlayerAimState>().col.GetComponent<CrystalState>().myMat.material);
                player.GetComponent<Player>().ChangeStaffState(aim.GetComponent<PlayerAimState>().col.GetComponent<CrystalState>().state);
            }
        }
    }
    //색크리스탈을 이미 가지고있는 녀석이 있다면 링크 해재! (초기화)
    void ResetCrystal(int num)
    {
        for (int i = 0; i < emptyCrystalLenght; i++)
        {
            if (emptyCrystal[i].GetComponent<CrystalState>().myNum == num)
            {
                emptyCrystal[i].GetComponent<CrystalState>().Reset();
            }
        }
    }


}
