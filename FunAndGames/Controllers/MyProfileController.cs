using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer.Services;
using FunAndGames.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using ModelsLayer.ModelsDTO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FunAndGames.Controllers
{
    public class MyProfileController : Controller
    {
        private readonly IUserPostsService _userPostsService;
        private readonly IMapper _mapper;

        public MyProfileController(IUserPostsService userPostsService, IMapper mapper)
        {
            _userPostsService = userPostsService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var indexModel = new CreatePostModel();
            indexModel.UserPosts= _userPostsService.GetAllUserPosts(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return View(indexModel);
        }

        public async Task<IActionResult> CreatePost(CreatePostModel newPost)
        {
            newPost.userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _userPostsService.CreateNewPostAsync(_mapper.Map<CreatePostModelDTO>(newPost));

            return RedirectToAction("Index");
        }

    }
}