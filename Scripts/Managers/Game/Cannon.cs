using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Cannon : MonoBehaviour
{
    [FormerlySerializedAs("_cannonColorType")]
    public Define.ColorType CannonColor;
    private bool _isAi = false;
    private Transform _launchPoint = null;
    private int _dir = 1;
    private float _speed = 50f;

    private float _timeCheck = 0f;
    private float _timeRange = 3.0f;


    public void Init()
    {
        _launchPoint = Util.FindChild<Transform>(gameObject, "Root");

        switch (CannonColor)
        {
            case Define.ColorType.Red:
                _isAi = false;
                break;

            case Define.ColorType.Blue:
                _isAi = true;
                _timeRange = 3.0f;
                break;
        }
    }

    private void Update()
    {
        if (Managers.Game.GameStatePlay)
        {
            _launchPoint.Rotate(Vector3.forward * Time.deltaTime * _dir * _speed);

            if (_isAi)
            {
                _timeCheck += Time.deltaTime;

                if (_timeCheck > _timeRange)
                {
                    _timeCheck = 0f;
                    _timeRange = Random.Range(3, 7);
                    Managers.Game.EnemyCannon.Fire();
                }
            }

            if (_launchPoint.eulerAngles.z < 125)
                _dir = 1;
            else if (_launchPoint.eulerAngles.z > 235)
                _dir = -1;
        }
        else
        {
            if (_fireRoutine != null)
            {
                StopCoroutine(_fireRoutine);
                _fireRoutine = null;
            }
        }
    }

    private Coroutine _fireRoutine = null;
    public void Fire()
    {
        if (_fireRoutine != null)
        {
            StopCoroutine(_fireRoutine);
            _fireRoutine = null;
        }

        _fireRoutine = StartCoroutine(FireRoutine());
    }

    private IEnumerator FireRoutine()
    {
        int shootCount = 0;
        if (_isAi)
            shootCount = Random.Range(10, 35);
        else
            shootCount = Managers.Game.GameSceneUI.ShootCount;

        for (int i = shootCount; i > 0; i--)
        {
            GameObject bullet = Managers.Resource.Instantiate("Bullet");
            bullet.transform.position = _launchPoint.position;
            float z = _launchPoint.eulerAngles.z - 180;
            bullet.transform.eulerAngles = new Vector3(_launchPoint.eulerAngles.x,
                                                        _launchPoint.eulerAngles.y,
                                                        z);
            bullet?.GetComponent<Bullet>().Init(CannonColor);

            if (!_isAi)
                Managers.Game.GameSceneUI.SetShootCount(-1);

            yield return Util.GetWaitForSeconds(.06f);
        }
    }
}
