## JWT Authorization ASP.NET MVC

Simple application in order to show how to use JWT to control authorization flow into applications.
The authorization token is created based on a given role. It is sent to the requests through Authorization header attribute.

The ActionFilter will check the headers in order to find an Authorization attribute with value and check it against the verification logic.
If the request is denied, the response will send a **403 (Forbidden)** status back to the user.

## Testing the application

- Choose a role type in the main page. Whenever you choose a new role type, the token will be changed to reflect that role.
- Try to access the pages in the right corner of the application, based on the role you have chosen.

##Built with

- .NET framework 4.5.2
- Visual Studio 2015 Express
- Implementation of JWT for .NET [here] (https://github.com/nicolaspfernandes/jwt)
