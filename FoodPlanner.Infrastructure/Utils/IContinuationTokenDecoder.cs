namespace FoodPlanner.Infrastructure.Utils
{
    public interface IContinuationTokenEncoding
    {
         public string Encode(ContinuationToken token);
         public ContinuationToken Decode(string token);
    }
}