using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razorPage.Models;

namespace razorPage.Pages
{
    public class BookModel : PageModel
    {
        private readonly MyDbContext _context;

        public List<Book> Books { get; set; } = new List<Book>();

        [BindProperty]
        public Book NewBook { get; set; }

        public BookModel(MyDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Books = _context.Book.ToList();
        }

        public IActionResult OnPost()
        {
            _context.Book.Add(NewBook);

            _context.SaveChanges();

            return RedirectToPage();
        }
    }
}
