namespace RateLimitingBankAPI.Models
{
    public enum Policy
    {
        UserBasedPolicy,
        ConcurrencyPolicy,
        FixedWindowPolicy,
        SlidingWindowPolicy,
        TokenBucketPolicy,
        GlobalPolicy
    }
}
