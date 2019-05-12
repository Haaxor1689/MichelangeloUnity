using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Michelangelo.Utility {
    internal static class CenterPopup {
        private static IEnumerable<Type> GetAllDerivedTypes(this AppDomain aAppDomain, Type aType) {
            return aAppDomain.GetAssemblies()
                             .SelectMany(assembly => assembly.GetTypes(), (assembly, type) => new { assembly, type })
                             .Where(t => t.type.IsSubclassOf(aType))
                             .Select(t => t.type);
        }

        private static Rect GetEditorMainWindowPos() {
            var containerWinType = AppDomain.CurrentDomain.GetAllDerivedTypes(typeof(ScriptableObject)).FirstOrDefault(t => t.Name == "ContainerWindow");
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
                if ((int) showModeField.GetValue(win) != 4) {
                    continue;
                }
                return (Rect) positionProperty.GetValue(win, null);
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
}
