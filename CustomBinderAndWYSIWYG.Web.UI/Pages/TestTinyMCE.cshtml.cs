using CustomBinderAndWYSIWYG.Web.UI.Models;
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
    }
}
