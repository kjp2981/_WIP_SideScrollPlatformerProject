﻿using UnityEngine;
using System.Collections;
using GoogleSheetsToUnity;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using GoogleSheetsToUnity.ThirdPary;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class TestData : ScriptableObject
{
    public string associatedSheet = "";
    public string associatedWorksheet = "";

    public List<string> items = new List<string>();
    
    public List<string> Names = new List<string>();

    /// <summary>
    /// 데이터를 출력해주는 거 같고
    /// </summary>
    /// <param name="list"></param>
    /// <param name="name"></param>
    internal void UpdateStats(List<GSTU_Cell> list, string name)
    {
        items.Clear();
        int math=0, korean=0, english=0;
        for (int i = 0; i < list.Count; i++)
        {
            switch (list[i].columnId)
            {
                case "Math":
                {
                    math = int.Parse(list[i].value);
                    break;
                }
                case "Korean":
                {
                    korean = int.Parse(list[i].value);
                    break;
                }
                case "English":
                {
                    english = int.Parse(list[i].value);
                    break;
                }
            }
        }
        Debug.Log($"{name}의 점수 수학:{math} 국어:{korean} 영어:{english}");
    }

}

[CustomEditor(typeof(TestData))]
public class DataEditor : Editor
{
    TestData data;

    void OnEnable()
    {
        data = (TestData)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("Read Data Examples");

        if (GUILayout.Button("Pull Data Method One"))
        {
            UpdateStats(UpdateMethodOne);
        }
    }

    /// <summary>
    /// 이게 데이터를 가져오는 함수 같음
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="mergedCells"></param>
    void UpdateStats(UnityAction<GstuSpreadSheet> callback, bool mergedCells = false)
    {
        SpreadsheetManager.Read(new GSTU_Search(data.associatedSheet, data.associatedWorksheet), callback, mergedCells);
    }

    /// <summary>
    /// 이건 foreach로 모든 데이터 출려갛는 거 이 함수를 변경하면 될 듯?
    /// </summary>
    /// <param name="ss"></param>
    void UpdateMethodOne(GstuSpreadSheet ss)
    {
        //data.UpdateStats(ss.rows["Jim"]);
        //foreach (string dataName in data.Names)
        //    data.UpdateStats(ss.rows[dataName], dataName);
        
        // TODO : 여기서 레벨 구간 데이터? 같은거를 가져와서 적용시키면 될듯

        EditorUtility.SetDirty(target);
    }
    
}