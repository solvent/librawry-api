using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using librawry.portable;

namespace librawry_api.Controllers {

	[ApiController]
	[Route("[controller]")]
	public class SearchController : ControllerBase {
		private readonly LibrawryContext _context;

		public SearchController(LibrawryContext context) {
			_context = context;
		}

		[HttpPost]
		public async Task<ActionResult<List<Title>>> Search(string query) {
			if (string.IsNullOrEmpty(query) || query.Length < 3) {
				return BadRequest("Please use at least 3 characters length string to search.");
			}

			var search = string.Join("%", query.Split(' '));

			return await _context.Titles
				.Where(x => EF.Functions.Like(x.Name, $"%{search}%") ||
					x.Episodes.Any(y => EF.Functions.Like(y.Name, $"%{search}%")))
				.OrderBy(x => x.Name)
				.Select(x => new Title {
					Id = x.Id,
					Name = x.Name
				})
				.ToListAsync();
		}

		public class Title {
			public int Id {
				get; set;
			}

			public string Name {
				get; set;
			}
		}
	}
}
