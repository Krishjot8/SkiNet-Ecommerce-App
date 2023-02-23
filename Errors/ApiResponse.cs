namespace ECommerce_App.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

     
        public int StatusCode { get; set; }
        
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch

            {
                400 => "you have made a bad request",
                401 => "You are not Authorized",
                404 => "The page you are looking for doesn't exist",
                500 => "The server encountered an error anc could not complete your request.",


            _ => null

            };
        }

    }
}


