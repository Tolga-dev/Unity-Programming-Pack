using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Event : MonoBehaviour
{
    public static event Handler eventHandler;

    void Start()
    {
        EventListener eventListener = new EventListener();
        eventHandler += new Handler(eventListener.Show);
        GenNumber();

    }

    public static void OnEvent(ManagerEventArgs e)
    {
        if (e != null)
            eventHandler(new object(), e);
    }

    public static void GenNumber()
    {
        for (int i = 0; i < 99; i++)
        {
            if (i % 7 == 0)
            {
                ManagerEventArgs e = new ManagerEventArgs(i);
                OnEvent(e);
            }
        }
    }
}

public delegate void Handler([CanBeNull] object o, ManagerEventArgs e);

public class ManagerEventArgs : EventArgs
{
    public readonly int num;

    public ManagerEventArgs(int num)
    {
        this.num = num;
    }
}

public class EventListener
{
    public void Show(object o, ManagerEventArgs e)
    {
        Debug.Log(e.num);
    }
}
     