using Microsoft.AspNetCore.Mvc;
using librawry.portable;
using librawry.portable.repo.titles;

namespace librawry.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TitlesController : ControllerBase {
	private readonly IUnitOfWork unitOfWork;

	public TitlesController(IUnitOfWork unitOfWork) {
		this.unitOfWork = unitOfWork;
	}

	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesDefaultResponseType]
	public async Task<ActionResult<DetailsResponse>> Details([FromRoute] int id) {
		var details = await unitOfWork.TitleRepository.GetDetails(id);
		if (details == null) {
			return NotFound();
		}
		return Ok(details);
	}

	[HttpPost]
	public async Task<ActionResult<IEnumerable<ListResponse>>> List([FromBody] ListRequest param) {
		return Ok(await unitOfWork.TitleRepository.GetList(param));
	}
}
