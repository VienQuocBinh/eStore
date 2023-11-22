using BusinessObject;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace eStore.Controllers
{
    public class MembersController : Controller
    {
        private MemberRepo repo = null;

        public MembersController(EStoreContext context)
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string connectionString = configuration.GetConnectionString("Database");
            repo = new MemberRepo(connectionString);
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            List<Member> members = repo.GetMembers();
            return View(members);
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || repo.GetMembers() == null)
            {
                return NotFound();
            }

            var member = repo.GetMembers().FirstOrDefault(member => member.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }



        // GET: Members/Create
        public IActionResult Create()
        {
            ViewData["ID"] = repo.GetLatestMemberId() + 1;
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,Email,CompanyName,City,Country,Password")] Member member)
        {
            if (ModelState.IsValid)
            {
                var session = HttpContext.Session;
                session.Clear();
                string regex = "^[a-zA-Z0-9]+.@gmail.com$";
                if (!Regex.IsMatch(member.Email, regex))
                {
                    session.SetString("CreateMemberError", "Email is incorrect format");
                    return View(member);
                }

                repo.AddNewMember(member);
                repo.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || repo.GetMembers() == null)
            {
                return NotFound();
            }

            var member = repo.GetMembers().FirstOrDefault(member => member.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            else
            {
                return View(member);
            }
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,Email,CompanyName,City,Country,Password")] Member member)
        {
            if (id != member.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var session = HttpContext.Session;
                    session.Clear();
                    string regex = "^[a-zA-Z0-9]+.@gmail.com$";
                    if (!Regex.IsMatch(member.Email, regex))
                    {
                        session.SetString("EditMemberError", "Email is incorrect format");
                        return RedirectToAction(nameof(Edit));
                    }
                    repo.UpdateMemberInfo(member);
                    repo.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.MemberId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }
        public async Task<IActionResult> UserOrder(int? id)
        {
            IOrderRepo orderRepo = new OrderRepo();
            if (id != 0)
            {
                List<Order> order = orderRepo.GetOrders()
                    .Where(a => a.MemberId == id).ToList();
                if (order.Count == 0)
                {
                    TempData["empty"] = "This user have no orders";
                    return RedirectToAction(nameof(UserDetail));
                }
                else
                {
                    return View(order);
                }
            }
            else
            {
                TempData["empty"] = "This user have no orders";
                return RedirectToAction(nameof(UserDetail));
            }

        }

        public async Task<IActionResult> UserEdit(int? id)
        {
            if (id == null || repo.GetMembers() == null)
            {
                return NotFound();
            }

            var member = repo.GetMembers().FirstOrDefault(member => member.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEdit(int id, [Bind("MemberId,Email,CompanyName,City,Country,Password")] Member member)
        {
            if (id != member.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    repo.UpdateMemberInfo(member);
                    repo.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.MemberId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(UserDetail));
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || repo.GetMembers() == null)
            {
                return NotFound();
            }

            var member = repo.GetMembers().FirstOrDefault(member => member.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (repo.GetMembers() == null)
            {
                return Problem("Entity set 'EStoreContext.Members'  is null.");
            }
            var member = repo.GetMembers().FirstOrDefault(member => member.MemberId == id);
            if (member != null)
            {
                repo.DeleteMember(member);
                repo.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UserDetail()
        {
            var member = repo.GetMemberByEmail(HttpContext.Session.GetString("email"));
            return View(member);
        }

        private bool MemberExists(int id)
        {
            return repo.GetMembers().Exists(member => member.MemberId == id);
        }
    }
}
