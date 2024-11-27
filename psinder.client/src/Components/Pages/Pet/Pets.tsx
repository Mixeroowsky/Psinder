import { api } from "@/Helpers/Apis/PetsApi";
import { Pet } from "@/Helpers/Interfaces/PetInterface";
import Spinner from "@/Helpers/Spinner";
import { useEffect, useState } from "react";
import GetShelterName from "@/Helpers/GetShelterName";
import { Card, CardHeader, CardTitle, CardContent } from "@/Components/ui/card";

const Pets = () => {
  const [isLoading, setIsLoading] = useState(false);
  const [pets, setPets] = useState<Pet[]>([]);

  useEffect(() => {
    const fetchPets = async () => {
      try {
        setIsLoading(true);
        const pets = await api.GetAllPets();
        setPets(pets);
      } catch (error) {
      } finally {
        setIsLoading(false);
      }
    };
    fetchPets();
  }, []);

  const breedType = (type: number) => {
    switch (type) {
      case 0:
        return "Dog";
      case 1:
        return "Cat";
      case 2:
        return "Other";
      default:
        return "unknown";
    }
  };

  const sex = (type: number) => {
    switch (type) {
      case 0:
        return "Male";
      case 1:
        return "Female";
      default:
        return "unknown";
    }
  };

  return (
    <div className="flex p-10 justify-center ">
      <div className="p-5 max-w-screen-xl rounded-md border w-6/12">
        {isLoading ? (
          <div className="text-center">
            <Spinner />
          </div>
        ) : pets.length > 0 ? (
          pets.map((pet) => (
            <Card
              key={pet.id}
              className="shadow-md hover:shadow-lg transition-shadow duration-200 m-5 "
            >
              <CardHeader className="w-48">
                <CardTitle className="text-3xl ">{pet.name}</CardTitle>
              </CardHeader>
              <CardContent className="flex justify-between mb-5">
                <div>
                  <div className="text-lg p-4">{pet.description}</div>
                  <p className="mt-10">
                    Breed type:<span> {breedType(pet.breedType)}</span>
                  </p>
                  <p>
                    Age: <span>{pet.age}</span>
                  </p>
                  <p>
                    Sex:<span> {sex(pet.sex)}</span>
                  </p>
                  <p>
                    At shelter:
                    <span>
                      {" "}
                      <GetShelterName id={pet.shelterId} />
                    </span>
                  </p>
                </div>
                <div className="text-center  mr-10 rounded-md p-3 border transform -mt-10">
                  {pet.photoUrl ? (
                    <img
                      src={`https://localhost:7290/uploads/${pet.photoUrl}`}
                      alt={pet.name}
                      className="max-w-sm  w-24 h-24 sm:w-32 sm:h-32 md:w-64 md:h-64 object-cover"
                    />
                  ) : (
                    <p>No photo available</p>
                  )}
                </div>
              </CardContent>
            </Card>
          ))
        ) : (
          <div className="mt-5 text-center text-2xl">No pets to adopt</div>
        )}
      </div>
    </div>
  );
};

export default Pets;
