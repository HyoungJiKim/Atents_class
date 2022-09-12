using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    Transform[] bgSlots;
    public float scrollSpeed = 2.5f;

    const float Background_Width = 13.6f;
    protected virtual void Awake()
    {
        bgSlots = new Transform[transform.childCount];
        for(int i=0; i<bgSlots.Length; i++)
        {
            bgSlots[i] = transform.GetChild(i);
        }
    }

    private void Update()
    {
        float minusX = transform.position.x - Background_Width;
        //foreach(Transform slot in bgSlots)
        //{
        //    slot.Translate(scrollSpeed * Time.deltaTime * -transform.right);
        //    if (slot.position.x < minusX)
        //    {
        //        MoveRightEnd(slot);
        //    }
        //}

        for(int i = 0; i < bgSlots.Length; i++)
        {
            bgSlots[i].Translate(scrollSpeed * Time.deltaTime * -transform.right);
            if (bgSlots[i].position.x < minusX)
            {
                MoveRightEnd(i);
            }
        }

    }

    protected virtual void MoveRightEnd(int index)
    {
        bgSlots[index].Translate(Background_Width * bgSlots.Length * transform.right);
    }

}
