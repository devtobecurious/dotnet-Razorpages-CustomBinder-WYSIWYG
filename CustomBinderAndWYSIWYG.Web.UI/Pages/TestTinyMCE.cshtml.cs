using CustomBinderAndWYSIWYG.Web.UI.Models;
using CustomBinderAndWYSIWYG.Web.UI.Models.Binders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace CustomBinderAndWYSIWYG.Web.UI
{
	public class TestTinyMCEModel(IOptions<Keys> keysOptions) : PageModel
	{
		#region Fields
		private Keys keys = keysOptions.Value;
		#endregion

		#region Properties
		[ModelBinder(binderType: typeof(ListBlockModelBinder))]
		public List<Block> Blocks { get; set; }
		#endregion

		#region Public methods
		public void OnGet()
		{
			ViewData[nameof(Keys)] = keys;
		}

		public void OnPost()
		{

		}

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
		#endregion
	}
}
