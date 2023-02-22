namespace MLWebApplication1.Models
{
    public class ModelLabel
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public string Name { get; set; }

        public float Score { get; set; }

        public static List<ModelLabel> GetLabels()
        {
            return new List<ModelLabel>()
            { new ModelLabel { Id = 0, Label = "water_spot", Name = "water_spot" },
                    new ModelLabel { Id = 1, Label = "welding_line", Name = "welding_line" },
                };
        }
    }
}
