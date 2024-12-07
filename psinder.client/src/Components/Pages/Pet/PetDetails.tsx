import { Button } from "@/Components/ui/button";
import {
  CardHeader,
  CardTitle,
  CardDescription,
  CardContent,
  CardFooter,
  Card,
} from "@/Components/ui/card";
import { api } from "@/Helpers/Apis/PetsApi";
import { Pet } from "@/Helpers/Interfaces/PetInterface";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

const PetDetails = () => {
  const [pet, setPet] = useState<Pet>({
    id: 0,
    name: "",
    age: 0,
    sex: 0,
    breedType: 0,
    description: "",
    photoUrl: "",
    shelterId: 0,
  });
  const { id } = useParams();
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

  useEffect(() => {
    const petData = async () => {
      if (id != null) {
        try {
          const data = await api.GetPetById(parseInt(id));
          setPet({
            id: data.id,
            name: data.name,
            age: data.age,
            sex: data.sex,
            breedType: data.breedType,
            description: data.description,
            photoUrl: data.photoUrl,
            shelterId: data.shelterId,
          });
        } catch {}
      }
    };
    petData();
  }, []);
  return (
    <div className="mt-10">
      <Card className="max-w-xl mx-auto shadow-lg border rounded-lg overflow-hidden">
        <CardHeader className="flex flex-col items-center text-center">
          {pet.photoUrl ? (
            <img
              src={`https://localhost:7290/uploads/${pet.photoUrl}`}
              alt={pet.name}
              className="max-w-sm w-24 h-24 sm:w-32 sm:h-32 md:w-32 md:h-32 object-cover "
            />
          ) : (
            <p>No photo available</p>
          )}
          <CardTitle className="text-xl font-bold">{pet.name}</CardTitle>
          <CardDescription className="text-sm ">
            {pet.description}
          </CardDescription>
        </CardHeader>
        <CardContent className="space-y-4 p-4">
          <div className="flex justify-between items-center">
            <span className="text-gray-600 font-medium">Breed:</span>
            {breedType(pet.breedType)}
          </div>
          <div className="flex justify-between items-center">
            <span className="text-gray-600 font-medium">Sex:</span>
            {pet.sex === 0 ? "Male" : "Female"}
          </div>
          <div className="flex justify-between items-center">
            <span className="text-gray-600 font-medium">Age:</span>
            <span>{pet.age} years</span>
          </div>
          <div className="flex justify-between items-center">
            <span className="text-gray-600 font-medium">Shelter:</span>
            <span className="text-blue-600 font-semibold">{pet.shelterId}</span>
          </div>
        </CardContent>
        <CardFooter className="flex justify-center">
          <Button variant="default" className="w-full">
            Contact Shelter
          </Button>
        </CardFooter>
      </Card>
    </div>
  );
};

export default PetDetails;
