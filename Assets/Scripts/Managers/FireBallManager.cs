using System.Collections.Generic;
using UnityEngine;

public class FireBallManager : MonoBehaviour
{
    [SerializeField] int _maxFireBallAmount = 2;
    [SerializeField] GameObject _fireBallPrefab;

    private int _currentFireBallAmount;
    private List<FireBall> _fireBalls = new List<FireBall>();

    public void SpawnFireBall(Vector3 startPosition, Vector3 endPosition)
    {
        if (_currentFireBallAmount < _maxFireBallAmount)
        {
            FireBall fireBall = Instantiate(_fireBallPrefab).GetComponent<FireBall>();
            fireBall.transform.position = startPosition;
            fireBall.SetTargetPosition(startPosition, endPosition);
            
            _fireBalls.Add(fireBall);
            ++_currentFireBallAmount;
        }
    }

    private void Update()
    {
        if (_fireBalls.Count <= 0)
            return;

        for(int i = 0; i < _fireBalls.Count; ++i)
        {
            if (_fireBalls[i] == null || !_fireBalls[i]._isMarkForDelete)
                continue;

            Destroy(_fireBalls[i]);
            _fireBalls[i] = null;
            --_currentFireBallAmount;
        }
    }
}
