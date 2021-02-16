using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer.Services;
using FunAndGames.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelsLayer.DatabaseEntities;
using ModelsLayer.Identity;
using ModelsLayer.ModelsDTO;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FunAndGames.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;
        private readonly UserService _service;
        private readonly IUserPostsService _userPostsService;
        private readonly IMapper _mapper;

        public HomeController(
            UserManager<ApplicationUser> userManager, 
            AppDbContext context, 
            UserService service,
            IUserPostsService userPostsService, 
            IMapper mapper
            )
        {
            _userManager = userManager;
            _context = context;
            _service = service;
            _userPostsService = userPostsService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var indexModel = new CreatePostModel();
            indexModel.UserPosts = _userPostsService.GetAllUserPosts(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return View(indexModel);
        }
        public void SetHeartbeat(string currentuserid, string username)
        {
            _service.AddNewHeartbeat(currentuserid, username);
        }
        public JsonResult GetAllOnlineUsers()
        {
            var ResultJson = JsonConvert.SerializeObject(_service.GetAllOnlineUsers());
            return Json(ResultJson);
        }
        public void SendMessage(string currentuserid, string username, string message)
        {
            _service.SendMessage(currentuserid, username, message);
        }

        public IActionResult PersonToSearch(string personToSearch)
        {
            

            return View();
        }
        public IActionResult Auth()
        {


            return View();
        }

        public async Task<IActionResult> CreatePost(CreatePostModel newPost)
        {
            newPost.userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _userPostsService.CreateNewPostAsync(_mapper.Map<CreatePostModelDTO>(newPost));

            return RedirectToAction("Index");
        }
    }
}
