using System.Text.Json.Serialization;

namespace librawry_api.Classes.Entities.Controllers.Titles {

	public class ListResponseTitle {

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
