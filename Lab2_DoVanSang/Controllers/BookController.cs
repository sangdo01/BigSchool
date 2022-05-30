using Lab2_DoVanSang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab2_DoVanSang.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        private List<Book> listBook;
        public BookController()
        {
            listBook = new List<Book>();
            listBook.Add(new Book()
            {
                Id = 1,
                Title = "Sach 1",
                Author = "Tac gia sach 1",
                PublicYear = 2001,
                Price = 1000,
                Cover = "./Content/images/images (6).jpg"
            });

            listBook.Add(new Book()
            {
                Id = 2,
                Title = "Sach 2",
                Author = "Tac gia sach 2",
                PublicYear = 2002,
                Price = 2000,
                Cover = "./Content/images/images (7).jpg"
            });

            listBook.Add(new Book()
            {
                Id = 3,
                Title = "Sach 3",
                Author = "Tac gia sach 3",
                PublicYear = 2003,
                Price = 3000,
                Cover = "./Content/images/images (8).jpg"
            });
        }

        // GET: Book
        public ActionResult ListBooks()
        {
            ViewBag.TitlePageName = "Book view page";
            return View(listBook);
        }
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Book book = listBook.Find(s => s.Id == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Book book = listBook.Find(s => s.Id == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var editBook = listBook.Find(b => b.Id == book.Id);
                    editBook.Title = book.Title;
                    editBook.Author = book.Author;
                    editBook.Cover = book.Cover;
                    editBook.Price = book.Price;
                    editBook.PublicYear = book.PublicYear;
                    return View("Listbooks", listBook);
                }
                catch (Exception ex)
                {
                    return HttpNotFound();
                }
            }
            else
            {
                ModelState.AddModelError("", "Input Modle not Vailid!");
                return View(book);
            }
        }
       
        public ActionResult CreateBook()
        {
            return View();
        }
        [HttpPost, ActionName("CreateBook")]
        [ValidateAntiForgeryToken]
        public ActionResult Contact([Bind(Include = "Id, Title, Author, Cover")] Book book)
        {
            var b = new List<Book>();
            b.Add(new Book()
            {
                Id = 1,
                Title = "Sach 1",
                Author = "Tac gia sach 1",
                PublicYear = 2001,
                Price = 1000,
                Cover = "./Content/images/images (6).jpg"
            });

            b.Add(new Book()
            {
                Id = 2,
                Title = "Sach 2",
                Author = "Tac gia sach 2",
                PublicYear = 2002,
                Price = 2000,
                Cover = "./Content/images/images (7).jpg"
            });

            b.Add(new Book()
            {
                Id = 3,
                Title = "Sach 3",
                Author = "Tac gia sach 3",
                PublicYear = 2003,
                Price = 3000,
                Cover = "./Content/images/images (8).jpg"
            });
            try
            {
                if (ModelState.IsValid)
                {
                    b.Add(book);
                }
            }
            catch (Exception )
            {
                ModelState.AddModelError("", "Error Save Data");
                throw;
            }
            return View("ListBooks", b);
        }

        // Ham Delete
        // GET: Hàm Delete(get) 
        // với tham số ID của mục cần xóa
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var book = listBook.Find(s => s.Id == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        // POST: Hàm Delete(get) thực hiện lệnh xóa
        // với tham số truyền vào là id mục cần xóa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var del = listBook.Find(b => b.Id == id); 
                    listBook.Remove(del);
                    return View("Listbooks", del);
                }
                catch (Exception ex)
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return View(book);
            }
        }
    }
}