using Microsoft.AspNetCore.Mvc;
using librawry.portable;
using librawry.portable.repo.titles;

namespace librawry_api.Controllers {

	[ApiController]
	[Route("api/[controller]")]
	public class TitlesController : ControllerBase {
		private readonly IUnitOfWork unitOfWork;

		public TitlesController(IUnitOfWork unitOfWork) {
			this.unitOfWork = unitOfWork;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<DetailsResponse>> Details([FromRoute] int id) {
			return await unitOfWork.TitleRepository.GetDetails(id);
		}

		[HttpPost]
		public async Task<ActionResult<IEnumerable<ListResponse>>> List([FromBody] ListRequest param) {
			return Ok(await unitOfWork.TitleRepository.GetList(param));
		}
	}
}
