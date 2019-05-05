using System.Collections.Generic;
using System.Linq;
using Michelangelo.Model.MichelangeloApi;
using UnityEditor.IMGUI.Controls;

namespace Michelangelo.Scripts {
    public class ParseTreeView : TreeView {
        private readonly ParseTree parseTree;

        public ParseTreeView(TreeViewState state, ParseTree parseTree) : base(state) {
            this.parseTree = parseTree;
            Reload();
        }

        protected override TreeViewItem BuildRoot() => new TreeViewItem { id = -1, depth = -1 };
        
        protected override IList<TreeViewItem> BuildRows(TreeViewItem root) {
            if (parseTree == null) {
                return new List<TreeViewItem>();
            }

            var rows = GetRows() ?? new List<TreeViewItem>(parseTree.Count);
            rows.Clear();

            foreach (var node in parseTree.GetRoots()) {
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
            foreach (var child in node.GetChildren(parseTree)) {
                var childItem = new TreeViewItem { id = (int) child.Id, displayName = child.Name };

                item.AddChild(childItem);
                rows.Add(childItem);
                if (!child.IsLeaf) {
                    if (IsExpanded(childItem.id)) {
                        AddChildrenRecursive(parseTree[child.Id], childItem, rows);
                    } else {
                        childItem.children = CreateChildListForCollapsedParent();
                    }
                }
            }
        }

        protected override IList<int> GetAncestors(int id) {
            var current = parseTree[(uint) id];
            var ancestors = new List<int>();
            while (current.Rule != "ROOT") {
                current = parseTree.GetParent(current.Id);
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
                foreach (var child in parseTree[(uint)current].GetChildren(parseTree)) {
                    stack.Push((int)child.Id);
                }
            }
            return parents;
        }

        protected override void SelectionChanged(IList<int> selectedIds) {
            base.SelectionChanged(selectedIds);
        }
    }
}
