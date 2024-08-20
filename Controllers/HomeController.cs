using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Dynamic;
using WebApplication1.Models;
using WebApplication1.Models.DAL;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsersRepository _usersRepository;
        private readonly IGroupRepository _groupRepository;

        public HomeController(ILogger<HomeController> logger, IUsersRepository userRepo, IGroupRepository groupRepo)
        {
            _logger = logger;
            _usersRepository  = userRepo;
            _groupRepository = groupRepo;
        }

        public IActionResult Index()
        {
            var users = _usersRepository.GetUsers().ToList();
            List<UserViewModel> userList = new List<UserViewModel>();

            foreach (var i in users)
            {
                userList.Add(new UserViewModel
                {
                    Id = i.Id,
                    Name = i.Name!,
                    Email = i.Email!,
                    GroupId = i.GroupId,
                    GroupName = _groupRepository.GetGroupById(i.GroupId)!.Name
                });
            }            

            return View(userList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public ActionResult Create()
        {
            List<Group> groups = _groupRepository.GetGroups().ToList();

            List<SelectListItem> selectListItems = new List<SelectListItem>();

            selectListItems = groups.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            var tuple = new Tuple<User, List<SelectListItem>>(new User(), selectListItems);
            return View(tuple);

        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "Item1")] User user)
        {
            _usersRepository.InsertUser(user);
            _usersRepository.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Update(int Id)
        {
            User? user = _usersRepository.GetUserById(Id);

            List<Group> groups = _groupRepository.GetGroups().ToList();

            List<SelectListItem> selectListItems = groups.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            UserEditModel userEditModel = new()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                GroupId = selectListItems
            };

            return View(userEditModel);
        }
        [HttpPost]
        public ActionResult Update([Bind]User user)
        {
            _usersRepository.UpdateUser(user);
            _usersRepository.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int Id)
        {
            _usersRepository.DeleteUser(Id);
            _usersRepository.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
