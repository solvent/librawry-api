using System.Text.Json.Serialization;

namespace librawry_api.Classes.Entities.Controllers.Titles {

	public class ListRequest {

		[JsonPropertyName("search")]
		public string Search {
			get; set;
		}
	}
}
