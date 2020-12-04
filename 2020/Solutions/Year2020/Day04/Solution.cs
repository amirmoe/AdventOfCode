using System.Linq;

namespace AdventOfCode.Solutions.Year2020.Day04
{
    internal class Day04 : ASolution
    {
        public Day04() : base(04, 2020, "Passport Processing")
        {
            // DebugInput = "pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980\nhcl:#623a2f\n\neyr:2029 ecl:blu cid:129 byr:1989\niyr:2014 pid:896056539 hcl:#a97842 hgt:165cm\n\nhcl:#888785\nhgt:164cm byr:2001 iyr:2015 cid:88\npid:545766238 ecl:hzl\neyr:2022\n\niyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719";
        }

        protected override string SolvePartOne()
        {
            var passports = Input.SplitByNewline(false, "\n\n");
            var count = passports.Count(Passport1IsValid);
            return count.ToString();
        }

        protected override string SolvePartTwo()
        {
            var passports = Input.SplitByNewline(false, "\n\n");
            var count = passports.Count(Passport2IsValid);
            return count.ToString();
        }

        private static bool Passport1IsValid(string passport)
        {
            return passport.Contains($"{PassportConstants.BirthYear}:") &&
                   passport.Contains($"{PassportConstants.ExpirationYear}:") &&
                   passport.Contains($"{PassportConstants.IssueYear}:") &&
                   passport.Contains($"{PassportConstants.Height}:") &&
                   passport.Contains($"{PassportConstants.EyeColor}:") &&
                   passport.Contains($"{PassportConstants.HairColor}:") &&
                   passport.Contains($"{PassportConstants.PassportId}:");
        }

        private static bool Passport2IsValid(string passport)
        {
            return Validators.BirthYearIsValid(passport) && Validators.ExpirationYearIsValid(passport) &&
                   Validators.IssueYearIsValid(passport) && Validators.HeightIsValid(passport) &&
                   Validators.HairColorIsValid(passport) && Validators.EyeColorIsValid(passport) &&
                   Validators.PassportIdIsValid(passport);
        }
    }
}