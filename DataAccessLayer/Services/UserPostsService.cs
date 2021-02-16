using ModelsLayer.DatabaseEntities;
using ModelsLayer.ModelsDTO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public class UserPostsService : IUserPostsService
    {
        private readonly AppDbContext _context;
        public UserPostsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateNewPostAsync(CreatePostModelDTO newPost)
        {
            var userPost = new UserPost
            {
                UserId = newPost.userID,
                postMessage = newPost.postMessage,
                When = newPost.StartDate
            };


            //ako posta sliku
            if (newPost.image != null && newPost.image.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString();
                var filePath = Path.Combine(Directory.GetCurrentDirectory()
                    , "wwwroot\\img\\postsImages", fileName + ".png");
                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await newPost.image.CopyToAsync(fileSteam);
                }
                userPost.ImagePath = fileName;
            }

            //string findLink = Regex.Matches(newPost.postMessage, @"(http|ftp|https):\/\/([\w\-_]+(?:(?:\.[\w\-_]+)+))([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?")[0].ToString();

            if (newPost.postMessage != null)
            {
                Match match = Regex.Match(newPost.postMessage, @"https\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(/\S*)?$");
                string findLink = match.Value;


                if (findLink.Length > 0)
                {
                    string linkName = Guid.NewGuid().ToString();

                    ////website preview
                    ChromeOptions options = new ChromeOptions();
                    options.AddArgument("headless");//Comment if we want to see the window. 
                    var ass = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options);
                    driver.Navigate().GoToUrl(findLink);
                    var screenshot = (driver as ITakesScreenshot).GetScreenshot();
                    screenshot.SaveAsFile("wwwroot\\img\\linkImages\\" + linkName + ".png");
                    driver.Close();
                    driver.Quit();
                    ////

                    userPost.linkImagePath = linkName;
                    userPost.linkURL = findLink;
                }

            }
            _context.UserPosts.Add(userPost);
            _context.SaveChanges();
        }

        public IEnumerable<UserPost> GetAllUserPosts(string userID)
        {
            return _context.UserPosts.Where(a => a.UserId == userID);
        }
    }
}
