using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TicketNumber : MonoBehaviour
{
    private Text text;

    private DontDestroyOnLoad _dontDestroyOnLoad;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        _dontDestroyOnLoad = FindObjectOfType<DontDestroyOnLoad>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = _dontDestroyOnLoad.tickets.ToString();
    }
}
