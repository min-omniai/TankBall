using System.Collections;
using UnityEngine;

public class PingpongBlockGlow : MonoBehaviour
{
    private Coroutine _timerRoutine = null;

    public void Timer(float timer)
    {
        if (_timerRoutine != null)
        {
            StopCoroutine(_timerRoutine);
            _timerRoutine = null;
            return;
        }

        _timerRoutine = StartCoroutine(TimerRoutine(timer));
    }
    private IEnumerator TimerRoutine(float timer)
    {
        yield return Util.GetWaitForSeconds(timer);

        _timerRoutine = null;
        gameObject.SetActive(false);
    }
}
