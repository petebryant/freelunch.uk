using freelunch.uk.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;

namespace freelunch.uk.Common
{
    public static class ValidationHelper
    {
        public static string GetValidationResults(object entity)
        {
            StringBuilder output = new StringBuilder();

            using (var context = new ApplicationDbContext())
            {
                var result = context.Entry(entity).GetValidationResult();

                foreach (DbValidationError error in result.ValidationErrors)
                {
                    output.AppendLine(error.ErrorMessage);
                }
            }

            return output.ToString();
        }
    }
}