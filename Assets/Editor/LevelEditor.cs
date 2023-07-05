using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//该脚本作为Level组件的编辑器
[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
    Level level;

    Vector2 scrollPos;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        level = target as Level;
        OnRulesGUI(level);
    }

    /// <summary>
    /// 绘制规则编辑器
    /// </summary>
    /// <param name="level"></param>
    void OnRulesGUI(Level level)
    {
        //在监视面板上的名称
        GUILayout.Label("Rules:");
        //GUILayout.BeginScrollView(scrollPos);
        GUILayout.BeginVertical();
        for (int i = 0; i < level.Rules.Count; i++)
        {
            GUILayout.BeginHorizontal();
            //规则预制件拖动范围
            EditorGUILayout.ObjectField(level.Rules[i].Monster, typeof(Unit),true);
            //移除按钮
            if (GUILayout.Button("Remove"))
            {
                level.Rules.RemoveAt(i);
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();

        //GUILayout.EndScrollView();
        //添加规则按钮
        if (GUILayout.Button("Add Rule"))
        {
            level.Rules.Add(new SpawnRule());
        }                
    }
}
