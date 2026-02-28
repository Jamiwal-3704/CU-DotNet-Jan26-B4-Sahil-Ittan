using NUnit.Framework;
using EmployeeBonusSolution;
using System;

namespace EmployeeBonusTests
{
    [TestFixture]
    public class EmployeeBonusTests
    {
        // ✅ Test Case 1 – Normal High Performer (No Cap Triggered)
        [Test]
        public void NetAnnualBonus_NormalHighPerformer_ReturnsExpected()
        {
            var emp = new EmployeeBonus
            {
                BaseSalary = 500000,
                PerformanceRating = 5,
                YearsOfExperience = 6,
                DepartmentMultiplier = 1.1m,
                AttendancePercentage = 95
            };

            Assert.AreEqual(123200.00m, emp.NetAnnualBonus);
        }

        // ✅ Test Case 2 – Attendance Penalty Applied
        [Test]
        public void NetAnnualBonus_AttendanceBelow85_AppliesPenalty()
        {
            var emp = new EmployeeBonus
            {
                BaseSalary = 400000,
                PerformanceRating = 4,
                YearsOfExperience = 8,
                DepartmentMultiplier = 1.0m,
                AttendancePercentage = 80
            };

            Assert.AreEqual(60480.00m, emp.NetAnnualBonus);
        }

        // ✅ Test Case 3 – Cap Triggered
        [Test]
        public void NetAnnualBonus_ExceedsCap_Applies40PercentCap()
        {
            var emp = new EmployeeBonus
            {
                BaseSalary = 1_000_000,
                PerformanceRating = 5,
                YearsOfExperience = 15,
                DepartmentMultiplier = 1.5m,
                AttendancePercentage = 95
            };

            Assert.AreEqual(280000.00m, emp.NetAnnualBonus);
        }

        // ✅ Test Case 4 – Zero Salary
        [Test]
        public void NetAnnualBonus_BaseSalaryZero_ReturnsZero()
        {
            var emp = new EmployeeBonus
            {
                BaseSalary = 0
            };

            Assert.AreEqual(0.00m, emp.NetAnnualBonus);
        }

        // ✅ Test Case 5 – Low Performer (Rating 2)
        [Test]
        public void NetAnnualBonus_LowPerformerRating2_ReturnsExpected()
        {
            var emp = new EmployeeBonus
            {
                BaseSalary = 300000,
                PerformanceRating = 2,
                YearsOfExperience = 3,
                DepartmentMultiplier = 1.0m,
                AttendancePercentage = 90
            };

            Assert.AreEqual(13500.00m, emp.NetAnnualBonus);
        }

        // ✅ Test Case 6 – Exact 150,000 Tax Boundary
        [Test]
        public void NetAnnualBonus_Exact150kTaxBoundary_Applies10PercentTax()
        {
            var emp = new EmployeeBonus
            {
                BaseSalary = 600000,
                PerformanceRating = 3,
                YearsOfExperience = 0,
                DepartmentMultiplier = 1.0m,
                AttendancePercentage = 100
            };

            Assert.AreEqual(64800.00m, emp.NetAnnualBonus);
        }

        // ✅ Test Case 7 – High Tax Slab (>300k Without Cap)
        [Test]
        public void NetAnnualBonus_HighTaxSlab_ReturnsExpected()
        {
            var emp = new EmployeeBonus
            {
                BaseSalary = 900000,
                PerformanceRating = 5,
                YearsOfExperience = 11,
                DepartmentMultiplier = 1.2m,
                AttendancePercentage = 100
            };

            Assert.AreEqual(226800.00m, emp.NetAnnualBonus);
        }

        // ✅ Test Case 8 – Rounding Precision
        [Test]
        public void NetAnnualBonus_RoundingPrecision_ReturnsTwoDecimals()
        {
            var emp = new EmployeeBonus
            {
                BaseSalary = 555555,
                PerformanceRating = 4,
                YearsOfExperience = 6,
                DepartmentMultiplier = 1.13m,
                AttendancePercentage = 92
            };

            Assert.AreEqual(118649.88m, emp.NetAnnualBonus);
        }

        // ✅ Invalid Rating Test
        [Test]
        public void NetAnnualBonus_InvalidRating_ThrowsException()
        {
            var emp = new EmployeeBonus
            {
                BaseSalary = 300000,
                PerformanceRating = 7
            };

            Assert.Throws<InvalidOperationException>(() =>
            {
                var _ = emp.NetAnnualBonus;
            });
        }
    }
}