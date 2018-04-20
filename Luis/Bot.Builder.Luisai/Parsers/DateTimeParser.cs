using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bot.Builder.Luisai.Parsers
{
    public class DateTimeParser
    {
        public static DateTime ParseDateTime(LuisResult result, EntityRecommendation entity, string luisBuildtinIdentifier)
        {
            DateTime resultDateTime = new DateTime();
            //find buildtin datetime 
            EntityRecommendation build = null;

            var findings = result.Entities.Where(e =>
            {
                return e.Type == luisBuildtinIdentifier && e.StartIndex == entity.StartIndex;
            });

            if (findings.Any())
            {
                build = findings.First();
            }
            
            if (build != null)
            {
                //parse buildtin
                IResolutionParser parse = new ResolutionParser();
                
                Dictionary<string, string> buildResolution = (Dictionary<string, string>)build.Resolution;
                buildResolution.Add("resolution_type", luisBuildtinIdentifier);

                Resolution res;
                BuiltIn.DateTime.DateTimeResolution dtRes;
                if (parse.TryParse(build.Resolution, out res))
                {
                    dtRes = res as BuiltIn.DateTime.DateTimeResolution;

                    if (dtRes != null)
                    {
                        resultDateTime = new DateTime((dtRes.Year.HasValue && dtRes.Year.Value > 0) ? dtRes.Year.Value : resultDateTime.Year,
                            (dtRes.Month.HasValue && dtRes.Month.Value > 0) ? dtRes.Month.Value : resultDateTime.Month,
                            (dtRes.Day.HasValue && dtRes.Day.Value > 0) ? dtRes.Day.Value : resultDateTime.Day,
                            (dtRes.Hour.HasValue && dtRes.Hour.Value >= 0) ? dtRes.Hour.Value : resultDateTime.Hour,
                            (dtRes.Minute.HasValue && dtRes.Minute.Value >= 0) ? dtRes.Minute.Value : resultDateTime.Minute,
                            (dtRes.Second.HasValue && dtRes.Second.Value >= 0) ? dtRes.Second.Value : resultDateTime.Second);
                    }
                }
            }

            if (resultDateTime.Equals(new DateTime()))
            {
                //clear all whitespaces
                string text = new string(entity.Entity.Where(c => !char.IsWhiteSpace(c)).ToArray());

                //try using Text to parse with Chronic
                var chronic = new Chronic.Parser().Parse(text);
                if (chronic != null)
                {
                    //Chronic success
                    resultDateTime = chronic.ToTime();
                }
            }

            return resultDateTime;
        }

    }
}
