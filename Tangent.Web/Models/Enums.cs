using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Tangent.Web.Models
{
    public enum Gender
    {
        [EnumMember(Value = "Male")]
        M,
        [EnumMember(Value = "Female")]
        F
    }

    public enum ReviewType
    {
        [EnumMember(Value = "Performance Increase")]
        P,
        [EnumMember(Value = "Starting Salary")]
        S,
        [EnumMember(Value = "Annual Increase")]
        A,
        [EnumMember(Value = "Expectation Review")]
        E
    }

    public enum Race
    {
        [EnumMember(Value = "Black African")]
        B,
        [EnumMember(Value = "Coloured")]
        C,
        [EnumMember(Value = "Indian or Asian")]
        I,
        [EnumMember(Value = "White")]
        W,
        [EnumMember(Value = "None Dominant")]
        N,
    }
}
