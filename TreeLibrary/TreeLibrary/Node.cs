namespace TreeLibrary
{

    public class Node<T>
    {
        public T ValueNode;
        public Node<T> ParentNode;
        public Node<T> Left;
        public Node<T> Right;

        public Node(T value, Node<T> parentNode, Node<T> Left, Node<T> rightNode)
        {
            this.ValueNode = value;
            this.ParentNode = parentNode;
            this.Left = Left;
            this.Right = rightNode;
        }

        public Node(T value, Node<T> parentNode)
            : this(value, parentNode, null, null)
        {

        }

        public override string ToString()
        {
            return string.Format("[pk:{0}, k:{1}, lk:{2}, rk:{3}]",
                 (ParentNode != null) ? ParentNode.ValueNode.ToString() : "null",
                 ValueNode.ToString(),
                 (Left != null) ? Left.ValueNode.ToString() : "null",
                 (Right != null) ? Right.ValueNode.ToString() : "null");
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Node<T> node = obj as Node<T>;

            if (node == null)
            {
                return false;
            }

            return node.ToString() == this.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
