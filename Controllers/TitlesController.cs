using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using librawry.portable;
using librawry_api.Classes.Entities.Controllers.Titles;

namespace librawry_api.Controllers {

	[ApiController]
	[Route("api/[controller]")]
	public class TitlesController : ControllerBase {
		private readonly LibrawryContext _context;

		public TitlesController(LibrawryContext context) {
			_context = context;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<DetailsResponseTitle>> Details([FromRoute] int id) {
			return await _context.Titles
				.Where(x => x.Id == id)
				.Select(x => new DetailsResponseTitle {
					Id = x.Id,
					Name = x.Name,
					Tags = x.TagRefs
						.Select(y => y.Tag)
						.Select(y => new DetailsResponseTag {
							Id = y.Id,
							Name = y.Name
						}),
					Episodes = x.Episodes
						.Select(y => new DetailsResponseEpisode {
							Id = y.Id,
							Name = y.Name
						})
				})
				.AsSplitQuery()
				.FirstOrDefaultAsync();
		}

		[HttpPost]
		public async Task<ActionResult<List<ListResponseTitle>>> List([FromBody] ListRequest requestBody) {
			if (requestBody == null) {
				return BadRequest();
			}
			if (string.IsNullOrEmpty(requestBody.Search) || requestBody.Search.Length < 3) {
				return BadRequest("Please use at least 3 characters length string to search.");
			}

			var search = string.Join("%", requestBody.Search.Split(' '));

			return await _context.Titles
				.Where(x => EF.Functions.Like(x.Name, $"%{search}%") ||
					x.Episodes.Any(y => EF.Functions.Like(y.Name, $"%{search}%")))
				.OrderBy(x => x.Name)
				.Select(x => new ListResponseTitle {
					Id = x.Id,
					Name = x.Name
				})
				.ToListAsync();
		}
	}
}
