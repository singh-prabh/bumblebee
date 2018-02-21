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
        public string AccessToken { get; set; }
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

        //person model
        public static PersonModel MainPerson { get; set; }

        //setup the rest client
        public static void SetupAPIHelper()
        {
            RestClient = new RestClient()
            {
                BaseUrl = new Uri(BaseURL)
            };
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
                string content = AuthResponse.Data.AccessToken;
                ClientToken = content;
                TokenResponse = content;
                //add token
                RestClient.Authenticator = new JwtAuthenticator(ClientToken);
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
            RestRequest GetPersonRequest = new RestRequest("/person" + queryString, Method.GET)
            {
                RequestFormat = DataFormat.Json
            };
            //get list
            var PersonResponse = RestClient.Execute<List<PersonModel>>(GetPersonRequest);
            //check the status
            var statuscode = PersonResponse.StatusCode;
            if (statuscode == OKCode)
            {
                PersonModel person = PersonResponse.Data[0];
                MainPerson = person;
                return person;
            }
            PersonModel ErrorPerson = new PersonModel
            {
                PersonID = 0
            };
            return ErrorPerson;
        }

        //states list for dropdowns
        public static List<StatesModel> GetStateList()
        {
            //setup the request
            RestRequest GetStatesRequest = new RestRequest("/states", Method.GET)
            {
                RequestFormat = DataFormat.Json
            };
            //get list
            var StatesResponse = RestClient.Execute<List<StatesModel>>(GetStatesRequest);
            //get status code
            if (StatesResponse.StatusCode == OKCode)
            {
                return StatesResponse.Data;
            }
            return null;
        }

        //save company for person
        public static bool SaveCompanyForUser(CompanyModel companyModel)
        {
            //first save company in database
            RestRequest PostCompanyRequest = new RestRequest("/company", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            PostCompanyRequest.AddParameter("name", companyModel.CompanyName);
            PostCompanyRequest.AddParameter("website", companyModel.CompanyURL);
            //post comany data
            var CompanyResponse = RestClient.Execute<CompanyModel>(PostCompanyRequest);
            //check status
            var status = CompanyResponse.StatusCode;
            if (CompanyResponse.StatusCode == CreatedCode)
            {
                //get the company id from the response
                CompanyModel createdCompany = CompanyResponse.Data;
                int companyID = createdCompany.CompanyID;
                //add the address 
                RestRequest PostAddressRequest = new RestRequest("/address", Method.POST)
                {
                    RequestFormat = DataFormat.Json
                };
                PostAddressRequest.AddParameter("companyID", companyID);
                PostAddressRequest.AddParameter("street1", companyModel.CompanyStreet1);
                PostAddressRequest.AddParameter("street2", companyModel.CompanyStreet2);
                PostAddressRequest.AddParameter("city", companyModel.CompanyCity);
                PostAddressRequest.AddParameter("zip", companyModel.CompanyZip);
                PostAddressRequest.AddParameter("stateID", companyModel.CompanyState);
                PostAddressRequest.AddParameter("typeID", 2);
                RestClient.Execute(PostAddressRequest);
                //add the phone
                RestRequest PostPhoneRequest = new RestRequest("/phone", Method.POST)
                {
                    RequestFormat = DataFormat.Json
                };
                PostPhoneRequest.AddParameter("companyID", companyID);
                //fix phone number
                string newNumber = companyModel.CompanyPhone.Replace("-", "");
                PostPhoneRequest.AddParameter("number", newNumber);
                PostPhoneRequest.AddParameter("typeID", 2);
                RestClient.Execute(PostPhoneRequest);
                //update person with company
                RestRequest UpdatePersonRequest = new RestRequest("/person/" + MainPerson.PersonID, Method.PUT);
                UpdatePersonRequest.AddParameter("firstName", MainPerson.FirstName);
                UpdatePersonRequest.AddParameter("lastName", MainPerson.LastName);
                UpdatePersonRequest.AddParameter("companyID", companyID);
                UpdatePersonRequest.AddParameter("email", MainPerson.PersonEmail);
                UpdatePersonRequest.AddParameter("finishedOnboarding", true);
                RestClient.Execute(UpdatePersonRequest);
                //add done
                return true;
            }
            return false;
        }

        //get company list
        public static List<CompanyModel> GetCompanyList()
        {
            //setup request
            RestRequest GetCompanyRequest = new RestRequest("/company", Method.GET)
            {
                RequestFormat = DataFormat.Json
            };
            //setup response
            var GetCompanyResponse = RestClient.Execute<List<CompanyModel>>(GetCompanyRequest);
            if (GetCompanyResponse.StatusCode == OKCode)
            {
                return GetCompanyResponse.Data;
            }
            return null;
        }

        //get company for id
        public static AddressModel GetAddressForCompany(int companyID)
        {
            //setup request
            RestRequest GetCompanyAddressRequest = new RestRequest("/address?companyID=" + companyID, Method.GET)
            {
                RequestFormat = DataFormat.Json
            };
            //setup response 
            var GetAddressResponse = RestClient.Execute<List<AddressModel>>(GetCompanyAddressRequest);
            if (GetAddressResponse.StatusCode == OKCode)
            {
                return GetAddressResponse.Data[0];
            }
            return null;
        }

        //get project list
        public static List<ProjectModel> GetProjectList()
        {
            //setup request
            RestRequest GetProjectRequest = new RestRequest("/project", Method.GET)
            {
                RequestFormat = DataFormat.Json
            };
            //setup response
            var GetProjectResponse = RestClient.Execute<List<ProjectModel>>(GetProjectRequest);
            if (GetProjectResponse.StatusCode == OKCode)
            {
                return GetProjectResponse.Data;
            }
            return null;
        }

        //create new project
        public static void CreateProjectForComapany(ProjectModel projectModel)
        {
            
        }
    }
}
