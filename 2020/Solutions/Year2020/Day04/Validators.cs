using System;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020.Day04
{
    public static class Validators
    {
        private static bool GetValueByKey(string passport, string key, out string value)
        {
            value = string.Empty;
            if (!passport.Contains($"{key}:")) return false;
            value = passport.Split($"{key}:")[1].Split(new[] {" ", "\n"}, StringSplitOptions.None)[0];
            return true;
        }

        public static bool BirthYearIsValid(string passport)
        {
            if (GetValueByKey(passport, PassportConstants.BirthYear, out var birthYear))
            {
                if (int.TryParse(birthYear, out var birthYearInt))
                {
                    if (birthYearInt < 1920 || birthYearInt > 2002) return false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        public static bool IssueYearIsValid(string passport)
        {
            if (GetValueByKey(passport, PassportConstants.IssueYear, out var issueYear))
            {
                if (int.TryParse(issueYear, out var issueYearInt))
                {
                    if (issueYearInt < 2010 || issueYearInt > 2020) return false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        public static bool ExpirationYearIsValid(string passport)
        {
            if (GetValueByKey(passport, PassportConstants.ExpirationYear, out var expirationYear))
            {
                if (int.TryParse(expirationYear, out var expirationYearInt))
                {
                    if (expirationYearInt < 2020 || expirationYearInt > 2030) return false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        public static bool HeightIsValid(string passport)
        {
            if (GetValueByKey(passport, PassportConstants.Height, out var height))
            {
                var value = height.Split(new[] {"cm", "in"}, StringSplitOptions.None)[0];
                var unit = height.Contains("cm") ? "cm" : "in";

                if (int.TryParse(value, out var valueInt))
                {
                    if (unit == "cm")
                    {
                        if (valueInt < 150 || valueInt > 193) return false;
                    }
                    else if (valueInt < 59 || valueInt > 76)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        public static bool HairColorIsValid(string passport)
        {
            if (GetValueByKey(passport, PassportConstants.HairColor, out var hairColor))
            {
                var match = Regex.Match(hairColor, @"#[a-f0-9]{6,}");
                if (!match.Success) return false;
            }
            else
            {
                return false;
            }

            return true;
        }

        public static bool EyeColorIsValid(string passport)
        {
            if (GetValueByKey(passport, PassportConstants.EyeColor, out var eyeColor))
            {
                var match = Regex.Match(eyeColor, @"(amb|blu|brn|gry|grn|hzl|oth)");
                if (!match.Success) return false;
            }
            else
            {
                return false;
            }

            return true;
        }


        public static bool PassportIdIsValid(string passport)
        {
            if (GetValueByKey(passport, PassportConstants.PassportId, out var passportId))
            {
                if (passportId.Length != 9) return false;
                var match = Regex.Match(passportId, @"\d{9}");
                if (!match.Success) return false;
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}