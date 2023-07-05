using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//�ýű���ΪLevel����ı༭��
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
    /// ���ƹ���༭��
    /// </summary>
    /// <param name="level"></param>
    void OnRulesGUI(Level level)
    {
        //�ڼ�������ϵ�����
        GUILayout.Label("Rules:");
        //GUILayout.BeginScrollView(scrollPos);
        GUILayout.BeginVertical();
        for (int i = 0; i < level.Rules.Count; i++)
        {
            GUILayout.BeginHorizontal();
            //����Ԥ�Ƽ��϶���Χ
            EditorGUILayout.ObjectField(level.Rules[i].Monster, typeof(Unit),true);
            //�Ƴ���ť
            if (GUILayout.Button("Remove"))
            {
                level.Rules.RemoveAt(i);
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();

        //GUILayout.EndScrollView();
        //��ӹ���ť
        if (GUILayout.Button("Add Rule"))
        {
            level.Rules.Add(new SpawnRule());
        }                
    }
}
