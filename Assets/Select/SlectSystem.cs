using System;
using UnityEngine;
using TMPro;
public class SlectSystem : MonoBehaviour
{
   private Transform _thisTransform ;
   private RectTransform _thisRectTransform ;
   private Vector3 _thisRectPosyMax;
   [SerializeField] private GameObject[] _objects;


   private void Start()
   {
      _thisTransform = GetComponent<Transform>();
      _thisRectTransform = GetComponent<RectTransform>();
      _thisRectPosyMax  =  _thisRectTransform.TransformPoint(new Vector3(0,  _thisRectTransform.rect.yMax,0));
      GenereteBox();
   }

   private void GenereteBox()
   {
      Instantiate(_objects[0], _thisRectPosyMax, Quaternion.identity);
   }
}
