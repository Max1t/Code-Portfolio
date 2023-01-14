using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SnapToValue : MonoBehaviour
{

    public IEnumerator SnapTo(ScrollRect scrollRect, float duration, int steps)
    {
        float time = 0;
        float endDragValue = scrollRect.horizontalScrollbar.value;
        float lerpEndValue;



        List<float> stepValues = new List<float>();
        int points = steps - 1;

        for (int i = 0; i < points; ++i)
        {
            stepValues.Add(i * (1 / (float)points));
        }
        stepValues.Add(1);

        foreach (var item in stepValues)
        {
            //Debug.Log(item);
        }



        float nearestStepValue = stepValues.OrderBy(x => Mathf.Abs(x - endDragValue)).First();
        lerpEndValue = nearestStepValue;

        /*
            if (endDragValue < 0.5)
            {
                lerpEndValue = 0;
            }
            else
            {
                lerpEndValue = 1;
            }
            */
        while (time < duration)
        {
            scrollRect.horizontalScrollbar.value = Mathf.Lerp(endDragValue, lerpEndValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
