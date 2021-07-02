using UnityEngine;
using System;
using System.Collections.Generic;

public class BezierSpline : MonoBehaviour
{
    
    [SerializeField]
    public List<Vector3> points = new List<Vector3>();

    [SerializeField]
    private List<BezierControlPointMode>  modes = new List<BezierControlPointMode>();

    [SerializeField]
    private bool loop;

    public bool Loop
    {
        get
        {
            return loop;
        }
        set
        {
            loop = value;
            if (value == true)
            {
                modes[modes.Count - 1] = modes[0];
                SetControlPoint(0, points[0]);
            }
        }
    }

    public int ControlPointCount
    {
        get
        {
            return points.Count;
        }
    }

    public Vector3 GetControlPoint(int index)
    {
        return points[index];
    }

    public void SetControlPoint(int index, Vector3 point)
    {
        if (index % 3 == 0)
        {
            Vector3 delta = point - points[index];
            if (loop)
            {
                if (index == 0)
                {
                    points[1] += delta;
                    points[points.Count - 2] += delta;
                    points[points.Count - 1] = point;
                }
                else if (index == points.Count - 1)
                {
                    points[0] = point;
                    points[1] += delta;
                    points[index - 1] += delta;
                }
                else
                {
                    points[index - 1] += delta;
                    points[index + 1] += delta;
                }
            }
            else
            {
                if (index > 0)
                {
                    points[index - 1] += delta;
                }
                if (index + 1 < points.Count)
                {
                    points[index + 1] += delta;
                }
            }
        }
        points[index] = point;
        EnforceMode(index);
    }

    public BezierControlPointMode GetControlPointMode(int index)
    {
        return modes[(index + 1) / 3];
    }

    public void SetControlPointMode(int index, BezierControlPointMode mode)
    {
        int modeIndex = (index + 1) / 3;
        modes[modeIndex] = mode;
        if (loop)
        {
            if (modeIndex == 0)
            {
                modes[modes.Count - 1] = mode;
            }
            else if (modeIndex == modes.Count - 1)
            {
                modes[0] = mode;
            }
        }
        EnforceMode(index);
    }

    private void EnforceMode(int index)
    {
        int modeIndex = (index + 1) / 3;
        BezierControlPointMode mode = modes[modeIndex];
        if (mode == BezierControlPointMode.Free || !loop && (modeIndex == 0 || modeIndex == modes.Count - 1))
        {
            return;
        }

        int middleIndex = modeIndex * 3;
        int fixedIndex, enforcedIndex;
        if (index <= middleIndex)
        {
            fixedIndex = middleIndex - 1;
            if (fixedIndex < 0)
            {
                fixedIndex = points.Count - 2;
            }
            enforcedIndex = middleIndex + 1;
            if (enforcedIndex >= points.Count)
            {
                enforcedIndex = 1;
            }
        }
        else
        {
            fixedIndex = middleIndex + 1;
            if (fixedIndex >= points.Count)
            {
                fixedIndex = 1;
            }
            enforcedIndex = middleIndex - 1;
            if (enforcedIndex < 0)
            {
                enforcedIndex = points.Count - 2;
            }
        }

        Vector3 middle = points[middleIndex];
        Vector3 enforcedTangent = middle - points[fixedIndex];
        if (mode == BezierControlPointMode.Aligned)
        {
            enforcedTangent = enforcedTangent.normalized * Vector3.Distance(middle, points[enforcedIndex]);
        }
        points[enforcedIndex] = middle + enforcedTangent;
    }

    public int CurveCount
    {
        get
        {
            return (points.Count - 1) / 3;
        }
    }

    public Vector3 GetPoint(float t)
    {
        int i;
        if (t >= 1f)
        {
            t = 1f;
            i = points.Count - 4;
        }
        else
        {
            t = Mathf.Clamp01(t) * CurveCount;
            i = (int)t;
            t -= i;
            i *= 3;
        }
        return transform.TransformPoint(Bezier.GetPoint(points[i], points[i + 1], points[i + 2], points[i + 3], t));
    }

    public Vector3 GetVelocity(float t)
    {
        int i;
        if (t >= 1f)
        {
            t = 1f;
            i = points.Count - 4;
        }
        else
        {
            t = Mathf.Clamp01(t) * CurveCount;
            i = (int)t;
            t -= i;
            i *= 3;
        }
        return transform.TransformPoint(Bezier.GetFirstDerivative(points[i], points[i + 1], points[i + 2], points[i + 3], t)) - transform.position;
    }
    public float GetFloatVelocity(float t)
    {
        int i;
        if (t >= 1f)
        {
            t = 1f;
            i = points.Count - 4;
        }
        else
        {
            t = Mathf.Clamp01(t) * CurveCount;
            i = (int)t;
            t -= i;
            i *= 3;
        }
        return (transform.TransformPoint(Bezier.GetFirstDerivative(points[i], points[i + 1], points[i + 2], points[i + 3], t)) - transform.position).magnitude;
    }
    public Vector3 GetDirection(float t)
    {
        return GetVelocity(t).normalized;
    }
    public float GetFloatDirection(float t)
    {
        return Vector3.Angle(Vector3.up ,GetVelocity(t).normalized);
    }

    public int AddCurve()
    {
        
      
        Vector3 point = points[points.Count - 1];
        points.Add(point);
        points.Add(point);
        points.Add(point);

        point.x += 1f;
        points[points.Count - 3] = point;
        point.x += 1f;
        points[points.Count - 2] = point;
        point.x += 1f;
        points[points.Count - 1] = point;
        modes.Add(BezierControlPointMode.Free);

        modes[modes.Count - 1] = modes[modes.Count - 2];
        EnforceMode(points.Count - 4);

        if (loop)
        {
            points[points.Count - 1] = points[0];
            modes[modes.Count - 1] = modes[0];
            EnforceMode(0);
        }
        return 0;
    }
    public int RemoveCurve(int index)
    {
        if (index % 3 != 0)
        {
         
            return -1;
        }
        if (index == 0)
        {
         
            return -2;
        }
        if (index != points.Count - 1)
        {
          
            points.RemoveAt(index + 3);
            points.RemoveAt(index + 2);
            points.RemoveAt(index + 1);

            modes.RemoveAt(index / 3);

            modes[index / 3 - 1] = modes[index / 3];
           
        }
        else {
            points.RemoveAt(points.Count - 1);
            points.RemoveAt(points.Count - 2);
            points.RemoveAt(points.Count - 3);


            modes.RemoveAt(modes.Count - 1);

            modes[modes.Count - 1] = modes[modes.Count - 2];


        }
        EnforceMode(points.Count - 4);
        if (loop)
        {
            points[points.Count - 1] = points[0];
            modes[modes.Count - 1] = modes[0];
            EnforceMode(0);
        }
        return 0;
    }
    public void Reset()
    {
        points = new List<Vector3> {
			new Vector3(1f, 0f, 0f),
			new Vector3(2f, 0f, 0f),
			new Vector3(3f, 0f, 0f),
			new Vector3(4f, 0f, 0f)
		};
        modes = new List<BezierControlPointMode> {
			BezierControlPointMode.Free,
			BezierControlPointMode.Free
		};
    }
}

public enum BezierControlPointMode
{
    Free,
    Aligned,
    Mirrored
}