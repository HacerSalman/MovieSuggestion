using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSuggestion.Data.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace MovieSuggestion.Core.Utils
    {
        public class EnvironmentVariable
        {

            private static EnvironmentVariable _configuration;

            public static EnvironmentVariable GetConfiguration()
            {
                if (_configuration != null)
                    return _configuration;
                _configuration = new EnvironmentVariable();
                LoadConfiguration();
                return _configuration;
            }

            public string DbConnection { get; private set; }
            public string JwtKey { get; private set; }
            public string JwtValidIssuer { get; private set; }
            public string JwtValidAudience { get; private set; }
            public string HangfireUserName { get; private set; }
            public string HangfireUserPassword { get; private set; }
            public string MovieClientApiKey { get; private set; }
            public string MovieClientBaseUrl { get; private set; }
            public string MailFrom { get; private set; }
            public string SmtpHost { get; private set; }
            public string SmtpPass { get; private set; }

            private static void LoadConfiguration()
            {
                GetConfiguration().DbConnection = Environment.GetEnvironmentVariable("MOVIE_SUGGESTION_DB_CONNECTION");
                GetConfiguration().JwtKey = Environment.GetEnvironmentVariable("MOVIE_SUGGESTION_JWT_KEY");
                GetConfiguration().JwtValidIssuer = Environment.GetEnvironmentVariable("MOVIE_SUGGESTION_VALID_ISSUER");
                GetConfiguration().JwtValidAudience = Environment.GetEnvironmentVariable("MOVIE_SUGGESTION_VALID_AUDIENCE");
                GetConfiguration().HangfireUserName = Environment.GetEnvironmentVariable("MOVIE_SUGGESTION_HANGFIRE_USERNAME");
                GetConfiguration().HangfireUserPassword = Environment.GetEnvironmentVariable("MOVIE_SUGGESTION_HANGFIRE_PASSWORD");
                GetConfiguration().MovieClientBaseUrl = Environment.GetEnvironmentVariable("MOVIE_CLIENT_BASE_URL");
                GetConfiguration().MovieClientApiKey = Environment.GetEnvironmentVariable("MOVIE_CLIENT_API_KEY");
                GetConfiguration().MailFrom = Environment.GetEnvironmentVariable("MOVIE_SUGGESTION_MAIL_FROM");
                GetConfiguration().SmtpHost = Environment.GetEnvironmentVariable("MOVIE_SUGGESTION_SMTP_HOST");
                GetConfiguration().SmtpPass = Environment.GetEnvironmentVariable("MOVIE_SUGGESTION_SMTP_PASS");
            }
        }
    }

}
