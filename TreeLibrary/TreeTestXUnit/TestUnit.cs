using System;
using Xunit;
using TreeLibrary;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TreeTestXUnit
{
    public class UnitTest
    {
        [Fact]
        public void CountShouldReturnAmountOfNodesInTree()
        {
            //arrange
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Add(5);
            tree.Add(1);
            tree.Add(2);
            tree.Add(7);

            int expected = 4;

            //act
            int actual = tree.Count;

            //assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsReadOnlyShouldReturnPosibilityToChanchingTree()
        {
            //arrange
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Add(5);
            tree.Add(1);
            tree.Add(2);
            tree.Add(7);

            bool expected = false;

            //act
            bool actual = tree.IsReadOnly;

            //assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(-5, true)]
        [InlineData(100, true)]
        [InlineData(int.MaxValue, true)]
        public void InsertShouldReturnResultOfAddingNodeToTheTree(int data, bool expected)
        {
            //arrange
            BinaryTree<int> tree = new BinaryTree<int>();

            //act
            bool actual = tree.Insert(data);

            //assert
            Assert.True(actual);
        }

        [Fact]
        public void InsertShouldThrowArgumentNullExceptionDueToIncorrectKey()
        {
            //arrange
            BinaryTree<string> tree = new BinaryTree<string>();

            //act
            Func<bool> actual = () => tree.Insert(null);

            //assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await Actual(tree));
        }

        async Task Actual(BinaryTree<string> tree)
        {
            if (tree == null)
            {
                throw new ArgumentNullException(nameof(tree));
            }

            tree.Insert(null);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(-5)]
        [InlineData(100)]
        public void FindNodeShouldFindNodeInTheTree(int expected)
        {
            //arrange
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Add(5);
            tree.Add(1);
            tree.Add(expected);
            tree.Add(7);

            //act
            Node<int> node = tree.FindNode(expected);

            //assert
            Assert.Equal(expected, node.ValueNode);
        }

        [Fact]
        public void FindNodeShouldThrowArgumentNullExceptionDueToIncorrectKey()
        {
            //arrange
            BinaryTree<string> tree = new BinaryTree<string>();

            //act
            Action actual = () => tree.FindNode(null);

            //assert
            Assert.Throws<ArgumentNullException>(actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(-5)]
        [InlineData(100)]
        public void RemoveNodeShouldRemoveConcreteNodeInTheTree(int data)
        {
            //arrange
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Add(5);
            tree.Add(1);
            tree.Add(data);
            tree.Add(7);

            //act
            bool actual = tree.RemoveNode(new Node<int>(data, null));

            //assert
            Assert.True(actual);
        }

        [Fact]
        public void RemoveNodeShouldThrowArgumentNullExceptionDueToIncorrectNode()
        {
            //arrange
            BinaryTree<string> tree = new BinaryTree<string>();
            tree.Add("a");
            tree.Add("b");
            tree.Add("c");
            tree.Add("d");

            //act
            Action action = () => tree.Remove(null);

            //assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void MinimumNodeShouldFindMinimunValueInTheTree()
        {
            //arrange
            int expected = -5;

            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Add(5);
            tree.Add(1);
            tree.Add(expected);
            tree.Add(7);

            //act
            Node<int> minimumNode = tree.MinimumNode(tree.Root);

            //assert
            Assert.Equal(expected, minimumNode.ValueNode);
        }

        [Fact]
        public void MinimumNodeShouldThrowArgumentNullExceptionDueToIncorrectRoot()
        {
            //arrange
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Add(5);
            tree.Add(1);
            tree.Add(3);
            tree.Add(7);
            ArgumentNullException expected = new ArgumentNullException();

            //act
            Action minimumAction = () => tree.MinimumNode(null);
            ArgumentNullException actual = Assert.Throws<ArgumentNullException>(minimumAction);
            //assert
            Assert.Contains(expected.Message, actual.Message);
        }

        [Fact]
        public void MaximumNodeShouldFindMaximumValueInTheTree()
        {
            //arrange
            int expected = 100;

            BinaryTree<int> tree = new BinaryTree<int>
            {
                5,
                1,
                expected,
                7
            };

            //act
            Node<int> maximumNode = tree.MaximumNode(tree.Root);

            //assert
            Assert.Equal(expected, maximumNode.ValueNode);
        }

        [Fact]
        public void MaximumNodeShouldThrowArgumentNullExceptionDueToIncorrectRoot()
        {
            //arrange
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Add(5);
            tree.Add(1);
            tree.Add(3);
            tree.Add(7);
            ArgumentNullException expected = new ArgumentNullException();

            //act
            Action minimumAction = () => tree.MaximumNode(null);
            ArgumentNullException actual = Assert.Throws<ArgumentNullException>(minimumAction);
            //assert
            Assert.Contains(expected.Message, actual.Message);
        }

        [Fact]
        public void FindNextNodeShouldFindNextNodeInTheTree()
        {
            //arrange
            int expected = 2;

            BinaryTree<int> tree = new BinaryTree<int>
            {
                5,
                1,
                expected,
                7
            };

            //act
            Node<int> nextNode = tree.FindNextNode(tree.FindNode(1));

            //assert
            Assert.Equal(expected, nextNode.ValueNode);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(-5, true)]
        [InlineData(100, true)]
        public void RemoveShouldCallMethodRemoveNodeAndReturnResultOfTheDeleting(int data, bool expected)
        {
            //arrange
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Add(5);
            tree.Add(1);
            tree.Add(data);
            tree.Add(7);

            //act
            bool actual = tree.Remove(data);

            //assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToStringShouldReturnTreeInStringForm()
        {
            //arrange
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Add(5);
            tree.Add(int.MaxValue);
            tree.Add(int.MinValue);

            StringBuilder expected = new StringBuilder();

            foreach (int node in tree)
            {
                expected.AppendLine(node.ToString());
            }
            //act
            string actual = tree.ToString();

            //assert
            Assert.Equal(expected.ToString(), actual);
        }

        [Fact]
        public void CopyToShouldCopyNodesFromTreeToTheArrayOfNodes()
        {
            //arrange
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Add(5);
            tree.Add(5);
            tree.Add(5);
            tree.Add(1);
            tree.Add(4);
            tree.Add(5);
            tree.Add(7);

            int[] expected = new int[7];
            expected[0] = 1;
            expected[1] = 4;
            expected[2] = 5;
            expected[3] = 5;
            expected[4] = 5;
            expected[5] = 5;
            expected[6] = 7;

            int[] nodes = new int[7];

            //act
            tree.CopyTo(nodes, 0);

            //Assert
            Assert.Equal(expected, nodes);
        }

        [Fact]
        public void CopyToShouldThrowBinaryTreeExceptionDueToIncorrectArrayIndex()
        {
            //arrange
            BinaryTree<int> tree = new BinaryTree<int>();
            BinaryTreeException expected = new BinaryTreeException("Index is less then 0.");

            //act
            int[] nodes = new int[7];
            nodes[0] = 5;
            nodes[1] = 5;
            nodes[2] = 5;
            Action action = () => tree.CopyTo(nodes, -1);
            BinaryTreeException actual = Assert.Throws<BinaryTreeException>(action);
            
            //assert
            Assert.Contains(expected.Message, actual.Message);
        }

        [Fact]
        public void CopyToShouldThrowBinaryTreeExceptionDueToNotEnoughElementInArray()
        {
            //arrange
            BinaryTree<int> tree = new BinaryTree<int>()
            {
                1, 1, 1, 1, 1, 1, 1
            };
            BinaryTreeException expected = new BinaryTreeException("Not enough elements after index in the destination array.");

            //act
            //fix
            int[] nodes = new int[7];
            nodes[0] = 5;
            nodes[1] = 5;
            nodes[2] = 5;
            Action action = () => tree.CopyTo(nodes, 2);
            BinaryTreeException actual = Assert.Throws<BinaryTreeException>(action);

            //assert
            Assert.Contains(expected.Message, actual.Message);
        }

        [Fact]
        public void GetEnumeratorShouldReturnAnIEnumeratorInstanceWithResetedIteratorOfTheTree()
        {
            //arrange
            BinaryTree<int> tree = new BinaryTree<int>()
            {
                2, 5, 1, 7, 4
            };
            List<Node<int>> testList = new List<Node<int>>()
            {
                new Node<int>(1, null),
                new Node<int>(5, null),
                new Node<int>(7, null),
                new Node<int>(2, null),
                new Node<int>(6, null)
            };
            int expected = testList[0].ValueNode;

            //act 
            IEnumerator<int> actual = tree.GetEnumerator();
            actual.MoveNext();

            //assert
            Assert.Equal(expected, actual.Current);
        }
    }
}
