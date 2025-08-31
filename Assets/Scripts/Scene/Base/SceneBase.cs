using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneBase
{
    public abstract string GetSceneName();
    public abstract void OnLoad();
    public abstract void OnUnload();

}
