import { useEffect, useState } from "react";
import { api } from "./Apis/ShelterApi";

type props = {
  id: number;
};

const GetShelterName = ({ id }: props) => {
  const [name, setShelterName] = useState<string>("");
  useEffect(() => {
    const getBookName = async () => {
      try {
        const name = await api.GetShelterById(id);
        setShelterName(name.name.charAt(0).toUpperCase() + name.name.slice(1));
      } catch (error) {
        console.error("Error while fetching shelter's name:", error);
      }
    };
    if (id) {
      getBookName();
    }
  }, [id]);

  return <>{name}</>;
};
export default GetShelterName;
