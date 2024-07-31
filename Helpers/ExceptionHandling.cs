using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LojinhaDaPaulinhaAPI.Helpers
{
    public class ExceptionHandling
    {
        public static string GetCustomErrorMessage(Exception ex)
        {
            if (ex is DbUpdateException && ex.InnerException is SqlException innerEx)
            {
                var innerExMessage = innerEx.Message;

                // Check for Index Unique violation (on Create or Update).
                if (innerExMessage.Contains("IX_"))
                {
                    return "This object already exists.";
                }

                // Check for Foreign Key violation.
                if (innerExMessage.Contains("FK_"))
                {
                    // On Delete.
                    if (innerExMessage.Contains("DELETE"))
                    {
                        return "This object won't be deleted because it has dependent objects.";
                    }

                    // On Create or Update.
                    return "The other object this one is dependent on does not exist in the database.";
                }
            }

            return null;
        }
    }
}
