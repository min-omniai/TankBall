using System.Collections;
using UnityEngine;

public class GoalProduction : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(ProductionRoutine());
    }

    private IEnumerator ProductionRoutine()
    {
        while (true)
        {
            float dis = (transform.position - Managers.Game.PlayerCannon.transform.position).sqrMagnitude;

            if (dis <= 1f)
            {
                Managers.Game.PlayerCannon.Fire();
                GetComponent<Poolable>().Destroy();

                yield break;
            }

            transform.position = Vector3.MoveTowards(transform.position, Managers.Game.PlayerCannon.transform.position, 20 * Time.deltaTime);

            yield return null;
        }
    }
}
