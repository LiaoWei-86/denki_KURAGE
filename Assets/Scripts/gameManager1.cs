using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager1 : MonoBehaviour
{
    private Dictionary<string, Vector2> points;
    public GameObject player;
    public GameObject A1B1Line;
 
    public InputField inputField;

    public Text hintText;

    // Start is called before the first frame update
    void Start()
    {
        points = new Dictionary<string, Vector2>
        {
            { "A0", GameObject.Find("A0").transform.position },
            { "A1", GameObject.Find("A1").transform.position },
            { "A2", GameObject.Find("A3").transform.position },
            { "B0", GameObject.Find("B0").transform.position },
            { "B1", GameObject.Find("B1").transform.position },
            { "B2", GameObject.Find("B2").transform.position }
        };

        // 将玩家放在A0位置
        player.transform.position = points["A0"];
    }

    // Update is called once per frame
    void Update()
    {
        // 检查是否按下了Enter键
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnSubmit();
        }
    }
    void OnSubmit()
    {
        string input = inputField.text;
        if (input == "A1B1")
        {
            A1B1Line.SetActive(true);
            MovePlayer(new List<string> { "A1", "B1", "B2" });
        }
        else
        {
            hintText.text = "起点と終点以外の点を入力することができますよ。";
            MovePlayer(new List<string> { "A0", "A1", "A3" });
        }
        inputField.text = ""; // 清空输入框
    }

    void MovePlayer(List<string> path)
    {
        StartCoroutine(MovePlayerCoroutine(path));
    }

    IEnumerator MovePlayerCoroutine(List<string> path)
    {
        foreach (var point in path)
        {
            Vector2 targetPosition = points[point];
            while ((Vector2)player.transform.position != targetPosition)
            {
                player.transform.position = Vector2.MoveTowards(player.transform.position, targetPosition, Time.deltaTime * 5f);
                yield return null;
            }
        }
    }
    

}
