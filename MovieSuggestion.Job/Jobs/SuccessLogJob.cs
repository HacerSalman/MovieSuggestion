using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieSuggestion.Job.Jobs
{
    /// <summary>Test Job</summary>
    public class SuccessLogJob
    {
        public SuccessLogJob()
        {
        }

        public void Process()
        {
            try
            {
                Console.WriteLine(@"👌 Job Started SuccessLogJob : " + DateTime.Now);
       
                Console.WriteLine(@"👌 Job Finished SuccessLogJob : " + DateTime.Now);
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"🤔 Job Exception SuccessLogJob " + ex);
                throw;
            }
        }
    }
}
