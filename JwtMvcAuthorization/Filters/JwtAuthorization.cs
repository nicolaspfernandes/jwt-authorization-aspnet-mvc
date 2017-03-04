namespace JwtMvcAuthorization.Filters {

    using Token;

    using System.Net;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Collections.Generic;
    using Controllers;

    /// <summary>
    /// Class to support ActionFilterAttribute in order to control access to pages by given roles.
    /// </summary>
    public class JwtAuthorization : ActionFilterAttribute {
        
        /// <summary>
        /// Default constructor. Receives a list of roles that should be checked against authorization cookie.
        /// </summary>
        /// <param name="roles">List of roles.</param>
        public JwtAuthorization(params Roles[] roles) {

            Roles = new List<Roles>();
            Roles.AddRange(roles);
        }

        /// <summary>
        /// Overriding implementation of OnActionExecuting method. Checks if the rules matches with the 
        /// permissions allowed by the authorization cookie and set the value to the base controller. 
        /// If not, send the user right away to a forbidden page.
        /// </summary>
        /// <param name="filterContext">ActionExecutingContext instance to work with Http data.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            
            if (Roles.Count > 0 && !Roles.Contains(Token.Roles.Anonymous)) {

                var authorizationCookie = filterContext.HttpContext.Request.Cookies[JwtAuthorizationCookieName];
                if (authorizationCookie != null && authorizationCookie.Value != null) {

                    var accessToken = new AccessToken(authorizationCookie.Value);
                    if (!accessToken.IsValid || !Roles.Contains(accessToken.Role)) {
                        RedirectToForbiddenPage(filterContext);
                    }
                } else {
                    RedirectToForbiddenPage(filterContext);
                }
            }
        }

        /// <summary>
        /// Method that uses HttpContext to send the user back to forbidden page, in case of permission denied by the filter.
        /// </summary>
        /// <param name="filterContext">ActionExecutingContext instance to work with Http data.</param>
        private void RedirectToForbiddenPage(ActionExecutingContext filterContext) {

            filterContext.HttpContext.Response.StatusCode = (int) HttpStatusCode.Forbidden;
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new {
                action = "ForbiddenPage",
                controller = "Home"
            }));
        }

        private List<Roles> Roles { get; set; }
        private readonly string JwtAuthorizationCookieName = "JwtAuthorizationToken";
    }
}