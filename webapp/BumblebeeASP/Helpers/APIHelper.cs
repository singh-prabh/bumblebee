using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using BumblebeeASP.Models;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;

namespace BumblebeeASP.Helpers
{
    class TokenResponse
    {
        public string accessToken { get; set; }
    }
    
    public static class APIHelper
    {
        //status codes
        static HttpStatusCode UnauthorizedCode = HttpStatusCode.Unauthorized;
        static HttpStatusCode OKCode = HttpStatusCode.OK;
        static HttpStatusCode CreatedCode = HttpStatusCode.Created;
        static HttpStatusCode InternalErrorCode = HttpStatusCode.InternalServerError;

        //get API URL from Environment 
        static string BaseURL = System.Environment.GetEnvironmentVariable("BASEURL");

        //jwt token
        public static string ClientToken { get; set; }

        //rest client
        static RestClient RestClient { get; set; }

        //setup the rest client
        public static void SetupAPIHelper()
        {
            RestClient = new RestClient();
            RestClient.BaseUrl = new Uri(BaseURL);
            RestClient.AddHandler("application/json", new JsonDeserializer());
        }

        //return valid token for api
        public static string CheckToken(LoginModel loginModel)
        {
            var TokenString = "";
            //if token is null try to get a new one
            if (ClientToken == null)
            {
                PrinterClass.printErrorMessage("token is empty, get a new one: " + ClientToken);
                TokenString = GetNewToken(loginModel.LoginEmail, loginModel.LoginPassword);
            }
            //token not null, check if expired
            else if (ClientToken != null)
            {
                var WebToken = new JwtSecurityToken(ClientToken);
                var ExpirationTime = WebToken.ValidTo;
                var CreatedTime = WebToken.ValidFrom;
                var CurrentTime = new DateTime();
                if (CurrentTime.IsBetween(CreatedTime, ExpirationTime))
                {
                    PrinterClass.printDebugMessage("the token is still valid : " + WebToken);
                    TokenString = ClientToken;
                }
                else
                {
                    PrinterClass.printErrorMessage("this token has expired");
                    TokenString = GetNewToken(loginModel.LoginEmail, loginModel.LoginPassword);
                }
            }
            PrinterClass.printDebugMessage("returning token value: " + TokenString);
            return TokenString;
        }

        //get token from api
        private static string GetNewToken(string email, string password)
        {
            var TokenResponse = "";
            //setup rest request
            RestRequest AuthRequest = new RestRequest("/authentication", Method.POST);
            AuthRequest.AddParameter("email", email);
            AuthRequest.AddParameter("password", password);
            AuthRequest.AddParameter("strategy", "local");
            AuthRequest.RequestFormat = DataFormat.Json;
            //get response
            var AuthResponse = RestClient.Execute<TokenResponse>(AuthRequest);
            //check response
            var StatusCode = AuthResponse.StatusCode;
            //return generic error
            if (StatusCode == UnauthorizedCode)
            {
                TokenResponse = "error";
            }
            else if (StatusCode == CreatedCode)
            {
                //get token from response
                string content = AuthResponse.Data.accessToken;
                ClientToken = content;
                TokenResponse = content;
                PrinterClass.printDebugMessage("new token created and saved: " + ClientToken);
            }
            return TokenResponse;
        }
        //register new user
        public static bool RegisterNewUser(RegisterModel registerModel)
        {
            //setup the request
            RestRequest CreateUserRequest = new RestRequest("/users", Method.POST);
            CreateUserRequest.AddParameter("email", registerModel.UserEmail);
            CreateUserRequest.AddParameter("password", registerModel.UserPassword);
            CreateUserRequest.RequestFormat = DataFormat.Json;
            //get response
            var Response = RestClient.Execute(CreateUserRequest);
            //check response status code
            var StatusCode = Response.StatusCode;
            PrinterClass.printDebugMessage("staus code = " + StatusCode);
            if (StatusCode == CreatedCode)
            {
                //new user created, add user to person table as well
                RestRequest CreatePersonRequest = new RestRequest("/person", Method.POST);
                CreatePersonRequest.AddParameter("firstName", registerModel.FirstName);
                CreatePersonRequest.AddParameter("lastName", registerModel.LastName);
                CreatePersonRequest.AddParameter("email", registerModel.UserEmail);
                //set token
                string token = GetNewToken(registerModel.UserEmail, registerModel.UserPassword);
                RestClient.Authenticator = new JwtAuthenticator(token);
                //get response
                var PersonResponse = RestClient.Execute(CreatePersonRequest);
                //check response code
                var PersonStatusCode = PersonResponse.StatusCode;
                if (PersonStatusCode == CreatedCode)
                {
                    //person has been added to database, send result
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (StatusCode == InternalErrorCode)
            {
                //error adding new user
                return false;
            }
            return false;
        }

        //login attempt
        public static PersonModel GetPersonFromLogin(LoginModel loginModel)
        {
            //setup the query
            String queryString = "?";
            queryString += "email=" + loginModel.LoginEmail;
            //setup the request to check the user table
            RestRequest GetPersonRequest = new RestRequest("/person" + queryString, Method.GET);
            GetPersonRequest.RequestFormat = DataFormat.Json;
            //add token
            RestClient.Authenticator = new JwtAuthenticator(ClientToken);
            //get list
            var PersonResponse = RestClient.Execute<List<PersonModel>>(GetPersonRequest);
            //check the status
            var statuscode = PersonResponse.StatusCode;
            if (statuscode == OKCode)
            {
                PersonModel person = PersonResponse.Data[0];
                return person;
            }
            PersonModel ErrorPerson = new PersonModel();
            ErrorPerson.PersonID = 0;
            return ErrorPerson;
        }
    }
}
