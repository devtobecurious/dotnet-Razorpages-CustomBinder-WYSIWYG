using CustomBinderAndWYSIWYG.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace CustomBinderAndWYSIWYG.Web.UI
{
	public class TestTinyMCEModel(IOptions<Keys> keysOptions) : PageModel
	{
		private Keys keys = keysOptions.Value;

		public void OnGet()
		{
			ViewData[nameof(Keys)] = keys;
		}

		//public async Task<IActionResult> OnPostUploadImageAsync(IFormFile file)
		//{
		//	if (file == null || file.Length == 0)
		//	{
		//		return BadRequest("Le fichier est obligatoire.");
		//	}

		//	// Logique d'upload simplifiée ici...

		//	return new JsonResult(new { location = "/chemin/vers/limage.jpg" });
		//}

		public async Task<IActionResult> OnPostUploadImageAsync()
		{
			var file = Request.Form.Files.GetFile("file"); // "file" est le nom du champ attendu par TinyMCE
			if (file != null && file.Length > 0)
			{
				var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", file.FileName);

				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await file.CopyToAsync(stream);
				}

				var url = $"{Request.Scheme}://{Request.Host}/images/{file.FileName}";
				return new JsonResult(new { location = url });
			}

			return BadRequest("Aucun fichier n'a été uploadé.");
		}
	}
}
