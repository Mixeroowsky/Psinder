import { useEffect, useState } from "react";
import { Card, CardHeader, CardTitle, CardContent } from "../ui/card";
import { api } from "../../Helpers/Apis/PetsApi";
import { Pet } from "../../Helpers/Interfaces/PetInterface";

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
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6 w-full max-w-7xl px-4">
        {pets.map((pets) => (
          <Card
            key={pets.id}
            className="hover:shadow-lg transition-shadow duration-200"
          >
            <CardHeader>
              <CardTitle>{pets.name}</CardTitle>
            </CardHeader>
            <CardContent>
              <p>{pets.description}</p>
            </CardContent>
          </Card>
        ))}
      </div>
    </div>
  );
};

export default Home;
