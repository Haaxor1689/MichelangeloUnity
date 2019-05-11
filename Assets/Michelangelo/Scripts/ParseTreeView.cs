using System;
using System.Collections.Generic;
using System.Linq;
using Michelangelo.Models;
using Michelangelo.Models.MichelangeloApi;
using Michelangelo.Utility;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Michelangelo.Scripts {
    public class ParseTreeView : TreeView {
        private readonly ObjectBase parentObject;

        private ParseTree ParseTree => parentObject.ParseTree;

        public ParseTreeView(ObjectBase parentObject) : base(parentObject.TreeViewState) {
            this.parentObject = parentObject;
            Reload();
        }

        protected override TreeViewItem BuildRoot() => new TreeViewItem { id = -1, depth = -1 };
        
        protected override IList<TreeViewItem> BuildRows(TreeViewItem root) {
            if (ParseTree == null) {
                return new List<TreeViewItem>();
            }

            var rows = GetRows() ?? new List<TreeViewItem>(ParseTree.Count);
            rows.Clear();

            foreach (var node in ParseTree.GetRoots()) {
                var item = new TreeViewItem { id = (int) node.Id, displayName = node.Name };
                root.AddChild(item);
                rows.Add(item);
                
                if (IsExpanded(item.id)) {
                    AddChildrenRecursive(node, item, rows);
                } else {
                    item.children = CreateChildListForCollapsedParent();
                }
            }
            SetupDepthsFromParentsAndChildren(root);
            return rows;
        }

        private void AddChildrenRecursive(NormalizedParseTreeModel node, TreeViewItem item, IList<TreeViewItem> rows) {
            item.children = new List<TreeViewItem>(node.Children.Length);
            foreach (var child in node.GetChildren(ParseTree)) {
                var childItem = new TreeViewItem { id = (int) child.Id, displayName = child.Name };

                item.AddChild(childItem);
                rows.Add(childItem);
                if (!child.IsLeaf) {
                    if (IsExpanded(childItem.id)) {
                        AddChildrenRecursive(ParseTree[child.Id], childItem, rows);
                    } else {
                        childItem.children = CreateChildListForCollapsedParent();
                    }
                }
            }
        }

        protected override IList<int> GetAncestors(int id) {
            var current = ParseTree[(uint) id];
            var ancestors = new List<int>();
            while (current.Rule != "ROOT") {
                current = ParseTree[ParseTree[current.Id].Parent];
                ancestors.Add((int) current.Id);
            }
            return ancestors;
        }

        protected override IList<int> GetDescendantsThatHaveChildren(int id) {
            var stack = new Stack<int>();
            stack.Push(id);

            var parents = new List<int>();
            while (stack.Count > 0) {
                var current = stack.Pop();
                parents.Add(current);
                if (current == -1) {
                    foreach (var root in ParseTree.GetRoots()) {
                        stack.Push((int) root.Id);
                    }
                    continue;
                }
                foreach (var child in ParseTree[(uint)current].GetChildren(ParseTree)) {
                    if (child.Children.Length > 0) {
                        stack.Push((int) child.Id);
                    }
                }
            }
            return parents;
        }

        protected override void SelectionChanged(IList<int> selectedIds) {
            base.SelectionChanged(selectedIds);
            parentObject.MeshHighlights = selectedIds.Where(id => ParseTree[(uint) id].Rule != "ROOT").Select(id => {
                var node = ParseTree[(uint) id];
                var matrix = MeshUtilities.MatrixFromArray(node.Shape.Transform);
                return new MeshGizmoData {
                    Position = matrix.ExtractPosition(),
                    Rotation = matrix.ExtractRotation(),
                    Scale = matrix.ExtractScale() + new Vector3(0.05f, 0.05f, 0.05f),
                    Model = node.Shape
                };
            }).ToList();
        }

        protected override void RowGUI(RowGUIArgs args) {
            if (args.item.displayName == "ROOT") {
                base.RowGUI(args);
                return;
            }
            var contentIndent = GetContentIndent(args.item);

            var labelRect = args.rowRect;
            labelRect.x += contentIndent;
            EditorGUI.LabelField(labelRect, args.label);

            var buttonRect = args.rowRect;
            buttonRect.x += args.rowRect.width - 100;
            buttonRect.width = 100;
            if (GUI.Button(buttonRect, "Attach GO")) {
                parentObject.AttachGameObjectToNode((uint) args.item.id);
            }
        }
    }
}
