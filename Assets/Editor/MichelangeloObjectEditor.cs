using System;
using System.Collections;
using System.Linq;
using Michelangelo.Model;
using Michelangelo.Scripts;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MichelangeloObject))]
public class LevelScriptEditor : Editor {
    private string errorMessage;
    private bool isLoading;

    private Vector2 scrollPos;

    public override void OnInspectorGUI() {
        if (!Michelangelo.Session.WebAPI.IsAuthenticated) {
            EditorGUILayout.LabelField("To use this feature, please log in to Michelangelo first, through Window -> Michelangelo.");
            return;
        }

        MichelangeloObject obj = target as MichelangeloObject;
        if (obj.grammar != null &&
            obj.grammar != Grammar.Placeholder &&
            (obj.grammar.code == "" || obj.grammar.code == null) &&
            !isLoading) {
            Reload();
        }

        EditorGUILayout.LabelField("Name: ", obj.grammar.name);
        EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth));
        EditorGUILayout.LabelField("Code: ", EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.MinHeight(100), GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth));
        EditorGUILayout.TextArea(obj.grammar.code);
        EditorGUILayout.EndScrollView();

        GUILayout.Space(20.0f);
        if (isLoading) {
            EditorGUILayout.LabelField("Please wait...", EditorStyles.boldLabel);
            return;
        }

        if (GUILayout.Button("Reload")) {
            Reload();
        }
        if (obj.grammar.code != "" && GUILayout.Button("Generate")) {
            isLoading = true;
            MichelangeloSession.GenerateGrammar(obj.grammar.id).Then(model => {
                obj.model = model;
                isLoading = false;
                Repaint();
            }).Catch(HandleError);
        }
        if (errorMessage != null && errorMessage != "") {
            var style = new GUIStyle(EditorStyles.textField);
            style.normal.textColor = Color.red;

            GUILayout.Space(20.0f);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(errorMessage, style);
            if (GUILayout.Button("X")) {
                errorMessage = null;
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private void Reload() {
        MichelangeloObject obj = target as MichelangeloObject;
        isLoading = true;
        MichelangeloSession.UpdateGrammar(obj.grammar.id).Then(_ => {
            isLoading = false;
            Repaint();
        }).Catch(HandleError);
    }

    private void HandleError(Exception error) {
        errorMessage = error.Message;
        isLoading = false;
        Repaint();
        Debug.LogError(error);
    }
}