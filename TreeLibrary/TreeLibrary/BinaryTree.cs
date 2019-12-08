using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TreeLibrary
{
    public class BinaryTree<T> : ICollection<T> where T : IComparable
    {
        public Node<T> Root { get; private set; }
        public int NodesAmount { get; private set; }
        public delegate void BinaryTreeHandler(object sender, TreeEventArgs<T> e);
        public event BinaryTreeHandler Notify;

        public int Count
        {
            get
            {
                return NodesAmount;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public BinaryTree()
        {
            Root = null;
            NodesAmount = 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new NodeEnumerator(this);
        }

        public bool Insert(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Value is null");
            }

            Node<T> ParentNode = null;
            int compare = 0;
            Notify?.Invoke(this, new TreeEventArgs<T>($"Value - {value} inserted to the tree", value));

            for (Node<T> currentNode = Root; currentNode != null;)
            {
                compare = value.CompareTo(currentNode.ValueNode);
                ParentNode = currentNode;

                if (compare >= 0)
                {
                    currentNode = currentNode.Right;
                }
                else if (compare < 0)
                {
                    currentNode = currentNode.Left;
                }
                else
                {
                    currentNode = null;
                }
            }

            Node<T> newNode = new Node<T>(value, ParentNode);
            NodesAmount++;

            if (ParentNode == null)
            {
                Root = newNode;
            }
            else if (compare >= 0)
            {
                ParentNode.Right = newNode;
            }
            else
            {
                ParentNode.Left = newNode;
            }

            return true;
        }

        public Node<T> FindNode(T key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("Key is null.");
            }

            Node<T> currentNode = Root;

            while (currentNode != null)
            {
                int compare = key.CompareTo(currentNode.ValueNode);

                if (compare > 0)
                {
                    currentNode = currentNode.Right;
                }
                else if (compare < 0)
                {
                    currentNode = currentNode.Left;
                }
                else
                {
                    break;
                }
            }

            if (currentNode != null)
            {
                Notify?.Invoke(this, new TreeEventArgs<T>($"Node with value - {currentNode.ValueNode} found", currentNode.ValueNode));
            }
            else
            {
                Notify?.Invoke(this, new TreeEventArgs<T>($"Node with value - {currentNode.ValueNode} did not found", currentNode.ValueNode));
            }

            return currentNode;
        }

        public bool RemoveNode(Node<T> node)
        {
            if (node == null)
            {
                Notify?.Invoke(this, new TreeEventArgs<T>($"Node with value - {node.ValueNode} did not removed", node.ValueNode));
                return false;
            }

            NodesAmount--;
            Notify?.Invoke(this, new TreeEventArgs<T>($"Node with value - {node.ValueNode} removed", node.ValueNode));

            if (node.Left == null)
            {
                ReplaceNode(node, node.Right);
            }
            else if (node.Right == null)
            {
                ReplaceNode(node, node.Left);
            }
            else
            {
                Node<T> nextNode = MinimumNode(node.Right);

                if (nextNode.ParentNode != node)
                {
                    ReplaceNode(nextNode, nextNode.Right);
                    nextNode.Right = node.Right;
                    nextNode.Right.ParentNode = nextNode;
                }

                ReplaceNode(node, nextNode);
                nextNode.Left = node.Left;
                nextNode.Left.ParentNode = nextNode;
            }

            return true;
        }

        public Node<T> MinimumNode(Node<T> root)
        {
            if (root == null)
            {
                throw new ArgumentNullException("Root is null.");
            }

            Node<T> currentNode = root;

            while (currentNode.Left != null)
            {
                currentNode = currentNode.Left;
            }

            return currentNode;
        }

        public Node<T> MaximumNode(Node<T> root)
        {
            if (root == null)
            {
                throw new ArgumentNullException("Root is null.");
            }

            Node<T> currentNode = root;

            while (currentNode.Right != null)
            {
                currentNode = currentNode.Right;
            }

            return currentNode;
        }

        public Node<T> FindNextNode(Node<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("Node is null.");
            }

            if (node.Right != null)
            {
                return MinimumNode(node.Right);
            }

            Node<T> nextNode = node.ParentNode;

            while (nextNode != null && node == nextNode.Right)
            {
                node = nextNode;
                nextNode = nextNode.ParentNode;
            }

            return nextNode;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (T node in this)
            {
                stringBuilder.AppendLine(node.ToString());
            }

            return stringBuilder.ToString();
        }

        private void ReplaceNode(Node<T> oldNode, Node<T> newNode)
        {
            if (oldNode.ParentNode == null)
            {
                Root = newNode;
            }
            else if (oldNode == oldNode.ParentNode.Left)
            {
                oldNode.ParentNode.Left = newNode;
            }
            else
            {
                oldNode.ParentNode.Right = newNode;
            }

            if (newNode != null)
            {
                newNode.ParentNode = oldNode.ParentNode;
            }
        }

        public void Add(T item)
        {
            Insert(item);
        }

        public void Clear()
        {
            Root = null;
        }

        public bool Contains(T item)
        {
            return FindNode(item) != null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < 0)
            {
                throw new BinaryTreeException("Index is less then 0.");
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new BinaryTreeException("Not enough elements after index in the destination array.");

            }

            if (NodesAmount != 0)
            {
                int i = 0;

                foreach (T value in this)
                {
                    array[i + arrayIndex] = value;
                    i++;
                }
            }
        }

        public bool Remove(T key)
        {
            return RemoveNode(FindNode(key)) == true;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class NodeEnumerator : IEnumerator<T>
        {
            private BinaryTree<T> Tree;
            private Node<T> CurrentNode;
            private bool IsReseted;

            public NodeEnumerator(BinaryTree<T> tree)
            {
                Tree = tree;
                Reset();
            }

            object IEnumerator.Current
            {
                get
                {
                    return CurrentNode;
                }
            }

            public Node<T> Current
            {
                get
                {
                    return CurrentNode;
                }
            }

            T IEnumerator<T>.Current
            {
                get
                {
                    return CurrentNode.ValueNode;
                }
            }

            public bool MoveNext()
            {
                if (IsReseted)
                {
                    if (Tree.Root != null)
                    {
                        CurrentNode = Tree.MinimumNode(Tree.Root);
                    }

                    IsReseted = false;
                }
                else if (CurrentNode != null)
                {
                    CurrentNode = Tree.FindNextNode(CurrentNode);
                }

                return (CurrentNode != null);
            }

            public void Reset()
            {
                CurrentNode = null;
                IsReseted = true;
            }

            void IDisposable.Dispose()
            {

            }
        }
    }
}
