using ModelsLayer.DatabaseEntities;
using ModelsLayer.ModelsDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public interface IUserPostsService
    {
        Task CreateNewPostAsync(CreatePostModelDTO newPost);
        IEnumerable<UserPost> GetAllUserPosts(string userID);
        
    }
}