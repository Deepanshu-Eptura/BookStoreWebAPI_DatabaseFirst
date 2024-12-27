using BookStoreWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<Author> Get()
        {
            using (var context = new BookStoreContext())
            {
                //to get all author
                //return context.Author.ToList();

                //add a Author
                //Author author = new Author();
                //author.FirstName = "Avi";
                //author.LastName = "Saini";


                //context.Author.Add(author);

                ////to update the author
                //Author author = context.Author.Where(auth => auth.FirstName == "Avi").FirstOrDefault();
                //author.Phone = "12346-77777";

                // remove author
                Author author = context.Author.Where(auth => auth.FirstName =="Avi").FirstOrDefault();
                context.Author.Remove(author);


                context.SaveChanges();

                ////to get author by id
                return context.Author.Where(auth => auth.FirstName == "Avi").ToList();
            }

        }
    }
}
