namespace BankingApp.Models
{
    public class Reputation
    {
        public string? email { get; set; }
        public string? did_you_mean { get; set; }
        public string? user { get; set; }
        public string? domain { get; set; }
        public double? score { get; set; }

        public bool free { get; set; }


        public bool? can_connect_smtp { get; set; }
        public bool? is_catch_all { get; set; }
        public bool is_deliverable { get; set; }
        public string? is_disabled { get; set; }
        public bool? is_disposable { get; set; }
        public string? is_inbox_full { get; set; }
        public bool? is_role_account { get; set; }
        public bool? mx_records { get; set; }
        public bool? syntax_valid { get; set; }

    }
}
