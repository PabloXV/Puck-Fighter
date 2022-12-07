using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInputController : InputController
{
    public override void TakeTurn()
    {
        StartCoroutine(nameof(AimAndLaunch));
    }

    private IEnumerator AimAndLaunch()
    {
        Aim();
        yield return new WaitForSeconds(0.5f);
        Launch();
    }

    private void Aim()
    {
        trajectory = Random.insideUnitCircle;

        puck.UpdateVelocityIndicator(trajectory);
    }

    private void Launch()
    {
        puck.Launch(trajectory);
    }
}
