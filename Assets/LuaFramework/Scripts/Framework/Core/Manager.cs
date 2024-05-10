using UnityEngine;
using System.Collections;
using LuaFramework;

public class Manager : IManager {

    public virtual void OnGameInit() {}
    public virtual void OnGameStart() {}
    public virtual void OnGameUpdate() {}
    public virtual void OnGameDestroy() {}
}
