using System.Text.Json.Serialization;

namespace librawry_api.Classes.Entities.Controllers.Titles {

	public class DetailsResponseTag {

		[JsonPropertyName("id")]
		public int Id {
			get; set;
		}

		[JsonPropertyName("name")]
		public string Name {
			get; set;
		}
	}
}