using FubuMVC.Core;
using FubuMVC.Core.View;

namespace FubuDinner.Web
{
    public static class NerdDinnerPageExtensions
    {
        /// <summary>
        /// Resolves a URL path for client consumption
        /// </summary>
        /// <param name="viewPage"></param>
        /// <param name="url">The original URL. A tilde (~) prefix will resolve to the application path</param>
        /// <returns></returns>
        public static string Content(this IFubuView viewPage, string url)
        {
            return url.ToFullUrl();
        }

        /// <summary>
        /// Renders an HTML link tag to include the specified Cascasding Style Sheet from the application's CSS folder
        /// </summary>
        /// <param name="viewPage"></param>
        /// <param name="url">The name of the CSS file, relative to application CSS folder</param>
        /// <returns></returns>
        public static string CSS(this IFubuView viewPage, string url)
        {
            var baseFolder = Content(viewPage, "~/Content/");
            const string template = @"<link href=""{0}{1}"" rel=""stylesheet"" type=""text/css"" media=""screen""/>";

            return template.ToFormat(baseFolder, url);
        }

        /// <summary>
        /// Renders an HTML script tag to include the specified javascript file from the application's script folder
        /// </summary>
        /// <param name="viewPage"></param>
        /// <param name="url">The name of the javascript file, relative to the application's script folder</param>
        /// <returns></returns>
        public static string Script(this IFubuView viewPage, string url)
        {
            var baseFolder = Content(viewPage, "~/Scripts/");
            const string template = @"<script type=""text/javascript"" src=""{0}{1}""></script>";

            return template.ToFormat(baseFolder, url);
        }
    }
}