namespace Adaptit.Training.JobVacancy.Backend.Options;

using System.ComponentModel.DataAnnotations;

public class PamStillingOptions
{
  internal const string Section = "PamStillingApi";
  [Required(AllowEmptyStrings = false)]
  public string ApiKey { get; set; }
  [Required]
  public Uri BaseAddress { get; set; }
}
