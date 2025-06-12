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

        [BindProperty]
        public Book EditableBook { get; set; }

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

        public IActionResult OnPostDelete(int id)
        {
            var book = _context.Book.Find(id);

            if (book == null)
                return NotFound();

            _context.Book.Remove(book);
            _context.SaveChanges();

            return RedirectToPage();
        }

        public IActionResult OnPostEdit([FromBody] Book editedBook)
        {
            var book = _context.Book.Find(editedBook.Id);
            if (book == null) return NotFound();

            book.Title = editedBook.Title;
            book.Description = editedBook.Description;
            book.Author = editedBook.Author;

            _context.SaveChanges();

            return new JsonResult(new { success = true });
        }
    }
}
