using System;
using System.Linq;

namespace TreeLibrary
{
    public class Student : IComparable
    {
        public string StudentName { get; private set; }
        public string TestName { get; private set; }
        public DateTime Date { get; private set; }
        public double Score { get; private set; }

        public Student(DateTime Date, double Score, string StudentName, string TestName)
        {
            this.Date = Date;
            this.Score = Score;
            this.StudentName = StudentName;
            this.TestName = TestName;
        }

        public Student(string StudentName, string TestName, double Score)
        {
            this.StudentName = StudentName;
            this.TestName = TestName;
            this.Date = DateTime.Now;
            this.Score = Score;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Object is null.");
            }

            Student result = obj as Student;

            if (result == null)
            {
                throw new ArgumentNullException("Result is null.");
            }

            if (this.Score > result.Score)
            {
                return 1;
            }
            else if (this.Score < result.Score)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public override bool Equals(object obj)
        {
            if (this == null || obj == null)
            {
                return false;
            }

            if (obj is Student student)
            {
                if (student.Score == this.Score && student.StudentName == this.StudentName &&
                    student.TestName == this.TestName && student.Date == this.Date)
                {
                    return true;
                }
                return false;
            }

            return false;
        }

        public override int GetHashCode()
        {
            char[] letters = StudentName.ToArray();
            int HashCode = 0;

            foreach (char letter in letters)
            {
                HashCode += (int)letter & 0x0f;
            }

            return HashCode;
        }

        public override string ToString()
        {
            return $"Student name: {StudentName}\tTest name: {TestName} \tTest result: {Score}\tDate {Date.ToShortDateString()}";
        }
    }
}
