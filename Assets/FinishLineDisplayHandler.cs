using Unity.VisualScripting;
using UnityEngine;

public class FinishLineDisplayHandler
{
	//private float _distance;
    private float _duration;
    //private float _endSpeed;

	public FinishLineDisplayHandler(float distance, float duration, float endSpeed)
	{
        //_distance = distance;
        _duration = duration;
        //_endSpeed = endSpeed;
    }

    public void Calculate(int tick, ref float speed, float _distance, float _endSpeed, bool canfinish)
    {
        if (_duration <= 0f) return;

        //if (_distance <= 0 )
        //{
        //    speed = 0f;
        //    return;
        //}

        //var tmpS = speed;

        speed = CalculateV3(speed, _distance, tick, _endSpeed, canfinish);

        //if (tmpS >= _endSpeed && speed <= _endSpeed
        //    || tmpS <= _endSpeed && speed >= _endSpeed)
        //{
        //    speed = _endSpeed;
        //}
    }

    private float CalculateV3(float v2, float _distance, int t3, float _endSpeed, bool canfinish)
    {
        // we calculate v2 ~ end 's duration
        // endX - x dont have v2 * Time.fixedDeltaTime we add here
        float avgSpeed = (v2 + _endSpeed) * 0.5f;
        float distance = (_distance + v2 * Main.fixedTime);
        float duration = distance / avgSpeed;

        // t1 ~ t2 's tick time + v2 ~ end 's duration = tatal time
        // t2 = t3 - 1
        // t1 = 1
        var t = ((t3 - 1) - 1) * Main.fixedTime;

        if (/*duration + t > _duration || duration <= 0*/!canfinish)
        {
            // cant finish in _duration
            // insert a mid speed

            // (m + c)/2 * ta + (m + e) / 2 * tb = d
            // m = (2d - c * ta - e * tb) / (ta + tb)
            // calculate by chatgpt

            //var ta = _duration * 0.5f - t;
            //var tb = _duration * 0.5f;


            var timeLeft = _duration - t;
            Debug.Log($"timeLeft {timeLeft}");
            if (timeLeft < Main.fixedTime)
            {
                return _endSpeed;
            }

            {
                var ta = timeLeft * 1/3f;
                var tb = timeLeft * 2/3f;

                var midSpeed = (2 * distance - v2 * ta - _endSpeed * tb) / (ta + tb);

                // _endSpeed > midSpeed
                // cant break limit
                //if (ta < Main.fixedTime)
                //{
                //    return distance / (_duration - t) * 2 - _endSpeed;
                //}

                var acceleration = (midSpeed - v2) / ta;
                Debug.Log($"t: {t3}- A: ({midSpeed} - {v2}) / {ta} - {distance}");
                return v2 + acceleration * Main.fixedTime;
            }
        }
        else
        {
            // can finish

            if (duration <= 0)
            {
                return _endSpeed;
            }

            var acceleration = (_endSpeed - v2) / duration;
            Debug.Log($"t: {t3}- B");
            return v2 + acceleration * Main.fixedTime;
        }
    }
}
