using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickLoanTracker.Models;

namespace QuickLoanTracker.Controllers
{
    public class LoanController : Controller
    {

        private static List<Loan> loans = new List<Loan>()
        {
            new Loan{Id=1,BorrowerName="Sahil",LenderName="HDFC",Amount=20000,IsSettled=false},

            new Loan{Id=2,BorrowerName="Shivam",LenderName="ICICI",Amount=50000,IsSettled=true}
        };

        // GET: LoanController
        public ActionResult Index()
        {
            return View(loans);
        }

        // GET: LoanController/Details/5
        public ActionResult Details(int id)
        {
            return View(model: loans[id-1]);
        }

        // GET: LoanController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoanController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Loan loan)
        {
                if(ModelState.IsValid)
                {
                    loan.Id = loans.Max(x => x.Id) + 1;
                    loans.Add(loan);
                
                    return RedirectToAction("Index");
                }

                return View(loan);
        }

        // GET: LoanController/Edit/5
        public ActionResult Edit(int id)
        {
            var loan = loans.FirstOrDefault(x=>x.Id == id);

            if(loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // POST: LoanController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Loan loan)
        {
            var existingLoan = loans.FirstOrDefault(x=>x.Id==loan.Id);

            if(existingLoan != null)
            {
                existingLoan.BorrowerName = loan.BorrowerName;
                existingLoan.LenderName = loan.LenderName;
                existingLoan.Amount = loan.Amount;
                existingLoan.IsSettled = loan.IsSettled;
            }
            return RedirectToAction("Index");
        }

        // GET: LoanController/Delete/5
        public ActionResult Delete(int id)
        {
            var loan = loans.FirstOrDefault(x => x.Id == id);

            if(loan != null)
            {
                loans.Remove(loan);
            }

            return RedirectToAction("Index");
        }

        // POST: LoanController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
