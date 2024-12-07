import {
  CardHeader,
  CardTitle,
  CardDescription,
  CardContent,
  CardFooter,
  Card,
} from "@/Components/ui/card";
import { api as petApi } from "@/Helpers/Apis/PetsApi";
import { api as shelterApi } from "@/Helpers/Apis/ShelterApi";
import { Pet } from "@/Helpers/Interfaces/PetInterface";
import { Shelter } from "@/Helpers/Interfaces/ShelterInterface";
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

  const [shelter, setShelter] = useState<Shelter>({
    name: "",
    city: "",
    postCode: "",
    street: "",
    buildingNumber: 0,
    apartmentNumber: 0,
    phoneNumber: "",
    email: "",
    userId: 0,
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
          const petData = await petApi.GetPetById(parseInt(id));
          setPet({
            id: petData.id,
            name: petData.name,
            age: petData.age,
            sex: petData.sex,
            breedType: petData.breedType,
            description: petData.description,
            photoUrl: petData.photoUrl,
            shelterId: petData.shelterId,
          });
          if (petData.shelterId != null) {
            const shelterData = await shelterApi.GetShelterById(
              petData.shelterId
            );
            setShelter({
              name: shelterData.name,
              city: shelterData.city,
              postCode: shelterData.postCode,
              street: shelterData.street,
              buildingNumber: shelterData.buildingNumber,
              apartmentNumber: shelterData.apartmentNumber,
              phoneNumber: shelterData.phoneNumber,
              email: shelterData.email,
              userId: shelterData.userId,
            });
          }
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
        </CardContent>
        <hr />
        <CardFooter className="flex flex-col justify-between ">
          <div>
            <h3>Shelter contact:</h3>
          </div>
          <div className="flex flex-col space-y-4 p-4 flex-grow  w-full justify-between">
            <div className="flex justify-between items-center">
              <span className="text-gray-600 font-medium">Name:</span>
              {shelter.name}
            </div>
            <div className="flex justify-between items-center">
              <span className="text-gray-600 font-medium">City:</span>
              {shelter.city}
            </div>
            <div className="flex justify-between items-center">
              <span className="text-gray-600 font-medium">Address:</span>
              <span>
                {shelter.street + " " + shelter.buildingNumber}/
                {shelter.apartmentNumber}{" "}
              </span>
            </div>
            <div className="flex justify-between items-center">
              <span className="text-gray-600 font-medium">Phone number:</span>
              <span className="text-blue-600 font-semibold">
                {shelter.phoneNumber}
              </span>
            </div>
            <div className="flex justify-between items-center">
              <span className="text-gray-600 font-medium">Email address:</span>
              <span className="text-blue-600 font-semibold">
                {shelter.email}
              </span>
            </div>
          </div>
        </CardFooter>
      </Card>
    </div>
  );
};

export default PetDetails;
