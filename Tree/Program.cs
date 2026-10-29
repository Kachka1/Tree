using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransportSystemsTree
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    public class Codec
    {
        public static string Serialize(TreeNode root)
        {
            if (root == null) return "[]";
            var res = new List<string>();
            var q = new Queue<TreeNode>();
            q.Enqueue(root);

            while (q.Count > 0)
            {
                var node = q.Dequeue();
                if (node == null)
                {
                    res.Add("null");
                }
                else
                {
                    res.Add(node.val.ToString());
                    q.Enqueue(node.left);
                    q.Enqueue(node.right);
                }
            }

            while (res.Count > 0 && res[res.Count - 1] == "null")
            {
                res.RemoveAt(res.Count - 1);
            }

            return "[" + string.Join(",", res) + "]";
        }

        public static TreeNode Deserialize(string data)
        {
            if (string.IsNullOrWhiteSpace(data) || data == "[]") return null;
            var parts = data.Trim('[', ']').Split(',');
            var root = new TreeNode(int.Parse(parts[0].Trim()));
            var q = new Queue<TreeNode>();
            q.Enqueue(root);
            int i = 1;

            while (q.Count > 0 && i < parts.Length)
            {
                var node = q.Dequeue();
                if (parts[i].Trim() != "null")
                {
                    node.left = new TreeNode(int.Parse(parts[i].Trim()));
                    q.Enqueue(node.left);
                }
                i++;
                if (i < parts.Length && parts[i].Trim() != "null")
                {
                    node.right = new TreeNode(int.Parse(parts[i].Trim()));
                    q.Enqueue(node.right);
                }
                i++;
            }
            return root;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("=== 1 ===");
            {
                var p1 = Codec.Deserialize("[1,2,3]");
                var q1 = Codec.Deserialize("[1,2,3]");
                Console.WriteLine($"1: Input: p = [1,2,3], q = [1,2,3] Output: {IsSameTree(p1, q1).ToString().ToLower()}");

                var p2 = Codec.Deserialize("[1,2]");
                var q2 = Codec.Deserialize("[1,null,2]");
                Console.WriteLine($"2: Input: p = [1,2], q = [1,null,2] Output: {IsSameTree(p2, q2).ToString().ToLower()}");

                var p3 = Codec.Deserialize("[1,2,1]");
                var q3 = Codec.Deserialize("[1,1,2]");
                Console.WriteLine($"3: Input: p = [1,2,1], q = [1,1,2] Output: {IsSameTree(p3, q3).ToString().ToLower()}\n");
            }

            Console.WriteLine("=== 2 ===");
            {
                var r1 = Codec.Deserialize("[1,2,2,3,4,4,3]");
                Console.WriteLine($"1: Input: root = [1,2,2,3,4,4,3] Output: {IsSymmetric(r1).ToString().ToLower()}");

                var r2 = Codec.Deserialize("[1, 2, 2, null, 3, null, 3]");
                Console.WriteLine($"2: Input: root = [1, 2, 2, null, 3, null, 3] Output: {IsSymmetric(r2).ToString().ToLower()}");

                var p3 = Codec.Deserialize("[1,2,1]");
                var q3 = Codec.Deserialize("[1,1,2]");
                Console.WriteLine($"3: Input: p = [1,2,1], q = [1,1,2] Output: {IsSameTree(p3, q3).ToString().ToLower()}\n");
            }

            Console.WriteLine("=== 3 ===");
            {
                var r1 = Codec.Deserialize("[4,2,7,1,3,6,9]");
                Console.WriteLine($"1: Input: root = [4,2,7,1,3,6,9] Output: {Codec.Serialize(InvertTree(r1))}");

                var r2 = Codec.Deserialize("[2,1,3]");
                Console.WriteLine($"2: Input: root = [2,1,3] Output: {Codec.Serialize(InvertTree(r2))}");

                var r3 = Codec.Deserialize("[]");
                Console.WriteLine($"3: Input: root = [] Output: {Codec.Serialize(InvertTree(r3))}\n");
            }

            Console.WriteLine("=== 4 ===");
            {
                var r1 = Codec.Deserialize("[3,1,4,null,2]");
                Console.WriteLine($"1: Input: root = [3,1,4,null,2], k = 1 Output: {KthSmallest(r1, 1)}");

                var r2 = Codec.Deserialize("[5,3,6,2,4,null,null,1]");
                Console.WriteLine($"2: Input: root = [5,3,6,2,4,null,null,1], k = 3 Output: {KthSmallest(r2, 3)}\n");
            }

            Console.WriteLine("=== 5 ===");
            {
                string s1 = "[1,2,3,null,null,4,5]";
                Console.WriteLine($"1: Input: root = {s1} Output: {Codec.Serialize(Codec.Deserialize(s1))}");

                string s2 = "[]";
                Console.WriteLine($"2: Input: root = {s2} Output: {Codec.Serialize(Codec.Deserialize(s2))}\n");
            }

            Console.WriteLine("=== 6 ===");
            {
                var r1 = Codec.Deserialize("[1,2,3]");
                Console.WriteLine($"1: Input: root = [1,2,3] Output: {MaxPathSum(r1)}");

                var r2 = Codec.Deserialize("[-10,9,20,null,null,15,7]");
                Console.WriteLine($"2: Input: root = [-10,9,20,null,null,15,7] Output: {MaxPathSum(r2)}\n");
            }

            Console.WriteLine("=== 7 ===");
            {
                var r1 = Codec.Deserialize("[0,0,null,0,0]");
                Console.WriteLine($"1: Input: root = [0,0,null,0,0] Output: {MinCameraCover(r1)}");

                var r2 = Codec.Deserialize("[0,0,null,0,null,0,null,null,0]");
                Console.WriteLine($"2: Input: root = [0,0,null,0,null,0,null,null,0] Output: {MinCameraCover(r2)}\n");
            }

            Console.WriteLine("=== 8 ===");
            {
                var r1 = Codec.Deserialize("[3,9,20,null,null,15,7]");
                Console.WriteLine($"1: Input: root = [3,9,20,null,null,15,7] Output: {FormatNestedList(VerticalTraversal(r1))}");

                var r2 = Codec.Deserialize("[1,2,3,4,5,6,7]");
                Console.WriteLine($"2: Input: root = [1,2,3,4,5,6,7] Output: {FormatNestedList(VerticalTraversal(r2))}");

                var r3 = Codec.Deserialize("[1,2,3,4,6,5,7]");
                Console.WriteLine($"3: Input: root = [1,2,3,4,6,5,7] Output: {FormatNestedList(VerticalTraversal(r3))}\n");
            }

            Console.WriteLine("=== 9 ===");
            {
                string s1 = "1-2--3--4-5--6--7";
                Console.WriteLine($"1: Input: traversal = \"{s1}\" Output: {Codec.Serialize(RecoverFromPreorder(s1))}");

                string s2 = "1-2--3---4-5--6---7";
                Console.WriteLine($"2: Input: traversal = \"{s2}\" Output: {Codec.Serialize(RecoverFromPreorder(s2))}");

                string s3 = "1-401--349---90--88";
                Console.WriteLine($"3: Input: traversal = \"{s3}\" Output: {Codec.Serialize(RecoverFromPreorder(s3))}");
            }
        }

        public static bool IsSameTree(TreeNode p, TreeNode q)
        {
            if (p == null && q == null) return true;
            if (p == null || q == null) return false;
            if (p.val != q.val) return false;
            return IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
        }

        public static bool IsSymmetric(TreeNode root)
        {
            if (root == null) return true;
            return IsMirror(root.left, root.right);
        }

        private static bool IsMirror(TreeNode node1, TreeNode node2)
        {
            if (node1 == null && node2 == null) return true;
            if (node1 == null || node2 == null) return false;
            return (node1.val == node2.val) && IsMirror(node1.left, node2.right) && IsMirror(node1.right, node2.left);
        }

        public static TreeNode InvertTree(TreeNode root)
        {
            if (root == null) return null;
            TreeNode temp = root.left;
            root.left = root.right;
            root.right = temp;
            InvertTree(root.left);
            InvertTree(root.right);
            return root;
        }

        public static int KthSmallest(TreeNode root, int k)
        {
            var stack = new Stack<TreeNode>();
            TreeNode curr = root;

            while (curr != null || stack.Count > 0)
            {
                while (curr != null)
                {
                    stack.Push(curr);
                    curr = curr.left;
                }
                curr = stack.Pop();
                k--;
                if (k == 0) return curr.val;
                curr = curr.right;
            }
            return -1;
        }

        private static int maxSum;
        public static int MaxPathSum(TreeNode root)
        {
            maxSum = int.MinValue;
            MaxPathDown(root);
            return maxSum;
        }

        private static int MaxPathDown(TreeNode node)
        {
            if (node == null) return 0;
            int left = Math.Max(0, MaxPathDown(node.left));
            int right = Math.Max(0, MaxPathDown(node.right));
            maxSum = Math.Max(maxSum, left + right + node.val);
            return Math.Max(left, right) + node.val;
        }

        private static int cameras;
        public static int MinCameraCover(TreeNode root)
        {
            cameras = 0;
            if (DfsCamera(root) == 0) cameras++;
            return cameras;
        }

        private static int DfsCamera(TreeNode node)
        {
            if (node == null) return 2;
            int left = DfsCamera(node.left);
            int right = DfsCamera(node.right);

            if (left == 0 || right == 0)
            {
                cameras++;
                return 1;
            }
            if (left == 1 || right == 1) return 2;
            return 0;
        }

        public static IList<IList<int>> VerticalTraversal(TreeNode root)
        {
            var list = new List<(int x, int y, int val)>();
            DfsVertical(root, 0, 0, list);

            var sorted = list.OrderBy(i => i.x).ThenBy(i => i.y).ThenBy(i => i.val).ToList();
            var res = new List<IList<int>>();
            int currentX = int.MinValue;

            foreach (var item in sorted)
            {
                if (item.x != currentX)
                {
                    res.Add(new List<int>());
                    currentX = item.x;
                }
                res.Last().Add(item.val);
            }
            return res;
        }

        private static void DfsVertical(TreeNode node, int x, int y, List<(int x, int y, int val)> list)
        {
            if (node == null) return;
            list.Add((x, y, node.val));
            DfsVertical(node.left, x - 1, y + 1, list);
            DfsVertical(node.right, x + 1, y + 1, list);
        }

        private static string FormatNestedList(IList<IList<int>> list)
        {
            return "[" + string.Join(",", list.Select(l => "[" + string.Join(",", l) + "]")) + "]";
        }

        public static TreeNode RecoverFromPreorder(string traversal)
        {
            var stack = new Stack<TreeNode>();
            int i = 0;

            while (i < traversal.Length)
            {
                int depth = 0;
                while (i < traversal.Length && traversal[i] == '-')
                {
                    depth++;
                    i++;
                }

                int val = 0;
                while (i < traversal.Length && char.IsDigit(traversal[i]))
                {
                    val = val * 10 + (traversal[i] - '0');
                    i++;
                }

                var node = new TreeNode(val);

                while (stack.Count > depth)
                {
                    stack.Pop();
                }

                if (stack.Count > 0)
                {
                    if (stack.Peek().left == null) stack.Peek().left = node;
                    else stack.Peek().right = node;
                }

                stack.Push(node);
            }

            while (stack.Count > 1)
            {
                stack.Pop();
            }

            return stack.Count > 0 ? stack.Pop() : null;
        }
    }
}