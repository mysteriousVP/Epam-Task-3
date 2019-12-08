using System;
using System.Linq;
using System.Text;

namespace RangeArray
{
    public class ReIndexedArray<TItem>
    {
        private TItem[] array;
        public int LowerIndex { get; set; }
        public int UpperIndex { get; set; }
        public int Lenght { get; private set; }

        public ReIndexedArray(int lower, int upper)
        {
            upper++;
            if (upper <= lower)
            {
                throw new ArrayException("Invalid indixes.");
            }

            this.Lenght = upper - lower;
            this.array = new TItem[upper - lower];
            this.LowerIndex = lower;
            this.UpperIndex = --upper;
        }

        public ReIndexedArray(TItem[] items)
        {
            if (items == null)
            {
                throw new ArrayException("You cannot create an empty array");
            }

            this.Lenght = items.Length;
            this.array = items.ToArray();
            this.LowerIndex = 0;
            this.UpperIndex = items.Length - 1;
        }

        public ReIndexedArray(TItem[] items, int lower, int upper)
        {
            if (items == null)
            {
                throw new ArrayException("You cannot create an empty array");
            }

            upper++;
            if (upper <= lower || items.Length < upper - lower)
            {
                throw new ArrayException("Invalid indixes.");
            }

            this.Lenght = items.Length;
            this.array = items.ToArray();
            this.LowerIndex = lower;
            this.UpperIndex = --upper;
        }

        public TItem this[int index]
        {
            get
            {
                if (index >= LowerIndex && index <= UpperIndex)
                {
                    return this.array[index - LowerIndex];
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }

            set
            {
                if (index >= LowerIndex && index <= UpperIndex)
                {
                    if (!(value is TItem))
                    {
                        throw new ArrayException("You cannot add data of this type.");
                    }

                    this.array[index - LowerIndex] = (TItem)value;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArrayException("message");
            }
            else
            {
                if (obj as ReIndexedArray<TItem> != null)
                {
                    ReIndexedArray<TItem> indexedArray = (ReIndexedArray<TItem>)obj;
                    if (indexedArray.Lenght == this.Lenght && (
                       indexedArray.array.Concat(this.array).Distinct().ToArray().Length == 1 ||
                       indexedArray.array.Concat(this.array).Distinct().ToArray().Length == this.array.Length))
                    {
                        return true;
                    }
                }
                else
                {

                }

                return false;
            }
        }

        public override int GetHashCode()
        {
            return this.LowerIndex ^ this.Lenght ^ this.UpperIndex;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = this.LowerIndex; i < this.UpperIndex; i++)
            {
                stringBuilder.Append("[" + this[i].ToString() + "], ");
            }
            stringBuilder.Append("[" + this[this.UpperIndex].ToString() + "];");

            return stringBuilder.ToString();
        }

        public TItem[] ToArray()
        {
            if (this == null)
            {
                throw new ArrayException("You cannot get an array from empty instance.");
            }

            TItem[] arrayGeneric = new TItem[this.Lenght];

            arrayGeneric = this.array.ToArray();

            return arrayGeneric;
        }
    }
}
