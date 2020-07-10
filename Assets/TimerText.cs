using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour {

    public Text text;

    public float time;

    public bool isStop;

    private void Start()
    {
        time = 0;
        isStop = false;
    }

    // Update is called once per frame
    void Update () {
        if (!isStop)
        {
            time += Time.deltaTime;
            text.text = time.ToString("#0.#");

        }
	}
}
