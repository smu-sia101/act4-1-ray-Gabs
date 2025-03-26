using Microsoft.AspNetCore.Mvc;

namespace PetNameGenerator.Controllers
{
    [ApiController]
    [Route("api/petName")]
    public class PetNameController : ControllerBase
    {
        private static string[] DogNames = { "Buddy", "Max", "Charlie", "Rocky", "Rex" };
        private static string[] CatNames = { "Whiskers", "Mittens", "Luna", "Simba", "Tiger" };
        private static string[] BirdNames = { "Tweety", "Sky", "Chirpy", "Raven", "Sunny" };

        private static string[] DogLastNames = { "Barker", "Woofington", "Pawsworth", "Furry", "McSnout" };
        private static string[] CatLastNames = { "Whiskerton", "Meowster", "Purrington", "Clawford", "Felinestein" };
        private static string[] BirdLastNames = { "Featherstone", "Skywalker", "Beakman", "Wingate", "Chirpwell" };

        [HttpPost("generate/{animalType}")]
        public IActionResult Post([FromRoute] string animalType, [FromQuery] bool? twoPart = null)
        {
            if (string.IsNullOrWhiteSpace(animalType))
            {
                return BadRequest(new { error = "The 'animalType' field is required." });
            }

            string[] names;
            switch (animalType.ToLower())
            {
                case "dog":
                    names = DogNames;
                    break;
                case "cat":
                    names = CatNames;
                    break;
                case "bird":
                    names = BirdNames;
                    break;
                default:
                    return BadRequest(new { error = "Invalid animal type. Allowed values: dog, cat, bird." });
            }

            var random = new Random();
            string generatedName = names[random.Next(names.Length)];

            if (twoPart == true) // Removed invalid `/` and corrected condition
            {
                string[] lastNames;
                switch (animalType.ToLower())
                {
                    case "dog":
                        lastNames = DogLastNames;
                        break;
                    case "cat":
                        lastNames = CatLastNames;
                        break;
                    case "bird":
                        lastNames = BirdLastNames;
                        break;
                    default:
                        return BadRequest();
                }

                generatedName += " " + lastNames[random.Next(lastNames.Length)];
            }

            return Ok(new { name = generatedName });
        }
    }
}
