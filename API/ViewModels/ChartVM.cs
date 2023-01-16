namespace API.ViewModels
{
    public class ChartVM
    {
        public string[] Labels { get; set; }
        public int[] Series { get; set; }

        public ChartVM(string[] labels, int[] series)
        {
            Labels = labels;
            Series = series;
        }
    }
}
