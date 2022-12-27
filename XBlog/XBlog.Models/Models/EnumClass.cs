using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBlog.Models.Models
{
    internal class EnumClass
    {
    }
    public enum Rating
    {
        One=1,
        Two,
        Three,
        Four,
        Five,
    }
    public enum Gender
    {
        Male = 1,
        Female
    }

    public enum Role
    {
        User = 1,
        Authur,
        AuthurAndSeller,
        Admin, // center manager our staff
        SuperAdmin
    }
    public enum ReactionType
    {
        Like=1,
        Love,
        Wow,
        Hate,
        Angry
    }
    public enum Status
    {
        Successful=1,
        Pending,
        Failed,
    }
    public enum PaymentType
    {
        Card = 1,
        Bank,
        Cypto,
    }
    public enum PaymentGateway
    {
        Paystack = 1,
        SwitchWallet,
        BankTranfer,
    }
    public enum OrderStatus
    {
        Cancelled=1,
        AwaitingPayment,
        PaymentConfirmed,
        Shipped,
        Delivered,
    }
}
