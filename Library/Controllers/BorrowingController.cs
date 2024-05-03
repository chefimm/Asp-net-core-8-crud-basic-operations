using Library.Models;
using Library.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;

namespace Library.Controllers
{
    public class BorrowingController : Controller
    {
        private readonly IBorrowingService borrowingService;
        private readonly UserManager<IdentityUser> userManager;

        public BorrowingController(IBorrowingService borrowingService, UserManager<IdentityUser> userManager)
        {
            this.borrowingService = borrowingService;
            this.userManager = userManager;
        }

        public IActionResult Borrow(int bookId)
        {
            var userId = userManager.GetUserId(User);
            var borrowing = new Borrowing { BookId = bookId, BorrowerId = userId };

            var result = borrowingService.BorrowBook(borrowing);
            if (result)
            {
                TempData["msg"] = "Kitap başarıyla ödünç alındı.";
            }
            else
            {
                TempData["msg"] = "Kitap ödünç alma işlemi başarısız.";
            }

            return RedirectToAction("GetAll", "Book"); // Assuming you have a GetAll action in the BookController
        }

        public IActionResult Return(int borrowingId)
        {
            var result = borrowingService.ReturnBook(borrowingId);
            if (result)
            {
                TempData["msg"] = "Kitap başarıyla iade edildi.";
            }
            else
            {
                TempData["msg"] = "Kitap iade işlemi başarısız.";
            }

            return RedirectToAction("GetAll", "Book"); // Assuming you have a GetAll action in the BookController
        }

        public IActionResult GetBorrowings()
        {
            var userId = userManager.GetUserId(User);
            var borrowings = borrowingService.GetBorrowingsByUser(userId);
            return View(borrowings);
        }

        public IActionResult GetOverdueBorrowings()
        {
            var overdueBorrowings = borrowingService.GetOverdueBorrowings();
            return View(overdueBorrowings);
        }
    }
}
