using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class CategoryRepository : BaseRepository
    {

        public Category AddCategory(Category category)
        {
            var entry = _context.Categories.Add(category);
            _context.SaveChanges();
            return entry.Entity;
        }


        public ListResponse<Category> GetCategories(int pageIndex, int pageSize, string keyword)
        {
            keyword = keyword?.ToLower().Trim();
            var query = _context.Categories.Where(c => keyword == null || c.Name.ToLower().Contains(keyword)).AsQueryable();
            var totalRecord = query.Count();

            IEnumerable<Category> category = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return new ListResponse<Category>()
            {
                Results = category,
                TotalRecords = totalRecord,
            };
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public Category UpdateCategory(Category model)
        {
            var entry = _context.Categories.Update(model);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteCategory(int id)
        {
            Category category = _context.Categories.FirstOrDefault(w => w.CategoryId == id);
            if (category == null)
                return false;

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return true;
        }
    }
}
