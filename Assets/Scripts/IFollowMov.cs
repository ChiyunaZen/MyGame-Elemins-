using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFollowMov
{
    void StartFollowing();

    void StopFollowing();

    IEnumerator RestartFollowing();
}
