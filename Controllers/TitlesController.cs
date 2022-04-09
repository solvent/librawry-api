using Microsoft.AspNetCore.Mvc;
using librawry.portable.ef;
using librawry.portable.repo;
using librawry.portable.repo.titles;
using System.Text.Json.Serialization;

namespace librawry_api.Controllers {

	[ApiController]
	[Route("api/[controller]")]
	public class TitlesController : ControllerBase {
		private readonly LibrawryContext _context;

		public TitlesController(LibrawryContext context) {
			_context = context;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<DetailsResponse>> Details([FromRoute] int id) {
			return await new TitleRepository(_context).GetDetails(id);
		}

		[HttpPost]
		public async Task<ActionResult<IEnumerable<ListResponse>>> List([FromBody] ListRequest requestBody) {
			if (requestBody == null) {
				return BadRequest();
			}

			if (string.IsNullOrEmpty(requestBody.Search) || requestBody.Search.Length < 3) {
				return BadRequest("Please use at least 3 characters length string to search.");
			}

			return Ok(await new TitleRepository(_context).GetList(requestBody.Search));
		}
	}

	public class ListRequest {
		[JsonPropertyName("search")]
		public string Search {
			get; set;
		}
	}
}
