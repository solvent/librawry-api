using System.Text.Json.Serialization;

namespace librawry_api.Classes.Entities.Controllers.Titles {

	public class DetailsResponseTitle {

		[JsonPropertyName("id")]
		public int Id {
			get; set;
		}

		[JsonPropertyName("name")]
		public string Name {
			get; set;
		}

		[JsonPropertyName("episodes")]
		public IEnumerable<DetailsResponseEpisode> Episodes {
			get; set;
		}

		[JsonPropertyName("tags")]
		public IEnumerable<DetailsResponseTag> Tags {
			get; set;
		}
	}
}
