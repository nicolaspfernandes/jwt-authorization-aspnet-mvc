namespace JwtMvcAuthorization.Controllers {

    using Token;
    using Models;
    using Filters;

    using System;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Login controller with actions that will control authorization flow. Creates cookies with JWT token and removes it when needed.
    /// </summary>
    public class LoginController : Controller {
        
        /// <summary>
        /// Login action that will receive a role from the main view, creates a new JWT token and attach it to a new http cookie.
        /// </summary>
        /// <param name="model">LoginViewModel model with login parameters.</param>
        /// <returns>Action to Home/Index view.</returns>
        [HttpPost, JwtAuthorization]
        public RedirectToRouteResult Login(LoginViewModel model) {

            var accessToken = new AccessToken("testing", model.Role, TokenExpirations.Daily);
            var authorizationCookie = new HttpCookie(authorizationCookieName, accessToken.ToTokenString());

            HttpContext.Response.Cookies.Add(authorizationCookie);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Logout action that will remove the authorization cookie from the user browser and clean current permissions of them.
        /// </summary>
        /// <returns>Action to Home/Index view.</returns>
        [HttpPost, JwtAuthorization]
        public RedirectToRouteResult Logout() {

            var authorizationCookie = HttpContext.Response.Cookies.Get(authorizationCookieName);
            authorizationCookie.Expires = DateTime.Now.AddDays(-1d);
            return RedirectToAction("Index", "Home");
        }

        private string authorizationCookieName = "JwtAuthorizationToken";
    }
}