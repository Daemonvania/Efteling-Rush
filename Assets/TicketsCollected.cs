using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketsCollected : MonoBehaviour
{
    private DontDestroyOnLoad _dontDestroyOnLoad;

    int initTickets;
    // Start is called before the first frame update
    void Start()
    {
        _dontDestroyOnLoad = FindObjectOfType<DontDestroyOnLoad>();
        initTickets = _dontDestroyOnLoad.tickets;
    }

    // Update is called once per frame
    public void UpdateTicketCounter()
    {
            
    }
}
