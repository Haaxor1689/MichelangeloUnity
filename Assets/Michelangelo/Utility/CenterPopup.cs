using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public static class CenterPopup {
    public static Type[] GetAllDerivedTypes(this AppDomain aAppDomain, Type aType) {
        var result = new List<Type>();
        var assemblies = aAppDomain.GetAssemblies();
        foreach (var assembly in assemblies) {
            var types = assembly.GetTypes();
            foreach (var type in types) {
                if (type.IsSubclassOf(aType)) {
                    result.Add(type);
                }
            }
        }
        return result.ToArray();
    }

    public static Rect GetEditorMainWindowPos() {
        var containerWinType = AppDomain.CurrentDomain.GetAllDerivedTypes(typeof(ScriptableObject)).Where(t => t.Name == "ContainerWindow").FirstOrDefault();
        if (containerWinType == null) {
            throw new MissingMemberException("Can't find internal type ContainerWindow. Maybe something has changed inside Unity");
        }
        var showModeField = containerWinType.GetField("m_ShowMode", BindingFlags.NonPublic | BindingFlags.Instance);
        var positionProperty = containerWinType.GetProperty("position", BindingFlags.Public | BindingFlags.Instance);
        if (showModeField == null || positionProperty == null) {
            throw new MissingFieldException("Can't find internal fields 'm_ShowMode' or 'position'. Maybe something has changed inside Unity");
        }
        var windows = Resources.FindObjectsOfTypeAll(containerWinType);
        foreach (var win in windows) {
            var showmode = (int) showModeField.GetValue(win);
            if (showmode == 4) // main window
            {
                var pos = (Rect) positionProperty.GetValue(win, null);
                return pos;
            }
        }
        throw new NotSupportedException("Can't find internal main window. Maybe something has changed inside Unity");
    }

    public static void CenterOnMainWin(this EditorWindow aWin) {
        var main = GetEditorMainWindowPos();
        var pos = aWin.position;
        var w = (main.width - pos.width) * 0.5f;
        var h = (main.height - pos.height) * 0.5f;
        pos.x = main.x + w;
        pos.y = main.y + h;
        aWin.position = pos;
    }
}
