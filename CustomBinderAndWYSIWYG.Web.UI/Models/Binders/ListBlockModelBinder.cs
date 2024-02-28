
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CustomBinderAndWYSIWYG.Web.UI.Models.Binders
{
	public class ListBlockModelBinder : IModelBinder
	{
		private readonly ILogger _logger;

		public ListBlockModelBinder(ILoggerFactory loggerFactory)
		{
			if (loggerFactory == null)
			{
				throw new ArgumentNullException(nameof(loggerFactory));
			}

			_logger = loggerFactory.CreateLogger<ListBlockModelBinder>();
		}

		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			if (bindingContext == null)
			{
				throw new ArgumentNullException(nameof(bindingContext));
			}

			var request = bindingContext.HttpContext.Request;
			if (!request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
			{
				bindingContext.Result = ModelBindingResult.Success(null);
				return Task.CompletedTask;
			}

			var blocks = new List<Block>();

			var form = bindingContext.HttpContext.Request.Form;
			var editKeys = form.Keys.Where(k => k.StartsWith("edit_"));

			foreach (var key in editKeys)
			{
				var value = form[key];
				if (!string.IsNullOrEmpty(value))
				{
					blocks.Add(new Block { Content = value });
				}
			}

			bindingContext.Result = ModelBindingResult.Success(blocks);
			return Task.CompletedTask;
		}
	}
}
