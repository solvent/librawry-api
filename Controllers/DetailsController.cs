using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using librawry.portable;

namespace librawry_api.Controllers {

	[ApiController]
	[Route("api/[controller]")]
	public class DetailsController : ControllerBase {
		private readonly LibrawryContext _context;

		public DetailsController(LibrawryContext context) {
			_context = context;
		}

		[HttpPost]
		public async Task<ActionResult<Title>> Details(int id) {
			return await _context.Titles
				.Include("TagRefs.Tag")
				.Include("Episodes")
				.Where(x => x.Id == id)
				.Select(x => new Title {
					Id = x.Id,
					Name = x.Name,
					Tags = x.TagRefs
						.Select(y => y.Tag)
						.Select(y => new Tag {
							Id = y.Id,
							Name = y.Name
						}),
					Episodes = x.Episodes
						.Select(y => new Episode {
							Id = y.Id,
							Name = y.Name
						})
				})
				.FirstOrDefaultAsync();
		}

		public class Title {
			public int Id {
				get; set;
			}

			public string Name {
				get; set;
			}

			public IEnumerable<Episode> Episodes {
				get; set;
			}

			public IEnumerable<Tag> Tags {
				get; set;
			}
		}

		public class Tag {
			public int Id {
				get; set;
			}

			public string Name {
				get; set;
			}
		}

		public class Episode {
			public int Id {
				get; set;
			}

			public string Name {
				get; set;
			}
		}
	}
}
