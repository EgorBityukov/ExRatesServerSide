namespace ExRatesServerSide.Models.ResponseModels
{
    public class RateShort
    {
        public int Cur_ID { get; set; }
        public DateTime Date { get; set; }
        public decimal? Cur_OfficialRate { get; set; }
    }
}
