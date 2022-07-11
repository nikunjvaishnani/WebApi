using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherStore.Repository
{
    public class PublisherRepository : BaseRepository
    {
        public ListResponse<Publisher> GetPublishers(int pageIndex, int pageSize, string keyword)
        {
            keyword = keyword?.ToLower().Trim();
            var query = _context.Publishers.Where(u => keyword == null || u.Name.ToLower().Contains(keyword)).AsQueryable();

            int totalRecords = query.Count();
            IEnumerable<Publisher> Publishers = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return new ListResponse<Publisher>()
            {
                Results = Publishers,
                TotalRecords = totalRecords
            };
        }

        public Publisher AddPublisher(Publisher Publisher)
        {
            var entry = _context.Publishers.Add(Publisher);
            _context.SaveChanges();
            return entry.Entity;
        }

        public Publisher UpdatePublisher(Publisher Publisher)
        {
            var entry = _context.Publishers.Update(Publisher);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool DeletePublisher(int id)
        {
            var Publisher = _context.Publishers.FirstOrDefault(c => c.PublisherId == id);
            if (Publisher == null)
                return false;

            _context.Publishers.Remove(Publisher);
            _context.SaveChanges();
            return true;
        }
    }
}
