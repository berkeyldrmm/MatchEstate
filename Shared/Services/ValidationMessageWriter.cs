namespace BusinessLayer.Services
{
    public static class ValidationMessageWriter
    {
        public static string MessageWriter(IEnumerable<string> allErrors)
        {
            string message = "<ul>";
            foreach (var error in allErrors)
            {
                message += "<li>" + error + "</li>";
            }
            message += "</ul>";

            return message;
        }
    }
}
