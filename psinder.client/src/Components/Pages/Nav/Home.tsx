import { useEffect, useState } from "react";
import { Card, CardHeader, CardTitle, CardContent } from "../../ui/card";
import { api as petsApi } from "../../../Helpers/Apis/PetsApi";
import { api as sheltersApi } from "../../../Helpers/Apis/ShelterApi";
import { Pet } from "../../../Helpers/Interfaces/PetInterface";
import { Shelter } from "@/Helpers/Interfaces/ShelterInterface";

const Home = () => {
  const [pets, setPets] = useState<Pet[]>([]);
  const [shelters, setShelters] = useState<Shelter[]>([]);
  useEffect(() => {
    const fetchPets = async () => {
      const data = await petsApi.GetAllPets();
      const randomPets = data.sort(() => 0.5 - Math.random());
      setPets(randomPets.slice(0, 3));
    };
    const fetchShelters = async () => {
      const data = await sheltersApi.GetAllShelters();
      const randomShelters = data.sort(() => 0.5 - Math.random());
      setShelters(randomShelters.slice(0, 3));
    };
    fetchPets();
    fetchShelters();
  }, []);
  return (
    <div className="">
      <h2 className="m-8 pl-10">Meet our Pets!</h2>
      <div className="m-8 pl-10 grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6 w-full max-w-7xl px-4">
        {pets.map((pets) => (
          <Card
            key={pets.id}
            className="shadow-md hover:shadow-lg transition-shadow duration-200"
          >
            <CardHeader>
              <CardTitle className="text-xl font-bold">{pets.name}</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="flex justify-between">
                <div>
                  <p className="text-base font-normal">{pets.description}</p>
                </div>
                <div className="text-center rounded-md border transform -translate-y-10 mr-6">
                  {pets.photoUrl ? (
                    <img
                      src={`https://localhost:7290/uploads/${pets.photoUrl}`}
                      alt={pets.name}
                      className="max-w-sm w-24 h-24 sm:w-32 sm:h-32 md:w-32 md:h-32 object-cover "
                    />
                  ) : (
                    <p>No photo available</p>
                  )}
                </div>
              </div>
            </CardContent>
          </Card>
        ))}
      </div>
      <h2 className="m-8 pl-10">Check our shelters</h2>
      <div className="m-8 pl-10 grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6 w-full max-w-7xl px-4">
        {shelters.map((shelter) => (
          <Card
            key={shelter.userId}
            className="shadow-md hover:shadow-lg transition-shadow duration-200"
          >
            <CardHeader>
              <CardTitle className="text-xl font-bold">
                {shelter.name}
              </CardTitle>
            </CardHeader>
            <CardContent>
              <div className="mb-6">
                <p className="text-base font-normal">{shelter.city}</p>
              </div>
            </CardContent>
          </Card>
        ))}
      </div>
    </div>
  );
};

export default Home;
