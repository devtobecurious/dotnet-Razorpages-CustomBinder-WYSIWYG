using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CustomBinderAndWYSIWYG.Web.UI.Models.Binders
{
	public class ListBlockModelBinderProvider : IModelBinderProvider
	{
		public IModelBinder? GetBinder(ModelBinderProviderContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			var loggerFactory = context.Services.GetRequiredService<ILoggerFactory>();
			return new ListBlockModelBinder(loggerFactory);
		}
	}
}
