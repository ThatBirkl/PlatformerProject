using UnityEngine;
using System.Collections;

interface RInterface
{
    void SaveTRObject();
    void LoadTRObject(RObject trobject);
    void Stop();
    void Reactivate();
}

abstract public class RObject
{

}
