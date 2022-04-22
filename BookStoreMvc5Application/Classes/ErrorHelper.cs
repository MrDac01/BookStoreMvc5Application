using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BookStoreMvc5Application.Classes
{
    public class ErrorHelper
    {
        public ErrorHelper()
        {

        }

		/// <summary>
		/// Show viewName for error
		/// </summary>
		/// <param name="httpContext"></param>
		/// <param name="httpCode"></param>
		public void ProcessError(HttpContext httpContext, int httpCode, Exception exception)
		{
			ProcessError(new HttpContextWrapper(httpContext), httpCode, exception);
		}

		/// <summary>
		/// Show viewName for error
		/// </summary>
		/// <param name="httpContext"></param>
		/// <param name="httpCode"></param>
		public void ProcessError(HttpContextBase httpContext, int httpCode, Exception exception)
		{
			string actionName = MatchCodeToAction(httpCode);

			DoProcessError(httpContext, actionName, exception);
		}

		private void DoProcessError(HttpContextBase httpContext, string actionName, Exception exception)
		{
			var routeData = new System.Web.Routing.RouteData();

			routeData.Values["language"] = System.Globalization.CultureInfo.CurrentCulture.ToString();
			routeData.Values["area"] = string.Empty;
			routeData.Values["controller"] = "Error";
			routeData.Values["action"] = actionName;

            var requestContext = new System.Web.Routing.RequestContext(httpContext, routeData)
            {
                RouteData = routeData
            };
            requestContext.HttpContext.Items.Add("exception", exception);

			var controllerFactory = System.Web.Mvc.ControllerBuilder.Current.GetControllerFactory();
			var controller = controllerFactory.CreateController(requestContext, "Error");

			controller.Execute(requestContext);
			controllerFactory.ReleaseController(controller);
		}

		public void ProcessServiceError(HttpContext httpContextBase, System.Exception exception)
		{
			DoProcessError(new HttpContextWrapper(httpContextBase), "ServiceError", exception);
		}

		private static string MatchCodeToAction(int httpCode)
		{
			switch (httpCode)
			{
				case 403:
					return "HttpError403";
				case 404:
					// page not found
					return "HttpError404";
				case 500:
					// server error
					return "HttpError500";
				default:
					return "General";
			}
		}

		/// <summary>
		/// Получить объект данных ошибки для JSON-ответа
		/// </summary>
		/// <param name="e"></param>
		/// <param name="message"></param>
		/// <param name="extra"></param>
		/// <param name="jsonRequestBehavior"></param>
		/// <returns></returns>
		public static object GetErrorJsonObject(Exception e, string message, object extra)
		{
			StringBuilder sb = new StringBuilder();
			ExceptionDetails ex = null;

			if (e == null && string.IsNullOrEmpty(message) && extra == null)
			{
				throw new ArgumentNullException("e", "Одновременно пустое исключение и сообщение.");
			}

			if (!string.IsNullOrEmpty(message))
			{
				sb.AppendLine(message);
			}

			if (e != null)
			{
				ex = e.ToExceptionDetails();

				ex.Inner = UnwrapException(ex, e);

			}

			return new
			{
				status = false,
				success = false,
				error = ex,
				message = sb.ToString(),
				extra
			};
		}

		/// <summary>
		/// Разорачивание исключения
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		/// <returns></returns>
		private static ExceptionDetails UnwrapException(ExceptionDetails source, Exception e)
		{
			e = e.InnerException;

			if (e != null)
			{
				source.Inner = source.Inner = e.ToExceptionDetails();

				return UnwrapException(source.Inner, e);
			}

			return null;
		}
	}

	/// <summary>
	/// Расщирения для исключений
	/// </summary>
	static class ExceptionExtensions
	{
		/// <summary>
		/// В детали исключения
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public static ExceptionDetails ToExceptionDetails(this Exception e)
		{
			return new ExceptionDetails
			{
				Source = e.Source,
				Trace = e.StackTrace,
				Message = e.Message,
			};
		}
	}

	/// <summary>
	/// Детали исключения, упрощенный вариант
	/// </summary>
	class ExceptionDetails
	{
		/// <summary>
		/// Источник
		/// </summary>
		public string Source { get; set; }
		/// <summary>
		/// Строка трассировки
		/// </summary>
		public string Trace { get; set; }

		/// <summary>
		/// Сообщение
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Вложенное исключение
		/// </summary>
		public ExceptionDetails Inner { get; set; }
	}
}