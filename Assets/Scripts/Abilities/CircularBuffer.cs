using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircularBuffer
{
    private RObject[] RObjectArray;
    private int end;
    private int start;
    private int size;

    public CircularBuffer(int _size)
    {
        size = _size;
        start = end = 0;
        RObjectArray = new RObject[size];
    }

    public void Push(RObject obj)
    {
        if ((end + 1) % size == start)
        {
            start = (start + 1) % size;
        }
        RObjectArray[end] = obj;
        end = (end + 1) % size;
    }

    public RObject Pop()
    {
        if (end != start)
        {
            end = (end - 1 + size) % size;
            return RObjectArray[end];
        }
        else
        {
            return null;
        }
    }

    public void Clear()
    {
        for (int i = 0; i < size; i++)
        {
            RObjectArray[i] = null;
        }
        start = end = 0;
    }

    public int Count
    {
        get { return (end - start + size) % size; }
    }
}

