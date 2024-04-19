using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Models;
using TodoList.Models.Entities;

namespace TodoList.Controllers
{
	public class UserController : Controller
	{
        private readonly ApplicationDbContext dbContext;

        public UserController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

       

        [HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddTodoViewModel viewModel)
		{
			var user = new User
			{
				Title = viewModel.Title,
				Description = viewModel.Description,
				Date = viewModel.Date
			};

			await dbContext.Users.AddAsync(user);
			await dbContext.SaveChangesAsync();

			return View();
		}


		[HttpGet]
		public async Task<IActionResult> List()
		{
			var users = await dbContext.Users.ToListAsync();

			return View(users);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			var user = await dbContext.Users.FindAsync(id);

			return View(user);

		}

		[HttpPost]
		public async Task<IActionResult> Edit(User viewModel)
		{
			var user = await dbContext.Users.FindAsync(viewModel.Id);

			if(user is not null)
			{
				user.Title = viewModel.Title;
				user.Description = viewModel.Description;
				user.Date = viewModel.Date;

				await dbContext.SaveChangesAsync();
			}

			return RedirectToAction("List", "User");
		}

		[HttpPost]
		public async Task<IActionResult> Delete(User viewModel)
		{
			var user = await dbContext.Users
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == viewModel.Id);

			if(user is not null )
			{
				dbContext.Users.Remove(viewModel);
				await dbContext.SaveChangesAsync(true);
			}

			return RedirectToAction("List", "User");
		}
	}
}
