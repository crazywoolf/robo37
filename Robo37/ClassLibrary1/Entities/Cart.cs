using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public IEnumerable<CartLine> Lines { get { return lineCollection; } }

        public void AddItem(Course course, int quantity)
        {
            CartLine line = lineCollection
                .Where(b => b.Course.CourseId == course.CourseId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine { Course = course, Quantity = quantity });
            }

            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Course course)
        {
            lineCollection.RemoveAll(l => l.Course.CourseId == course.CourseId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Course.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }
    }

    public class CartLine
    {
        public Course Course { get; set; }
        public int Quantity { get; set; }
    }
}
