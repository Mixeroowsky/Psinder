import { useEffect, useState } from "react";
import { Card, CardHeader, CardTitle, CardContent } from "../../ui/card";
import { api } from "../../../Helpers/Apis/PetsApi";
import { Pet } from "../../../Helpers/Interfaces/PetInterface";

const Home = () => {
  const [pets, setPets] = useState<Pet[]>([]);
  useEffect(() => {
    const fetchData = async () => {
      const data = await api.GetAllPets();
      const randomPets = data.sort(() => 0.5 - Math.random());
      setPets(randomPets.slice(0, 3));
    };
    fetchData();
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
    </div>
  );
};

export default Home;
