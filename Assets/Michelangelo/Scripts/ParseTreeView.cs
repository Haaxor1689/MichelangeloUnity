using Michelangelo.Model.MichelangeloApi;
using UnityEditor.IMGUI.Controls;

namespace Michelangelo.Scripts {
    internal class ParseTreeView : TreeView {
        private readonly ParseTree parseTree;

        public ParseTreeView(TreeViewState state, ParseTree parseTree) : base(state) {
            if (parseTree == null) {
                return;
            }
            this.parseTree = parseTree;
            Reload();
        }

        protected override TreeViewItem BuildRoot() {
            var root = new TreeViewItem { id = -1, depth = -1 };
            foreach (var node in parseTree.GetRoots()) {
                var item = new TreeViewItem { id = (int) node.Id, displayName = node.Rule };
                root.AddChild(item);
                AddChildrenRecursive(node, item);
            }
            SetupDepthsFromParentsAndChildren(root);
            return root;
        }

        private void AddChildrenRecursive(NormalizedParseTreeModel node, TreeViewItem item) {
            foreach (var child in node.Children) {
                var childNode = parseTree[child.Index];
                var childItem = new TreeViewItem { id = (int) childNode.Id, displayName = childNode.Rule };

                item.AddChild(childItem);
                AddChildrenRecursive(childNode, childItem);
            }
        }
    }
}
