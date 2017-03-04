namespace JwtMvcAuthorization.Controllers {

    using Token;
    using Filters;
    
    using System.Web.Mvc;

    /// <summary>
    /// Home controller with actions to return the views we will use in the application.
    /// </summary>
    public class HomeController : Controller {
        
        /// <summary>
        /// Index action. Returns the main view that contains everything to test the authorization methods.
        /// </summary>
        /// <returns>Main view.</returns>
        public ViewResult Index() {
            return View();
        }
        
        /// <summary>
        /// Common user page. Returns a view when the user clicks "Go to Common User Page" in the main page. Shows content for common users.
        /// To access this page, the permission should be one of the following: Common, Superuser or Administrator.
        /// </summary>
        /// <returns>Common user View.</returns>
        [JwtAuthorization(Roles.Common, Roles.Superuser, Roles.Administrator)]
        public ViewResult CommonUserPage() {
            return View();
        }

        /// <summary>
        /// Superuser page. Returns a view when the user clicks "Go to Superuser Page" in the main page. Shows content for superusers.
        /// To access this page, the permission should be one of the following: Superuser or Administrator.
        /// </summary>
        /// <returns>Superuser View.</returns>
        [JwtAuthorization(Roles.Superuser, Roles.Administrator)]
        public ViewResult SuperuserPage() {
            return View();
        }

        /// <summary>
        /// Administrator page. Returns a view when the user clicks "Go to Administrator Page" in the main page. Shows content only for administrators.
        /// To access this page, the permission should be Administrator.
        /// </summary>
        /// <returns>Administrator View.</returns>
        [JwtAuthorization(Roles.Administrator)]
        public ViewResult AdministratorPage() {
            return View();
        }

        /// <summary>
        /// Anonymous page. Returns a view when the user clicks "Go to Anonymous Page" in the main page. Shows content for everyone.
        /// There is no filter in this page. Any type of user can access it.
        /// </summary>
        /// <returns>Anonymous View.</returns>
        [JwtAuthorization(Roles.Anonymous)]
        public ViewResult AnonymousPage() {
            return View();
        }

        /// <summary>
        /// Forbidden page. Returns a view with "Forbidden" message when the user does not have the permission for a specific view.
        /// </summary>
        /// <returns>Forbidden View.</returns>
        public ViewResult ForbiddenPage() {
            return View();
        }
    }
}