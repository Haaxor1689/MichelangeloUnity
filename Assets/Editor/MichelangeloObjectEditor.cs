using System.Collections;
using Michelangelo.Model;
using Michelangelo.Scripts;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MichelangeloObject))]
public class LevelScriptEditor : Editor {
    private string errorMessage;

    private Vector2 scrollPos;

    private void OnEnable() {
        MichelangeloSession.taskDone += TaskDone;
    }

    private void OnDisable() {
        MichelangeloSession.taskDone -= TaskDone;
    }

    public override void OnInspectorGUI() {
        if (!Michelangelo.Session.WebAPI.IsAuthenticated) {
            EditorGUILayout.LabelField("To use this feature, please log in to Michelangelo first, through Window -> Michelangelo.");
            return;
        }

        MichelangeloObject obj = target as MichelangeloObject;
        if (obj.grammar != null && obj.grammar.code == "" && !MichelangeloSession.isLoading) {
            MichelangeloSession.UpdateGrammar(obj.grammar.id);
        }

        EditorGUILayout.LabelField("Name: ", obj.grammar.name);
        EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth));
        EditorGUILayout.LabelField("Code: ", EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.MinHeight(100), GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth));
        EditorGUILayout.TextArea(obj.grammar.code);
        EditorGUILayout.EndScrollView();

        GUILayout.Space(20.0f);
        if (GUILayout.Button("Reload")) {
            MichelangeloSession.UpdateGrammar(obj.grammar.id);
        }
        if (obj.grammar.code != "" && GUILayout.Button("Generate")) {
            MichelangeloSession.modelGenerated += obj.ModelGenerated;
            MichelangeloSession.GenerateGrammar(obj.grammar.id);
        }
        GUILayout.Space(20.0f);
        if (errorMessage != null && errorMessage != "") {
            var style = new GUIStyle(EditorStyles.textField);
            style.normal.textColor = Color.red;

            GUILayout.Space(20.0f);
            GUILayout.Label(errorMessage, style);
        }
    }

    private void TaskDone(string message) {
        errorMessage = message;
        Repaint();
    }
}